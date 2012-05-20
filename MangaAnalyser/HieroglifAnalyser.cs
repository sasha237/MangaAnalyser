using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;

namespace MangaAnalyser
{
    class HieroglifAnalyser
    {
        public Dictionary<GrabRect, Bitmap> m_bItemsDict;

        public HieroglifAnalyser(Bitmap b, List<GrabRect> rtList)
        {
            m_bItemsDict = new Dictionary<GrabRect, Bitmap>();
            foreach (var el in rtList)
            {
                Bitmap bufB = b.Clone(el.Rect, PixelFormat.Format24bppRgb);
                bufB = ConvertToHorisontal(bufB, el);
                bufB = Inflate(bufB);
                m_bItemsDict.Add(el, bufB);
            }
        }
        private Bitmap ConvertToHorisontal(Bitmap bmp, GrabRect el)
        {
            int iCeilWidth = el.Rect.Width/el.ColCount;
            int iCeilHeight = el.Rect.Height/el.RowCount;
            Bitmap bmpNew = new Bitmap(el.Rect.Width*el.RowCount, el.Rect.Height/el.RowCount);
            int iCount = 0;
            Graphics gr = Graphics.FromImage(bmpNew);
            for (int i = el.ColCount - 1; i >= 0; i--)
            {
                for (int j = 0; j < el.RowCount; j++)
                {
                    gr.DrawImage(bmp.Clone(new Rectangle(i * iCeilWidth, j * iCeilHeight, iCeilWidth, iCeilHeight), PixelFormat.Format24bppRgb), iCount * iCeilWidth, 0);
                    iCount++;
                }
            }
            gr.Dispose();
            return bmpNew;
        }
        private Bitmap Inflate(Bitmap bufB)
        {
            Bitmap bmp = new Bitmap(bufB.Width + 6, bufB.Height + 6);
            Graphics gr = Graphics.FromImage(bmp);
            gr.FillRectangle(new SolidBrush(Color.White), 0, 0, bmp.Width, bmp.Height);
            gr.DrawImage(bufB, 3, 3);
            gr.Dispose();
            return bmp;
        }
    }
}
