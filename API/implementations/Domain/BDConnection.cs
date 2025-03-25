using Microsoft.Data.SqlClient;
using System;
using System.Data.Common;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace API.implementations.Domain
{
    public class BDConnection : IBDConnection
    {
        public string IP { get; set; }
        public string Pass { get; set; }
        public string User { get; set; }
        public string DBName { get; set; }
        public int Port { get; set; }

        public SqlConnection Connection;

        public BDConnection()
        {

        }
        public BDConnection(string ip, string pass, string user, string dbname, int port )
        {
            IP = ip;
            Pass = pass;
            User = user;
            DBName = dbname;
            Port = port;
        }

        public void Close()
        {
            Connection.Close();
        }

        public async void Connect()
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = $"{IP}:{Port}",
                UserID = User,
                Password = Pass,
                InitialCatalog = DBName
            };

            string connectionString = builder.ConnectionString;

            try
            {
                await using var connection = new SqlConnection(connectionString);
                Connection = connection;

                await connection.OpenAsync();
            }
            catch (SqlException e)
            {
                Console.WriteLine($"SQL Error: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
