using static Interop.LibPlist;

namespace PlistSharp
{
    public class PlistUid : PlistNode
    {
        public PlistUid(PlistNode? parent = null)
            : base(plist_type.PLIST_UID, parent)
        {
        }

        public PlistUid(plist_t node, PlistNode? parent = null)
            : base(node, parent)
        {
        }

        public PlistUid(PlistUid i)
            : base(plist_type.PLIST_UID)
        {
            plist_set_uid_val(_node, i.GetValue());
        }

        public PlistUid(ulong i)
            : base(plist_type.PLIST_UID)
        {
            plist_set_uid_val(_node, i);
        }

        public override PlistNode Clone()
        {
            return new PlistUid(this);
        }

        public void SetValue(ulong i)
        {
            plist_set_uid_val(_node, i);
        }

        public ulong GetValue()
        {
            plist_get_uid_val(_node, out ulong i);
            return i;
        }
    }
}
