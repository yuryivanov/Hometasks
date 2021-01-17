using System;
using System.IO;
using System.Threading.Tasks;
using System.Text;

namespace FileParsing
{
    public class ReaderAndWriter
    {
        public async Task<string> ReadFile()
        {
            try
            {
                var path = @"D:\sample.txt";

                var data = "";

                using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
                {
                    data = await sr.ReadToEndAsync();
                    return data;
                }
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong.");
            }
        }
        public async Task WriteFileNew(string text, string pathForNewFile)
        {
            try
            {
                await using (StreamWriter sw = new StreamWriter(pathForNewFile, false, Encoding.UTF8)) //false - перезаписываем в файл
                {
                    await sw.WriteAsync(text);
                }
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong.");
            }
        }

        public async Task WriteFileOld(string text, string pathForNewFile)
        {
            try
            {
                await using (StreamWriter sw = new StreamWriter(pathForNewFile, true, Encoding.UTF8)) //true - пишем дальше в файл
                {
                    await sw.WriteAsync(text);
                }
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong.");
            }
        }
    }
}
