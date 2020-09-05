using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Foundation.SynchronizedStorage
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class SynchronizedStorageMessage<T>
    {
        public String Name { get; set; }
        public T Storage { get; set; }
    }
}
