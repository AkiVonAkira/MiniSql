namespace MiniSql;
class Program
{
    static void Main(string[] args)
    {
        MainMenu();
    }

    private static void MainMenu()
    {
        Menu mainMenu = new Menu(new string[] { "Show All Persons", "Create Person", "Create Project", "Exit" });
        int selectedIndex = mainMenu.DisplayMenu("Select an Option");
        bool showMenu = true;
        while (showMenu)
        {
            switch (selectedIndex)
            {
                case 0:
                    ShowAllPersons();
                    selectedIndex = mainMenu.DisplayMenu();
                    break;
                case 1:
                    CreatePersonMenu();
                    selectedIndex = mainMenu.DisplayMenu();
                    break;
                case 2:
                    CreateProjectMenu();
                    selectedIndex = mainMenu.DisplayMenu();
                    break;
                case 3:
                    Environment.ExitCode = 0;
                    showMenu = false;
                    break;
            }
        }
    }

    private static void CreatePersonMenu()
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
                        Console.WriteLine($"\nNew Person created with the name {name}");
                    }
                    showMenu = false;
                    break;
                case 2:
                    MainMenu();
                    break;
            }
        }
        Helper.EnterToContinue();
    }

    private static void CreateProjectMenu()
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
                        Console.WriteLine($"\nNew Project created with the name {name}");
                    }
                    showMenu = false;
                    break;
                case 2:
                    MainMenu();
                    break;
            }
        }
        Helper.EnterToContinue();
    }

    private static void ShowAllPersons()
    {
        Console.Clear();

        var persons = PostgresDataAccess.LoadPersonModel();
        var projects = PostgresDataAccess.LoadProjectModel();
        var projectPersons = PostgresDataAccess.LoadProjectPersonModel();

        string[] personArray = Helper.GetAllPersons();
        int personIndex = Helper.MenuIndexer(personArray, "Select a Person to Modify", true);
        if (personIndex == personArray.Length) { MainMenu(); }

        //foreach (var person in persons)
        //{
        //    Console.WriteLine($"\n{person.person_name}");

        //    foreach (var projectPerson in projectPersons)
        //    {
        //        if (projectPerson.person_id == person.id)
        //        {
        //            var project = projects.Find(p => p.id == projectPerson.project_id);

        //            if (project != null)
        //            {
        //                Console.WriteLine($"    {project.project_name}: {projectPerson.hours} hours");
        //            }
        //        }
        //    }
        //}

        Helper.EnterToContinue();
    }

}
