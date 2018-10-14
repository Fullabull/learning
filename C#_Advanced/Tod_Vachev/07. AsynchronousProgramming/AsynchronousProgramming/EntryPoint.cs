using System;
using System.Threading.Tasks;

namespace AsynchronousProgramming
{
    class EntryPoint
    {
        static void Main()
        {
            int count = 200000;
            char charToConcatenate = '1';

            // Asynchronous call of the async ConcatenateChars method
            Task<string> t = ConcatenateCharsAsync(charToConcatenate, count);

            // This line of code will be executed asynchronously
            Console.WriteLine("In progress");
                       
            Console.WriteLine("The length of the result is " + t.Result.Length);

            // Normal Synchronous Call of the ConcatenateChars method
            ConcatenateChars(charToConcatenate, count);
        }

        public static string ConcatenateChars(char charToConcatenate, int count)
        {
            string concatenatedString = string.Empty;

            for (int i = 0; i < count; i++)
            {
                concatenatedString += charToConcatenate;
            }

            return concatenatedString;
        }

        public async static Task<string> ConcatenateCharsAsync(char charToConcatenate, int count)
        {
            return await Task<string>.Factory.StartNew(() =>
            {
                return ConcatenateChars(charToConcatenate, count);
            });
        }
    }
}