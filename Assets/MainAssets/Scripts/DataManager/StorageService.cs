using System.IO;
using SGS29.Utilities;
using UnityEngine;

/// <summary>
/// Service responsible for saving and loading game data.
/// Implements the Singleton pattern and provides a flexible interface for managing data.
/// 
/// Key Features:
/// 1. **Flexibility**: Supports saving complex data types such as strings, numbers, and objects.
/// 2. **Scalability**: Easily extendable for larger projects.
/// 3. **Versatility**: Supports saving data in various formats:
///    - JSON
///    - Binary format
///    - Server-side storage (integration capabilities with server solutions).
/// 4. **Scene Persistence**: Automatically saves and loads data for seamless gameplay.
/// 
/// This system provides a more robust and flexible solution for data storage compared to standard PlayerPrefs.
/// </summary>
public class StorageService : MonoSingleton<StorageService>
{
    private GameDataManager _gameDataManager; // Manager for handling game data
    private string _saveFilePath; // Path to the save file
    private string _fileName; // Name of the save file

    /// <summary>
    /// Initializes the service, ensuring it persists between scenes,
    /// and sets the path to the file for saving data.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this); // Ensures the object persists between scenes
    }

    /// <summary>
    /// Configures the game data manager and the path to the save file.
    /// </summary>
    private void Start()
    {
        _fileName = "saveData.json"; // Sets the default file name
        _saveFilePath = Path.Combine(Application.persistentDataPath, _fileName); // Determines the full path to the file
        _gameDataManager = new GameDataManager(new JsonStorage(), new JsonDataSerializer(), _saveFilePath); // Initializes the data manager
    }

    /// <summary>
    /// Sets the path for the save file.
    /// </summary>
    /// <param name="saveFilePath">The path to the file for saving data.</param>
    public void SetFilePath(string saveFilePath)
    {
        _saveFilePath = saveFilePath; // Assigns the new file path
    }

    /// <summary>
    /// Sets the name of the save file, adding ".json" to it if not already present.
    /// </summary>
    /// <param name="fileName">The name of the file without an extension.</param>
    public void SetFileName(string fileName)
    {
        // Adds the .json extension if it is not already present
        _fileName = fileName.EndsWith(".json") ? fileName : string.Concat(fileName, ".json");
    }

    /// <summary>
    /// Saves data with the specified key.
    /// </summary>
    /// <typeparam name="T">The type of data being saved.</typeparam>
    /// <param name="key">The key under which the data will be saved.</param>
    /// <param name="data">The data to save.</param>
    public void SaveData<T>(string key, T data)
    {
        _gameDataManager.SaveData(key, data); // Calls the save method in the manager
    }

    /// <summary>
    /// Loads data by the specified key.
    /// </summary>
    /// <typeparam name="T">The expected type of the data.</typeparam>
    /// <param name="key">The key for retrieving the data.</param>
    /// <returns>Returns the loaded data or the default value if the key is not found.</returns>
    public T GetData<T>(string key)
    {
        return _gameDataManager.LoadData<T>(key); // Loads the data by key
    }

    /// <summary>
    /// Loads data as a string, providing flexibility for cases where the data type is unknown or variable.
    /// </summary>
    /// <param name="key">The key for retrieving the data.</param>
    /// <returns>Returns the data as a string or null if the key is not found.</returns>
    public string GetDataAsString(string key)
    {
        var data = _gameDataManager.LoadData<object>(key); // Retrieves data as an object
        return data != null ? data.ToString() : null; // Returns the data as a string if found
    }

    /// <summary>
    /// Ensures the StorageService instance exists at runtime, 
    /// creating it if it does not.
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void EnsureStorageServiceExists()
    {
        // Checks for the existence of StorageService in the scene
        if (FindObjectOfType<StorageService>() == null)
        {
            GameObject storageServiceObject = new GameObject("StorageService"); // Creates a new object for the service
            storageServiceObject.AddComponent<StorageService>(); // Adds the StorageService component
        }
    }
}