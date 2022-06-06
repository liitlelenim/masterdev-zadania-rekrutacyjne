using Microsoft.AspNetCore.Mvc;
using Zadanie1.Services;

namespace Zadanie1.Controllers
{
    public class ExportController : Controller
    {
        private readonly IExportService _exportService;

        public ExportController(IExportService exportService)
        {
            _exportService = exportService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Export(string format)
        {
            if (format == "CSV")
            {
                return File(await _exportService.CreateCsvFileAsync(), "csv/text", $"dane-klienci-{DateTime.Now}.csv");
            }
            else if (format == "XLSX")
            {
                return File(_exportService.CreateXlsxFile(),
                    "application/vnd.ms-excel",
                    $"dane-klienci-{DateTime.Now}.xlsx");
            }

            return BadRequest("Invalid export format has been requested.");
        }
    }
}