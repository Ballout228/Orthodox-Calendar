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

namespace Orthodox_Calendar
{
    public partial class Form1 : Form
    {
        private Uri BASE_URL = new Uri("http://days.pravoslavie.ru/Days/");

        public Form1()
        {
            InitializeComponent();
            GetWebPage(BASE_URL, "20201002.html");
        }


        private void GetWebPage(Uri baseUrl, string date)
        {
			Uri myUri = new Uri(baseUrl,  date);
            WebRequest request = WebRequest.Create(myUri);
            WebResponse response = request.GetResponse();

            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);

                string responseFromServer = reader.ReadToEnd();
            }

        }

       
    }
}
// http://days.pravoslavie.ru/Days/20201002.html