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

namespace Orthodox_Calendar
{
	public partial class Form1 : Form
	{
		private Uri BASE_URL = new Uri("http://days.pravoslavie.ru/Days/");

		public Form1()
		{
			InitializeComponent();
		}


		private string[] GetWebPage(Uri baseUrl, string date)
		{
			//Uri myUri = new Uri(baseUrl,  date);
			//         WebRequest request = WebRequest.Create(myUri);
			//         WebResponse response = request.GetResponse();

			//         using (Stream dataStream = response.GetResponseStream())
			//         {
			//             StreamReader reader = new StreamReader(dataStream);

			//             string responseFromServer = reader.ReadToEnd();
			//         }

			string html = @"http://days.pravoslavie.ru/Days/" + date + ".html";
			HtmlDocument HD = new HtmlDocument();

			var web = new HtmlWeb
			{
				AutoDetectEncoding = false,
				OverrideEncoding = Encoding.UTF8,
			};

			HD = web.Load(html);

			HtmlNode week = HD.DocumentNode.SelectSingleNode("//span[@class='DD_NED']");
			HtmlNode glas = HD.DocumentNode.SelectSingleNode("//span[@class='DD_GLAS']");
			HtmlNode marks = HD.DocumentNode.SelectSingleNode("//div[@class='DD_TEXT']");

			string value1 = week.InnerText;

			return new string[] { week.InnerText, glas.InnerText, marks.InnerText };
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var parsedFields = GetWebPage(BASE_URL, "20201002.html");

			label1.Text =  parsedFields[0];
			label2.Text =  parsedFields[1];
			textBox1.Text = parsedFields[2];
			monthCalendar1.SelectionRange.Start.ToString();
		}

		private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
		{
			var yearMonthDay = monthCalendar1.SelectionRange.Start.ToString("yyyyMMdd");

			var parsedFields = GetWebPage(BASE_URL, yearMonthDay);

			label1.Text = parsedFields[0];
			label2.Text = parsedFields[1];
			textBox1.Text = parsedFields[2];
			monthCalendar1.SelectionRange.Start.ToString();
		}
	}
}
// monthCalendar1.SelectionRange.Start.ToString();