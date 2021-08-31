using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Server;
using Vintagestory.API.Util;

namespace Foundation.SynchronizedStorage
{
    /// <summary>
    /// A helper class to reduce the boilerplate code of synchronizing data from server to client (one way)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SynchronizeOneWayStorage<T> where T : class, new()
    {
        public string Name { get; }
        public ICoreAPI Api { get; }
        private IServerNetworkChannel _serverChannel;
        private IClientNetworkChannel _clientChannel;
        private T _storage;

        public T Storage
        {
            get => _storage;
            set
            {
                _storage = value;
                Refresh();
            }
        }

        public event System.Action<T> OnChanged;

        public void Refresh()
        {
            SendDataToClients();
            OnChanged?.Invoke(Storage);
        }

        public SynchronizeOneWayStorage(string name, ICoreAPI api)
        {
            Name = name;
            Api = api;
        }

        #region Server
        public virtual void StartServerSide(ICoreServerAPI api)
        {
            api.Event.SaveGameLoaded += () => OnSaveGameLoading(api);
            api.Event.GameWorldSave += () => OnSaveGameSaving(api);
            api.Event.PlayerJoin += player => SendDataToClients();

            _serverChannel = api.Network.RegisterChannel(Name)
                .RegisterMessageType<SynchronizedStorageMessage<T>>();
        }
        protected virtual void OnSaveGameSaving(ICoreServerAPI api)
        {
            api.WorldManager.SaveGame.StoreData(Name, SerializerUtil.Serialize(Storage));
        }

        protected virtual void OnSaveGameLoading(ICoreServerAPI api)
        {
            var data = api.WorldManager.SaveGame.GetData(Name);
            Storage = data != null ? SerializerUtil.Deserialize<T>(data) : new T();
        }

        protected virtual void SendDataToClients()
        {
            if (Api is ICoreServerAPI)
            {
                _serverChannel?.BroadcastPacket(new SynchronizedStorageMessage<T>() { Name = Name, Storage = Storage });
            }
        }
        #endregion

        #region Client

        public virtual void StartClientSide(ICoreClientAPI api)
        {
            _clientChannel = api.Network.RegisterChannel(Name)
                .RegisterMessageType<SynchronizedStorageMessage<T>>()
                .SetMessageHandler<SynchronizedStorageMessage<T>>(OnClientDataReceived);
        }

        protected virtual void OnClientDataReceived(SynchronizedStorageMessage<T> msg)
        {
            if (msg.Name == Name)
            {
                Storage = msg.Storage;
            }
        }
        #endregion
    }
}
