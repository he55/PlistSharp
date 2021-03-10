namespace PlistSharp
{
    public class PlistInteger : PlistNode
    {
        public PlistInteger(ulong value, PlistStructure? parent = null)
        {
            _node = plist.plist_new_uint(value);
            _parent = parent;
        }

        public PlistInteger(plist_t node, PlistStructure? parent = null)
        {
            _node = node;
            _parent = parent;
        }

        public override PlistNode Copy() => new PlistInteger(Value);

        public ulong Value
        {
            get
            {
                plist.plist_get_uint_val(_node, out ulong value);
                return value;
            }
            set => plist.plist_set_uint_val(_node, value);
        }
    }
}
