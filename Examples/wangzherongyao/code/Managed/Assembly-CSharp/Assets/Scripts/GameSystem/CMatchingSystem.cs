﻿namespace Assets.Scripts.GameSystem
{
    using Assets.Scripts.Framework;
    using Assets.Scripts.GameLogic;
    using Assets.Scripts.UI;
    using CSProtocol;
    using ResData;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    [MessageHandlerClass]
    internal class CMatchingSystem : Singleton<CMatchingSystem>
    {
        private int AutoSingleMatchTimeSeq = -1;
        public int AutoSingleMatchTipInterval = 0x2710;
        private bool bAutoRobotMatch;
        private bool bInMatching;
        private bool bInMatchingTeam;
        private bool bPkAI;
        public bool bPlayerActive = true;
        public CacheMathingInfo cacheMathingInfo = new CacheMathingInfo();
        public int confirmPlayerNum;
        private uint mapId;
        private ListView<ResAcntBattleLevelInfo> mapList;
        private COM_BATTLE_MAP_TYPE mapType;
        public static string PATH_MATCHING_CONFIRMBOX = "UGUI/Form/System/PvP/Matching/Form_MatchingConfirmBox.prefab";
        public static string PATH_MATCHING_ENTRY = "UGUI/Form/System/PvP/Form_PvPEntry.prefab";
        public static string PATH_MATCHING_INMATCHING = "UGUI/Form/System/PvP/Matching/Form_InMatching.prefab";
        public static string PATH_MATCHING_MULTI = "UGUI/Form/System/PvP/Matching/Form_MultiMatching.prefab";
        public static string PATH_MATCHING_WAITING = "UGUI/Form/System/PvP/Matching/Form_MatchWaiting.prefab";
        public static int s_PVP_RULE_ID = 6;
        public static int s_TRAIN_RULE_ID = 7;
        public TeamInfo teamInfo;

        public void Clear()
        {
            this.bInMatchingTeam = false;
            this.bInMatching = false;
            this.bPlayerActive = true;
            this.bAutoRobotMatch = false;
            EndAutoSingleMatchTimer();
        }

        private static void CloseInMatchingForm()
        {
            Singleton<CUIManager>.GetInstance().CloseForm(PATH_MATCHING_INMATCHING);
            EndAutoSingleMatchTimer();
        }

        public void CloseMatchingConfirm()
        {
            Singleton<CUIManager>.GetInstance().CloseForm(PATH_MATCHING_CONFIRMBOX);
        }

        private static void EndAutoSingleMatchTimer()
        {
            if (Singleton<CMatchingSystem>.instance.AutoSingleMatchTimeSeq != -1)
            {
                Singleton<CTimerManager>.instance.RemoveTimer(Singleton<CMatchingSystem>.instance.AutoSingleMatchTimeSeq);
                Singleton<CMatchingSystem>.instance.AutoSingleMatchTimeSeq = -1;
            }
        }

        public void EntertainMentAddLock(GameObject btnObj)
        {
            if (btnObj != null)
            {
                Transform transform = btnObj.transform;
                if (!Singleton<CFunctionUnlockSys>.instance.FucIsUnlock(RES_SPECIALFUNCUNLOCK_TYPE.RES_SPECIALFUNCUNLOCKTYPE_ENTERTAINMENT))
                {
                    transform.GetComponent<Button>().interactable = false;
                    transform.FindChild("Lock").gameObject.CustomSetActive(true);
                    ResSpecialFucUnlock dataByKey = GameDataMgr.specialFunUnlockDatabin.GetDataByKey((uint) 0x19);
                    transform.FindChild("Lock/Text").GetComponent<Text>().text = Utility.UTF8Convert(dataByKey.szLockedTip);
                }
                else
                {
                    transform.GetComponent<Button>().interactable = true;
                    transform.FindChild("Lock").gameObject.CustomSetActive(false);
                }
            }
        }

        public static uint Get1v1MapId()
        {
            return GetMapIDInner(2);
        }

        public static uint Get2v2MapId()
        {
            return GetMapIDInner(4);
        }

        public static uint Get3v3MapId()
        {
            return GetMapIDInner(6);
        }

        public static uint Get5v5MapId()
        {
            return GetMapIDInner(10);
        }

        public static uint GetCPMap3v3Id()
        {
            uint dwMapId = 0;
            Dictionary<long, object>.Enumerator enumerator = GameDataMgr.cpLevelDatabin.GetEnumerator();
            while (enumerator.MoveNext())
            {
                KeyValuePair<long, object> current = enumerator.Current;
                ResCounterPartLevelInfo info = (ResCounterPartLevelInfo) current.Value;
                if ((info.stLevelCommonInfo.bMaxAcntNum == 6) && (info.bIsSingle == 1))
                {
                    dwMapId = info.dwMapId;
                    break;
                }
            }
            DebugHelper.Assert(dwMapId > 0);
            return dwMapId;
        }

        private static uint GetMapIDInner(byte MaxAcntNum)
        {
            uint dwMapId = 0;
            Dictionary<long, object>.Enumerator enumerator = GameDataMgr.pvpLevelDatabin.GetEnumerator();
            while (enumerator.MoveNext())
            {
                KeyValuePair<long, object> current = enumerator.Current;
                ResAcntBattleLevelInfo info = (ResAcntBattleLevelInfo) current.Value;
                if (info.stLevelCommonInfo.bMaxAcntNum == MaxAcntNum)
                {
                    dwMapId = info.dwMapId;
                    break;
                }
            }
            DebugHelper.Assert(dwMapId > 0);
            return dwMapId;
        }

        private void GetTeamInfo(COMDT_TEAM_INFO teamData)
        {
            this.teamInfo.stTeamInfo.bGameMode = teamData.stTeamInfo.bGameMode;
            this.teamInfo.stTeamInfo.bPkAI = teamData.stTeamInfo.bPkAI;
            this.teamInfo.stTeamInfo.bMapType = teamData.stTeamInfo.bMapType;
            this.teamInfo.stTeamInfo.dwMapId = teamData.stTeamInfo.dwMapId;
            this.mapId = teamData.stTeamInfo.dwMapId;
            this.mapType = (COM_BATTLE_MAP_TYPE) teamData.stTeamInfo.bMapType;
            this.teamInfo.stSelfInfo.ullUid = teamData.stSelfInfo.ullUid;
            this.teamInfo.stSelfInfo.iGameEntity = teamData.stSelfInfo.iGameEntity;
            this.teamInfo.stTeamMaster.ullUid = teamData.stTeamMaster.ullUid;
            this.teamInfo.stTeamMaster.iGameEntity = teamData.stTeamMaster.iGameEntity;
            this.teamInfo.MemInfoList.Clear();
            for (int i = 0; i < teamData.stMemInfo.dwMemNum; i++)
            {
                TeamMember teamMemberInfo = this.GetTeamMemberInfo(teamData.stMemInfo.astMemInfo[i]);
                this.teamInfo.MemInfoList.Add(teamMemberInfo);
            }
        }

        private TeamMember GetTeamMemberInfo(COMDT_TEAMMEMBER_INFO info)
        {
            return new TeamMember { uID = { ullUid = info.stMemberDetail.stMemberUniq.ullUid, iGameEntity = info.stMemberDetail.stMemberUniq.iGameEntity, iLogicWorldId = info.stMemberDetail.iMemberLogicWorldId }, MemberName = StringHelper.UTF8BytesToString(ref info.stMemberDetail.szMemberName), dwMemberHeadId = info.stMemberDetail.dwMemberHeadId, dwMemberLevel = info.stMemberDetail.dwMemberLevel, dwPosOfTeam = info.dwPosOfTeam };
        }

        private void HideRobotBtnGroup()
        {
            CUIFormScript form = Singleton<CUIManager>.GetInstance().GetForm(PATH_MATCHING_ENTRY);
            if (form != null)
            {
                form.GetWidget(5).CustomSetActive(false);
                form.GetWidget(6).CustomSetActive(false);
                form.GetWidget(7).CustomSetActive(false);
                form.GetWidget(8).CustomSetActive(false);
            }
        }

        public override void Init()
        {
            base.Init();
            this.teamInfo = new TeamInfo();
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Matching_OpenEntry, new CUIEventManager.OnUIEventHandler(this.OnMatchingRoom_OpenEntry));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Matching_SelectMap, new CUIEventManager.OnUIEventHandler(this.OnMatching_SelectMap));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Matching_SelectPvP, new CUIEventManager.OnUIEventHandler(this.OnMatching_SelectPvP));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Matching_SelectPkAI, new CUIEventManager.OnUIEventHandler(this.OnMatching_SelectPkAI));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Matching_StartSingle, new CUIEventManager.OnUIEventHandler(this.OnMatching_StartSingle));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Matching_StartMulti, new CUIEventManager.OnUIEventHandler(this.OnMatching_StartMulti));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Matching_CreateTeam, new CUIEventManager.OnUIEventHandler(this.OnMatching_CreateTeam));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Matching_LeaveTeam, new CUIEventManager.OnUIEventHandler(this.OnMatching_LeaveTeam));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Matching_ReqLeave, new CUIEventManager.OnUIEventHandler(this.OnMatching_ReqLeave));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Matching_OpenInvite, new CUIEventManager.OnUIEventHandler(this.OnMatching_OpenInvite));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Matching_KickPlayer, new CUIEventManager.OnUIEventHandler(this.OnMatching_KickPlayer));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Matching_ConfirmMatch, new CUIEventManager.OnUIEventHandler(this.OnMatching_ConfirmGame));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Matching_OpenConfirmBox, new CUIEventManager.OnUIEventHandler(this.OnMatching_OpenConfirmBox));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Matching_Waiting, new CUIEventManager.OnUIEventHandler(this.onMatchWatingTimeUp));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Matching_RuleView, new CUIEventManager.OnUIEventHandler(this.OnMatching_RuleView));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Matching_BtnGroup_Click, new CUIEventManager.OnUIEventHandler(this.OnBtnGroupClick));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Matching_BtnGroup_ClickClose, new CUIEventManager.OnUIEventHandler(this.OnBtnGroupClose));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Matching_Robot_BtnGroup_Click, new CUIEventManager.OnUIEventHandler(this.OnRobotBtnGroupClick));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Matching_Begin1v1, new CUIEventManager.OnUIEventHandler(this.OnMatching_Begin1v1));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Matching_Begin3v3Team, new CUIEventManager.OnUIEventHandler(this.OnMatching_Begin3v3Multi));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Matching_Begin5v5Team, new CUIEventManager.OnUIEventHandler(this.OnMatching_Begin5v5Multi));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.MatchingExt_BeginMelee, new CUIEventManager.OnUIEventHandler(this.OnMatching_BeginMeleeMulti));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.MatchingExt_BeginFire, new CUIEventManager.OnUIEventHandler(this.OnMatching_BeginFireMulti));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Matching_Begin5v5LadderIn2, new CUIEventManager.OnUIEventHandler(this.OnMatching_Begin5v5LadderIn2));
            Singleton<EventRouter>.GetInstance().AddEventHandler<byte, string>(EventID.INVITE_ERRCODE_NTF, new Action<byte, string>(this, (IntPtr) this.OnInviteErrCodeNtf));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Matching_Robot1V1, new CUIEventManager.OnUIEventHandler(this.OnMatching_Robot1V1));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Matching_RobotCounterPartSingle, new CUIEventManager.OnUIEventHandler(this.OnMatching_RobotCPSingle));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Matching_RobotTeam, new CUIEventManager.OnUIEventHandler(this.OnMatching_RobotTeam));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Matching_Guide_1v1, new CUIEventManager.OnUIEventHandler(this.OnMatchingGuide1v1));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Matching_Guide_3v3, new CUIEventManager.OnUIEventHandler(this.OnMatchingGuide3v3));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Matching_Guide_5v5, new CUIEventManager.OnUIEventHandler(this.OnMatchingGuide5v5));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Matching_Guide_Casting, new CUIEventManager.OnUIEventHandler(this.OnMatchingGuideCasting));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Matching_Guide_Jungle, new CUIEventManager.OnUIEventHandler(this.OnMatchingGuideJungle));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Matching_Training, new CUIEventManager.OnUIEventHandler(this.OnMatchingTraining));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Matching_GuidePanel, new CUIEventManager.OnUIEventHandler(this.OnMatchingGuidePanel));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Matching_GuideAdvance, new CUIEventManager.OnUIEventHandler(this.OnMatchingGuideAdvance));
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Matching_GuideAdvanceConfirm, new CUIEventManager.OnUIEventHandler(this.OpenGuideAdvancePage));
        }

        private bool IsTraningLevelLocked(CRoleInfo roleInfo, int trainingLevelCompletedBit)
        {
            switch (trainingLevelCompletedBit)
            {
                case 0x53:
                    return !roleInfo.IsGuidedStateSet(0x62);

                case 0x54:
                    return !roleInfo.IsGuidedStateSet(0x55);

                case 0x55:
                    return !roleInfo.IsGuidedStateSet(0x53);

                case 0:
                    return false;

                case 0x62:
                    return !roleInfo.IsGuidedStateSet(0);
            }
            return false;
        }

        private static void MatchPunishmentWaiting(float time)
        {
            CUIFormScript script = Singleton<CUIManager>.GetInstance().OpenForm(PATH_MATCHING_WAITING, false, true);
            if (script != null)
            {
                GameObject widget = script.GetWidget(0);
                if (widget != null)
                {
                    Text component = widget.GetComponent<Text>();
                    if (component != null)
                    {
                        component.text = Singleton<CTextManager>.GetInstance().GetText("FailToEnterQuque");
                    }
                }
                GameObject obj3 = script.GetWidget(1);
                if (obj3 != null)
                {
                    Text text2 = obj3.GetComponent<Text>();
                    if (text2 != null)
                    {
                        text2.text = Singleton<CTextManager>.GetInstance().GetText("PunishmentDescribe");
                    }
                }
                GameObject obj4 = script.GetWidget(2);
                if (obj4 != null)
                {
                    CUITimerScript script2 = obj4.GetComponent<CUITimerScript>();
                    if (script2 != null)
                    {
                        script2.SetTotalTime(time);
                        script2.StartTimer();
                    }
                }
            }
        }

        private static void OnAutoSingleMatchTimer(int inTimeSeq)
        {
            Singleton<CMatchingSystem>.instance.bPlayerActive = false;
            Singleton<CMatchingSystem>.instance.OnMatching_ReqLeave(null);
        }

        private void OnBtnGroupClick(CUIEvent uiEvent)
        {
            GameObject gameObject;
            if (uiEvent.m_eventParams.tag == 1)
            {
                uiEvent.m_srcFormScript.GetWidget(3).CustomSetActive(true);
                gameObject = uiEvent.m_srcFormScript.transform.FindChild("panelGroup2/btnGroup/Button3").gameObject;
            }
            else
            {
                uiEvent.m_srcFormScript.GetWidget(4).CustomSetActive(true);
                gameObject = uiEvent.m_srcFormScript.transform.FindChild("panelGroup3/btnGroup/Button4").gameObject;
            }
            uiEvent.m_srcFormScript.GetWidget(2).CustomSetActive(false);
            this.EntertainMentAddLock(gameObject);
            this.ShowBonusImage(uiEvent.m_srcFormScript);
        }

        private void OnBtnGroupClose(CUIEvent uiEvent)
        {
            CUIFormScript form = Singleton<CUIManager>.GetInstance().GetForm(PATH_MATCHING_ENTRY);
            if (form != null)
            {
                form.GetWidget(3).CustomSetActive(false);
                form.GetWidget(4).CustomSetActive(false);
                form.GetWidget(9).CustomSetActive(false);
                form.GetWidget(10).CustomSetActive(true);
                if (CSysDynamicBlock.bLobbyEntryBlocked)
                {
                    form.GetWidget(10).CustomSetActive(false);
                }
                form.GetWidget(2).CustomSetActive(true);
                this.ShowBonusImage(form);
                this.HideRobotBtnGroup();
                Singleton<CMiShuSystem>.instance.CheckActPlayModeTipsForPvpEntry();
            }
        }

        private void OnInviteErrCodeNtf(byte errorCode, string userName)
        {
            if (errorCode == 20)
            {
                CloseInMatchingForm();
                Singleton<CMatchingSystem>.GetInstance().bInMatching = false;
                Singleton<CMatchingSystem>.GetInstance().bInMatchingTeam = false;
            }
        }

        [MessageHandler(0x7ee)]
        public static void OnLeaveTeam(CSPkg msg)
        {
            Singleton<CUIManager>.GetInstance().CloseSendMsgAlert();
            if (msg.stPkgData.stAcntLeaveRsp.bResult == 0)
            {
                Singleton<CMatchingSystem>.GetInstance().bInMatchingTeam = false;
                Singleton<CUIManager>.GetInstance().CloseForm(PATH_MATCHING_MULTI);
                Singleton<CTopLobbyEntry>.GetInstance().CloseForm();
                Singleton<CInviteSystem>.GetInstance().CloseInviteForm();
            }
            else
            {
                Singleton<CUIManager>.GetInstance().OpenTips(Utility.ProtErrCodeToStr(0x7ee, msg.stPkgData.stAcntLeaveRsp.bResult), false, 1f, null, new object[0]);
            }
        }

        private void OnMatching_Begin1v1(CUIEvent uiEvent)
        {
            this.cacheMathingInfo.uiEventId = uiEvent.m_eventID;
            this.cacheMathingInfo.mapId = uiEvent.m_eventParams.tagUInt;
            this.cacheMathingInfo.CanGameAgain = true;
            ReqStartSingleMatching(uiEvent.m_eventParams.tagUInt, false, COM_BATTLE_MAP_TYPE.COM_BATTLE_MAP_TYPE_VERSUS);
        }

        private void OnMatching_Begin3v3Multi(CUIEvent uiEvent)
        {
            if (!MonoSingleton<NewbieGuideManager>.GetInstance().CheckTriggerTime(NewbieGuideTriggerTimeType.onClick33Team, new uint[0]))
            {
                this.cacheMathingInfo.uiEventId = uiEvent.m_eventID;
                this.cacheMathingInfo.mapId = uiEvent.m_eventParams.tagUInt;
                this.cacheMathingInfo.CanGameAgain = true;
                ReqCreateTeam(uiEvent.m_eventParams.tagUInt, false, COM_BATTLE_MAP_TYPE.COM_BATTLE_MAP_TYPE_VERSUS, COM_AI_LEVEL.COM_AI_LEVEL_MIDDLE_OF_NORMALBATTLE);
                Singleton<CNewbieAchieveSys>.GetInstance().trackFlag = CNewbieAchieveSys.TrackFlag.SINGLE_MATCH_3V3_ENTER;
            }
        }

        private void OnMatching_Begin5v5LadderIn2(CUIEvent uiEvent)
        {
            this.cacheMathingInfo.uiEventId = uiEvent.m_eventID;
            this.cacheMathingInfo.mapId = CLevelCfgLogicManager.GetRankBattleMapID();
            this.cacheMathingInfo.CanGameAgain = true;
            ReqCreateTeam(this.cacheMathingInfo.mapId, false, COM_BATTLE_MAP_TYPE.COM_BATTLE_MAP_TYPE_RANK, COM_AI_LEVEL.COM_AI_LEVEL_MIDDLE_OF_NORMALBATTLE);
        }

        private void OnMatching_Begin5v5Multi(CUIEvent uiEvent)
        {
            if (!MonoSingleton<NewbieGuideManager>.GetInstance().CheckTriggerTime(NewbieGuideTriggerTimeType.onClick55Team, new uint[0]))
            {
                this.cacheMathingInfo.uiEventId = uiEvent.m_eventID;
                this.cacheMathingInfo.mapId = uiEvent.m_eventParams.tagUInt;
                this.cacheMathingInfo.CanGameAgain = true;
                ReqCreateTeam(uiEvent.m_eventParams.tagUInt, false, COM_BATTLE_MAP_TYPE.COM_BATTLE_MAP_TYPE_VERSUS, COM_AI_LEVEL.COM_AI_LEVEL_MIDDLE_OF_NORMALBATTLE);
                Singleton<CNewbieAchieveSys>.GetInstance().trackFlag = CNewbieAchieveSys.TrackFlag.SINGLE_MATCH_5V5_ENTER;
            }
        }

        private void OnMatching_BeginFireMulti(CUIEvent uiEvent)
        {
            bool flag = false;
            if (uiEvent.m_srcFormScript != null)
            {
                flag = MonoSingleton<NewbieGuideManager>.GetInstance().CheckTriggerTime(NewbieGuideTriggerTimeType.onClickFireMatch, new uint[0]);
            }
            if (!flag)
            {
                this.cacheMathingInfo.uiEventId = uiEvent.m_eventID;
                this.cacheMathingInfo.mapId = uiEvent.m_eventParams.tagUInt;
                this.cacheMathingInfo.CanGameAgain = true;
                ReqCreateTeam(uiEvent.m_eventParams.tagUInt, false, COM_BATTLE_MAP_TYPE.COM_BATTLE_MAP_TYPE_ENTERTAINMENT, COM_AI_LEVEL.COM_AI_LEVEL_MIDDLE_OF_NORMALBATTLE);
            }
        }

        private void OnMatching_BeginMeleeMulti(CUIEvent uiEvent)
        {
            if (!uiEvent.m_srcWidget.GetComponent<Button>().interactable)
            {
                ResSpecialFucUnlock dataByKey = GameDataMgr.specialFunUnlockDatabin.GetDataByKey((uint) 0x19);
                Singleton<CUIManager>.GetInstance().OpenTips(dataByKey.szLockedTip, false, 1f, null, new object[0]);
            }
            else
            {
                this.cacheMathingInfo.uiEventId = uiEvent.m_eventID;
                this.cacheMathingInfo.mapId = uiEvent.m_eventParams.tagUInt;
                this.cacheMathingInfo.CanGameAgain = true;
                ReqCreateTeam(uiEvent.m_eventParams.tagUInt, false, COM_BATTLE_MAP_TYPE.COM_BATTLE_MAP_TYPE_ENTERTAINMENT, COM_AI_LEVEL.COM_AI_LEVEL_MIDDLE_OF_NORMALBATTLE);
                Singleton<CNewbieAchieveSys>.GetInstance().trackFlag = CNewbieAchieveSys.TrackFlag.None;
            }
        }

        private void OnMatching_ConfirmGame(CUIEvent uiEvent)
        {
            Button component = uiEvent.m_srcWidget.GetComponent<Button>();
            if (component.interactable)
            {
                if (CFakePvPHelper.bInFakeConfirm)
                {
                    CFakePvPHelper.OnSelfConfirmed(uiEvent.m_srcFormScript.gameObject, this.currentMapPlayerNum);
                }
                else
                {
                    SendMatchingConfirm();
                }
                component.interactable = false;
            }
        }

        private void OnMatching_CreateTeam(CUIEvent uiEvent)
        {
            ResAcntBattleLevelInfo dataByKey = GameDataMgr.pvpLevelDatabin.GetDataByKey(this.mapId);
            DebugHelper.Assert(dataByKey != null);
            if (dataByKey.stLevelCommonInfo.bMaxAcntNum <= 2)
            {
                Singleton<CUIManager>.GetInstance().OpenTips("PVP_Single_Map_Can_Not_Match", true, 1f, null, new object[0]);
            }
            else
            {
                COM_BATTLE_MAP_TYPE mapType = COM_BATTLE_MAP_TYPE.COM_BATTLE_MAP_TYPE_VERSUS;
                if (dataByKey.stLevelCommonInfo.bChaosPickRule > 0)
                {
                    mapType = COM_BATTLE_MAP_TYPE.COM_BATTLE_MAP_TYPE_ENTERTAINMENT;
                }
                ReqCreateTeam(this.mapId, this.bPkAI, mapType, COM_AI_LEVEL.COM_AI_LEVEL_MIDDLE_OF_NORMALBATTLE);
            }
        }

        private void OnMatching_KickPlayer(CUIEvent uiEvent)
        {
            if (this.IsSelfTeamMaster)
            {
                GameObject gameObject = uiEvent.m_srcWidget.transform.parent.parent.gameObject;
                byte num = 0;
                if (gameObject.name.EndsWith("1"))
                {
                    num = 0;
                }
                else if (gameObject.name.EndsWith("2"))
                {
                    num = 1;
                }
                else if (gameObject.name.EndsWith("3"))
                {
                    num = 2;
                }
                for (int i = 0; i < this.teamInfo.MemInfoList.Count; i++)
                {
                    if (this.teamInfo.MemInfoList[i].dwPosOfTeam == num)
                    {
                        ReqKickPlayer(this.teamInfo.MemInfoList[i].uID);
                        break;
                    }
                }
            }
            else
            {
                DebugHelper.Assert(false, "Not Team Master!");
            }
        }

        private void OnMatching_LeaveTeam(CUIEvent uiEvent)
        {
            if (this.bInMatchingTeam)
            {
                ReqLeaveTeam();
            }
            else
            {
                DebugHelper.Assert(false, "Not In Matching Team");
            }
        }

        private void OnMatching_OpenConfirmBox(CUIEvent uiEvent)
        {
            Handheld.Vibrate();
            this.currentMapPlayerNum = CLevelCfgLogicManager.GetMapPlayerNum(this.mapId, this.mapType);
            this.confirmPlayerNum = 0;
            Assets.Scripts.GameSystem.RoomInfo roomInfo = Singleton<CRoomSystem>.GetInstance().roomInfo;
            DebugHelper.Assert(roomInfo != null, "Room Info is NULL!!!");
            if (roomInfo != null)
            {
                CUIFormScript form = Singleton<CUIManager>.GetInstance().OpenForm(PATH_MATCHING_CONFIRMBOX, false, true);
                CMatchingView.InitConfirmBox(form.gameObject, this.currentMapPlayerNum, ref roomInfo, form);
                MonoSingleton<NewbieGuideManager>.GetInstance().StopCurrentGuide();
                MonoSingleton<NewbieGuideManager>.GetInstance().CheckTriggerTime(NewbieGuideTriggerTimeType.onOpenMatchingConfirmBox, new uint[0]);
            }
        }

        private void OnMatching_OpenInvite(CUIEvent uiEvent)
        {
            CUIEvent event2 = new CUIEvent {
                m_eventID = enUIEventID.Invite_OpenForm
            };
            event2.m_eventParams.tag = 2;
            event2.m_eventParams.tag2 = this.teamInfo.stTeamInfo.bMapType;
            Singleton<CUIEventManager>.GetInstance().DispatchUIEvent(event2);
        }

        private void OnMatching_ReqLeave(CUIEvent uiEvent)
        {
            if (this.bInMatching)
            {
                ReqLeaveMatching(uiEvent != null);
            }
            else
            {
                DebugHelper.Assert(false, "Not In Matching");
            }
        }

        private void OnMatching_Robot1V1(CUIEvent uiEvent)
        {
            this.cacheMathingInfo.uiEventId = uiEvent.m_eventID;
            this.cacheMathingInfo.mapId = uiEvent.m_eventParams.tagUInt;
            this.cacheMathingInfo.CanGameAgain = true;
            ReqStartSingleMatching(uiEvent.m_eventParams.tagUInt, true, COM_BATTLE_MAP_TYPE.COM_BATTLE_MAP_TYPE_VERSUS);
        }

        private void OnMatching_RobotCPSingle(CUIEvent uiEvent)
        {
            this.cacheMathingInfo.uiEventId = uiEvent.m_eventID;
            this.cacheMathingInfo.mapId = uiEvent.m_eventParams.tagUInt;
            this.cacheMathingInfo.CanGameAgain = true;
            this.cacheMathingInfo.AILevel = (COM_AI_LEVEL) uiEvent.m_eventParams.tag;
            CSPkg msg = NetworkModule.CreateDefaultCSPKG(0x7da);
            msg.stPkgData.stMatchReq.bMapType = 2;
            msg.stPkgData.stMatchReq.dwMapId = uiEvent.m_eventParams.tagUInt;
            msg.stPkgData.stMatchReq.bIsPkAI = Convert.ToByte(COM_GAMEPK_TYPE.COM_GAMEPK_WITH_AI);
            msg.stPkgData.stMatchReq.bGameMode = 1;
            msg.stPkgData.stMatchReq.bAILevel = Convert.ToByte(uiEvent.m_eventParams.tag);
            Singleton<NetworkModule>.GetInstance().SendLobbyMsg(ref msg, true);
            Singleton<CNewbieAchieveSys>.GetInstance().trackFlag = CNewbieAchieveSys.TrackFlag.SINGLE_COMBAT_3V3_ENTER;
            MonoSingleton<NewbieGuideManager>.GetInstance().SetNewbieBit(9, true);
        }

        private void OnMatching_RobotTeam(CUIEvent uiEvent)
        {
            this.cacheMathingInfo.uiEventId = uiEvent.m_eventID;
            this.cacheMathingInfo.mapId = uiEvent.m_eventParams.tagUInt;
            this.cacheMathingInfo.AILevel = (COM_AI_LEVEL) uiEvent.m_eventParams.tag;
            this.cacheMathingInfo.CanGameAgain = true;
            uint tagUInt = uiEvent.m_eventParams.tagUInt;
            COM_AI_LEVEL tag = (COM_AI_LEVEL) uiEvent.m_eventParams.tag;
            COM_BATTLE_MAP_TYPE mapType = COM_BATTLE_MAP_TYPE.COM_BATTLE_MAP_TYPE_VERSUS;
            if (tagUInt >= 0x15f90)
            {
                mapType = COM_BATTLE_MAP_TYPE.COM_BATTLE_MAP_TYPE_ENTERTAINMENT;
            }
            ReqCreateTeam(tagUInt, true, mapType, tag);
        }

        private void OnMatching_RuleView(CUIEvent uiEvent)
        {
            int txtKey = s_PVP_RULE_ID;
            CUIFormScript form = Singleton<CUIManager>.instance.GetForm(PATH_MATCHING_ENTRY);
            if (form != null)
            {
                GameObject gameObject = form.transform.FindChild("panelGroup4").gameObject;
                if ((gameObject != null) && gameObject.activeSelf)
                {
                    txtKey = s_TRAIN_RULE_ID;
                }
            }
            Singleton<CUIManager>.GetInstance().OpenInfoForm(txtKey);
        }

        private void OnMatching_SelectMap(CUIEvent uiEvent)
        {
            CUIFormScript srcFormScript = uiEvent.m_srcFormScript;
            int selectedIndex = uiEvent.m_srcWidget.GetComponent<CUIListScript>().GetSelectedIndex();
            ResAcntBattleLevelInfo info = this.mapList[selectedIndex];
            this.mapId = info.dwMapId;
            srcFormScript.gameObject.transform.Find("Panel_Main/TxtMap").gameObject.GetComponent<Text>().text = StringHelper.BytesToString(info.stLevelCommonInfo.szName);
        }

        private void OnMatching_SelectPkAI(CUIEvent uiEvent)
        {
            CUIFormScript srcFormScript = uiEvent.m_srcFormScript;
            this.SetPKMode(srcFormScript, true);
        }

        private void OnMatching_SelectPvP(CUIEvent uiEvent)
        {
            CUIFormScript srcFormScript = uiEvent.m_srcFormScript;
            this.SetPKMode(srcFormScript, false);
        }

        private void OnMatching_StartMulti(CUIEvent uiEvent)
        {
            if (this.mapId > 0)
            {
                ReqStartMultiMatching();
            }
        }

        private void OnMatching_StartSingle(CUIEvent uiEvent)
        {
            if (this.mapId > 0)
            {
                ReqStartSingleMatching(this.mapId, this.bPkAI, COM_BATTLE_MAP_TYPE.COM_BATTLE_MAP_TYPE_VERSUS);
            }
        }

        private void OnMatchingGuide1v1(CUIEvent uiEvent)
        {
            LobbyLogic.ReqStartGuideLevel11(true);
        }

        private void OnMatchingGuide3v3(CUIEvent uiEvent)
        {
            CRoleInfo masterRoleInfo = Singleton<CRoleInfoManager>.GetInstance().GetMasterRoleInfo();
            if ((masterRoleInfo == null) || !masterRoleInfo.IsGuidedStateSet(0x53))
            {
                Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.GetInstance().GetText("Trainlevel_Text_Lock_3"), false);
            }
            else
            {
                LobbyLogic.ReqStartGuideLevel33(true);
            }
        }

        private void OnMatchingGuide5v5(CUIEvent uiEvent)
        {
            CRoleInfo masterRoleInfo = Singleton<CRoleInfoManager>.GetInstance().GetMasterRoleInfo();
            if ((masterRoleInfo == null) || !masterRoleInfo.IsGuidedStateSet(0))
            {
                Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.GetInstance().GetText("Trainlevel_Text_Lock_1"), false);
            }
            else
            {
                LobbyLogic.ReqStartGuideLevel55(true);
            }
        }

        private void OnMatchingGuideAdvance(CUIEvent uiEvent)
        {
            if (GameDataMgr.globalInfoDatabin.GetDataByKey((uint) 0x9b).dwConfValue > 0)
            {
                Singleton<CUIManager>.instance.OpenMessageBoxWithCancel(Singleton<CTextManager>.GetInstance().GetText("Tutorial_Wifi_Alert"), enUIEventID.Matching_GuideAdvanceConfirm, enUIEventID.Matching_GuideAdvanceCancel, false);
            }
            else
            {
                Singleton<CUIManager>.instance.OpenTips("Common_Not_Open", true, 1f, null, new object[0]);
            }
        }

        private void OnMatchingGuideCasting(CUIEvent uiEvent)
        {
            CRoleInfo masterRoleInfo = Singleton<CRoleInfoManager>.GetInstance().GetMasterRoleInfo();
            if ((masterRoleInfo == null) || !masterRoleInfo.IsGuidedStateSet(0x62))
            {
                Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.GetInstance().GetText("Trainlevel_Text_Lock_2"), false);
            }
            else
            {
                LobbyLogic.ReqStartGuideLevelCasting(true);
            }
        }

        private void OnMatchingGuideJungle(CUIEvent uiEvent)
        {
            CRoleInfo masterRoleInfo = Singleton<CRoleInfoManager>.GetInstance().GetMasterRoleInfo();
            if ((masterRoleInfo == null) || !masterRoleInfo.IsGuidedStateSet(0x55))
            {
                Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.GetInstance().GetText("Trainlevel_Text_Lock_4"), false);
            }
            else
            {
                LobbyLogic.ReqStartGuideLevelJungle(true);
            }
        }

        private void OnMatchingGuidePanel(CUIEvent uiEvent)
        {
            Singleton<CUIManager>.instance.CloseForm(CRoomSystem.PATH_CREATE_ROOM);
            CUIFormScript form = Singleton<CUIManager>.instance.GetForm(PATH_MATCHING_ENTRY);
            if (form != null)
            {
                form.GetWidget(9).CustomSetActive(true);
                form.GetWidget(10).CustomSetActive(false);
                form.GetWidget(2).CustomSetActive(false);
                form.GetWidget(3).CustomSetActive(false);
                form.GetWidget(4).CustomSetActive(false);
                this.ShowAwards(form);
                this.ShowBonusImage(form);
                CRoleInfo masterRoleInfo = Singleton<CRoleInfoManager>.instance.GetMasterRoleInfo();
                if (masterRoleInfo != null)
                {
                    masterRoleInfo.SetNewbieAchieve(0x11, true, true);
                }
            }
            MonoSingleton<NewbieGuideManager>.GetInstance().CheckTriggerTime(NewbieGuideTriggerTimeType.onEntryTrainLevelEntry, new uint[0]);
        }

        private void OnMatchingRoom_OpenEntry(CUIEvent uiEvent)
        {
            Singleton<CNewbieAchieveSys>.GetInstance().trackFlag = CNewbieAchieveSys.TrackFlag.None;
            if (Singleton<CFunctionUnlockSys>.instance.FucIsUnlock(RES_SPECIALFUNCUNLOCK_TYPE.RES_SPECIALFUNCUNLOCKTYPE_PVPMODE))
            {
                if (Singleton<CRoleInfoManager>.GetInstance().GetMasterRoleInfo() != null)
                {
                    if (this.IsInMatching)
                    {
                        Singleton<CUIManager>.GetInstance().OpenTips("PVP_Matching", true, 1f, null, new object[0]);
                    }
                    else
                    {
                        enPvPEntryFormWidget none = enPvPEntryFormWidget.None;
                        if (uiEvent.m_eventParams.tag != 0)
                        {
                            none = (enPvPEntryFormWidget) uiEvent.m_eventParams.tag;
                        }
                        this.OpenPvPEntry(none);
                    }
                }
            }
            else
            {
                ResSpecialFucUnlock dataByKey = GameDataMgr.specialFunUnlockDatabin.GetDataByKey((uint) 10);
                Singleton<CUIManager>.instance.OpenTips(Utility.UTF8Convert(dataByKey.szLockedTip), false, 1f, null, new object[0]);
            }
        }

        private void OnMatchingTraining(CUIEvent uiEvent)
        {
            ReqStartTrainingLevel();
        }

        public void onMatchWatingTimeUp(CUIEvent uiEvent)
        {
            Singleton<CUIManager>.GetInstance().CloseForm(PATH_MATCHING_WAITING);
        }

        private void OnOpenLadder(CUIEvent uiEvent)
        {
            Singleton<CNewbieAchieveSys>.GetInstance().trackFlag = CNewbieAchieveSys.TrackFlag.None;
            Singleton<CUIManager>.GetInstance().OpenForm(CLadderSystem.FORM_LADDER_ENTRY, false, true);
        }

        [MessageHandler(0x7f0)]
        public static void OnPlayerConfirmMatching(CSPkg msg)
        {
            CUIFormScript form = Singleton<CUIManager>.GetInstance().GetForm(PATH_MATCHING_CONFIRMBOX);
            if (form != null)
            {
                CMatchingSystem instance = Singleton<CMatchingSystem>.GetInstance();
                instance.confirmPlayerNum++;
                CMatchingView.UpdateConfirmBox(form.gameObject, msg.stPkgData.stRoomConfirmRsp.ullUid);
                Assets.Scripts.GameSystem.RoomInfo roomInfo = Singleton<CRoomSystem>.GetInstance().roomInfo;
                if ((roomInfo != null) && roomInfo.roomAttrib.bWarmBattle)
                {
                    CFakePvPHelper.UpdateConfirmBox(form.gameObject, Singleton<CMatchingSystem>.GetInstance().currentMapPlayerNum);
                }
            }
        }

        [MessageHandler(0x7e7)]
        public static void OnPlayerJoin(CSPkg msg)
        {
            Singleton<CUIManager>.GetInstance().CloseSendMsgAlert();
            if (msg.stPkgData.stJoinTeamRsp.bErrCode == 0)
            {
                CMatchingSystem instance = Singleton<CMatchingSystem>.GetInstance();
                instance.mapId = msg.stPkgData.stJoinTeamRsp.stJoinRsp.stOfSucc.stTeamInfo.dwMapId;
                instance.mapType = (COM_BATTLE_MAP_TYPE) msg.stPkgData.stJoinTeamRsp.stJoinRsp.stOfSucc.stTeamInfo.bMapType;
                instance.bInMatchingTeam = true;
                instance.GetTeamInfo(msg.stPkgData.stJoinTeamRsp.stJoinRsp.stOfSucc);
                CUIFormScript script = Singleton<CUIManager>.GetInstance().OpenForm(PATH_MATCHING_MULTI, false, true);
                Singleton<CTopLobbyEntry>.GetInstance().OpenForm();
                Singleton<CInviteSystem>.GetInstance().OpenInviteForm(COM_INVITE_JOIN_TYPE.COM_INVITE_JOIN_TEAM);
                CLevelCfgLogicManager.SetTeamData(script.gameObject, instance.teamInfo);
                instance.cacheMathingInfo.CanGameAgain = instance.IsSelfTeamMaster;
                if (!instance.IsSelfTeamMaster)
                {
                    MonoSingleton<NewbieGuideManager>.instance.StopCurrentGuide();
                }
            }
            else if (msg.stPkgData.stJoinTeamRsp.bErrCode == 0x11)
            {
                MatchPunishmentWaiting((float) msg.stPkgData.stJoinTeamRsp.stJoinRsp.stOfBePunished.dwLeftSec);
            }
            else if (msg.stPkgData.stJoinTeamRsp.bErrCode == 0x16)
            {
                Singleton<CUIManager>.instance.OpenTips("HuoKenPlayModeNotOpenTip", true, 1f, null, new object[0]);
            }
            else
            {
                Singleton<CUIManager>.instance.OpenTips(Utility.ProtErrCodeToStr(0x7e7, msg.stPkgData.stJoinTeamRsp.bErrCode), false, 1f, null, new object[0]);
            }
        }

        public static void OnPlayerLeaveMatching()
        {
            Singleton<CMatchingSystem>.GetInstance().bInMatching = false;
            Singleton<CMatchingSystem>.GetInstance().bInMatchingTeam = false;
            if (Singleton<CMatchingSystem>.instance.bPlayerActive)
            {
                if (Singleton<CMatchingSystem>.instance.bAutoRobotMatch)
                {
                    if (Singleton<CUIManager>.GetInstance().GetForm(CTaskSys.TASK_FORM_PATH) != null)
                    {
                        Singleton<CTaskSys>.instance.OnCloseTaskForm(null);
                    }
                    Singleton<CTaskSys>.instance.OnOpenTaskForm(null);
                }
            }
            else
            {
                stUIEventParams par = new stUIEventParams();
                uint.TryParse(Singleton<CTextManager>.instance.GetText("MapID_Robot_3V3_1"), out par.tagUInt);
                par.tag = (int) Singleton<CMatchingSystem>.GetInstance().cacheMathingInfo.AILevel;
                Singleton<CUIEventManager>.GetInstance().DispatchUIEvent(enUIEventID.Matching_RobotCounterPartSingle, par);
            }
            CloseInMatchingForm();
            Singleton<CMatchingSystem>.instance.bPlayerActive = true;
            Singleton<CMatchingSystem>.instance.bAutoRobotMatch = false;
        }

        private void OnRobotBtnGroupClick(CUIEvent uiEvent)
        {
            this.HideRobotBtnGroup();
            CUIFormScript form = Singleton<CUIManager>.GetInstance().GetForm(PATH_MATCHING_ENTRY);
            if (form != null)
            {
                int tag = uiEvent.m_eventParams.tag;
                if (tag >= 0)
                {
                    if ((tag == 3) && !uiEvent.m_srcWidget.GetComponent<Button>().interactable)
                    {
                        ResSpecialFucUnlock dataByKey = GameDataMgr.specialFunUnlockDatabin.GetDataByKey((uint) 0x19);
                        Singleton<CUIManager>.GetInstance().OpenTips(dataByKey.szLockedTip, false, 1f, null, new object[0]);
                    }
                    else
                    {
                        GameObject widget = form.GetWidget(5 + tag);
                        if (widget != null)
                        {
                            widget.CustomSetActive(true);
                        }
                        if (tag == 1)
                        {
                            MonoSingleton<NewbieGuideManager>.GetInstance().CheckTriggerTime(NewbieGuideTriggerTimeType.onClick3v3AI, new uint[0]);
                        }
                    }
                }
            }
        }

        [MessageHandler(0x7ed)]
        public static void OnSelfBeKicked(CSPkg msg)
        {
            Singleton<CMatchingSystem>.GetInstance().bInMatchingTeam = false;
            Singleton<CUIManager>.GetInstance().CloseForm(PATH_MATCHING_MULTI);
            Singleton<CTopLobbyEntry>.GetInstance().CloseForm();
            Singleton<CInviteSystem>.GetInstance().CloseInviteForm();
            Singleton<CUIManager>.GetInstance().OpenTips("PVP_Kick_Tip", true, 1f, null, new object[0]);
        }

        [MessageHandler(0x7db)]
        public static void OnStartMatching(CSPkg msg)
        {
            if (msg.stPkgData.stMatchRsp.bResult == 1)
            {
                Singleton<CMatchingSystem>.GetInstance().bInMatchingTeam = false;
                Singleton<CMatchingSystem>.GetInstance().bInMatching = false;
                Singleton<CRoomSystem>.GetInstance().BuildRoomInfo(msg.stPkgData.stMatchRsp.stMatchResDetail.stMatchSucc);
                Singleton<CHeroSelectSystem>.GetInstance().SetPvpDataByRoomInfo(Singleton<CRoomSystem>.GetInstance().roomInfo, 1);
                EndAutoSingleMatchTimer();
                Singleton<CUIManager>.GetInstance().CloseAllFormExceptLobby(true);
            }
            else if (msg.stPkgData.stMatchRsp.bResult == 2)
            {
                Singleton<CUIManager>.GetInstance().CloseAllFormExceptLobby(true);
                Singleton<CMatchingSystem>.GetInstance().bInMatching = true;
                OpenInMatchingForm();
                Singleton<CChatController>.instance.model.channelMgr.Clear(EChatChannel.Team, 0L, 0);
                Singleton<CChatController>.instance.model.channelMgr.Clear(EChatChannel.Room, 0L, 0);
                Singleton<CChatController>.instance.model.channelMgr.SetChatTab(CChatChannelMgr.EChatTab.Normal);
                Singleton<CChatController>.instance.ShowPanel(true, false);
                Singleton<CChatController>.instance.view.UpView(false);
                Singleton<CChatController>.instance.model.sysData.ClearEntryText();
                Singleton<CTopLobbyEntry>.instance.CloseForm();
                if (msg.stPkgData.stMatchRsp.stMatchResDetail.stMatchProcess.bReason == 0x12)
                {
                    Singleton<CUIManager>.GetInstance().OpenTips(Singleton<CTextManager>.GetInstance().GetText("Err_NM_Othercancel"), false, 1f, null, new object[0]);
                }
                else if (msg.stPkgData.stMatchRsp.stMatchResDetail.stMatchProcess.bReason == 0x13)
                {
                    Singleton<CUIManager>.GetInstance().OpenTips(Singleton<CTextManager>.GetInstance().GetText("Err_NM_Otherexit"), false, 1f, null, new object[0]);
                    Singleton<CSoundManager>.GetInstance().PostEvent("UI_matching_lost", null);
                }
            }
            else if (msg.stPkgData.stMatchRsp.bResult == 3)
            {
                if (msg.stPkgData.stMatchRsp.stMatchResDetail.stMatchErr.iErrCode == 1)
                {
                    Singleton<CUIManager>.GetInstance().CloseSendMsgAlert();
                    CloseInMatchingForm();
                    Singleton<CMatchingSystem>.GetInstance().bInMatching = false;
                    Singleton<CMatchingSystem>.GetInstance().bInMatchingTeam = false;
                    DateTime banTime = MonoSingleton<IDIPSys>.GetInstance().GetBanTime(COM_ACNT_BANTIME_TYPE.COM_ACNT_BANTIME_BANPLAYPVP);
                    object[] args = new object[] { banTime.Year, banTime.Month, banTime.Day, banTime.Hour, banTime.Minute };
                    string strContent = string.Format("您被禁止竞技！截止时间为{0}年{1}月{2}日{3}时{4}分", args);
                    Singleton<CUIManager>.GetInstance().OpenMessageBox(strContent, false);
                }
                else if (msg.stPkgData.stMatchRsp.stMatchResDetail.stMatchErr.iErrCode == 2)
                {
                    Singleton<CUIManager>.GetInstance().CloseSendMsgAlert();
                    CloseInMatchingForm();
                    Singleton<CMatchingSystem>.GetInstance().bInMatching = false;
                    Singleton<CMatchingSystem>.GetInstance().bInMatchingTeam = false;
                    Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.instance.GetText("Union_Battle_Tips1"), false);
                }
                else if (msg.stPkgData.stMatchRsp.stMatchResDetail.stMatchErr.iErrCode == 3)
                {
                    Singleton<CUIManager>.GetInstance().CloseSendMsgAlert();
                    CloseInMatchingForm();
                    Singleton<CMatchingSystem>.GetInstance().bInMatching = false;
                    Singleton<CMatchingSystem>.GetInstance().bInMatchingTeam = false;
                    Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.instance.GetText("Union_Battle_Tips4"), false);
                }
                else if (msg.stPkgData.stMatchRsp.stMatchResDetail.stMatchErr.iErrCode == 4)
                {
                    Singleton<CUIManager>.GetInstance().CloseSendMsgAlert();
                    float dwLeftSec = msg.stPkgData.stMatchRsp.stMatchResDetail.stMatchErr.stErrParam.stBePunished.dwLeftSec;
                    MatchPunishmentWaiting(dwLeftSec);
                }
                else
                {
                    Singleton<CUIManager>.GetInstance().CloseSendMsgAlert();
                    Singleton<CMatchingSystem>.GetInstance().bInMatching = false;
                    Singleton<CMatchingSystem>.GetInstance().bInMatchingTeam = false;
                    object[] replaceArr = new object[] { Utility.ProtErrCodeToStr(0x7db, msg.stPkgData.stMatchRsp.stMatchResDetail.stMatchErr.iErrCode) };
                    Singleton<CUIManager>.GetInstance().OpenTips("PVP_Matching_Errpr", true, 1f, null, replaceArr);
                    CloseInMatchingForm();
                }
            }
        }

        [MessageHandler(0x7ea)]
        public static void OnTeamChange(CSPkg msg)
        {
            Singleton<CUIManager>.GetInstance().CloseSendMsgAlert();
            TeamInfo teamInfo = Singleton<CMatchingSystem>.GetInstance().teamInfo;
            bool flag = false;
            if (msg.stPkgData.stTeamChgNtf.stChgDt.iChgType == 0)
            {
                TeamMember teamMemberInfo = Singleton<CMatchingSystem>.GetInstance().GetTeamMemberInfo(msg.stPkgData.stTeamChgNtf.stChgDt.stChgInfo.stPlayerAdd.stMemInfo);
                teamInfo.MemInfoList.Add(teamMemberInfo);
                flag = true;
            }
            else if (msg.stPkgData.stTeamChgNtf.stChgDt.iChgType != 1)
            {
                if (msg.stPkgData.stTeamChgNtf.stChgDt.iChgType == 2)
                {
                    teamInfo.stTeamMaster.ullUid = msg.stPkgData.stTeamChgNtf.stChgDt.stChgInfo.stMasterChg.stNewMaster.ullUid;
                    teamInfo.stTeamMaster.iGameEntity = msg.stPkgData.stTeamChgNtf.stChgDt.stChgInfo.stMasterChg.stNewMaster.iGameEntity;
                    flag = true;
                }
            }
            else
            {
                COMDT_TEAM_MEMBER_UNIQ stLevelMember = msg.stPkgData.stTeamChgNtf.stChgDt.stChgInfo.stPlayerLeave.stLevelMember;
                for (int i = 0; i < teamInfo.MemInfoList.Count; i++)
                {
                    if ((teamInfo.MemInfoList[i].uID.ullUid == stLevelMember.ullUid) && (teamInfo.MemInfoList[i].uID.iGameEntity == stLevelMember.iGameEntity))
                    {
                        teamInfo.MemInfoList.RemoveAt(i);
                        break;
                    }
                }
                flag = true;
            }
            if (flag)
            {
                CUIFormScript form = Singleton<CUIManager>.GetInstance().GetForm(PATH_MATCHING_MULTI);
                if (form != null)
                {
                    CLevelCfgLogicManager.SetTeamData(form.gameObject, teamInfo);
                }
            }
        }

        private void OpenGuideAdvancePage(CUIEvent uiEvent)
        {
            CUICommonSystem.OpenUrl("http://pvp.qq.com/ingame/all/video_stage.shtml?partition=" + MonoSingleton<TdirMgr>.GetInstance().SelectedTdir.logicWorldID, true);
        }

        private static void OpenInMatchingForm()
        {
            uint dwConfValue = GameDataMgr.globalInfoDatabin.GetDataByKey((uint) 0x6a).dwConfValue;
            uint result = 0;
            bool flag = uint.TryParse(Singleton<CTextManager>.instance.GetText("MapID_Robot_3V3Team_1"), out result);
            CRoleInfo masterRoleInfo = Singleton<CRoleInfoManager>.GetInstance().GetMasterRoleInfo();
            if (((masterRoleInfo != null) && (masterRoleInfo.PvpLevel < dwConfValue)) && (flag && (result == Singleton<CMatchingSystem>.instance.cacheMathingInfo.mapId)))
            {
                Singleton<CMatchingSystem>.instance.bAutoRobotMatch = true;
                Singleton<CMatchingSystem>.instance.AutoSingleMatchTipInterval = (int) GameDataMgr.globalInfoDatabin.GetDataByKey((uint) 0x69).dwConfValue;
                StartAutoSingleMatchTimer();
            }
            Singleton<CUIManager>.GetInstance().OpenForm(PATH_MATCHING_INMATCHING, false, true);
        }

        public void OpenPvPEntry(enPvPEntryFormWidget enOpenEntry)
        {
            CUIFormScript form = Singleton<CUIManager>.GetInstance().GetForm(PATH_MATCHING_ENTRY);
            if (form == null)
            {
                form = Singleton<CUIManager>.GetInstance().OpenForm(PATH_MATCHING_ENTRY, false, true);
                CMatchingView.InitMatchingEntry(form);
                this.OnBtnGroupClose(null);
            }
            if (enOpenEntry != enPvPEntryFormWidget.None)
            {
                CUICommonSystem.SetObjActive(form.GetWidget((int) enOpenEntry), true);
                CUICommonSystem.SetObjActive(form.GetWidget(2), false);
            }
            if (enOpenEntry == enPvPEntryFormWidget.GuideBtnGroup)
            {
                this.ShowAwards(form);
                form.GetWidget(10).CustomSetActive(false);
            }
            if (enOpenEntry == enPvPEntryFormWidget.PlayerBattleBtnGroupPanel)
            {
                GameObject gameObject = form.transform.FindChild("panelGroup2/btnGroup/Button3").gameObject;
                this.EntertainMentAddLock(gameObject);
            }
            else if (enOpenEntry == enPvPEntryFormWidget.ComputerBattleBtnGroupPanel)
            {
                GameObject btnObj = form.transform.FindChild("panelGroup3/btnGroup/Button4").gameObject;
                this.EntertainMentAddLock(btnObj);
            }
            this.ShowBonusImage(form);
        }

        private CUseable QueryLevelAwardItem(int levelId)
        {
            ResLevelCfgInfo dataByKey = GameDataMgr.levelDatabin.GetDataByKey(levelId);
            if (dataByKey == null)
            {
                return null;
            }
            uint key = dataByKey.SettleIDDetail[0];
            ResCommonSettle settle = GameDataMgr.settleDatabin.GetDataByKey(key);
            if (settle == null)
            {
                return null;
            }
            uint dwRewardID = settle.astFirstCompleteReward[0].dwRewardID;
            ResRandomRewardStore inRewardInfo = GameDataMgr.randowmRewardDB.GetDataByKey(dwRewardID);
            if (inRewardInfo == null)
            {
                return null;
            }
            return CUseableManager.GetUseableByRewardInfo(inRewardInfo);
        }

        public static void ReqCreateTeam(uint MapId, bool bPkAI, COM_BATTLE_MAP_TYPE mapType, COM_AI_LEVEL npcAILevel = 2)
        {
            DebugHelper.Assert(MapId != 0, "MapId Should not be 0!!!");
            if (MapId > 0)
            {
                Singleton<CMatchingSystem>.GetInstance().cacheMathingInfo.AILevel = npcAILevel;
                CSPkg msg = NetworkModule.CreateDefaultCSPKG(0x7e6);
                Singleton<CMatchingSystem>.GetInstance().mapId = MapId;
                Singleton<CMatchingSystem>.GetInstance().mapType = mapType;
                msg.stPkgData.stCreateTeamReq.stBaseInfo.bGameMode = 1;
                msg.stPkgData.stCreateTeamReq.stBaseInfo.dwMapId = MapId;
                msg.stPkgData.stCreateTeamReq.stBaseInfo.bMapType = (byte) mapType;
                msg.stPkgData.stCreateTeamReq.stBaseInfo.bPkAI = Convert.ToByte(!bPkAI ? COM_GAMEPK_TYPE.COM_GAMEPK_WITHOUT_AI : COM_GAMEPK_TYPE.COM_GAMEPK_WITH_AI);
                msg.stPkgData.stCreateTeamReq.stBaseInfo.bAILevel = Convert.ToByte(npcAILevel);
                Singleton<NetworkModule>.GetInstance().SendLobbyMsg(ref msg, true);
            }
        }

        public static void ReqKickPlayer(PlayerUniqueID uid)
        {
            CSPkg msg = NetworkModule.CreateDefaultCSPKG(0x7ec);
            msg.stPkgData.stOperTeamReq.stOper.iOperType = 2;
            msg.stPkgData.stOperTeamReq.stOper.stOperDetail.construct(2L);
            msg.stPkgData.stOperTeamReq.stOper.stOperDetail.stKickOutTeamMember.ullUid = uid.ullUid;
            msg.stPkgData.stOperTeamReq.stOper.stOperDetail.stKickOutTeamMember.iGameEntity = uid.iGameEntity;
            Singleton<NetworkModule>.GetInstance().SendLobbyMsg(ref msg, true);
        }

        public static void ReqLeaveMatching(bool bManual)
        {
            if (Singleton<CMatchingSystem>.GetInstance().bInMatching)
            {
                CSPkg msg = NetworkModule.CreateDefaultCSPKG(0x3ff);
                msg.stPkgData.stQuitMultGameReq.bManualQuit = !bManual ? ((byte) 0) : ((byte) 1);
                Singleton<NetworkModule>.GetInstance().SendLobbyMsg(ref msg, false);
            }
        }

        public static void ReqLeaveTeam()
        {
            CSPkg msg = NetworkModule.CreateDefaultCSPKG(0x7eb);
            Singleton<NetworkModule>.GetInstance().SendLobbyMsg(ref msg, false);
        }

        public static void ReqStartMultiMatching()
        {
            CSPkg msg = NetworkModule.CreateDefaultCSPKG(0x7ec);
            msg.stPkgData.stOperTeamReq.stOper.iOperType = 1;
            Singleton<NetworkModule>.GetInstance().SendLobbyMsg(ref msg, true);
        }

        public static void ReqStartSingleMatching(uint MapId, bool bPkAI, COM_BATTLE_MAP_TYPE mapType = 1)
        {
            Singleton<CMatchingSystem>.GetInstance().mapId = MapId;
            Singleton<CMatchingSystem>.GetInstance().mapType = mapType;
            Singleton<CMatchingSystem>.GetInstance().cacheMathingInfo.AILevel = COM_AI_LEVEL.COM_AI_LEVEL_MIDDLE_OF_NORMALBATTLE;
            CSPkg msg = NetworkModule.CreateDefaultCSPKG(0x7da);
            msg.stPkgData.stMatchReq.bMapType = Convert.ToByte(mapType);
            msg.stPkgData.stMatchReq.dwMapId = MapId;
            msg.stPkgData.stMatchReq.bIsPkAI = Convert.ToByte(!bPkAI ? COM_GAMEPK_TYPE.COM_GAMEPK_WITHOUT_AI : COM_GAMEPK_TYPE.COM_GAMEPK_WITH_AI);
            msg.stPkgData.stMatchReq.bGameMode = 1;
            msg.stPkgData.stMatchReq.bAILevel = Convert.ToByte(COM_AI_LEVEL.COM_AI_LEVEL_MIDDLE_OF_NORMALBATTLE);
            Singleton<NetworkModule>.GetInstance().SendLobbyMsg(ref msg, true);
        }

        public static void ReqStartTrainingLevel()
        {
            int dwConfValue = (int) GameDataMgr.globalInfoDatabin.GetDataByKey((uint) 120).dwConfValue;
            LobbyLogic.ReqStartGuideLevelSelHero(true, dwConfValue);
        }

        public static void SendMatchingConfirm()
        {
            CSPkg msg = NetworkModule.CreateDefaultCSPKG(0x7ef);
            Singleton<NetworkModule>.GetInstance().SendLobbyMsg(ref msg, false);
        }

        private static void SetItemCell(CUIFormScript formScript, GameObject itemCell, CUseable itemUseable)
        {
            Image component = itemCell.transform.Find("imgIcon").GetComponent<Image>();
            Text target = itemCell.transform.Find("lblIconCount").GetComponent<Text>();
            CUIUtility.SetImageSprite(component, itemUseable.GetIconPath(), formScript, true, false, false);
            target.text = itemUseable.m_stackCount.ToString();
            CUICommonSystem.AppendMultipleText(target, itemUseable.m_stackMulti);
            if (itemUseable.m_stackCount <= 0)
            {
                target.gameObject.CustomSetActive(false);
            }
        }

        private void SetPKMode(CUIFormScript form, bool bPkAI)
        {
            this.bPkAI = bPkAI;
            CMatchingView.SetPkMode(form, bPkAI);
        }

        private void ShowAwards(CUIFormScript form)
        {
            if (form != null)
            {
                List<int> list = new List<int> {
                    (int) GameDataMgr.globalInfoDatabin.GetDataByKey((uint) 0x77).dwConfValue,
                    (int) GameDataMgr.globalInfoDatabin.GetDataByKey((uint) 0x79).dwConfValue,
                    (int) GameDataMgr.globalInfoDatabin.GetDataByKey((uint) 0x74).dwConfValue,
                    (int) GameDataMgr.globalInfoDatabin.GetDataByKey((uint) 0x75).dwConfValue,
                    (int) GameDataMgr.globalInfoDatabin.GetDataByKey((uint) 0x76).dwConfValue
                };
                CRoleInfo masterRoleInfo = Singleton<CRoleInfoManager>.instance.GetMasterRoleInfo();
                List<int> list2 = new List<int> { 0x53, 0x54, 0, 0x55, 0x62 };
                int count = list.Count;
                for (int i = 0; i < count; i++)
                {
                    int levelId = list[i];
                    CUseable item = this.QueryLevelAwardItem(levelId);
                    string name = string.Format("panelGroup4/itemCell{0}", i + 2);
                    string str2 = string.Format("panelGroup4/Complete{0}", i + 2);
                    GameObject gameObject = form.transform.FindChild(name).gameObject;
                    GameObject itemComplete = form.transform.FindChild(str2).gameObject;
                    bool bFin = masterRoleInfo.IsGuidedStateSet(list2[i]);
                    this.ShowAwardTip(item, gameObject, form, bFin, itemComplete);
                    Transform transform = form.GetWidget(9).transform.FindChild(string.Format("btnGroup/Button{0}", i + 2));
                    if (this.IsTraningLevelLocked(masterRoleInfo, list2[i]))
                    {
                        transform.gameObject.GetComponent<Image>().color = CUIUtility.s_Color_GrayShader;
                        transform.FindChild("Lock").gameObject.CustomSetActive(true);
                    }
                    else
                    {
                        transform.gameObject.GetComponent<Image>().color = CUIUtility.s_Color_White;
                        transform.FindChild("Lock").gameObject.CustomSetActive(false);
                    }
                }
            }
        }

        private void ShowAwardTip(CUseable item, GameObject itemCell, CUIFormScript form, bool bFin, GameObject itemComplete)
        {
            if ((form != null) && (itemCell != null))
            {
                if (item != null)
                {
                    if (bFin)
                    {
                        itemCell.CustomSetActive(false);
                        itemComplete.CustomSetActive(true);
                    }
                    else
                    {
                        itemCell.CustomSetActive(true);
                        SetItemCell(form, itemCell, item);
                        itemComplete.CustomSetActive(false);
                    }
                }
                else
                {
                    itemCell.CustomSetActive(false);
                    itemComplete.CustomSetActive(bFin);
                }
            }
        }

        public void ShowBonusImage(CUIFormScript form)
        {
            if (form != null)
            {
                CRoleInfo masterRoleInfo = Singleton<CRoleInfoManager>.instance.GetMasterRoleInfo();
                GameObject gameObject = form.transform.FindChild("panelGroup5/ButtonTrain/ImageBonus").gameObject;
                if ((masterRoleInfo != null) && masterRoleInfo.IsTrainingLevelFin())
                {
                    gameObject.CustomSetActive(false);
                }
                else
                {
                    gameObject.CustomSetActive(true);
                }
            }
        }

        private static void StartAutoSingleMatchTimer()
        {
            EndAutoSingleMatchTimer();
            Singleton<CMatchingSystem>.instance.AutoSingleMatchTimeSeq = Singleton<CTimerManager>.instance.AddTimer(Singleton<CMatchingSystem>.instance.AutoSingleMatchTipInterval, 1, new CTimer.OnTimeUpHandler(CMatchingSystem.OnAutoSingleMatchTimer), true);
        }

        public override void UnInit()
        {
            base.UnInit();
            this.teamInfo = null;
            if (this.mapList != null)
            {
                this.mapList.Clear();
                this.mapList = null;
            }
        }

        public int currentMapPlayerNum { get; private set; }

        public bool IsInMatching
        {
            get
            {
                return this.bInMatching;
            }
        }

        public bool IsInMatchingTeam
        {
            get
            {
                return this.bInMatchingTeam;
            }
        }

        public bool IsSelfTeamMaster
        {
            get
            {
                return ((this.teamInfo.stTeamMaster.ullUid == this.teamInfo.stSelfInfo.ullUid) && (this.teamInfo.stTeamMaster.iGameEntity == this.teamInfo.stSelfInfo.iGameEntity));
            }
        }
    }
}

