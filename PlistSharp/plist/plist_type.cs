namespace PlistSharp
{
    /// <summary>
    /// The enumeration of plist node types.
    /// </summary>
    public enum plist_type
    {
        /// <summary>
        /// Boolean, scalar type
        /// </summary>
        PLIST_BOOLEAN,

        /// <summary>
        /// Unsigned integer, scalar type
        /// </summary>
        PLIST_UINT,

        /// <summary>
        /// Real, scalar type
        /// </summary>
        PLIST_REAL,

        /// <summary>
        /// ASCII string, scalar type
        /// </summary>
        PLIST_STRING,

        /// <summary>
        /// Ordered array, structured type
        /// </summary>
        PLIST_ARRAY,

        /// <summary>
        /// Unordered dictionary (key/value pair), structured type
        /// </summary>
        PLIST_DICT,

        /// <summary>
        /// Date, scalar type
        /// </summary>
        PLIST_DATE,

        /// <summary>
        /// Binary data, scalar type
        /// </summary>
        PLIST_DATA,

        /// <summary>
        /// Key in dictionaries (ASCII String), scalar type
        /// </summary>
        PLIST_KEY,

        /// <summary>
        /// Special type used for 'keyed encoding'
        /// </summary>
        PLIST_UID,

        /// <summary>
        /// No type
        /// </summary>
        PLIST_NONE
    }
}
