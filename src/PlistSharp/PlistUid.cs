namespace PlistSharp
{
    public class PlistUid : PlistNode
    {
        public PlistUid(ulong value, PlistStructure? parent = null)
        {
            _node = LibPlist.plist_new_uid(value);
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
                LibPlist.plist_get_uid_val(_node, out ulong value);
                return value;
            }

            set => LibPlist.plist_set_uid_val(_node, value);
        }
    }
}
