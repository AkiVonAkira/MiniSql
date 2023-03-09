namespace MiniSql
{
    internal class Helper
    {
        // This method returns the index of the selected menu item from an array of strings
        internal static int MenuIndexer(string[] array, string headerText = "", bool hasBack = false)
        {
            // If the 'hasBack' flag is set to true, add a "Go Back" option to the end of the menu
            if (hasBack)
            {
                Array.Resize(ref array, array.Length + 1);
                array[array.Length - 1] = "Go Back";
            }
            // Create a new instance of the 'Menu' class with the array of menu items
            Menu menu = new Menu(array);
            // Get the selected index using the 'UseMenu' method
            int index = menu.DisplayMenu(headerText);
            // Return the selected index
            return index;
        }

        internal static void EnterToContinue()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\n↪ Press Enter to Continue...");
            Console.ResetColor();

            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
            }
            while (key.Key != ConsoleKey.Enter);
        }

        internal static string[] GetAllPersons()
        {
            var persons = PostgresDataAccess.LoadPersonModel();
            var projects = PostgresDataAccess.LoadProjectModel();
            var projectPersons = PostgresDataAccess.LoadProjectPersonModel();

            List<string> results = new List<string>();

            foreach (var person in persons)
            {
                string personString = $"{person.person_name}\n";

                bool hasProjects = false;

                foreach (var projectPerson in projectPersons)
                {
                    if (projectPerson.person_id == person.id)
                    {
                        var project = projects.Find(proj => proj.id == projectPerson.project_id);
                        personString += $"{project.project_name}: ";
                        if (project != null)
                        {
                            personString += $"{project.project_name}: {projectPerson.hours} hours\n";
                            hasProjects = true;
                        }
                    }
                }

                if (!hasProjects)
                {
                    personString += "This Person has no projects\n";
                }

                results.Add(personString);
            }

            return results.ToArray();
        }

        internal static void ResetCursor()
        {
            Console.Clear();
            Console.CursorLeft = 0;
            Console.CursorTop = 0;
        }
    }
}
