string fileDirectoryPath = "./";
string fileName = "plik-testowy";
string extension = ".txt";

StreamReader reader = new StreamReader(fileDirectoryPath + fileName + extension);
string newFileContent = reader
    .ReadToEnd()
    .Replace("praca", "job");
reader.Close();

File.Delete(fileDirectoryPath + fileName + extension);
DateTime date = DateTime.Now;

StreamWriter writer =
    new StreamWriter(fileDirectoryPath + fileName + "_changed" + date.ToString().Replace(".", "")[0..8] + extension);

writer.Write(newFileContent);
writer.Close();