namespace PlistSharp
{
    public class PlistDate : PlistNode
    {
        public PlistDate(timeval value, PlistStructure? parent = null)
        {
            _node = LibPlist.plist_new_date((int)value.tv_sec, value.tv_usec);
            _parent = parent;
        }

        public PlistDate(plist_t node, PlistStructure? parent = null)
        {
            _node = node;
            _parent = parent;
        }

        public override PlistNode Copy() => new PlistDate(Value);

        public timeval Value
        {
            get
            {
                LibPlist.plist_get_date_val(_node, out int tv_sec, out int tv_usec);
                timeval value = new timeval
                {
                    tv_sec = tv_sec,
                    tv_usec = tv_usec
                };

                return value;
            }

            set => LibPlist.plist_set_date_val(_node, (int)value.tv_sec, value.tv_usec);
        }
    }
}
