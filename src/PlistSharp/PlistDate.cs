using static Interop.LibPlist;

namespace PlistSharp
{
    public class PlistDate : PlistNode
    {
        public PlistDate(PlistNode? parent = null)
            : base(plist_type.PLIST_DATE, parent)
        {
        }

        public PlistDate(plist_t node, PlistNode? parent = null)
            : base(node, parent)
        {
        }

        public PlistDate(PlistDate d)
            : base(plist_type.PLIST_DATE)
        {
            timeval t = d.GetValue();
            plist_set_date_val(_node, (int)t.tv_sec, t.tv_usec);
        }

        public PlistDate(timeval t)
            : base(plist_type.PLIST_DATE)
        {
            plist_set_date_val(_node, (int)t.tv_sec, t.tv_usec);
        }

        public override PlistNode Clone()
        {
            return new PlistDate(this);
        }

        public void SetValue(timeval t)
        {
            plist_set_date_val(_node, (int)t.tv_sec, t.tv_usec);
        }

        public timeval GetValue()
        {
            plist_get_date_val(_node, out int tv_sec, out int tv_usec);
            timeval t = new timeval();
            t.tv_sec = tv_sec;
            t.tv_usec = tv_usec;
            return t;
        }
    }
}
