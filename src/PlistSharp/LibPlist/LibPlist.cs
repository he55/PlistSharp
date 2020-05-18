using System;
using System.Runtime.InteropServices;
using va_list = System.IntPtr;

namespace PlistSharp
{
    public static class LibPlist
    {
        public const string LibPlistLib = "libplist";


        /********************************************
         *                                          *
         *          Creation & Destruction          *
         *                                          *
         ********************************************/

        /// <summary>
        /// Create a new root plist_t type #PLIST_DICT
        /// </summary>
        /// <returns>the created plist</returns>
        [DllImport(LibPlistLib)]
        public static extern plist_t plist_new_dict();


        /// <summary>
        /// Create a new root plist_t type #PLIST_ARRAY
        /// </summary>
        /// <returns>the created plist</returns>
        [DllImport(LibPlistLib)]
        public static extern plist_t plist_new_array();


        /// <summary>
        /// Create a new plist_t type #PLIST_STRING
        /// </summary>
        /// <param name="val">the sting value, encoded in UTF8.</param>
        /// <returns>the created item</returns>
        [DllImport(LibPlistLib)]
        public static extern plist_t plist_new_string(/* const char * */ string val);


        /// <summary>
        /// Create a new plist_t type #PLIST_BOOLEAN
        /// </summary>
        /// <param name="val">the boolean value, 0 is false, other values are true.</param>
        /// <returns>the created item</returns>
        [DllImport(LibPlistLib)]
        public static extern plist_t plist_new_bool(byte val);


        /// <summary>
        /// Create a new plist_t type #PLIST_UINT
        /// </summary>
        /// <param name="val">the unsigned integer value</param>
        /// <returns>the created item</returns>
        [DllImport(LibPlistLib)]
        public static extern plist_t plist_new_uint(ulong val);


        /// <summary>
        /// Create a new plist_t type #PLIST_REAL
        /// </summary>
        /// <param name="val">the real value</param>
        /// <returns>the created item</returns>
        [DllImport(LibPlistLib)]
        public static extern plist_t plist_new_real(double val);


        /// <summary>
        /// Create a new plist_t type #PLIST_DATA
        /// </summary>
        /// <param name="val">the binary buffer</param>
        /// <param name="length">the length of the buffer</param>
        /// <returns>the created item</returns>
        [DllImport(LibPlistLib)]
        public static extern plist_t plist_new_data(/* const char * */ IntPtr val, ulong length);


        /// <summary>
        /// Create a new plist_t type #PLIST_DATE
        /// </summary>
        /// <param name="sec">the number of seconds since 01/01/2001</param>
        /// <param name="usec">the number of microseconds</param>
        /// <returns>the created item</returns>
        [DllImport(LibPlistLib)]
        public static extern plist_t plist_new_date(int sec, int usec);


        /// <summary>
        /// Create a new plist_t type #PLIST_UID
        /// </summary>
        /// <param name="val">the unsigned integer value</param>
        /// <returns>the created item</returns>
        [DllImport(LibPlistLib)]
        public static extern plist_t plist_new_uid(ulong val);


        /// <summary>
        /// Destruct a plist_t node and all its children recursively
        /// </summary>
        /// <param name="plist">the plist to free</param>
        [DllImport(LibPlistLib)]
        public static extern void plist_free(plist_t plist);


        /// <summary>
        /// Return a copy of passed node and it's children
        /// </summary>
        /// <param name="node">node the plist to copy</param>
        /// <returns>copied plist</returns>
        [DllImport(LibPlistLib)]
        public static extern plist_t plist_copy(plist_t node);


        /********************************************
         *                                          *
         *            Array functions               *
         *                                          *
         ********************************************/

        /// <summary>
        /// Get size of a #PLIST_ARRAY node.
        /// </summary>
        /// <param name="node">the node of type #PLIST_ARRAY</param>
        /// <returns>size of the #PLIST_ARRAY node</returns>
        [DllImport(LibPlistLib)]
        public static extern uint plist_array_get_size(plist_t node);


        /// <summary>
        /// Get the nth item in a #PLIST_ARRAY node.
        /// </summary>
        /// <param name="node">the node of type #PLIST_ARRAY</param>
        /// <param name="n">the index of the item to get. Range is [0, array_size[</param>
        /// <returns>the nth item or NULL if node is not of type #PLIST_ARRAY</returns>
        [DllImport(LibPlistLib)]
        public static extern plist_t plist_array_get_item(plist_t node, uint n);


        /// <summary>
        /// Get the index of an item. item must be a member of a #PLIST_ARRAY node.
        /// </summary>
        /// <param name="node">the node</param>
        /// <returns>the node index or UINT_MAX if node index can't be determined</returns>
        [DllImport(LibPlistLib)]
        public static extern uint plist_array_get_item_index(plist_t node);


        /// <summary>
        /// Set the nth item in a #PLIST_ARRAY node.
        /// The previous item at index n will be freed using #plist_free
        /// </summary>
        /// <param name="node">the node of type #PLIST_ARRAY</param>
        /// <param name="item">the new item at index n. The array is responsible for freeing item when it is no longer needed.</param>
        /// <param name="n">the index of the item to get. Range is [0, array_size[. Assert if n is not in range.</param>
        [DllImport(LibPlistLib)]
        public static extern void plist_array_set_item(plist_t node, plist_t item, uint n);


        /// <summary>
        /// Append a new item at the end of a #PLIST_ARRAY node.
        /// </summary>
        /// <param name="node">the node of type #PLIST_ARRAY</param>
        /// <param name="item">the new item. The array is responsible for freeing item when it is no longer needed.</param>
        [DllImport(LibPlistLib)]
        public static extern void plist_array_append_item(plist_t node, plist_t item);


        /// <summary>
        /// Insert a new item at position n in a #PLIST_ARRAY node.
        /// </summary>
        /// <param name="node">the node of type #PLIST_ARRAY</param>
        /// <param name="item">the new item to insert. The array is responsible for freeing item when it is no longer needed.</param>
        /// <param name="n">The position at which the node will be stored. Range is [0, array_size[. Assert if n is not in range.</param>
        [DllImport(LibPlistLib)]
        public static extern void plist_array_insert_item(plist_t node, plist_t item, uint n);


        /// <summary>
        /// Remove an existing position in a #PLIST_ARRAY node.
        /// Removed position will be freed using #plist_free.
        /// </summary>
        /// <param name="node">the node of type #PLIST_ARRAY</param>
        /// <param name="n">The position to remove. Range is [0, array_size[. Assert if n is not in range.</param>
        [DllImport(LibPlistLib)]
        public static extern void plist_array_remove_item(plist_t node, uint n);


        /// <summary>
        /// Remove a node that is a child node of a #PLIST_ARRAY node.
        /// node will be freed using #plist_free.
        /// </summary>
        /// <param name="node">The node to be removed from its #PLIST_ARRAY parent.</param>
        [DllImport(LibPlistLib)]
        public static extern void plist_array_item_remove(plist_t node);


        /// <summary>
        /// Create an iterator of a #PLIST_ARRAY node.
        /// The allocated iterator should be freed with the standard free function.
        /// </summary>
        /// <param name="node">The node of type #PLIST_ARRAY</param>
        /// <param name="iter">Location to store the iterator for the array.</param>
        [DllImport(LibPlistLib)]
        public static extern void plist_array_new_iter(plist_t node, out plist_array_iter iter);


        /// <summary>
        /// Increment iterator of a #PLIST_ARRAY node.
        /// </summary>
        /// <param name="node">The node of type #PLIST_ARRAY.</param>
        /// <param name="iter">Iterator of the array</param>
        /// <param name="item">
        /// Location to store the item. The caller must *not* free the
        /// returned item. Will be set to NULL when no more items are left
        /// to iterate.
        /// </param>
        [DllImport(LibPlistLib)]
        public static extern void plist_array_next_item(plist_t node, plist_array_iter iter, out plist_t item);


        /********************************************
         *                                          *
         *         Dictionary functions             *
         *                                          *
         ********************************************/

        /// <summary>
        /// Get size of a #PLIST_DICT node.
        /// </summary>
        /// <param name="node">the node of type #PLIST_DICT</param>
        /// <returns>size of the #PLIST_DICT node</returns>
        [DllImport(LibPlistLib)]
        public static extern uint plist_dict_get_size(plist_t node);


        /// <summary>
        /// Create an iterator of a #PLIST_DICT node.
        /// The allocated iterator should be freed with the standard free function.
        /// </summary>
        /// <param name="node">The node of type #PLIST_DICT.</param>
        /// <param name="iter">Location to store the iterator for the dictionary.</param>
        [DllImport(LibPlistLib)]
        public static extern void plist_dict_new_iter(plist_t node, out plist_dict_iter iter);


        /// <summary>
        /// Increment iterator of a #PLIST_DICT node.
        /// </summary>
        /// <param name="node">The node of type #PLIST_DICT</param>
        /// <param name="iter">Iterator of the dictionary</param>
        /// <param name="key">
        /// Location to store the key, or NULL. The caller is responsible
        /// for freeing the the returned string.
        /// </param>
        /// <param name="val">
        /// Location to store the value, or NULL. The caller must *not*
        /// free the returned value. Will be set to NULL when no more
        /// key/value pairs are left to iterate.
        /// </param>
        [DllImport(LibPlistLib)]
        public static extern void plist_dict_next_item(plist_t node, plist_dict_iter iter, /* char** */ out IntPtr key, out plist_t val);


        /// <summary>
        /// Get key associated key to an item. Item must be member of a dictionary.
        /// </summary>
        /// <param name="node">the item</param>
        /// <param name="key">a location to store the key. The caller is responsible for freeing the returned string.</param>
        [DllImport(LibPlistLib)]
        public static extern void plist_dict_get_item_key(plist_t node, /* char** */ out IntPtr key);


        /// <summary>
        /// Get the nth item in a #PLIST_DICT node.
        /// </summary>
        /// <param name="node">the node of type #PLIST_DICT</param>
        /// <param name="key">the identifier of the item to get.</param>
        /// <returns>
        /// the item or NULL if node is not of type #PLIST_DICT. The caller should not free
        /// the returned node.
        /// </returns>
        [DllImport(LibPlistLib)]
        public static extern plist_t plist_dict_get_item(plist_t node, /* const char* */ string key);


        /// <summary>
        /// Get key node associated to an item. Item must be member of a dictionary.
        /// </summary>
        /// <param name="node">the item</param>
        /// <returns>the key node of the given item, or NULL.</returns>
        [DllImport(LibPlistLib)]
        public static extern plist_t plist_dict_item_get_key(plist_t node);


        /// <summary>
        /// Set item identified by key in a #PLIST_DICT node.
        /// The previous item identified by key will be freed using #plist_free.
        /// If there is no item for the given key a new item will be inserted.
        /// </summary>
        /// <param name="node">the node of type #PLIST_DICT</param>
        /// <param name="key">the identifier of the item to set.</param>
        /// <param name="item">the new item associated to key</param>
        [DllImport(LibPlistLib)]
        public static extern void plist_dict_set_item(plist_t node, /* const char* */ string key, plist_t item);


        /// <summary>
        /// Remove an existing position in a #PLIST_DICT node.
        /// Removed position will be freed using #plist_free
        /// </summary>
        /// <param name="node">the node of type #PLIST_DICT</param>
        /// <param name="key">The identifier of the item to remove. Assert if identifier is not present.</param>
        [DllImport(LibPlistLib)]
        public static extern void plist_dict_remove_item(plist_t node, /* const char* */ string key);


        /// <summary>
        /// Remove an existing position in a #PLIST_DICT node.
        /// Removed position will be freed using #plist_free
        /// </summary>
        /// <param name="node">the node of type #PLIST_DICT</param>
        /// <param name="key">The identifier of the item to remove. Assert if identifier is not present.</param>
        [DllImport(LibPlistLib)]
        public static extern void plist_dict_remove_item(plist_t node, IntPtr key);


        /// <summary>
        /// Merge a dictionary into another. This will add all key/value pairs
        /// from the source dictionary to the target dictionary, overwriting
        /// any existing key/value pairs that are already present in target.
        /// </summary>
        /// <param name="target">pointer to an existing node of type #PLIST_DICT</param>
        /// <param name="source">node of type #PLIST_DICT that should be merged into target</param>
        [DllImport(LibPlistLib)]
        public static extern void plist_dict_merge(ref plist_t target, plist_t source);


        /********************************************
         *                                          *
         *                Getters                   *
         *                                          *
         ********************************************/

        /// <summary>
        /// Get the parent of a node
        /// </summary>
        /// <param name="node">the parent (NULL if node is root)</param>
        /// <returns></returns>
        [DllImport(LibPlistLib)]
        public static extern plist_t plist_get_parent(plist_t node);


        /// <summary>
        /// Get the #plist_type of a node.
        /// </summary>
        /// <param name="node">the node</param>
        /// <returns>the type of the node</returns>
        [DllImport(LibPlistLib)]
        public static extern plist_type plist_get_node_type(plist_t node);


        /// <summary>
        /// Get the value of a #PLIST_KEY node.
        /// This function does nothing if node is not of type #PLIST_KEY
        /// </summary>
        /// <param name="node">the node</param>
        /// <param name="val">
        /// a pointer to a C-string. This function allocates the memory,
        /// caller is responsible for freeing it.
        /// </param>
        [DllImport(LibPlistLib)]
        public static extern void plist_get_key_val(plist_t node, /* char** */ out IntPtr val);


        /// <summary>
        /// Get the value of a #PLIST_STRING node.
        /// This function does nothing if node is not of type #PLIST_STRING
        /// </summary>
        /// <param name="node">the node</param>
        /// <param name="val">
        /// a pointer to a C-string. This function allocates the memory,
        /// caller is responsible for freeing it. Data is UTF-8 encoded.
        /// </param>
        [DllImport(LibPlistLib)]
        public static extern void plist_get_string_val(plist_t node, /* char** */ out IntPtr val);


        // /**
        //  * Get a pointer to the buffer of a #PLIST_STRING node.
        //  *
        //  * @note DO NOT MODIFY the buffer. Mind that the buffer is only available
        //  *   until the plist node gets freed. Make a copy if needed.
        //  *
        //  * @param node The node
        //  * @param length If non-NULL, will be set to the length of the string
        //  *
        //  * @return Pointer to the NULL-terminated buffer.
        //  */
        // const char* plist_get_string_ptr(plist_t node, uint64_t* length);


        /// <summary>
        /// Get the value of a #PLIST_BOOLEAN node.
        /// This function does nothing if node is not of type #PLIST_BOOLEAN
        /// </summary>
        /// <param name="node">the node</param>
        /// <param name="val">a pointer to a uint8_t variable.</param>
        [DllImport(LibPlistLib)]
        public static extern void plist_get_bool_val(plist_t node, out byte val);


        /// <summary>
        /// Get the value of a #PLIST_UINT node.
        /// This function does nothing if node is not of type #PLIST_UINT
        /// </summary>
        /// <param name="node">the node</param>
        /// <param name="val">a pointer to a uint64_t variable.</param>
        [DllImport(LibPlistLib)]
        public static extern void plist_get_uint_val(plist_t node, out ulong val);


        /// <summary>
        /// Get the value of a #PLIST_REAL node.
        /// This function does nothing if node is not of type #PLIST_REAL
        /// </summary>
        /// <param name="node">the node</param>
        /// <param name="val">a pointer to a double variable.</param>
        [DllImport(LibPlistLib)]
        public static extern void plist_get_real_val(plist_t node, out double val);


        /// <summary>
        /// Get the value of a #PLIST_DATA node.
        /// This function does nothing if node is not of type #PLIST_DATA
        /// </summary>
        /// <param name="node">the node</param>
        /// <param name="val">
        /// a pointer to an unallocated char buffer. This function allocates the memory,
        /// caller is responsible for freeing it.
        /// </param>
        /// <param name="length">the length of the buffer</param>
        [DllImport(LibPlistLib)]
        public static extern void plist_get_data_val(plist_t node, /* char** */ out IntPtr val, out ulong length);


        // /**
        //  * Get a pointer to the data buffer of a #PLIST_DATA node.
        //  *
        //  * @note DO NOT MODIFY the buffer. Mind that the buffer is only available
        //  *   until the plist node gets freed. Make a copy if needed.
        //  *
        //  * @param node The node
        //  * @param length Pointer to a uint64_t that will be set to the length of the buffer
        //  *
        //  * @return Pointer to the buffer
        //  */
        // const char* plist_get_data_ptr(plist_t node, uint64_t* length);


        /// <summary>
        /// Get the value of a #PLIST_DATE node.
        /// This function does nothing if node is not of type #PLIST_DATE
        /// </summary>
        /// <param name="node">the node</param>
        /// <param name="sec">a pointer to an int32_t variable. Represents the number of seconds since 01/01/2001.</param>
        /// <param name="usec">a pointer to an int32_t variable. Represents the number of microseconds</param>
        [DllImport(LibPlistLib)]
        public static extern void plist_get_date_val(plist_t node, out int sec, out int usec);


        /// <summary>
        /// Get the value of a #PLIST_UID node.
        /// This function does nothing if node is not of type #PLIST_UID
        /// </summary>
        /// <param name="node">the node</param>
        /// <param name="val">a pointer to a uint64_t variable.</param>
        [DllImport(LibPlistLib)]
        public static extern void plist_get_uid_val(plist_t node, out ulong val);


        /********************************************
         *                                          *
         *                Setters                   *
         *                                          *
         ********************************************/

        /// <summary>
        /// Set the value of a node.
        /// Forces type of node to #PLIST_KEY
        /// </summary>
        /// <param name="node">the node</param>
        /// <param name="val">the key value</param>
        [DllImport(LibPlistLib)]
        public static extern void plist_set_key_val(plist_t node, /* const char * */ string val);


        /// <summary>
        /// Set the value of a node.
        /// Forces type of node to #PLIST_STRING
        /// </summary>
        /// <param name="node">the node</param>
        /// <param name="val">
        /// the string value. The string is copied when set and will be
        /// freed by the node.
        /// </param>
        [DllImport(LibPlistLib)]
        public static extern void plist_set_string_val(plist_t node, /* const char * */ string val);


        /// <summary>
        /// Set the value of a node.
        /// Forces type of node to #PLIST_BOOLEAN
        /// </summary>
        /// <param name="node">the node</param>
        /// <param name="val">the boolean value</param>
        [DllImport(LibPlistLib)]
        public static extern void plist_set_bool_val(plist_t node, byte val);


        /// <summary>
        /// Set the value of a node.
        /// Forces type of node to #PLIST_UINT
        /// </summary>
        /// <param name="node">the node</param>
        /// <param name="val">the unsigned integer value</param>
        [DllImport(LibPlistLib)]
        public static extern void plist_set_uint_val(plist_t node, ulong val);


        /// <summary>
        /// Set the value of a node.
        /// Forces type of node to #PLIST_REAL
        /// </summary>
        /// <param name="node">the node</param>
        /// <param name="val">the real value</param>
        [DllImport(LibPlistLib)]
        public static extern void plist_set_real_val(plist_t node, double val);


        /// <summary>
        /// Set the value of a node.
        /// Forces type of node to #PLIST_DATA
        /// </summary>
        /// <param name="node">the node</param>
        /// <param name="val">
        /// the binary buffer. The buffer is copied when set and will
        /// be freed by the node.
        /// </param>
        /// <param name="length">the length of the buffer</param>
        [DllImport(LibPlistLib)]
        public static extern void plist_set_data_val(plist_t node, /* const char * */ IntPtr val, ulong length);


        /// <summary>
        /// Set the value of a node.
        /// Forces type of node to #PLIST_DATE
        /// </summary>
        /// <param name="node">the node</param>
        /// <param name="sec">the number of seconds since 01/01/2001</param>
        /// <param name="usec">the number of microseconds</param>
        [DllImport(LibPlistLib)]
        public static extern void plist_set_date_val(plist_t node, int sec, int usec);


        /// <summary>
        /// Set the value of a node.
        /// Forces type of node to #PLIST_UID
        /// </summary>
        /// <param name="node">the node</param>
        /// <param name="val">the unsigned integer value</param>
        [DllImport(LibPlistLib)]
        public static extern void plist_set_uid_val(plist_t node, ulong val);


        /********************************************
         *                                          *
         *            Import & Export               *
         *                                          *
         ********************************************/

        /// <summary>
        /// Export the #plist_t structure to XML format.
        /// </summary>
        /// <param name="plist">the root node to export</param>
        /// <param name="plist_xml">
        /// a pointer to a C-string. This function allocates the memory,
        /// caller is responsible for freeing it. Data is UTF-8 encoded.
        /// </param>
        /// <param name="length">a pointer to an uint32_t variable. Represents the length of the allocated buffer.</param>
        [DllImport(LibPlistLib)]
        public static extern void plist_to_xml(plist_t plist, /* char** */ out IntPtr plist_xml, out uint length);


        // /**
        //  * Frees the memory allocated by plist_to_xml().
        //  *
        //  * @param plist_xml The buffer allocated by plist_to_xml().
        //  */
        // void plist_to_xml_free(char *plist_xml);


        /// <summary>
        /// Export the #plist_t structure to binary format.
        /// </summary>
        /// <param name="plist">the root node to export</param>
        /// <param name="plist_bin">
        /// a pointer to a char* buffer. This function allocates the memory,
        /// caller is responsible for freeing it.
        /// </param>
        /// <param name="length">a pointer to an uint32_t variable. Represents the length of the allocated buffer.</param>
        [DllImport(LibPlistLib)]
        public static extern void plist_to_bin(plist_t plist, /* char** */ out IntPtr plist_bin, out uint length);


        // /**
        //  * Frees the memory allocated by plist_to_bin().
        //  *
        //  * @param plist_bin The buffer allocated by plist_to_bin().
        //  */
        // void plist_to_bin_free(char *plist_bin);


        /// <summary>
        /// Import the #plist_t structure from XML format.
        /// </summary>
        /// <param name="plist_xml">a pointer to the xml buffer.</param>
        /// <param name="length">length of the buffer to read.</param>
        /// <param name="plist">a pointer to the imported plist.</param>
        [DllImport(LibPlistLib)]
        public static extern void plist_from_xml(/* const char * */ string plist_xml, uint length, out plist_t plist);


        /// <summary>
        /// Import the #plist_t structure from binary format.
        /// </summary>
        /// <param name="plist_bin">a pointer to the xml buffer.</param>
        /// <param name="length">length of the buffer to read.</param>
        /// <param name="plist">a pointer to the imported plist.</param>
        [DllImport(LibPlistLib)]
        public static extern void plist_from_bin(/* const char * */ IntPtr plist_bin, uint length, out plist_t plist);


        /// <summary>
        /// Import the #plist_t structure from memory data.
        /// This method will look at the first bytes of plist_data
        /// to determine if plist_data contains a binary or XML plist.
        /// </summary>
        /// <param name="plist_data">a pointer to the memory buffer containing plist data.</param>
        /// <param name="length">length of the buffer to read.</param>
        /// <param name="plist">a pointer to the imported plist.</param>
        [DllImport(LibPlistLib)]
        public static extern void plist_from_memory(/* const char * */ IntPtr plist_data, uint length, out plist_t plist);


        /// <summary>
        /// Import the #plist_t structure from memory data.
        /// This method will look at the first bytes of plist_data
        /// to determine if plist_data contains a binary or XML plist.
        /// </summary>
        /// <param name="plist_data">a pointer to the memory buffer containing plist data.</param>
        /// <param name="length">length of the buffer to read.</param>
        /// <param name="plist">a pointer to the imported plist.</param>
        [DllImport(LibPlistLib)]
        public static extern unsafe void plist_from_memory(byte* plist_data, uint length, out plist_t plist);


        /// <summary>
        /// Test if in-memory plist data is binary or XML
        /// This method will look at the first bytes of plist_data
        /// to determine if plist_data contains a binary or XML plist.
        /// This method is not validating the whole memory buffer to check if the
        /// content is truly a plist, it's only using some heuristic on the first few
        /// bytes of plist_data.
        /// </summary>
        /// <param name="plist_data">a pointer to the memory buffer containing plist data.</param>
        /// <param name="length">length of the buffer to read.</param>
        /// <returns>1 if the buffer is a binary plist, 0 otherwise.</returns>
        [DllImport(LibPlistLib)]
        public static extern int plist_is_binary(/* const char * */ IntPtr plist_data, uint length);


        /// <summary>
        /// Test if in-memory plist data is binary or XML
        /// This method will look at the first bytes of plist_data
        /// to determine if plist_data contains a binary or XML plist.
        /// This method is not validating the whole memory buffer to check if the
        /// content is truly a plist, it's only using some heuristic on the first few
        /// bytes of plist_data.
        /// </summary>
        /// <param name="plist_data">a pointer to the memory buffer containing plist data.</param>
        /// <param name="length">length of the buffer to read.</param>
        /// <returns>1 if the buffer is a binary plist, 0 otherwise.</returns>
        [DllImport(LibPlistLib)]
        public static extern unsafe int plist_is_binary(byte* plist_data, uint length);


        /********************************************
         *                                          *
         *                 Utils                    *
         *                                          *
         ********************************************/

        // /**
        //  * Get a node from its path. Each path element depends on the associated father node type.
        //  * For Dictionaries, var args are casted to const char*, for arrays, var args are caster to uint32_t
        //  * Search is breath first order.
        //  *
        //  * @param plist the node to access result from.
        //  * @param length length of the path to access
        //  * @return the value to access.
        //  */
        // plist_t plist_access_path(plist_t plist, uint32_t length, ...);


        /// <summary>
        /// Variadic version of #plist_access_path.
        /// </summary>
        /// <param name="plist">the node to access result from.</param>
        /// <param name="length">length of the path to access</param>
        /// <param name="v">list of array's index and dic'st key</param>
        /// <returns>the value to access.</returns>
        [DllImport(LibPlistLib)]
        public static extern plist_t plist_access_pathv(plist_t plist, uint length, va_list v);


        /// <summary>
        /// Compare two node values
        /// </summary>
        /// <param name="node_l">left node to compare</param>
        /// <param name="node_r">rigth node to compare</param>
        /// <returns>TRUE is type and value match, FALSE otherwise.</returns>
        [DllImport(LibPlistLib)]
        public static extern byte plist_compare_node_value(plist_t node_l, plist_t node_r);


        // Helper macros for the different plist types

        public static bool PLIST_IS_PLIST(plist_t plist) =>
            plist_get_node_type(plist) != plist_type.PLIST_NONE;

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
