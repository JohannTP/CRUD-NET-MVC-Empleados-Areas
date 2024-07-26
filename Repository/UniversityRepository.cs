using EmployeeNavigatorV3.Models;
using EmployeeNavigatorV3.Repository.IRepository;
using Microsoft.Data.SqlClient;

namespace EmployeeNavigatorV3.Repository
{
    public class UniversityRepository : IUniversityRepository
    {
        private readonly string conn;

        public UniversityRepository(IConfiguration configuration)
        {
            conn = configuration.GetConnectionString("CadenaSQL");
        }
        public async Task<List<University>> GetAllUniversities()
        {
            List<University> listUniversities = new List<University>();
            try
            {
                using(SqlConnection connection = new SqlConnection(conn))
                {
                    await connection.OpenAsync();

                    using(SqlCommand cmd = new SqlCommand("SP_GetAllUniversities", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                University uni = new University()
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Name = reader["Name"].ToString(),
                                    Description = reader["Description"].ToString()
                                };
                                listUniversities.Add(uni);
                            }
                        }
                    }
                }
                return listUniversities;
            }
            catch(SqlException e)
            {
                throw;
            }
        }
    }
}
