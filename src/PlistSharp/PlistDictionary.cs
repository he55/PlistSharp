using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static Interop.LibPlist;

namespace PlistSharp
{
    public class PlistDictionary : PlistStructure, IDictionary<string, PlistNode>
    {
        private readonly IDictionary<string, PlistNode> _map = new Dictionary<string, PlistNode>();

        public PlistDictionary(PlistNode? parent = null)
            : base(plist_type.PLIST_DICT, parent)
        {
        }

        public PlistDictionary(plist_t node, PlistNode? parent = null)
            : base(parent)
        {
            _node = node;
            dictionary_fill(_node);
        }

        public PlistDictionary(PlistDictionary d)
            : base()
        {
            _node = plist_copy(d.GetPlist());
            dictionary_fill(_node);
        }

        /// <inheritdoc />
        public ICollection<string> Keys => _map.Keys;

        /// <inheritdoc />
        public ICollection<PlistNode> Values => _map.Values;

        /// <inheritdoc />
        public int Count => _map.Count;

        /// <inheritdoc />
        public bool IsReadOnly => false;

        /// <inheritdoc />
        public PlistNode this[string key]
        {
            get => _map[key];
            set
            {
                PlistNode clone = value.Clone();
                UpdateNodeParent(clone);
                plist_dict_set_item(_node, key, clone.GetPlist());
                _map[key] = clone;
            }
        }

        /// <inheritdoc />
        public void Add(string key, PlistNode value)
        {
            PlistNode clone = value.Clone();
            UpdateNodeParent(clone);
            plist_dict_set_item(_node, key, clone.GetPlist());
            _map.Add(key, clone);
        }

        /// <inheritdoc />
        public bool ContainsKey(string key)
        {
            return _map.ContainsKey(key);
        }

        /// <inheritdoc />
        public bool Remove(string key)
        {
            plist_dict_remove_item(_node, key);
            return _map.Remove(key);
        }

        /// <inheritdoc />
        public bool TryGetValue(string key, out PlistNode value)
        {
            return _map.TryGetValue(key, out value);
        }

        /// <inheritdoc />
        public void Add(KeyValuePair<string, PlistNode> item)
        {
            PlistNode clone = item.Value.Clone();
            UpdateNodeParent(clone);
            plist_dict_set_item(_node, item.Key, clone.GetPlist());
            _map.Add(KeyValuePair.Create(item.Key, clone));
        }

        /// <inheritdoc />
        public void Clear()
        {
            foreach (var key in _map.Keys)
            {
                plist_dict_remove_item(_node, key);
            }
            _map.Clear();
        }

        /// <inheritdoc />
        public bool Contains(KeyValuePair<string, PlistNode> item)
        {
            return _map.Contains(item);
        }

        /// <inheritdoc />
        public void CopyTo(KeyValuePair<string, PlistNode>[] array, int arrayIndex)
        {
            KeyValuePair<string, PlistNode>[] clones = new KeyValuePair<string, PlistNode>[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                PlistNode clone = array[i].Value.Clone();
                UpdateNodeParent(clone);
                plist_dict_set_item(_node, array[i].Key, clone.GetPlist());
                clones[i] = KeyValuePair.Create(array[i].Key, clone);
            }
            _map.CopyTo(clones, arrayIndex);
        }

        /// <inheritdoc />
        public bool Remove(KeyValuePair<string, PlistNode> item)
        {
            plist_dict_remove_item(_node, item.Key);
            return _map.Remove(item);
        }

        /// <inheritdoc />
        public IEnumerator<KeyValuePair<string, PlistNode>> GetEnumerator()
        {
            return _map.GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _map.GetEnumerator();
        }

        public override PlistNode Clone()
        {
            return new PlistDictionary(this);
        }

        // public PlistNode? Begin()
        // {
        //     foreach (var item in _map)
        //     {
        //         return item.Value;
        //     }
        //     return null;
        // }

        // public PlistNode? End()
        // {
        //     PlistNode? node = null;
        //     foreach (var item in _map)
        //     {
        //         node = item.Value;
        //     }
        //     return node;
        // }

        // public override void Remove(PlistNode node)
        // {
        //     plist_dict_get_item_key(node.GetPlist(), out IntPtr key);
        //     plist_dict_remove_item(_node, key);

        //     string dicKey = Marshal.PtrToStringUTF8(key);
        //     Marshal.FreeHGlobal(key);

        //     _map.Remove(dicKey);
        // }

        // public string GetNodeKey(PlistNode node)
        // {
        //     foreach (var item in _map)
        //     {
        //         if (item.Value == node)
        //         {
        //             return item.Key;
        //         }
        //     }
        //     return string.Empty;
        // }

        private void dictionary_fill(plist_t node)
        {
            plist_dict_new_iter(node, out plist_dict_iter it);

            plist_t subnode;
            while (true)
            {
                subnode = (plist_t)IntPtr.Zero;
                plist_dict_next_item(node, it, out IntPtr key, out subnode);
                if (key == IntPtr.Zero || subnode == IntPtr.Zero)
                {
                    break;
                }

                string dicKey = Marshal.PtrToStringUTF8(key);
                _map[dicKey] = PlistNode.FromPlist(subnode, this)!;

                Marshal.FreeHGlobal(key);
            }

            Marshal.FreeHGlobal(it);
        }
    }
}
