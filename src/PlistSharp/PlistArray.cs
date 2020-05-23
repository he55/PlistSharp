using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace PlistSharp
{
    public class PlistArray : PlistStructure, IEnumerable<PlistNode>
    {
        private readonly IList<PlistNode> _array = new List<PlistNode>();

        public PlistArray(PlistStructure? parent = null)
        {
            CreatePlistNode(plist_type.PLIST_ARRAY, parent);
        }

        public PlistArray(plist_t node, PlistStructure? parent = null)
        {
            _node = node;
            _parent = parent;
            array_fill(_node);
        }

        public int Count => _array.Count;

        public PlistNode this[int index]
        {
            get => _array[index];
            set
            {
                PlistNode clone = value.Clone();
                clone._parent = this;
                LibPlist.plist_array_insert_item(_node, clone._node, (uint)index);
                _array[index] = clone;
            }
        }

        public int IndexOf(PlistNode item)
        {
            return _array.IndexOf(item);
        }

        public void Insert(int index, PlistNode item)
        {
            PlistNode clone = item.Clone();
            clone._parent = this;
            LibPlist.plist_array_insert_item(_node, clone._node, (uint)index);
            _array.Insert(index, clone);
        }

        public void RemoveAt(int index)
        {
            LibPlist.plist_array_remove_item(_node, (uint)index);
            _array.RemoveAt(index);
        }

        public void Add(PlistNode item)
        {
            PlistNode clone = item.Clone();
            clone._parent = this;
            LibPlist.plist_array_append_item(_node, clone._node);
            _array.Add(clone);
        }

        public void Clear()
        {
            for (int i = 0; i < _array.Count; i++)
            {
                LibPlist.plist_array_remove_item(_node, 0);
            }
            _array.Clear();
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
            PlistArray plistArray = new PlistArray();
            plistArray._node = LibPlist.plist_copy(_node);
            plistArray.array_fill(plistArray._node);

            return plistArray;
        }

        private void array_fill(plist_t node)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                uint length = LibPlist.plist_array_get_size(node);
                for (uint i = 0; i < length; i++)
                {
                    plist_t item = LibPlist.plist_array_get_item(node, i);
                    _array.Add(FromPlist(item, this)!);
                }
            }
            else
            {
                LibPlist.plist_array_new_iter(node, out plist_array_iter iter);

                while (true)
                {
                    LibPlist.plist_array_next_item(node, iter, out plist_t subnode);
                    if (subnode == IntPtr.Zero)
                    {
                        break;
                    }
                    _array.Add(FromPlist(subnode, this)!);
                }
                Marshal.FreeHGlobal(iter);
            }
        }
    }
}
