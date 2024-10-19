namespace cdvproject.DataManagement.Serializer
{
    /// <summary>
    /// Interface for serializing data.
    /// Defines methods for serialization and deserialization.
    /// </summary>
    public interface IDataSerializer
    {
        /// <summary>
        /// Serializes an object to JSON format.
        /// </summary>
        /// <typeparam name=“T”>The type of data to be serialized.</typeparam
        /// <param name=“data”>The data to be serialized.</param
        /// <returns>Returns the serialized data in JSON format.
        string Serialize<T>(T data);

        /// <summary>
        /// Deserializes JSON to an object of the specified type.
        /// </summary>
        /// <typeparam name=“T”>The type of object to deserialize the data to.</typeparam
        /// <param name=“data”>Data in JSON format.</param
        /// <returns>Returns the deserialized object.</returns
        T Deserialize<T>(string data);
    }
}