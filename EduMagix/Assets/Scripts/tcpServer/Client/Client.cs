using UnityEngine;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Net;
using System.IO;

public class Client : MonoBehaviour
{
    public string serverIP = "127.0.0.1"; // Set this to your server's IP address.
    public int serverPort = 33435;             // Set this to your server's port.
    private string messageToSend = "Hello Server!"; // The message to send.
    public SetUpData setUpData;
    private TcpClient client;
    private NetworkStream stream;
    private Thread clientReceiveThread;
    public ClientHandler clientHandler;

    void Start()
    {
        //serverIP = new WebClient().DownloadString("http://icanhazip.com/");
        ConnectToServer();
    }

    void Update()
    {

    }

    void ConnectToServer()
    {
        try
        {
            print(setUpData.SavedDatas[0].IP);
            if (setUpData.SavedDatas[0].IP == "")
            {
                string strHostName = "";
                strHostName = System.Net.Dns.GetHostName();
                var ipEntry = Dns.GetHostEntry(strHostName);
                var addr = ipEntry.AddressList;

            }
            Debug.Log(setUpData.SavedDatas[0].IP);
            client = new TcpClient(setUpData.SavedDatas[0].IP, serverPort);
            stream = client.GetStream();
            Debug.Log("Connected to server.");

            clientReceiveThread = new Thread(new ThreadStart(ListenForData));
            clientReceiveThread.IsBackground = true;
            clientReceiveThread.Start();
        }
        catch (SocketException e)
        {
            Debug.LogError("SocketException: " + e.ToString());
        }
    }

    private void ListenForData()
    {


        try
        {
            byte[] bytes = new byte[1024000];
            while (true)
            {

                // Check if there's any data available on the network stream
                if (stream.DataAvailable)
                {
                    int length;
                    print("ontvangen");
                    SendMessageToServer("komt binnen");
                    // Read incoming stream into byte array.
                    while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        var incomingData = new byte[length];
                        Array.Copy(bytes, 0, incomingData, 0, length);
                        print (length);
                        // Convert byte array to string message.
                        if (incomingData.Length < 50){
                            string serverMessage = Encoding.UTF8.GetString(incomingData);
                            clientHandler.responceToServerMessage(serverMessage);
                            Debug.Log("Server message received: " + serverMessage);                            
                        }
                        else{
                            print("dataclasss");
                            Data data = new Decryptor().DeserializeDB(bytes);
                            clientHandler.regristerNewKlas(data);
                        }
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }
    public void ResponceToClient(string message)
    {
        SendMessageToServer(message);
    }
    public void SendMessageToServer(string message)
    {
        if (client == null || !client.Connected)
        {
            Debug.LogError("Client not connected to server.");
            return;
        }

        byte[] data = Encoding.UTF8.GetBytes(message);
        stream.Write(data, 0, data.Length);
        Debug.Log("Sent message to server: " + message);
    }

    void OnApplicationQuit()
    {
        if (stream != null)
            stream.Close();
        if (client != null)
            client.Close();
        if (clientReceiveThread != null)
            clientReceiveThread.Abort();
    }
}