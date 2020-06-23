﻿using System.Linq;
using System.Collections.Generic;

namespace Project0.Business.Database {

    /// <summary>
    /// Mock database containing all Store instances
    /// </summary>
    public class StoreDatabase : MockDatabase<Store> {

        public StoreDatabase (string jsonFile) : base (jsonFile) { }

        /// <summary>
        /// Returns all stores in the database
        /// </summary>
        public List<Store> FindAll {
            get => mItems;
        }

        /// <summary>
        /// Find a single store in the database with the given name
        /// </summary>
        /// <param name="storename">Name of the store</param>
        /// <returns>Store with matching name</returns>
        public Store FindByName (string storename) {

            var stores = from item in mItems
                        where item.Name == storename
                        select item;

            return stores.First ();
        }
    }
}
