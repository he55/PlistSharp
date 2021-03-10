using System;

namespace PlistSharp
{
    public abstract class PlistNode : IDisposable
    {
        internal PlistStructure _parent;
        internal plist_t _node;

        public abstract PlistNode Copy();

        public plist_type PlistType => plist.plist_get_node_type(_node);

        public static PlistNode FromPlist(plist_t node, PlistStructure parent = null)
        {
            plist_type type = plist.plist_get_node_type(node);

            switch (type)
            {
                case plist_type.PLIST_DICT: return new PlistDictionary(node, parent);
                case plist_type.PLIST_ARRAY: return new PlistArray(node, parent);
                case plist_type.PLIST_BOOLEAN: return new PlistBoolean(node, parent);
                case plist_type.PLIST_UINT: return new PlistInteger(node, parent);
                case plist_type.PLIST_REAL: return new PlistReal(node, parent);
                case plist_type.PLIST_STRING: return new PlistString(node, parent);
                case plist_type.PLIST_KEY: return new PlistKey(node, parent);
                case plist_type.PLIST_UID: return new PlistUid(node, parent);
                case plist_type.PLIST_DATE: return new PlistDate(node, parent);
                case plist_type.PLIST_DATA: return new PlistData(node, parent);
                default: throw new NotSupportedException();
            }
        }

        #region IDisposable Support

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _parent = null;
                }

                if (_parent == null)
                {
                    plist.plist_free(_node);
                }

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
