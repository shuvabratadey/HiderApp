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

namespace Hider
{
    public partial class Form1 : Form
    {
        string path = @"C:\HiderApp\pass.cat";
        string mainFile = @"C:\HiderApp\mainFile.cat";
        bool New = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists(path))
            {
                Directory.CreateDirectory(@"C:\HiderApp");
                label3.Text = "ENTER NEW PASSWORD";
                New = true;
            }
            else
            {
                New = false;
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (New == true)
            {
                if (textBox1.Text.Length != 0)
                {
                    File.WriteAllText(path, textBox1.Text);
                    File.SetAttributes(path, FileAttributes.Hidden | FileAttributes.System | FileAttributes.ReadOnly);
                    File.WriteAllText(mainFile, "");
                    File.SetAttributes(mainFile, FileAttributes.Hidden | FileAttributes.System | FileAttributes.ReadOnly);
                    Form1 obj = new Form1();
                    obj.Hide();
                    MainForm mainForm = new MainForm();
                    mainForm.Show();
                }
                else
                {
                    MessageBox.Show("Innvalid pass");
                }
            }
            else
            {
                if(textBox1.Text==File.ReadAllText(path))
                {
                    
                    MainForm mainForm = new MainForm();
                    mainForm.Show();
                }
                else
                {
                    MessageBox.Show("Invalid Password");
                }
            }
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
