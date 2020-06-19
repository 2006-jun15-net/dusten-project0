using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Project0.Business.Behavior {
    interface ISerialized {

        void Serialize (string file);
        void Deserialize (string file);
    }
}
