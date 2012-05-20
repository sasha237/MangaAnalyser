using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;

namespace MangaAnalyser
{
    public class HttpClient
    {
		/// <summary>
		///Headers of next request
		/// </summary>
        public readonly WebHeaderCollection Headers = new WebHeaderCollection();
		/// <summary>
		///Header's content User-Agent  
		/// </summary>
        public string UserAgent="Mozilla/5.0 (Windows; U; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727)";
        /// <summary>
        ///Referer for using. Usually automatic 
        /// </summary>
		public string Referer="";
		/// <summary>
		///Timeout of requests. If timed out, then exception
		/// </summary>
        public int Timeout = 20000;
		
        public CookieContainer Cookies = new CookieContainer();
        /// <summary>
        ///If true, tnen Referer will not automaticcally change
		///TODO: To remove and do more adequate
        /// </summary>
		public bool Ajax = false;
		
		/// <summary>
		/// Proxy 
		/// </summary>
        public string Proxy
		{
			get { return m_Proxystring; }
            set {
				m_Proxystring = value;
				if((value==null)||(value.Length==0)) m_Proxy=null;
				else m_Proxy=new WebProxy("http://"+value+"/");
			}
		}
        private IWebProxy m_Proxy=null;
		private string m_Proxystring="";
		
		
		
		
 		/// <summary>
 		///Make POST-request
 		/// </summary>
 		/// <param name="URL">
 		/// URL fully
 		/// A <see cref="System.String"/>
 		/// </param>
 		/// <param name="Data">
 		/// Data for request
 		/// A <see cref="System.String"/>
 		/// </param>
 		/// <param name="ContentType">
 		/// Content of Content-Type of header
 		/// A <see cref="System.String"/>
 		/// </param>
 		/// <returns>
 		/// Japan of request
 		/// A <see cref="System.String"/>
 		/// </returns>       
        public string UploadString(string URL, string Data, string ContentType)
        {
            HttpWebRequest req = PresetRequest(URL);
            if(ContentType!=null) req.ContentType=ContentType;
            req.Method = "POST";
            int len = Encoding.UTF8.GetByteCount(Data);
            req.ContentLength=len;
            //req.GetRequestStream().Write(Encoding.UTF8.GetBytes(Data), 0, len);
            return GetResponseText(req,Encoding.UTF8.GetBytes(Data));
        }
 		/// <param name="URL">
 		/// URL fully
 		/// A <see cref="System.String"/>
 		/// </param>
 		/// <param name="Data">
 		/// Data for request
 		/// A <see cref="System.String"/>
 		/// </param>
 		/// </param>
 		/// <returns>
 		/// Resualt of request
 		/// A <see cref="System.String"/>
 		/// </returns>       
        public string UploadString(string URL, string Data)
        {
            return UploadString(URL, Data,"application/x-www-form-urlencoded");
        }
		/// <summary>
		/// POST-request with formed multipart-data
		/// </summary>
		/// <param name="URL">
		/// URL полностью
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="Data">
		/// Data for request
		/// A <see cref="System.Byte[]"/>
		/// </param>
		/// <param name="boundary">
		/// Delimiter. See HTTP docs
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// Japan of request
		/// A <see cref="System.String"/>
		/// </returns>        
        public string UploadMultipartData(string URL, byte[] Data, string boundary)
        {
            HttpWebRequest req = PresetRequest(URL);
            if (boundary != null)
                req.ContentType = "multipart/form-data; boundary=" + boundary;
            req.Method = "POST";
            req.ContentLength = Data.Length;
            //req.GetRequestStream().Write(Data,0,Data.Length);

            return GetResponseText(req,Data);
        }

        public Stream UploadMultipartDataStream(string URL, byte[] Data, string boundary)
        {
            HttpWebRequest req = PresetRequest(URL);
            if (boundary != null)
                req.ContentType = "multipart/form-data; boundary=" + boundary;
            req.Method = "POST";
            req.ContentLength = Data.Length;
            //req.GetRequestStream().Write(Data,0,Data.Length);

            return GetResponseStream(req, Data);
        }


        private void FloodThread(object arg)
        {
            HttpWebRequest req = (HttpWebRequest)arg;
            try
            {
                GetResponseText(req,null);
            }
            catch { };
        }

		/// <summary>
		///Make flood to server
		/// </summary>
        public void Flood(string URL, int ThreadCount, string Referer, bool UsePost, string PostData)
        {

            HttpWebRequest[] reqs = new HttpWebRequest[ThreadCount];
            System.Threading.Thread[] threads = new System.Threading.Thread[ThreadCount];
            for (int c = 0; c < ThreadCount; c++)
            {
                reqs[c] = PresetRequest(URL);
                reqs[c].ServicePoint.ConnectionLimit = ThreadCount;
                reqs[c].Referer = Referer;
                
                if(UsePost)
                {
                    reqs[c].Method = "POST";
                    reqs[c].ContentType = "application/x-www-form-urlencoded";
                    int len = Encoding.UTF8.GetByteCount(PostData);
                    reqs[c].ContentLength = len;
                    reqs[c].GetRequestStream().Write(Encoding.UTF8.GetBytes(PostData), 0, len);
                
                };
                threads[c] = new Thread(FloodThread);
            }
            
            for (int c = 0; c < ThreadCount; c++) threads[c].Start(reqs[c]);

        }

        public void Flood(string URL, int ThreadCount, string  Referer) { Flood(URL, ThreadCount, Referer,false, ""); }
        public void Flood(string URL, int ThreadCount) { Flood(URL, ThreadCount, Referer); }
		/// <summary>
		///Set GET-request
		/// </summary>
		/// <param name="URL">
		/// URL fully
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// Japan of request
		/// A <see cref="System.String"/>
		/// </returns>
		public string DownloadString(string URL)
        {
            HttpWebRequest req = PresetRequest(URL);
            return GetResponseText(req,null);
        }

		private string GetResponseText(HttpWebRequest req,byte[] data)
		{
			while(true)
			{
				try
				{
					return GetResponseText2(req,data);
				}
				catch(Exception e)
				{
					//TODO: Add error handling stuff here
					throw e;
				}
			}
		}
        private string GetResponseText2(HttpWebRequest req,byte[] data)
        {
			if(data!=null)	req.GetRequestStream().Write(data, 0, data.Length);
            HttpWebResponse resp = GetResponseWithTimeout(req);
            Stream stream = resp.GetResponseStream();
            if (resp.GetResponseHeader("Content-Encoding").ToLower().Contains("gzip"))
                stream = new System.IO.Compression.GZipStream(stream,
                    System.IO.Compression.CompressionMode.Decompress);
            TextReader reader = new StreamReader(stream, UTF8Encoding.UTF8);
            return reader.ReadToEnd();
        }

        private Stream GetResponseStream(HttpWebRequest req, byte[] data)
        {
            while (true)
            {
                try
                {
                    return GetResponseStream2(req, data);
                }
                catch (Exception e)
                {
                    //TODO: Add error handling stuff here
                    throw e;
                }
            }
        }
        private Stream GetResponseStream2(HttpWebRequest req, byte[] data)
        {
            if (data != null) req.GetRequestStream().Write(data, 0, data.Length);
            HttpWebResponse resp = GetResponseWithTimeout(req);
            Stream stream = resp.GetResponseStream();
            if (resp.GetResponseHeader("Content-Encoding").ToLower().Contains("gzip"))
                stream = new System.IO.Compression.GZipStream(stream,
                    System.IO.Compression.CompressionMode.Decompress);
            return stream;
        }

		public byte[] DownloadData(string URL)
		{	
            HttpWebRequest req = PresetRequest(URL);
			HttpWebResponse resp = GetResponseWithTimeout(req);
            Stream stream = resp.GetResponseStream();
            if (resp.GetResponseHeader("Content-Encoding").ToLower().Contains("gzip"))
                stream = new System.IO.Compression.GZipStream(stream,
                    System.IO.Compression.CompressionMode.Decompress);
			int length=int.Parse(resp.GetResponseHeader("Content-Length"));
            
			byte[] rv=new byte[length];
			int roff=0,r=0;
			while((roff+r)!=length)
			{
				r=stream.Read(rv,roff,length-roff);
				roff+=r;
				r=0;
			}
			return rv;
		}

        private void GetResponseCallback(IAsyncResult ar)
        {
            ((AutoResetEvent)ar.AsyncState).Set();
        }


        private HttpWebResponse GetResponseWithTimeout(HttpWebRequest req)
        {
            
			bool m_Ajax = Ajax; Ajax = false;
            AutoResetEvent ev=new AutoResetEvent(false);
            IAsyncResult Japan =req.BeginGetResponse(GetResponseCallback, ev);

            if(!ev.WaitOne(Timeout))
            {
                req.Abort();
                return null;
            }
            if (!m_Ajax) Referer = req.RequestUri.ToString();
            return (HttpWebResponse)req.EndGetResponse(Japan);

        }

        private HttpWebRequest PresetRequest(string URL)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL);
            req.Expect = "";
            req.Headers.Add(Headers);
            req.Accept="text/html, application/xml;q=0.9, application/xhtml+xml, image/png, image/jpeg, image/gif, image/x-xbitmap";
            req.Headers.Add("Accept-Encoding: gzip, identity");
            req.KeepAlive = false;
            req.CookieContainer = Cookies;
            req.Referer = Referer;
            if(Proxy!=null)
				req.Proxy = m_Proxy;
            req.UserAgent = UserAgent;
            req.Method = "GET";
			
            return req;
        }
        public HttpClient()
        {
			
            System.Net.ServicePointManager.Expect100Continue = false;
        }
    }
}
