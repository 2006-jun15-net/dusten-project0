using System.IO;

namespace Project0.DataAccess {

    public static class ConnectionString {

        /// <summary>
        /// Connection string is stored at root of the project in a hidden
        /// '.connection-string.txt' file 
        /// </summary>
        public static string mConnectionString = File.ReadAllText ("../../../../.connection-string.txt");
    }
}
