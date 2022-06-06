using System.Text;
using SpreadsheetLight;
using Zadanie1.DAL;

namespace Zadanie1.Services
{
    public interface IExportService
    {
        public Task<MemoryStream> CreateCsvFileAsync();
        public MemoryStream CreateXlsxFile();
    }

    public class ExportService : IExportService
    {
        private readonly AppDbContext _db;

        public ExportService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<MemoryStream> CreateCsvFileAsync()
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            await writer.WriteLineAsync("Id;Name;Surename;PESEL;BirthYear;Plec");

            StringBuilder stringBuilder = new StringBuilder();

            foreach (var customer in _db.Customers.ToList())
            {
                stringBuilder.Append(customer.Id + ";");
                stringBuilder.Append(customer.Name + ";");
                stringBuilder.Append(customer.Surename + ";");
                stringBuilder.Append(customer.PESEL + ";");
                stringBuilder.Append(customer.BirthYear + ";");
                stringBuilder.Append(customer.Płeć + ";");

                await writer.WriteLineAsync(stringBuilder.ToString());
                stringBuilder.Clear();
            }

            ;
            await writer.FlushAsync();
            stream.Position = 0;
            return stream;
        }

        public MemoryStream CreateXlsxFile()
        {
            using SLDocument sheet = new SLDocument();

            sheet.SetCellValue("A1", "Id");
            sheet.SetCellValue("B1", "Name");
            sheet.SetCellValue("C1", "Surename");
            sheet.SetCellValue("D1", "PESEL");
            sheet.SetCellValue("E1", "Birth Year");
            sheet.SetCellValue("F1", "Płeć");
            
            int i = 2;
            foreach (var customer in _db.Customers.ToList())
            {
                sheet.SetCellValue("A" + i, customer.Id);
                sheet.SetCellValue("B" + i, customer.Name);
                sheet.SetCellValue("C" + i, customer.Surename);
                sheet.SetCellValue("D" + i, customer.PESEL);
                sheet.SetCellValue("E" + i, customer.BirthYear);
                sheet.SetCellValue("F" + i, customer.Płeć.ToString());
                i++;
            }

            MemoryStream stream = new MemoryStream();
            sheet.SaveAs(stream);
            stream.Position = 0;
            return stream;
        }
    }
}