const string basePath = @"C:\test\";
string fileName = "test_kuba_blaszczyk.txt";


string fileContent = File.ReadAllText(basePath + fileName);

int aAmount = fileContent.Count(character => character == 'a');

Console.WriteLine(aAmount);
if (fileName.Length > 15)
{
    using FileStream fileStream = File.Open(basePath + fileName, FileMode.Open);
    string[] fileNameWords = fileStream.Name.Split("_");
    string newFileName = $"test_{fileNameWords[1][0..3]}_{fileNameWords[2][0..3]}.txt";
    File.Create(basePath + newFileName).Close();
    using StreamWriter writer = new StreamWriter(basePath + newFileName);
    writer.Write(fileContent);
    writer.Close();
    fileStream.Close();
    File.Delete(basePath + fileName);
}