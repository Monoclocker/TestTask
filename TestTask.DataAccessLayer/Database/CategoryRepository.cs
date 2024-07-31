using Microsoft.Data.SqlClient;
using TestTask.DataAccessLayer.Entities;
using TestTask.DataAccessLayer.Interfaces;

namespace TestTask.DataAccessLayer.Database
{
    internal class CategoryRepository: IRepository<Category>
    {
        readonly SqlConnection connection;
        SqlCommand? command;
        SqlTransaction? transaction;

        public CategoryRepository(DbContext context) 
        {
            this.connection = context.connection;
        }
        public Category? Get(int id)
        {
            command = new SqlCommand(
                    "SELECT * FROM dbo.categories WHERE Id = @id",
                    connection);
            
            SqlParameter idParameter = new SqlParameter("@id", id);

            command.Parameters.Add(idParameter);

            using (SqlDataReader reader = command.ExecuteReader()) 
            {
                if (reader.Read())
                {
                    return new Category()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };
                }
                return null;
            }

        }
        public IEnumerable<Category> GetAll()
        {
            command = new SqlCommand(
                    "SELECT * FROM dbo.categories",
                    connection);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read()) 
                {
                    yield return new Category()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };
                }
            }
        }
        public void Create(Category entity)
        {
            transaction = connection.BeginTransaction();

            command = new SqlCommand(
                    "INSERT INTO dbo.categories (Name) VALUES (@name)", connection
                );

            command.Transaction = transaction;

            SqlParameter nameParameter = new SqlParameter("@name", entity.Name);

            command.Parameters.Add(nameParameter);
            
            try
            {
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex) 
            {
                transaction.Rollback();
            }
        }
        public void Update(Category entity)
        {
            transaction = connection.BeginTransaction();

            command = new SqlCommand(
                    "UPDATE dbo.categories SET Name = @name WHERE Id = @id", 
                    connection);

            command.Transaction = transaction;

            SqlParameter idParameter = new SqlParameter("@id", entity.Id);
            SqlParameter nameParameter = new SqlParameter("@name", entity.Name);

            command.Parameters.Add(idParameter);
            command.Parameters.Add(nameParameter);

            try
            {
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
        }
        public void Delete(int id)
        {
            transaction = connection.BeginTransaction();

            command = new SqlCommand(
                    "DELETE dbo.categories WHERE Id=@id", connection);

            command.Transaction = transaction;

            SqlParameter idParameter = new SqlParameter("@id", id);

            command.Parameters.Add(idParameter);
            
            try
            {
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
        }

    }
}
