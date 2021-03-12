namespace PlistSharp
{
    public static class PlistDictionaryExtensions
    {
        public static PlistNode GetValueOrDefault(this PlistDictionary @this, string key)
        {
            if (@this.Keys.Contains(key))
            {
                return @this[key];
            }
            return null;
        }
    }
}
