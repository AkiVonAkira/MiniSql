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
        Menu mainMenu = new Menu(new string[]
        {
            "Show All Persons",
            "Edit Person",
            "Create Person",
            "Create Project",
            "Exit",
            "Dev Shortcut"
        });
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

        string[] personArray = Helper.GetAllPersons();
        int personIndex = Helper.MenuIndexer(personArray, "Select a Person to Modify", true);
        if (personIndex == personArray.Length) { MainMenu(); }
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
        var projects = PostgresDataAccess.LoadProjectModel();
        var projectPersons = PostgresDataAccess.LoadProjectPersonModel();
        string[] projectsArray = projects.Select(project => project.project_name).ToArray();
        int projectIndex = Helper.MenuIndexer(projectsArray, "Select a project");
        int selectedProject_id = projects[projectIndex].id;

        Menu hourMenu = new Menu(new string[] { "Hours", "Back" });
        int selectedIndex = hourMenu.DisplayMenu("How many hours have you worked on the project?");
        int hours = hourMenu.WriteInMenuNumbers();

        PostgresDataAccess.CreateProjectPersonModel(selectedProject_id, SelectedPersonID, hours);

        Helper.ResetCursor();
        Console.WriteLine($"Assigned {projectsArray[projectIndex]} with {hours} hours.");

        Helper.EnterToContinue();
    }

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
