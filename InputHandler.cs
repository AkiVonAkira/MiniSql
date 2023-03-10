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

    public int ReadNumberInput()
    {
        ConsoleKeyInfo key;
        string userInput = "";

        do
        {
            key = Console.ReadKey(true);

            if (char.IsDigit(key.KeyChar))
            {
                userInput += key.KeyChar;
                Console.Write(key.KeyChar);
            }
            else if (key.Key == ConsoleKey.Backspace && userInput.Length > 0)
            {
                userInput = userInput.Substring(0, userInput.Length - 1);
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        Console.WriteLine();

        if (string.IsNullOrEmpty(userInput))
        {
            Console.WriteLine("Error: No input detected. Please enter a number.");
            return ReadNumberInput();
        }

        if (!int.TryParse(userInput, out int result))
        {
            Console.WriteLine("Error: Invalid input. Please enter a valid number.");
            return ReadNumberInput();
        }

        return result;
    }
}
