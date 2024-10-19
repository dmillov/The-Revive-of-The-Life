using cdvproject.DataManagement.Serializer;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace cdvproject.DataManagement
{
    /// <summary>
    /// Class for managing game data using a storage and serialization system.
    /// This class is responsible for saving, loading, and processing game data.
    /// </summary>
    public class GameDataManager
    {
        private readonly IStorage _storage; // Interface for data storage.
        private readonly IDataSerializer _serializer; // Interface for data serialization.
        private string _filePath; // File path for saving data.

        // Dictionary for storing different types of data
        private Dictionary<string, object> _dataStore; // Dictionary to hold data to be saved.

        /// <summary>
        /// Constructor for the GameDataManager class. 
        /// Initializes storage, serialization, and file path, 
        /// and attempts to load existing data from the file upon initialization.
        /// </summary>
        /// <param name="storage">Interface for data storage. Used for saving and loading files.</param>
        /// <param name="serializer">Interface for data serialization. Used for converting data to JSON format and vice versa.</param>
        /// <param name="filePath">File path for saving data. Specifies where the game data will be stored.</param>
        public GameDataManager(IStorage storage, IDataSerializer serializer, string filePath)
        {
            _storage = storage; // Initialize data storage
            _serializer = serializer; // Initialize serialization
            _filePath = filePath; // Set the file path
            _dataStore = new Dictionary<string, object>(); // Initialize the dictionary for data storage

            LoadAllData(); // Attempt to load existing data from the file upon initialization
        }

        /// <summary>
        /// Changes the file path for saving data.
        /// </summary>
        /// <param name="path">New path for the save file.</param>
        public void ChangePath(string path)
        {
            _filePath = path;
        }

        /// <summary>
        /// Saves data associated with the specified key.
        /// Adds or updates the data in the dictionary and saves it to a file.
        /// </summary>
        /// <typeparam name="T">Type of data to be saved.</typeparam>
        /// <param name="key">Key for saving data. Used to identify the data in the dictionary.</param>
        /// <param name="data">Data to be saved. Can be of any supported type.</param>
        public void SaveData<T>(string key, T data)
        {
            _dataStore[key] = data; // Add or update the data in the dictionary
            SaveAllData();          // Save the entire dictionary to the file
        }

        /// <summary>
        /// Loads data associated with the specified key.
        /// If the data is stored, it returns it; otherwise, it returns the default value for type T.
        /// </summary>
        /// <typeparam name="T">Type of data to be loaded.</typeparam>
        /// <param name="key">Key for loading data. Used to find data in the dictionary.</param>
        /// <returns>Returns the data if the key is found; otherwise, returns the default value.</returns>
        public T LoadData<T>(string key)
        {
            if (_dataStore.ContainsKey(key))
            {
                return (T)_dataStore[key]; // Cast the object to the required type
            }

            Debug.LogError("Key not found: " + key); // Log an error if the key is not found
            return default; // Return default value
        }

        /// <summary>
        /// Saves all data to the file.
        /// Uses serialization to convert the dictionary to JSON format and saves it to the specified file.
        /// </summary>
        private void SaveAllData()
        {
            try
            {
                // Check if the directory exists or create a new directory for the file
                string directory = Path.GetDirectoryName(_filePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory); // Create the directory if it does not exist
                    Debug.Log("Directory created: " + directory);
                }

                string jsonData = _serializer.Serialize(_dataStore); // Serialize the dictionary to JSON format
                _storage.Save(_filePath, jsonData); // Save the data to the file
                // Debug.Log("All data saved to " + _filePath); // Log a message indicating successful save
            }
            catch (Exception e)
            {
                Debug.LogError("Error saving data: " + e.Message); // Log an error message if saving fails
            }
        }

        /// <summary>
        /// Loads all data from the file.
        /// If the file does not exist, it creates an empty dictionary.
        /// </summary>
        private void LoadAllData()
        {
            try
            {
                // If the file does not exist, create an empty dictionary
                if (!File.Exists(_filePath))
                {
                    // Debug.LogWarning("Save file not found. Creating new empty data store.");
                    _dataStore = new Dictionary<string, object>(); // Initialize a new dictionary
                    return; // Exit the method
                }

                // If the file exists, load the data
                string jsonData = _storage.Load(_filePath); // Load the data from the file
                _dataStore = _serializer.Deserialize<Dictionary<string, object>>(jsonData); // Deserialize the data into a dictionary
                // Debug.Log("Data loaded from " + _filePath); // Log a message indicating successful load
            }
            catch (FileNotFoundException)
            {
                Debug.LogWarning("Save file not found. Starting with empty data."); // Warning message if the file is not found
                _dataStore = new Dictionary<string, object>(); // Create a new dictionary
            }
            catch (Exception e)
            {
                Debug.LogError("Error loading data: " + e.Message); // Log an error message if loading fails
                _dataStore = new Dictionary<string, object>(); // Create a new dictionary
            }
        }
    }
}