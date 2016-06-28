using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace TextConverter
{
    

    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
        }

        private void Form1_Load(object sender, EventArgs e) { }

        public string[] GetTextBoxLines()
        {
            return this.richTextBox1.Lines;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //richTextBox2.ReadOnly = false;
            string[] worktext = GetTextBoxLines();
            Convertor director = new Convertor();
            
            if (worktext.Length == 0)
            {
                MessageBox.Show("Error! Text field is empty!");
            }

            else
            {
                if (comboBox1.SelectedItem == null)
                    MessageBox.Show("Error! Choose format!");
                else
                {
                    Builder b1;
                    if (comboBox1.SelectedItem.ToString() == "HTML")
                        b1 = new HTMLBuilder();
                    else
                        b1 = new MarkdownBuilder();

                    director.Construct(b1, worktext);
                    Product p1 = b1.GetResult();
                    this.richTextBox2.Text = p1.Show();
                }
            }
        }

        private void Open_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() ==  DialogResult.OK)  
            {
                string path = openFileDialog1.FileName; StreamReader sr = new StreamReader(path,   Encoding.Default);
                richTextBox1.Text = sr.ReadToEnd(); sr.Close();
                Text = "Text Editor - " +   Path.GetFileName(path);
                saveFileDialog1.FileName = path; openFileDialog1.FileName = "";
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() ==   DialogResult.OK)
            { string path = saveFileDialog1.FileName; SaveToFile(path); Text = "Text Editor - " +   Path.GetFileName(path); }
        }

        private void SaveToFile(string path)
        {
            StreamWriter sw = new StreamWriter(path, false, Encoding.Default); sw.WriteLine(richTextBox2.Text); sw.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
