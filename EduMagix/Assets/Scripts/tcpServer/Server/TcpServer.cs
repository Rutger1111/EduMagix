using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using System.Collections.Generic;
using System.Collections.Concurrent;

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

    private void Start()
    {
        thread = new Thread(new ThreadStart(SetupServer));
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
        string ip = new WebClient().DownloadString("http://icanhazip.com/");
        Debug.Log(ip);
        IPAddress localAddr = IPAddress.Parse("192.168.68.128");
        server = new TcpListener(localAddr, 992);
        server.Start();

        byte[] buffer = new byte[1024];
        string data = null;
        try
        {
            while (true)
            {
                Debug.Log("Waiting for connection...");
                client = server.AcceptTcpClient();
                Debug.Log("Connected!");
                data = null;
                stream = client.GetStream();

                int i;

                while ((i = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
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
                client.Close();
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
    public void ResponceToClient(string message)
    {
        SendMessageToClient(message);
    }
    private void OnApplicationQuit()
    {

    }

    public void SendMessageToClient(string message)
    {
        byte[] msg = Encoding.UTF8.GetBytes(message);
        stream.Write(msg, 0, msg.Length);
        Debug.Log("Sent: " + message);
    }

    public void Dispose()
    {
        stream.Close();
        client.Close();
        server.Stop();
        thread.Abort();
    }
}