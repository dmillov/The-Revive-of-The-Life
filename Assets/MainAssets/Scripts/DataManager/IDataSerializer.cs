
/// <summary>
/// Інтерфейс для серіалізації даних.
/// Визначає методи для серіалізації та десеріалізації.
/// </summary>
public interface IDataSerializer
{
    /// <summary>
    /// Серіалізує об'єкт у формат JSON.
    /// </summary>
    /// <typeparam name="T">Тип даних, які потрібно серіалізувати.</typeparam>
    /// <param name="data">Дані, які потрібно серіалізувати.</param>
    /// <returns>Повертає серіалізовані дані у форматі JSON.</returns>
    string Serialize<T>(T data);

    /// <summary>
    /// Десеріалізує JSON у об'єкт заданого типу.
    /// </summary>
    /// <typeparam name="T">Тип об'єкта, до якого потрібно десеріалізувати дані.</typeparam>
    /// <param name="data">Дані у форматі JSON.</param>
    /// <returns>Повертає десеріалізований об'єкт.</returns>
    T Deserialize<T>(string data);
}
