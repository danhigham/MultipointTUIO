using System;
using System.Net;
using System.Xml;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using System.Text;

namespace OSC.NET
{
    public delegate void ClientConnectedEventHandler(object sender);

    public class TcpServer
    {
        private TcpListener tcpListener;
        private Thread listenThread;

        private List<TcpClient> clients;

        public event ClientConnectedEventHandler TCPClientConnected;

        public TcpServer(int port)
        {
            clients = new List<TcpClient>();
            
            this.tcpListener = new TcpListener(IPAddress.Loopback, port);
            this.listenThread = new Thread(new ThreadStart(ListenForClients));
            this.listenThread.Start();
        }

        private void ListenForClients()
        {
            this.tcpListener.Start();

            while (true)
            {
                //blocks until a client has connected to the server

               
                    TcpClient newClient = this.tcpListener.AcceptTcpClient();

                    if (TCPClientConnected != null) TCPClientConnected(newClient);

                    clients.Add(newClient);
                    Console.Write("Client connected");
               
                
            }
        }

        public void Close()
        {
            foreach (TcpClient client in clients)
            {
                client.Close();
            }


            try
            {
                this.tcpListener.Stop();
                this.listenThread.Abort();
            }  
            catch (Exception e)
            {
                //The blocking call AcceptTcpClient borks when the process is killed!
                Console.Write(e.Message);
            }
        }

        public void Broadcast(XmlDocument XmlMessage)
        {
            foreach (TcpClient client in clients)
            {
                //Change the port parameter on the OSCPacket node 
                XmlMessage.DocumentElement.Attributes["PORT"].Value = ((IPEndPoint)client.Client.RemoteEndPoint).Port.ToString();

                String message = XmlMessage.InnerXml;
                message += "\0"; //Arrrgggghhhh! Flash is teh ghey!

                NetworkStream clientStream = client.GetStream();
                ASCIIEncoding encoder = new ASCIIEncoding();
                byte[] buffer = encoder.GetBytes(message);

                clientStream.Write(buffer, 0, buffer.Length);
                clientStream.Flush();
            }
        }
    }

	/// <summary>
	/// OSCTransmitter
	/// </summary>
	public class OSCTransmitter
	{
		protected UdpClient udpClient;
        protected TcpServer tcpServer;
		protected string remoteHost;
		protected int remotePort;

        protected bool portIsTcp;

        public event ClientConnectedEventHandler TCPClientConnected;

		public OSCTransmitter(string remoteHost, int remotePort)
		{
			this.remoteHost = remoteHost;
			this.remotePort = remotePort;
			Connect(false);
		}

        public OSCTransmitter(string remoteHost, int remotePort, bool tcp)
        {
            this.remoteHost = remoteHost;
            this.remotePort = remotePort;
            Connect(tcp);
        }

		public void Connect(bool tcp)
		{
            portIsTcp = tcp;
            if (!tcp)
            {
                if (this.udpClient != null) Close();
                this.udpClient = new UdpClient(this.remoteHost, this.remotePort);
            }
            else
            {
                this.tcpServer = new TcpServer(this.remotePort);  
                tcpServer.TCPClientConnected+=new ClientConnectedEventHandler(tcpServer_TCPClientConnected);
                
            }
		}

        void tcpServer_TCPClientConnected(object sender)
        {
            if (TCPClientConnected != null) TCPClientConnected(sender);
        }

		public void Close()
		{
            if (!portIsTcp)
            {
                this.udpClient.Close();
                this.udpClient = null;
            }
            else
            {
                this.tcpServer.Close();
                this.tcpServer = null;
            }
		}

		public int Send(OSCPacket packet)
		{
			int byteNum = 0;
            byte[] data;
			try 
			{
                if (!portIsTcp)
                {
                    data = packet.BinaryData;
                    byteNum = this.udpClient.Send(data, data.Length);
                }
                else
                {
                    OSCBundle bundle = packet as OSCBundle;

                    if (bundle != null)
                    {
                        foreach (var value in bundle.Values)
                        {
                            if (value is OSCMessage)
                            {
                                OSCMessage message = value as OSCMessage;
                                XmlElement xmlMessage = message.ToXml;

                                //<OSCPACKET ADDRESS="127.0.0.1" PORT="49178" TIME="1">
                                XmlDocument xDoc = new XmlDocument();
                                XmlElement xmlPacket = xDoc.CreateElement("OSCPACKET");

                                XmlAttribute address = xDoc.CreateAttribute("ADDRESS");
                                address.Value = "127.0.0.1";

                                XmlAttribute port = xDoc.CreateAttribute("PORT");
                                port.Value = "0";

                                XmlAttribute time = xDoc.CreateAttribute("TIME");
                                time.Value = "0";

                                xmlPacket.Attributes.Append(address);
                                xmlPacket.Attributes.Append(port);
                                xmlPacket.Attributes.Append(time);


                                xDoc.AppendChild(xmlPacket);

                                xmlPacket.AppendChild(xDoc.ImportNode(xmlMessage, true));
                                String xml = xDoc.InnerXml;
                                //Console.Write(xml);
                                this.tcpServer.Broadcast(xDoc);
                            }
                        }
                    }
                }
			}
			catch (Exception e)
			{
				Console.Error.WriteLine(e.Message);
				Console.Error.WriteLine(e.StackTrace);
			}

			return byteNum;
		}

	}
}
