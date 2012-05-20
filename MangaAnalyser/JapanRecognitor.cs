using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing.Imaging;
using System.Net;
using System.Web;
using System.Drawing;

namespace MangaAnalyser
{
    public static class JapanRecognitor
    {
        public static string Recognize(Bitmap inputBitmap)
        {
            MemoryStream ms = new MemoryStream();
            inputBitmap.Save(ms, ImageFormat.Bmp);

            byte[] bmp = ms.ToArray();

            HttpClient hClient = new HttpClient();
            //Make POST-request
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("d").Substring(0, 13);
            string NameAffix = "--" + boundary + "\r\nContent-Disposition: form-data; name=\"";

            System.IO.MemoryStream postdata = new System.IO.MemoryStream();

            //Header of file
            string formdata = "";
            formdata += NameAffix + "userfile\"; filename=\"image.bmp\"\r\n";
            formdata += "Content-Type: image/bmp\r\n\r\n";

            //Write
            postdata.Write(Encoding.ASCII.GetBytes(formdata), 0, formdata.Length);
            postdata.Write(bmp, 0, bmp.Length);
            //Prepare ending
            formdata = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"outputencoding\"\r\n\r\nutf-8\r\n";
            formdata += "--" + boundary + "\r\nContent-Disposition: form-data; name=\"outputformat\"\r\n\r\ntxt\r\n";
            formdata += "--" + boundary + "\r\nContent-Disposition: form-data; name=\"eclass\"\r\n\r\nauto\r\n";
            formdata += "--" + boundary + "--";
            //Write
            postdata.Write(Encoding.ASCII.GetBytes(formdata), 0, formdata.Length);
            byte[] buffer = new byte[postdata.Length];
            postdata.Seek(0, System.IO.SeekOrigin.Begin);
            postdata.Read(buffer, 0, buffer.Length);
            System.IO.File.WriteAllBytes("log.txt", buffer);
            hClient.Timeout = hClient.Timeout * 10;
            hClient.Referer = "http://appsv.ocrgrid.org/nhocr/";
            return hClient.UploadMultipartData(
                "http://appsv.ocrgrid.org/cgi-bin/weocr/nhocr.cgi", buffer, boundary);
        }
    }
}
