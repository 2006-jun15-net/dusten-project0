using System.Collections.Generic;

using Project0.DataAccess.Model;

namespace Project0.DataAccess.Repository {

    interface IRepository<T> where T : IModel {

        List<T> FindAll ();
        T FindById (int id);
    }
}
