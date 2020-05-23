namespace PlistSharp
{
    public class PlistUid : PlistNode
    {
        public PlistUid(PlistStructure? parent = null)
        {
            CreatePlistNode(plist_type.PLIST_UID, parent);
        }

        public PlistUid(plist_t node, PlistStructure? parent = null)
        {
            _node = node;
            _parent = parent;
        }

        public PlistUid(ulong value)
        {
            CreatePlistNode(plist_type.PLIST_UID);
            LibPlist.plist_set_uid_val(_node, value);
        }

        public override PlistNode Clone()
        {
            PlistUid plistUid = new PlistUid();
            LibPlist.plist_set_uid_val(plistUid._node, GetValue());

            return plistUid;
        }

        public void SetValue(ulong value)
        {
            LibPlist.plist_set_uid_val(_node, value);
        }

        public ulong GetValue()
        {
            LibPlist.plist_get_uid_val(_node, out ulong value);
            return value;
        }
    }
}
