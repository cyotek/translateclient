#region License block : MPL 1.1/GPL 2.0/LGPL 2.1
/* ***** BEGIN LICENSE BLOCK *****
 * Version: MPL 1.1/GPL 2.0/LGPL 2.1
 *
 * The contents of this file are subject to the Mozilla Public License Version
 * 1.1 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 * http://www.mozilla.org/MPL/
 *
 * Software distributed under the License is distributed on an "AS IS" basis,
 * WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
 * for the specific language governing rights and limitations under the
 * License.
 *
 * The Original Code is the FreeCL.Net library.
 *
 * The Initial Developer of the Original Code is 
 *  Oleksii Prudkyi (Oleksii.Prudkyi@gmail.com).
 * Portions created by the Initial Developer are Copyright (C) 2007-2008
 * the Initial Developer. All Rights Reserved.
 *
 * Contributor(s):
 *
 * Alternatively, the contents of this file may be used under the terms of
 * either the GNU General Public License Version 2 or later (the "GPL"), or
 * the GNU Lesser General Public License Version 2.1 or later (the "LGPL"),
 * in which case the provisions of the GPL or the LGPL are applicable instead
 * of those above. If you wish to allow use of your version of this file only
 * under the terms of either the GPL or the LGPL, and not to allow others to
 * use your version of this file under the terms of the MPL, indicate your
 * decision by deleting the provisions above and replace them with the notice
 * and other provisions required by the GPL or the LGPL. If you do not delete
 * the provisions above, a recipient may use your version of this file under
 * the terms of any one of the MPL, the GPL or the LGPL.
 *
 * ***** END LICENSE BLOCK ***** */
#endregion

using System;
using System.Net; 
using System.IO; 
using System.Web; 
using System.IO.Compression;
using System.Text; 
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Translate
{
	public enum WebRequestContentType
	{
		None,
		UrlEncoded,
		Multipart,
		UrlEncodedGet
	}
	
	public abstract class StreamConvertor
	{
		public abstract Stream ConvertStream(Stream stream);
	}
	/// <summary>
	/// Description of WebRequestHelper.
	/// </summary>
	public class WebRequestHelper : IDisposable
	{
	
		public WebRequestHelper(Result result, Uri url, NetworkSetting networkSetting, WebRequestContentType contentType)
		{
			this.result = result;
			this.url = url;
			this.networkSetting = networkSetting;
			this.contentType = contentType;
		}
	
		public WebRequestHelper(Result result, Uri url, NetworkSetting networkSetting, WebRequestContentType contentType, Encoding encoding)
		{
			this.result = result;
			this.url = url;
			this.networkSetting = networkSetting;
			this.contentType = contentType;
			this.encoding = encoding;
		}
	
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="4#gzip")]
		public WebRequestHelper(Result result, Uri url, NetworkSetting networkSetting, WebRequestContentType contentType, bool gzipRequest)
		{
			this.result = result;
			this.url = url;
			this.networkSetting = networkSetting;
			this.contentType = contentType;
			this.gZipRequest = gzipRequest;
		}
				
		Result result;
		public Result Result {
			get { return result; }
			set { result = value; }
		}
		
		Uri url;
		public Uri Url {
			get { return url; }
			set { url = value; }
		}
		
		NetworkSetting networkSetting;
		public NetworkSetting NetworkSetting {
			get { return networkSetting; }
			set { networkSetting = value; }
		}
		
		WebRequestContentType contentType;
		public WebRequestContentType ContentType {
			get { return contentType; }
			set { contentType = value; }
		}
		
		bool gZipRequest;
		public bool GZipRequest {
			get { return gZipRequest; }
			set { gZipRequest = value; }
		}
		
		Encoding encoding = Encoding.UTF8;
		public Encoding Encoding {
			get { return encoding; }
			set { encoding = value; }
		}
		
		string acceptLanguage;
		public string AcceptLanguage {
			get { return acceptLanguage; }
			set { acceptLanguage = value; }
		}
		
		string acceptCharset;
		public string AcceptCharset {
			get { return acceptCharset; }
			set { acceptCharset = value; }
		}
		
		StreamConvertor streamConvertor;
		public StreamConvertor StreamConvertor {
			get { return streamConvertor; }
			set { streamConvertor = value; }
		}
		
		CookieContainer cookieContainer = null;
		public CookieContainer CookieContainer {
			get { return cookieContainer; }
			set { cookieContainer = value; }
		}
		
		string referer;
		public string Referer {
			get { return referer; }
			set { referer = value; }
		}
		
		string multiPartBoundary = "-----------------------------325433208117628";
		
		MemoryStream postStream;
		BinaryWriter postData;
		
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.InvalidOperationException.#ctor(System.String)")]
		public void AddPostKey(string key, byte[] value)
		{
			if(alreadyGenerated)
				throw new InvalidOperationException("AddPostKey() after GetResponse() not supported");
				
			if(contentType != WebRequestContentType.Multipart)
				throw new InvalidOperationException("AddPostKey supported only for Multipart content type");
				
			if (this.postData == null) 
			{
				this.postStream = new MemoryStream();
				this.postData = new BinaryWriter(this.postStream);
			}
				
	
			string nl = "\r\n";
			this.postData.Write( Encoding.UTF8.GetBytes(
					"--" + this.multiPartBoundary + nl + 
					"Content-Disposition: form-data; name=\"" +key+"\"" + nl + nl) );
						
			this.postData.Write( value );
	
			this.postData.Write( Encoding.UTF8.GetBytes(nl) );
		}
	
		public void AddPostKey(string key, string value)
		{
			this.AddPostKey(key,Encoding.UTF8.GetBytes(value));
		}
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.InvalidOperationException.#ctor(System.String)")]
		public void AddPostData(string data)
		{
			if(contentType != WebRequestContentType.UrlEncoded)
				throw new InvalidOperationException("AddPostData supported only for UrlEncoded content type");
		
			if (this.postData == null) 
			{
				this.postStream = new MemoryStream();
				this.postData = new BinaryWriter(this.postStream);
			}
			this.postData.Write( Encoding.UTF8.GetBytes(data) );
		}
		
		
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public string GetResponse()
		{
			result.RetryCount ++;
			while(true)
			{
				try
				{
					return InternalGetResponse();
				}
				catch(WebException we)
				{
					//keep alive of google without proxy is break over 9 secs without notify
					//retry in this case
					if(we.Status != WebExceptionStatus.KeepAliveFailure)
					{
						throw;
					}
				}
			}			
		}

		bool alreadyGenerated;
		string InternalGetResponse()
		{
			//request
			WebRequest request = WebRequest.Create (url);
			result.BytesSent += url.OriginalString.Length;
			
			request.Proxy = networkSetting.Proxy;
			request.Timeout = networkSetting.Timeout;
			if(contentType != WebRequestContentType.UrlEncodedGet)
			{
				request.Method = "POST"; 
				result.BytesSent += 4;
			}
			else 
			{
				request.Method = "GET"; 
				result.BytesSent += 3;
			}
			result.BytesSent += 12; //system data
			
			HttpWebRequest webRequest = request as  HttpWebRequest;
			webRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
			
			if(!string.IsNullOrEmpty(acceptLanguage))
			{
				webRequest.Headers[HttpRequestHeader.AcceptLanguage] = acceptLanguage;
			}

			if(!string.IsNullOrEmpty(acceptCharset))
			{
				webRequest.Headers[HttpRequestHeader.AcceptCharset] = acceptCharset;
			}
			
			
			//headers
			webRequest.UserAgent = "Mozilla/5.0, Translate.Net";
			//webRequest.UserAgent = "Mozilla/5.0";
			
			if(string.IsNullOrEmpty(referer))
				webRequest.Referer = url.AbsoluteUri;
			else	
				webRequest.Referer = referer;
			
			request.Headers.Add(HttpRequestHeader.KeepAlive , "300");
			request.Credentials = CredentialCache.DefaultCredentials;
			
			if(cookieContainer != null)
				webRequest.CookieContainer = cookieContainer;
				
			
			if(contentType == WebRequestContentType.UrlEncoded)
			{
				request.ContentType = "application/x-www-form-urlencoded";
			}
			else if(contentType == WebRequestContentType.Multipart)
			{
				request.ContentType = "multipart/form-data; boundary=" + this.multiPartBoundary;
				if(!alreadyGenerated)
					postData.Write( Encoding.UTF8.GetBytes( "--" + this.multiPartBoundary + "\r\n" ) );
			}
			
			alreadyGenerated = true;
			
			if(contentType != WebRequestContentType.UrlEncodedGet)
			{
				Stream requestStream = request.GetRequestStream (); 
				
				postData.Flush();
				if(gZipRequest)
				{
					request.Headers.Add("Content-Encoding", "gzip");
					GZipStream gzipStream = new GZipStream(requestStream, CompressionMode.Compress);
					postStream.WriteTo(gzipStream);
					gzipStream.Close();
				}
				else
				{
					postStream.WriteTo(requestStream);
				}
				requestStream.Close ();
			}

		
			WebResponse response = request.GetResponse ();
			
			result.BytesSent += request.Headers.ToByteArray().Length;
			if(request.ContentLength != -1)
			{
				result.BytesSent += request.ContentLength;
			}
			result.BytesReceived += response.Headers.ToByteArray().Length;
			
			Stream responseStream = response.GetResponseStream ();
			
			if(streamConvertor != null)
				responseStream = streamConvertor.ConvertStream(responseStream);
				
			StreamReader reader = new StreamReader (responseStream, encoding);
			string resultStr = reader.ReadToEnd ();
			if(response.ContentLength != -1)
				result.BytesReceived += response.ContentLength;
			else if(!string.IsNullOrEmpty(response.Headers[HttpResponseHeader.ContentLength]))
				result.BytesReceived += int.Parse(response.Headers[HttpResponseHeader.ContentLength], CultureInfo.InvariantCulture);
			else
				result.BytesReceived += resultStr.Length;
				
			result.BytesReceived += 21; //system data	

			reader.Close ();
			responseStream.Close();
			response.Close();			   
			return resultStr;
		}
		
		~WebRequestHelper()
	  	{
      		Dispose(false);
	  	}

    	public void Dispose()
    	{
      		Dispose(true);
      		GC.SuppressFinalize(this);
    	}
    
    	protected virtual void Dispose(bool disposing)
    	{
		
			if(disposing)
			{
				if(postData != null)
				{
					postData.Close();
					postData = null;
				}

				if(postStream != null)
				{
					postStream.Dispose();
					postStream = null;
				}
				result = null;
				url = null;
				networkSetting = null;
			}		
		}

		
	}
}
