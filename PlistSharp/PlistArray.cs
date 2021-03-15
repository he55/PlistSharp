using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace PlistSharp
{
    public class PlistArray : PlistStructure, IEnumerable<PlistNode>
    {
        private readonly IList<PlistNode> _array = new List<PlistNode>();

        public PlistArray(PlistStructure parent = null)
        {
            _node = plist.plist_new_array();
            _parent = parent;
        }

        public PlistArray(plist_t node, PlistStructure parent = null)
        {
            _node = node;
            _parent = parent;
            Fill();
        }

        public override int Count => _array.Count;

        public override PlistNode Copy() => new PlistArray(plist.plist_copy(_node));

        public PlistNode this[int index]
        {
            get => _array[index];
            set
            {
                value = value.Copy();
                value._parent = this;
                plist.plist_array_insert_item(_node, value._node, (uint)index);
                _array[index] = value;
            }
        }

        public int IndexOf(PlistNode item)
        {
            return _array.IndexOf(item);
        }

        public void Insert(int index, PlistNode item)
        {
            item = item.Copy();
            item._parent = this;
            plist.plist_array_insert_item(_node, item._node, (uint)index);
            _array.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            plist.plist_array_remove_item(_node, (uint)index);
            _array.RemoveAt(index);
        }

        public void Add(PlistNode item)
        {
            item = item.Copy();
            item._parent = this;
            plist.plist_array_append_item(_node, item._node);
            _array.Add(item);
        }

        public void Clear()
        {
            for (int i = 0; i < _array.Count; i++)
            {
                plist.plist_array_remove_item(_node, 0);
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

        protected override void Fill()
        {
            uint length = plist.plist_array_get_size(_node);
            for (uint i = 0; i < length; i++)
            {
                plist_t item = plist.plist_array_get_item(_node, i);
                _array.Add(FromPlist(item, this));
            }
        }
    }
}
