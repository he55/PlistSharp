using static Interop.LibPlist;

namespace PlistSharp
{
    public class PlistReal : PlistNode
    {
        public PlistReal(PlistNode? parent = null)
            : base(plist_type.PLIST_REAL, parent)
        {
        }

        public PlistReal(plist_t node, PlistNode? parent = null)
            : base(node, parent)
        {
        }

        public PlistReal(PlistReal d)
            : base(plist_type.PLIST_REAL)
        {
            plist_set_real_val(_node, d.GetValue());
        }

        public PlistReal(double d)
            : base(plist_type.PLIST_REAL)
        {
            plist_set_real_val(_node, d);
        }

        public override PlistNode Clone()
        {
            return new PlistReal(this);
        }

        public void SetValue(double d)
        {
            plist_set_real_val(_node, d);
        }

        public double GetValue()
        {
            plist_get_real_val(_node, out double d);
            return d;
        }
    }
}