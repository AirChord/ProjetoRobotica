using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


public class Client //: MonoBehaviour
{
    /*
    public static Client Instance;
    public static int dataBufferSize = 4096;

    public static string ip = "192.168.10.123";
    public static int port = 7000;
    public TCP tcp;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    private void Start()
    {
        //tcp = new TCP();
    }
    public void ConnectToServer()
    {
        Debug.Log("ConectServer");
        this.tcp = new TCP();
        this.tcp.Connect();
    }

    public void DiconnectServer()
    {
        Debug.Log("DiconectServer");
        this.tcp.Disconnect();
    }

    public void SendToServer(string data)
    {
        Debug.Log("Send2Server");
        this.tcp.Send(data);
    }
    
    public class TCP
    {
    */
    //public TcpClient socket;
        /// <summary>
        /// ////////////////////////////////////////////////
        /// </summary>
        public static int dataBufferSize = 4096;

        public static string ip = "192.168.10.123";
        public static int port = 7000;
       /// <summary>
       /// ////////////////////////////////////////////////
       /// </summary>
        public TcpClient _socket;
        private NetworkStream readStream;
        private NetworkStream writeStream;
        private byte[] receiveBuffer;

        public void Connect(TcpClient cl)
        {
            Debug.Log("TryConnect");
            this._socket = cl;
            //{
            //    ReceiveBufferSize = dataBufferSize,
            //    SendBufferSize = dataBufferSize
            //};

            receiveBuffer = new byte[dataBufferSize];
            this._socket.BeginConnect(System.Net.IPAddress.Parse(ip), port, ConnectCallback, this._socket);
            
        }

        private void ConnectCallback(IAsyncResult ia)
        {
            this._socket.EndConnect(ia);
            if (!this._socket.Connected)
            {
                Debug.Log("Unnable to Connect!");
                return;
            }

            readStream = this._socket.GetStream();
            readStream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
            Send("Hello\n");
        }

        private void ReceiveCallback(IAsyncResult ia)
        {
            try
            {
                int _byteLenght = readStream.EndRead(ia);
                if (_byteLenght <= 0)
                {
                    //TODO: diconnect
                    Disconnect();
                }

                byte[] _data = new byte[_byteLenght];
                Array.Copy(receiveBuffer, _data, _byteLenght);
                string newMessage = System.Text.Encoding.UTF8.GetString(_data, 0, _byteLenght);
                Debug.Log(newMessage);
            }
            catch
            {
                //TODO: disconnect
                Disconnect();
            }
        }

        public void Disconnect()
        {
            Debug.Log("socket close");
            this._socket.Close();
        }

        public void Send(string message)
        {
            Debug.Log("startSending");
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(message);

            if (!this._socket.Connected)
            {
                Debug.Log("SocketnotConnected!");
                return;
            }

            writeStream = this._socket.GetStream();
            writeStream.BeginWrite(buffer, 0, buffer.Length, (asyncWriteResult) =>
            {
                //Console.Write("w");
                writeStream.EndWrite(asyncWriteResult);
                //src.BeginRead(buffer, 0, buffer.Length, cbk, null);
            }, null);
            //stream.Write(buffer, 0, buffer.Length);
        }

        private void WriteCallback(IAsyncResult ia)
        {

            writeStream.EndWrite(ia);
        }
    //}
}

