using System;

namespace PlistSharp
{
    public class PlistDate : PlistNode
    {
        private static readonly DateTime s_baseDateTime = new DateTime(2001, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public PlistDate(timeval value, PlistStructure? parent = null)
        {
            _node = plist.plist_new_date((int)value.tv_sec, value.tv_usec);
            _parent = parent;
        }

        public PlistDate(plist_t node, PlistStructure? parent = null)
        {
            _node = node;
            _parent = parent;
        }

        public override PlistNode Copy()
        {
            plist.plist_get_date_val(_node, out int tv_sec, out int tv_usec);
            timeval value = new timeval
            {
                tv_sec = tv_sec,
                tv_usec = tv_usec
            };

            return new PlistDate(value);
        }

        public DateTime Value
        {
            get
            {
                plist.plist_get_date_val(_node, out int tv_sec, out int tv_usec);
                return s_baseDateTime.AddSeconds(tv_sec);
            }
            set
            {
                plist.plist_set_date_val(_node, (int)(value - s_baseDateTime).TotalSeconds, 0);
            }
        }
    }
}
