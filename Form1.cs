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

        }

        /*private string CPParse(string data)
        {
            Regex myregex = new Regex("\\d{5}");
            var matches = myregex.Matches;

        }
        */
        private void button1_Click(object sender, EventArgs e)
        {
            System.Net.WebRequest obj = System.Net.HttpWebRequest.Create("http://www.codesposte.fr/index.php?recherche=vannes");

            System.Net.WebResponse objResponse = obj.GetResponse();

            System.IO.StreamReader stream = null;

            try
            {
                stream = new System.IO.StreamReader(objResponse.GetResponseStream());

                string cleanedtext = HtmlSanitizer.getText(stream.ReadToEnd());
                //textBox2.Text = cleanedtext;
                Regex cpregex = new Regex("\\d{5}");
                Regex villeregex = new Regex("[A-Z].");

                var cpmatches = cpregex.Matches(cleanedtext);
                var villematches = villeregex.Matches(cleanedtext);


                foreach (Match match in cpmatches)
                {
                    Console.Write(match.Value + "\n");
                }
                    foreach (Match vmatch in villematches)
                    {
                        listBox1.Text = vmatch.Value + "\n";
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
