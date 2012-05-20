using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Net;
using System.Web;


namespace MangaAnalyser
{
    public partial class AnalyseForm : Form
    {
        int iMul = 5;
        Bitmap m_b = null;
        List<GrabRect> m_rtList = new List<GrabRect>();
        int iNumbers = 1;
        public AnalyseForm()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            m_rtList.Add(new GrabRect(iNumbers.ToString(), Color.FromArgb(255, 0, 0), new Rectangle(5, 5, 200, 50),2,3));
            iNumbers++;
            RedrawItems();  
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            GrabRect rt = ImagepropertyGrid.SelectedObject as GrabRect;
            m_rtList.Remove(rt);
            bDragging = null;
            bSizing = null;
            ImagepropertyGrid.SelectedObject = null;
            RedrawItems();
        }

        private void SelectFilebutton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Select file";
            if (dlg.ShowDialog() != DialogResult.OK)
                return;
            FiletextBox.Text = dlg.FileName;
            PreviewFile();
        }
        void PreviewFile()
        {
            try
            {
                if (!System.IO.File.Exists(FiletextBox.Text))
                    return;
                Bitmap bm = (Bitmap)Bitmap.FromFile(FiletextBox.Text);
                m_b = new Bitmap(bm.Width, bm.Height, PixelFormat.Format24bppRgb);
                Graphics gr = Graphics.FromImage(m_b);
                gr.DrawImage(bm,0,0);
                gr.Dispose();
                RedrawItems();
            }
            catch (System.Exception e)
            {
                MessageBox.Show("Non supported type. " + e.Message);
            }
        }
        private void Analysebutton_Click(object sender, EventArgs e)
        {
            HieroglifAnalyser an = new HieroglifAnalyser(m_b, m_rtList);
            GrabRect rt = ImagepropertyGrid.SelectedObject as GrabRect;
            if (rt != null)
            {
                Bitmap subBmp = null;
                an.m_bItemsDict.TryGetValue(rt, out subBmp);
                JapanpictureBox.Image = subBmp;
                JapantextBox.Text = JapanRecognitor.Recognize(subBmp);
                rt.JapanString = JapantextBox.Text;
            }

        }

        private void ImagepictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            ImagepropertyGrid.SelectedObject = null;
            foreach (var el in m_rtList)
            {
                if (el.Rect.Contains(e.Location))
                {
                    ImagepropertyGrid.SelectedObject = el;
                    ShowItem();
                    break;
                }
            }
        }
        Cursor iCursorType = Cursors.Arrow;
        int iDraggingType = -1;
        GrabRect bDragging = null;
        GrabRect bSizing = null;
        Point pOld = new Point(0, 0);

        private void ImagepictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (bSizing == null)
            {
                bDragging = null;
                foreach (var el in m_rtList)
                {
                    Rectangle rt = el.Rect;
                    rt.Inflate(-iMul, -iMul);
                    if (rt.Contains(e.Location))
                    {
                        bDragging = el;
                        Cursor = iCursorType = Cursors.SizeAll;
                        break;
                    }
                }
            }
            if (bDragging == null)
            {
                bSizing = null;
                foreach (var el in m_rtList)
                {
                    if (el.Rect.Contains(e.Location))
                    {
                        iDraggingType = DetectPointOnRect(el.Rect, e.Location);
                        if (iDraggingType >= 1 && iDraggingType <= 8)
                        {
                            bSizing = el;
                            Cursor = iCursorType = GetCursorByType(iDraggingType);
                            break;
                        }
                    }
                }
            }
            pOld = e.Location;
        }

        private void ImagepictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (bDragging != null && pOld != e.Location)
            {
                Cursor = iCursorType;
                int iLeft = bDragging.Rect.X + e.Location.X - pOld.X;
                int iTop = bDragging.Rect.Y + e.Location.Y - pOld.Y;
                int iWidth = bDragging.Rect.Width;
                int iHeight = bDragging.Rect.Height;
                if (iLeft<0||iTop<0||m_b.Width<iLeft+iWidth||m_b.Height<iTop+iHeight)
                    return;
                bDragging.Rect = new Rectangle(iLeft, iTop, iWidth, iHeight);
                pOld = e.Location;
                RedrawItems();
                return;
            }
            if (bSizing != null && pOld != e.Location)
            {
                Cursor = iCursorType;
                int dX = e.Location.X - pOld.X;
                int dY = e.Location.Y - pOld.Y;
                switch (iDraggingType)
                {
                    case 1:
                        bSizing.Rect = new Rectangle(bSizing.Rect.X + dX, bSizing.Rect.Y + dY, bSizing.Rect.Width - dX, bSizing.Rect.Height - dY);
                        break;
                    case 2:
                        bSizing.Rect = new Rectangle(bSizing.Rect.X, bSizing.Rect.Y + dY, bSizing.Rect.Width, bSizing.Rect.Height - dY);
                        break;
                    case 3:
                        bSizing.Rect = new Rectangle(bSizing.Rect.X, bSizing.Rect.Y + dY, bSizing.Rect.Width + dX, bSizing.Rect.Height - dY);
                        break;
                    case 4:
                        bSizing.Rect = new Rectangle(bSizing.Rect.X + dX, bSizing.Rect.Y, bSizing.Rect.Width - dX, bSizing.Rect.Height);
                        break;
                    case 5:
                        bSizing.Rect = new Rectangle(bSizing.Rect.X, bSizing.Rect.Y, bSizing.Rect.Width + dX, bSizing.Rect.Height);
                        break;
                    case 6:
                        bSizing.Rect = new Rectangle(bSizing.Rect.X + dX, bSizing.Rect.Y, bSizing.Rect.Width - dX, bSizing.Rect.Height + dY);
                        break;
                    case 7:
                        bSizing.Rect = new Rectangle(bSizing.Rect.X, bSizing.Rect.Y, bSizing.Rect.Width, bSizing.Rect.Height + dY);
                        break;
                    case 8:
                        bSizing.Rect = new Rectangle(bSizing.Rect.X, bSizing.Rect.Y, bSizing.Rect.Width + dX, bSizing.Rect.Height + dY);
                        break;
                    default:
                        break;
                }
                if (bSizing.Rect.Width < iMul * 2)
                    bSizing.Rect = new Rectangle(bSizing.Rect.X, bSizing.Rect.Y, iMul * 2, bSizing.Rect.Height);
                if (bSizing.Rect.Height < iMul * 2)
                    bSizing.Rect = new Rectangle(bSizing.Rect.X, bSizing.Rect.Y, bSizing.Rect.Width, iMul * 2);
                pOld = e.Location;
                RedrawItems();
                return;
            }
            Cursor iType = null;
            foreach (var el in m_rtList)
            {
                if (el.Rect.Contains(e.Location))
                {
                    iType = GetCursor(el.Rect, e.Location);
                    break;
                }
            }
            if (iType == null)
                iType = Cursors.Arrow;
            Cursor = iType;
        }

        private void ImagepictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            bDragging = null;
            bSizing = null;

        }

        private void ImagepropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            RedrawItems();
            
        }
        private void ShowItem()
        {
            HieroglifAnalyser an = new HieroglifAnalyser(m_b, m_rtList);
            GrabRect rt = ImagepropertyGrid.SelectedObject as GrabRect;
            if (rt!=null)
            {
                Bitmap subBmp = null;
                an.m_bItemsDict.TryGetValue(rt, out subBmp);
                JapanpictureBox.Image = subBmp;
                JapantextBox.Text = rt.JapanString;
                EnglishtextBox.Text = rt.EnglishString;
            }
        }
        private string Translate(string str)
        {
            HttpClient hClient = new HttpClient();
            str = System.Web.HttpUtility.UrlEncode(str);
            string sReq = "http://translate.google.ru/translate_a/t?client=t&text=" + str + "&hl=ja&sl=ja&tl=en&multires=1&otf=2&trs=1&ssel=0&tsel=0&sc=1";
            string sResp = hClient.DownloadString(sReq);
            sResp = sResp.Substring(4);
            sResp = sResp.Substring(0, sResp.IndexOf("\",\""));
            sResp = sResp.Replace("\\\"","\"");
            return sResp;
        }

        private void RedrawItems()
        {
            try
            {
                if (m_b == null)
                    return;
                ImagepictureBox.Image = (Image)m_b.Clone();
                Graphics gr = Graphics.FromImage(ImagepictureBox.Image);
                Font f = new Font("Arial", 12);
                foreach (var el in m_rtList.Reverse<GrabRect>())
                {
                    Pen p = new Pen(el.Col, 1);
                    SolidBrush b = new SolidBrush(el.Col);
                    gr.DrawRectangle(p, el.Rect);

                    for (int y = 0; y < el.RowCount; ++y)
                    {
                        gr.DrawLine(p, el.Rect.Left, el.Rect.Top + y * el.Rect.Height / el.RowCount, el.Rect.Right, el.Rect.Top + y * el.Rect.Height / el.RowCount);
                    }

                    for (int x = 0; x < el.ColCount; ++x)
                    {
                        gr.DrawLine(p, el.Rect.Left + x * el.Rect.Width / el.ColCount, el.Rect.Top, el.Rect.Left + x * el.Rect.Width / el.ColCount, el.Rect.Bottom);
                    }

                    gr.DrawString(el.Name, f, b, el.Rect.Left, el.Rect.Top);
                    p.Dispose();
                    b.Dispose();
                }
                f.Dispose();
                gr.Dispose();
                ShowItem();
                Application.DoEvents();
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private Cursor GetCursorByType(int iCursType)
        {
            switch (iCursType)
            {
                case 1:
                    return Cursors.SizeNWSE;
                case 2:
                    return Cursors.SizeNS;
                case 3:
                    return Cursors.SizeNESW;
                case 4:
                    return Cursors.SizeWE;
                case 5:
                    return Cursors.SizeWE;
                case 6:
                    return Cursors.SizeNESW;
                case 7:
                    return Cursors.SizeNS;
                case 8:
                    return Cursors.SizeNWSE;
                case 9:
                    return Cursors.SizeAll;
                default:
                    return Cursors.Arrow;
            }
        }
        private Cursor GetCursor(Rectangle rt, Point pt)
        {
            return GetCursorByType(DetectPointOnRect(rt, pt));
        }
        private int DetectPointOnRect(Rectangle rt, Point pt)
        {
            Rectangle buf = rt;

            buf.Inflate(-iMul, -iMul);
            if (buf.Contains(pt))
            {
                return 9;
            }
            if (!rt.Contains(pt))
            {
                return -1;
            }

            Rectangle rttl = new Rectangle(rt.Left, rt.Top, iMul, iMul)
                , rtt = new Rectangle(buf.Left, rt.Top, rt.Width - 2 * iMul, iMul)
                , rttr = new Rectangle(buf.Right, rt.Top, iMul, iMul)
                , rtl = new Rectangle(rt.Left, buf.Top, iMul, rt.Height - 2 * iMul)
                , rtr = new Rectangle(buf.Right, buf.Top, iMul, rt.Height - 2 * iMul)
                , rtbl = new Rectangle(rt.Left, buf.Bottom, iMul, iMul)
                , rtb = new Rectangle(buf.Left, buf.Bottom, rt.Width - 2 * iMul, iMul)
                , rtbr = new Rectangle(buf.Right, buf.Bottom, iMul, iMul);

            if (rttl.Contains(pt))
            {
                return 1;
            }
            if (rtt.Contains(pt))
            {
                return 2;
            }
            if (rttr.Contains(pt))
            {
                return 3;
            }
            if (rtl.Contains(pt))
            {
                return 4;
            }
            if (rtr.Contains(pt))
            {
                return 5;
            }
            if (rtbl.Contains(pt))
            {
                return 6;
            }
            if (rtb.Contains(pt))
            {
                return 7;
            }
            if (rtbr.Contains(pt))
            {
                return 8;
            }

            return -1;
        }

        private void Translatebutton_Click(object sender, EventArgs e)
        {
            EnglishtextBox.Text = Translator.Translate(JapantextBox.Text);
            GrabRect rt = ImagepropertyGrid.SelectedObject as GrabRect;
            if (rt != null)
            {
                rt.EnglishString = EnglishtextBox.Text;
            }
        }

        private void ImagepictureBox_Click(object sender, EventArgs e)
        {

        }

        private void JapantextBox_TextChanged(object sender, EventArgs e)
        {
            EnglishtextBox.Text = Translator.Translate(JapantextBox.Text);
            GrabRect rt = ImagepropertyGrid.SelectedObject as GrabRect;
            if (rt != null)
            {
                rt.JapanString = JapantextBox.Text;
            }
        }

        private void EnglishtextBox_TextChanged(object sender, EventArgs e)
        {
            EnglishtextBox.Text = Translator.Translate(JapantextBox.Text);
            GrabRect rt = ImagepropertyGrid.SelectedObject as GrabRect;
            if (rt != null)
            {
                rt.EnglishString = EnglishtextBox.Text;
            }
        }
    }
}
