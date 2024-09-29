using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

class TCPServerOpg4
{
    static void Main(string[] args)
    {
        Console.WriteLine("TCP Server Opgave 4:");

        TcpListener listener = new TcpListener(IPAddress.Any, 7);
        listener.Start();

        while (true)
        {
            TcpClient socket = listener.AcceptTcpClient();
            IPEndPoint clientEndPoint = socket.Client.RemoteEndPoint as IPEndPoint;
            Console.WriteLine("Client connected: " + clientEndPoint.Address);

            Task.Run(() => HandleClient(socket));
        }
    }

    static void HandleClient(TcpClient socket)
    {
        using NetworkStream ns = socket.GetStream();
        using StreamReader reader = new StreamReader(ns);
        using StreamWriter writer = new StreamWriter(ns) { AutoFlush = true };

        while (socket.Connected)
        {
            string? command = reader.ReadLine();
            Console.WriteLine("Received: " + command);

            if (command == "Random")
            {
                writer.WriteLine("Input numbers");
                writer.Flush();
                string? input = reader.ReadLine();
                string[] parts = input?.Split(' ');
                if (parts.Length == 2 && int.TryParse(parts[0], out int num1) && int.TryParse(parts[1], out int num2))
                {
                    Random random = new Random();
                    int randomNumber = random.Next(num1, num2 + 1);
                    writer.WriteLine(randomNumber);
                }
                else
                {
                    writer.WriteLine("Invalid input format. Please provide two numbers.");
                }
                writer.Flush();
            }
            else if (command == "Add")
            {
                writer.WriteLine("Input numbers");
                writer.Flush();
                string? input = reader.ReadLine();
                string[] parts = input?.Split(' ');
                if (parts.Length == 2 && int.TryParse(parts[0], out int num1) && int.TryParse(parts[1], out int num2))
                {
                    int sum = num1 + num2;
                    writer.WriteLine(sum);
                }
                else
                {
                    writer.WriteLine("Invalid input format. Please provide two numbers.");
                }
                writer.Flush();
            }
            else if (command == "Subtract")
            {
                writer.WriteLine("Input numbers");
                writer.Flush();
                string? input = reader.ReadLine();
                string[] parts = input?.Split(' ');
                if (parts.Length == 2 && int.TryParse(parts[0], out int num1) && int.TryParse(parts[1], out int num2))
                {
                    int difference = num1 - num2;
                    writer.WriteLine(difference);
                }
                else
                {
                    writer.WriteLine("Invalid input format. Please provide two numbers.");
                }
                writer.Flush();
            }
            else
            {
                writer.WriteLine("Unknown command. Please use Random, Add, or Subtract.");
                writer.Flush();
            }
        }
    }
}
