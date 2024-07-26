using EmployeeNavigatorV3.Models;
using EmployeeNavigatorV3.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeeNavigatorV3.Controllers
{
    public class AttachmentController : Controller
    {
        private readonly IAttachmentRepository _attachmentRepository;

        public AttachmentController(IAttachmentRepository attachmentRepository)
        {
            _attachmentRepository = attachmentRepository;
        }
        public async Task<IActionResult> Index(int id)
        {
            var documentos = await _attachmentRepository.GetAllDocuments(id);
            ViewBag.Documentos = documentos;
            ViewBag.PersonId = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file, int personId)
        {

            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError(string.Empty, "Please select a file to upload.");
                var documentos = await _attachmentRepository.GetAllDocuments(personId);
                ViewBag.IdLinea = personId;
                ViewBag.Documentos = documentos;
                return RedirectToAction("Index", new { id = personId });
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);

                var attachment = new Attachment
                {
                    PersonId = personId,
                    Nombre = Path.GetFileName(file.FileName),
                    Archivo = memoryStream.ToArray(),
                    Extension = Path.GetExtension(file.FileName),
                };

                await _attachmentRepository.CreateDocument(attachment);
            }

            return RedirectToAction("Index", new { id = personId }); // Redirige a la acción apropiada después de subir el archivo
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var documentoEliminado = await _attachmentRepository.DeleteDocument(id);
            if (documentoEliminado)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}
