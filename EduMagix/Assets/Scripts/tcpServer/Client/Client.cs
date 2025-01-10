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
    TcpClient client;
    private NetworkStream stream;
    private Thread clientReceiveThread;
    public ClientHandler clientHandler;
    public ISliderSetUpCommand sliderSetUpCommand;
    DebugTextCollector textCollector;


    private float delay;
    private float timer;
    void Start()
    {
        delay = 1.0f;
        timer = 0.0f;
        sliderSetUpCommand = GetComponent<ISliderSetUpCommand>();
        textCollector = DebugTextCollector.GetTextCollector();
        //serverIP = new WebClient().DownloadString("http://icanhazip.com/");
        //ConnectToServer();
    }

    void Update()
    {

        timer += Time.deltaTime;
        if(timer > delay){
            if(client == null){
                textCollector.AddDebugText("Connecting");
                ConnectToServer();
            }
            delay += 0.25f;
            timer = 0;
        }
    }

    void ConnectToServer()
    {
        uPnPHelper.DebugMode = true;
        uPnPHelper.LogErrors = true;
        uPnPHelper.Start(uPnPHelper.Protocol.TCP, 33435, 0, "Unity uPnP Port Forward Test.");
        textCollector.AddDebugText("called ConnectToSever");
        try
        {
            textCollector.AddDebugText("komtHIer");
            //textCollector.AddDebugText((setUpData.SavedDatas[0].IP));
            /*
            if (setUpData.SavedDatas[0].IP == "" || setUpData.SavedDatas[0].IP == null)
            {
                textCollector.AddDebugText("didnt see ip");
                string strHostName = "";
                strHostName = System.Net.Dns.GetHostName();
                var ipEntry = Dns.GetHostEntry(strHostName);
                var addr = ipEntry.AddressList;

            }
            textCollector.AddDebugText(setUpData.SavedDatas[0].IP);
            */
            textCollector.AddDebugText("KomtHIer bij de client = new client");
            client = new TcpClient("192.168.2.12", 33435);
            stream = client.GetStream();
            textCollector.AddDebugText("Connected to server.");

            clientReceiveThread = new Thread(new ThreadStart(ListenForData));
            clientReceiveThread.IsBackground = true;
            clientReceiveThread.Start();
        }
        catch (SocketException e)
        {
            textCollector.AddDebugText("SocketException: " + e.ToString());
        }
    }

    private void ListenForData()
    {
        textCollector.AddDebugText("client" + client);
        textCollector.AddDebugText("stream" + stream);
        try
        {
            byte[] bytes = new byte[4096];
            while (true)
            {

                // Check if there's any data available on the network stream
                if (stream.DataAvailable)
                {
                    int length;
                    print("ontvangen");
                    textCollector.AddDebugText("komt binnen");
                    // Read incoming stream into byte array.
                    while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        var incomingData = new byte[length];
                        Array.Copy(bytes, 0, incomingData, 0, length);
                        textCollector.AddDebugText("" + length);
                        // Convert byte array to string message.
                        if (incomingData.Length < 10){
                            string serverMessage = Encoding.UTF8.GetString(incomingData);
                            clientHandler.responceToServerMessage(serverMessage);
                            textCollector.AddDebugText("Server message received: " + serverMessage);                            
                        }
                        // if bigger then normal variables then convert to class
                        else{
                            textCollector.AddDebugText("dataclasss");
                            Data data = new Decryptor().DeserializeDB(bytes);
                            textCollector.AddDebugText("klaspunten" + data.currentAmountOfPoints);
                            ListOfData.GetListOfData().AddData(data);
                            clientHandler.datas.Add(data);
                            clientHandler.baseCommandClasses.Add(sliderSetUpCommand);
                            SendMessageToServer("Received");
                            textCollector.AddDebugText("sendtMessage");
                        }
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            textCollector.AddDebugText("Socket exception: " + socketException);
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