using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupChat
{
    internal class MessageConsole
    {

        public static (int Top, int Left) GetCursorPosition(string text, int cursorPosition)
        {
            // Normalize line endings to \n and split the input into lines
            List<string> lines = text.Replace(Environment.NewLine, "\n").Split('\n').ToList();
            //normalize cursor position to be within the bounds of the text
            cursorPosition = Math.Max(0, Math.Min(cursorPosition, text.Length));
            // Calculate the row and column of the cursor position
            int row = 0;
            int col = 0;
            int lineLengthSum = 0;
            foreach (string line in lines)
            {
                lineLengthSum += line.Length + 1; // Account for the newline character
                if (cursorPosition < lineLengthSum)
                {
                    row++;
                    col = cursorPosition - (lineLengthSum - line.Length);
                    if (col > line.Length) // Account for the newline character
                    {
                        col = line.Length;
                    }
                    break;
                }
                row++;
            }

            // Determine the maximum width of the box
            int maxWidth = lines.Max(line => line.Length) + 2;

            // Determine the maximum height of the box
            int maxHeight = lines.Count + 2;

            // Calculate the relative row and column of the cursor position
            int relativeRow = row;
            int relativeCol = 2 + col;
            if (relativeRow > maxHeight)
            {
                relativeRow = maxHeight;
            }
            if (relativeCol > maxWidth)
            {
                relativeCol = maxWidth;
            }

            return (Top: relativeRow, Left: relativeCol);
        }
        public static void DrawBox(string text, params object?[] args)
        {
            DrawBox(string.Format(text, args: args));
        }

        public static void DrawBox(string input)
        {
            // Get the width and height of the console
            int consoleWidth = Console.WindowWidth;

            // Define the box width and height
            int boxWidth = consoleWidth;
            int boxHeight = 2;

            // Normalize the input string by replacing Environment.NewLine with \n
            input = input.Replace(Environment.NewLine, "\n");

            // Split the input string into lines and trim each line if necessary
            string[] lines = input.Split(new[] { '\n' }, StringSplitOptions.None);
            for (int i = 0; i < lines.Length && i < boxHeight; i++)
            {
                if (lines[i].Length > (boxWidth - 4))
                {
                    lines[i] = lines[i].Substring(0, boxWidth - 4);
                }
            }

            // Draw the box
            Console.WriteLine(new string('-', boxWidth));
            for (int i = 0; i < 2; i++)
            {
                if (i < lines.Length)
                {
                    Console.Write("|");
                    Console.Write(lines[i].PadRight(boxWidth - 3));
                    Console.Write("|");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine(new string(' ', boxWidth));
                }
            }
            Console.WriteLine(new string('-', boxWidth));
        }

        public static (int Left, int Top) WipeWrite(string text, params object?[] args)
        {
            return WipeWrite(string.Format(text, args: args));
        }

        public static (int Left, int Top) WipeWrite(string text)
        {
            // Normalize line endings to \n and split the input into lines
            List<string> lines = text.Replace(Environment.NewLine, "\n").Split('\n').ToList();
            (int Left, int Top) curpos = (Console.CursorTop, Console.CursorLeft);
            // if a line is longer than the console width, split it into multiple lines, insert them into the list and remove the original line
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Length > Console.WindowWidth)
                {
                    List<string> splitLines = new List<string>();
                    while (lines[i].Length > Console.WindowWidth)
                    {
                        splitLines.Add(lines[i].Substring(0, Console.WindowWidth));
                        lines[i] = lines[i].Substring(Console.WindowWidth);
                    }
                    splitLines.Add(lines[i]);
                    lines.RemoveAt(i);
                    lines.InsertRange(i, splitLines);
                }
            }
            // Write each line with padding and overwrite any previous output on each line
            for (int i = 0; i < lines.Count(); i++)
            {
                // Calculate the number of characters that will fit on the current line
                int consoleWidth = Console.WindowWidth;
                int availableWidth = consoleWidth - Console.CursorLeft;
                int remainingWidth = Math.Max(availableWidth, 0);
                curpos.Left = availableWidth; curpos.Top = Console.CursorTop;
                // Pad the text with spaces to fit on the current line
                string paddedText = lines[i].PadRight(remainingWidth);

                // Write the padded text and move the cursor to the beginning of the next line
                Console.Write(paddedText);
                Console.WriteLine();
            }
            return curpos;
        }
    }
}
