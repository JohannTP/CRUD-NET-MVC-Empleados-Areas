using EmployeeNavigatorV3.Models;
using EmployeeNavigatorV3.Repository.IRepository;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeNavigatorV3.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly string conn;

        public PersonRepository(IConfiguration configuration)
        {
            conn = configuration.GetConnectionString("CadenaSQL");
        }

        public async Task<int> CreatePerson(Person person)
        {
            try
            {
                using(SqlConnection connection = new SqlConnection(conn))
                {
                    await connection.OpenAsync();
                    using(SqlCommand cmd = new SqlCommand("SP_CreatePerson", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Code", person.Code);
                        cmd.Parameters.AddWithValue("@Dni", person.Dni);
                        cmd.Parameters.AddWithValue("@FirstName", person.Name);
                        cmd.Parameters.AddWithValue("@LastName", person.LastName);
                        cmd.Parameters.AddWithValue("@Email", person.Email);
                        cmd.Parameters.AddWithValue("@DateOfBirth", person.DateOfBirth);
                        cmd.Parameters.AddWithValue("@Age", person.Age);
                        cmd.Parameters.AddWithValue("@IdArea", person.Area);
                        cmd.Parameters.AddWithValue("@IdProfession", person.Profession);
                        cmd.Parameters.AddWithValue("@IdCargo", person.Position);
                        cmd.Parameters.AddWithValue("@IdUni", person.Uni);
                        cmd.Parameters.AddWithValue("@Status", person.Status);

                        var outputParam = new SqlParameter("@PersonId", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        await cmd.ExecuteNonQueryAsync();

                        return (int)outputParam.Value;
                    }
                }
            }
            catch(SqlException e)
            {
                throw;
            }
        }

        public async Task<bool> DeletePerson(int id)
        {
            try
            {
                using(SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();
                    using(SqlCommand cmd = new SqlCommand("SP_DeletePerson", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch(SqlException e)
            {
                throw;
            }
        }

        public async Task<List<Person>> GetAllPersons()
        {
            List<Person> listPersons = new List<Person>();
            try
            {
                using(SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();
                    using(SqlCommand cmd =  new SqlCommand("SP_GetAllPersons", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        using(SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                Person person = new Person
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Code = reader["Code"].ToString(),
                                    Dni = reader["Dni"].ToString(),
                                    Name = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Age = Convert.ToInt32(reader["Age"]),
                                    Area = reader["Area"].ToString()
                                };
                                listPersons.Add(person);
                            }
                        }
                    }
                }
                return listPersons;
            }
            catch (SqlException e)
            {
                throw;
            }
            throw new NotImplementedException();
        }

        public async Task<Person> GetPersonById(int id)
        {
            Person person = null;
            try
            {
                using(SqlConnection connection = new SqlConnection(conn))
                {
                    await connection.OpenAsync();

                    using(SqlCommand cmd = new SqlCommand("SP_GetPersonById", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);
                        using(SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                person = new Person
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Code = reader["Code"].ToString(),
                                    Dni = reader["Dni"].ToString(),
                                    Name = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Age = Convert.ToInt32(reader["Age"]),
                                    DateOfBirth = reader.GetDateTime((reader.GetOrdinal("DateOfBirth"))),
                                    Area = reader["Area"].ToString(),
                                    Uni = reader["Universidad"].ToString(),
                                    Profession = reader["Profesion"].ToString(),
                                    Position = reader["Cargo"].ToString()
                                };
                            }
                        }
                    }
                    return person;
                }
            }
            catch(SqlException e)
            {
                throw;
            }

        }

        public async Task<bool> UpdatePerson(Person person, int id)
        {
            try
            {
                using(SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();

                    using(SqlCommand cmd = new SqlCommand("SP_UpdatePerson", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.Parameters.AddWithValue("@Code", person.Code);
                        cmd.Parameters.AddWithValue("@Dni", person.Dni);
                        cmd.Parameters.AddWithValue("@FirstName", person.Name);
                        cmd.Parameters.AddWithValue("@LastName", person.LastName);
                        cmd.Parameters.AddWithValue("@Email", person.Email);
                        cmd.Parameters.AddWithValue("@DateOfBirth", person.DateOfBirth);
                        cmd.Parameters.AddWithValue("@Age", person.Age);
                        cmd.Parameters.AddWithValue("@IdArea", person.Area);
                        cmd.Parameters.AddWithValue("@IdProfession", person.Profession);
                        cmd.Parameters.AddWithValue("@IdCargo", person.Position);
                        cmd.Parameters.AddWithValue("@IdUni", person.Uni);
                        cmd.Parameters.AddWithValue("@Status", person.Status);

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch(SqlException e)
            {
                throw;
            }
        }
    }
}
