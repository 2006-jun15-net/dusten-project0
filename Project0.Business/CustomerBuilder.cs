using Project0.DataAccess.Model;
using System;

namespace Project0.Business {

    public class CustomerBuilder {

        public const string NAME_REGEX = @"^[A-Z][a-z]+ [A-Z][a-z]+";

        public Customer Build (string name) {

            var names = name.Split (" ");

            if (names.Length != 2) {

                Console.WriteLine ("Need name in the form of 'Firstname Lastname'");
                return default;
            }

            return new Customer {

                Firstname = names[0],
                Lastname = names[1]
            };
        }
    }
}
