
/// <summary>
/// Інтерфейс для системи зберігання.
/// Визначає методи для збереження та завантаження даних.
/// </summary>
public interface IStorage
{
    /// <summary>
    /// Зберігає дані у файл за вказаним шляхом.
    /// </summary>
    /// <param name="filePath">Шлях до файлу, куди будуть збережені дані.</param>
    /// <param name="data">Дані, які потрібно зберегти.</param>
    void Save(string filePath, string data);

    /// <summary>
    /// Завантажує дані з файлу за вказаним шляхом.
    /// </summary>
    /// <param name="filePath">Шлях до файлу, звідки будуть завантажені дані.</param>
    /// <returns>Повертає вміст файлу у вигляді рядка.</returns>
    string Load(string filePath);
}
