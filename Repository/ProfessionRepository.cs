using EmployeeNavigatorV3.Models;
using EmployeeNavigatorV3.Repository.IRepository;
using Microsoft.Data.SqlClient;

namespace EmployeeNavigatorV3.Repository
{
    public class ProfessionRepository : IProfessionRepository
    {
        private readonly string conn;

        public ProfessionRepository(IConfiguration configuration)
        {
            conn = configuration.GetConnectionString("CadenaSQL");
        }
        public async Task<List<Profession>> GetAllProfessions()
        {
            List<Profession> listProfessions = new List<Profession>();
            try
            {
                using(SqlConnection connection = new SqlConnection(conn))
                {
                    await connection.OpenAsync();

                    using(SqlCommand cmd = new SqlCommand("SP_GetAllProfessions", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Profession profession = new Profession
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Name = reader["Name"].ToString(),
                                    Description = reader["Description"].ToString()
                                };
                                listProfessions.Add(profession);
                            }
                        }
                    }
                }
                return listProfessions;
            }
            catch(SqlException e)
            {
                throw;
            }
        }
    }
}
