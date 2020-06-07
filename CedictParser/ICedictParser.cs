using System.Collections.Generic;

namespace CedictParser
{
    /// <summary>
    /// Defines a CC-CEDICT parser.
    /// </summary>
    public interface ICedictParser
    {
        /// <summary>
        /// Reads the next CC-CEDICT entry.
        /// </summary>
        /// <returns>The next CC-CEDICT entry or null
        /// if there is nothing left to read.</returns>
        CedictEntry Read();

        /// <summary>
        /// Read all the remaining CC-CEDICT entries.
        /// </summary>
        /// <returns>A list of CC-CEDICT entries or null
        /// if there is nothing left to read.</returns>
        IList<CedictEntry> ReadToEnd();
    }
}
