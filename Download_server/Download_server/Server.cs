using System.Net;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System;

namespace Download_server
{
    class Server
    {
        string[] strfile;
        IPEndPoint ipsv;
        public IPEndPoint IPSV
        {
            get { return ipsv; }
        }
        Socket sksv;
        Socket client;
        public Socket SKSV
        {
            get { return sksv; }
        }

        public Server()
        {
            sksv = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ipsv = new IPEndPoint(IPAddress.Loopback, 1234);
            sksv.Bind(ipsv);
        }
        public void listen()
        {
            sksv.Listen(4);
            System.Console.WriteLine("dang doi ket noi tu client");
        }
        public void accept()
        {
            client = sksv.Accept();
            System.Console.WriteLine("chap nhan ket noi tu : {0}", client.RemoteEndPoint.ToString());
            string s = "Hello client";
            gui(s);
            Lietkefile();
        }
        public void Nhanfile()
        {
            try
            {
                bool check = true;
                while (check)
                {
                    byte[] nhan = new byte[1024];
                    int rec = client.Receive(nhan);
                    string snhan = Encoding.ASCII.GetString(nhan, 0, rec);
                    switch (snhan)
                    {
                        case "exit":
                            {
                                check = false;
                                client.Close();
                                //client.Shutdown(SocketShutdown.Both);
                                sksv.Close();
                            }
                            break;

                        case "text1.txt":
                            {
                                gui(readfile("text1.txt"));
                            }
                            break;
                        case "text2.txt":
                            {
                                gui(readfile("text2.txt"));
                            }
                            break;
                        case "text3.txt":
                            {
                                gui(readfile("text3.txt"));
                            }
                            break;
                        default:
                            {
                                Console.WriteLine("From client: " + snhan);
                            }
                            break;
                    }

                }
            }
            catch
            { Console.WriteLine("Loi!"); }
        }
        public void gui(string s)
        {
            byte[] gui = Encoding.ASCII.GetBytes(s);
            client.Send(gui);
        }
        public void Lietkefile()
        {
            DirectoryInfo d = new DirectoryInfo(@"filedowload\");
            FileInfo[] Files = d.GetFiles("*.txt");
            strfile = new string[Files.Length];
            for (int i = 0; i < Files.Length; i++)
            {
                strfile[i] = Files[i].ToString();
            }
            string mess = string.Join(" | ", strfile);
            mess = "Cac file ban co the download: " + mess;
            gui(mess);
        }
        public string readfile(string namefile)
        {
            string file = @"filedowload\" + namefile;
            string line = "";
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(file))
                {
                    // Read the stream to a string, and write the string to the console.
                    line = sr.ReadToEnd();
                    Console.WriteLine(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return line;
        }
    }
}
