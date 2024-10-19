namespace cdvproject.DataManagement
{
    /// <summary>
    /// Interface for the storage system.
    /// Defines methods for saving and loading data.
    /// </summary>
    public interface IStorage
    {
        /// <summary>
        /// Saves data to a file at the specified path.
        /// </summary>
        /// <param name=“filePath”>Path to the file where the data will be saved.
        /// <param name=“data”>The data to be saved.</param
        void Save(string filePath, string data);

        /// <summary>
        /// Loads data from a file at the specified path.
        /// </summary>
        /// <param name=“filePath”>Path to the file where the data will be loaded.
        /// <returns>Returns the contents of the file as a string.</returns
        string Load(string filePath);
    }
}