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

        //internal static string[] GetAllPersons()
        //{
        //    // Load the models from DB
        //    var persons = PostgresDataAccess.LoadPersonModel();
        //    var projects = PostgresDataAccess.LoadProjectModel();
        //    var projectPersons = PostgresDataAccess.LoadProjectPersonModel();

        //    List<string> results = new List<string>();

        //    // Loop through each person
        //    foreach (var person in persons)
        //    {
        //        string personString = $"{person.person_name}\n";

        //        bool hasProjects = false;

        //        // Loop through each projectPerson
        //        foreach (var projectPerson in projectPersons)
        //        {
        //            // Check if the projectPerson has the same id as the same person in this loop
        //            if (projectPerson.person_id == person.id)
        //            {
        //                // Now find the project that is on the person
        //                var project = projects.Find(proj => proj.id == projectPerson.project_id);
        //                if (project != null)
        //                {
        //                    personString += $"{project.project_name}: {projectPerson.hours} hours\n";
        //                    hasProjects = true;
        //                }
        //            }
        //        }

        //        if (!hasProjects)
        //        {
        //            personString += "This Person has no projects\n";
        //        }
        //        // Add everything we concatenated in personString to our resultS List now
        //        results.Add(personString);
        //    }
        //    // return our List as an array so we can use it in our menus
        //    return results.ToArray();
        //}
        internal static string[] GetAllPersons()
        {
            // Load the models from DB
            var persons = PostgresDataAccess.LoadPersonModel();
            var projects = PostgresDataAccess.LoadProjectModel();
            var projectPersons = PostgresDataAccess.LoadProjectPersonModel();

            List<string> results = new List<string>();

            // Loop through each person
            foreach (var person in persons)
            {
                string personString = $"{person.person_name}\n";

                bool hasProjects = false;

                // Loop through each projectPerson associated with this person
                foreach (var projectPerson in projectPersons.Where(pp => pp.person_id == person.id))
                {
                    // Find the project associated with this projectPerson
                    var project = projects.FirstOrDefault(p => p.id == projectPerson.project_id);

                    if (project != null)
                    {
                        // Extract the project name from the project and include it in the output string
                        personString += $"{project.project_name}: {projectPerson.hours} hours\n";
                        hasProjects = true;
                    }
                }

                if (!hasProjects)
                {
                    personString += "This Person has no projects\n";
                }

                // Add everything we concatenated in personString to our results List now
                results.Add(personString);
            }

            // return our List as an array so we can use it in our menus
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
