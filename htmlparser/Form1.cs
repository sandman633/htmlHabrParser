using htmlparser.Core;
using htmlparser.Core.Habra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace htmlparser
{
    public partial class Form1 : Form
    {

        ParserWorker<string[]> parser;
        public Form1()
        {
            InitializeComponent();
            parser = new ParserWorker<string[]>(new HabraParser());
            parser.OnNewDataRef += Parser_OnNewDataRef;
            parser.OnNewData += Parser_OnNewData;
            
        }

        private void Parser_OnNewDataRef(object arg1, string[] arg2)
        {
            listBox2.Items.AddRange(arg2);
        }

        private void Parser_OnComplete(object obj)
        {
            MessageBox.Show("End!");
        }

        private void Parser_OnNewData(object arg1, string[] arg2)
        {
            listBox1.Items.AddRange(arg2);
        }

        private void butStart_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            parser.Settings = new HabraSettings((int)numericUpDown1.Value,(int)numericUpDown2.Value);
            parser.Start();
        }

        private void butStop_Click(object sender, EventArgs e)
        {
            parser.Stop();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Process.Start($"{listBox2.SelectedItem}");
        }
        private void Scroll(object sender, EventArgs e)
        {
            if (sender is ListBox)
                SenderBox(sender as ListBox);
        }
        private void SenderBox(ListBox lb)
        {
            listBox1.TopIndex = lb.TopIndex;
            listBox2.TopIndex = lb.TopIndex;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Scroll(sender, e);
            listBox2.SelectedIndex = listBox1.SelectedIndex;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox2.Visible = false;
        }
    }
}
