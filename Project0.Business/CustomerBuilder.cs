using Project0.DataAccess.Model;
using System;

namespace Project0.Business {

    public class CustomerBuilder {

        public const string NAME_REGEX = @"^[A-Z][a-z]+ [A-Z][a-z]+";

        public Customer Build (string name) {

            var names = name.Split (" ");

            if (names.Length != 2) {
                throw new BusinessLogicException ("Need name in the form of 'Firstname Lastname'");
            }

            return new Customer {

                Firstname = names[0],
                Lastname = names[1]
            };
        }
    }
}
