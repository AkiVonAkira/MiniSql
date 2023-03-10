namespace MiniSql;
class Program
{
    internal static int SelectedPersonID { get; set; }
    static void Main(string[] args)
    {
        MainMenu();
    }

    internal static void MainMenu()
    {
        // Initialize Menu object with its options
        Menu mainMenu = new Menu(new string[]
        {
            "Show All Persons",
            "Edit Person",
            "Create Person",
            "Create Project",
            "Exit",
            "Dev Shortcut"
        });
        // Display the menu and return the selected menu item index
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
                    EditPersons();
                    selectedIndex = mainMenu.DisplayMenu();
                    break;
                case 2:
                    Creators.CreatePersonMenu();
                    selectedIndex = mainMenu.DisplayMenu();
                    break;
                case 3:
                    Creators.CreateProjectMenu();
                    selectedIndex = mainMenu.DisplayMenu();
                    break;
                case 4:
                    Environment.ExitCode = 0;
                    showMenu = false;
                    break;
                case 5:
                    SelectedPersonID = 1;
                    //TrimModels();
                    //AssignProjects();
                    UpdateHours();
                    selectedIndex = mainMenu.DisplayMenu();
                    break;
            }
        }
    }

    private static void EditPersons()
    {
        var persons = PostgresDataAccess.LoadPersonModel();

        // Initialize string array with formatted information about the person and their projects
        string[] personArray = Helper.GetAllPersons();

        // Create a menu with all persons and return the selcted person
        int personIndex = Helper.MenuIndexer(personArray, "Select a Person to Modify", true);
        // if the user presses back, actually go back
        if (personIndex == personArray.Length) { MainMenu(); }

        // get the database id of the person from our selcted index
        int selectedPerson_id = persons[personIndex].id;
        SelectedPersonID = selectedPerson_id;

        PersonMenu();
    }

    internal static void PersonMenu()
    {
        Menu personMenu = new Menu(new string[] { "Show All Projects", "Assign Project", "Edit Hours", "Edit Name", "Back" });
        int selectedIndex = personMenu.DisplayMenu("Please select an option");

        bool showMenu = true;
        while (showMenu)
        {
            switch (selectedIndex)
            {
                case 0:
                    ShowAllProjects();
                    selectedIndex = personMenu.DisplayMenu();
                    break;
                case 1:
                    AssignProjects();
                    selectedIndex = personMenu.DisplayMenu();
                    break;
                case 2:
                    //UpdateHours();
                    selectedIndex = personMenu.DisplayMenu();
                    break;
                case 3:
                    break;
                case 4:
                    MainMenu();
                    break;
            }
        }
        Helper.EnterToContinue();
    }

    private static void UpdateHours()
    {
        // load the person from the database with our selected person id and get project array
        var person = PostgresDataAccess.LoadPersonModel().FirstOrDefault(p => p.id == SelectedPersonID);
        var projects = PostgresDataAccess.LoadProjectModel();
        var projectPersons = PostgresDataAccess.LoadProjectPersonModel();
        string[] projectPersonArray = Helper.GetAllProjectsOnPerson(SelectedPersonID);

        // Create a menu with all projects an return the selected project
        int projectIndex = Helper.MenuIndexer(projectPersonArray, "Select a project", true);
        if (projectIndex == projectPersonArray.Length) { PersonMenu(); }

        Console.WriteLine($"Selected Person ID: {SelectedPersonID}");

        Console.WriteLine("\nPerson:");
        Console.WriteLine($"ID: {person.id}");
        Console.WriteLine($"First Name: {person.person_name}");

        Console.WriteLine("\nProject Persons:");
        foreach (var pp in projectPersons)
        {
            Console.WriteLine($"Project Person ID: {pp.id}");
            Console.WriteLine($"Project ID: {pp.project_id}");
            Console.WriteLine($"Person ID: {pp.person_id}");
            Console.WriteLine($"Hours: {pp.hours}");
            Console.WriteLine();
        }

        Console.WriteLine("\nProjects on Person:");
        foreach (var projectName in projectPersonArray)
        {
            Console.WriteLine(projectName);
        }


        Helper.EnterToContinue();
    }

    private static void AssignProjects()
    {
        // Load the models from the DB
        var projects = PostgresDataAccess.LoadProjectModel();
        var projectPersons = PostgresDataAccess.LoadProjectPersonModel();

        // Get a string array of all the projects from the model
        string[] projectArray = projects.Select(project => project.project_name).ToArray();

        // Create a menu with all projects an return the selected project
        int projectIndex = Helper.MenuIndexer(projectArray, "Select a project", true);
        if (projectIndex == projectArray.Length) { PersonMenu(); }

        int selectedProject_id = projects[projectIndex].id;

        // Create a new menu so we can write how many hours
        Menu hourMenu = new Menu(new string[] { "Hours", "Back" });
        int hourIndex = hourMenu.DisplayMenu("How many hours have you worked on the project?");
        if (hourIndex == 1) { PersonMenu(); }
        // This allows us to write only numbers in the menu alternative
        int hours = hourMenu.WriteInMenuNumbers();

        // create a row in the db with the info we collected above
        bool success = PostgresDataAccess.CreateProjectPersonModel(selectedProject_id, SelectedPersonID, hours);
        if (success)
        {
            Console.WriteLine($"Assigned {projects[projectIndex].project_name} with {hours.ToString()} hours.");
        }
        else
        {
            Console.WriteLine("Could not assign project to person. Try again later");
        }

        Helper.EnterToContinue();
    }

    // Simple method to just show all persons
    private static void ShowAllPersons()
    {
        Console.Clear();

        string[] personArray = Helper.GetAllPersons();

        foreach (var person in personArray)
        {
            Console.WriteLine(person);
        }

        Helper.EnterToContinue();
    }

    // Simple method to just show all projects
    private static void ShowAllProjects()
    {
        Console.Clear();

        // load the person from the database with our selected person id and get project array
        var person = PostgresDataAccess.LoadPersonModel().FirstOrDefault(p => p.id == SelectedPersonID);
        string[] projectPersonArray = Helper.GetAllProjectsOnPerson(SelectedPersonID);

        // print the persons name and all of their projects
        if (person != null)
            Console.WriteLine($"Projects for {person.person_name}:\n");
        foreach (var project in projectPersonArray)
        {
            Console.WriteLine(project);
        }

        Helper.EnterToContinue();
    }

    internal static void TrimModels()
    {
        var persons = PostgresDataAccess.LoadPersonModel();
        foreach (var person in persons)
        {
            person.person_name = person.person_name.Trim();
        }
        PostgresDataAccess.PersonModelTrim(persons);

        var projects = PostgresDataAccess.LoadProjectModel();
        foreach (var project in projects)
        {
            project.project_name = project.project_name.Trim();
        }
        PostgresDataAccess.ProjectModelTrim(projects);
    }
}
