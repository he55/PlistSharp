namespace PlistSharp
{
    public class PlistReal : PlistNode
    {
        public PlistReal(PlistStructure? parent = null)
        {
            CreatePlistNode(plist_type.PLIST_REAL, parent);
        }

        public PlistReal(plist_t node, PlistStructure? parent = null)
        {
            _node = node;
            _parent = parent;
        }

        public PlistReal(double value)
        {
            CreatePlistNode(plist_type.PLIST_REAL);
            LibPlist.plist_set_real_val(_node, value);
        }

        public override PlistNode Copy()
        {
            PlistReal plistReal = new PlistReal();
            LibPlist.plist_set_real_val(plistReal._node, GetValue());

            return plistReal;
        }

        public void SetValue(double value)
        {
            LibPlist.plist_set_real_val(_node, value);
        }

        public double GetValue()
        {
            LibPlist.plist_get_real_val(_node, out double value);
            return value;
        }
    }
}
