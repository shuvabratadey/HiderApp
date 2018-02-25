using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace Hider
{
    public partial class MainForm : Form
    {
        string mainFile = @"C:\HiderApp\mainFile.cat";
        static string mainStr = null;
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog(); // Show the dialog.
            openFileDialog1.InitialDirectory = "d:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|png files (*.png)|*.png|jpeg files (*.jpeg)|*.jpeg|mp3 files (*.mp3)|*.mp3|mp4 files (*.mp4)|*.mp4|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 6;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK) // Test result.
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (File.Exists(textBox1.Text))
            {
                FileAttributes attributes = File.GetAttributes(textBox1.Text);
                if ((attributes & FileAttributes.Hidden) != FileAttributes.Hidden && (attributes & FileAttributes.System) != FileAttributes.System && (attributes & FileAttributes.ReadOnly) != FileAttributes.ReadOnly)
                {
                    File.SetAttributes(mainFile, FileAttributes.Normal);
                    File.AppendAllText(mainFile, textBox1.Text + Environment.NewLine);
                    File.SetAttributes(mainFile, FileAttributes.Hidden | FileAttributes.System | FileAttributes.ReadOnly);
                    File.SetAttributes(textBox1.Text, FileAttributes.ReadOnly | FileAttributes.Hidden | FileAttributes.System);
                    this.Ref();
                }
            }
            else MessageBox.Show("File is Not Exists");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (File.Exists(textBox1.Text))
            {
                File.SetAttributes(textBox1.Text, FileAttributes.Normal);
                this.Ref();
            }
            else MessageBox.Show("File is Not Exists");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Ref();
        }
        public void Ref()
        {
            File.SetAttributes(mainFile, FileAttributes.Normal);
            comboBox1.Items.Clear();
            string[] strArr = File.ReadAllLines(mainFile);
                for (int i = 0; i < strArr.Length; i++)
                {
                    if (File.Exists(strArr[i]))
                    {
                        FileAttributes attributes = File.GetAttributes(strArr[i]);
                        if ((attributes & FileAttributes.Hidden) == FileAttributes.Hidden && (attributes & FileAttributes.System) == FileAttributes.System && (attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                        {
                            mainStr += strArr[i] + "\n";
                            comboBox1.Items.Add(strArr[i]);
                        }
                    }
                }
             File.WriteAllText(mainFile, mainStr);
            File.SetAttributes(mainFile, FileAttributes.Hidden | FileAttributes.System | FileAttributes.ReadOnly);
             mainStr = "";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = comboBox1.Text;
        }
    }
}
