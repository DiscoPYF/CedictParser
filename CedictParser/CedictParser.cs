using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace CedictParser
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
    }
}
