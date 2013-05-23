using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Html.Helpers;
using System.Text.RegularExpressions;

namespace Outils
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.listView1.View = System.Windows.Forms.View.Details;

            ColumnHeader columnheader0 = new ColumnHeader();
            columnheader0.Text = "CP";

            ColumnHeader columnheader1 = new ColumnHeader();
            columnheader1.Text = "Ville";
            columnheader1.Width = 150;
            columnheader1.TextAlign = HorizontalAlignment.Left;

           
            listView1.Columns.Add(columnheader0);
            listView1.Columns.Add(columnheader1);

        }

        /*private string CPParse(string data)
        {
            Regex myregex = new Regex("\\d{5}");
            var matches = myregex.Matches;

        }
        */
        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();

            System.Net.WebRequest obj = System.Net.HttpWebRequest.Create("http://www.codespostaux.com/home/r.php?q=" + textBox1.Text + "&Pays=FR");

            System.Net.WebResponse objResponse = obj.GetResponse();

            System.IO.StreamReader stream = null;

            try
            {
                stream = new System.IO.StreamReader(objResponse.GetResponseStream());

                string cleanedtext = HtmlSanitizer.getText(stream.ReadToEnd());
                textBox2.Text = cleanedtext;

                Regex cpregex = new Regex(@"(\d{5}?)([A-Z\s]*)");
                //Regex villeregex = new Regex(@"[A-Z\s]*");

                var cpmatches = cpregex.Matches(cleanedtext);
                //var villematches = villeregex.Matches(cleanedtext);

                //Console.Write(cleanedtext);

                MatchCollection matches = cpregex.Matches(cleanedtext);

                for (int i = 0; i != matches.Count; ++i)
                {
                    Console.Write(matches[i].Groups["1"].Value + " - " + matches[i].Groups["2"].Value + "\n");
                }
                
                    


            }

            
            catch { }

            finally
            {
                if (objResponse != null)
                {
                    objResponse.Close();
                }
            }


        }
    }
}
