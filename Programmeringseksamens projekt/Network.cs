using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Programmeringseksamens_projekt
{
	internal class Network
	{
		private TcpListener Listener;
		private TcpClient Client;
		private NetworkStream Stream;
		public bool IsStarted { get; private set; } = false;
		public Network()
		{

		}

		public async Task StartServer()
		{
			Listener = new TcpListener(IPAddress.Any, 2026);
			Listener.Start();
			IsStarted = true;

			Debug.WriteLine("Waiting for connection...");
			Client = await Listener.AcceptTcpClientAsync();
			Stream = Client.GetStream();

			Debug.WriteLine("Client connected.");
			return;
		}

		public async Task Connect(string ip)
		{
			Client = new TcpClient();
			await Client.ConnectAsync(ip, 2026);

			Stream = Client.GetStream();
			IsStarted = true;

			Debug.WriteLine("Connected to server.");
			return;
		}

		public async Task Send(byte[] data)
		{
			if (Stream == null)
			{
				return;
			}

			byte[] magicBuffer = Encoding.UTF8.GetBytes("MAGIC");
			byte[] lengthBuffer = BitConverter.GetBytes(data.Length);

			byte[] combinedBuffer = new byte[9 + data.Length];
			magicBuffer.CopyTo(combinedBuffer, 0);
			lengthBuffer.CopyTo(combinedBuffer, 5);
			data.CopyTo(combinedBuffer, 9);

			await Stream.WriteAsync(combinedBuffer, 0, data.Length + 9);
		}

		public async Task<byte[]> Read()
		{
			Debug.WriteLine("Began read");
			if (Stream == null)
			{
				return new byte[] { };
			}

			byte[] magicBuffer = new byte[5];
			await Stream.ReadAsync(magicBuffer, 0, 5);
			if (!magicBuffer.SequenceEqual(Encoding.UTF8.GetBytes("MAGIC")))
			{
				Debug.WriteLine("Didn't find MAGIC bytes");
				Debug.WriteLine(magicBuffer[0] + " " + magicBuffer[1] + " " + magicBuffer[2] + " " + magicBuffer[3] + " " + magicBuffer[4]);
				return new byte[0];
			}

			byte[] lengthBuffer = new byte[4];
			await Stream.ReadAsync(lengthBuffer, 0, 4);
			int length = BitConverter.ToInt32(lengthBuffer, 0);
			Debug.WriteLine("Received a message with length " + length);

			byte[] buffer = new byte[length];
			int totalBytesRead = 0;
			while (totalBytesRead < length)
			{
				int bytesRead = await Stream.ReadAsync(
					buffer,
					totalBytesRead,
					buffer.Length - totalBytesRead
				);
				Debug.WriteLine("Read " + bytesRead + " bytes");

				totalBytesRead += bytesRead;
			}

			return buffer;
		}

		public bool BytesAvailable()
		{
			if (!IsStarted) return false;
			if (Stream == null) return false;

			return Stream.DataAvailable;
		}
	}
}
