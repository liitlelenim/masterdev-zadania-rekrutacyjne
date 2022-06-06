using Microsoft.AspNetCore.Mvc;
using Zadanie1.Services;

namespace Zadanie1.Controllers
{
    public class ImportController : Controller
    {
        private readonly IImportService _importService;

        public ImportController(IImportService importService)
        {
            _importService = importService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Import(IFormFile? file, string format)
        {
            if (file is null)
            {
                ViewData["messageType"] = "error";
                ViewData["message"] = "Plik nie został poprawnie przesłany.";
                return View("Index");
            }

            if (format != "CSV" && format != "XLSX")
            {
                ViewData["messageType"] = "error";
                ViewData["message"] = "Podany został nieobsługiwany format pliku.";
                return View("Index");
            }

            if (format != file.FileName[^3..].ToUpper() && format != file.FileName[^4..].ToUpper())
            {
                ViewData["messageType"] = "error";
                ViewData["message"] = "Zadeklarowany format pliku nie zgadza się przesłanymi danymi.";
                return View("Index");
            }

            if (format == "CSV")
            {
                try
                {
                    await _importService.AddCustomersFromCsvToDatabaseAsync(file);
                    ViewData["messageType"] = "success";
                    ViewData["message"] = "Dane zostały poprawnie zaimportowane z pliku CSV.";
                    return View("Index");
                }
                catch (ArgumentException exception)
                {
                    ViewData["messageType"] = "error";
                    ViewData["message"] = exception.Message;
                    return View("Index");
                }
            }
            else if (format == "XLSX")
            {
                await _importService.AddCustomersFromXlsxToDatabaseAsync(file);
                ViewData["messageType"] = "success";
                ViewData["message"] = "Dane zostały poprawnie zaimportowane z pliku XLSX.";
                return View("Index");
            }

            return View("Index");
        }
    }
}