using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Security.Principal;
using System.Diagnostics;
using System.Threading;

namespace KmdScript
{
    public partial class Form1 : Form
    {
        string[] _args;
        public Form1(string[] args)
        {
            InitializeComponent();
            _args = args;
        }
        // variables:
        int words = 0;
        string currentfile = string.Empty;
        int value2;
        string username = WindowsIdentity.GetCurrent().Name.Split('\\').Last();
        Thread thread = Thread.CurrentThread;
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            words = richTextBox1.TextLength;
            Number.Text = words.ToString();
            if (richTextBox1.Text.Length == 1)
            {
                words = 1;
            }
            if (richTextBox1.Text != string.Empty)
            {
               foreach (string a in _args)
               {
                    words++;
               }
            }
        }
        // codes for Filemenu
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentfile == "") saveAsToolStripMenuItem_Click(sender, e);
            else
                richTextBox1.SaveFile(currentfile, RichTextBoxStreamType.RichText);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
                if (currentfile == "")
                {
                    saveFileDialog1.FileName = "Noname";
                   
                }
                if (DialogResult.OK == saveFileDialog1.ShowDialog())
                {
                     
                    if (Path.GetExtension(saveFileDialog1.FileName) == ".cs" || Path.GetExtension(saveFileDialog1.Filter) == ".rtf" || Path.GetExtension(saveFileDialog1.Filter) == ".kse" || Path.GetExtension(saveFileDialog1.Filter) == ".html")
                        richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                    else
                        richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.RichText);
                    currentfile = saveFileDialog1.FileName;

                    this.Text = Path.GetFileName(currentfile) + " - KmdScript";

                }

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(username == "omare")
            {
                try
                {

                    // If the user selected a file...
                    if (DialogResult.OK == openFileDialog1.ShowDialog())
                    {
                        // get the name of the selected file    
                        currentfile = openFileDialog1.FileName;


                        // If the file is of plain text format...
                        if (Path.GetExtension(openFileDialog1.Filter) == ".cs" || Path.GetExtension(openFileDialog1.Filter) == ".rtf" || Path.GetExtension(openFileDialog1.Filter) == ".kse" || Path.GetExtension(openFileDialog1.Filter) == ".html")
                            richTextBox1.LoadFile(currentfile, RichTextBoxStreamType.PlainText);



                        // If the file of rich text type...
                        else
                            // load the file's content into the rich textbox as plain text
                            richTextBox1.LoadFile(currentfile, RichTextBoxStreamType.RichText);
                        // Add the file name to the window title
                        this.Text = Path.GetFileName(currentfile) + " - Kmd script";

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                if (((DialogResult.OK == MessageBox.Show("Acces is denied", "Exit", MessageBoxButtons.OK))))
                {
                    Application.Exit();
                }
            }

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != string.Empty)

            {


                // If the user agrees to losing content...
                if (((DialogResult.OK == MessageBox.Show("The content may be lost.", "Continue?", MessageBoxButtons.OKCancel))))
                {
                    // set current file to none
                    Application.Restart();
                }
            }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // codes for EditMenu
        private void textColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == colorDialog1.ShowDialog())
            {
                int selection = richTextBox1.ForeColor.ToArgb();
                richTextBox1.ForeColor = colorDialog1.Color;
                richTextBox1.SelectionColor = System.Drawing.Color.FromArgb(selection);
            }
        }

        private void screenColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == colorDialog2.ShowDialog())
            {
                richTextBox1.BackColor = colorDialog2.Color;
            }
        }

        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.ForeColor != Color.Black)
            {
                richTextBox1.ForeColor = colorDialog1.Color;
                richTextBox1.BackColor = Color.Black;
            }
            if (richTextBox1.ForeColor == Color.Black)
            {
                richTextBox1.ForeColor = Color.White;
                richTextBox1.BackColor = Color.Black;
            }
        }

        private void programingThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.ForeColor = Color.Lime;
            richTextBox1.BackColor = Color.Black;
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Font myFont = fontDialog1.Font;
            if (DialogResult.OK == fontDialog1.ShowDialog())
            {
                richTextBox1.Font = myFont;
                richTextBox1.SelectionFont = new Font(myFont.Name, myFont.Size);
                richTextBox1.Font = new Font(myFont.Name, myFont.Size);
                numericUpDown1.Value = (decimal)fontDialog1.Font.Size;
            }

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            value2 = (int)numericUpDown1.Value;
            value2 += (int)richTextBox1.Font.Size;
            richTextBox1.SelectionFont = new Font(Font.Name, value2);

        }

        private void lightThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
                richTextBox1.ForeColor = Color.Black;
                richTextBox1.BackColor = Color.White;
        }

        private void ClearSearch_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = 0;
            richTextBox1.SelectAll();
            richTextBox1.SelectionBackColor = richTextBox1.BackColor;
            textBox1.Text = null;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
         
        }

        private void highlightColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
          colorDialog3.ShowDialog();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            string[] search = textBox1.Text.Split('+');
            foreach (string s in search)
            {
                int startIndex = 0;
                while (startIndex < richTextBox1.TextLength)
                {
                    int wordstartindex = richTextBox1.Find(s, startIndex, RichTextBoxFinds.None);
                    if (wordstartindex != -1)
                    {
                        richTextBox1.SelectionStart = wordstartindex;
                        richTextBox1.SelectionLength = s.Length;
                        richTextBox1.SelectionBackColor = colorDialog3.Color;
                    }
                    else
                        break;
                    startIndex += wordstartindex + s.Length;
                }
            }

        }
        private void CreateExtend()
        {
            if (username     == "omare")
            {
                string AppName = "KmdScript";
                string exePath = string.Format("\"{0}\"", Application.ExecutablePath);
                string exename = System.AppDomain.CurrentDomain.FriendlyName;
                Registry.ClassesRoot.CreateSubKey(".kse").SetValue("", AppName);
                Registry.ClassesRoot.CreateSubKey(AppName + @"\Kmd script extension icon").SetValue("", exePath);
                Registry.ClassesRoot.CreateSubKey(AppName + @"\Shell\open\command").SetValue("", exePath + "\"%1\"");
                Registry.ClassesRoot.CreateSubKey(AppName + @"\Shell\edit\command").SetValue("", exePath + "\"%1\"");

                Registry.ClassesRoot.CreateSubKey(string.Format(@"Applications\{0}\Shell\open\command", exename)).SetValue("", exePath + "\"%1\"");
                Registry.ClassesRoot.CreateSubKey(string.Format(@"Applications\{0}\Shell\edit\command", exename)).SetValue("", exePath + "\"%1\"");
            }
            else
            {
                if (((DialogResult.OK == MessageBox.Show("Acces is denied","Exit", MessageBoxButtons.OK))))
                {
                    Application.Exit();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (username == "omare")
            {
                if (_args.Length > 0)   
                {
                    if (File.Exists(_args[0]))
                    {
                        richTextBox1.Text = File.ReadAllText(_args[0]);

                        // If the file is of plain text format...
                        if (Path.GetExtension(openFileDialog1.Filter) == ".cs" || Path.GetExtension(openFileDialog1.Filter) == ".rtf" || Path.GetExtension(openFileDialog1.Filter) == ".kse" || Path.GetExtension(openFileDialog1.Filter) == ".html")
                            richTextBox1.LoadFile(_args[0], RichTextBoxStreamType.PlainText);

                        // If the file of rich text type...
                        else
                            // load the file's content into the rich textbox as plain text
                            richTextBox1.LoadFile(_args[0], RichTextBoxStreamType.RichText);
                        // Add the file name to the window title
                        this.Text = Path.GetFileName(_args[0]) + " - Kmd script";
                        // The Hello "UserName" Text:
                        Random random = new Random();
                        int r = random.Next(10, 500);
                        int r2 = random.Next(10, 72);
                        label2.Text = "Hello " + username + "!";
                        label2.Location = new Point(r, r);
                        label2.Font = new Font("Bold", r2);
                        timer1.Interval = 3000; // 5 seconds
                        timer1.Tick += new EventHandler(timer1_Tick);
                        timer1.Start();

                    }
                }
                try
                {
                    CreateExtend();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error bedan comes and say acces to .kse is denied,don't care");
                }
            }
            else
            {
                if (((DialogResult.OK == MessageBox.Show("Acces is denied", "Exit", MessageBoxButtons.OK))))
                {
                    Application.Exit();
                }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
                label2.Text = "";
        }

        private void richTextBox1_Enter(object sender, EventArgs e)
        {
            // The Hello "UserName" Text:
            Random random = new Random();
            int r = random.Next(10, 500);
            int r2 = random.Next(10, 72);
            label2.Text = "Hello " + username + "!";
            label2.Location = new Point(r, r);
            label2.Font = new Font("Bold", r2);
            timer1.Interval = 3000; // 5 seconds
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
