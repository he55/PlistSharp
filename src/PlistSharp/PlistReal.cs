namespace PlistSharp
{
    public class PlistReal : PlistNode
    {
        public PlistReal(PlistStructure? parent = null)
            : base(plist_type.PLIST_REAL, parent)
        {
        }

        public PlistReal(plist_t node, PlistStructure? parent = null)
        {
            _node = node;
            _parent = parent;
        }

        public PlistReal(PlistReal d)
            : base(plist_type.PLIST_REAL)
        {
            LibPlist.plist_set_real_val(_node, d.GetValue());
        }

        public PlistReal(double d)
            : base(plist_type.PLIST_REAL)
        {
            LibPlist.plist_set_real_val(_node, d);
        }

        public override PlistNode Clone()
        {
            return new PlistReal(this);
        }

        public void SetValue(double d)
        {
            LibPlist.plist_set_real_val(_node, d);
        }

        public double GetValue()
        {
            LibPlist.plist_get_real_val(_node, out double d);
            return d;
        }
    }
}
