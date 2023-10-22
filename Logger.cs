namespace Linx_Web.FileServer
{
	public class Logger
	{
		private static string GetTime()
		{
			return DateTime.Now.ToString("[HH:mm:ss.ffff] ");
		}
		private static string GetType(string Type)
		{
			return Type switch
			{
				"I" => "[Info] ",
				"W" => "[Warn] ",
				"E" => "[Err] ",
				_ => string.Empty,
			};
		}
		public static void UpdateScreen()
		{
			Console.SetCursorPosition(0, Console.CursorTop);
			SetColor(ColorType.Aqua);
			Console.Write(">>> ");
		}
		public static void Info<Template>(Template message)
		{
			Log("I", message);
		}
		public static void Info<Template>(Template prefix, Template message)
		{
			Log("I", prefix, message);
		}
		public static void Info<Template>(ColorType color, Template message)
		{
			Log("I", color, message);
		}
		public static void Info<Template>(ColorType color, Template prefix, Template message)
		{
			Log("I", color, prefix, message);
		}
		public static void Warn<Template>(Template message)
		{
			Log("W", message);
		}
		public static void Warn<Template>(Template prefix, Template message)
		{
			Log("W", prefix, message);
		}
		public static void Warn<Template>(ColorType color, Template message)
		{
			Log("W", color, message);
		}
		public static void Warn<Template>(ColorType color, Template prefix, Template message)
		{
			Log("W", color, prefix, message);
		}
		public static void Err<Template>(Template message)
		{
			Log("E", message);
		}
		public static void Err<Template>(Template prefix, Template message)
		{
			Log("E", prefix, message);
		}
		public static void Err<Template>(ColorType color, Template message)
		{
			Log("E", color, message);
		}
		public static void Err<Template>(ColorType color, Template prefix, Template message)
		{
			Log("E", color, prefix, message);
		}

		private static void Log<Template>(string type, Template message)
		{
			Console.ForegroundColor = type switch
			{
				"I" => ConsoleColor.Green,
				"W" => ConsoleColor.DarkYellow,
				"E" => ConsoleColor.Red,
				_ => Console.ForegroundColor
			};
			Console.WriteLine(GetTime() + GetType(type) + message);
		}
		private static void Log<Template>(string type, Template prefix, Template message)
		{
			Console.ForegroundColor = type switch
			{
				"I" => ConsoleColor.Green,
				"W" => ConsoleColor.DarkYellow,
				"E" => ConsoleColor.Red,
				_ => Console.ForegroundColor
			};
			Console.WriteLine(GetTime() + "[" + prefix + "] " + message);
		}
		private static void Log<Template>(string type, ColorType color, Template message)
		{
			SetColor(color);
			Console.WriteLine(GetTime() + GetType(type) + message);
		}
		private static void Log<Template>(string type, ColorType color, Template prefix, Template message)
		{
			SetColor(color);
			Console.WriteLine(GetTime() + "[" + prefix + "] " + message);
		}


		public static void SetColor(ColorType Color)
		{
			Console.ForegroundColor = Color switch
			{
				ColorType.Black => ConsoleColor.Black,
				ColorType.DarkBlue => ConsoleColor.DarkBlue,
				ColorType.DarkGreen => ConsoleColor.DarkGreen,
				ColorType.DarkAqua => ConsoleColor.DarkCyan,
				ColorType.DarkRed => ConsoleColor.DarkRed,
				ColorType.DarkMagenta => ConsoleColor.DarkMagenta,
				ColorType.Yellow => ConsoleColor.DarkYellow,
				ColorType.Gray => ConsoleColor.Gray,
				ColorType.DarkGray => ConsoleColor.DarkGray,
				ColorType.Blue => ConsoleColor.Blue,
				ColorType.Green => ConsoleColor.Green,
				ColorType.Aqua => ConsoleColor.Cyan,
				ColorType.Red => ConsoleColor.Red,
				ColorType.Magenta => ConsoleColor.Magenta,
				ColorType.LightYellow => ConsoleColor.Yellow,
				ColorType.White => ConsoleColor.White,
				_ => ConsoleColor.White,
			};
		}
	}
}
