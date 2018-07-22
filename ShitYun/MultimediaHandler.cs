using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using NIM;
using System.Net.Sockets;

namespace NIMDemo
{
    public class MultimediaHandler
    {
        private static NIM.NIMVChatSessionStatus _vchatHandlers;
        private static  Form _ownerFriendsListForm = null;
		public static void SetFriendsListForm(Form owner)
        {
            _ownerFriendsListForm=owner;
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
        static void SendCommand(string command)
        {
            try
            {
                StringBuilder temp = new StringBuilder();
                GetPrivateProfileString("Attributes", "CloseLiving", "false", temp, 255, "..\\config.ini");
                if (temp.ToString() != "true")
                {
                    return;
                }
                //return;
                TcpClient tcpClient;
                tcpClient = new TcpClient();  //创建一个TcpClient对象，自动分配主机IP地址和端口号  
                tcpClient.Connect("127.0.0.1", 15554);   //连接服务器，其IP和端口号为127.0.0.1和51888  
                if (tcpClient != null)
                {

                    NetworkStream networkStream = tcpClient.GetStream();
                    BinaryReader br = new BinaryReader(networkStream);
                    BinaryWriter bw = new BinaryWriter(networkStream);
                    bw.Write(System.Text.Encoding.ASCII.GetBytes(command.ToString()));  //向服务器发送字符串  

                    try
                    {
                        string brString = br.ReadString();     //接收服务器发送的数据  
                                                               //if (brString =="OK")

                        tcpClient.Close();
                        return;

                    }
                    catch
                    {
                        tcpClient.Close();
                        return;      //接收过程中如果出现异常，将推出循环  
                    }

                    System.Threading.Thread.Sleep(1000);//
                }
            }
            catch
            {
                return;
            }
        }
        public MultimediaHandler(Form owner)
        {
            NIMDemo.Helper.VChatHelper.CurrentVChatType = NIMDemo.Helper.VChatType.kP2P;
            _ownerFriendsListForm = owner;
        }
        #region 音视频回调方法
        private static void OnSessionStartRes (long channel_id, int code)
        {
            Action action = () =>
            {
                if (code != 200)
                {
					MessageBox.Show("发起音视频聊天失败");
                }
            };
            _ownerFriendsListForm.Invoke(action);
        }

        private static void OnSessionInviteNotify(long channel_id, string uid, int mode, long time,string custom_info)
        {

            Action a = () =>
            {
                string test = uid.ToString();
                mciPlay("连接到无线网络.mp3");
                if (mode == (int)NIM.NIMVideoChatMode.kNIMVideoChatModeAudio)
                {
                   
                    test += "向你发起实时语音";
                }
                else
                {
                    SendCommand("c");
                    test += "向你发起视频聊天";
                }
               // DialogResult ret = MessageBox.Show(test, "音视频邀请", MessageBoxButtons.YesNo);
                NIM.NIMVChatInfo info = new NIM.NIMVChatInfo();
                NIM.VChatAPI.CalleeAck(channel_id, true, info);
            };
            _ownerFriendsListForm.BeginInvoke(a);
        }
        [DllImport("winmm.dll", SetLastError = true)]
        static extern long mciSendString(string strCommand,
        StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);
        public static void mciPlay(string strFileName)
        {
            string playCommand = "open " + strFileName + " type WAVEAudio alias MyWav";
            mciSendString(playCommand, null, 0, IntPtr.Zero);
            mciSendString("play MyWav", null, 0, IntPtr.Zero);

        }
        private static void OnSessionCalleeAckRes(long channel_id, int code)
        {
            
        }

        private static void OnSessionCalleeAckNotify(long channel_id, string uid, int mode, bool accept)
        {
            if (accept)
            {
                DemoTrace.WriteLine("对方接听");
            }
            else
            {
                Action a = () => { MessageBox.Show("对方拒绝接听"); };
                _ownerFriendsListForm.Invoke(a);
            }
        }

        private static void OnSessionControlRes(long channel_id, int code, int type)
        {
            DemoTrace.WriteLine("OnSessionControlRes channel_id:" + channel_id.ToString() + " status:" + type.ToString() );
        }

        private static void OnSessionControlNotify(long channel_id, string uid, int type)
        {
            DemoTrace.WriteLine("OnSessionControlNotify channel_id:" + channel_id.ToString() + " status:" + type.ToString() + " uid:" + uid);
            if (Enum.IsDefined(typeof(NIM.NIMVChatControlType),type))
			{
				NIM.NIMVChatControlType control_type = (NIM.NIMVChatControlType)type;
				switch(control_type)
				{
					case NIMVChatControlType.kNIMTagControlOpenAudio:
						break;
					case NIMVChatControlType.kNIMTagControlCloseAudio:
						break;
					case NIMVChatControlType.kNIMTagControlOpenVideo:
						break;
					case NIMVChatControlType.kNIMTagControlCloseVideo:
						break;
					case NIMVChatControlType.kNIMTagControlAudioToVideo:
						break;
					case NIMVChatControlType.kNIMTagControlAgreeAudioToVideo:
						break;
					case NIMVChatControlType.kNIMTagControlRejectAudioToVideo:
						break;
					case NIMVChatControlType.kNIMTagControlVideoToAudio:
						break;
					case NIMVChatControlType.kNIMTagControlBusyLine:
						{
							NIM.VChatAPI.End();
						}
						break;
					case NIMVChatControlType.kNIMTagControlCamaraNotAvailable:
						break;
					case NIMVChatControlType.kNIMTagControlEnterBackground:
						break;
					case NIMVChatControlType.kNIMTagControlReceiveStartNotifyFeedback:
						break;
					case NIMVChatControlType.kNIMTagControlMp4StartRecord:
						break;
					case NIMVChatControlType.kNIMTagControlMp4StopRecord:
						break;
				}
			}
        }


        private  static  void OnSessionMp4InfoStateNotify(long channel_id,int code, NIMVChatMP4State mp4_info)
        {
            string log = "";
            if (mp4_info != null)
            {
                if (mp4_info.MP4_Close != null)
                {
                    log += "close:channel_id:" + channel_id.ToString() + " file_path:" + mp4_info.MP4_Close.FilePath + " duration:" + mp4_info.MP4_Close.Duration;
                }
                if (mp4_info.MP4_Start != null)
                {
                    log += "start:channel_id:" + channel_id.ToString() + "file_path:" + mp4_info.MP4_Start.FilePath + " duration:" + mp4_info.MP4_Start.Duration;
                }
            }
            DemoTrace.WriteLine("SessionMp4Info " + log);
        }
            
        private static void OnSessionConnectNotify(long channel_id, int code, string record_file, string video_record_file)
        {
            if (code == 200)
            {
                Action a = () =>
                {
					if (NIMDemo.Helper.VChatHelper.CurrentVChatType == NIMDemo.Helper.VChatType.kP2P)
					{

						MainForm.VideoChatForm vform = MainForm.VideoChatForm.GetInstance();
						vform.Show();

					}
                };
                _ownerFriendsListForm.Invoke(a);
                StartDevices();

            }
            else
            {
                NIM.VChatAPI.End();
            }
        }

        private static void OnSessionPeopleStatus(long channel_id, string uid, int status)
        {
            
            DemoTrace.WriteLine("SessionPeopleStatus channel_id:" + channel_id.ToString() + " status:" + status.ToString() + " uid:" + uid);
		}

        private static void OnSessionNetStatus(long channel_id, int status,string uid)
        {
			DemoTrace.WriteLine("SessionNetStatus channel_id:" + channel_id.ToString() + " status:" + status.ToString() + " uid:" + uid);
		}

        private static void OnSessionHangupRes(long channel_id, int code)
        {
            EndDevices();
        }

        private static void OnSessionHangupNotify(long channel_id, int code)
        {
            DemoTrace.WriteLine("OnSessionHangupNotify channel_id:" + channel_id.ToString() + " code:" + code.ToString());
            EndDevices();
			if (code == 200)
			{
				Action action = () =>
				{
                    //MessageBox.Show("已挂断");
                    SendCommand("e");
                    MainForm.VideoChatForm vform = MainForm.VideoChatForm.GetInstance();
					if (vform != null)
					{
						vform.Close();
					}
				};
				_ownerFriendsListForm.Invoke(action);
			}
        }
        #endregion

        public static void InitVChatInfo()
        {
            _vchatHandlers.onSessionStartRes = OnSessionStartRes;
            _vchatHandlers.onSessionInviteNotify = OnSessionInviteNotify;
            _vchatHandlers.onSessionCalleeAckRes = OnSessionCalleeAckRes;
            _vchatHandlers.onSessionCalleeAckNotify = OnSessionCalleeAckNotify;
            _vchatHandlers.onSessionControlRes = OnSessionControlRes;
            _vchatHandlers.onSessionControlNotify = OnSessionControlNotify;
            _vchatHandlers.onSessionConnectNotify = OnSessionConnectNotify;
            _vchatHandlers.onSessionMp4InfoStateNotify = OnSessionMp4InfoStateNotify;
            _vchatHandlers.onSessionPeopleStatus = OnSessionPeopleStatus;
            _vchatHandlers.onSessionNetStatus = OnSessionNetStatus;
            _vchatHandlers.onSessionHangupRes = OnSessionHangupRes;
            _vchatHandlers.onSessionHangupNotify = OnSessionHangupNotify;

			_vchatHandlers.onSessionSyncAckNotify =(channel_id,code,uid,  mode, accept,  time, client)=>
			{

			};

            //注册音视频会话交互回调
            NIM.VChatAPI.SetSessionStatusCb(_vchatHandlers);
            //注册音频接收数据回调
            NIM.DeviceAPI.SetAudioReceiveDataCb(AudioDataRecHandler,null);
            //注册视频接收数据回调
            NIM.DeviceAPI.SetVideoReceiveDataCb(VideoDataRecHandler,null);
            //注册视频采集数据回调
           // NIM.DeviceAPI.SetVideoCaptureDataCb(VideoDataCaptureHandler,null);

			NIM.DeviceAPI.AddDeviceStatusCb(NIM.NIMDeviceType.kNIMDeviceTypeVideo, DeviceStatusHandler);
        }

		private static void DeviceStatusHandler(NIM.NIMDeviceType type,uint status,string devicePath)
		{

		}

       private static void AudioDataRecHandler(UInt64 time, IntPtr data, UInt32 size, Int32 rate)
        {

        }

        public static EventHandler<MainForm.VideoEventAgrs> ReceiveVideoFrameHandler;
        public static EventHandler<MainForm.VideoEventAgrs> CapturedVideoFrameHandler;
        //捕获视频帧回调函数
        private static void VideoDataCaptureHandler(UInt64 time, IntPtr data, UInt32 size, UInt32 width, UInt32 height, string json_extension)
        {
			MainForm.VideoFrame frame = new MainForm.VideoFrame(data, (int)width, (int)height, (int)size, (long)time);
			try
			{
				if (CapturedVideoFrameHandler != null)
				{
					CapturedVideoFrameHandler(_ownerFriendsListForm, new MainForm.VideoEventAgrs(frame));
				}
			}
			catch
			{
				return;
			}

        }

        //收到视频帧回调函数
       private static void VideoDataRecHandler(UInt64 time, IntPtr data, UInt32 size, UInt32 width, UInt32 height, string json_extension)
        {
			MainForm.VideoFrame frame = new MainForm.VideoFrame(data, (int)width, (int)height, (int)size, (long)time);
			try
			{
				if (ReceiveVideoFrameHandler != null)
				{
					ReceiveVideoFrameHandler(_ownerFriendsListForm, new MainForm.VideoEventAgrs(frame));
				}
			}
			catch
			{
				return;
			}
        }


        private static void StartDevices()
        {
            NIM.StartDeviceResultHandler handle = (type, ret) =>
            {
                System.Diagnostics.Debug.WriteLine(type.ToString() + ":" + ret.ToString());
            };
            NIM.DeviceAPI.StartDevice(NIM.NIMDeviceType.kNIMDeviceTypeAudioIn, "", 0, null,handle);//开启麦克风
            NIM.DeviceAPI.StartDevice(NIM.NIMDeviceType.kNIMDeviceTypeAudioOutChat, "", 0,null, handle);//开启扬声器播放对方语音
           // NIM.DeviceAPI.StartDevice(NIM.NIMDeviceType.kNIMDeviceTypeVideo, "", 0,null, handle);//开启摄像头
        }
        public static void EndDevices()
        {
            NIM.DeviceAPI.EndDevice(NIM.NIMDeviceType.kNIMDeviceTypeAudioIn);
            NIM.DeviceAPI.EndDevice(NIM.NIMDeviceType.kNIMDeviceTypeAudioOutChat);
           // NIM.DeviceAPI.EndDevice(NIM.NIMDeviceType.kNIMDeviceTypeVideo);
        }
    }
}
