using EmployeeNavigatorV3.Models;

namespace EmployeeNavigatorV3.Repository.IRepository
{
    public interface IUniversityRepository
    {
        public Task<List<University>> GetAllUniversities();
    }
}
