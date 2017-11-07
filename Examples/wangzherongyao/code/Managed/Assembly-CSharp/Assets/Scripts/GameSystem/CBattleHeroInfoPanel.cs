﻿namespace Assets.Scripts.GameSystem
{
    using Assets.Scripts.Common;
    using Assets.Scripts.Framework;
    using Assets.Scripts.GameLogic;
    using Assets.Scripts.UI;
    using ResData;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class CBattleHeroInfoPanel : Singleton<CBattleHeroInfoPanel>
    {
        public CUIFormScript m_FormScript;
        private static string s_battleHeroInfoForm = "UGUI/Form/Battle/Form_Battle_HeroInfoV8.prefab";
        private static string s_propPanel = "Panel_Prop";
        public static readonly string valForm1 = "<color=#60bd67>{0}</color>({1}+<color=#60bd67>{2}</color>)";
        public static readonly string valForm2 = "<color=#60bd67>{0}</color>";

        private static string GetFormPercentStr(int percent, bool isExtra)
        {
            if (isExtra)
            {
                return string.Format(valForm2, CUICommonSystem.GetValuePercent(percent));
            }
            return CUICommonSystem.GetValuePercent(percent);
        }

        private static string GetFormStr(float baseValue, float growValue)
        {
            if (growValue > 0f)
            {
                return string.Format(valForm1, baseValue + growValue, baseValue, growValue);
            }
            return baseValue.ToString();
        }

        public void Hide()
        {
            Singleton<CUIManager>.GetInstance().CloseForm(s_battleHeroInfoForm);
            this.m_FormScript = null;
            Singleton<CUIParticleSystem>.instance.Show(null);
        }

        public override void Init()
        {
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.BattleHeroInfo_InfoTypeChange, new CUIEventManager.OnUIEventHandler(this.OnBttleHeroInfoTabChange));
        }

        public bool IsFormOpened()
        {
            return (this.m_FormScript != null);
        }

        protected void OnBttleHeroInfoTabChange(CUIEvent uiEvent)
        {
            CUIListScript component = uiEvent.m_srcWidget.GetComponent<CUIListScript>();
            if (component != null)
            {
                int selectedIndex = component.GetSelectedIndex();
                if (this.m_FormScript != null)
                {
                    if (selectedIndex == 0)
                    {
                        this.m_FormScript.m_formWidgets[2].CustomSetActive(true);
                        this.m_FormScript.m_formWidgets[1].CustomSetActive(false);
                    }
                    else
                    {
                        this.m_FormScript.m_formWidgets[2].CustomSetActive(false);
                        this.m_FormScript.m_formWidgets[1].CustomSetActive(true);
                    }
                }
            }
        }

        public void OnSkillTipsShow()
        {
            if (this.m_FormScript != null)
            {
                GameObject gameObject = this.m_FormScript.transform.Find("SkillTipsBg").gameObject;
                if (!gameObject.activeSelf)
                {
                    gameObject.CustomSetActive(true);
                }
                SkillSlotType[] typeArray1 = new SkillSlotType[4];
                typeArray1[1] = SkillSlotType.SLOT_SKILL_1;
                typeArray1[2] = SkillSlotType.SLOT_SKILL_2;
                typeArray1[3] = SkillSlotType.SLOT_SKILL_3;
                SkillSlotType[] typeArray = typeArray1;
                GameObject[] objArray = new GameObject[typeArray.Length];
                objArray[0] = gameObject.transform.Find("Panel0").gameObject;
                objArray[1] = gameObject.transform.Find("Panel1").gameObject;
                objArray[2] = gameObject.transform.Find("Panel2").gameObject;
                objArray[3] = gameObject.transform.Find("Panel3").gameObject;
                Skill skillObj = null;
                if (Singleton<GamePlayerCenter>.instance.GetHostPlayer() != null)
                {
                    PoolObjHandle<ActorRoot> captain = Singleton<GamePlayerCenter>.instance.GetHostPlayer().Captain;
                    if (captain != 0)
                    {
                        IHeroData data = CHeroDataFactory.CreateHeroData((uint) captain.handle.TheActorMeta.ConfigId);
                        SkillSlot[] skillSlotArray = captain.handle.SkillControl.SkillSlotArray;
                        for (int i = 0; i < typeArray.Length; i++)
                        {
                            SkillSlot slot = skillSlotArray[(int) typeArray[i]];
                            objArray[i].CustomSetActive(true);
                            if (slot != null)
                            {
                                skillObj = slot.SkillObj;
                            }
                            else if ((i < (typeArray.Length - 1)) && (i > 0))
                            {
                                skillObj = new Skill((captain.handle.TheActorMeta.ConfigId * 100) + (i * 10));
                            }
                            else
                            {
                                skillObj = null;
                            }
                            if (skillObj != null)
                            {
                                Image component = objArray[i].transform.Find("SkillMask/SkillImg").GetComponent<Image>();
                                if ((component != null) && !string.IsNullOrEmpty(skillObj.IconName))
                                {
                                    component.SetSprite(CUIUtility.s_Sprite_Dynamic_Skill_Dir + skillObj.IconName, Singleton<CUIManager>.GetInstance().GetForm(s_battleHeroInfoForm), true, false, false);
                                }
                                Text text = objArray[i].transform.Find("Text_Tittle").GetComponent<Text>();
                                if ((text != null) && (skillObj.cfgData.szSkillName.Length > 0))
                                {
                                    text.text = StringHelper.UTF8BytesToString(ref skillObj.cfgData.szSkillName);
                                }
                                Text text2 = objArray[i].transform.Find("Text_CD").GetComponent<Text>();
                                int skillCDMax = 0;
                                if (slot != null)
                                {
                                    skillCDMax = slot.GetSkillCDMax();
                                }
                                text2.text = (i != 0) ? Singleton<CTextManager>.instance.GetText("Skill_Cool_Down_Tips", new string[1]) : Singleton<CTextManager>.instance.GetText("Skill_Common_Effect_Type_5");
                                if ((i < (typeArray.Length - 1)) && (i > 0))
                                {
                                    Text text3 = objArray[i].transform.Find("Text_EnergyCost").GetComponent<Text>();
                                    if (slot == null)
                                    {
                                        string[] args = new string[] { skillObj.cfgData.iEnergyCost.ToString() };
                                        text3.text = Singleton<CTextManager>.instance.GetText("Skill_Energy_Cost_Tips", args);
                                    }
                                    else
                                    {
                                        string[] textArray3 = new string[] { slot.NextSkillEnergyCostTotal().ToString() };
                                        text3.text = Singleton<CTextManager>.instance.GetText("Skill_Energy_Cost_Tips", textArray3);
                                    }
                                }
                                uint[] skillEffectType = skillObj.cfgData.SkillEffectType;
                                GameObject obj3 = null;
                                for (int j = 1; j <= 2; j++)
                                {
                                    obj3 = objArray[i].transform.Find(string.Format("EffectNode{0}", j)).gameObject;
                                    if ((j <= skillEffectType.Length) && (skillEffectType[j - 1] != 0))
                                    {
                                        obj3.CustomSetActive(true);
                                        obj3.GetComponent<Image>().SetSprite(CSkillData.GetEffectSlotBg((SkillEffectType) skillEffectType[j - 1]), this.m_FormScript, true, false, false);
                                        obj3.transform.Find("Text").GetComponent<Text>().text = CSkillData.GetEffectDesc((SkillEffectType) skillEffectType[j - 1]);
                                    }
                                    else
                                    {
                                        obj3.CustomSetActive(false);
                                    }
                                }
                                Text text4 = objArray[i].transform.Find("Text_Detail").GetComponent<Text>();
                                ValueDataInfo[] actorValue = captain.handle.ValueComponent.mActorValue.GetActorValue();
                                if ((text4 != null) && (skillObj.cfgData.szSkillDesc.Length > 0))
                                {
                                    text4.text = CUICommonSystem.GetSkillDesc(skillObj.cfgData.szSkillDesc, actorValue, slot.GetSkillLevel());
                                }
                            }
                            else if (i == (typeArray.Length - 1))
                            {
                                Text text5 = objArray[i].transform.Find("Text_Detail").GetComponent<Text>();
                                if (Singleton<BattleLogic>.GetInstance().GetCurLvelContext().isPVPLevel)
                                {
                                    text5.text = Singleton<CTextManager>.GetInstance().GetText("Skill_Text_Lock_PVP");
                                }
                                else
                                {
                                    text5.text = Singleton<CTextManager>.GetInstance().GetText("Skill_Text_Lock_PVE");
                                }
                            }
                        }
                    }
                }
            }
        }

        private void RefreshHeroPropPanel(GameObject root, ref ValueDataInfo[] info, int level, uint heroId)
        {
            Transform transform = root.transform;
            Text component = transform.Find("TextL1").GetComponent<Text>();
            Text text2 = transform.Find("TextR1").GetComponent<Text>();
            Text text3 = transform.Find("TextL2").GetComponent<Text>();
            Text text4 = transform.Find("TextR2").GetComponent<Text>();
            Text text5 = transform.Find("TextL3").GetComponent<Text>();
            Text text6 = transform.Find("TextR3").GetComponent<Text>();
            Text text7 = transform.Find("TextL4").GetComponent<Text>();
            Text text8 = transform.Find("TextR4").GetComponent<Text>();
            Text text9 = transform.Find("TextL5").GetComponent<Text>();
            Text text10 = transform.Find("TextR5").GetComponent<Text>();
            Text text11 = transform.Find("TextL6").GetComponent<Text>();
            Text text12 = transform.Find("TextR6").GetComponent<Text>();
            Text text13 = transform.Find("TextL7").GetComponent<Text>();
            Text text14 = transform.Find("TextR7").GetComponent<Text>();
            Text text15 = transform.Find("TextL8").GetComponent<Text>();
            Text text16 = transform.Find("TextR8").GetComponent<Text>();
            Text text17 = transform.Find("TextL9").GetComponent<Text>();
            Text text18 = transform.Find("TextR9").GetComponent<Text>();
            Text text19 = transform.Find("TextL10").GetComponent<Text>();
            Text text20 = transform.Find("TextR10").GetComponent<Text>();
            Text text21 = transform.Find("TextL11").GetComponent<Text>();
            Text text22 = transform.Find("TextR11").GetComponent<Text>();
            Text text23 = transform.Find("TextL12").GetComponent<Text>();
            Text text24 = transform.Find("TextR12").GetComponent<Text>();
            Text text25 = transform.Find("TextL13").GetComponent<Text>();
            Text text26 = transform.Find("TextR13").GetComponent<Text>();
            Text text27 = transform.Find("TextL14").GetComponent<Text>();
            Text text28 = transform.Find("TextR14").GetComponent<Text>();
            Text text29 = transform.Find("TextL15").GetComponent<Text>();
            Text text30 = transform.Find("TextR15").GetComponent<Text>();
            Text text31 = transform.Find("TextL16").GetComponent<Text>();
            Text text32 = transform.Find("TextR16").GetComponent<Text>();
            Text text33 = transform.Find("TextL17").GetComponent<Text>();
            Text text34 = transform.Find("TextR17").GetComponent<Text>();
            Text text35 = transform.Find("TextL18").GetComponent<Text>();
            Text text36 = transform.Find("TextR18").GetComponent<Text>();
            int totalValue = 0;
            int percent = 0;
            ResBattleParam anyData = GameDataMgr.battleParam.GetAnyData();
            component.text = Singleton<CTextManager>.GetInstance().GetText("Hero_Prop_PhyAtkPt");
            text2.text = GetFormStr((float) info[1].basePropertyValue, (float) info[1].extraPropertyValue);
            text3.text = Singleton<CTextManager>.GetInstance().GetText("Hero_Prop_MgcAtkPt");
            text4.text = GetFormStr((float) info[2].basePropertyValue, (float) info[2].extraPropertyValue);
            text5.text = Singleton<CTextManager>.GetInstance().GetText("Hero_Prop_MaxHp");
            text6.text = GetFormStr((float) info[5].basePropertyValue, (float) info[5].extraPropertyValue);
            text7.text = Singleton<CTextManager>.GetInstance().GetText("Hero_Prop_MaxEp");
            text8.text = GetFormStr((float) info[0x20].basePropertyValue, (float) info[0x20].extraPropertyValue);
            totalValue = info[3].totalValue;
            percent = (int) ((totalValue * 0x2710) / ((totalValue + (level * anyData.dwM_PhysicsDefend)) + anyData.dwN_PhysicsDefend));
            text9.text = Singleton<CTextManager>.GetInstance().GetText("Hero_Prop_PhyDefPt");
            text10.text = string.Format("{0}|{1}", GetFormStr((float) info[3].basePropertyValue, (float) info[3].extraPropertyValue), GetFormPercentStr(percent, info[3].extraPropertyValue > 0));
            totalValue = info[4].totalValue;
            percent = (int) ((totalValue * 0x2710) / ((totalValue + (level * anyData.dwM_MagicDefend)) + anyData.dwN_MagicDefend));
            text11.text = Singleton<CTextManager>.GetInstance().GetText("Hero_Prop_MgcDefPt");
            text12.text = string.Format("{0}|{1}", GetFormStr((float) info[4].basePropertyValue, (float) info[4].extraPropertyValue), GetFormPercentStr(percent, info[4].extraPropertyValue > 0));
            totalValue = info[0x1c].totalValue;
            percent = ((int) ((0x2710 * totalValue) / ((totalValue + (level * anyData.dwM_AttackSpeed)) + anyData.dwN_AttackSpeed))) + info[0x12].totalValue;
            text13.text = Singleton<CTextManager>.GetInstance().GetText("Hero_Prop_AtkSpdLvl");
            text14.text = GetFormPercentStr(percent, info[0x12].extraPropertyValue > 0);
            percent = info[20].totalValue;
            text15.text = Singleton<CTextManager>.GetInstance().GetText("Hero_Prop_CdReduce");
            text16.text = GetFormPercentStr(percent, info[20].extraPropertyValue > 0);
            totalValue = info[0x18].totalValue;
            percent = ((int) ((0x2710 * totalValue) / ((totalValue + (level * anyData.dwM_Critical)) + anyData.dwN_Critical))) + info[6].totalValue;
            text17.text = Singleton<CTextManager>.GetInstance().GetText("Hero_Prop_CritLvl");
            text18.text = GetFormPercentStr(percent, info[6].extraPropertyValue > 0);
            text19.text = Singleton<CTextManager>.GetInstance().GetText("Hero_Prop_MoveSpd");
            text20.text = GetFormStr((float) (info[15].basePropertyValue / 10), (float) (info[15].extraPropertyValue / 10));
            text21.text = Singleton<CTextManager>.GetInstance().GetText("Hero_Prop_HpRecover");
            totalValue = info[0x10].totalValue;
            string str = string.Format(Singleton<CTextManager>.GetInstance().GetText("Hero_Prop_HpRecover_Desc"), totalValue);
            text22.text = GetFormStr((float) info[0x10].basePropertyValue, (float) info[0x10].extraPropertyValue);
            text23.text = Singleton<CTextManager>.GetInstance().GetText("Hero_Prop_EpRecover");
            totalValue = info[0x21].totalValue;
            string str2 = string.Format(Singleton<CTextManager>.GetInstance().GetText("Hero_Prop_EpRecover_Desc"), totalValue);
            text24.text = GetFormStr((float) info[0x21].basePropertyValue, (float) info[0x21].extraPropertyValue);
            text25.text = Singleton<CTextManager>.GetInstance().GetText("Hero_Prop_PhyArmorHurt");
            text26.text = string.Format("{0}|{1}", GetFormStr((float) info[7].baseValue, (float) info[7].extraPropertyValue), GetFormPercentStr(info[0x22].totalValue, info[0x22].extraPropertyValue > 0));
            text27.text = Singleton<CTextManager>.GetInstance().GetText("Hero_Prop_MgcArmorHurt");
            text28.text = string.Format("{0}|{1}", GetFormStr((float) info[8].baseValue, (float) info[8].extraPropertyValue), GetFormPercentStr(info[0x23].totalValue, info[0x23].extraPropertyValue > 0));
            totalValue = info[0x1a].totalValue;
            percent = ((int) ((0x2710 * totalValue) / ((totalValue + (level * anyData.dwM_PhysicsHemophagia)) + anyData.dwN_PhysicsHemophagia))) + info[9].totalValue;
            text29.text = Singleton<CTextManager>.GetInstance().GetText("Hero_Prop_PhyVampLvl");
            text30.text = GetFormPercentStr(percent, info[9].extraPropertyValue > 0);
            totalValue = info[0x1b].totalValue;
            percent = ((int) ((0x2710 * totalValue) / ((totalValue + (level * anyData.dwM_MagicHemophagia)) + anyData.dwN_MagicHemophagia))) + info[10].totalValue;
            text31.text = Singleton<CTextManager>.GetInstance().GetText("Hero_Prop_MgcVampLvl");
            text32.text = GetFormPercentStr(percent, info[10].extraPropertyValue > 0);
            ResHeroCfgInfo dataByKey = GameDataMgr.heroDatabin.GetDataByKey(heroId);
            if (dataByKey != null)
            {
                text33.text = Singleton<CTextManager>.GetInstance().GetText("Hero_Prop_BaseAtkRange");
                text34.text = Utility.UTF8Convert(dataByKey.szAttackRangeDesc);
            }
            else
            {
                text33.text = string.Empty;
                text34.text = string.Empty;
            }
            totalValue = info[0x1d].totalValue;
            percent = ((int) ((0x2710 * totalValue) / ((totalValue + (level * anyData.dwM_Tenacity)) + anyData.dwN_Tenacity))) + info[0x11].totalValue;
            text35.text = Singleton<CTextManager>.GetInstance().GetText("Hero_Prop_CtrlReduceLvl");
            text36.text = GetFormPercentStr(percent, info[0x11].extraPropertyValue > 0);
        }

        private void RefreshPropPanel(GameObject root, ref PoolObjHandle<ActorRoot> actor)
        {
            if ((actor != 0) && (root != null))
            {
                GameObject gameObject = root.transform.Find(s_propPanel).gameObject.transform.Find("Panel_HeroProp").gameObject;
                ActorRoot handle = actor.handle;
                ValueDataInfo[] actorValue = handle.ValueComponent.mActorValue.GetActorValue();
                int soulLevel = handle.ValueComponent.mActorValue.SoulLevel;
                if (Singleton<BattleLogic>.GetInstance().m_GameInfo.gameContext.IsBalanceProp())
                {
                }
                this.RefreshHeroPropPanel(gameObject, ref actorValue, soulLevel, (uint) actor.handle.TheActorMeta.ConfigId);
            }
        }

        public void Show()
        {
            this.m_FormScript = Singleton<CUIManager>.GetInstance().OpenForm(s_battleHeroInfoForm, false, true);
            if ((this.m_FormScript != null) && (Singleton<GamePlayerCenter>.GetInstance().GetHostPlayer() != null))
            {
                PoolObjHandle<ActorRoot> captain = Singleton<GamePlayerCenter>.GetInstance().GetHostPlayer().Captain;
                if (captain != 0)
                {
                    GameObject listObj = this.m_FormScript.m_formWidgets[3];
                    if (listObj == null)
                    {
                        return;
                    }
                    string[] titleList = new string[] { "属性", "技能" };
                    CUICommonSystem.InitMenuPanel(listObj, titleList, 0);
                    this.ShowHero(captain);
                    this.RefreshPropPanel(this.m_FormScript.gameObject, ref captain);
                    this.OnSkillTipsShow();
                    this.m_FormScript.m_formWidgets[2].CustomSetActive(true);
                    this.m_FormScript.m_formWidgets[1].CustomSetActive(false);
                }
                Singleton<CUIParticleSystem>.instance.Hide(null);
            }
        }

        private void ShowHero(PoolObjHandle<ActorRoot> actor)
        {
            ResHeroCfgInfo dataByKey = GameDataMgr.heroDatabin.GetDataByKey(actor.handle.TheActorMeta.ConfigId);
            GameObject obj2 = this.m_FormScript.m_formWidgets[4];
            if (obj2 != null)
            {
                string str = CUIUtility.s_Sprite_Dynamic_BustHero_Dir;
                string str2 = "30" + actor.handle.TheActorMeta.ConfigId.ToString() + "0";
                Image component = obj2.transform.GetComponent<Image>();
                if (component != null)
                {
                    component.SetSprite(CUIUtility.s_Sprite_Dynamic_BustHero_Dir + str2, Singleton<CUIManager>.GetInstance().GetForm(s_battleHeroInfoForm), true, false, false);
                }
            }
            GameObject obj3 = this.m_FormScript.m_formWidgets[5];
            if (obj3 != null)
            {
                Text text = obj3.transform.GetComponent<Text>();
                if (text != null)
                {
                    text.text = StringHelper.UTF8BytesToString(ref dataByKey.szName);
                }
            }
            GameObject obj4 = this.m_FormScript.m_formWidgets[6];
            if (obj4 != null)
            {
                Text text2 = obj4.transform.GetComponent<Text>();
                if (text2 != null)
                {
                    text2.text = StringHelper.UTF8BytesToString(ref dataByKey.szHeroTips);
                }
            }
        }

        public void SwitchPanel()
        {
        }

        public override void UnInit()
        {
            Singleton<CUIEventManager>.GetInstance().RemoveUIEventListener(enUIEventID.BattleHeroInfo_InfoTypeChange, new CUIEventManager.OnUIEventHandler(this.OnBttleHeroInfoTabChange));
        }
    }
}

