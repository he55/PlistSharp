namespace PlistSharp
{
    public class PlistBoolean : PlistNode
    {
        public PlistBoolean(bool value, PlistStructure? parent = null)
        {
            _node = LibPlist.plist_new_bool(value ? (byte)1 : (byte)0);
            _parent = parent;
        }

        public PlistBoolean(plist_t node, PlistStructure? parent = null)
        {
            _node = node;
            _parent = parent;
        }

        public override PlistNode Copy() => new PlistBoolean(Value);

        public bool Value
        {
            get
            {
                LibPlist.plist_get_bool_val(_node, out byte value);
                return value != 0;
            }

            set => LibPlist.plist_set_bool_val(_node, value ? (byte)1 : (byte)0);
        }
    }
}
