using EmployeeNavigatorV3.Models;

namespace EmployeeNavigatorV3.Repository.IRepository
{
    public interface IAttachmentRepository
    {
        Task<List<Attachment>> GetAllDocuments(int personId);
        Task<bool> CreateDocument(Attachment documento);
        Task<bool> DeleteDocument(int id);
    }
}
