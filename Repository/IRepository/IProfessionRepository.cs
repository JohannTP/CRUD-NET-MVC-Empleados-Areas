

using EmployeeNavigatorV3.Models;

namespace EmployeeNavigatorV3.Repository.IRepository
{
    public interface IProfessionRepository
    {
        public Task<List<Profession>> GetAllProfessions();
    }
}
