using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentenceHash
{
    class Batch
    {
        List<int> elementIdx = new List<int>();
        List<int> sampleIdx = new List<int>();
        List<int> feaIdx = new List<int>();

        public int BatchSize { get { return sampleIdx.Count; } }

        public int ElementSize { get { return elementIdx.Count; } }

        public void addSample(List<int> sample, int fea)
        {
            sampleIdx.Add(elementIdx.Count);
            feaIdx.Add(fea);
            elementIdx.AddRange(sample);
        }

        public void init()
        {
            elementIdx.Clear();
            sampleIdx.Clear();
        }

        public void writeTextBatch(StreamWriter sw)
        {
            sw.Write(ElementSize);
            foreach(int i in sampleIdx)
            {
                sw.Write(i);
            }
            foreach(int i in feaIdx)
            {
                sw.Write(i);
            }
            foreach(int i in elementIdx)
            {
                sw.Write(i);
            }
        }

        public void writeBinaryBatch(BinaryWriter bw)
        {
            bw.Write(ElementSize);
            foreach (int i in sampleIdx)
            {
                bw.Write(i);
            }
            foreach (int i in feaIdx)
            {
                bw.Write(i);
            }
            foreach (int i in elementIdx)
            {
                bw.Write(i);
            }
        }
    }
}
