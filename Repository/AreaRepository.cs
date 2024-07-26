using EmployeeNavigatorV3.Models;
using EmployeeNavigatorV3.Repository.IRepository;
using Microsoft.Data.SqlClient;

namespace EmployeeNavigatorV3.Repository
{
    public class AreaRepository : IAreaRepository
    {
        private readonly string conn;
        public AreaRepository(IConfiguration configuration)
        {
            conn = configuration.GetConnectionString("CadenaSQL");
        }


        public async Task<bool> CreateArea(Area area)
        {
            try
            {
                using(SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();
                    using(SqlCommand cmd = new SqlCommand("SP_InsertArea", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Name",area.Name);
                        cmd.Parameters.AddWithValue("@Description", area.Description);
                        cmd.Parameters.AddWithValue("@CreatedDateTime", area.CreatedDateTime);

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public async Task<bool> DeleteArea(int id)
        {
            try
            {
                using(SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();
                    using(SqlCommand cmd = new SqlCommand("SP_DeleteArea", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id",id);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public async Task<List<Area>> GetAllAreas()
        {
            List<Area> listArea = new List<Area>();
            try
            {
                using (SqlConnection connection = new(conn))
                {
                    await connection.OpenAsync();
                    using(SqlCommand cmd = new SqlCommand("SP_GetAllAreas", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Area area = new Area
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Name = reader["name"].ToString(),
                                    Description = reader["description"].ToString()
                                };
                                listArea.Add(area);
                            }
                        }
                    }
                }
                return listArea;
            }
            catch(SqlException e)
            {
                throw;
            }
        }

        public async Task<Area> GetAreaById(int id)
        {
            Area area = null;
            try
            {
                using(SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();
                    using(SqlCommand cmd = new SqlCommand("SP_GetAreaById", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);
                        using(SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                area = new Area
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Name = reader["Name"].ToString(),
                                    Description = reader["Description"].ToString()
                                };
                            }
                        }
                    }
                }
                return area;
            }
            catch(SqlException e)
            {
                throw;
            }
        }

        public async Task<bool> UpdateArea(Area area, int id)
        {
            try
            {
                using(SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();
                    using(SqlCommand cmd = new SqlCommand("SP_UpdateArea",connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.Parameters.AddWithValue("@Name", area.Name);
                        cmd.Parameters.AddWithValue("@Description", area.Description);
                        cmd.Parameters.AddWithValue("@ModificationDate", area.ModificationDate);

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch(SqlException ex)
            {
                throw;
            }
        }
    }
}
