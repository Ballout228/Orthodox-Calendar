using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using System.Text.RegularExpressions;

namespace Orthodox_Calendar
{
	
	public partial class Form1 : Form
	{
		private Uri BASE_URL = new Uri("http://days.pravoslavie.ru/Days/");

		public Form1()
		{
			InitializeComponent();
		}

		private string[] GetWebPage(Uri baseUrl, string date )
		{
		
			string html = @"http://days.pravoslavie.ru/Days/" + date + ".html";
			HtmlDocument HD = new HtmlDocument();

			var web = new HtmlWeb{AutoDetectEncoding = false, OverrideEncoding = Encoding.UTF8,};

			HD = web.Load(html);
			
			

			string week = HD.DocumentNode.SelectSingleNode("//span[@class='DD_NED']").InnerText;
			string glas = HD.DocumentNode.SelectSingleNode("//span[@class='DD_GLAS']").InnerText;
			HtmlNode text = HD.DocumentNode.SelectSingleNode("//div[@class='DD_TEXT']");
			string saints = text.InnerText;
			HtmlNode firstline = text.FirstChild;

			

			HtmlNode img = firstline.FirstChild;

			string sign = "Служба не имеет праздничного знака";
			if (img.Attributes["alt"] != null)
			{
				sign = img.Attributes["alt"].Value;

			}
			





			object post1 = HD.DocumentNode.SelectSingleNode("//span[@class='DD_TPTXT']").InnerText;
			object post = "Можно вкушать мясо";

			if (HD.DocumentNode.SelectSingleNode("//span[@class='DD_POST']") != null)

			{
				post = HD.DocumentNode.SelectSingleNode("//span[@class='DD_POST']").InnerText;
			}

			return new string[] { week, glas, saints, Convert.ToString(post), Convert.ToString(post1), sign,  };
		}
	

		private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)

		{
	
			DateTime now = monthCalendar1.SelectionRange.Start.AddDays(-13);

			var parsedFields = GetWebPage(BASE_URL, now.ToString("yyyyMMdd"));
			
			label1.Text = parsedFields[0];
			label2.Text = parsedFields[1];
			textBox1.Text = parsedFields[2];
            label3.Text = parsedFields[4];
			label4.Text = parsedFields[3];
			label5.Text = parsedFields[5];
			







		}
	}
}
