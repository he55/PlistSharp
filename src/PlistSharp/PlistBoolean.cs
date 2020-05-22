namespace PlistSharp
{
    public class PlistBoolean : PlistNode
    {
        public PlistBoolean(PlistNode? parent = null)
            : base(plist_type.PLIST_BOOLEAN, parent)
        {
        }

        public PlistBoolean(plist_t node, PlistNode? parent = null)
        {
            _node = node;
            _parent = parent;
        }

        public PlistBoolean(PlistBoolean b)
            : base(plist_type.PLIST_BOOLEAN)
        {
            LibPlist.plist_set_bool_val(_node, BoolToByte(b.GetValue()));
        }

        public PlistBoolean(bool b)
            : base(plist_type.PLIST_BOOLEAN)
        {
            LibPlist.plist_set_bool_val(_node, BoolToByte(b));
        }

        public override PlistNode Clone()
        {
            return new PlistBoolean(this);
        }

        public void SetValue(bool b)
        {
            LibPlist.plist_set_bool_val(_node, BoolToByte(b));
        }

        public bool GetValue()
        {
            LibPlist.plist_get_bool_val(_node, out byte b);
            return b != 0;
        }

        private byte BoolToByte(bool b)
        {
            return b ? (byte)1 : (byte)0;
        }
    }
}
