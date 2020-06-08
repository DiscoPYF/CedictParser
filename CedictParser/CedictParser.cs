using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CedictParserLib
{
    /// <summary>
    /// An implementation of <see cref="ICedictParser"/> that
    /// reads CC-CEDICT entries from a stream.
    /// </summary>
    public class CedictParser : ICedictParser, IDisposable
    {
        private StreamReader reader;

        private readonly Regex regex;

        private static readonly string pattern = @"(\S+) (\S+) (\[.+\]) (/.+/)";

        private const string commentToken = "#";

        /// <summary>
        /// Creates a new instance of <see cref="CedictParser"/>
        /// with the provided <see cref="StreamReader"/> to parse from.
        /// </summary>
        /// <param name="reader">The <see cref="StreamReader"/> to read data from for parsing.</param>
        public CedictParser(StreamReader reader)
        {
            this.reader = reader;
            regex = new Regex(pattern);
        }

        /// <summary>
        /// Reads the next CC-CEDICT entry from the stream.
        /// </summary>
        /// <returns>The next CC-CEDICT entry or null
        /// if there is nothing left to read.</returns>
        public CedictEntry Read()
        {
            string line;

            do
            {
                line = reader.ReadLine();
            }
            while (line != null && line.StartsWith(commentToken));

            if (line == null)
            {
                return null;
            }

            return ParseEntry(line);
        }

        /// <summary>
        /// Reads the next CC-CEDICT entry asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous read operation.
        /// The result of the task contains the next CC-CEDICT entry.</returns>
        public async Task<CedictEntry> ReadAsync()
        {
            string line;

            do
            {
                line = await reader.ReadLineAsync();
            }
            while (line != null && line.StartsWith(commentToken));

            if (line == null)
            {
                return null;
            }

            return ParseEntry(line);
        }

        /// <summary>
        /// Read all the remaining CC-CEDICT entries.
        /// </summary>
        /// <returns>A list of CC-CEDICT entries.
        /// The list is empty if there is nothing else to read.</returns>
        public IList<CedictEntry> ReadToEnd()
        {
            var result = new List<CedictEntry>();

            CedictEntry entry;

            do
            {
                entry = Read();

                if (entry != null)
                {
                    result.Add(entry);
                }
            }
            while (entry != null);

            return result;
        }

        /// <summary>
        /// Reads all the remaining CC-CEDICT entries asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous read operation.
        /// The result of the task contains a list of CC-CEDICT entries.</returns>
        public async Task<IList<CedictEntry>> ReadToEndAsync()
        {
            var result = new List<CedictEntry>();

            CedictEntry entry;

            do
            {
                entry = await ReadAsync();

                if (entry != null)
                {
                    result.Add(entry);
                }
            }
            while (entry != null);

            return result;
        }

        /// <summary>
        /// Disposes the underlying stream reader.
        /// </summary>
        public void Dispose()
        {
            if (reader != null)
            {
                try
                {
                    reader.Dispose();
                }
                finally
                {
                    reader = null;
                }
            }
        }

        private CedictEntry ParseEntry(string line)
        {
            var entry = new CedictEntry();

            Match match = regex.Match(line);

            if (match.Success)
            {
                // Skip the first match because it's the whole line
                entry.Traditional = match.Groups[1].Value;
                entry.Simplified = match.Groups[2].Value;
                entry.Pinyin = match.Groups[3].Value.Trim('[', ']');

                string definitionsValue = match.Groups[4].Value;

                entry.Definitions = definitionsValue.Trim('/').Split('/');
            }

            return entry;
        }
    }
}
