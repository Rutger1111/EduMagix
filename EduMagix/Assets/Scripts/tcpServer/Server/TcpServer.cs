using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using System.Collections.Generic;
using System.Collections.Concurrent;
using static UnityEngine.EventSystems.EventTrigger;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using Unity.VisualScripting;

public class TcpServer : MonoBehaviour, IDisposable
{

    TcpListener server = null;
    TcpClient client = null;
    NetworkStream stream = null;
    Thread thread;
    public List<TcpClient> Clients;
    public List<bool> ClientIsAnsweringNumber;
    public List<bool> ClientIsAnsweringString;
    public Serverhandler serverhandler;
    public SetUpData setUpData;
    public DebugTextCollector textCollector;
    public PortForwarder portForwarder;
    private void Start()
    {
        textCollector = DebugTextCollector.GetTextCollector();
        thread = new Thread(new ThreadStart(SetupServer));
        uPnPHelper.DebugMode = true;
        uPnPHelper.LogErrors = true;
        uPnPHelper.Start(uPnPHelper.Protocol.TCP, 33435, 0, "Unity uPnP Port Forward Test.");
        thread.Start();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SendMessageToClient("Hello");
        }
    }

    private void SetupServer()
    {
//        string ip = new WebClient().DownloadString("http://icanhazip.com/");
        string strHostName = "Server";
        strHostName = System.Net.Dns.GetHostName();
        var ipEntry =Dns.GetHostEntry(strHostName);
        var addr = ipEntry.AddressList;
        
        FileHandler fileHandler = FileHandler.GetFileHandler();
        
        string ipTest = setUpData.SavedDatas[0].IP;
        textCollector.AddDebugText(ipTest);
        IPAddress localAddr = IPAddress.Parse(addr[addr.Length - 1].ToString());
        textCollector.AddDebugText("used" +IPAddress.Parse(addr[addr.Length - 1].ToString()) + "hoi");
        server = new TcpListener(localAddr, 33435);
        server.Start();

        try
        {
            while (true)
            {
                if (client == null){
                    GetClient();
                }
                else if(client != null){
                    ReadClientStream();
                }
            }
        }
        catch (SocketException e)
        {
            Debug.Log("SocketException: " + e);
        }
        finally
        {
            server.Stop();
        }
    }
    public void GetClient(){
        try{
            //textCollector.AddDebugText("accepting client");
            client = server.AcceptTcpClient();
        }
        catch(Exception ex){
            textCollector.AddDebugText("accepting failed"+ex);
        }

        textCollector.AddDebugText("client =" + client);
        textCollector.AddDebugText("Connected!");
        try{    
            textCollector.AddDebugText("trying getstream");
            stream = client.GetStream();
            SendMessageToClient("connected");
        }
        catch(Exception ex){
            textCollector.AddDebugText("getting stream failed"+ex);
        }
    }
    public void ReadClientStream(){
        byte[] buffer = new byte[10024];
        string data = null;
        int i;
        int read = 0;
        while ((i = stream.Read(buffer, read, buffer.Length - read)) != 0)
        {
            read += i;
            print(i);
            data = Encoding.UTF8.GetString(buffer, 0, i);
            Debug.Log("Received: " + data);
            /*
            for (int Clienti = 0; Clienti < Clients.Count; Clienti ++)
            {
                
                if (Clients[Clienti] == client)
                {
                    if(ClientIsAnsweringNumber[Clienti] == true)
                    {
                        
                        serverhandler.CommandAddPoints.amount = int.Parse(data);
                        serverhandler.CommandAddPoints.Invoke();
                        
                    }
                    else if (ClientIsAnsweringString[Clienti] == true)
                    {

                    }
                }
            }
            */
            serverhandler.responceToServerMessage(data);
            Debug.Log("Server response: " + data.ToString());
        }
        Debug.Log("GaatVoorbijWhileLoop");
        //client.Close();
    }
    public void ResponceToClient(string message)
    {
        SendMessageToClient(message);
    }
    private void OnApplicationQuit()
    {
        Dispose();
    }

    public void SendMessageToClient(string message)
    {
        byte[] msg = Encoding.UTF8.GetBytes(message);
        stream.Write(msg, 0, msg.Length);
        Debug.Log("Sent: " + message);
    }
    public void SendDataToClient(byte[] bytes){
        textCollector.AddDebugText("client: " + client);
        print(stream);
        stream.Write(bytes,0, bytes.Length);
        textCollector.AddDebugText("written stream" + bytes);

    }

    public void Dispose()
    {
        stream.Close();
        client.Close();
        server.Stop();
        thread.Abort();
    }
}