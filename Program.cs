using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Resources;

namespace Linx_Web.FileServer
{
	class Program
	{
		private const string ResourcesPath = "D:/Resources";
		private const int MaxConcurrentRequests = 100;
		private static ushort Port = 1012;
		private static bool IsRootPath = false;
		private static readonly ushort[] UnSafePorts = {1, 7, 9, 11, 13, 15, 17, 19, 20, 21, 22, 23, 25, 37, 42, 43, 53, 77, 79, 87, 95, 101, 102, 103, 104, 109, 110, 111, 113, 115, 117, 119, 123, 135, 139, 143, 179, 389, 465, 512, 513, 514, 515, 526, 530, 531, 532, 540, 556, 563, 587, 601, 636, 993, 995, 2049, 3659, 4045, 6000, 6665, 6666, 6667, 6668, 6669};

		private static readonly ConcurrentDictionary<string, byte[]> ResourceCache = new();
		private static readonly ResourceManager Language_Resources = new ("Linx_Web.FileServer.Resources", typeof(Program).Assembly);

		static void Main(string[] args)
		{
			if (args.Length > 0) _ = ushort.TryParse(args[0], out Port);
			if (Port == 0) Port++;
			if (Port == 65535) Port = 1;
			foreach (var UnSafePort in UnSafePorts)
			{
				if (Port == 0) Port++;
				if (Port == 65535) Port = 1;
				if (Port == UnSafePort) Port++;
			}
			while (true)
			{
				Logger.Info(Language_Resources.GetString("ServerStarting")!);
				var LocalEndPoint = new IPEndPoint(IPAddress.Parse("0.0.0.0"), Port);
				var Listener = new TcpListener(LocalEndPoint);
				try
				{
					Listener.Start();
					IPAddress? LocalIPAddress = null;
					string HostName = Dns.GetHostName();
					IPHostEntry IPHostInfo = Dns.GetHostEntry(HostName);
					foreach (IPAddress Address in IPHostInfo.AddressList)
					{
						if (Address.AddressFamily == AddressFamily.InterNetwork)
						{
							LocalIPAddress = Address;
							break;
						}
					}
					string LocalIp = LocalIPAddress.ToString();
					Logger.Info(Language_Resources.GetString("CurrentIP") + LocalIp);
					Logger.Info(Language_Resources.GetString("ServerStarted") + Port + ".");


					var semaphore = new Semaphore(MaxConcurrentRequests, MaxConcurrentRequests);

					while (true)
					{
						Logger.Info(Language_Resources.GetString("WaitingClients")!);

						semaphore.WaitOne();

						var client = Listener.AcceptTcpClient();

						Logger.Info(Language_Resources.GetString("ClientConnected") + client.Client.RemoteEndPoint);

						ThreadPool.QueueUserWorkItem(HandleRequest, new RequestState(client, semaphore));						
					}
				}
				catch (SocketException)
				{
					Port++;
				}
				catch (Exception e)
				{
					Logger.Err(Language_Resources.GetString("ErrorOccurred") + e);
				}
				finally
				{
					Listener.Stop();
				}
			}
		}
		
		private static void HandleRequest(object State)
		{
			var Request = (RequestState)State;
			var Client = Request.Client;
			var Sign = Request.Semaphore;

			try
			{
				var RequestStream = Client.GetStream();
				var Reader = new StreamReader(RequestStream);
				var RequestLine = Reader.ReadLine();

				if (RequestLine != null)
				{
					var Tokens = RequestLine.Split(' ');
					var Method = Tokens[0];
					var Path = Tokens[1];

					if (Method == "GET")
					{
						if (Path == "/")
						{
							IsRootPath = true;
							Logger.Info(Language_Resources.GetString("IsRootPath")!);
							//Logger.Info(ColorType.Aqua, "Debug", IsRootPath);
							var FilesHtml = GetFilesHtml();
							var ContentBytes = Encoding.UTF8.GetBytes(FilesHtml);
							SendResponse(Client, ContentBytes);
						}
						if (ResourceCache.TryGetValue(Path, out var ResourceBytes))
						{
							SendResponse(Client, ResourceBytes);
						}
						else
						{
							ResourceBytes = LoadResource(Path);
							ResourceCache.TryAdd(Path, ResourceBytes);
							SendResponse(Client, ResourceBytes);
						}
						
					}
				}

				Client.Close();
			}
			catch (Exception e)
			{
				Logger.Err(Language_Resources.GetString("ErrorOccurred") + e.Message);
			}
			finally
			{
				Sign.Release();
			}
		}
		private static string GetFilesHtml()
		{
			var Website = new StringBuilder();
			Website.Append("<html>");
			Website.Append("<meta charset=\"UTF-8\">");
			Website.Append($"<head><title>{Language_Resources.GetString("HTMLTitle")}</title></head>");
			Website.Append("<body>");
			Website.Append($"<h2>{Language_Resources.GetString("HTMLContent")}</h2>");
			Website.Append("<ul>");

			var RootPath = Path.Combine(ResourcesPath);
			var Files = Directory.GetFiles(RootPath);
			foreach (var File in Files)
			{
				var FileName = Path.GetFileName(File);
				Website.AppendFormat("<li><a href=\"/{0}\">{1}</a></li>", FileName, FileName);
			}

			Website.Append("</ul>");
			Website.Append("</body>");
			Website.Append("</html>");

			return Website.ToString();
		}

		private static byte[] LoadResource(string RelativePath)
		{
			string NotFoundMessage;
			var FullPath = Path.Combine(ResourcesPath, RelativePath.TrimStart('/'));

			Logger.Info(Language_Resources.GetString("LoadingResource") + FullPath);

			byte[] Content;
			if (File.Exists(FullPath))
			{
				Logger.Info($"{FullPath} {Language_Resources.GetString("ReadyToTransfer")}");
				using FileStream Transfer = new(FullPath, FileMode.Open, FileAccess.Read);
				using BinaryReader Reader = new(Transfer);
				Logger.Info($"{FullPath} {Language_Resources.GetString("FileIsTransferring")}");
				Content = Reader.ReadBytes((int)Transfer.Length);
				Logger.Info($"{FullPath} {Language_Resources.GetString("FileTransferred")}");
				IsRootPath = false;
			}
			else if (IsRootPath)
			{
				//Logger.Info(ColorType.Aqua, "Debug", IsRootPath);
				Content = Encoding.UTF8.GetBytes(string.Empty);
				IsRootPath = false;
			}
			else
			{
				//Logger.Info(ColorType.Aqua, "Debug", IsRootPath);
				Logger.Warn($"{FullPath} {Language_Resources.GetString("FileDoesNotExist")}");
				NotFoundMessage = $"<html><meta charset=\"UTF-8\"><head><title>404</title></head><body><h1>{Language_Resources.GetString("404NotFound")}</h1><br><h2>{RelativePath}</h2></body></html>";
				Content = Encoding.UTF8.GetBytes(NotFoundMessage);
				IsRootPath = false;
			}
			return Content;


		}

		private static void SendResponse(TcpClient Client, byte[] ResourceBytes)
		{
			var ETag = CalculateEtag(ResourceBytes);

			Logger.Info($"{Language_Resources.GetString("SendingResponse")}");
			Logger.Info("ETag", ETag);

			var ResponseStream = Client.GetStream();
			var Writer = new StreamWriter(ResponseStream);

			Writer.WriteLine("HTTP/1.1 200 OK");
			Writer.WriteLine();
			Writer.Flush();

			ResponseStream.Write(ResourceBytes, 0, ResourceBytes.Length);
		}

		private static string CalculateEtag(byte[] Data)
		{
			using var Sha256 = SHA256.Create();
			var Hash = Sha256.ComputeHash(Data);
			return Convert.ToBase64String(Hash);
		}

		private class RequestState
		{
			public TcpClient Client { get; }
			public Semaphore Semaphore { get; }

			public RequestState(TcpClient client, Semaphore semaphore)
			{
				Client = client;
				Semaphore = semaphore;
			}
		}
	}
}