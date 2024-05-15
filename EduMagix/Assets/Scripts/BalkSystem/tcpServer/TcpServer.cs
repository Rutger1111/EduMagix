using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class TcpServer : MonoBehaviour
{
    TcpListener server = null;
    TcpClient client = null;
    NetworkStream stream = null;
    Thread thread;

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
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");
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

                    string response = "Server response: " + data.ToString();
                    SendMessageToClient(message: response);
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

    private void OnApplicationQuit()
    {
        stream.Close();
        client.Close();
        server.Stop();
        thread.Abort();
    }

    public void SendMessageToClient(string message)
    {
        byte[] msg = Encoding.UTF8.GetBytes(message);
        stream.Write(msg, 0, msg.Length);
        Debug.Log("Sent: " + message);
    }
}