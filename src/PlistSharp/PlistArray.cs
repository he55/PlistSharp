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
            _node = LibPlist.plist_new_array();
            _parent = parent;
        }

        public PlistArray(plist_t node, PlistStructure? parent = null)
        {
            _node = node;
            _parent = parent;
            Fill();
        }

        public int Count => _array.Count;

        public PlistNode this[int index]
        {
            get => _array[index];
            set
            {
                PlistNode copy = value.Copy();
                copy._parent = this;
                LibPlist.plist_array_insert_item(_node, copy._node, (uint)index);
                _array[index] = copy;
            }
        }

        public int IndexOf(PlistNode item)
        {
            return _array.IndexOf(item);
        }

        public void Insert(int index, PlistNode item)
        {
            PlistNode copy = item.Copy();
            copy._parent = this;
            LibPlist.plist_array_insert_item(_node, copy._node, (uint)index);
            _array.Insert(index, copy);
        }

        public void RemoveAt(int index)
        {
            LibPlist.plist_array_remove_item(_node, (uint)index);
            _array.RemoveAt(index);
        }

        public void Add(PlistNode item)
        {
            PlistNode copy = item.Copy();
            copy._parent = this;
            LibPlist.plist_array_append_item(_node, copy._node);
            _array.Add(copy);
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

        public override PlistNode Copy() => new PlistArray(LibPlist.plist_copy(_node));

        protected override void Fill()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                uint length = LibPlist.plist_array_get_size(_node);
                for (uint i = 0; i < length; i++)
                {
                    plist_t item = LibPlist.plist_array_get_item(_node, i);
                    _array.Add(FromPlist(item, this)!);
                }
            }
            else
            {
                LibPlist.plist_array_new_iter(_node, out plist_array_iter iter);

                while (true)
                {
                    LibPlist.plist_array_next_item(_node, iter, out plist_t subnode);
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
