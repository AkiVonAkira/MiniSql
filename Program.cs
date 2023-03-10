using System.Data;

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
                    AssignProjects();
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
        Menu personMenu = new Menu(new string[] { "Assign Project", "Edit Hours", "Edit Name", "Back" });
        int selectedIndex = personMenu.DisplayMenu("Please select an option");

        bool showMenu = true;
        while (showMenu)
        {
            switch (selectedIndex)
            {
                case 0:
                    AssignProjects();
                    selectedIndex = personMenu.DisplayMenu();
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    MainMenu();
                    break;
            }
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
        int selectedIndex = hourMenu.DisplayMenu("How many hours have you worked on the project?");
        // This allows us to write only numbers in the menu alternative
        int hours = hourMenu.WriteInMenuNumbers();

        // create a row in the db with the info we collected above
        PostgresDataAccess.CreateProjectPersonModel(selectedProject_id, SelectedPersonID, hours);

        Helper.ResetCursor();
        // TODO: Fix weird writing here where it cuts off certain words
        // and doesnt show the project name
        Console.WriteLine($"Assigned {projectArray[projectIndex]} with {hours} hours.");

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
}
