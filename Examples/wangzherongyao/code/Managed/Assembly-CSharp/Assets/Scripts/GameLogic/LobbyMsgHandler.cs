﻿namespace Assets.Scripts.GameLogic
{
    using Apollo;
    using Assets.Scripts.Common;
    using Assets.Scripts.Framework;
    using Assets.Scripts.GameSystem;
    using Assets.Scripts.UI;
    using CSProtocol;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Text;
    using UnityEngine;

    [MessageHandlerClass]
    public class LobbyMsgHandler
    {
        [CompilerGenerated]
        private static CTimer.OnTimeUpHandler <>f__am$cache2;
        private static bool bVisitorSvr;

        private static void DNSAsyncCallback(IAsyncResult ar)
        {
            IPAddress[] addressArray = Dns.EndGetHostAddresses(ar);
            if ((addressArray != null) && (addressArray.Length != 0))
            {
                ApolloConfig.loginOnlyIp = addressArray[0].ToString();
            }
        }

        public static void ForceKillCrystal(int Camp)
        {
            List<PoolObjHandle<ActorRoot>> gameActors = Singleton<GameObjMgr>.instance.GameActors;
            int count = gameActors.Count;
            for (int i = 0; i < count; i++)
            {
                PoolObjHandle<ActorRoot> handle = gameActors[i];
                if ((((handle != 0) && (handle.handle.ActorControl != null)) && ((handle.handle.TheActorMeta.ActorType == ActorTypeDef.Actor_Type_Organ) && (handle.handle.TheActorMeta.ActorCamp == Camp))) && (handle.handle.TheStaticData.TheOrganOnlyInfo.OrganType == 2))
                {
                    handle.handle.ValueComponent.actorHp = 0;
                    break;
                }
            }
        }

        public static void HandleGameSettle(bool bSuccess, bool bShouldDisplayWinLose, byte GameResult, COMDT_SETTLE_HERO_RESULT_DETAIL heroSettleInfo, COMDT_RANK_SETTLE_INFO rankInfo, COMDT_ACNT_INFO acntInfo, COMDT_REWARD_MULTIPLE_DETAIL mltDetail, COMDT_PVPSPECITEM_OUTPUT specReward, COMDT_REWARD_DETAIL reward)
        {
            Singleton<BattleLogic>.instance.onPreGameSettle();
            if (bSuccess)
            {
                SettleEventParam prm = new SettleEventParam {
                    isWin = GameResult == 1
                };
                Singleton<GameEventSys>.GetInstance().SendEvent<SettleEventParam>(GameEventDef.Event_SettleComplete, ref prm);
                if (bShouldDisplayWinLose)
                {
                    Singleton<BattleLogic>.GetInstance().ShowWinLose(GameResult == 1);
                }
                if (heroSettleInfo != null)
                {
                    Singleton<BattleStatistic>.GetInstance().heroSettleInfo = heroSettleInfo;
                }
                if (rankInfo != null)
                {
                    Singleton<BattleStatistic>.GetInstance().rankInfo = rankInfo;
                }
                if (acntInfo != null)
                {
                    Singleton<BattleStatistic>.GetInstance().acntInfo = acntInfo;
                }
                Singleton<BattleStatistic>.GetInstance().SpecialItemInfo = specReward;
                Singleton<BattleStatistic>.GetInstance().Rewards = reward;
                if (mltDetail != null)
                {
                    Singleton<BattleStatistic>.GetInstance().multiDetail = mltDetail;
                }
            }
            if (Singleton<CUIManager>.instance.GetForm(WinLose.m_FormPath) == null)
            {
                Singleton<GameBuilder>.instance.EndGame();
            }
        }

        public static void HandleSingleGameSettle(CSPkg msg)
        {
            Singleton<SingleGameSettleMgr>.GetInstance().StartSettle(msg.stPkgData.stFinSingleGameRsp);
            int iLevelID = 0;
            bool flag = false;
            bool flag2 = false;
            iLevelID = msg.stPkgData.stFinSingleGameRsp.stDetail.stGameInfo.iLevelID;
            flag = msg.stPkgData.stFinSingleGameRsp.stDetail.stGameInfo.bGameType == 2;
            flag2 = msg.stPkgData.stFinSingleGameRsp.stDetail.stGameInfo.bGameType == 1;
            if (!flag && !flag2)
            {
                uint[] param = new uint[] { iLevelID };
                MonoSingleton<NewbieGuideManager>.GetInstance().CheckSkipCondition(NewbieGuideSkipConditionType.hasCompleteDungeon, param);
            }
            if (Singleton<BattleLogic>.GetInstance().GetCurLvelContext().isPVPLevel && !flag)
            {
                Singleton<BattleLogic>.GetInstance().ShowWinLose(Singleton<WinLose>.instance.LastSingleGameWin);
                Singleton<WinLose>.instance.LastSingleGameWin = true;
            }
            else
            {
                Singleton<LobbyLogic>.GetInstance().StopSettlePanelTimer();
            }
            if (flag)
            {
                uint dwConfValue = GameDataMgr.globalInfoDatabin.GetDataByKey((uint) 0x76).dwConfValue;
                CRoleInfo masterRoleInfo = Singleton<CRoleInfoManager>.GetInstance().GetMasterRoleInfo();
                if ((Singleton<BattleLogic>.GetInstance().GetCurLvelContext().iLevelID == dwConfValue) && masterRoleInfo.IsNewbieAchieveSet(0x3d))
                {
                    masterRoleInfo.SetNewbieAchieve(0x3f, true, true);
                }
            }
        }

        private static void InitRelayConnnecting(COMDT_TGWINFO inRelayTgw)
        {
            if (inRelayTgw.dwVipCnt > 0)
            {
                LookUpDNSOfServer(ApolloConfig.loginOnlyIpOrUrl);
                ConnectorParam para = new ConnectorParam {
                    bIsUDP = (inRelayTgw.bIsUDP <= 0) ? false : true,
                    ip = ApolloConfig.loginOnlyIpOrUrl
                };
                para.SetVPort(inRelayTgw.wVPort);
                Singleton<NetworkModule>.GetInstance().InitGameServerConnect(para);
            }
        }

        private static void LookUpDNSOfServer(string host)
        {
            ApolloConfig.loginOnlyIp = string.Empty;
            try
            {
                Dns.BeginGetHostAddresses(host, new AsyncCallback(LobbyMsgHandler.DNSAsyncCallback), host);
            }
            catch (Exception)
            {
            }
        }

        [MessageHandler(0x70c)]
        public static void onAddHeroNty(CSPkg msg)
        {
            CRoleInfo masterRoleInfo = Singleton<CRoleInfoManager>.GetInstance().GetMasterRoleInfo();
            DebugHelper.Assert(masterRoleInfo != null, "onAddHeroNty role is null");
            uint dwHeroID = msg.stPkgData.stAddHeroNty.stHeroInfo.stCommonInfo.dwHeroID;
            if (masterRoleInfo != null)
            {
                masterRoleInfo.InitHero(msg.stPkgData.stAddHeroNty.stHeroInfo);
                masterRoleInfo.SetHeroSkinData(msg.stPkgData.stAddHeroNty.stHeroSkin);
                if (masterRoleInfo.IsValidExperienceHero(dwHeroID))
                {
                    Singleton<EventRouter>.instance.BroadCastEvent<uint, int>("HeroExperienceAdd", dwHeroID, masterRoleInfo.GetExperienceHeroValidDays(dwHeroID));
                }
            }
            Singleton<EventRouter>.instance.BroadCastEvent<uint>("HeroAdd", dwHeroID);
            Singleton<CUIManager>.GetInstance().CloseSendMsgAlert();
        }

        [MessageHandler(0x71f)]
        public static void onAddHeroSkinNty(CSPkg msg)
        {
            Singleton<CUIManager>.GetInstance().CloseSendMsgAlert();
            Singleton<CRoleInfoManager>.GetInstance().GetMasterRoleInfo().OnAddHeroSkin(msg.stPkgData.stHeroSkinAdd.dwHeroID, msg.stPkgData.stHeroSkinAdd.dwSkinID);
            Singleton<EventRouter>.instance.BroadCastEvent<uint, uint, uint>("HeroSkinAdd", msg.stPkgData.stHeroSkinAdd.dwHeroID, msg.stPkgData.stHeroSkinAdd.dwSkinID, msg.stPkgData.stHeroSkinAdd.dwFrom);
        }

        [MessageHandler(0x402)]
        public static void onAskTransferVisitorData(CSPkg msg)
        {
            Singleton<CUIManager>.GetInstance().OpenMessageBoxWithCancel(Singleton<CTextManager>.GetInstance().GetText("Transfer_Visitor_Data"), enUIEventID.Login_Trans_Visitor_Yes, enUIEventID.Login_Trans_Visitor_No, false);
        }

        [MessageHandler(0x709)]
        public static void onChooseHeroRsp(CSPkg msg)
        {
            if (msg.stPkgData.stAchieveHeroRsp.iResult == 0)
            {
                CRoleInfo masterRoleInfo = Singleton<CRoleInfoManager>.GetInstance().GetMasterRoleInfo();
                masterRoleInfo.InitHero(msg.stPkgData.stAchieveHeroRsp.stHeroInfo);
                masterRoleInfo.SetHeroSkinData(msg.stPkgData.stAchieveHeroRsp.stHeroSkin);
                int dwConfValue = (int) GameDataMgr.globalInfoDatabin.GetDataByKey((uint) 0x74).dwConfValue;
                Singleton<LobbyLogic>.GetInstance().ReqStartGuideLevel(dwConfValue, msg.stPkgData.stAchieveHeroRsp.stHeroInfo.stCommonInfo.dwHeroID);
            }
        }

        [MessageHandler(0x503)]
        public static void OnCoinGetPathRsp(CSPkg msg)
        {
            SCPKG_COINGETPATH_RSP stCoinGetPathRsp = msg.stPkgData.stCoinGetPathRsp;
            CRoleInfo masterRoleInfo = Singleton<CRoleInfoManager>.GetInstance().GetMasterRoleInfo();
            if (masterRoleInfo != null)
            {
                masterRoleInfo.SetCoinGetCntData(stCoinGetPathRsp);
                if (Singleton<CUIManager>.GetInstance().GetForm(CTaskSys.TASK_FORM_PATH) != null)
                {
                    Singleton<CTaskSys>.GetInstance().OnRefreshTaskView();
                }
            }
        }

        public static void OnConfirmReloginNow(CUIEvent uiEvent)
        {
            Singleton<CUIEventManager>.GetInstance().RemoveUIEventListener(enUIEventID.Net_SvrNtfReloginNow, new CUIEventManager.OnUIEventHandler(LobbyMsgHandler.OnConfirmReloginNow));
            Singleton<GameBuilder>.instance.EndGame();
            Singleton<NetworkModule>.instance.CloseGameServerConnect();
            Singleton<GameEventSys>.instance.SendEvent(GameEventDef.Event_LobbyRelogining);
            Singleton<LobbyLogic>.instance.LoginGame();
        }

        [MessageHandler(0x4a6)]
        public static void onCSError(CSPkg msg)
        {
            Singleton<CUIManager>.instance.CloseSendMsgAlert();
            if (msg.stPkgData.stNtfErr.iErrCode == 20)
            {
                Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.GetInstance().GetText("Common_Protocol_Error"), enUIEventID.Lobby_ConfirmErrExit, false);
            }
            else if (msg.stPkgData.stNtfErr.iErrCode == 0x15)
            {
                Singleton<LobbyLogic>.GetInstance().GotoAccLoginPage();
                Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.GetInstance().GetText("Common_Log_Off_Tip"), enUIEventID.Lobby_ConfirmErrExit, false);
            }
            else if (msg.stPkgData.stNtfErr.iErrCode == 0x22)
            {
                Singleton<LobbyLogic>.GetInstance().GotoAccLoginPage();
                Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.GetInstance().GetText("Common_Svr_Acnt_Exception"), enUIEventID.Lobby_ConfirmErrExit, false);
            }
            else if (msg.stPkgData.stNtfErr.iErrCode == 0x16)
            {
                Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.GetInstance().GetText("Common_Svr_Shutdown_Tip"), enUIEventID.Lobby_ConfirmErrExit, false);
            }
            else if (msg.stPkgData.stNtfErr.iErrCode == 0x17)
            {
                Singleton<NetworkModule>.GetInstance().CloseAllServerConnect();
                Singleton<CUIManager>.GetInstance().CloseSendMsgAlert();
                Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.GetInstance().GetText("Common_Svr_Not_WhiteList"), enUIEventID.Lobby_ConfirmErrExit, false);
            }
            else if (msg.stPkgData.stNtfErr.iErrCode == 0x18)
            {
                Singleton<NetworkModule>.GetInstance().CloseAllServerConnect();
                Singleton<CUIManager>.GetInstance().CloseSendMsgAlert();
                Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.GetInstance().GetText("Common_Svr_In_BlackList"), enUIEventID.Lobby_ConfirmErrExit, false);
            }
            else if (msg.stPkgData.stNtfErr.iErrCode == 0x13)
            {
                Singleton<NetworkModule>.GetInstance().CloseAllServerConnect();
                Singleton<CUIManager>.GetInstance().CloseSendMsgAlert();
                Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.GetInstance().GetText("Sever_maintaining"), enUIEventID.Lobby_ConfirmErrExit, false);
            }
            else if (msg.stPkgData.stNtfErr.iErrCode == 30)
            {
                if (msg.stPkgData.stNtfErr.stErrDetail.stRegisterNameErrNtf.bIsEvil == 1)
                {
                    Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.GetInstance().GetText("Register_Name_Invalid_Words"), false);
                }
                else
                {
                    Singleton<CRoleRegisterSys>.instance.ShowErrorCode(msg.stPkgData.stNtfErr.stErrDetail.stRegisterNameErrNtf.szUserName);
                }
            }
            else if (msg.stPkgData.stNtfErr.iErrCode == 0x1f)
            {
                Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.GetInstance().GetText("Room_Name_Invalid_Words"), false);
            }
            else if (msg.stPkgData.stNtfErr.iErrCode == 0x20)
            {
                Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.GetInstance().GetText("Register_Reach_Limit"), false);
            }
            else if (msg.stPkgData.stNtfErr.iErrCode == 0x24)
            {
                Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.GetInstance().GetText("Register_Reach_Total_Limit"), false);
            }
            else if (msg.stPkgData.stNtfErr.iErrCode == 40)
            {
                Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.GetInstance().GetText("Video_Server_Error"), enUIEventID.Lobby_ConfirmErrExit, false);
            }
            else if (msg.stPkgData.stNtfErr.iErrCode == 0x29)
            {
                if (MonoSingleton<Reconnection>.instance.isProcessingRelayRecover)
                {
                    Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.GetInstance().GetText("MultiGame_Not_Sync"), enUIEventID.Lobby_ConfirmErrExit, false);
                }
                else
                {
                    Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.GetInstance().GetText("MultiGame_Not_Sync_Try"), enUIEventID.Battle_MultiHashInvalid, false);
                }
            }
            else if (msg.stPkgData.stNtfErr.iErrCode == 0x19)
            {
                Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.GetInstance().GetText("VersionIsLow"), enUIEventID.Lobby_ConfirmErrExit, false);
            }
            else if (msg.stPkgData.stNtfErr.iErrCode == 0x21)
            {
                Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.GetInstance().GetText("Common_Svr_BanAcnt"), enUIEventID.Lobby_ConfirmErrExit, false);
            }
            else if (msg.stPkgData.stNtfErr.iErrCode == 0x23)
            {
                Singleton<CUIManager>.GetInstance().OpenMessageBox("您已被踢下线", enUIEventID.Lobby_ConfirmErrExit, false);
            }
            else if (msg.stPkgData.stNtfErr.iErrCode == 0x10)
            {
                Singleton<LobbyLogic>.GetInstance().GotoAccLoginPage();
                Singleton<CUIManager>.GetInstance().OpenSendMsgAlert(Singleton<CTextManager>.GetInstance().GetText("ERR_RECONNLOGICWORLDIDINVALID_Tips"), 6, enUIEventID.None);
            }
            else if (msg.stPkgData.stNtfErr.iErrCode == 50)
            {
                MonoSingleton<Reconnection>.GetInstance().RequestRelaySyncCacheFrames(false);
            }
            else if (msg.stPkgData.stNtfErr.iErrCode == 0x11)
            {
                Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.GetInstance().GetText("VersionIsLow"), enUIEventID.Lobby_ConfirmErrExit, false);
            }
            else if (msg.stPkgData.stNtfErr.iErrCode == 0x12)
            {
                Singleton<LobbyLogic>.GetInstance().SvrNtfUpdateClient();
            }
            else if (msg.stPkgData.stNtfErr.iErrCode == 0x88)
            {
                Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.GetInstance().GetText("Chat_Common_Tips_7"), false);
            }
            else if (msg.stPkgData.stNtfErr.iErrCode == 0x89)
            {
                Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.GetInstance().GetText("Chat_Common_Tips_9"), false);
            }
            else if (msg.stPkgData.stNtfErr.iErrCode == 0x8a)
            {
                Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.GetInstance().GetText("Chat_Common_Tips_8"), false);
            }
            else if (msg.stPkgData.stNtfErr.iErrCode == 0x34)
            {
                Singleton<CUIManager>.GetInstance().OpenTips(Singleton<CTextManager>.GetInstance().GetText("ERR_APOLLOPAY_FAST"), false, 1f, null, new object[0]);
            }
            else if (msg.stPkgData.stNtfErr.iErrCode == 0x89)
            {
                Singleton<CUIManager>.GetInstance().OpenTips(Singleton<CTextManager>.GetInstance().GetText("ERR_InBattleMsg_CD"), false, 1f, null, new object[0]);
            }
            Singleton<EventRouter>.instance.BroadCastEvent<int>(EventID.ERRCODE_NTF, msg.stPkgData.stNtfErr.iErrCode);
        }

        [MessageHandler(0x3e9)]
        public static void onGameLoginDispatch(CSPkg msg)
        {
        }

        [MessageHandler(0x3eb)]
        public static void onGameLoginEvent(CSPkg msg)
        {
            if (msg.stPkgData.stGameLoginRsp.bIsSucc != 0)
            {
                if (msg.stPkgData.stGameLoginRsp.bAcntGM != 0)
                {
                    Singleton<CheatWindowExternalIntializer>.CreateInstance();
                    MonoSingleton<ConsoleWindow>.instance.bEnableUseGmPanel = true;
                }
                isHostGMAcnt = msg.stPkgData.stGameLoginRsp.bAcntGM != 0;
                uint dwApolloEnvFlag = msg.stPkgData.stGameLoginRsp.dwApolloEnvFlag;
                if ((dwApolloEnvFlag & 1) > 0)
                {
                    ApolloConfig.payEnv = "release";
                }
                else
                {
                    ApolloConfig.payEnv = "test";
                }
                if ((dwApolloEnvFlag & 2) > 0)
                {
                    ApolloConfig.payEnabled = true;
                }
                else
                {
                    ApolloConfig.payEnabled = false;
                }
                SendMidasToken(Singleton<ApolloHelper>.GetInstance().GetAccountInfo(false));
                Singleton<CRoleInfoManager>.GetInstance().Clean();
                Singleton<LobbyLogic>.GetInstance().uPlayerID = msg.stPkgData.stGameLoginRsp.dwGameAcntObjID;
                Singleton<LobbyLogic>.GetInstance().ulAccountUid = msg.stPkgData.stGameLoginRsp.ullGameAcntUid;
                Singleton<LobbyLogic>.GetInstance().CreateLocalPlayer(msg.stPkgData.stGameLoginRsp.dwGameAcntObjID, msg.stPkgData.stGameLoginRsp.ullGameAcntUid);
                Singleton<CRoleInfoManager>.GetInstance().GetMasterRoleInfo().InitGuidedStateBits(msg.stPkgData.stGameLoginRsp.stNewbieBits);
                Singleton<CRoleInfoManager>.GetInstance().GetMasterRoleInfo().InitNewbieAchieveBits(msg.stPkgData.stGameLoginRsp.stClientBits);
                Singleton<CRoleInfoManager>.GetInstance().GetMasterRoleInfo().InitClientBits(msg.stPkgData.stGameLoginRsp.stNewCltBits);
                Singleton<CRoleInfoManager>.GetInstance().GetMasterRoleInfo().InitInBattleNewbieBits(msg.stPkgData.stGameLoginRsp.stInBattleNewbieBits);
                Singleton<CRoleInfoManager>.instance.GetMasterRoleInfo().SetAttributes(msg.stPkgData.stGameLoginRsp);
                Singleton<CTaskSys>.GetInstance().OnInitTask(msg);
                Singleton<CFunctionUnlockSys>.instance.OnSetUnlockTipsMask(msg.stPkgData.stGameLoginRsp);
                Singleton<CMailSys>.GetInstance().InitLoginRsp(msg.stPkgData.stGameLoginRsp);
                Singleton<CFunctionUnlockSys>.GetInstance();
                Singleton<CPurchaseSys>.GetInstance().SetSvrData(ref msg.stPkgData.stGameLoginRsp.stShopBuyRcd);
                Singleton<CArenaSystem>.GetInstance().InitServerData(msg.stPkgData.stGameLoginRsp.stArenaData);
                for (int i = 0; i < msg.stPkgData.stGameLoginRsp.BanTime.Length; i++)
                {
                    MonoSingleton<IDIPSys>.GetInstance().SetBanTimeInfo((COM_ACNT_BANTIME_TYPE) i, msg.stPkgData.stGameLoginRsp.BanTime[i]);
                }
                Singleton<CGuildSystem>.GetInstance().RequestGuildInfo();
                Singleton<CAchievementSystem>.GetInstance().SendReqGetRankingAcountInfo();
                Singleton<RankingSystem>.GetInstance().SendReqRankingDetail();
                Singleton<CNameChangeSystem>.GetInstance().SetPlayerNameChangeCount((int) msg.stPkgData.stGameLoginRsp.dwChgNameCnt);
                if (msg.stPkgData.stGameLoginRsp.bIsVisitorSvr == 1)
                {
                    PlayerPrefs.SetString("visitorUid", msg.stPkgData.stGameLoginRsp.ullGameAcntUid.ToString());
                    bVisitorSvr = true;
                }
                else
                {
                    bVisitorSvr = false;
                }
                Singleton<HeadIconSys>.instance.OnHeadIconSyncList(msg.stPkgData.stGameLoginRsp.stHeadImage.wHeadImgCnt, msg.stPkgData.stGameLoginRsp.stHeadImage.astHeadImgInfo);
                Singleton<EventRouter>.instance.BroadCastEvent(EventID.HEAD_IMAGE_FLAG_CHANGE);
                CChatNetUT.Send_GetChat_Req(EChatChannel.Lobby);
                Singleton<CLoudSpeakerSys>.instance.StartReqTimer();
                Singleton<BeaconHelper>.GetInstance().Event_CommonReport("Event_LoginMsgResp");
            }
        }

        [MessageHandler(0x3f5)]
        public static void onGameLoginFinish(CSPkg msg)
        {
            if (!Singleton<LobbyLogic>.instance.isLogin)
            {
                Singleton<LobbySvrMgr>.GetInstance().isLogin = true;
                Singleton<LobbyLogic>.instance.isLogin = true;
                Singleton<CUIManager>.instance.CloseSendMsgAlert();
                bool flag = Singleton<CRoleInfoManager>.GetInstance().GetMasterRoleInfo().IsGuidedStateSet(0);
                if (CSysDynamicBlock.bNewbieBlocked)
                {
                    flag = true;
                }
                if (!flag)
                {
                    Singleton<CRoleRegisterSys>.instance.CloseRoleCreateForm();
                    if (Singleton<CRoleInfoManager>.GetInstance().GetMasterRoleInfo().GameDifficult == COM_ACNT_NEWBIE_TYPE.COM_ACNT_NEWBIE_TYPE_NULL)
                    {
                        Singleton<CRoleRegisterSys>.instance.OpenGameDifSelectForm();
                    }
                    else
                    {
                        LobbyLogic.ReqStartGuideLevel11(false);
                    }
                }
                Singleton<GameStateCtrl>.GetInstance().GotoState("LobbyState");
                if (!Singleton<LobbySvrMgr>.GetInstance().isFirstLogin)
                {
                    Singleton<LobbySvrMgr>.GetInstance().isFirstLogin = true;
                    if (<>f__am$cache2 == null)
                    {
                        <>f__am$cache2 = delegate (int seq) {
                            if (!Singleton<BattleLogic>.GetInstance().isRuning && LobbyForm.AutoPopAllow)
                            {
                                if (bVisitorSvr)
                                {
                                    Singleton<ApolloHelper>.GetInstance().ShowIOSGuestNotice();
                                }
                                else
                                {
                                    Singleton<ApolloHelper>.GetInstance().ShowNotice(0, "2");
                                }
                            }
                        };
                    }
                    Singleton<CTimerManager>.GetInstance().AddTimer(0x3e8, 1, <>f__am$cache2);
                }
            }
        }

        [MessageHandler(0x412)]
        public static void onGameLoginLimit(CSPkg msg)
        {
            if (msg.stPkgData.stLoginLimitRsp.iErrCode == 0x21)
            {
                DateTime time = Utility.ToUtcTime2Local((long) msg.stPkgData.stLoginLimitRsp.dwLimitTime);
                object[] args = new object[] { time.Year, time.Month, time.Day, time.Hour, time.Minute };
                string strContent = string.Format("您已被封号！封号截止时间为{0}年{1}月{2}日{3}时{4}分", args);
                Singleton<CUIManager>.GetInstance().OpenMessageBox(strContent, enUIEventID.Lobby_ConfirmErrExit, false);
            }
            else if (msg.stPkgData.stLoginLimitRsp.iErrCode == 0x23)
            {
                DateTime time2 = Utility.ToUtcTime2Local((long) msg.stPkgData.stLoginLimitRsp.dwLimitTime);
                object[] objArray2 = new object[] { time2.Year, time2.Month, time2.Day, time2.Hour, time2.Minute };
                string str2 = string.Format("您已被踢下线！截止时间为{0}年{1}月{2}日{3}时{4}分", objArray2);
                Singleton<CUIManager>.GetInstance().OpenMessageBox(str2, enUIEventID.Lobby_ConfirmErrExit, false);
            }
        }

        [MessageHandler(0x3f9)]
        public static void onGameLogout(CSPkg msg)
        {
            if (msg.stPkgData.stGameLogoutRsp.iLogoutType == 0)
            {
                Singleton<LobbyLogic>.GetInstance().GotoAccLoginPage();
            }
            else if (msg.stPkgData.stGameLogoutRsp.iLogoutType == 1)
            {
            }
            if (Singleton<CRoleInfoManager>.GetInstance().GetMasterRoleInfo() != null)
            {
                Singleton<CRoleInfoManager>.GetInstance().GetMasterRoleInfo().Clear();
            }
        }

        [MessageHandler(0x43d)]
        public static void OnGameOverEvent(CSPkg msg)
        {
            if (!Singleton<BattleLogic>.instance.isWaitGameEnd)
            {
                if (msg.stPkgData.stNtfCltGameOver.bWinCamp > 0)
                {
                    COM_PLAYERCAMP playerCamp = Singleton<GamePlayerCenter>.instance.GetHostPlayer().PlayerCamp;
                    Singleton<BattleLogic>.instance.battleStat.iBattleResult = (((byte) playerCamp) != msg.stPkgData.stNtfCltGameOver.bWinCamp) ? 2 : 1;
                    COM_PLAYERCAMP oppositeCmp = COM_PLAYERCAMP.COM_PLAYERCAMP_1;
                    if (Singleton<BattleLogic>.instance.battleStat.iBattleResult == 1)
                    {
                        oppositeCmp = BattleLogic.GetOppositeCmp(playerCamp);
                    }
                    else
                    {
                        oppositeCmp = playerCamp;
                    }
                    ForceKillCrystal((int) oppositeCmp);
                }
                else
                {
                    Singleton<LobbyLogic>.instance.ReqMultiGameOver(false);
                }
                Singleton<CSurrenderSystem>.instance.DelayCloseSurrenderForm(5);
            }
        }

        [MessageHandler(0x3f6)]
        public static void onGameReloginNow(CSPkg msg)
        {
            Singleton<NetworkModule>.instance.ResetLobbySending();
            if (Singleton<LobbyLogic>.instance.isLogin)
            {
                Singleton<LobbySvrMgr>.GetInstance().isLogin = false;
                Singleton<LobbyLogic>.instance.isLogin = false;
                Singleton<CUIManager>.GetInstance().OpenMessageBox("与服务器连接丢失，将重新与服务器建立连接。", enUIEventID.Net_SvrNtfReloginNow, false);
                Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Net_SvrNtfReloginNow, new CUIEventManager.OnUIEventHandler(LobbyMsgHandler.OnConfirmReloginNow));
            }
            else
            {
                Singleton<LobbyLogic>.instance.LoginGame();
            }
        }

        [MessageHandler(0x3fa)]
        public static void onGameServerLoginKeyReq(CSPkg msg)
        {
            Singleton<EventRouter>.GetInstance().RemoveEventHandler<ApolloAccountInfo>(EventID.ApolloHelper_Login_Success, new Action<ApolloAccountInfo>(LobbyMsgHandler.SendMidasToken));
            Singleton<EventRouter>.GetInstance().AddEventHandler<ApolloAccountInfo>(EventID.ApolloHelper_Login_Success, new Action<ApolloAccountInfo>(LobbyMsgHandler.SendMidasToken));
            Singleton<ApolloHelper>.GetInstance().Login(Singleton<ApolloHelper>.GetInstance().CurPlatform, 0L, null);
        }

        [MessageHandler(0xa2d)]
        public static void OnGetRankingAccountInfoRsp(CSPkg msg)
        {
            Singleton<CUIManager>.GetInstance().CloseSendMsgAlert();
            if (msg.stPkgData.stGetRankingAcntInfoRsp.iErrCode == 0)
            {
                Singleton<EventRouter>.GetInstance().BroadCastEvent<SCPKG_GET_RANKING_ACNT_INFO_RSP>("Ranking_Get_Ranking_Account_Info", msg.stPkgData.stGetRankingAcntInfoRsp);
                Singleton<EventRouter>.GetInstance().BroadCastEvent<SCPKG_GET_RANKING_ACNT_INFO_RSP>(EventID.ACHIEVE_GET_RANKING_ACCOUNT_INFO, msg.stPkgData.stGetRankingAcntInfoRsp);
                Singleton<EventRouter>.GetInstance().BroadCastEvent<SCPKG_GET_RANKING_ACNT_INFO_RSP>("UnionRank_Get_Rank_Account_Info", msg.stPkgData.stGetRankingAcntInfoRsp);
            }
        }

        [MessageHandler(0xa2b)]
        public static void OnGetRankingListRsp(CSPkg msg)
        {
            Singleton<CUIManager>.GetInstance().CloseSendMsgAlert();
            if (msg.stPkgData.stGetRankingListRsp.iErrCode != 0)
            {
                if (msg.stPkgData.stGetRankingListRsp.iErrCode != 1)
                {
                    Singleton<CUIManager>.GetInstance().OpenMessageBox(string.Format("Error Code {0}", msg.stPkgData.stGetRankingListRsp.iErrCode), false);
                }
            }
            else
            {
                COM_APOLLO_TRANK_SCORE_TYPE bNumberType = (COM_APOLLO_TRANK_SCORE_TYPE) msg.stPkgData.stGetRankingListRsp.bNumberType;
                switch (bNumberType)
                {
                    case COM_APOLLO_TRANK_SCORE_TYPE.COM_APOLLO_TRANK_SCORE_TYPE_POWER:
                    case COM_APOLLO_TRANK_SCORE_TYPE.COM_APOLLO_TRANK_SCORE_TYPE_PVP_EXP:
                    case COM_APOLLO_TRANK_SCORE_TYPE.COM_APOLLO_TRANK_SCORE_TYPE_HERO_NUM:
                    case COM_APOLLO_TRANK_SCORE_TYPE.COM_APOLLO_TRANK_SCORE_TYPE_SKIN_NUM:
                    case COM_APOLLO_TRANK_SCORE_TYPE.COM_APOLLO_TRANK_SCORE_TYPE_LADDER_POINT:
                    case COM_APOLLO_TRANK_SCORE_TYPE.COM_APOLLO_TRANK_SCORE_TYPE_ACHIEVEMENT:
                    case COM_APOLLO_TRANK_SCORE_TYPE.COM_APOLLO_TRANK_SCORE_TYPE_CONTINOUS_WIN:
                    case COM_APOLLO_TRANK_SCORE_TYPE.COM_APOLLO_TRANK_SCORE_TYPE_WIN_GAMENUM:
                    case COM_APOLLO_TRANK_SCORE_TYPE.COM_APOLLO_TRANK_SCORE_TYPE_VIP_SCORE:
                        Singleton<EventRouter>.GetInstance().BroadCastEvent<SCPKG_GET_RANKING_LIST_RSP>("Ranking_Get_Ranking_List", msg.stPkgData.stGetRankingListRsp);
                        break;

                    default:
                        if ((bNumberType >= COM_APOLLO_TRANK_SCORE_TYPE.COM_APOLLO_TRANK_SCORE_TYPE_COINMATCH_LOW_DAY) && (bNumberType <= COM_APOLLO_TRANK_SCORE_TYPE.COM_APOLLO_TRANK_SCORE_TYPE_COUPONSMATCH_HIGH_GUILD))
                        {
                            Singleton<EventRouter>.GetInstance().BroadCastEvent<SCPKG_GET_RANKING_LIST_RSP>("UnionRank_Get_Rank_List", msg.stPkgData.stGetRankingListRsp);
                        }
                        else
                        {
                            switch (bNumberType)
                            {
                                case COM_APOLLO_TRANK_SCORE_TYPE.COM_APOLLO_TRANK_SCORE_TYPE_GUILD_POWER:
                                    Singleton<EventRouter>.GetInstance().BroadCastEvent<SCPKG_GET_RANKING_LIST_RSP>("Guild_Get_Power_Ranking", msg.stPkgData.stGetRankingListRsp);
                                    return;

                                case COM_APOLLO_TRANK_SCORE_TYPE.COM_APOLLO_TRANK_SCORE_TYPE_GUILD_RANK_POINT:
                                    Singleton<EventRouter>.GetInstance().BroadCastEvent<SCPKG_GET_RANKING_LIST_RSP, enGuildRankpointRankListType>("Guild_Get_Rankpoint_Ranking", msg.stPkgData.stGetRankingListRsp, enGuildRankpointRankListType.CurrentWeek);
                                    return;

                                case COM_APOLLO_TRANK_SCORE_TYPE.COM_APOLLO_TRANK_SCORE_TYPE_GUILD_SEASON:
                                    Singleton<EventRouter>.GetInstance().BroadCastEvent<SCPKG_GET_RANKING_LIST_RSP, enGuildRankpointRankListType>("Guild_Get_Rankpoint_Ranking", msg.stPkgData.stGetRankingListRsp, enGuildRankpointRankListType.SeasonBest);
                                    return;

                                case COM_APOLLO_TRANK_SCORE_TYPE.COM_APOLLO_TRANK_SCORE_TYPE_CUSTOM_EQUIP:
                                    Singleton<EventRouter>.GetInstance().BroadCastEvent<SCPKG_GET_RANKING_LIST_RSP>(EventID.CUSTOM_EQUIP_RANK_LIST_GET, msg.stPkgData.stGetRankingListRsp);
                                    return;
                            }
                        }
                        break;
                }
            }
        }

        [MessageHandler(0x725)]
        public static void onGMAddAllHeroSkinNty(CSPkg msg)
        {
            Singleton<CUIManager>.GetInstance().CloseSendMsgAlert();
            if (msg.stPkgData.stGMAddAllSkillRsp.iResult == 0)
            {
                CRoleInfo masterRoleInfo = Singleton<CRoleInfoManager>.GetInstance().GetMasterRoleInfo();
                if (masterRoleInfo != null)
                {
                    masterRoleInfo.OnGmAddAllSkin();
                }
            }
        }

        [MessageHandler(0x715)]
        public static void onGmAddHeroRsp(CSPkg msg)
        {
            if (msg.stPkgData.stGMAddHeroRsp.iResult == 0)
            {
                CRoleInfo masterRoleInfo = Singleton<CRoleInfoManager>.GetInstance().GetMasterRoleInfo();
                masterRoleInfo.InitHero(msg.stPkgData.stGMAddHeroRsp.stHeroInfo);
                masterRoleInfo.SetHeroSkinData(msg.stPkgData.stGMAddHeroRsp.stHeroSkin);
                Singleton<EventRouter>.instance.BroadCastEvent<uint>("HeroAdd", msg.stPkgData.stGMAddHeroRsp.stHeroInfo.stCommonInfo.dwHeroID);
            }
        }

        [MessageHandler(0x718)]
        public static void onGmUnlockHeroRsp(CSPkg msg)
        {
            if (msg.stPkgData.stGMUnlockHeroPVPRsp.chResult == 0)
            {
                uint dwHeroID = msg.stPkgData.stGMUnlockHeroPVPRsp.dwHeroID;
                DictionaryView<uint, CHeroInfo>.Enumerator enumerator = Singleton<CRoleInfoManager>.GetInstance().GetMasterRoleInfo().GetHeroInfoDic().GetEnumerator();
                while (enumerator.MoveNext())
                {
                    if (dwHeroID == 0)
                    {
                        KeyValuePair<uint, CHeroInfo> current = enumerator.Current;
                        CHeroInfo local1 = current.Value;
                        local1.MaskBits |= 2;
                    }
                    else
                    {
                        KeyValuePair<uint, CHeroInfo> pair2 = enumerator.Current;
                        if (dwHeroID == pair2.Key)
                        {
                            KeyValuePair<uint, CHeroInfo> pair3 = enumerator.Current;
                            CHeroInfo local2 = pair3.Value;
                            local2.MaskBits |= 2;
                        }
                    }
                }
            }
        }

        [MessageHandler(0x70b)]
        public static void OnHeroExpUp(CSPkg msg)
        {
            Singleton<CRoleInfoManager>.GetInstance().GetMasterRoleInfo().SetHeroExp(msg.stPkgData.stHeroExpAdd.dwHeroID, msg.stPkgData.stHeroExpAdd.wCurLevel, (int) msg.stPkgData.stHeroExpAdd.dwCurExp);
        }

        [MessageHandler(0x70a)]
        public static void onHeroInfoNty(CSPkg msg)
        {
            Singleton<CHeroSelectSystem>.GetInstance().m_defaultBattleListInfo = msg.stPkgData.stAcntHeroInfoNty.stBattleListInfo;
            CRoleInfo masterRoleInfo = Singleton<CRoleInfoManager>.GetInstance().GetMasterRoleInfo();
            if (masterRoleInfo != null)
            {
                masterRoleInfo.SetHeroInfo(msg.stPkgData.stAcntHeroInfoNty);
            }
            else
            {
                DebugHelper.Assert(false, "Master RoleInfo is NULL!!!");
            }
        }

        [MessageHandler(0x712)]
        public static void OnHeroInfoUpdate(CSPkg msg)
        {
            CRoleInfo masterRoleInfo = Singleton<CRoleInfoManager>.GetInstance().GetMasterRoleInfo();
            DebugHelper.Assert(masterRoleInfo != null, "OnHeroInfoUpdate role is null");
            if (masterRoleInfo != null)
            {
                masterRoleInfo.OnHeroInfoUpdate(msg.stPkgData.stHeroInfoUpdNtf);
            }
        }

        [MessageHandler(0x406)]
        public static void onLobbyConnectRedirect(CSPkg msg)
        {
            Singleton<NetworkModule>.GetInstance().CloseLobbyServerConnect();
            Singleton<NetworkModule>.GetInstance().lobbySvr.RedirectNewPort(msg.stPkgData.stGameConnRedirect.wRedirectGameVport);
        }

        [MessageHandler(0x410)]
        public static void onLobbyOffingRestartReq(CSPkg msg)
        {
            byte num = 0;
            if (!Singleton<LobbyLogic>.instance.isLogin)
            {
                num = 1;
                Singleton<NetworkModule>.instance.ResetLobbySending();
            }
            CSPkg pkg = NetworkModule.CreateDefaultCSPKG(0x411);
            pkg.stPkgData.stOffingRestartRsp.bNeedLoginRsp = num;
            pkg.stPkgData.stOffingRestartRsp.iLogicWorldID = MonoSingleton<TdirMgr>.GetInstance().SelectedTdir.logicWorldID;
            pkg.stPkgData.stOffingRestartRsp.bPrivilege = (byte) Singleton<ApolloHelper>.GetInstance().GetCurrentLoginPrivilege();
            uint versionNumber = CVersion.GetVersionNumber(GameFramework.AppVersion);
            pkg.stPkgData.stOffingRestartRsp.iCltAppVersion = (int) versionNumber;
            uint cachedResourceVersion = Singleton<CResourceManager>.GetInstance().GetCachedResourceVersion();
            pkg.stPkgData.stOffingRestartRsp.iCltResVersion = (int) cachedResourceVersion;
            byte[] bytes = Encoding.ASCII.GetBytes(SystemInfo.deviceUniqueIdentifier);
            if (bytes.Length > 0)
            {
                Buffer.BlockCopy(bytes, 0, pkg.stPkgData.stOffingRestartRsp.szCltIMEI, 0, (bytes.Length <= 0x40) ? bytes.Length : 0x40);
            }
            Singleton<NetworkModule>.instance.SendLobbyMsg(ref pkg, false);
        }

        [MessageHandler(0x47f)]
        public static void OnMasterDianQuanUpdate(CSPkg msg)
        {
            Singleton<CUIManager>.GetInstance().CloseSendMsgAlert();
            long llCouponsCnt = msg.stPkgData.stAcntCouponsRsp.llCouponsCnt;
            CRoleInfo masterRoleInfo = Singleton<CRoleInfoManager>.instance.GetMasterRoleInfo();
            if (masterRoleInfo != null)
            {
                masterRoleInfo.DianQuan = (ulong) llCouponsCnt;
            }
        }

        [MessageHandler(0x3f2)]
        public static void OnMasterInfoUpdate(CSPkg msg)
        {
            CRoleInfo masterRoleInfo = Singleton<CRoleInfoManager>.instance.GetMasterRoleInfo();
            if (masterRoleInfo != null)
            {
                masterRoleInfo.OnUpdate(msg.stPkgData.stNtfAcntInfoUpd);
            }
        }

        [MessageHandler(0x3f3)]
        public static void OnMasterLevelUp(CSPkg msg)
        {
            Singleton<CRoleInfoManager>.instance.GetMasterRoleInfo().OnLevelUp(msg.stPkgData.stNtfAcntLevelUp);
        }

        [MessageHandler(0x3f7)]
        public static void OnMasterPvpLevelUp(CSPkg msg)
        {
            Singleton<CRoleInfoManager>.instance.GetMasterRoleInfo().OnPvpLevelUp(msg.stPkgData.stNtfAcntPvpLevelUp);
        }

        [MessageHandler(0x437)]
        public static void onMultiChoosePlayerLeave(CSPkg msg)
        {
            string strContent = Utility.ProtErrCodeToStr(0x437, msg.stPkgData.stMultGameAbortNtf.bReason);
            Singleton<CUIManager>.GetInstance().OpenTips(strContent, false, 1f, null, new object[0]);
            Singleton<CUIEventManager>.GetInstance().DispatchUIEvent(enUIEventID.HeroSelect_CloseForm);
            Singleton<CRoomSystem>.GetInstance().CloseRoom();
            Singleton<CMatchingSystem>.GetInstance().CloseMatchingConfirm();
            Singleton<NetworkModule>.GetInstance().CloseGameServerConnect();
            if ((msg.stPkgData.stMultGameAbortNtf.bReason == 0x11) || (msg.stPkgData.stMultGameAbortNtf.bReason == 15))
            {
                Singleton<CSoundManager>.GetInstance().PostEvent("UI_matching_lost", null);
            }
            Singleton<GameStateCtrl>.GetInstance().GotoState("LobbyState");
            Singleton<LobbyLogic>.GetInstance().inMultiRoom = false;
        }

        [MessageHandler(0x435)]
        public static void onMultiGameFight(CSPkg msg)
        {
            if (!Singleton<BattleLogic>.instance.isWaitMultiStart && !Singleton<BattleLogic>.instance.isFighting)
            {
                if (MonoSingleton<GameLoader>.instance.isLoadStart)
                {
                    Singleton<BattleLogic>.instance.isWaitMultiStart = true;
                    Singleton<FrameSynchr>.instance.StartSynchr(false);
                }
                else
                {
                    Singleton<FrameSynchr>.instance.StartSynchr(true);
                    Singleton<BattleLogic>.instance.StartFightMultiGame();
                }
            }
        }

        [MessageHandler(0x433)]
        public static void onMultiGameLoad(CSPkg msg)
        {
            if (!Singleton<LobbyLogic>.instance.inMultiGame)
            {
                Singleton<LobbyLogic>.instance.inMultiGame = true;
                Singleton<GameBuilder>.instance.StartGame(new MultiGameContext(msg.stPkgData.stMultGameBeginLoad));
            }
        }

        [MessageHandler(0x439)]
        public static void onMultiGameOver(CSPkg msg)
        {
        }

        [MessageHandler(0x436)]
        public static void onMultiGameReady(CSPkg msg)
        {
            InitRelayConnnecting(msg.stPkgData.stMultGameReadyNtf.stRelayTGW);
        }

        [MessageHandler(0x441)]
        public static void OnMultiGameRecover(CSPkg msg)
        {
            if (!Singleton<NetworkModule>.GetInstance().gameSvr.connected && !Singleton<LobbyLogic>.instance.inMultiGame)
            {
                InitRelayConnnecting(msg.stPkgData.stMultGameRecoverNtf.stRelayTGW);
            }
        }

        [MessageHandler(0x43a)]
        public static void onMultiGameSettle(CSPkg msg)
        {
            Singleton<LobbyLogic>.GetInstance().StopSettleMsgTimer();
            Singleton<BattleStatistic>.instance.RecordMvp(msg.stPkgData.stMultGameSettleGain.stDetail.stGameInfo);
            object[] inParameters = new object[] { msg.stPkgData.stMultGameSettleGain.iErrCode };
            DebugHelper.Assert(msg.stPkgData.stMultGameSettleGain.iErrCode == 0, "SCID_MULTGAME_SETTLEGAIN Error: {0}", inParameters);
            if (MonoSingleton<Reconnection>.GetInstance().isProcessingRelayRecover)
            {
                Singleton<GameBuilder>.GetInstance().EndGame();
                Singleton<GameStateCtrl>.GetInstance().GotoState("LobbyState");
                Singleton<CUIManager>.GetInstance().OpenTips("gameWasOver", true, 1f, null, new object[0]);
            }
            else
            {
                if (Singleton<BattleLogic>.instance.isWaitGameEnd)
                {
                    DebugHelper.Assert(Singleton<BattleLogic>.instance.m_cachedSvrEndData == null);
                    Singleton<BattleLogic>.instance.m_cachedSvrEndData = msg;
                }
                else
                {
                    if (msg.stPkgData.stMultGameSettleGain.iErrCode == 0)
                    {
                        CLevelCfgLogicManager.SetMasterPvpDetailWhenGameSettle(msg.stPkgData.stMultGameSettleGain.stDetail.stGameInfo);
                    }
                    HandleGameSettle(msg.stPkgData.stMultGameSettleGain.iErrCode == 0, true, msg.stPkgData.stMultGameSettleGain.stDetail.stGameInfo.bGameResult, msg.stPkgData.stMultGameSettleGain.stDetail.stHeroList, msg.stPkgData.stMultGameSettleGain.stDetail.stRankInfo, msg.stPkgData.stMultGameSettleGain.stDetail.stAcntInfo, msg.stPkgData.stMultGameSettleGain.stDetail.stMultipleDetail, msg.stPkgData.stMultGameSettleGain.stDetail.stSpecReward, msg.stPkgData.stMultGameSettleGain.stDetail.stReward);
                }
                Singleton<NetworkModule>.instance.CloseGameServerConnect();
                MonoSingleton<VoiceSys>.GetInstance().LeaveRoom();
            }
        }

        [MessageHandler(0x57b)]
        public static void onPlayerInfoPush(CSPkg msg)
        {
            CRoleInfo masterRoleInfo = Singleton<CRoleInfoManager>.GetInstance().GetMasterRoleInfo();
            if (masterRoleInfo != null)
            {
                masterRoleInfo.m_payLevel = msg.stPkgData.stAcntDetailInfoRsp.dwPayLevel;
                masterRoleInfo.pvpDetail = msg.stPkgData.stAcntDetailInfoRsp.stPvpDetailInfo;
                masterRoleInfo.m_baseGuildInfo.name = StringHelper.UTF8BytesToString(ref msg.stPkgData.stAcntDetailInfoRsp.szGuildName);
                masterRoleInfo.m_HighestGradeOfRank = msg.stPkgData.stAcntDetailInfoRsp.bMaxGradeOfRank;
            }
        }

        [MessageHandler(0x9cd)]
        public static void OnRoleExtraCoinAndExpChange(CSPkg msg)
        {
            CRoleInfo masterRoleInfo = Singleton<CRoleInfoManager>.GetInstance().GetMasterRoleInfo();
            if ((masterRoleInfo != null) && (msg.stPkgData.stPropMultipleNtf != null))
            {
                masterRoleInfo.SetExtraCoinAndExp(msg.stPkgData.stPropMultipleNtf.stPropMultiple);
            }
        }

        [MessageHandler(0x57c)]
        public static void onRoleHeadUrlChange(CSPkg msg)
        {
            CRoleInfo masterRoleInfo = Singleton<CRoleInfoManager>.GetInstance().GetMasterRoleInfo();
            if (masterRoleInfo != null)
            {
                masterRoleInfo.HeadUrl = Singleton<ApolloHelper>.GetInstance().ToSnsHeadUrl(ref msg.stPkgData.stAcntHeadUrlChgNtf.szHeadUrl);
            }
        }

        [MessageHandler(0x43f)]
        public static void OnRunawayRSP(CSPkg msg)
        {
            if (msg.stPkgData.stRunAwayRsp.bNeedDisplaySettle <= 0)
            {
                HandleGameSettle(true, msg.stPkgData.stRunAwayRsp.bNeedDisplaySettle > 0, 2, null, null, null, null, null, null);
                Singleton<NetworkModule>.instance.CloseGameServerConnect();
            }
        }

        [MessageHandler(0x41d)]
        public static void onSingleGameFinish(CSPkg msg)
        {
            Singleton<CUIManager>.GetInstance().CloseSendMsgAlert();
            Singleton<LobbyLogic>.GetInstance().StopSettleMsgTimer();
            if (msg.stPkgData.stFinSingleGameRsp.iErrCode == 0)
            {
                if (Singleton<BattleLogic>.GetInstance().GetCurLvelContext().GameType == COM_GAME_TYPE.COM_SINGLE_GAME_OF_BURNING)
                {
                    Singleton<ApolloHelper>.GetInstance().ApolloRepoertEvent("Burning Game Receives Server Ending Package", null, true);
                }
                if (Singleton<BattleLogic>.instance.isWaitGameEnd)
                {
                    DebugHelper.Assert(Singleton<BattleLogic>.instance.m_cachedSvrEndData == null);
                    Singleton<BattleLogic>.instance.m_cachedSvrEndData = msg;
                }
                else
                {
                    HandleSingleGameSettle(msg);
                }
            }
            else
            {
                Singleton<CUIManager>.GetInstance().OpenMessageBox("结算失败，点击确定返回大厅。", enUIEventID.Net_SingleGameFinishError, false);
            }
        }

        [MessageHandler(0x41b)]
        public static void onSingleGameLoad(CSPkg msg)
        {
            if (msg.stPkgData.stStartSingleGameRsp.iErrCode == 0)
            {
                Singleton<LobbyLogic>.instance.inMultiGame = false;
                Singleton<GameBuilder>.instance.StartGame(SingleGameContextFactory.CreateSingleGameContext(msg.stPkgData.stStartSingleGameRsp));
            }
            else
            {
                Singleton<CUIManager>.GetInstance().CloseSendMsgAlert();
                Singleton<CUIEventManager>.GetInstance().DispatchUIEvent(enUIEventID.HeroSelect_CloseForm);
                if (msg.stPkgData.stStartSingleGameRsp.iErrCode == 10)
                {
                    if (msg.stPkgData.stStartSingleGameRsp.bGameType == 7)
                    {
                        DateTime banTime = MonoSingleton<IDIPSys>.GetInstance().GetBanTime(COM_ACNT_BANTIME_TYPE.COM_ACNT_BANTIME_BANPLAYBURNING);
                        object[] args = new object[] { banTime.Year, banTime.Month, banTime.Day, banTime.Hour, banTime.Minute };
                        string strContent = string.Format("您被禁止进入六国远征！截止时间为{0}年{1}月{2}日{3}时{4}分", args);
                        Singleton<CUIManager>.GetInstance().OpenMessageBox(strContent, false);
                    }
                    else if (msg.stPkgData.stStartSingleGameRsp.bGameType == 8)
                    {
                        DateTime time2 = MonoSingleton<IDIPSys>.GetInstance().GetBanTime(COM_ACNT_BANTIME_TYPE.COM_ACNT_BANTIME_BANPLAYAREAN);
                        object[] objArray2 = new object[] { time2.Year, time2.Month, time2.Day, time2.Hour, time2.Minute };
                        string str2 = string.Format("您被禁止进入武道会！截止时间为{0}年{1}月{2}日{3}时{4}分", objArray2);
                        Singleton<CUIManager>.GetInstance().OpenMessageBox(str2, false);
                    }
                    else if (msg.stPkgData.stStartSingleGameRsp.bGameType == 0)
                    {
                        DateTime time3 = MonoSingleton<IDIPSys>.GetInstance().GetBanTime(COM_ACNT_BANTIME_TYPE.COM_ACNT_BANTIME_BANPLAYPVE);
                        object[] objArray3 = new object[] { time3.Year, time3.Month, time3.Day, time3.Hour, time3.Minute };
                        string str3 = string.Format("您被禁止进入闯关！截止时间为{0}年{1}月{2}日{3}时{4}分", objArray3);
                        Singleton<CUIManager>.GetInstance().OpenMessageBox(str3, false);
                    }
                }
                else
                {
                    Singleton<CUIManager>.GetInstance().OpenTips(Utility.ProtErrCodeToStr(0x41b, msg.stPkgData.stStartSingleGameRsp.iErrCode), false, 1f, null, new object[0]);
                }
            }
        }

        public static void SendMidasToken(ApolloAccountInfo accountInfo)
        {
            Singleton<EventRouter>.GetInstance().RemoveEventHandler<ApolloAccountInfo>(EventID.ApolloHelper_Login_Success, new Action<ApolloAccountInfo>(LobbyMsgHandler.SendMidasToken));
            CSPkg msg = NetworkModule.CreateDefaultCSPKG(0x3fb);
            if (accountInfo != null)
            {
                string str = string.Empty;
                foreach (ApolloToken token in accountInfo.TokenList)
                {
                    if (ApolloConfig.platform == ApolloPlatform.Wechat)
                    {
                        if (token.Type == ApolloTokenType.Access)
                        {
                            str = token.Value;
                        }
                    }
                    else if ((ApolloConfig.platform == ApolloPlatform.QQ) || (ApolloConfig.platform == ApolloPlatform.WTLogin))
                    {
                        if (token.Type == ApolloTokenType.Pay)
                        {
                            str = token.Value;
                        }
                    }
                    else if ((ApolloConfig.platform == ApolloPlatform.Guest) && (token.Type == ApolloTokenType.Access))
                    {
                        str = token.Value;
                    }
                }
                StringHelper.StringToUTF8Bytes(str, ref msg.stPkgData.stLoginSynRsp.szOpenKey);
                string pf = string.Empty;
                if (accountInfo.Pf == string.Empty)
                {
                    pf = "desktop_m_qq-73213123-android-73213123-qq-1104466820-BC569F700D770A26CD422F24FD675F10";
                }
                else
                {
                    pf = accountInfo.Pf;
                }
                StringHelper.StringToUTF8Bytes(pf, ref msg.stPkgData.stLoginSynRsp.szPf);
                Singleton<NetworkModule>.GetInstance().SendLobbyMsg(ref msg, false);
                Singleton<CUIManager>.GetInstance().CloseSendMsgAlert();
                Singleton<ApolloHelper>.GetInstance().InitPay();
            }
        }

        public static bool isHostGMAcnt
        {
            [CompilerGenerated]
            get
            {
                return <isHostGMAcnt>k__BackingField;
            }
            [CompilerGenerated]
            protected set
            {
                <isHostGMAcnt>k__BackingField = value;
            }
        }
    }
}

