using System;
using System.Net;
using System.Threading.Tasks;
using CommObject;
using ObjectCommunication;
using ObjectCommunication.Models;

namespace VisionAlignChamber.Communication
{
    /// <summary>
    /// CTC 통신용 서버 래퍼 클래스
    /// </summary>
    public class CTCCommServer : IDisposable
    {
        #region Fields

        private readonly int _port;
        private readonly object _lock = new object();
        private AwaitServer _server;

        #endregion

        #region Events

        public delegate void ConnectionStateChangeEventHandler(object sender, IPEndPoint endPoint, bool connect);
        public event ConnectionStateChangeEventHandler ConnectionStateChange;

        public delegate void RcvObjectEventHandler(object sender, IPEndPoint senderEndPoint, CommObjectBase obj);
        public event RcvObjectEventHandler OnRcvObject;

        public delegate void CommLogEventHandler(object sender, string log);
        public event CommLogEventHandler OnCommLog;

        protected void OnLog(string log) => OnCommLog?.Invoke(this, log);

        #endregion

        #region Properties

        public bool IsListening => _server?.Listening ?? false;
        public int ClientCount => _server?.CountOfClients ?? 0;

        #endregion

        #region Constructor

        public CTCCommServer(int port)
        {
            _port = port;
            Initialize();
        }

        #endregion

        #region Public Methods

        public void Start()
        {
            try
            {
                if (_server != null)
                {
                    Task.Run(async () =>
                    {
                        await _server.StartCommunicationAsync();
                    });
                    OnLog("[CTCServer] Started");
                }
            }
            catch (Exception ex)
            {
                OnLog($"[CTCServer] Start Error: {ex.Message}");
            }
        }

        public void Stop()
        {
            _server?.StopCommunication();
            OnLog("[CTCServer] Stopped");
        }

        /// <summary>
        /// 특정 클라이언트에게 전송
        /// </summary>
        public async Task SendAsync(string ipAddress, int port, CommObjectBase obj)
        { 
            try
            {
                if (_server != null)
                {
                    await _server.Send(ipAddress, port, obj);
                }
            }
            catch (Exception ex)
            {
                OnLog($"[CTCServer] Send Error: {ex.Message}");
            }
        }

        /// <summary>
        /// 특정 클라이언트에게 전송
        /// </summary>
        public async Task SendAsync(IPEndPoint endPoint, CommObjectBase obj)
        {
            await SendAsync(endPoint.Address.ToString(), endPoint.Port, obj);
        }

        /// <summary>
        /// 연결된 모든 클라이언트에게 전송
        /// </summary>
        public async Task SendBroadcastAsync(CommObjectBase obj)
        {
            try
            {
                if (_server != null)
                {
                    await _server.SendBroadcast(obj);
                }
            }
            catch (Exception ex)
            {
                OnLog($"[CTCServer] Broadcast Error: {ex.Message}");
            }
        }

        /// <summary>
        /// 동기 전송 (Fire and forget)
        /// </summary>
        public void Send(CommObjectBase obj)
        {
            _ = SendBroadcastAsync(obj);
        }

        public void Dispose()
        {
            lock (_lock)
            {
                if (_server != null)
                {
                    _server.OnReceivedObject -= Server_OnReceivedObject;
                    _server.OnStatus -= Server_OnStatus;
                    _server.OnServerLog -= Server_OnServerLog;
                    _server.Dispose();
                    _server = null;
                }
            }
        }

        #endregion

        #region Private Methods

        private void Initialize()
        {
            lock (_lock)
            {
                try
                {
                    var endpoint = new IPEndPoint(IPAddress.Any, _port);

                    _server = new AwaitServer(
                        endpoint,
                        "CTCCommServer",
                        RcvBufferSize: 1024 * 1024 * 5,
                        SendBufferSize: 1024 * 1024 * 5
                    );

                    _server.OnReceivedObject += Server_OnReceivedObject;
                    _server.OnStatus += Server_OnStatus;
                    _server.OnServerLog += Server_OnServerLog;

                    OnLog("[CTCServer] Initialized");
                }
                catch (Exception ex)
                {
                    OnLog($"[CTCServer] Initialize Error: {ex.Message}");
                }
            }
        }

        private void Server_OnReceivedObject(IPEndPoint sender, CommObjectBase obj)
        {
            try
            {
                OnLog($"[CTCServer] Received from {sender}: {obj.GetType().Name}");
                OnRcvObject?.Invoke(this, sender, obj);
            }
            catch (Exception ex)
            {
                OnLog($"[CTCServer] Receive Error: {ex.Message}");
            }
        }

        private void Server_OnStatus(IPEndPoint endPoint, bool isConnected)
        {
            OnLog($"[CTCServer] Client {endPoint} - {(isConnected ? "Connected" : "Disconnected")}");
            ConnectionStateChange?.Invoke(this, endPoint, isConnected);
        }

        private void Server_OnServerLog(string log)
        {
            OnLog(log);
        }

        #endregion
    }
}
