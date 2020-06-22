using System.Collections.Generic;

namespace Project0.Business.Database {

    /// <summary>
    /// Mock database containing all Store instances
    /// </summary>
    public class StoreDatabase : MockDatabase<Store> {

        public StoreDatabase (string jsonFile) : base (jsonFile) { }

        public List<Store> FindAll {
            get => mItems;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storename"></param>
        /// <returns></returns>
        public Store FindByName (string storename) {

            foreach (var item in mItems) {

                if (item.Name == storename) {
                    return item;
                }
            }

            return default;
        }

        // TODO get list of all stores
    }
}
