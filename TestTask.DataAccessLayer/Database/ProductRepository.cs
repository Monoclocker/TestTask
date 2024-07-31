using Microsoft.Data.SqlClient;
using TestTask.DataAccessLayer.Entities;
using TestTask.DataAccessLayer.Interfaces;

namespace TestTask.DataAccessLayer.Database
{
    internal class ProductRepository : IRepository<Product>
    {
        readonly SqlConnection connection;
        SqlCommand? command;
        SqlTransaction? transaction;

        public ProductRepository(DbContext context)
        {
            this.connection = context.connection;
        }
        public Product? Get(int id)
        {
            command = new SqlCommand(
                    "SELECT * FROM dbo.products WHERE dbo.products.Id = @id", connection);

            SqlParameter idParameter = new SqlParameter("@id", id);

            command.Parameters.Add(idParameter);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Product()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        CategoryId = reader.GetInt32(2),
                        Price = reader.GetInt32(3)
                    };
                }
                return null;
            }
        }
        public IEnumerable<Product> GetAll()
        {
            command = new SqlCommand(
                "SELECT * FROM dbo.products", connection);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read()) 
                {
                    yield return new Product()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        CategoryId = reader.GetInt32(2),
                        Price = reader.GetInt32(3)
                    };
                }
            }
        }
        public void Create(Product entity)
        {
            transaction = connection.BeginTransaction();

            command = new SqlCommand(
                "INSERT INTO dbo.products(Name, CategoryId, Price)" +
                "VALUES (@name, @categoryId, @price)", connection);

            SqlParameter nameParameter = new SqlParameter("@name", entity.Name);
            SqlParameter categoryIdParameter = new SqlParameter("@categoryId", entity.CategoryId);
            SqlParameter priceParameter = new SqlParameter("@price", entity.Price);

            command.Parameters.AddRange([nameParameter, categoryIdParameter, priceParameter]);
            command.Transaction = transaction;
            
            try
            {
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }

        }
        public void Update(Product entity)
        {
            transaction = connection.BeginTransaction();

            command = new SqlCommand(
                "UPDATE dbo.products SET Name = @name, CategoryId = @categoryId, Price = @price WHERE Id = @id", 
                connection);

            SqlParameter idParameter = new SqlParameter("@id", entity.Id);
            SqlParameter nameParameter = new SqlParameter("@name", entity.Name);
            SqlParameter categoryIdParameter = new SqlParameter("@categoryId", entity.CategoryId);
            SqlParameter priceParameter = new SqlParameter("@price", entity.Price);

            command.Parameters.AddRange([idParameter, nameParameter, categoryIdParameter, priceParameter]);
            command.Transaction = transaction;
            
            try
            {
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
        }
        public void Delete(int id)
        {
            transaction = connection.BeginTransaction();

            command = new SqlCommand(
                "DELETE dbo.products WHERE dbo.products.Id = @id", connection);

            SqlParameter idParameter = new SqlParameter("@id", id);

            command.Parameters.Add(idParameter);

            command.Transaction = transaction;

            try
            {
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
        }
    }
}
