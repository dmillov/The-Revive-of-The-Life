using UnityEngine;
using Newtonsoft.Json;

/// <summary>
/// Реалізація серіалізації даних у форматі JSON.
/// </summary>
public class JsonDataSerializer : IDataSerializer
{
    /// <summary>
    /// Серіалізує об'єкт у формат JSON.
    /// </summary>
    /// <typeparam name="T">Тип даних, які потрібно серіалізувати.</typeparam>
    /// <param name="data">Дані, які потрібно серіалізувати.</param>
    /// <returns>Повертає серіалізовані дані у форматі JSON.</returns>
    public string Serialize<T>(T data)
    {
        return JsonConvert.SerializeObject(data);
    }

    /// <summary>
    /// Десеріалізує JSON у об'єкт заданого типу.
    /// </summary>
    /// <typeparam name="T">Тип об'єкта, до якого потрібно десеріалізувати дані.</typeparam>
    /// <param name="data">Дані у форматі JSON.</param>
    /// <returns>Повертає десеріалізований об'єкт.</returns>
    public T Deserialize<T>(string data)
    {
        return JsonConvert.DeserializeObject<T>(data);
    }
}
