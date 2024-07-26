using EmployeeNavigatorV3.Models;
using EmployeeNavigatorV3.Repository.IRepository;
using Microsoft.Data.SqlClient;

namespace EmployeeNavigatorV3.Repository
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly string conn;

        public AttachmentRepository(IConfiguration configuration)
        {
            conn = configuration.GetConnectionString("CadenaSQL");
        }
        public async Task<bool> CreateDocument(Attachment documento)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    await connection.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_InsertAttachment", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@personId", documento.PersonId);
                        cmd.Parameters.AddWithValue("@attachment_name", documento.Nombre);
                        cmd.Parameters.AddWithValue("@attachment_file", documento.Archivo);
                        cmd.Parameters.AddWithValue("@attachment_extension", documento.Extension);

                        await cmd.ExecuteNonQueryAsync();

                    }
                }
                return true;
            }
            catch (SqlException e)
            {
                throw;
            }
        }

        public async Task<bool> DeleteDocument(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    await connection.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_DeleteAttachment", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@attachment_Id", id);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (SqlException e)
            {
                throw;
            }
        }

        public async Task<List<Attachment>> GetAllDocuments(int personId)
        {
            List<Attachment> listAttachments = new List<Attachment>();
            try
            {
                using (SqlConnection connection = new(conn))
                {
                    await connection.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_GetAllAttachments", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@personId", personId);
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                Attachment documento = new Attachment
                                {
                                    Id = Convert.ToInt32(reader["attachment_Id"]),
                                    PersonId = Convert.ToInt32(reader["attachment_PersonId"]),
                                    Nombre = reader["attachment_name"].ToString(),
                                    Extension = reader["attachment_extension"].ToString(),
                                    Archivo = (byte[])reader["attachment_file"],
                                };
                                listAttachments.Add(documento);
                            }
                        }
                    }
                }
                return listAttachments;
            }
            catch (SqlException e)
            {
                throw;
            }
        }
    }
}
