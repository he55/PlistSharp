using System;

namespace PlistSharp
{
    public class PlistNode : IDisposable
    {
        internal PlistNode? _parent;
        internal plist_t _node;

        protected PlistNode(PlistNode? parent = null)
        {
            _parent = parent;
        }

        protected PlistNode(plist_t node, PlistNode? parent = null)
        {
            _node = node;
            _parent = parent;
        }

        protected PlistNode(plist_type type, PlistNode? parent = null)
        {
            _parent = parent;
            _node = (plist_t)IntPtr.Zero;

            switch (type)
            {
                case plist_type.PLIST_BOOLEAN:
                    _node = LibPlist.plist_new_bool(0);
                    break;
                case plist_type.PLIST_UINT:
                    _node = LibPlist.plist_new_uint(0);
                    break;
                case plist_type.PLIST_REAL:
                    _node = LibPlist.plist_new_real(0.0);
                    break;
                case plist_type.PLIST_STRING:
                    _node = LibPlist.plist_new_string(string.Empty);
                    break;
                case plist_type.PLIST_KEY:
                    _node = LibPlist.plist_new_string(string.Empty);
                    LibPlist.plist_set_key_val(_node, string.Empty);
                    break;
                case plist_type.PLIST_UID:
                    _node = LibPlist.plist_new_uid(0);
                    break;
                case plist_type.PLIST_DATA:
                    _node = LibPlist.plist_new_data(IntPtr.Zero, 0);
                    break;
                case plist_type.PLIST_DATE:
                    _node = LibPlist.plist_new_date(0, 0);
                    break;
                case plist_type.PLIST_ARRAY:
                    _node = LibPlist.plist_new_array();
                    break;
                case plist_type.PLIST_DICT:
                    _node = LibPlist.plist_new_dict();
                    break;
                case plist_type.PLIST_NONE:
                default:
                    break;
            }
        }

        public virtual PlistNode Clone()
        {
            throw new NotImplementedException();
        }

        public plist_type GetPlistType()
        {
            if (_node == IntPtr.Zero)
            {
                return plist_type.PLIST_NONE;
            }
            return LibPlist.plist_get_node_type(_node);
        }

        public plist_t GetPlist()
        {
            return _node;
        }

        public PlistNode? GetParent()
        {
            return _parent;
        }

        public static PlistNode? FromPlist(plist_t node, PlistNode? parent = null)
        {
            PlistNode? ret = null;
            if (node == IntPtr.Zero)
            {
                return ret;
            }

            plist_type type = LibPlist.plist_get_node_type(node);
            switch (type)
            {
                case plist_type.PLIST_DICT:
                    ret = new PlistDictionary(node, parent);
                    break;
                case plist_type.PLIST_ARRAY:
                    ret = new PlistArray(node, parent);
                    break;
                case plist_type.PLIST_BOOLEAN:
                    ret = new PlistBoolean(node, parent);
                    break;
                case plist_type.PLIST_UINT:
                    ret = new PlistInteger(node, parent);
                    break;
                case plist_type.PLIST_REAL:
                    ret = new PlistReal(node, parent);
                    break;
                case plist_type.PLIST_STRING:
                    ret = new PlistString(node, parent);
                    break;
                case plist_type.PLIST_KEY:
                    ret = new PlistKey(node, parent);
                    break;
                case plist_type.PLIST_UID:
                    ret = new PlistUid(node, parent);
                    break;
                case plist_type.PLIST_DATE:
                    ret = new PlistDate(node, parent);
                    break;
                case plist_type.PLIST_DATA:
                    ret = new PlistData(node, parent);
                    break;
                default:
                    LibPlist.plist_free(node);
                    break;
            }
            return ret;
        }

        #region IDisposable Support
        private bool _disposed = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _parent = null;
                }

                if (_parent == null)
                    LibPlist.plist_free(_node);

                _node = (plist_t)IntPtr.Zero;

                _disposed = true;
            }
        }

        ~PlistNode()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
