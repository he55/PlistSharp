using static Interop.LibPlist;

namespace PlistSharp
{
    public class PlistInteger : PlistNode
    {
        public PlistInteger(PlistNode? parent = null)
            : base(plist_type.PLIST_UINT, parent)
        {
        }

        public PlistInteger(plist_t node, PlistNode? parent = null)
            : base(node, parent)
        {
        }

        public PlistInteger(PlistInteger i)
            : base(plist_type.PLIST_UINT)
        {
            plist_set_uint_val(_node, i.GetValue());
        }

        public PlistInteger(ulong i)
            : base(plist_type.PLIST_UINT)
        {
            plist_set_uint_val(_node, i);
        }

        public override PlistNode Clone()
        {
            return new PlistInteger(this);
        }

        public void SetValue(ulong i)
        {
            plist_set_uint_val(_node, i);
        }

        public ulong GetValue()
        {
            plist_get_uint_val(_node, out ulong i);
            return i;
        }
    }
}