using System.IO;
using System.Linq;
using System.Collections.Generic;

using Newtonsoft.Json;

using Project0.Business.Behavior;

namespace Project0.Business.Database {

    /// <summary>
    /// The base class for any repository setups
    /// </summary>
    /// <typeparam name="T">Any type that implements the "ISerialized" interface</typeparam>
    public class Repository<T> where T : ISerialized {

        protected List<T> mItems;
        protected ulong mUuid;

        protected readonly string mJsonFile;

        public Repository (string jsonFile) {

            mItems = new List<T> ();
            mJsonFile = jsonFile;

            // IDs start at 1
            mUuid = 1;
        }

        /// <summary>
        /// Find a specified item given the serialization ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>Item</returns>
        public T FindByID (ulong id) {

            var items = from item in mItems 
                        where item.ID == id
                        select item;

            return items.First ();
        }

        /// <summary>
        /// Deserializes all items from a JSON file
        /// </summary>
        public virtual async void LoadItems () {

            string jsonText = await File.ReadAllTextAsync (mJsonFile);

            mItems = JsonConvert.DeserializeObject<List<T>> (jsonText);

            foreach (var item in mItems) {

                if (item.ID > mUuid) {
                    mUuid = item.ID;
                }
            }
        }

        /// <summary>
        /// Serializes all items and saves the resulting JSON text to a file
        /// </summary>
        public virtual async void SaveItems () {

            string jsonText = JsonConvert.SerializeObject (mItems);
            await File.WriteAllTextAsync (mJsonFile, jsonText);
        }
    }
}
