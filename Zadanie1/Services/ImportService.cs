using SpreadsheetLight;
using Zadanie1.DAL;
using Zadanie1.Models;

namespace Zadanie1.Services
{
    public interface IImportService
    {
        public Task AddCustomersFromCsvToDatabaseAsync(IFormFile csvFile);

        public Task AddCustomersFromXlsxToDatabaseAsync(IFormFile xlsxFile);
    }
    public class ImportService : IImportService
    {
        private readonly AppDbContext _db;
        public ImportService(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddCustomersFromCsvToDatabaseAsync(IFormFile csvFile)
        {
            using StreamReader reader = new StreamReader(csvFile.OpenReadStream());
            await reader.ReadLineAsync();
            List<CustomerModel> customersToAdd = new List<CustomerModel>();
            while (!reader.EndOfStream)
            {
                string? line = await reader.ReadLineAsync();
                if (line is null)
                {
                    throw new ArgumentException("Something went wrong while parsing csv file.");
                }
                string[] data = line.Split(";");

                if (!Plec.TryParse(data[5], out Plec plec))
                {
                    throw new ArgumentException("Something went wrong while parsing csv file.");
                }

                CustomerModel customer = new CustomerModel()
                {
                    Name = data[1],
                    Surename = data[2],
                    PESEL = data[3],
                    BirthYear = int.Parse(data[4]),
                    Płeć = plec
                };
                customersToAdd.Add(customer);
            }
            await _db.Customers.AddRangeAsync(customersToAdd);
            await _db.SaveChangesAsync();
        }

        public async Task AddCustomersFromXlsxToDatabaseAsync(IFormFile xlsxFile)
        {
            using SLDocument sheet = new SLDocument(xlsxFile.OpenReadStream());
            List<CustomerModel> customersToAdd = new List<CustomerModel>();
            int i = 2;
            while (sheet.GetCellValueAsString("A" + i).Length != 0)
            {
                if (!Plec.TryParse(sheet.GetCellValueAsString("E" + i), out Plec plec))
                {
                    throw new ArgumentException("Something went wrong while parsing csv file.");
                }
                CustomerModel customer = new CustomerModel()
                {
                    Name = sheet.GetCellValueAsString("A" + i),
                    Surename = sheet.GetCellValueAsString("B" + i),
                    PESEL = sheet.GetCellValueAsString("C" + i),
                    BirthYear = sheet.GetCellValueAsInt32("D" + i),
                    Płeć = plec
                };
                i++;

            }
            await _db.Customers.AddRangeAsync(customersToAdd);
            await _db.SaveChangesAsync();
        }
    }
}
