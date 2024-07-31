using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestTask.DataAccessLayer.Database;
using TestTask.DataAccessLayer.Interfaces;

namespace TestTask.DataAccessLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        public static void CreateDatabase(IConfiguration configuration)
        {
            SqlConnection context = new DbContext(configuration).connection;

            string dbCreateScript = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "DBCreateScript", "DBCreate.sql"));

            SqlCommand command = new SqlCommand(dbCreateScript, context);

            command.ExecuteNonQuery();
        }
    }
}
