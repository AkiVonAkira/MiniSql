namespace MiniSql
{
    internal class Creators
    {
        internal static void CreatePersonMenu()
        {
            Menu createPersonMenu = new Menu(new string[] { "Name", "Submit", "Back" });
            int selectedIndex = createPersonMenu.DisplayMenu("Please input your name");

            string name = "";

            bool showMenu = true;
            while (showMenu)
            {
                switch (selectedIndex)
                {
                    case 0:
                        name = createPersonMenu.WriteInMenu();
                        selectedIndex = createPersonMenu.DisplayMenu();
                        break;
                    case 1:
                        if (string.IsNullOrWhiteSpace(name))
                        {
                            Console.WriteLine("\n\nYou did not input a name!");
                        }
                        else
                        {
                            PostgresDataAccess.CreatePersonModel(name);
                            Console.WriteLine($"\nNew Person created with the name: {name}");
                        }
                        showMenu = false;
                        break;
                    case 2:
                        Program.MainMenu();
                        break;
                }
            }
            Helper.EnterToContinue();
        }

        internal static void CreateProjectMenu()
        {
            Menu createProjectMenu = new Menu(new string[] { "Name", "Submit", "Back" });
            int selectedIndex = createProjectMenu.DisplayMenu("Please input the project name");

            string name = "";

            bool showMenu = true;
            while (showMenu)
            {
                switch (selectedIndex)
                {
                    case 0:
                        name = createProjectMenu.WriteInMenu();
                        selectedIndex = createProjectMenu.DisplayMenu();
                        break;
                    case 1:
                        if (string.IsNullOrWhiteSpace(name))
                        {
                            Console.WriteLine("\n\nYou did not input a name!");
                        }
                        else
                        {
                            PostgresDataAccess.CreateProjectModel(name);
                            Console.WriteLine($"\nNew Project created with the name: {name}");
                        }
                        showMenu = false;
                        break;
                    case 2:
                        Program.MainMenu();
                        break;
                }
            }
            Helper.EnterToContinue();
        }

    }
}
