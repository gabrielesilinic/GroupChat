namespace GroupChat;

public class MessageBox
{
    private string message;
    private int messageCursor;
    private (int Top, int Left) cursorPosition;

    public MessageBox(string message, int messageCursor)
    {
        this.message = message;
        this.messageCursor = messageCursor;
        cursorPosition = MessageConsole.GetCursorPosition(message, messageCursor);
    }

    public void Clear()
    {
        message = "";
        messageCursor = 0;
        cursorPosition = MessageConsole.GetCursorPosition(message, messageCursor);
    }

    public void Update((int Top, int Left) offset)
    {
        string message = "Lorem ipsum\nDolor sit amet";
        int messagecursor = 4;
            Console.SetCursorPosition(0, 0);
            curpos = MessageConsole.GetCursorPosition(message, messagecursor);
            MessageConsole.DrawBox(message);
            Console.SetCursorPosition(curpos.Top, curpos.Left);
            var keyinfo = Console.ReadKey(true);
            switch (keyinfo.Key)
            {
                case ConsoleKey.LeftArrow:
                    messagecursor--;
                    break;
                case ConsoleKey.RightArrow:
                    //if the ctrl modifier is pressed then move to the end of the word
                    if (keyinfo.Modifiers == ConsoleModifiers.Control)
                    {
                        messagecursor = message.Replace('\n', ' ').IndexOf(' ', messagecursor) + 1;
                    }
                    else
                    {
                        messagecursor++;
                    }
                    break;
                default:
                    message = message.Insert(messagecursor, keyinfo.KeyChar.ToString());
                    messagecursor++;
                    break;
            }
            messagecursor = Math.Max(0, Math.Min(messagecursor, message.Length));
            Console.Title = messagecursor.ToString();
        
    }

    public string GetMessage()
    {
        return message;
    }

    public (int Top, int Left) GetCursorGlobalPosition((int Top, int Left) offset)
    {
        return (cursorPosition.Top + offset.Top, cursorPosition.Left + offset.Left);
    }
}
