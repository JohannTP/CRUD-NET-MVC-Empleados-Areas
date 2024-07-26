using EmployeeNavigatorV3.Models;
using EmployeeNavigatorV3.Repository.IRepository;
using Microsoft.Data.SqlClient;

namespace EmployeeNavigatorV3.Repository
{
    public class PositionRepository : IPositionRepository
    {

        private readonly string conn;

        public PositionRepository(IConfiguration configuration)
        {
            conn = configuration.GetConnectionString("CadenaSQL");        
        }

        public async Task<List<Position>> GetAllPositions()
        {
            List<Position> lisPositions = new List<Position>();
            try
            {
                using(SqlConnection connection = new SqlConnection(conn))
                {
                    await connection.OpenAsync();

                    using(SqlCommand cmd = new SqlCommand("SP_GetAllPositions", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Position position = new Position
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Name = reader["Name"].ToString(),
                                    Description = reader["Description"].ToString()
                                };
                                lisPositions.Add(position);
                            }
                        }
                    }
                }
                return lisPositions;
            }
            catch(SqlException e)
            {
                throw;
            }
        }
    }
}
