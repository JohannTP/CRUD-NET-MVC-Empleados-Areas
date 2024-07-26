using EmployeeNavigatorV3.Models;
using EmployeeNavigatorV3.Repository.IRepository;
using EmployeeNavigatorV3.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeNavigatorV3.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepository;
        private readonly IProfessionRepository _professionRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly IAreaRepository _areaRepository;

        public PersonController(IPersonRepository personRepository, IProfessionRepository professionRepository, 
            IPositionRepository positionRepository, IUniversityRepository universityRepository, IAreaRepository areaRepository)
        {
            _personRepository = personRepository;
            _professionRepository = professionRepository;
            _positionRepository = positionRepository;
            _universityRepository = universityRepository;
            _areaRepository = areaRepository;
        }
        public async Task<IActionResult> Index()
        {
            var persons = await _personRepository.GetAllPersons();
            await GetAttributes();
            ViewBag.Persons = persons;
            return View();
        }

        public async Task<IActionResult> Create()
        {
            await GetAttributes();
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            await GetAttributes();
            var person = await _personRepository.GetPersonById(id);
            ViewBag.PersonId = id;
            return View(person);
        }


        #region API

        [HttpPost]
        public async Task<IActionResult> Create(Person person)
        {
            person.Status = 1;
            var result = await _personRepository.CreatePerson(person);
            if (result != null)
            {
                TempData[DS.Exitosa] = "Empleado " + person.Name + " creado con exito";
                return RedirectToAction("Index", "Attachment", new { id = result });
            }

            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Person person)
        {
            person.Status = 1;
            var resul = await _personRepository.UpdatePerson(person, person.Id);
            if (resul)
            {
                TempData[DS.Exitosa] = "Empleado " + person.Name + " actualizado con exito";
                return RedirectToAction("Index", "Person");
            }
            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _personRepository.DeletePerson(id);
            if (result)
            {
                return Json(new { success = true });
            }
            return Json(new {success = false});
        }


        #endregion

        public async Task GetAttributes()
        {
            List<Area> listAreas = await _areaRepository.GetAllAreas();
            ViewBag.Areas = new SelectList(listAreas, "Id", "Name");

            List<University> listUniversities = await _universityRepository.GetAllUniversities();
            ViewBag.Universities = new SelectList(listUniversities, "Id", "Name");

            List<Position> listPositions = await _positionRepository.GetAllPositions();
            ViewBag.Positions = new SelectList(listPositions, "Id", "Name");

            List<Profession> listProfessions = await _professionRepository.GetAllProfessions();
            ViewBag.Professions = new SelectList(listProfessions, "Id", "Name");
        }
    }
}
