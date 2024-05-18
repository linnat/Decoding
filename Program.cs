using System.Text;
using System.Text.RegularExpressions;

namespace Decoding
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var result = Decode("coding_qual_input.txt");
            Console.WriteLine(result);
            Console.ReadKey();
        }

        static string Decode(string message_file)
        {
            string result = string.Empty;
            // Get input file
            string dir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!;
            string filePath = Path.Combine(dir, message_file);
            // file exists
            if (System.IO.File.Exists(filePath))
            {
                // Get all lines
                var lines = System.IO.File.ReadLines(filePath, Encoding.UTF8).ToList();
                // Get the number and text from each line by using regular expression
                var numWords = new Dictionary<int, string>();
                foreach (var numWord in lines)
                {
                    int number = Convert.ToInt32(Regex.Replace(numWord, "[^0-9]", ""));
                    string word = Regex.Replace(numWord, "[^a-zA-Z]", "");
                    numWords.Add(number, word);
                }
                // Get the numbers sorted.
                var numbers = numWords.Keys.ToList();
                numbers.Sort();

                int step = 1;
                var answer = new List<string>();
                while (numbers.Count > 0)
                {
                    if (numbers.Count >= step)
                    {
                        // Get the number at the end of each pyramid line, then get word in numWords
                        var lastNumber = numbers[step - 1];
                        answer.Add(numWords[lastNumber]);
                        numbers.RemoveRange(0, step);
                        step++;
                    }
                    else
                    {
                        break;
                    }
                }
                // List to string
                result = string.Join(" ", answer.ToArray());
            }
            return result;
        }
    }
}
