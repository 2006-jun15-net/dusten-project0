using System;

namespace Project0.Business {

    public class BusinessLogicException : Exception {

        public BusinessLogicException (string message) : base (message) { }
    }
}
