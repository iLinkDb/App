using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace IlinkDb.Data.MemoryDb
{
    public class RandomData
    {
        // private readonly Random _random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
        private Random _random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
        private readonly string[] _words = new[]
                                      {
                                          "lorem", "ipsum", "dolor", "sit", "amet", "nonumy", "eirmod", "tempor",
                                          "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed",
                                          "diam", "voluptua", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo",
                                          "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea"
                                          , "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem"
                                          , "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "sed",
                                          "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et", "dolore",
                                          "magna", "aliquyam", "erat", "sed", "diam", "voluptua", "at", "vero", "eos", "et"
                                          , "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet",
                                          "clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem",
                                          "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor", "sit", "amet",
                                          "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor",
                                          "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed",
                                          "diam", "voluptua", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo",
                                          "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea"
                                          , "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "duis",
                                          "autem", "vel", "eum", "iriure", "dolor", "in", "hendrerit", "in", "vulputate",
                                          "velit", "esse", "molestie", "consequat", "vel", "illum", "dolore", "eu",
                                          "feugiat", "nulla", "facilisis", "at", "vero", "eros", "et", "accumsan", "et",
                                          "iusto", "odio", "dignissim", "qui", "blandit", "praesent", "luptatum", "zzril",
                                          "delenit", "augue", "duis", "dolore", "te", "feugait", "nulla", "facilisi",
                                          "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer", "adipiscing", "elit",
                                          "sed", "diam", "nonummy", "nibh", "euismod", "tincidunt", "ut", "laoreet",
                                          "dolore", "magna", "aliquam", "erat", "volutpat", "ut", "wisi", "enim", "ad",
                                          "minim", "veniam", "quis", "nostrud", "exerci", "tation", "ullamcorper",
                                          "suscipit", "lobortis", "nisl", "ut", "aliquip", "ex", "ea", "commodo",
                                          "consequat", "duis", "autem", "vel", "eum", "iriure", "dolor", "in", "hendrerit",
                                          "in", "vulputate", "velit", "esse", "molestie", "consequat", "vel", "illum",
                                          "dolore", "eu", "feugiat", "nulla", "facilisis", "at", "vero", "eros", "et",
                                          "accumsan", "et", "iusto", "odio", "dignissim", "qui", "blandit", "praesent",
                                          "luptatum", "zzril", "delenit", "augue", "duis", "dolore", "te", "feugait",
                                          "nulla", "facilisi", "nam", "liber", "tempor", "cum", "soluta", "nobis",
                                          "eleifend", "option", "congue", "nihil", "imperdiet", "doming", "id", "quod",
                                          "mazim", "placerat", "facer", "possim", "assum", "lorem", "ipsum", "dolor", "sit"
                                          , "amet", "consectetuer", "adipiscing", "elit", "sed", "diam", "nonummy", "nibh",
                                          "euismod", "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat",
                                          "volutpat", "ut", "wisi", "enim", "ad", "minim", "veniam", "quis", "nostrud",
                                          "exerci", "tation", "ullamcorper", "suscipit", "lobortis", "nisl", "ut",
                                          "aliquip", "ex", "ea", "commodo", "consequat", "duis", "autem", "vel", "eum",
                                          "iriure", "dolor", "in", "hendrerit", "in", "vulputate", "velit", "esse",
                                          "molestie", "consequat", "vel", "illum", "dolore", "eu", "feugiat", "nulla",
                                          "facilisis", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo",
                                          "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea"
                                          , "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem"
                                          , "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "sed",
                                          "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et", "dolore",
                                          "magna", "aliquyam", "erat", "sed", "diam", "voluptua", "at", "vero", "eos", "et"
                                          , "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet",
                                          "clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem",
                                          "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor", "sit", "amet",
                                          "consetetur", "sadipscing", "elitr", "at", "accusam", "aliquyam", "diam", "diam",
                                          "dolore", "dolores", "duo", "eirmod", "eos", "erat", "et", "nonumy", "sed",
                                          "tempor", "et", "et", "invidunt", "justo", "labore", "stet", "clita", "ea", "et",
                                          "gubergren", "kasd", "magna", "no", "rebum", "sanctus", "sea", "sed", "takimata",
                                          "ut", "vero", "voluptua", "est", "lorem", "ipsum", "dolor", "sit", "amet",
                                          "consetetur", "sadipscing", "elitr", "sed", "diam", "consetetur", "sadipscing", "elitr",
                                          "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et",
                                          "dolore", "magna", "aliquyam", "erat", "consetetur", "sadipscing", "elitr", "sed",
                                          "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et",
                                          "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua", "at", "vero",
                                          "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum",
                                          "stet", "clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est",
                                          "lorem", "ipsum"
                                      };

        public RandomData()
        {
            // Without this, sometimes two tests will get the same seed value.
            System.Threading.Thread.Sleep(1);
        }

        public DateTime Date(int minYear, int maxYear)
        {
            return Date(new DateTime(minYear, 1, 1), new DateTime(maxYear - 1, 12, 31, 23, 59, 59));
        }

        public DateTime Date(DateTime min, DateTime max)
        {
            return new DateTime(Long(min.Ticks, max.Ticks));
        }

        public long Long(long min, long max)
        {
            return min + Convert.ToInt64((max - min) * _random.NextDouble());
        }

        public string Ipsum(int minWords, int maxWords)
        {
            return Ipsum(_random.Next(minWords, maxWords));
        }

        public string Word()
        {
            return _words[_random.Next(_words.Length - 1)];
        }

        public string Ipsum(int numWords)
        {
            var result = new StringBuilder("lorem");
            for (var i = 1; i < 5; i++)
            {
                if (i >= numWords)
                    return result.ToString();
                result.Append(" ").Append(_words[i - 1]);
            }
            for (var i = 6; i < numWords; i++)
                result.Append(" ").Append(Word());
            return result.ToString();
        }

        public int Int(int min, int max)
        {
            return _random.Next(min, max);
        }

        public int Int(int max)
        {
            return _random.Next(max);
        }

        public string Format(string format)
        {
            var sb = new StringBuilder();
            foreach (var ch in format)
            {
                switch (ch)
                {
                    case '0':
                        sb.Append(_random.Next(9).ToString(CultureInfo.InvariantCulture));
                        break;
                    case 'U':
                        sb.Append(Convert.ToChar(_random.Next(65, 90)));
                        break;
                    case 'l':
                        sb.Append(Convert.ToChar(_random.Next(97, 122)));
                        break;
                    default:
                        sb.Append(ch);
                        break;
                }
            }
            return sb.ToString();
        }

        public T Item<T>(List<T> list)
        {
            return list[_random.Next(list.Count - 1)];
        }

        public TK Value<T, TK>(Dictionary<T, TK> dictionary)
        {
            var keys = dictionary.Keys.ToList();
            return dictionary[Item(keys)];
        }

        private string GenString(int size, int minChr, int maxChr)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                sb.Append(Convert.ToChar(_random.Next(minChr, maxChr)));
            }
            return sb.ToString();
        }

        public string NumberString(int size)
        {
            return GenString(size, 48, 57);
        }

        public string Upper(int size)
        {
            return GenString(size, 65, 90);
        }

        public string Lower(int size)
        {
            return GenString(size, 97, 122);
        }

        public string Phone()
        {
            return Format("555-000-0000");
        }
    }
}
