using System.IO;
using System.Collections.Generic;

using Newtonsoft.Json;

using Project0.Business.Behavior;

namespace Project0.Business.Database {

    /// <summary>
    /// The base class for any mock database setups
    /// </summary>
    /// <typeparam name="T">Any type that implements the "ISerialized" interface</typeparam>
    public class MockDatabase<T> where T : ISerialized {

        protected List<T> mItems;

        public MockDatabase () {
            mItems = new List<T> ();
        }

        /// <summary>
        /// Find a specified item given the serialization ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>Item</returns>
        public T FindByID (int id) {

            foreach (var item in mItems) {

                if (item.ID == id) {
                    return item;
                }
            }

            return default;
        }

        /// <summary>
        /// Deserializes all items from a JSON file
        /// </summary>
        /// <param name="jsonFile">JSON file path</param>
        public async void LoadItems (string jsonFile) {

            string jsonText = await File.ReadAllTextAsync (jsonFile);
            mItems = JsonConvert.DeserializeObject<List<T>>(jsonText);
        }

        /// <summary>
        /// Serializes all items and saves the resulting JSON text to a file
        /// </summary>
        /// <param name="jsonFile">JSON file path</param>
        public async void SaveItems (string jsonFile) {

            string jsonText = JsonConvert.SerializeObject (mItems);
            await File.WriteAllTextAsync (jsonFile, jsonText);
        }
    }
}
