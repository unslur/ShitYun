using NIM;
using NIM.DataSync;
using NIM.SysMessage;
using NIMDemo;
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
    public partial class MainForm : Form
    {
        private static string _selfId;
        private bool _notifyNetworkDisconnect = true;
        private bool _beKicked = false;
        public MainForm()
        {
            InitializeComponent();
            this.HandleCreated += FriendsListForm_HandleCreated;
        }
        public MainForm(string id)
          : this()
        {
            _selfId = id;
            NIMDemo.Helper.UserHelper.SelfId = id;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        private void RegisterClientCallback()
        {
            NIM.ClientAPI.RegDisconnectedCb(() =>
            {
                if (_notifyNetworkDisconnect)
                    MessageBox.Show("网络连接断开，网络恢复后后会自动重连");
            });

          

            NIM.ClientAPI.RegAutoReloginCb((r) =>
            {
                if (r.Code == ResponseCode.kNIMResSuccess && r.LoginStep == NIMLoginStep.kNIMLoginStepLogin)
                {
                    MessageBox.Show("重连成功");
                }
            });
            ClientCallbacks.Register();
        }
        /// <summary>
        /// 注册全局回调
        /// </summary>
        private void RegisterNimCallback()
        {
           // NIM.Friend.FriendAPI.FriendProfileChangedHandler += OnFriendChanged;
            //NIM.User.UserAPI.UserRelationshipListSyncHander += OnUserRelationshipSync;
           // NIM.User.UserAPI.UserRelationshipChangedHandler += OnUserRelationshipChanged;
           // NIM.User.UserAPI.UserNameCardChangedHandler += OnUserNameCardChanged;
          //  NIM.ClientAPI.RegMulitiportPushEnableChangedCb(SyncMultipushState);
          //  NIM.TalkAPI.OnReceiveMessageHandler += OnReceiveMessage;
           // NIM.TalkAPI.RegRecallMessageCallback(OnRecallMessage);
         //   NIM.TalkAPI.RegReceiveBroadcastCb(OnReceiveBroadcast);
          //  NIM.TalkAPI.RegReceiveBroadcastMsgsCb(OnReceiveBroadMsgs);
            NIM.SysMessage.SysMsgAPI.ReceiveSysMsgHandler += OnReceivedSysNotification;
            NIM.DataSync.DataSyncAPI.RegCompleteCb(OnDataSyncCompleted);
        }
        private void OnDataSyncCompleted(NIMDataSyncType syncType, NIMDataSyncStatus status, string jsonAttachment)
        {
            if (syncType == NIMDataSyncType.kNIMDataSyncTypeTeamInfo)
            {
              //  _teamList.LoadTeams();
            }
            if (syncType == NIMDataSyncType.kNIMDataSyncTypeTeamUserList)
            {
                NIM.Team.TeamAPI.QueryTeamMembersInfo(jsonAttachment, true, false, (a, b, c, d) =>
                {

                });
            }
        }
        private void OnReceivedSysNotification(object sender, NIMSysMsgEventArgs e)
        {
            if (e.Message == null || e.Message.Content == null)
                return;
            DemoTrace.WriteLine("系统通知:" + e.Dump());
            if (e.Message.Content.MsgType == NIMSysMsgType.kNIMSysMsgTypeTeamInvite)
            {
                NIM.Team.TeamAPI.AcceptTeamInvitation(e.Message.Content.ReceiverId, e.Message.Content.SenderId, (x) =>
                {

                });
            }
        }
        //private readonly InvokeActionWrapper _actionWrapper;
        //void OnReceiveMessage(object sender, NIMReceiveMessageEventArgs args)
        //{
        //    DisplayReceivedMessage(args.Message.MessageContent);
        //    DemoTrace.WriteLine(args.Dump());
        //}
        //void DisplayReceivedMessage(NIMIMMessage msg)
        //{
        //    var sid = msg.SenderID;
        //    var msgType = msg.MessageType;
        //    Action action = () =>
        //    {
        //        ListViewItem item = new ListViewItem(sid);
        //        if (msgType != NIMMessageType.kNIMMessageTypeText)
        //            item.SubItems.Add(msgType.ToString());
        //        else
        //        {
        //            var m = msg as NIM.NIMTextMessage;
        //            item.SubItems.Add(m.TextContent);
        //        }
        //        item.Tag = msg;
        //        chatListView.Items.Add(item);
        //    };
        //    _actionWrapper.InvokeAction(action);
        //}
        //private void SyncMultipushState(ResponseCode code, ConfigMultiportPushParam param)
        //{
        //    _actionWrapper.InvokeAction(() =>
        //    {
        //        if (code == ResponseCode.kNIMResSuccess)
        //        {
        //            multipushCheckbox.Checked = param.Enabled;
        //        }
        //        else
        //        {
        //            MessageBox.Show("MultiportPush 操作失败:" + code.ToString());
        //        }
        //    });
        //}
        private void FriendsListForm_HandleCreated(object sender, EventArgs e)
        {
          
            RegisterNimCallback();
            //NIM.ClientAPI.IsMultiportPushEnabled(InitMultipushState);
           // multipushCheckbox.CheckedChanged += MultipushCheckbox_CheckedChanged;
            //_teamList.LoadTeams();
            //_sessionList.GetRecentSessionList();
           // _recentSessionList.LoadSessionList();

            //_rtsHandler = new RtsHandler(this);

            //NIM.TalkAPI.RegReceiveBatchMessagesCb((list) =>
            //{
            //    foreach (var m in list)
            //    {
            //        DisplayReceivedMessage(m.MessageContent);
            //    }
            //});

            //this.Text = string.Format("{0}  [{1}]", this.Text, _selfId);

            InitVChat();
            //LivingStreamForm form = new LivingStreamForm();
            //form.Show();
        }
        private readonly InvokeActionWrapper _actionWrapper;
        //private void InitMultipushState(ResponseCode code, ConfigMultiportPushParam param)
        //{
        //    _actionWrapper.InvokeAction(() =>
        //    {
        //        if (code == ResponseCode.kNIMResSuccess)
        //        {
        //            //multipushCheckbox.Checked = param.Enabled;
        //        }
        //    });
        //}
        private bool _nrtcInit = false;
        private MultimediaHandler _multimediaHandler = null;
        private void InitVChat()
        {
            System.Threading.ThreadPool.QueueUserWorkItem((args) =>
            {
                try
                {
                    if (_nrtcInit = NIM.VChatAPI.Init(""))
                    {
                        _multimediaHandler = new MultimediaHandler(this);
                        MultimediaHandler.InitVChatInfo();
                    }
                    else
                    {
                        MessageBox.Show("NIM VChatAPI init failed!");
                    }

                }
                catch (Exception exception)
                {
                    System.Diagnostics.Debug.WriteLine(exception.ToString());
                }
            });
        }
    }
}
