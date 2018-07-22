using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShitYun
{
    public partial class logOut : Form
    {
        private volatile static logOut _instance = null;
        private static readonly object lockHelper = new object();
        public logOut()
        {
            InitializeComponent();
        }
        public static logOut CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new logOut();
                }
            }
            return _instance;
        }
        public  void SetOutput(string text)
        {
            Action action = () =>
            {
                _instance.richTextBox1.AppendText(text);
                _instance.richTextBox1.AppendText(System.Environment.NewLine);
                //滚到最后
                //_instance.richTextBox1.Select(richTextBox1.TextLength, 0);
                //_instance.richTextBox1.Focus();

            };

            _instance.richTextBox1.Invoke(action);
        }
        public static void SetText(string text)
        {
            if (_instance != null)
            {
                _instance.SetOutput(text);
            }
        }

        private void logOut_Load(object sender, EventArgs e)
        {

        }

        private void logOut_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
