using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.IO;
using System.Drawing.Imaging;


namespace MangaAnalyser
{
    [DefaultPropertyAttribute("Id")]
    [TypeConverter(typeof(RectTitleConverter))]
    public class GrabRect
    {
        private string m_name;
        private Color m_color;
        private Rectangle m_rt;
        private int m_rowCount;
        private int m_colCount;
        private string m_sJapan;
        private string m_sEnglish;
        public GrabRect(string sName, Color cCol, Rectangle prt, int iRowCount, int iColCount)
        {
            Name = sName;
            Col = cCol;
            m_rt = prt;
            m_rowCount = iRowCount;
            m_colCount = iColCount;
        }
        [DisplayName("Id"), DescriptionAttribute("Object id")]
        public string Name
        {
            get
            {
                return m_name;
            }
            set
            {
                m_name = value;
            }
        }
        [ DisplayName("Color"), DescriptionAttribute("Color of selection")]
        public Color Col
        {
            get
            {
                return m_color;
            }
            set
            {
                m_color = value;
            }
        }
        [ DisplayName("Position"), DescriptionAttribute("Position on screen")]
        public Rectangle Rect
        {
            get
            {
                return m_rt;
            }
            set
            {
                m_rt = value;
            }
        }

        [DisplayName("Rows"), DescriptionAttribute("Count of rows")]
        public int RowCount
        {
            get 
            { 
                return m_rowCount; 
            }
            set 
            {
                int iH = m_rt.Height / m_rowCount;
                m_rowCount = value;
                m_rt.Height = iH * m_rowCount;
            }
        }
        [DisplayName("Columns"), DescriptionAttribute("Count of columns")]
        public int ColCount
        {
            get 
            { 
                return m_colCount; 
            }
            set 
            {
                int iW = m_rt.Width / m_colCount;

                m_colCount = value;
                m_rt.Width = iW * m_colCount;
            }
        }
        [DisplayName("Japan string"), DescriptionAttribute("Recognized Japanese string")]
        public string JapanString
        {
            get
            {
                return m_sJapan;
            }
            set
            {
                m_sJapan = value;
            }
        }
        [DisplayName("English string"), DescriptionAttribute("Translated English string")]
        public string EnglishString
        {
            get
            {
                return m_sEnglish;
            }
            set
            {
                m_sEnglish = value;
            }
        }
    }
    public class RectTitleConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture,
            object value, Type destType)
        {
            if (destType == typeof(String) && value is GrabRect)
            {
                GrabRect rectTitle = (GrabRect)value;
                return rectTitle.Name;
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }
}
