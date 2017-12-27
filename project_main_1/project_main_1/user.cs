using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace project_main_1
{
    class user
    {
        const int port = 8008;
        const string address = "127.0.0.1";
        static public NetworkStream stream;
        static void Start()
        {
            TcpClient client = null;
            try
            {
                client = new TcpClient(address, port);
                stream = client.GetStream();
                Thread clientThread = new Thread(dispatch);
                clientThread.Start();
                Thread clientThread1 = new Thread(admission);
                clientThread1.Start();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static public void dispatch()
        {
            while (true)
            {
                // ввод сообщения
                string message = Console.ReadLine();
                message = String.Format(message);
                // преобразуем сообщение в массив байтов
                byte[] data = Encoding.Unicode.GetBytes(message);
                // отправка сообщения
                stream.Write(data, 0, data.Length);
            }
        }
        static public void admission()
        {
            while (true)
            {
                // получаем ответ
                byte[] data = new byte[64]; // буфер для получаемых данных
                StringBuilder builder = new StringBuilder();
                int bytes = 0;
                do
                {
                    bytes = stream.Read(data, 0, data.Length);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (stream.DataAvailable);
                Console.WriteLine("Сервер: {0}", builder.ToString());
            }
        }
    }
}
