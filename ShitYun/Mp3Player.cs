using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ShitYun
{
    public class Mp3Player
    {
        #region - 属性 -
        [DllImport("winmm.dll")]
        static extern Int32 mciSendString(String command,
          StringBuilder buffer, Int32 bufferSize, IntPtr hwndCallback);

        /// <summary>
        /// 临时音乐文件存放处
        /// </summary>
        private string m_musicPath = "";

        /// <summary>
        /// 父窗体句柄
        /// </summary>
        private IntPtr m_Handle;
        #endregion

        #region - 构造函数 -
        /// <summary>
        /// 创建Mp3播放类
        /// </summary>
        /// <param name="music">嵌入的音乐文件</param>
        /// <param name="path">临时音乐文件保存路径</param>
        /// <param name="Handle">父窗体句柄</param>
        public Mp3Player(Byte[] music, string path, IntPtr Handle)
        {
            try
            {
                m_Handle = Handle;
                m_musicPath = Path.Combine(path, "temp.mp3");
                FileStream fs = new FileStream(m_musicPath, FileMode.Create);
                fs.Write(music, 0, music.Length);
                fs.Close();
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 创建Mp3播放类
        /// </summary>
        /// <param name="musicPath">要播放的mp3文件路径</param>
        /// <param name="Handle">父窗体句柄</param>
        public Mp3Player(string musicPath, IntPtr Handle)
        {
            m_musicPath = musicPath;
            m_Handle = Handle;
        }

        public Mp3Player(Byte[] music, IntPtr Handle) :
            this(music, @"C:\Windows\", Handle)
        {

        }

        #endregion

        #region - 播放音乐 -
        public void Open(string path)
        {
            if (path != "")
            {
                try
                {
                    mciSendString("open " + path + " alias media", null, 0, m_Handle);
                    mciSendString("play media", null, 0, m_Handle);
                }
                catch (Exception)
                {

                }
            }
        }

        public void Open()
        {
            Open(m_musicPath);
        }
        #endregion

        #region - 停止音乐播放 -
        void CloseMedia()
        {
            try
            {
                mciSendString("close all", null, 0, m_Handle);
            }
            catch (Exception)
            {
            }
        }
        #endregion
    }
}
