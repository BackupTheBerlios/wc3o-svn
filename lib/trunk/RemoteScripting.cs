/*
 * Remote Scripting Class Library
 * 
 * Written by Alvaro Mendez
 * Copyright (c) 2004. All Rights Reserved.
 * 
 * The AMS.Web namespace contains web-related interfaces and classes 
 * that don't fall into any particular category.
 * This file contains the RemoteScripting classes.
 * 
 * The code is thoroughly documented, however, if you have any questions, 
 * feel free to email me at alvaromendez@consultant.com.  Also, if you 
 * decide to this in a commercial application I would appreciate an email 
 * message letting me know.
 *
 * This code may be used in compiled form in any way you desire. This
 * file may be redistributed unmodified by any means providing it is 
 * not sold for profit without the authors written consent, and 
 * providing that this notice and the authors name and all copyright 
 * notices remains intact. This file and the accompanying source code 
 * may not be hosted on a website or bulletin board without the author's 
 * written permission.
 * 
 * This file is provided "as is" with no expressed or implied warranty.
 * The author accepts no liability for any damage/loss of business that
 * this product may cause.
 *
 * Last Updated: Sept. 3, 2004
 */

using System;
using System.Web;
using System.Text;
using System.IO;
using System.Web.UI;
using System.Reflection;
using System.Diagnostics;

	/// <summary>
	///   Class that handles Remote Scripting.  Based on the remote web client, this class
	///   can be used to invoke a method on the Page. </summary>
	public class RemoteScripting {
		private RemoteScriptingClient m_client;

		/// <summary>
		///   Constructs the object and determines the type of remote client, if any. </summary>
		/// <param name="page">
		///   The object representing the web page where the client wants to invoke the method. </param>
		public RemoteScripting(Page page) {
			m_client = RemoteScriptingClient.Create(page);
		}

		/// <summary>
		///   Gets the object for the client that made the remote request, if any. </summary>
		/// <remarks>
		///   If the client requested to invoke a method, this property returns an object
		///   representing the client; otherwise it returns null. </remarks>
		public RemoteScriptingClient Client {
			get {
				return m_client;
			}
		}

		/// <summary>
		///   Invokes the method on the page object as requested by the web client, if any. </summary>
		/// <param name="page">
		///   The object representing the web page where the client wants to invoke the method. </param>
		/// <returns>
		///   If the client requested to invoke a method, the return value is true; otherwise it's false. </returns>
		/// <remarks>
		///   This method conveniently combines the creation of a RemoteScripting object and subsequently 
		///   calling its InvokeMethod instance method.  It first verifies that a remote call was made and 
		///   if so makes it, writes the result to the response, sends it back to the client, and then returns true. </remarks>
		public static bool InvokeMethod(Page page) {
			return InvokeMethod(page, true);
		}

		/// <summary>
		///   Invokes the method on the page object as requested by the web client, if any. </summary>
		/// <param name="page">
		///   The object representing the web page where the client wants to invoke the method. </param>
		/// <param name="endResponse">
		///   If true and the client requested to invoke a method, 
		///   Response.End will be called after the method is invoked. </param>
		/// <returns>
		///   If the client requested to invoke a method, the return value is true; otherwise it's false. </returns>
		/// <remarks>
		///   This method conveniently combines the creation of a RemoteScripting object and subsequently 
		///   calling its InvokeMethod instance method.  The endResponse parameter allows for the extra convenience of 
		///   causing the result to be sent back to the client immediately.  
		///   This method first verifies that a remote call was made and if so makes it, writes the result to 
		///   the response, and then returns true. </remarks>
		public static bool InvokeMethod(Page page, bool endResponse) {
			RemoteScripting rs = new RemoteScripting(page);
			return rs.InvokeMethod(endResponse);
		}

		/// <summary>
		///   Invokes the method on the page object as requested by the web client, if any. </summary>
		/// <returns>
		///   If the client requested to invoke a method, the return value is true; otherwise it's false. </returns>
		/// <remarks>
		///   This method first verifies that a remote call was made and 
		///   if so makes it, writes the result to the response, sends it back to the client, and then returns true. 
		///   The remote call is based on the Request parameters of the Page object passed to the constructor, which 
		///   is also used to write back the result via its Response property. </remarks>
		public bool InvokeMethod() {
			return InvokeMethod(true);
		}

		/// <summary>
		///   Invokes the method on the page object as requested by the web client, if any. </summary>
		/// <param name="endResponse">
		///   If true and the client requested to invoke a method, 
		///   Response.End will be called after the method is invoked. </param>
		/// <returns>
		///   If the client requested to invoke a method, the return value is true; otherwise it's false. </returns>
		/// <remarks>
		///   This method first verifies that a remote call was made and if so makes it, writes the result to 
		///   the response, and then returns true.  The remote call is based on
		///   the Request parameters of the Page object passed to the constructor, which 
		///   is also used to write back the result via its Response property. The endResponse 
		///   parameter allows for the extra convenience of causing the result to be sent back 
		///   to the client immediately.  </remarks>
		public bool InvokeMethod(bool endResponse) {
			if (m_client == null)
				return false;

			m_client.InvokeMethod(endResponse);
			return true;
		}
	}

	////////////////////////////////////////////////////////////////////////////////////////////////

	/// <summary>
	///   Class that represents a Remote Scripting client. </summary>
public abstract class RemoteScriptingClient {
	/// <summary> The object representing the web page where the client wants to invoke the method. </summary>
	protected Page m_page;

	/// <summary>
	///   Constructs the object and stores the page object internally. </summary>
	/// <param name="page">
	///   The object representing the web page where the client wants to invoke the method. </param>
	protected RemoteScriptingClient(Page page) {
		m_page = page;
	}

	/// <summary>
	///   Creates a client object based on the parameters passed to the page. </summary>
	/// <param name="page">
	///   The object representing the web page requested by the client. </param>
	/// <returns>
	///   If the client requested to invoke a method, the return value is an object representing it; 
	///   otherwise it's null. </returns>
	/// <remarks>
	///   This method determines if a client is making a remote call and if so what type 
	///   of client is is (RS, JS, or MS).  This is needed to properly interpret the
	///   request and then to respond to it.  </remarks>
	public static RemoteScriptingClient Create(Page page) {
		if (page.Request.Params["RC"] != null)
			return new RS(page);
		if (page.Request.Params["RS"] != null || page.Request.Params["C"] != null)
			return new JS(page);
		if (page.Request.Params["_method"] != null)
			return new MS(page);
		return null;
	}

	/// <summary>
	///   Gets the object representing the web page requested by the client. </summary>
	/// <remarks>
	///   This object should contain an instance or static method with same name as 
	///   one to be invoked by the client. </remarks>
	public Page Page {
		get {
			return m_page;
		}
	}

	/// <summary>
	///   Gets the name of the method to be invoked. </summary>
	/// <remarks>
	///   This is retrieved from the parameters passed to the page, which are specific to each client. </remarks>
	public abstract string Method { get; }

	/// <summary>
	///   Gets a string array with the arguments (parameters) to be passed 
	///   to the method to be invoked. </summary>
	/// <remarks>
	///   This is retrieved from the parameters passed to the page, which are specific to each client. </remarks>
	public abstract string[] Arguments { get; }

	/// <summary>
	///   Retrieves the method's return value inside a string formatted based on the client's requirements. </summary>
	/// <param name="returnValue">
	///   The value returned by the invoked method. </param>
	/// <param name="success">
	///   Indicator of whether the method succeeded or not. </param>
	/// <returns>
	///   The return value is a string formatted based on the client's requirements containing
	///   the result of the invoked method. </returns>
	public abstract string GetResult(string returnValue, bool success);

	/// <summary>
	///   Invokes the method on the page object as requested by client. </summary>
	/// <param name="endResponse">
	///   If true Response.End will be called after the method is invoked. </param>
	/// <remarks>
	///   After the method is invoked, its result is written to the response.
	///   The endResponse parameter allows for the extra convenience of causing 
	///   the result to be sent back to the client immediately.  </remarks>
	public void InvokeMethod(bool endResponse) {
		InvokeMethod();

		if (endResponse)
			m_page.Response.End();
	}

	/// <summary>
	///   Invokes the method on the page object as requested by client. </summary>
	/// <remarks>
	///   After the method is invoked, its result is written to the response. </remarks>
	public virtual void InvokeMethod() {
		bool success = true;
		string returnValue = "";

		try {
			Control control = m_page;
			string method = Method;

			if (method != null) {
				int dot = method.IndexOf(".");
				if (dot > 0) {
					string controlID = method.Substring(0, dot);

					control = FindChildControl(m_page, controlID);
					if (control == null)
						throw new Exception("No child control found with UniqueID '" + controlID + "' to use for remote scripting invocation.");

					method = method.Substring(dot + 1);
				}

				object result = control.GetType().InvokeMember(method, BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.GetProperty, null, control, Arguments);
				if (result != null)
					returnValue = result.ToString();
			}
		} catch (TargetInvocationException ex) {
			success = false;
			returnValue = ex.InnerException.Message;  // send back the real message
		} catch (Exception ex) {
			m_page.Trace.Write("RemoteScriptingClient.Invoke", "Error invoking remote scripting method '" + Method + "'", ex);

			success = false;
			returnValue = ex.ToString();  // it's probably a bug - send back everything
		}

		try  // Write the output string
			{
			m_page.Response.Clear();
			m_page.Response.Write(GetResult(returnValue, success));
		} catch (Exception ex) {
			m_page.Trace.Write("RemoteScriptingClient.Invoke", "Error writing the remote scripting result.", ex);
		}
	}

	/// <summary>
	///   Finds a control's child control based on its unique ID. </summary>
	/// <param name="parent">
	///   The control's whose child will be found. </param>
	/// <param name="uniqueID">
	///   The ID of the child control to find. </param>
	/// <returns>
	///   If the child control was found its object is returned; otherwise null is returned. </returns>
	protected static Control FindChildControl(Control parent, string uniqueID) {
		foreach (Control control in parent.Controls) {
			if (control.UniqueID.Replace(":", "_") == uniqueID)
				return control;

			Control childControl = FindChildControl(control, uniqueID);
			if (childControl != null)
				return childControl;
		}
		return null;
	}

	/// <summary>
	///   Encodes a string like a URL but without the + for spaces. </summary>
	/// <param name="str">
	///   The string to return encoded. </param>
	/// <returns>
	///   The encoded string. </returns>
	protected static string Encode(string str) {
		return HttpUtility.UrlEncode(str).Replace("+", "%20");
	}

	////////////////////////////////////////////////////////////////////////////////////////////////

	/// <summary>
	///   Class that represents a JSRS Remote Scripting client. </summary>
	/// <remarks>
	///   JSRS is a popular remote client implementation found at http://www.ashleyit.com/rs/jsrs/test.htm.
	///   It's simple and it supports multiple browsers. </remarks>
	public class JS : RemoteScriptingClient {
		/// <summary>
		///   Constructs the object and stores the page object internally. </summary>
		/// <param name="page">
		///   The object representing the web page where the client wants to invoke the method. </param>
		internal JS(Page page) :
				base(page) {
		}

		/// <summary>
		///   Gets the name of the method to be invoked. </summary>
		/// <remarks>
		///   This is retrieved from the 'F' parameter, passed to the page. </remarks>
		public override string Method {
			get {
				return m_page.Request.Params["F"];
			}
		}

		/// <summary>
		///   Gets a string array with the arguments (parameters) to be passed 
		///   to the method to be invoked. </summary>
		/// <remarks>
		///   This is retrieved from the 'P' parameters, passed to the page. </remarks>
		public override string[] Arguments {
			get {
				// Count the parameters				
				int paramCount = 0;
				while (m_page.Request.Params["P" + paramCount] != null)
					paramCount++;

				string[] args = new string[paramCount];
				for (int i = 0; i < paramCount; i++) {
					args[i] = m_page.Request.Params["P" + i];
					args[i] = args[i].Substring(1, args[i].Length - 2);
				}

				return args;
			}
		}

		/// <summary>
		///   Retrieves the method's return value inside a string formatted based for the JSRS client. </summary>
		/// <param name="returnValue">
		///   The value returned by the invoked method. </param>
		/// <param name="success">
		///   Indicator of whether the method succeeded or not. </param>
		/// <returns>
		///   The return value is a string formatted for JSRS containing
		///   the result of the invoked method. </returns>
		public override string GetResult(string returnValue, bool success) {
			StringBuilder sb = new StringBuilder("<html><head></head><body onload=\"p=document.layers?parentLayer:window.parent;");

			string context = m_page.Request.Params["C"];
			if (context == null)
				context = m_page.Request.Params["RS"];

			if (success)
				sb.Append("p.jsrsLoaded('" + context + "');\">jsrsPayload:<br><form name=\"jsrs_Form\"><textarea rows=\"4\" cols=\"80\" name=\"jsrs_Payload\">" + returnValue + "</textarea></form>");
			else
				sb.Append("p.jsrsError('" + context + "','jsrsError: " + Encode(returnValue).Replace("'", "\\'") + "');\">jsrsError: " + returnValue);

			sb.Append("</body></html>");
			return sb.ToString();
		}
	}

	////////////////////////////////////////////////////////////////////////////////////////////////

	/// <summary>
	///   Class that represents my Remote Scripting client. </summary>
	/// <remarks>
	///   RS is my own remote client implementation which is basically a rewrite of JSRS.
	///   It's simpler and cleaner than JSRS and it also supports multiple browsers. </remarks>
	public class RS : JS {
		internal RS(Page page) :
				base(page) {
		}

		/// <summary>
		///   Gets the name of the method to be invoked. </summary>
		/// <remarks>
		///   This is retrieved from the 'M' parameter, passed to the page. </remarks>
		public override string Method {
			get {
				return m_page.Request.Params["M"];
			}
		}

		/// <summary>
		///   Retrieves the method's return value inside a string formatted based for the RS client. </summary>
		/// <param name="returnValue">
		///   The value returned by the invoked method. </param>
		/// <param name="success">
		///   Indicator of whether the method succeeded or not. </param>
		/// <returns>
		///   The return value is a string formatted for RS containing
		///   the result of the invoked method. </returns>
		public override string GetResult(string returnValue, bool success) {
			string callID = m_page.Request.Params["RC"];
			string result = success ? "true" : "false";

			return
				"<html><body onload=\"p=document.layers?parentLayer:window.parent;p.RS.pool['" + callID + "'].setResult(" + result + ");\">" +
				"Payload:<br><form name=\"rsForm\"><textarea rows=\"4\" cols=\"80\" name=\"rsPayload\">" + returnValue + "</textarea></form>" +
				"</body></html>";
		}

	}

	////////////////////////////////////////////////////////////////////////////////////////////////

	/// <summary>
	///   Class that represents Microsoft's Remote Scripting client. </summary>
	/// <remarks>
	///   Microsoft's remote client implementation is found in the _ScriptLibrary directory installed
	///   by Visual InterDev 6.0 on new web sites.  It is very well written and it supports synchronous 
	///   calls since it uses a Java applet to communicate with the server.  Its drawback is that the 
	///   Java applet performs slower and it may require the user to download of the Java run-time. </remarks>
	public class MS : RemoteScriptingClient {
		internal MS(Page page) :
				base(page) {
		}

		/// <summary>
		///   Gets the name of the method to be invoked. </summary>
		/// <remarks>
		///   This is retrieved from the '_method' parameter, passed to the page. </remarks>
		public override string Method {
			get {
				return m_page.Request.Params["_method"];
			}
		}

		/// <summary>
		///   Gets a string array with the arguments (parameters) to be passed 
		///   to the method to be invoked. </summary>
		/// <remarks>
		///   This is retrieved from the 'p' parameters, passed to the page. </remarks>
		public override string[] Arguments {
			get {
				int paramCount = Convert.ToInt32(m_page.Request.Params["pcount"]);
				string[] args = new string[paramCount];

				for (int i = 0; i < paramCount; i++)
					args[i] = m_page.Request.Params["p" + i];

				return args;
			}
		}

		/// <summary>
		///   Retrieves the method's return value inside a string formatted based for the MSRS client. </summary>
		/// <param name="returnValue">
		///   The value returned by the invoked method. </param>
		/// <param name="success">
		///   Indicator of whether the method succeeded or not. </param>
		/// <returns>
		///   The return value is a string formatted for MSRS containing
		///   the result of the invoked method. </returns>
		/// <remarks>
		///   Microsoft's Remote Scripting has three return types: SIMPLE, EVAL_OBJECT, and ERROR.
		///   We only support SIMPLE and ERROR. </remarks>
		public override string GetResult(string returnValue, bool success) {
			return "<METHOD VERSION=\"1.0.8044\"><RETURN_VALUE TYPE=" + (success ? "SIMPLE" : "ERROR") + ">" + Encode(returnValue) + "</RETURN_VALUE></METHOD>";
		}
	}
}

