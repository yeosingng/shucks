using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Shucks.Services.Hangman
{
    class HangmanWordGenerator
    {
        private List<string> words;
        Random rand;
        private string filePath = "C:\\Users\\yeosi\\source\\repos\\Shucks\\Shucks\\Services\\Hangman\\Words.txt";

        public HangmanWordGenerator()
        {
            rand = new Random();
            words = new List<string>();

            Stream stream;

            try
            {
                stream = File.Open(filePath, FileMode.Open);
                using (StreamReader sr = new StreamReader(stream))
                {
                    SetUpWordBank(sr);
                };
            }
            catch (FileNotFoundException e)
            {
                throw e;
            }
            catch (DirectoryNotFoundException e)
            {
                throw e;
            }
            catch (IOException e)
            {
                throw e;
            }
        }
        
        private void SetUpWordBank(StreamReader sr)
        {
            string word;
            while ((word = sr.ReadLine()) != null)
            {

                words.Add(word);
            }

            sr.Dispose();
        }

        public string GenerateRandomWord()
        {
            int index = rand.Next(words.Count);
            return words.ElementAt(index);
        }
    }
}
