using System.IO;
using cdvproject.DataManagement.Serializer;
using SGS29.Utilities;
using UnityEngine;

namespace cdvproject.DataManagement
{
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
        private const string DEFAULT_FILE_NAME = "GameData.json"; // Default file name for saving data

        /// <summary>
        /// Initializes the service, ensuring it persists between scenes,
        /// and sets the path to the file for saving data.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this); // Ensures the object persists between scenes

            _fileName = DEFAULT_FILE_NAME;
            _saveFilePath = Path.Combine(Application.persistentDataPath, _fileName);

            // Initializes the game data manager with JSON storage and serializer
            _gameDataManager = new GameDataManager(new JsonStorage(), new JsonDataSerializer(), _saveFilePath);
        }

        /// <summary>
        /// Resets the file name to the default settings.
        /// This ensures the service starts with a predefined filename.
        /// </summary>
        public void ResetNameToDefault()
        {
            SetFileName(DEFAULT_FILE_NAME);
        }

        /// <summary>
        /// Resets the file path to default settings.
        /// This ensures the service starts with a predefined path.
        /// </summary>
        public void ResetPathToDefault()
        {
            SetFilePath(Application.persistentDataPath);
        }

        /// <summary>
        /// Sets the path for the save file.
        /// This method updates the save file path and informs the data manager.
        /// </summary>
        /// <param name="saveFilePath">The path to the file for saving data.</param>
        public void SetFilePath(string saveFilePath)
        {
            if (string.IsNullOrEmpty(saveFilePath))
            {
                Debug.LogError("Attempted to set a null or empty file path.");
                return;
            }
            _saveFilePath = Path.Combine(saveFilePath, _fileName); // Assigns the new file path
            UpdateDataManagerPath(); // Updates the data manager's path
        }


        /// <summary>
        /// Sets the name of the save file, adding ".json" to it if not already present,
        /// and updates the file path accordingly.
        /// </summary>
        /// <param name="fileName">The name of the file without an extension.</param>
        public void SetFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                Debug.LogError("Attempted to set a null or empty file name.");
                return;
            }

            // Adds the .json extension if it is not already present
            _fileName = fileName.EndsWith(".json") ? fileName : string.Concat(fileName, ".json");

            // Update the save file path with the new file name while keeping the existing directory
            if (string.IsNullOrEmpty(_saveFilePath))
            {
                Debug.LogError("Cannot update path, _saveFilePath is null.");
                return;
            }
            string directory = Path.GetDirectoryName(_saveFilePath); // Get the current directory from the existing path
            _saveFilePath = Path.Combine(directory, _fileName); // Combine the existing directory with the new file name

            UpdateDataManagerPath(); // Updates the data manager's path
        }

        /// <summary>
        /// Saves data with the specified key.
        /// This method delegates the save operation to the game data manager.
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
        /// This method retrieves data from the game data manager.
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
        /// Updates the data manager with the new path for saving data.
        /// This is called whenever the save file path changes.
        /// </summary>
        private void UpdateDataManagerPath()
        {
            _gameDataManager.ChangePath(_saveFilePath);
        }

        /// <summary>
        /// Ensures the StorageService instance exists at runtime, 
        /// creating it if it does not.
        /// This method is called before any scene is loaded.
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
}