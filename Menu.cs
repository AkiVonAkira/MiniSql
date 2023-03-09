namespace MiniSql
{
    internal class Menu
    {
        private readonly string[] _menuItems;
        private int _selectedIndex;
        private List<string> _menuResultText = new List<string>();
        private string _header;

        private int _cursorTop;
        private int _cursorLeft;

        private InputHandler _inputHandler;

        public Menu(string[] menuItems)
        {
            _menuItems = menuItems;
            _selectedIndex = 0;

            // Initialize an input handler for the menu
            _inputHandler = new InputHandler();
        }

        public int DisplayMenu(string header = "")
        {
            ConsoleKey key;
            do
            {
                Console.Clear();

                if (!string.IsNullOrWhiteSpace(header))
                {
                    Console.WriteLine($"{header}\n");
                    _header = header;
                }
                else
                {
                    Console.WriteLine($"{_header}\n");
                }

                // Initialize cursor position
                _cursorTop = Console.CursorTop;
                _cursorLeft = Console.CursorLeft;
                for (int i = 0; i < _menuItems.Length; i++)
                {
                    Console.ForegroundColor = i == _selectedIndex ? ConsoleColor.Green : ConsoleColor.White;

                    int menuItemLines = 1;
                    if (_menuResultText.Count > i && _menuResultText[i] != null)
                    {
                        menuItemLines = _menuResultText[i].Split('\n').Length;
                    }

                    Console.Write($"{(i == _selectedIndex ? "➤ " : " ")}{_menuItems[i]}");
                    // Check if there's any text added to the current menu item and write it
                    if (_menuResultText.Count > i && _menuResultText[i] != null)
                    {
                        Console.Write($" : {_menuResultText[i]}");
                    }
                    Console.WriteLine();

                    // Update the cursor position to be on the same line as the current menu item
                    Console.CursorTop -= menuItemLines - 1;
                }
                Console.ResetColor();
                MoveCursorRight();

                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        MoveIndexUp();
                        break;
                    case ConsoleKey.DownArrow:
                        MoveIndexDown();
                        break;
                    case ConsoleKey.Enter:
                        return _selectedIndex;
                }
            } while (true);
        }

        private void MoveIndexUp()
        {
            _selectedIndex--;
            _selectedIndex = (_selectedIndex % _menuItems.Length + _menuItems.Length) % _menuItems.Length;
        }

        private void MoveIndexDown()
        {
            _selectedIndex++;
            _selectedIndex = (_selectedIndex % _menuItems.Length + _menuItems.Length) % _menuItems.Length;
        }

        public void MoveCursorRight()
        {
            int menuItemLength = _menuItems[_selectedIndex].Length + 2;

            if (_menuItems[_selectedIndex].Contains("\n"))
            {
                string[] lines = _menuItems[_selectedIndex].Split('\n');
                menuItemLength = lines[0].Length + 2;
            }
            else
            {
                for (int i = 0; i < _menuResultText.Count; i++)
                {
                    if (_selectedIndex == i)
                    {
                        menuItemLength += _menuResultText[i].Length + 2;
                        break;
                    }
                }
            }

            int totalLines = 0;
            for (int i = 0; i < _menuItems.Length; i++)
            {
                if (i == _selectedIndex) break;
                if (_menuItems[i].Contains("\n"))
                {
                    totalLines += _menuItems[i].Split('\n').Length;
                }
                else
                {
                    totalLines++;
                }
                // add the number of lines of the previous menu item if it had multiple lines
                if (totalLines % Console.WindowHeight == 0)
                {
                    if (_menuItems[i - 1].Contains("\n"))
                    {
                        totalLines += _menuItems[i - 1].Split('\n').Length - 1;
                    }
                    else
                    {
                        totalLines++;
                    }
                }
            }

            Console.CursorLeft = _cursorLeft + menuItemLength;
            Console.CursorTop = (_cursorTop + totalLines) % Console.WindowHeight;
            //Console.WriteLine($"_selectedIndex: {_selectedIndex}, totalLines: {totalLines}, CURSOR: {Console.CursorTop}");
        }

        public void MoveCursorLeft()
        {
            int newCursorPosition = Math.Max(Console.CursorLeft - 1, 0);
            Console.SetCursorPosition(newCursorPosition, Console.CursorTop);
        }

        internal string WriteInMenu()
        {
            //MoveCursorRight();
            Console.Write(" ↪ ");
            _inputHandler = new InputHandler();
            string userInput = (string)_inputHandler.ReadInput();
            AddMenuResults($"{userInput}", _selectedIndex);
            _selectedIndex++;
            MoveCursorRight();
            return userInput;
        }

        // Change a item in the menu array at given index
        internal void AddMenuResults(string item, int index)
        {
            // If the item is being set for the first time, add it to the text list
            if (index >= _menuResultText.Count)
            {
                for (int i = _menuResultText.Count; i <= index; i++)
                {
                    _menuResultText.Add(null);
                }
            }
            // Set the item at the given index
            _menuResultText[index] = item;
        }
    }
}
