namespace CedictParserLib
{
    /// <summary>
    /// Represents one CC-CEDICT entry.
    /// </summary>
    public class CedictEntry
    {
        /// <summary>
        /// The word in traditional Chinese.
        /// </summary>
        public string Traditional { get; set; }

        /// <summary>
        /// The word in simplified Chinese.
        /// </summary>
        public string Simplified { get; set; }

        /// <summary>
        /// The Mandarin pinyin.
        /// </summary>
        public string Pinyin { get; set; }

        /// <summary>
        /// The English definitions in American English.
        /// </summary>
        public string[] Definitions { get; set; }
    }
}
