using System;

namespace PlistSharp
{
    public abstract class PlistNode : IDisposable
    {
        internal PlistStructure? _parent;
        internal plist_t _node;

        public abstract PlistNode Copy();

        public plist_type PlistType => plist.plist_get_node_type(_node);

        public static PlistNode FromPlist(plist_t node, PlistStructure? parent = null)
        {
            plist_type type = plist.plist_get_node_type(node);
            return type switch
            {
                plist_type.PLIST_DICT => new PlistDictionary(node, parent),
                plist_type.PLIST_ARRAY => new PlistArray(node, parent),
                plist_type.PLIST_BOOLEAN => new PlistBoolean(node, parent),
                plist_type.PLIST_UINT => new PlistInteger(node, parent),
                plist_type.PLIST_REAL => new PlistReal(node, parent),
                plist_type.PLIST_STRING => new PlistString(node, parent),
                plist_type.PLIST_KEY => new PlistKey(node, parent),
                plist_type.PLIST_UID => new PlistUid(node, parent),
                plist_type.PLIST_DATE => new PlistDate(node, parent),
                plist_type.PLIST_DATA => new PlistData(node, parent),
                _ => throw new NotSupportedException(),
            };
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
