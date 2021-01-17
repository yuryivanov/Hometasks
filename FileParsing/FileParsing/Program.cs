using System;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FileParsing
{
    class Program
    {
        static async Task Main()
        {
            try
            {
                ReaderAndWriter readerAndWriter = new ReaderAndWriter();
                Task<string> task = readerAndWriter.ReadFile();

                //1.1 Parse the proposed document into sentences
                TextParser textParser = new TextParser();
                List<string> parsedSentences = textParser.ParseTextToSentences(task.Result).Item1;
                StringBuilder sb = textParser.ParseTextToSentences(task.Result).Item2;

                //1.2 Parse the proposed document into words
                List<string> parsedWords = textParser.ParseTextToWords(parsedSentences);

                //1.3 Parse the proposed document into punctuation marks
                textParser.ParseTextToPunctuationMarks(task.Result, sb);

                //2 Output words in alphabetical order to 2nd file, indicating the number of times the word has been used.
                var groupedWords = parsedWords.OrderBy(x => x).GroupBy(x => x);
                StringBuilder sb2 = new StringBuilder("");

                foreach (var item in groupedWords)
                {
                    sb2.Append(item.Key + " - " + item.Count() + "\n");
                }

                readerAndWriter.WriteFileNew(sb2.ToString(), @"D:\sample2.txt");

                //Output the longest sentence by the number of characters into 3rd file
                var longestSentenceByWords = parsedSentences.OrderByDescending(x => x.Length).First();
                readerAndWriter.WriteFileOld("\n1) [Sentence with highest number of characters:]       \n" + longestSentenceByWords, @"D:\sample3.txt");


                //In 3rd file, output the shortest sentence by the number of words
                readerAndWriter.WriteFileOld("\n2) [Sentence with shortest number of characters:]       \n" + parsedSentences.OrderByDescending(x => x.Length).Last(), @"D:\sample3.txt");

                //In 3rd file, print the most common letter          
                readerAndWriter.WriteFileOld(textParser.FindTheMostCommonLetter(task.Result), @"D:\sample3.txt");

                Thread.Sleep(1000);
            }
            catch (Exception)
            {

                throw new Exception("Something went wrong.");
            }

        }
    }
}
