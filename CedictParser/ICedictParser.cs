using System.Collections.Generic;

namespace CedictParser
{
    /// <summary>
    /// Defines a CEDICT parser.
    /// </summary>
    public interface ICedictParser
    {
        /// <summary>
        /// Reads the next CEDICT entry.
        /// </summary>
        /// <returns>The next CEDICT entry or null
        /// if there is nothing left to read.</returns>
        CedictEntry Read();

        /// <summary>
        /// Read all the CEDICT entries.
        /// </summary>
        /// <returns>A list of CEDICT entries or null
        /// if there is nothing left to read.</returns>
        IList<CedictEntry> ReadAll();
    }
}
