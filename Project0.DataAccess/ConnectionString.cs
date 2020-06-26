using System.IO;

namespace Project0.DataAccess {

    public static class ConnectionString {

        public static string mConnectionString = File.ReadAllText ("../../../../.connection-string.txt");
    }
}
