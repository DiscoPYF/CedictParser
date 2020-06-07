using System.Collections.Generic;
using System.Threading.Tasks;

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
        /// Reads the next CC-CEDICT entry asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous read operation.
        /// The result of the task contains the next CC-CEDICT entry.</returns>
        Task<CedictEntry> ReadAsync();

        /// <summary>
        /// Reads all the remaining CC-CEDICT entries.
        /// </summary>
        /// <returns>A list of CC-CEDICT entries.</returns>
        IList<CedictEntry> ReadToEnd();

        /// <summary>
        /// Reads all the remaining CC-CEDICT entries asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous read operation.
        /// The result of the task contains a list of CC-CEDICT entries.</returns>
        Task<IList<CedictEntry>> ReadToEndAsync();
    }
}
