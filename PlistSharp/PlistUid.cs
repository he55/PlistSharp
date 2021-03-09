namespace PlistSharp
{
    public class PlistUid : PlistNode
    {
        public PlistUid(ulong value, PlistStructure? parent = null)
        {
            _node = plist.plist_new_uid(value);
            _parent = parent;
        }

        public PlistUid(plist_t node, PlistStructure? parent = null)
        {
            _node = node;
            _parent = parent;
        }

        public override PlistNode Copy() => new PlistUid(Value);

        public ulong Value
        {
            get
            {
                plist.plist_get_uid_val(_node, out ulong value);
                return value;
            }

            set => plist.plist_set_uid_val(_node, value);
        }
    }
}
