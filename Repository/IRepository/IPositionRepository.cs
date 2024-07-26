using EmployeeNavigatorV3.Models;

namespace EmployeeNavigatorV3.Repository.IRepository
{
    public interface IPositionRepository
    {
        public Task<List<Position>> GetAllPositions();
    }
}
