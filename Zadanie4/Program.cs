string[] namesPool = new string[] { "Ania", "Kasia", "Basia", "Zosia" };
string[] lastNamePools = new string[] { "Kowalska", "Nowak" };

string currentDate = DateTime.Now
    .ToString()
    .Replace(":", "_")
    .Replace("-", "_");

string fileName = $"users_{currentDate}.csv";
StreamWriter writer = new StreamWriter(fileName);
await writer.WriteLineAsync("LP;Imie;Nazwisko;Rok Urodzenia");
Random rng = new Random();
for (int i = 1; i < 501; i++)
{
    int id = i;
    string name = namesPool[rng.Next(namesPool.Length)];
    string lastName = lastNamePools[rng.Next(lastNamePools.Length)];
    int yearOfBirth = 1990 + rng.Next(11);
    await writer.WriteLineAsync($"{id};{name};{lastName};{yearOfBirth}");
}

writer.Close();