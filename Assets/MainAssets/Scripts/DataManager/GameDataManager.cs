using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Клас для керування даними гри, який використовує систему зберігання та серіалізації.
/// </summary>
public class GameDataManager
{
    private readonly IStorage _storage;
    private readonly IDataSerializer _serializer;
    private readonly string _filePath;

    // Словник для зберігання різних типів даних
    private Dictionary<string, object> _dataStore;

    /// <summary>
    /// Конструктор класу GameDataManager. Ініціалізує зберігання, серіалізацію та шлях до файлу.
    /// </summary>
    /// <param name="storage">Інтерфейс для зберігання даних.</param>
    /// <param name="serializer">Інтерфейс для серіалізації даних.</param>
    /// <param name="filePath">Шлях до файлу для збереження даних.</param>
    public GameDataManager(IStorage storage, IDataSerializer serializer, string filePath)
    {
        _storage = storage;
        _serializer = serializer;
        _filePath = filePath;
        _dataStore = new Dictionary<string, object>();

        LoadAllData(); // Спроба завантажити наявні дані з файлу при ініціалізації
    }

    /// <summary>
    /// Зберігає дані за вказаним ключем.
    /// </summary>
    /// <typeparam name="T">Тип даних, які потрібно зберегти.</typeparam>
    /// <param name="key">Ключ для збереження даних.</param>
    /// <param name="data">Дані, які потрібно зберегти.</param>
    public void SaveData<T>(string key, T data)
    {
        _dataStore[key] = data; // Додаємо або оновлюємо дані у словнику
        SaveAllData();          // Зберігаємо весь словник
    }

    /// <summary>
    /// Завантажує дані за вказаним ключем.
    /// </summary>
    /// <typeparam name="T">Тип даних, які потрібно завантажити.</typeparam>
    /// <param name="key">Ключ для завантаження даних.</param>
    /// <returns>Повертає дані, якщо ключ знайдено, інакше повертає значення за замовчуванням.</returns>
    public T LoadData<T>(string key)
    {
        if (_dataStore.ContainsKey(key))
        {
            return (T)_dataStore[key]; // Конвертуємо об'єкт до потрібного типу
        }

        Debug.LogError("Key not found: " + key);
        return default;
    }

    /// <summary>
    /// Зберігає всі дані у файл.
    /// </summary>
    private void SaveAllData()
    {
        try
        {
            // Перевірка, чи існує файл або створення нової директорії для файлу
            string directory = Path.GetDirectoryName(_filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                Debug.Log("Directory created: " + directory);
            }

            string jsonData = _serializer.Serialize(_dataStore);
            _storage.Save(_filePath, jsonData);
            Debug.Log("All data saved to " + _filePath);
        }
        catch (Exception e)
        {
            Debug.LogError("Error saving data: " + e.Message);
        }
    }

    /// <summary>
    /// Завантажує всі дані з файлу.
    /// </summary>
    private void LoadAllData()
    {
        try
        {
            // Якщо файл не існує, створюємо порожній словник
            if (!File.Exists(_filePath)) 
            {
                Debug.LogWarning("Save file not found. Creating new empty data store.");
                _dataStore = new Dictionary<string, object>();
                return;
            }

            // Якщо файл існує, завантажуємо дані
            string jsonData = _storage.Load(_filePath);
            _dataStore = _serializer.Deserialize<Dictionary<string, object>>(jsonData);
            Debug.Log("Data loaded from " + _filePath);
        }
        catch (FileNotFoundException)
        {
            Debug.LogWarning("Save file not found. Starting with empty data.");
            _dataStore = new Dictionary<string, object>();
        }
        catch (Exception e)
        {
            Debug.LogError("Error loading data: " + e.Message);
            _dataStore = new Dictionary<string, object>();
        }
    }
}