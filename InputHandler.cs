internal class InputHandler
{
    public object ReadInput()
    {
        ConsoleKeyInfo key;
        string userInput = "";

        do
        {
            key = Console.ReadKey(true);

            // If the key is not a backspace or enter
            if (key.Key == ConsoleKey.Backspace)
            {
                // If the key is a backspace, remove the last character from the string
                if (key.Key == ConsoleKey.Backspace && userInput.Length > 0)
                {
                    userInput = userInput.Substring(0, userInput.Length - 1);
                    Console.Write("\b \b");
                }
            }
            else
            {
                userInput += key.KeyChar;
                Console.Write(key.KeyChar);
                //Console.Write("•");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        Console.WriteLine();

        return userInput;
    }
}
