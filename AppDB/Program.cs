namespace AppDB
{
    /*
     * Scaffold database
     * dotnet ef dbcontext scaffold "Data Source=media_database.db;" Microsoft.EntityFrameworkCore.Sqlite --use-database-names -o Models 
     * Rename generated class "Medium" to "Media". ? why does this happen ?
     * Sql, json and csv files for initial database
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}