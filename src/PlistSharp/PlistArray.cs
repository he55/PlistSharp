using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static Interop.LibPlist;

namespace PlistSharp
{
    public class PlistArray : PlistStructure, IList<PlistNode>
    {
        private readonly IList<PlistNode> _array = new List<PlistNode>();

        public PlistArray(PlistNode? parent = null)
            : base(plist_type.PLIST_ARRAY, parent)
        {
        }

        public PlistArray(plist_t node, PlistNode? parent = null)
            : base(parent)
        {
            _node = node;
            array_fill(_node);
        }

        public PlistArray(PlistArray a)
            : base()
        {
            _node = plist_copy(a.GetPlist());
            array_fill(_node);
        }

        /// <inheritdoc />
        public int Count => _array.Count;

        /// <inheritdoc />
        public bool IsReadOnly => false;

        /// <inheritdoc />
        public PlistNode this[int index]
        {
            get => _array[index];
            set
            {
                PlistNode clone = value.Clone();
                UpdateNodeParent(clone);
                plist_array_insert_item(_node, clone.GetPlist(), (uint)index);
                _array[index] = clone;
            }
        }

        /// <inheritdoc />
        public int IndexOf(PlistNode item)
        {
            return _array.IndexOf(item);
        }

        /// <inheritdoc />
        public void Insert(int index, PlistNode item)
        {
            PlistNode clone = item.Clone();
            UpdateNodeParent(clone);
            plist_array_insert_item(_node, clone.GetPlist(), (uint)index);
            _array.Insert(index, clone);
        }

        /// <inheritdoc />
        public void RemoveAt(int index)
        {
            plist_array_remove_item(_node, (uint)index);
            _array.RemoveAt(index);
        }

        /// <inheritdoc />
        public void Add(PlistNode item)
        {
            PlistNode clone = item.Clone();
            UpdateNodeParent(clone);
            plist_array_append_item(_node, clone.GetPlist());
            _array.Add(clone);
        }

        /// <inheritdoc />
        public void Clear()
        {
            for (int i = 0; i < _array.Count; i++)
            {
                plist_array_remove_item(_node, 0);
            }
            _array.Clear();
        }

        /// <inheritdoc />
        public bool Contains(PlistNode item)
        {
            return _array.Contains(item);
        }

        /// <inheritdoc />
        public void CopyTo(PlistNode[] array, int arrayIndex)
        {
            PlistNode[] clones = new PlistNode[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                PlistNode clone = array[i].Clone();
                UpdateNodeParent(clone);
                plist_array_insert_item(_node, clone.GetPlist(), (uint)(arrayIndex + i));
                clones[i] = clone;
            }
            _array.CopyTo(clones, arrayIndex);
        }

        /// <inheritdoc />
        bool ICollection<PlistNode>.Remove(PlistNode item)
        {
            int index = _array.IndexOf(item);
            plist_array_remove_item(_node, (uint)index);
            return _array.Remove(item);
        }

        /// <inheritdoc />
        public IEnumerator<PlistNode> GetEnumerator()
        {
            return _array.GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _array.GetEnumerator();
        }

        public override PlistNode Clone()
        {
            return new PlistArray(this);
        }

        private void array_fill(plist_t node)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                uint length = plist_array_get_size(node);
                for (uint i = 0; i < length; i++)
                {
                    plist_t item = plist_array_get_item(node, i);
                    _array.Add(PlistNode.FromPlist(item, this)!);
                }
                return;
            }

            plist_array_new_iter(node, out plist_array_iter iter);

            plist_t subnode;
            while (true)
            {
                subnode = (plist_t)IntPtr.Zero;
                plist_array_next_item(node, iter, out subnode);
                if (subnode == IntPtr.Zero)
                {
                    break;
                }
                _array.Add(PlistNode.FromPlist(subnode, this)!);
            }
            Marshal.FreeHGlobal(iter);
        }
    }
}