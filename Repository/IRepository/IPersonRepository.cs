using EmployeeNavigatorV3.Models;

namespace EmployeeNavigatorV3.Repository.IRepository
{
    public interface IPersonRepository
    {
        Task<List<Person>> GetAllPersons();
        Task<Person> GetPersonById(int id);
        Task<int> CreatePerson(Person person);
        Task<bool> UpdatePerson(Person person, int id);
        Task<bool> DeletePerson(int id);
    }
}
