﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


public class Client //: MonoBehaviour
{

    public static int dataBufferSize = 4096;

    public static string ip = "192.168.10.254"; //kuka = "192.168.10.254"
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
            //string newMessage = System.Text.Encoding.UTF8.GetString(_data, 0, _byteLenght);
            Debug.Log("Received data: ");
            Debug.Log(BitConverter.ToString(_data));

            Debug.Log("Message conv: ");
            byte[] mesRes = new byte[_data.Length];
            System.Buffer.BlockCopy(_data, 8, mesRes, 0, _data.Length-8);
            Debug.Log("M");

            //System.Array.Copy(_data, 8, mesRes, 0, _data.Length);


            //string newMessage = System.Text.Encoding.Default.GetString(mesRes);
            Debug.Log("Ai: " + Encoding.ASCII.GetString(mesRes) + " !");
           // Debug.Log("Message conv: ");


            readStream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
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
    /// Start sending process (request var information)
    /// </summary>
    /// <param name="var"></param>
    /// <param name="value"></param>
    /// <param name="idMsg"></param>
    public void Send(string var,  int idMsg)
    {
        Debug.Log("startSending");

        int lunghezza, varNameLen;
        byte var_hByte, var_lByte, msg_hByte, msg_lByte;
        byte hByteMsg, lByteMsg;


        // Message ID ( MAX: 0xFFFF )
        hByteMsg = Byte.Parse(((idMsg & 0xff00) >> 8).ToString());
        lByteMsg = Byte.Parse((idMsg & 0x00ff).ToString());

        //var length
        varNameLen = var.Length;
        var_hByte =  Byte.Parse(((varNameLen & 0xff00) >> 8).ToString());
        var_lByte = Byte.Parse((varNameLen & 0x00ff).ToString());

        lunghezza = 2 + 1 + varNameLen;
        msg_hByte = Byte.Parse(((lunghezza & 0xff00) >> 8).ToString());
        msg_lByte = Byte.Parse((lunghezza & 0x00ff).ToString());



        //var = "$POS_ACT"; //To remove

        byte[] buffer =  {
            hByteMsg, //message ID
            lByteMsg, //message ID
            msg_hByte, //MSG length
            msg_lByte, //MSG length
            0x00, //0 - read, 1 - write
            var_hByte, //next var length
            var_lByte, //next var length
            };
        byte[] b= System.Text.Encoding.UTF8.GetBytes(var);

        byte[] finalMess = new byte[buffer.Length + b.Length];
        System.Buffer.BlockCopy(buffer, 0, finalMess, 0, buffer.Length);
        System.Buffer.BlockCopy(b, 0, finalMess, buffer.Length, b.Length);

        

        if (!this._socket.Connected)
        {
            Debug.Log("SocketnotConnected!");
            return;
        }

        //writeStream = this._socket.GetStream();
        //writeStream.BeginWrite(buffer, 0, buffer.Length, (asyncWriteResult) =>
        //{
        //        //Console.Write("w");
        //        writeStream.EndWrite(asyncWriteResult);
        //        //src.BeginRead(buffer, 0, buffer.Length, cbk, null);
        //    }, null);
        readStream.BeginWrite(finalMess, 0, finalMess.Length, WriteCallback, null);
    }
    /// <summary>
    /// End send process
    /// </summary>
    /// <param name="ia"></param>
    private void WriteCallback(IAsyncResult ia)
    {

        readStream.EndWrite(ia);
    }
    //}
}

