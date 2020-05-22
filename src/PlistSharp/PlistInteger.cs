namespace PlistSharp
{
    public class PlistInteger : PlistNode
    {
        public PlistInteger(PlistStructure? parent = null)
            : base(plist_type.PLIST_UINT, parent)
        {
        }

        public PlistInteger(plist_t node, PlistStructure? parent = null)
        {
            _node = node;
            _parent = parent;
        }

        public PlistInteger(PlistInteger i)
            : base(plist_type.PLIST_UINT)
        {
            LibPlist.plist_set_uint_val(_node, i.GetValue());
        }

        public PlistInteger(ulong i)
            : base(plist_type.PLIST_UINT)
        {
            LibPlist.plist_set_uint_val(_node, i);
        }

        public override PlistNode Clone()
        {
            return new PlistInteger(this);
        }

        public void SetValue(ulong i)
        {
            LibPlist.plist_set_uint_val(_node, i);
        }

        public ulong GetValue()
        {
            LibPlist.plist_get_uint_val(_node, out ulong i);
            return i;
        }
    }
}
