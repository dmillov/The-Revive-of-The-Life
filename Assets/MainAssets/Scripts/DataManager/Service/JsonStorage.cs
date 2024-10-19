using System.IO;

namespace cdvproject.DataManagement
{
    /// <summary>
    /// Implementation of a data storage system in JSON format.
    /// This class provides methods for saving and loading data to and from files.
    /// It is one of several possible approaches for data storage, allowing for various implementations such as 
    /// server storage or binary format. This flexibility enables developers to choose the most suitable method 
    /// for their application's needs.
    /// </summary>
    public class JsonStorage : IStorage
    {
        /// <summary>
        /// Saves data to a file at the specified path.
        /// This method takes a JSON string and writes it to a file.
        /// </summary>
        /// <param name="filePath">The path to the file where the data will be saved.</param>
        /// <param name="data">The data to be saved, typically in JSON format.</param>
        public void Save(string filePath, string data)
        {
            File.WriteAllText(filePath, data); // Write the JSON string to the specified file
        }

        /// <summary>
        /// Loads data from a file at the specified path.
        /// This method reads the contents of a file and returns it as a string.
        /// </summary>
        /// <param name="filePath">The path to the file from which data will be loaded.</param>
        /// <returns>Returns the contents of the file as a string.</returns>
        /// <exception cref="FileNotFoundException">Thrown when the specified file does not exist.</exception>
        public string Load(string filePath)
        {
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath); // Read the contents of the file if it exists
            }
            throw new FileNotFoundException("File not found at: " + filePath); // Throw an exception if the file is not found
        }
    }
}