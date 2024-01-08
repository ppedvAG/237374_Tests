using Microsoft.Data.SqlClient;

namespace TddBank
{
    public class NorthwindManager
    {
        public int CountEmployees()
        {
            var conString = "Server=.;Database=Northwind;Trusted_Connection=true;TrustServerCertificate=True";
            conString = "Server=(localdb)\\mssqllocaldb;Database=Northwnd;Trusted_Connection=true;TrustServerCertificate=True";

            using var con = new SqlConnection(conString);
            con.Open();
            using var cmd = con.CreateCommand();
            cmd.CommandText = "WAITFOR DELAY '00:00:03';SELECT COUNT(*) FROM Employees";
            var count = cmd.ExecuteScalar();

            return (int)count;
        }
    }
}
