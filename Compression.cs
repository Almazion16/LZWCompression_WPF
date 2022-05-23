using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZWCompression
{
    class Compression
    {
        public static readonly string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяЁЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ><\n\r.`,!?-+=()*:;{}—/\"_'[]";

        public static List<int> ToCompress(string normal_text)
        {


            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            for (int i = 0; i < alphabet.Length; i++)
                dictionary.Add(alphabet[i].ToString(), i);


            string w = string.Empty;
            List<int> compressed = new List<int>();

            foreach (char c in normal_text)
            {

                string wc = w + c;
                if (dictionary.ContainsKey(wc))
                {
                    w = wc;
                }
                else
                {

                    compressed.Add(dictionary[w]);

                    dictionary.Add(wc, dictionary.Count);
                    w = c.ToString();
                }
            }


            if (!string.IsNullOrEmpty(w))
                compressed.Add(dictionary[w]);

            return compressed;
        }

        public static string ToDecompress(List<int> compressed)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            for (int i = 0; i < alphabet.Length; i++)
                dictionary.Add(i, alphabet[i].ToString());

            string w = dictionary[compressed[0]];
            compressed.RemoveAt(0);
            StringBuilder decompressed = new StringBuilder(w);

            foreach (int k in compressed)
            {
                string entry = null;
                if (dictionary.ContainsKey(k))
                    entry = dictionary[k];
                else if (k == dictionary.Count)
                    entry = w + w[0];

                decompressed.Append(entry);
                dictionary.Add(dictionary.Count, w + entry[0]);

                w = entry;
            }

            return decompressed.ToString();
        }

    }
}

