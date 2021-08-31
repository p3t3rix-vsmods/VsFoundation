using ProtoBuf;

namespace Foundation.SynchronizedStorage
{
    [ProtoContract(ImplicitFields = ImplicitFields.None)]
    public class SynchronizedStorageMessage<T>
    {
        [ProtoMember(1, IsRequired = true)]
        public string Name { get; set; }


        [ProtoMember(2, IsRequired = true)]
        public T Storage { get; set; }
    }
}
