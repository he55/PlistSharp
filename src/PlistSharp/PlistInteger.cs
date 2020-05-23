namespace PlistSharp
{
    public class PlistInteger : PlistNode
    {
        public PlistInteger(PlistStructure? parent = null)
        {
            CreatePlistNode(plist_type.PLIST_UINT, parent);
        }

        public PlistInteger(plist_t node, PlistStructure? parent = null)
        {
            _node = node;
            _parent = parent;
        }

        public PlistInteger(ulong value)
        {
            CreatePlistNode(plist_type.PLIST_UINT);
            LibPlist.plist_set_uint_val(_node, value);
        }

        public override PlistNode Copy()
        {
            PlistInteger plistInteger = new PlistInteger();
            LibPlist.plist_set_uint_val(plistInteger._node, GetValue());

            return plistInteger;
        }

        public void SetValue(ulong value)
        {
            LibPlist.plist_set_uint_val(_node, value);
        }

        public ulong GetValue()
        {
            LibPlist.plist_get_uint_val(_node, out ulong value);
            return value;
        }
    }
}
