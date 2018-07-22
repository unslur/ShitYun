using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShitYun;
using NimUtility;
using NIM;
using NIMDemo;
using System.Runtime.InteropServices;

namespace ShitYun
{
    
public partial class LoginForm : Form
    {
        private AccountCollection _accounts;
        private string _userName;
        private string _password;
        public static LoginForm LoginFormIstance;
        public LoginForm()
        {
            InitializeComponent();
        }
       
    private void Form1_Load(object sender, EventArgs e)
        {
            // OutputForm.Instance.Show();
            logOut.CreateInstance().Show();
            button1_Click(sender,e);
        }
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(
            string lpAppName,
            string lpKeyName,
            string lpDefault,
            StringBuilder lpReturnedString,
            int nSize,
            string lpFileName
            );
        [DllImport("winmm.dll", SetLastError = true)]
        static extern long mciSendString(string strCommand,
        StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);
        public static void mciPlay(string strFileName)
        {
            string playCommand = "open " + strFileName + " type WAVEAudio alias MyWav";
            mciSendString(playCommand, null, 0, IntPtr.Zero);
            mciSendString("play MyWav", null, 0, IntPtr.Zero);

        }
        private void button1_Click(object sender, EventArgs e)
        {
            //必须保证 NIM.ClientAPI.Init 调用成功
            mciPlay("连接到无线网络.mp3");
            if (!InitSdk())
                return;
            StringBuilder temp = new StringBuilder();
            
            GetPrivateProfileString("Attributes", "devicename", "", temp, 255, "..\\config.ini");
           
            _userName = temp.ToString();
            this.username.Text = _userName;
            //_userName = "device001";
            _password =this.password.Text;
            //使用明文密码或者其他加密方式请修改此处代码
            //var password = NIM.ToolsAPI.GetMd5(_password);
            var password = _password;
            if (!string.IsNullOrEmpty(_userName) && !string.IsNullOrEmpty(password))
            {
               
                if (string.IsNullOrEmpty(_appKey))
                {
                    MessageBox.Show("请设置app key");
                    return;
                }
                NIM.ClientAPI.Login(_appKey, _userName, password, HandleLoginResult);
            }
        }
        /// <summary>
        /// 登录结果处理
        /// </summary>
        /// <param name="result"></param>
        private void HandleLoginResult(NIM.NIMLoginResult result)
        {
            DemoTrace.WriteLine(result.LoginStep.ToString());
            Action action = () =>
            {
                

               
                if (result.LoginStep == NIM.NIMLoginStep.kNIMLoginStepLogin)
                {
                   
                    if (result.Code == NIM.ResponseCode.kNIMResSuccess)
                    {
                        this.Hide();
                        new MainForm(_userName).Show();
                      
                      
                        //PublishLoginEvent();

                    }
                    else
                    {
                       // NIM.ClientAPI.Logout(NIM.NIMLogoutType.kNIMLogoutChangeAccout, HandleLogoutResult);
                    }
                }
            };
            this.Invoke(action);
        }
        private string _appKey = null;
        private bool InitSdk()
        {
            //读取配置信息,用户可以自己定义配置文件格式和读取方式，使用默认配置项config设置为null即可
            var config = ConfigReader.GetSdkConfig();

            if (!NIM.ClientAPI.Init(config.AppKey, "NIMCSharpDemo", null, config))
            {
                MessageBox.Show("NIM init failed!");
                return false;
            }
            _appKey = config.AppKey;
            return true;
        }
    }
}
