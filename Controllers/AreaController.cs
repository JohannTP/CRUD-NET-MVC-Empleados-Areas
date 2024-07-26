using EmployeeNavigatorV3.Models;
using EmployeeNavigatorV3.Repository.IRepository;
using EmployeeNavigatorV3.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeNavigatorV3.Controllers
{
    public class AreaController : Controller
    {
        private readonly IAreaRepository _areaRepository;
        public AreaController(IAreaRepository areaRepository)
        {
            _areaRepository = areaRepository;
        }
        public async Task<IActionResult> Index()
        {
            var areas = await _areaRepository.GetAllAreas();
            ViewBag.Areas = areas;
            return View();
        }


        public ActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var area = await _areaRepository.GetAreaById(id);
            return View(area);
        }

        #region API

        [HttpPost]
        public async Task<IActionResult> Create(Area area)
        {
            area.CreatedDateTime = DateTime.Now;
            var result = await _areaRepository.CreateArea(area);
            if(result)
            {
                TempData[DS.Exitosa] = "Area " + area.Name + " creada con exito";
                return RedirectToAction("Index", "Area");   
            }
            return View(area);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Area area)
        {
            area.ModificationDate = DateTime.Now;
            var result = await _areaRepository.UpdateArea(area, area.Id);
            if(result)
            {
                TempData[DS.Exitosa] = "Area actualizada correctamente";
                return RedirectToAction("Index", "Area");
            }
            return View(area) ;
        }



        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _areaRepository.DeleteArea(id);
            if (result)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        //[HttpPost]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var result = await _areaRepository.DeleteArea(id);
        //    if (result)
        //    {
        //        return RedirectToAction("Index", "Area");
        //    }
        //    return View();
        //}
        #endregion
    }
}
