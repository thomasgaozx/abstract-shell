using System;
using System.Linq;
using System.Text;

namespace AbstractShell
{
    public static class ConsoleFormat
    {
        public static string LineBreak
        {
            get
            {
                StringBuilder builder = new StringBuilder(Console.WindowWidth);
                builder.Append('=',Console.WindowWidth);
                return builder.ToString();
            }
        }

        public static void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }

        /// <summary>
        /// Cite:
        /// https://stackoverflow.com/questions/20534318/make-console-writeline-wrap-words-instead-of-letters#answer-33508914
        /// </summary>
        /// <param name="paragraph">a paragraph to word wrap</param>
        /// <param name="print"></param>
        public static void WriteLineWordWrap(string paragraph, Action<string> print)
        {
            if (paragraph==null)
                return;

            string[] lines = paragraph.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            for (int i = 0; i < lines.Length; ++i)
            {
                string process = lines[i];

                while (process.Length > Console.WindowWidth)
                {
                    int wrapAt = process.LastIndexOf(' ', Math.Min(Console.WindowWidth - 1, process.Length));
                    if (wrapAt <= 0) break;

                    print(process.Substring(0, wrapAt));
                    process = process.Remove(0, wrapAt + 1);
                }

                print(process);
            }
        }

        public static string FillCharToRight(string s, int minLength, char filler = ' ')
        {
            string ns = s;
            int spare = minLength - s.Length;
            if (spare > 0)
                ns += string.Concat(Enumerable.Repeat(filler, 5));
            return ns;
        }

        private static void DefaultColour()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void SetColourAndPrompt(ConsoleColor color, string content)
        {
            Console.ForegroundColor = color;
            WriteLineWordWrap(content, Console.Write);
            DefaultColour();
        }

        private static void SetColourAndPrint(ConsoleColor color, string content)
        {
            Console.ForegroundColor = color;
            WriteLineWordWrap(content, Console.WriteLine);
            DefaultColour();
        }

        /// <summary>
        /// Write message to console <see cref="Console.WriteLine"/> (default color - green). 
        /// </summary>
        public static void Print(string content)
        {
            SetColourAndPrint(ConsoleColor.Green, content);
        }

        public static void Prompt(string content)
        {
            SetColourAndPrompt(ConsoleColor.Green, content);
        }

        /// <summary>
        /// Write a line of message to the console in red.
        /// </summary>
        /// <param name="content"></param>
        public static void PrintDanger(string content)
        {
            SetColourAndPrint(ConsoleColor.Red, content);
        }

        public static void PromptDanger(string content)
        {
            SetColourAndPrompt(ConsoleColor.Red, content);
        }

        /// <summary>
        /// Write a line of message to console in dark yellow.
        /// </summary>
        /// <param name="content"></param>
        public static void PrintWarning(string content)
        {
            SetColourAndPrint(ConsoleColor.Yellow, content);
        }

        public static void PromptWarning(string content)
        {
            SetColourAndPrompt(ConsoleColor.Yellow, content);
        }

        /// <summary>
        /// Write a line of message to console in bright blue.
        /// </summary>
        /// <param name="content"></param>
        public static void PrintBright(string content)
        {
            SetColourAndPrint(ConsoleColor.Cyan, content);
        }

        public static void PromptBright(string content)
        {
            SetColourAndPrompt(ConsoleColor.Cyan, content);
        }



    }
}
