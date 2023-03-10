using System.Data;

namespace MiniSql
{
    public static class Helper
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

        internal static string[] GetAllPersons(bool showAllProjects = false)
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
                bool moreProjects = false;
                int projectCount = 0;

                // Loop through each projectPerson associated with this person
                foreach (var projectPerson in projectPersons.Where(pp => pp.person_id == person.id))
                {
                    // Find the project associated with this projectPerson
                    var project = projects.FirstOrDefault(p => p.id == projectPerson.project_id);

                    if (project != null)
                    {
                        // Extract the project name from the project and include it in the output string
                        if (projectCount < 3 || showAllProjects)
                        {
                            personString += $"- {project.project_name}: {projectPerson.hours} hours\n";
                            hasProjects = true;
                        }
                        else
                        {
                            moreProjects = true;
                        }

                        projectCount++;
                    }
                }

                if (!hasProjects)
                {
                    personString += "This Person has no projects\n";
                }
                else if (moreProjects)
                {
                    personString += "And more...\n";
                }

                // Add everything we concatenated in personString to our results List now
                results.Add(personString);
            }

            // return our List as an array so we can use it in our menus
            return results.ToArray();
        }

        internal static string[] GetAllProjectsOnPerson(int personId)
        {
            // Load the models from DB
            var projects = PostgresDataAccess.LoadProjectModel();
            var projectPersons = PostgresDataAccess.LoadProjectPersonModel();

            List<string> results = new List<string>();

            // Loop through each projectPerson associated with this person
            foreach (var projectPerson in projectPersons.Where(pp => pp.person_id == personId))
            {
                string projectString = "";
                bool hasProjects = false;

                // Find the project associated with this projectPerson
                var project = projects.FirstOrDefault(p => p.id == projectPerson.project_id);

                if (project != null)
                {
                    // Extract the project name from the project and include it in the output string
                    projectString += $"- {project.project_name}: {projectPerson.hours} hours\n";
                    hasProjects = true;
                }

                if (!hasProjects)
                {
                    projectString += "This Person has no projects\n";
                }

                // Add everything we concatenated in personString to our results List now
                results.Add(projectString);
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
        internal static string FirstCharToUpper(this string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input[0].ToString().ToUpper() + input.Substring(1);
            }
        }
    }
}
