namespace PlistSharp
{
    public static class PlistDictionaryExtensions
    {
        public static PlistNode GetValueOrDefault(this PlistDictionary dict, string key)
        {
            if (dict.Keys.Contains(key))
            {
                return dict[key];
            }
            return null;
        }
    }
}
