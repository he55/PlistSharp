namespace PlistSharp
{
    public class PlistUid : PlistNode
    {
        public PlistUid(PlistStructure? parent = null)
            : base(plist_type.PLIST_UID, parent)
        {
        }

        public PlistUid(plist_t node, PlistStructure? parent = null)
        {
            _node = node;
            _parent = parent;
        }

        public PlistUid(PlistUid i)
            : base(plist_type.PLIST_UID)
        {
            LibPlist.plist_set_uid_val(_node, i.GetValue());
        }

        public PlistUid(ulong i)
            : base(plist_type.PLIST_UID)
        {
            LibPlist.plist_set_uid_val(_node, i);
        }

        public override PlistNode Clone()
        {
            return new PlistUid(this);
        }

        public void SetValue(ulong i)
        {
            LibPlist.plist_set_uid_val(_node, i);
        }

        public ulong GetValue()
        {
            LibPlist.plist_get_uid_val(_node, out ulong i);
            return i;
        }
    }
}
