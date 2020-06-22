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
        protected ulong mUuid;

        private string mJsonFile;

        public MockDatabase (string jsonFile) {

            mItems = new List<T> ();

            mJsonFile = jsonFile;
            mUuid = 0;
        }

        /// <summary>
        /// Find a specified item given the serialization ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>Item</returns>
        public T FindByID (ulong id) {

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
        public async void LoadItems () {

            string jsonText = await File.ReadAllTextAsync (mJsonFile);
            mItems = JsonConvert.DeserializeObject<List<T>>(jsonText);

            foreach (var item in mItems) {

                if (item.ID > mUuid) {
                    mUuid = item.ID;
                }
            }
        }

        /// <summary>
        /// Serializes all items and saves the resulting JSON text to a file
        /// </summary>
        public async void SaveItems () {

            string jsonText = JsonConvert.SerializeObject (mItems);
            await File.WriteAllTextAsync (mJsonFile, jsonText);
        }
    }
}
