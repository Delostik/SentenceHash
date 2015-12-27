using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentenceHash
{
    class Program
    {
        static string vocabFile;
        static string inputFile;
        static string outputFile;
        static int batchsize;
        static vocab dict = null;

        static int nLines = 0;
        static int maxElementsPerBatch = 0;

        static void Process()
        {
            dict = new vocab(vocabFile);
            StreamReader reader = new StreamReader(inputFile);
            StreamWriter Twriter = new StreamWriter(outputFile + ".txt");
            BinaryWriter Bwriter = new BinaryWriter(File.Open(outputFile + ".bin", FileMode.Create));
            string line;
            Batch batch = new Batch();

            while ((line = reader.ReadLine()) != null)
            {
                nLines++;
                if (batch.BatchSize == batchsize)
                {
                    maxElementsPerBatch = Math.Max(maxElementsPerBatch, batch.ElementSize);
                    batch.writeTextBatch(Twriter);
                    batch.writeBinaryBatch(Bwriter);
                    batch.init();
                }
                string[] words = line.Trim().Split(' ');
                List<int> sample = new List<int>();
                int featureId = int.Parse(words[0]);
                for (int i = 1; i < words.Count<string>(); i++)
                {
                    sample.Add(dict.getIndex(words[i]));
                }
                batch.addSample(sample, featureId);
            }
            batch.writeTextBatch(Twriter);
            batch.writeBinaryBatch(Bwriter);

            Twriter.Write(nLines);
            Twriter.Write(batchsize);
            Twriter.Write(maxElementsPerBatch);

            Bwriter.Write(nLines);
            Bwriter.Write(batchsize);
            Bwriter.Write(maxElementsPerBatch);

            Twriter.Close();
            Bwriter.Close();
        }

        static void DispHelp()
        {
            Console.WriteLine("SentenceHash.exe vocabfile inputfile outputfile batchsize");
        }

        static void Main(string[] args)
        {
            try
            {
                if (args.Length != 4)
                {
                    DispHelp();
                }
                else
                {
                    vocabFile = args[0];
                    inputFile = args[1];
                    outputFile = args[2];
                    batchsize = int.Parse(args[3]);
                    Process();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.ToString());
                Environment.Exit(0);
            }
        }
    }
}
