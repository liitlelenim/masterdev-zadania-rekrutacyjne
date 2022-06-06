using System.Text.Json;

Console.WriteLine("Podaj kwote do konwersji (PLN):");
string inputBuffer = Console.ReadLine();
decimal PLNValue;
if (Decimal.TryParse(inputBuffer, out PLNValue))
{
    Console.WriteLine("Pobieranie obecnego kursu PLN-USD...");
    HttpClient apiClient = new HttpClient();
    HttpResponseMessage response =
        await apiClient.GetAsync("https://api.nbp.pl/api/exchangerates/rates/a/usd/?format=json");
    string jsonResponseString = await response.Content.ReadAsStringAsync();
    var jsonResponse = JsonSerializer.Deserialize<Rootobject>(jsonResponseString);
    var USDExchangeRate = jsonResponse.rates[0].mid;
    Console.WriteLine("Podana kwota po konwersji do USD: " + Decimal.Round((PLNValue / USDExchangeRate), 2));
}
else
{
    Console.WriteLine("Podana kwota nie jest poprawną liczbą.");
}