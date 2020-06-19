using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Project0.Business.Behavior {
    interface ISerialized {

        void Serialize (File file);
        void Deserialize (File file);
    }
}
