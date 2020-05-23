namespace PlistSharp
{
    public class PlistBoolean : PlistNode
    {
        public PlistBoolean(PlistStructure? parent = null)
        {
            CreatePlistNode(plist_type.PLIST_BOOLEAN, parent);
        }

        public PlistBoolean(plist_t node, PlistStructure? parent = null)
        {
            _node = node;
            _parent = parent;
        }

        public PlistBoolean(bool value)
        {
            CreatePlistNode(plist_type.PLIST_BOOLEAN);
            LibPlist.plist_set_bool_val(_node, value ? (byte)1 : (byte)0);
        }

        public override PlistNode Clone()
        {
            PlistBoolean plistBoolean = new PlistBoolean();
            LibPlist.plist_set_bool_val(plistBoolean._node, GetValue() ? (byte)1 : (byte)0);

            return plistBoolean;
        }

        public void SetValue(bool value)
        {
            LibPlist.plist_set_bool_val(_node, value ? (byte)1 : (byte)0);
        }

        public bool GetValue()
        {
            LibPlist.plist_get_bool_val(_node, out byte value);
            return value != 0;
        }
    }
}
