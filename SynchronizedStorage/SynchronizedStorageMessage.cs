using ProtoBuf;

namespace Foundation.SynchronizedStorage
{
    [ProtoContract(ImplicitFields = ImplicitFields.None)]
    public class SynchronizedStorageMessage
    {
        [ProtoMember(1, IsRequired = true)] 
        public string Name { get; set; }


        [ProtoMember(2, IsRequired = true)] 
        public byte[] Storage { get; set; }
    }
}