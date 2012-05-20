using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MangaAnalyser
{
    public static class Translator
    {
        public static string Translate(string sInput)
        {
            if (string.IsNullOrEmpty(sInput))
                return "";
            try
            {
                HttpClient hClient = new HttpClient();
                sInput = System.Web.HttpUtility.UrlEncode(sInput);
                string sReq = "http://translate.google.ru/translate_a/t?client=t&text=" + sInput + "&hl=ja&sl=ja&tl=en&multires=1&otf=2&trs=1&ssel=0&tsel=0&sc=1";
                string sResp = hClient.DownloadString(sReq);
                sResp = sResp.Substring(4);
                sResp = sResp.Substring(0, sResp.IndexOf("\",\""));
                sResp = sResp.Replace("\\\"", "\"");
                return sResp;
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return "";
        }
    }
}
