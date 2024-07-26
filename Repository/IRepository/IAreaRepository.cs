using EmployeeNavigatorV3.Models;

namespace EmployeeNavigatorV3.Repository.IRepository
{
    public interface IAreaRepository
    {
        Task<List<Area>> GetAllAreas();
        Task<Area> GetAreaById(int id);
        Task<bool> CreateArea(Area area);
        Task<bool> UpdateArea(Area area, int id);
        Task<bool> DeleteArea(int id);
    }
}
