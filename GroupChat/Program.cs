using static System.Net.Mime.MediaTypeNames;

namespace GroupChat
{
    class Program
    {
        static void Main(string[] args)
        {
            (int Left, int Top) curpos = (Console.CursorLeft, Console.CursorTop);
            /*for (int i = 0; i < 10; i++)
            {
                if (i > 0)
                {
                    Console.SetCursorPosition(Math.Min(curpos.Left, Console.BufferWidth - 1), curpos.Top);
                }
                curpos = MessageConsole.WipeWrite("\nWow, World! {0}", i);
                MessageConsole.DrawBox("Hello World!");
                Thread.Sleep(1000);
            }*/
            
        }
    }
}
