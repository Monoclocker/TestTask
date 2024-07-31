using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.DataAccessLayer.Database
{
    internal class DbContext: IDisposable
    {
        public readonly SqlConnection connection;
        public DbContext(IConfiguration configuration) 
        {
            connection = new SqlConnection(configuration["connection"]);
            connection.Open();
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
