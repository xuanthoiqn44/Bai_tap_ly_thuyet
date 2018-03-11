using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //tao socket , ipendpoin
            Client cl = new Client();
            //ket noi toi sever
            cl.Ketnoi();
            //nhan du lieu
            //gui du lieu
            string s;
            cl.nhan();
            do
            {
                Console.Write("Client: ");
                s = Console.ReadLine();
                cl.Gui(s);
                if (s == "text1.txt" || s == "text2.txt" || s == "text3.txt")
                {
                    //nhập đường đẫn
                    Console.Write("Nhap duong dan: ");
                    string sf = Console.ReadLine();
                    sf = sf + "\\" + s;
                    cl.CreateFIle(sf, cl.nhan());
                }
            } while (s != "exit");
        }
    }
}
