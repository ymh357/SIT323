using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SIT323
{
    class Validater
    {
        public bool ValidateWordlist(String path)
        {
            bool isValid = true;
            String RE_validMatch = "^[a-zA-Z]+$";
            // Separated by commas, from the start to the end of a word contains only more than 1 letter.

            List<String> wordList = new List<string>();
            foreach(String line in File.ReadAllLines(path))
            {
                foreach(String word in line.Split(new char[] { ',' }))
                {
                    // If the word is invalid.
                    if(! System.Text.RegularExpressions.Regex.IsMatch(word, RE_validMatch))
                    {
                        isValid = false;
                        if(String.IsNullOrWhiteSpace(word))
                        {
                            Console.WriteLine("Invalid word is empty or white space.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid word contain non-letter character.");
                        }
                        continue;
                    }

                    // If the word is a real word.
                    else
                    {
                        // If dumplicate.
                        if (wordList.Contains(word))
                        {
                            isValid = false;
                            Console.WriteLine("Duplicated words exist.");
                            continue;
                        }
                        else
                        {
                            wordList.Add(word);
                        }
                    }
                }
            }
            return isValid;
        }
    }
}
