using System.IO;

/// <summary>
/// Реалізація системи зберігання даних у форматі JSON.
/// </summary>
public class JsonStorage : IStorage
{
    /// <summary>
    /// Зберігає дані у файл за вказаним шляхом.
    /// </summary>
    /// <param name="filePath">Шлях до файлу, куди будуть збережені дані.</param>
    /// <param name="data">Дані, які потрібно зберегти.</param>
    public void Save(string filePath, string data)
    {
        File.WriteAllText(filePath, data);
    }

    /// <summary>
    /// Завантажує дані з файлу за вказаним шляхом.
    /// </summary>
    /// <param name="filePath">Шлях до файлу, звідки будуть завантажені дані.</param>
    /// <returns>Повертає вміст файлу у вигляді рядка.</returns>
    public string Load(string filePath)
    {
        if (File.Exists(filePath))
        {
            return File.ReadAllText(filePath);
        }
        throw new FileNotFoundException("File not found at: " + filePath);
    }
}
