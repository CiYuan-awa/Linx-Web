﻿//------------------------------------------------------------------------------
// <auto-generated>
//	 此代码由工具生成。
//	 运行时版本:4.0.30319.42000
//
//	 对此文件的更改可能会导致不正确的行为，并且如果
//	 重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Linx_Web {
	using System;
	
	
	/// <summary>
	///   一个强类型的资源类，用于查找本地化的字符串等。
	/// </summary>
	// 此类是由 StronglyTypedResourceBuilder
	// 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
	// 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
	// (以 /str 作为命令选项)，或重新生成 VS 项目。
	[global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
	[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
	[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
	internal class Resources_zh_CN_ {
		
		private static global::System.Resources.ResourceManager resourceMan;
		
		private static global::System.Globalization.CultureInfo resourceCulture;
		
		[global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		internal Resources_zh_CN_() {
		}
		
		/// <summary>
		///   返回此类使用的缓存的 ResourceManager 实例。
		/// </summary>
		[global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
		internal static global::System.Resources.ResourceManager ResourceManager {
			get {
				if (object.ReferenceEquals(resourceMan, null)) {
					global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Linx_Web.Resources.zh-CN!", typeof(Resources_zh_CN_).Assembly);
					resourceMan = temp;
				}
				return resourceMan;
			}
		}
		
		/// <summary>
		///   重写当前线程的 CurrentUICulture 属性，对
		///   使用此强类型资源类的所有资源查找执行重写。
		/// </summary>
		[global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
		internal static global::System.Globalization.CultureInfo Culture {
			get {
				return resourceCulture;
			}
			set {
				resourceCulture = value;
			}
		}
		
		/// <summary>
		///   查找类似 404 文件未找到 的本地化字符串。
		/// </summary>
		internal static string _404NotFound {
			get {
				return ResourceManager.GetString("404NotFound", resourceCulture);
			}
		}
		
		/// <summary>
		///   查找类似 有客户端连接： 的本地化字符串。
		/// </summary>
		internal static string ClientConnected {
			get {
				return ResourceManager.GetString("ClientConnected", resourceCulture);
			}
		}
		
		/// <summary>
		///   查找类似 内容长度： 的本地化字符串。
		/// </summary>
		internal static string ContentLength {
			get {
				return ResourceManager.GetString("ContentLength", resourceCulture);
			}
		}
		
		/// <summary>
		///   查找类似 当前IP： 的本地化字符串。
		/// </summary>
		internal static string CurrentIP {
			get {
				return ResourceManager.GetString("CurrentIP", resourceCulture);
			}
		}
		
		/// <summary>
		///   查找类似 发生错误： 的本地化字符串。
		/// </summary>
		internal static string ErrorOccurred {
			get {
				return ResourceManager.GetString("ErrorOccurred", resourceCulture);
			}
		}
		
		/// <summary>
		///   查找类似 请求的文件不存在！ 的本地化字符串。
		/// </summary>
		internal static string FileDoesNotExist {
			get {
				return ResourceManager.GetString("FileDoesNotExist", resourceCulture);
			}
		}
		
		/// <summary>
		///   查找类似 服务器根目录 的本地化字符串。
		/// </summary>
		internal static string HTMLContent {
			get {
				return ResourceManager.GetString("HTMLContent", resourceCulture);
			}
		}
		
		/// <summary>
		///   查找类似 HXLyxx - Super 文件服务器 的本地化字符串。
		/// </summary>
		internal static string HTMLTitle {
			get {
				return ResourceManager.GetString("HTMLTitle", resourceCulture);
			}
		}
		
		/// <summary>
		///   查找类似 正在加载资源： 的本地化字符串。
		/// </summary>
		internal static string LoadingResource {
			get {
				return ResourceManager.GetString("LoadingResource", resourceCulture);
			}
		}
		
		/// <summary>
		///   查找类似 正在发送响应 的本地化字符串。
		/// </summary>
		internal static string SendingResponse {
			get {
				return ResourceManager.GetString("SendingResponse", resourceCulture);
			}
		}
		
		/// <summary>
		///   查找类似 服务器已在这个端口上启动：   的本地化字符串。
		/// </summary>
		internal static string ServerStarted {
			get {
				return ResourceManager.GetString("ServerStarted", resourceCulture);
			}
		}
		
		/// <summary>
		///   查找类似 服务器正在启动... 的本地化字符串。
		/// </summary>
		internal static string ServerStarting {
			get {
				return ResourceManager.GetString("ServerStarting", resourceCulture);
			}
		}
		
		/// <summary>
		///   查找类似 正在等待客户端连接... 的本地化字符串。
		/// </summary>
		internal static string WaitingClients {
			get {
				return ResourceManager.GetString("WaitingClients", resourceCulture);
			}
		}
	}
}