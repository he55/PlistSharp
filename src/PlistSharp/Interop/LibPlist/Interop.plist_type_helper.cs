public static partial class Interop
{
    public static partial class LibPlist
    {
        public static bool PLIST_IS_PLIST(plist_t plist) =>
            plist_get_node_type(plist) != plist_type.PLIST_NONE;

        // Helper macros for the different plist types

        public static bool PLIST_IS_BOOLEAN(plist_t plist) =>
            plist_get_node_type(plist) == plist_type.PLIST_BOOLEAN;

        public static bool PLIST_IS_UINT(plist_t plist) =>
            plist_get_node_type(plist) == plist_type.PLIST_UINT;

        public static bool PLIST_IS_REAL(plist_t plist) =>
            plist_get_node_type(plist) == plist_type.PLIST_REAL;

        public static bool PLIST_IS_STRING(plist_t plist) =>
            plist_get_node_type(plist) == plist_type.PLIST_STRING;

        public static bool PLIST_IS_ARRAY(plist_t plist) =>
            plist_get_node_type(plist) == plist_type.PLIST_ARRAY;

        public static bool PLIST_IS_DICT(plist_t plist) =>
            plist_get_node_type(plist) == plist_type.PLIST_DICT;

        public static bool PLIST_IS_DATE(plist_t plist) =>
            plist_get_node_type(plist) == plist_type.PLIST_DATE;

        public static bool PLIST_IS_DATA(plist_t plist) =>
            plist_get_node_type(plist) == plist_type.PLIST_DATA;

        public static bool PLIST_IS_KEY(plist_t plist) =>
            plist_get_node_type(plist) == plist_type.PLIST_KEY;

        public static bool PLIST_IS_UID(plist_t plist) =>
            plist_get_node_type(plist) == plist_type.PLIST_UID;
    }
}
