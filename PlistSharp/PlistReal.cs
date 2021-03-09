namespace PlistSharp
{
    public class PlistReal : PlistNode
    {
        public PlistReal(double value, PlistStructure? parent = null)
        {
            _node = plist.plist_new_real(value);
            _parent = parent;
        }

        public PlistReal(plist_t node, PlistStructure? parent = null)
        {
            _node = node;
            _parent = parent;
        }

        public override PlistNode Copy() => new PlistReal(Value);

        public double Value
        {
            get
            {
                plist.plist_get_real_val(_node, out double value);
                return value;
            }

            set => plist.plist_set_real_val(_node, value);
        }
    }
}
