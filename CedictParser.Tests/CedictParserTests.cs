using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CedictParser.Tests
{
    [TestClass]
    public class CedictParserTests
    {
        [TestMethod]
        public void Read_ShouldReturnNextEntry()
        {
            string s = "你好 你好 [ni3 hao3] /hello/hi/\n" +
                "再見 再见 [zai4 jian4] /goodbye/see you again later/";

            byte[] b = Encoding.UTF8.GetBytes(s);

            var stream = new MemoryStream(b);

            using (var reader = new StreamReader(stream))
            {
                var parser = new CedictParser(reader);

                CedictEntry entry = parser.Read();

                Assert.IsNotNull(entry);
                Assert.AreEqual("你好", entry.Traditional);
                Assert.AreEqual("你好", entry.Simplified);
                Assert.AreEqual("ni3 hao3", entry.Pinyin);
                Assert.IsNotNull(entry.Definitions);
                Assert.AreEqual(2, entry.Definitions.Length);
                Assert.AreEqual("hello", entry.Definitions[0]);
                Assert.AreEqual("hi", entry.Definitions[1]);

                entry = parser.Read();

                Assert.IsNotNull(entry);
                Assert.AreEqual("再見", entry.Traditional);
                Assert.AreEqual("再见", entry.Simplified);
                Assert.AreEqual("zai4 jian4", entry.Pinyin);
                Assert.IsNotNull(entry.Definitions);
                Assert.AreEqual(2, entry.Definitions.Length);
                Assert.AreEqual("goodbye", entry.Definitions[0]);
                Assert.AreEqual("see you again later", entry.Definitions[1]);
            }
        }

        [TestMethod]
        public void Read_ShouldReturnNull_WhenNothingLeftToRead()
        {
            string s = "你好 你好 [ni3 hao3] /hello/hi/";

            byte[] b = Encoding.UTF8.GetBytes(s);

            var stream = new MemoryStream(b);

            using (var reader = new StreamReader(stream))
            {
                var parser = new CedictParser(reader);

                CedictEntry entry = parser.Read();

                Assert.IsNotNull(entry);

                entry = parser.Read();

                Assert.IsNull(entry);
            }
        }

        [TestMethod]
        public void Read_ShouldReturnEntryWithDefaultPropertyValues_WhenEntryIsMalformed()
        {
            string s = "你好你好 [ni3 hao3] /hello/hi/";

            byte[] b = Encoding.UTF8.GetBytes(s);

            var stream = new MemoryStream(b);

            using (var reader = new StreamReader(stream))
            {
                var parser = new CedictParser(reader);

                CedictEntry entry = parser.Read();

                Assert.IsNotNull(entry);
                Assert.IsNull(entry.Traditional);
                Assert.IsNull(entry.Simplified);
                Assert.IsNull(entry.Pinyin);
                Assert.IsNull(entry.Definitions);
            }
        }

        [TestMethod]
        public void Read_ShouldSkipComments()
        {
            string s = "# This is a comment\n" +
                "再見 再见 [zai4 jian4] /goodbye/see you again later/";

            byte[] b = Encoding.UTF8.GetBytes(s);

            var stream = new MemoryStream(b);

            using (var reader = new StreamReader(stream))
            {
                var parser = new CedictParser(reader);

                CedictEntry entry = parser.Read();

                Assert.IsNotNull(entry);
                Assert.AreEqual("再見", entry.Traditional);
                Assert.AreEqual("再见", entry.Simplified);
                Assert.AreEqual("zai4 jian4", entry.Pinyin);
                Assert.IsNotNull(entry.Definitions);
                Assert.AreEqual(2, entry.Definitions.Length);
                Assert.AreEqual("goodbye", entry.Definitions[0]);
                Assert.AreEqual("see you again later", entry.Definitions[1]);
            }
        }

        [TestMethod]
        public void ReadToEnd_ShouldReturnAllEntries()
        {
            string s = "你好 你好 [ni3 hao3] /hello/hi/\n" +
                "再見 再见 [zai4 jian4] /goodbye/see you again later/";

            byte[] b = Encoding.UTF8.GetBytes(s);

            var stream = new MemoryStream(b);

            using (var reader = new StreamReader(stream))
            {
                var parser = new CedictParser(reader);

                IList<CedictEntry> entries = parser.ReadToEnd();

                Assert.IsNotNull(entries);
                Assert.AreEqual(2, entries.Count);

                CedictEntry entry = entries[0];

                Assert.IsNotNull(entry);
                Assert.AreEqual("你好", entry.Traditional);
                Assert.AreEqual("你好", entry.Simplified);
                Assert.AreEqual("ni3 hao3", entry.Pinyin);
                Assert.IsNotNull(entry.Definitions);
                Assert.AreEqual(2, entry.Definitions.Length);
                Assert.AreEqual("hello", entry.Definitions[0]);
                Assert.AreEqual("hi", entry.Definitions[1]);

                entry = entries[1];

                Assert.IsNotNull(entry);
                Assert.AreEqual("再見", entry.Traditional);
                Assert.AreEqual("再见", entry.Simplified);
                Assert.AreEqual("zai4 jian4", entry.Pinyin);
                Assert.IsNotNull(entry.Definitions);
                Assert.AreEqual(2, entry.Definitions.Length);
                Assert.AreEqual("goodbye", entry.Definitions[0]);
                Assert.AreEqual("see you again later", entry.Definitions[1]);
            }
        }
    }
}
