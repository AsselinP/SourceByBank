using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceByBank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n\n---Starting---");
            string projectPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            List<string> sourcesPaths = new List<string>();

            string[] filesInDirectory = null;

            List<MemoryBank> gameMemoryBanks = new List<MemoryBank>();

            bool fileBankIsANewBank = true;
            int bankTemp = 0;


            // DEBUG
            sourcesPaths.Add(projectPath + "\\src");
            sourcesPaths.Add(projectPath + "\\sfx");
            sourcesPaths.Add(projectPath + "\\sprites");


            foreach (string path in sourcesPaths)
            {
                Console.WriteLine(path);
            }
            // DEBUG

            
            foreach (string path in sourcesPaths)
            {
                filesInDirectory = Directory.GetFiles(path, "*.c", SearchOption.AllDirectories);
                foreach (string file in filesInDirectory)
                {
                    Console.WriteLine(file);

                }

                foreach (string file in filesInDirectory)
                {
                    fileBankIsANewBank = true;
                    string[] lines = File.ReadAllLines(file);
                    string firstOccurrence = lines.FirstOrDefault(l => l.Contains("#pragma bank"));
                    if (firstOccurrence != null)
                    {
                        firstOccurrence = firstOccurrence.Substring(13, 1);
                        int.TryParse(firstOccurrence, out bankTemp);
                        Console.WriteLine(bankTemp);
                        foreach (MemoryBank bank in gameMemoryBanks)
                        {
                            if (bankTemp == bank.n)
                            {
                                // add the current file to existing bank
                                fileBankIsANewBank = false;
                                bank.files.Add(file);
                            }
                        }

                        if (fileBankIsANewBank)
                        {
                            gameMemoryBanks.Add(new MemoryBank(bankTemp, file));
                        }
                    }
                }
            }

            Console.WriteLine("\n\n show result");
            foreach (MemoryBank bank in gameMemoryBanks)
            {
                Console.WriteLine($"{bank.n}");

                foreach (var file in bank.files)
                {
                    Console.WriteLine($"{file}");
                }
            }


            while (true) 
            {
            }
        }


        public class MemoryBank
        {
            public int n;  // index of memoryBank
            public List<string> files; // files in this memoryBank
            List<int> memoryByFiles;    // to do

            public MemoryBank(int n, string file)
            {
                this.n = n;
                files = new List<string>();
                files.Add(file);
            }

        }
    }
}
