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
        /// <returns>The next CC-CEDICT entry.</returns>
        CedictEntry Read();

        /// <summary>
        /// Read all the remaining CC-CEDICT entries.
        /// </summary>
        /// <returns>A list of CC-CEDICT entries.</returns>
        IList<CedictEntry> ReadToEnd();
    }
}
