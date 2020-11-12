using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;

namespace Client 
{
    public class ClientConnect : MonoBehaviour
    {
        [SerializeField] private Text _result;

        // адрес и порт сервера, к которому будем подключаться
        private int _port = 8005; // порт сервера
        private string _address = "109.87.31.123"; // адрес сервера ("192.168.1.105")

        // Start is called before the first frame update
        void Start()
        {

        }

        public void OnConnect()
        {
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(_address), _port);

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // подключаемся к удаленному хосту
                socket.Connect(ipPoint);
                //Console.Write("Введите сообщение:");
                string message = "Unity Connect";
                byte[] data = Encoding.Unicode.GetBytes(message);
                socket.Send(data);

                // получаем ответ
                data = new byte[256]; // буфер для ответа
                StringBuilder builder = new StringBuilder();
                int bytes = 0; // количество полученных байт

                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (socket.Available > 0);
                _result.text = "ответ сервера: " + builder.ToString();

                // закрываем сокет
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception ex)
            {
                _result.text = ex.Message;
            }
            Console.Read();

        }
    }
}