using Dapper;
using MiniSql.Models;
using Npgsql;
using System.Configuration;
using System.Data;

public class PostgresDataAccess
{
    private static string LoadConnectionString(string id = "monsters")
    {
        return ConfigurationManager.ConnectionStrings[id].ConnectionString;
    }

    internal static List<PersonModel> LoadPersonModel()
    {
        using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
        {
            var output = cnn.Query<PersonModel>("SELECT * FROM ika_person", new DynamicParameters());
            return output.ToList();
        }
    }

    internal static List<ProjectModel> LoadProjectModel()
    {
        using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
        {
            var output = cnn.Query<ProjectModel>("SELECT * FROM ika_project", new DynamicParameters());
            return output.ToList();
        }
    }

    internal static List<ProjectPersonModel> LoadProjectPersonModel()
    {
        using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
        {
            var output = cnn.Query<ProjectPersonModel>("SELECT * FROM ika_project_person", new DynamicParameters());
            return output.ToList();
        }
    }

    internal static void CreatePersonModel(string person_name)
    {
        using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
        {
            try
            {
                var output = cnn.Query($@"
                INSERT INTO ika_person (person_name)
                VALUES ('{person_name}')", new DynamicParameters());
            }
            catch (Npgsql.PostgresException e)
            {
                Console.WriteLine(e.MessageText);
            }
        }
    }

    internal static void CreateProjectModel(string project_name)
    {
        using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
        {
            try
            {
                var output = cnn.Query($@"
                INSERT INTO ika_project (project_name)
                VALUES ('{project_name}')", new DynamicParameters());
            }
            catch (Npgsql.PostgresException e)
            {
                Console.WriteLine(e.MessageText);
            }
        }
    }

    internal static void DeletePersonModel(int person_id)
    {
        using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
        {
            try
            {
                var output = cnn.Query($@"
                DELETE FROM ika_person
                WHERE(ika_person.id = {person_id}", new DynamicParameters());
            }
            catch (Npgsql.PostgresException e)
            {
                Console.WriteLine(e.MessageText);
            }
        }
    }
}
