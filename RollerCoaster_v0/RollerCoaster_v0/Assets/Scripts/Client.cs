using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


public class Client //: MonoBehaviour
{

    public static int dataBufferSize = 4096;

    public static string ip = "192.168.10.123"; //kuka = "192.168.10.254"
    public static int port = 7000;
    public TcpClient _socket;
    private NetworkStream readStream;
    private NetworkStream writeStream;
    private byte[] receiveBuffer;

    /// <summary>
    /// Start connecting to Server
    /// </summary>
    public void Connect()
    {
        Debug.Log("TryConnect");
        this._socket = new TcpClient();
        receiveBuffer = new byte[dataBufferSize];
        this._socket.BeginConnect(System.Net.IPAddress.Parse(ip), port, ConnectCallback, this._socket); //Start async connection

    }

    /// <summary>
    /// End connecting to server process and start reading process
    /// </summary>
    /// <param name="ia"></param>
    private void ConnectCallback(IAsyncResult ia)
    {
        this._socket.EndConnect(ia);    //
        if (!this._socket.Connected)
        {
            Debug.Log("Unnable to Connect!");
            return;
        }
        this._socket.NoDelay = true;    //parameter to accelerate transmission
        readStream = this._socket.GetStream();
        readStream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
    }

    /// <summary>
    /// Activated when buffer receive something
    /// </summary>
    /// <param name="ia"></param>
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

    /// <summary>
    /// Close Comunication
    /// </summary>
    public void Disconnect()
    {
        Debug.Log("socket close");
        this._socket.Close();
    }

    /// <summary>
    /// Start sending process
    /// </summary>
    /// <param name="message"></param>
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
        //writeStream.BeginWrite(buffer, 0, buffer.Length, (asyncWriteResult) =>
        //{
        //        //Console.Write("w");
        //        writeStream.EndWrite(asyncWriteResult);
        //        //src.BeginRead(buffer, 0, buffer.Length, cbk, null);
        //    }, null);
        writeStream.BeginWrite(buffer, 0, buffer.Length, WriteCallback, null);
    }
    /// <summary>
    /// End send process
    /// </summary>
    /// <param name="ia"></param>
    private void WriteCallback(IAsyncResult ia)
    {

        writeStream.EndWrite(ia);
    }
    //}
}

