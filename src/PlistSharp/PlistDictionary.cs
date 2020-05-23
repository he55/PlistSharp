using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace PlistSharp
{
    public class PlistDictionary : PlistStructure, IEnumerable<KeyValuePair<string, PlistNode>>
    {
        private readonly IDictionary<string, PlistNode> _map = new Dictionary<string, PlistNode>();

        public PlistDictionary(PlistStructure? parent = null)
        {
            _node = LibPlist.plist_new_dict();
            _parent = parent;
        }

        public PlistDictionary(plist_t node, PlistStructure? parent = null)
        {
            _node = node;
            _parent = parent;
            Fill();
        }

        public ICollection<string> Keys => _map.Keys;

        public ICollection<PlistNode> Values => _map.Values;

        public int Count => _map.Count;

        public PlistNode this[string key]
        {
            get => _map[key];
            set
            {
                PlistNode copy = value.Copy();
                copy._parent = this;
                LibPlist.plist_dict_set_item(_node, key, copy._node);
                _map[key] = copy;
            }
        }

        public void Add(string key, PlistNode value)
        {
            PlistNode copy = value.Copy();
            copy._parent = this;
            LibPlist.plist_dict_set_item(_node, key, copy._node);
            _map.Add(key, copy);
        }

        public bool ContainsKey(string key)
        {
            return _map.ContainsKey(key);
        }

        public bool Remove(string key)
        {
            LibPlist.plist_dict_remove_item(_node, key);
            return _map.Remove(key);
        }

        public bool TryGetValue(string key, out PlistNode value)
        {
            return _map.TryGetValue(key, out value);
        }

        public void Clear()
        {
            foreach (string key in _map.Keys)
            {
                LibPlist.plist_dict_remove_item(_node, key);
            }
            _map.Clear();
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

        public override PlistNode Copy() => new PlistDictionary(LibPlist.plist_copy(_node));

        protected override void Fill()
        {
            LibPlist.plist_dict_new_iter(_node, out plist_dict_iter it);

            while (true)
            {
                LibPlist.plist_dict_next_item(_node, it, out IntPtr key, out plist_t subnode);
                if (key == IntPtr.Zero)
                {
                    break;
                }

                string dicKey = Marshal.PtrToStringUTF8(key);
                _map[dicKey] = FromPlist(subnode, this)!;

                Marshal.FreeHGlobal(key);
            }

            Marshal.FreeHGlobal(it);
        }
    }
}
