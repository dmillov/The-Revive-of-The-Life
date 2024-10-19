using UnityEngine;
using Newtonsoft.Json;

namespace cdvproject.DataManagement.Serializer
{
    /// <summary>
    /// Implementation of data serialization in JSON format.
    /// This class provides methods for serializing and deserializing objects using the Newtonsoft.Json library.
    /// It represents one of the possible approaches for handling data serialization in a game.
    /// </summary>
    public class JsonDataSerializer : IDataSerializer
    {
        /// <summary>
        /// Serializes an object into JSON format.
        /// This method takes an object of any type and converts it into a JSON string representation.
        /// </summary>
        /// <typeparam name="T">The type of data to be serialized.</typeparam>
        /// <param name="data">The data to be serialized. This can be any object.</param>
        /// <returns>Returns the serialized data in JSON format.</returns>
        public string Serialize<T>(T data)
        {
            return JsonConvert.SerializeObject(data); // Convert the object to a JSON string
        }

        /// <summary>
        /// Deserializes JSON into an object of the specified type.
        /// This method takes a JSON string and converts it back into an object of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of object to which the data should be deserialized.</typeparam>
        /// <param name="data">The data in JSON format. This should be a valid JSON string.</param>
        /// <returns>Returns the deserialized object of type T.</returns>
        public T Deserialize<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data); // Convert the JSON string back to the specified object type
        }
    }
}