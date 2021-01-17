using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FileParsing
{
    public class TextParser
    {
        public (List<string>, StringBuilder) ParseTextToSentences(string data)
        {
            try
            {
                StringBuilder sb = new StringBuilder(data);

                sb.Replace("....", "4dotsMY4DOTS");

                sb.Replace("...", "3dotsMY3DOTS");

                sb.Replace(".", ".$$$").Replace("!", "!$$$").Replace("?", "?$$$").Replace("MY4DOTS", "$$$").Replace("MY3DOTS", "$$$").Replace("  ", "  $$$");

                string[] sentences = sb.ToString().Split("$$$", StringSplitOptions.RemoveEmptyEntries);

                List<string> sentencesList = new List<string>();

                for (int i = 0; i < sentences.Length; i++)
                {
                    if (sentences[i].Contains("4dots"))
                    {
                        sentences[i] = sentences[i].Replace("4dots", "....");
                    }
                    else if (sentences[i].Contains("3dots"))
                    {
                        sentences[i] = sentences[i].Replace("3dots", "...");
                    }

                    sentences[i] = sentences[i].Trim(' ', '\n', '\"', '\'', '*');
                    sentences[i] = sentences[i].Replace('\n', ' ');

                    if (!(sentences[i] == "!" || sentences[i] == "?" || sentences[i] == "." || sentences[i] == "..." || sentences[i] == "...." || sentences[i] == " " || sentences[i] == "  " || sentences[i] == "   " || sentences[i] == "" ||
                        sentences[i] == "|" || sentences[i] == "," || sentences[i].Length == 1))
                    {
                        sentencesList.Add(sentences[i]);
                    }
                }

                return (sentencesList, sb);
            }
            catch (Exception)
            {

                throw new Exception("Something went wrong.");
            }

        }

        public List<string> ParseTextToWords(List<string> sentencesList)
        {
            try
            {
                List<string> words = new List<string>();

                for (int i = 0; i < sentencesList.Count; i++)
                {
                    sentencesList[i] = sentencesList[i].Replace("-", "&");
                    sentencesList[i] = sentencesList[i].Replace("_", "&");
                    sentencesList[i] = sentencesList[i].Replace("	", "&");
                    sentencesList[i] = sentencesList[i].Replace("	", "&");
                    sentencesList[i] = sentencesList[i].Replace("	", "&");
                    sentencesList[i] = new string(sentencesList[i].Select(c => char.IsDigit(c) ? '*' : c).ToArray());

                    string[] arr = sentencesList[i].ToString().Split('.', ',', '!', '?', '\"', '\'', '(', ')', '[', ']', ';', ':', '&', '/', '#', '%', '№', '*', '|', '+', '~', ' ', '\n', '=', '$');
                    arr = arr.Select(x => x.Replace(" ", "")).ToArray();

                    foreach (var item in arr)
                    {
                        if (!(item == " " || item == "  " || item == "   " || item == ""))
                        {
                            words.Add(item);
                        }
                    }
                }

                return words;
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong.");
            }
        }

        public void ParseTextToPunctuationMarks(string data, StringBuilder sb)
        {
            try
            {
                sb.Replace("4dots", "\\");
                sb.Replace("3dots", "{");

                var fourDots = sb.ToString().Count(x => x == '\\');
                var threeDots = sb.ToString().Count(x => x == '{');
                var oneDots = sb.ToString().Count(x => x == '.');
                var commas = sb.ToString().Count(x => x == ',');
                var exclamationMarks = sb.ToString().Count(x => x == '!');
                var questionMarks = sb.ToString().Count(x => x == '?');
                var quotes = sb.ToString().Count(x => x == '\"');
                var apostrophes = sb.ToString().Count(x => x == '\'');
                var rightParenthesises = sb.ToString().Count(x => x == ')');
                var leftParenthesises = sb.ToString().Count(x => x == '(');
                var leftSquareBrackets = sb.ToString().Count(x => x == '[');
                var rightSquareBrackets = sb.ToString().Count(x => x == ']');
                var semicolons = sb.ToString().Count(x => x == ';');
                var colons = sb.ToString().Count(x => x == ':');
                var dashes = sb.ToString().Count(x => x == '-');
                var slashes = data.Count(x => x == '/');

                Console.WriteLine($"Number of punctuation marks in the text:" +
                    $"\nfour-dots sign: {fourDots}" +
                    $"\nellipsis: {threeDots}" +
                    $"\ndots: {oneDots}" +
                    $"\ncommas: {commas}" +
                    $"\nexclamation marks: {exclamationMarks}" +
                    $"\nquestion marks: {questionMarks}" +
                    $"\nquotes: {quotes}" +
                    $"\napostrophes: {apostrophes}" +
                    $"\nright parenthesises: {rightParenthesises}" +
                    $"\nleft parenthesises: {leftParenthesises}" +
                    $"\nleft square brackets: {leftSquareBrackets}" +
                    $"\nrigth square brackets: {rightSquareBrackets}" +
                    $"\nsemicolons: {semicolons}" +
                    $"\ncolons: {colons}" +
                    $"\ndashes: {dashes}" +
                    $"\nslashes: {slashes}");
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong.");
            }
        }

        public string FindTheMostCommonLetter(string data)
        {
            try
            {
                data = data.Replace(" ", "").Replace("\n", "").Replace("-", "").Replace("\"", "").Replace("\'", "").Replace("*", "").Replace(".", "").Replace(",", "")
                                    .Replace("!", "").Replace("?", "").Replace("(", "").Replace(")", "").Replace("[", "").Replace("]", "").Replace(";", "").Replace(":", "").Replace("&", "")
                                    .Replace("/", "").Replace("#", "").Replace("%", "").Replace("#", "").Replace("№", "").Replace("^", "").Replace("|", "").Replace("+", "").Replace("~", "");

                var mostCommonLetter = data.ToLower().OrderBy(x => x).GroupBy(x => x);

                var counter = 0;
                var mostCommonLetter2 = '&';

                foreach (var item in mostCommonLetter)
                {
                    if (item.Count() > counter)
                    {
                        counter = item.Count();
                        mostCommonLetter2 = item.Key;
                    }
                }

                return $"       \n3) Most common letter is \"{mostCommonLetter2}\", it can be found {counter} times in text";
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong.");
            }
        }
    }
}
