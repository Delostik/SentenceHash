using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace SentenceHash
{
    class vocab
    {
        Dictionary<string, int> dict = new Dictionary<string, int>();

        public vocab(string vocabFile)
        {
            StreamReader reader = new StreamReader(vocabFile);
            string line = "";
            int idx = -1;
            while ((line = reader.ReadLine()) != null)
            {
                idx++;
                string word = line.Trim().Split(' ')[0];
                dict.Add(word, idx);
            }
            reader.Close();
        }

        public int getIndex(string word)
        {
            if (dict.ContainsKey(word))
            {
                return dict[word];
            }
            return -1;
        }


    }
}
