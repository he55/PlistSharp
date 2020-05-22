namespace PlistSharp
{
    public class PlistBoolean : PlistNode
    {
        public PlistBoolean(PlistStructure? parent = null)
            : base(plist_type.PLIST_BOOLEAN, parent)
        {
        }

        public PlistBoolean(plist_t node, PlistStructure? parent = null)
        {
            _node = node;
            _parent = parent;
        }

        public PlistBoolean(PlistBoolean b)
            : base(plist_type.PLIST_BOOLEAN)
        {
            LibPlist.plist_set_bool_val(_node, b.GetValue() ? (byte)1 : (byte)0);
        }

        public PlistBoolean(bool b)
            : base(plist_type.PLIST_BOOLEAN)
        {
            LibPlist.plist_set_bool_val(_node, b ? (byte)1 : (byte)0);
        }

        public override PlistNode Clone()
        {
            return new PlistBoolean(this);
        }

        public void SetValue(bool b)
        {
            LibPlist.plist_set_bool_val(_node, b ? (byte)1 : (byte)0);
        }

        public bool GetValue()
        {
            LibPlist.plist_get_bool_val(_node, out byte b);
            return b != 0;
        }
    }
}
