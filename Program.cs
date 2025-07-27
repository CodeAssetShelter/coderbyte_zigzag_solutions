using System.Collections.Generic;
using System.Diagnostics;

namespace Zigzag
{
    internal class Program
    {
        // "REAL" one cycle is before next floor's index.
        // If rows is 4. one cycle is 0,1,2,3,2,1.
        // middle floor has two char.
        // two char are more higher, more closer (2 * i)

        static void Main(string[] args)
        {
            Console.WriteLine(Quiz(new string[] { "cat", "5" }));
            Console.WriteLine(Quiz(new string[] { "kaamvjjfl", "4" }));
            Console.WriteLine(Quiz(new[] { "abcdef", "2" }));

            Console.WriteLine(Quiz_Two(new string[] { "cat", "5" }));
            Console.WriteLine(Quiz_Two(new string[] { "kaamvjjfl", "4" }));
            Console.WriteLine(Quiz_Two(new[] { "abcdef", "2" }));
        }

        // Use Dictionary, Queue
        // If last char is '_', change to '\0';
        static string Quiz(string[] strArr)
        {
            if (strArr == null || strArr.Length < 2)
                throw new ArgumentException("strArr is must be [word, rows]");

            string str = strArr[0] ?? string.Empty;

            if (!int.TryParse(strArr[1], out int rows))
                throw new ArgumentException("rows is must be integer type.");

            const string TOKEN = "zpaow189za";

            if (rows <= 1 || rows >= str.Length)
            {
                var arr0 = (str + TOKEN).ToCharArray();
                for (int i = 3; i < arr0.Length; i += 4) arr0[i] = '_';
                return new string(arr0);
            }

            var zigzag = new Dictionary<int, Queue<char>>(rows);
            for (int i = 0; i < rows; i++)
                zigzag[i] = new Queue<char>();

            int floor = 0; 
            int move = 1;

            foreach (char ch in str)
            {
                zigzag[floor].Enqueue(ch);

                if (floor == 0) move = 1;
                else if (floor == rows - 1) move = -1;

                floor += move;
            }

            string res = string.Empty;
            for (int i = 0; i < rows; i++)
            {
                var q = zigzag[i];
                if (q.Count > 0)
                    res += new string(q.ToArray());
            }

            res += TOKEN;
            var outChars = res.ToCharArray();
            for (int i = 3; i < outChars.Length; i += 4)
            {
                if (i == outChars.Length - 1)
                    outChars[i] = '\0';
                else
                    outChars[i] = '_';
            }

            return new string(outChars);
        }

        // Use Array idx
        // If last char is '_', not change.;
        public static string Quiz_Two(string[] strArr)
        {
            string word = strArr[0];
            int rows = int.Parse(strArr[1]);

            string zigzag;
            if (rows <= 1 || rows >= word.Length)
            {
                zigzag = word;
            }
            else
            {
                int cycle = 2 * rows - 2;
                string sb = "";

                for (int i = 0; i < rows; i++)
                {
                    for (int j = i; j < word.Length; j += cycle)
                    {
                        sb += word[j];
                        int k = j + cycle - 2 * i;
                        if (i != 0 && i != rows - 1 && k < word.Length)
                        {
                            sb += word[k];
                        }
                    }
                }
                zigzag = sb;
            }

            const string TOKEN = "zpaow189za";
            char[] ch = (zigzag + TOKEN).ToCharArray();
            for (int i = 3; i < ch.Length; i += 4) ch[i] = '_';

            return new string(ch);
        }
    }
}
