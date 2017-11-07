﻿namespace Assets.Scripts.GameLogic.SkillFunc
{
    using Assets.Scripts.Common;
    using Assets.Scripts.GameLogic;
    using CSProtocol;
    using Pathfinding.RVO;
    using ResData;
    using System;
    using System.Collections.Generic;

    [SkillFuncHandlerClass]
    internal class SkillFuncSpecialDelegator
    {
        [SkillFuncHandler(0x2f, new int[] {  })]
        public static bool OnSkillFuncChangeHudStyle(ref SSkillFuncContext inContext)
        {
            PoolObjHandle<ActorRoot> inTargetObj = inContext.inTargetObj;
            if (inTargetObj != 0)
            {
                if (inContext.inStage == ESkillFuncStage.Enter)
                {
                    inTargetObj.handle.HudControl.bBossHpBar = true;
                }
                else if (inContext.inStage == ESkillFuncStage.Leave)
                {
                    inTargetObj.handle.HudControl.bBossHpBar = false;
                }
            }
            return true;
        }

        [SkillFuncHandler(0x37, new int[] {  })]
        public static bool OnSkillFuncChangeSkill(ref SSkillFuncContext inContext)
        {
            PoolObjHandle<ActorRoot> inTargetObj = inContext.inTargetObj;
            if (inTargetObj != 0)
            {
                int num = 0;
                int skillFuncParam = 0;
                BuffChangeSkillRule changeSkillRule = null;
                if (inContext.inStage == ESkillFuncStage.Enter)
                {
                    skillFuncParam = inContext.GetSkillFuncParam(0, false);
                    num = inContext.GetSkillFuncParam(1, false);
                    inContext.LocalParams[0].iParam = skillFuncParam;
                    if (inTargetObj.handle.BuffHolderComp != null)
                    {
                        changeSkillRule = inTargetObj.handle.BuffHolderComp.changeSkillRule;
                        if (changeSkillRule != null)
                        {
                            changeSkillRule.ChangeSkillSlot((SkillSlotType) skillFuncParam, num);
                        }
                    }
                }
                else if (inContext.inStage == ESkillFuncStage.Leave)
                {
                    skillFuncParam = inContext.LocalParams[0].iParam;
                    if (inTargetObj.handle.BuffHolderComp != null)
                    {
                        changeSkillRule = inTargetObj.handle.BuffHolderComp.changeSkillRule;
                        if (changeSkillRule != null)
                        {
                            changeSkillRule.RecoverSkillSlot((SkillSlotType) skillFuncParam);
                        }
                    }
                }
            }
            return true;
        }

        [SkillFuncHandler(0x10, new int[] {  })]
        public static bool OnSkillFuncChangeSkillCD(ref SSkillFuncContext inContext)
        {
            if (inContext.inStage == ESkillFuncStage.Enter)
            {
                int skillFuncParam = inContext.GetSkillFuncParam(0, false);
                int slotMask = inContext.GetSkillFuncParam(1, false);
                int num3 = inContext.GetSkillFuncParam(2, false);
                inContext.LocalParams[0].iParam = skillFuncParam;
                inContext.LocalParams[1].iParam = slotMask;
                inContext.LocalParams[2].iParam = num3;
                SkillFuncChangeSkillCDImpl(ref inContext, skillFuncParam, slotMask, num3);
            }
            else if (inContext.inStage == ESkillFuncStage.Leave)
            {
                int iParam = inContext.LocalParams[0].iParam;
                int num5 = inContext.LocalParams[1].iParam;
                int num6 = -inContext.LocalParams[2].iParam;
                if (iParam != 0)
                {
                    SkillFuncChangeSkillCDImpl(ref inContext, iParam, num5, num6);
                }
            }
            return true;
        }

        [SkillFuncHandler(0x38, new int[] {  })]
        public static bool OnSkillFuncDisableSkill(ref SSkillFuncContext inContext)
        {
            PoolObjHandle<ActorRoot> inTargetObj = inContext.inTargetObj;
            if (inTargetObj != 0)
            {
                if (inContext.inStage == ESkillFuncStage.Enter)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        if (inContext.GetSkillFuncParam(i, false) == 1)
                        {
                            inTargetObj.handle.ActorControl.AddDisableSkillFlag((SkillSlotType) i);
                        }
                    }
                }
                else if (inContext.inStage == ESkillFuncStage.Leave)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        if (inContext.GetSkillFuncParam(j, false) == 1)
                        {
                            inTargetObj.handle.ActorControl.RmvDisableSkillFlag((SkillSlotType) j);
                        }
                    }
                }
            }
            return true;
        }

        [SkillFuncHandler(0x2e, new int[] {  })]
        public static bool OnSkillFuncHpCondition(ref SSkillFuncContext inContext)
        {
            int num = 0;
            int skillFuncParam = inContext.GetSkillFuncParam(1, false);
            if ((skillFuncParam < 0) || (skillFuncParam >= 0x24))
            {
                return false;
            }
            RES_FUNCEFT_TYPE res_funceft_type = (RES_FUNCEFT_TYPE) skillFuncParam;
            PoolObjHandle<ActorRoot> inTargetObj = inContext.inTargetObj;
            if (inTargetObj == 0)
            {
                return false;
            }
            int num3 = inContext.GetSkillFuncParam(2, false);
            if (inContext.inStage != ESkillFuncStage.Leave)
            {
                int actorHp = inTargetObj.handle.ValueComponent.actorHp;
                int totalValue = inTargetObj.handle.ValueComponent.mActorValue[RES_FUNCEFT_TYPE.RES_FUNCEFT_MAXHP].totalValue;
                int num6 = 0x2710 - ((actorHp * 0x2710) / totalValue);
                int num7 = inContext.GetSkillFuncParam(3, false);
                int num8 = inContext.GetSkillFuncParam(0, false);
                if (num8 == 0)
                {
                    return false;
                }
                int num9 = (num6 * num7) / num8;
                int iParam = inContext.LocalParams[0].iParam;
                if (num3 == 1)
                {
                    num = (int) (inTargetObj.handle.ValueComponent.mActorValue[res_funceft_type] >> iParam);
                    num = (int) (inTargetObj.handle.ValueComponent.mActorValue[res_funceft_type] << num9);
                }
                else
                {
                    num = ((int) inTargetObj.handle.ValueComponent.mActorValue[res_funceft_type]) - iParam;
                    num = ((int) inTargetObj.handle.ValueComponent.mActorValue[res_funceft_type]) + num9;
                }
                inContext.LocalParams[0].iParam = num9;
            }
            else if (inContext.inStage == ESkillFuncStage.Leave)
            {
                int num11 = inContext.LocalParams[0].iParam;
                if (num3 == 1)
                {
                    num = (int) (inTargetObj.handle.ValueComponent.mActorValue[res_funceft_type] >> num11);
                }
                else
                {
                    num = ((int) inTargetObj.handle.ValueComponent.mActorValue[res_funceft_type]) - num11;
                }
            }
            return true;
        }

        [SkillFuncHandler(0x2d, new int[] {  })]
        public static bool OnSkillFuncIgnoreRVO(ref SSkillFuncContext inContext)
        {
            PoolObjHandle<ActorRoot> inTargetObj = inContext.inTargetObj;
            if (inTargetObj != 0)
            {
                RVOController component = null;
                if (inContext.inStage == ESkillFuncStage.Enter)
                {
                    component = inTargetObj.handle.gameObject.GetComponent<RVOController>();
                    if (component != null)
                    {
                        component.enabled = false;
                    }
                }
                else if (inContext.inStage == ESkillFuncStage.Leave)
                {
                    component = inTargetObj.handle.gameObject.GetComponent<RVOController>();
                    if (component != null)
                    {
                        component.enabled = true;
                    }
                }
            }
            return true;
        }

        [SkillFuncHandler(0x27, new int[] {  })]
        public static bool OnSkillFuncInvisible(ref SSkillFuncContext inContext)
        {
            PoolObjHandle<ActorRoot> inTargetObj = inContext.inTargetObj;
            if (inTargetObj != 0)
            {
                if (inContext.inStage == ESkillFuncStage.Enter)
                {
                    inTargetObj.handle.HorizonMarker.AddHideMark(COM_PLAYERCAMP.COM_PLAYERCAMP_COUNT, HorizonConfig.HideMark.Skill, 1);
                }
                else if (inContext.inStage == ESkillFuncStage.Leave)
                {
                    COM_PLAYERCAMP[] othersCmp = BattleLogic.GetOthersCmp(inTargetObj.handle.TheActorMeta.ActorCamp);
                    for (int i = 0; i < othersCmp.Length; i++)
                    {
                        if (inTargetObj.handle.HorizonMarker.HasHideMark(othersCmp[i], HorizonConfig.HideMark.Skill))
                        {
                            inTargetObj.handle.HorizonMarker.AddHideMark(othersCmp[i], HorizonConfig.HideMark.Skill, -1);
                        }
                    }
                }
            }
            return true;
        }

        [SkillFuncHandler(70, new int[] {  })]
        public static bool OnSkillFuncRemoveSkillBuff(ref SSkillFuncContext inContext)
        {
            if (inContext.inStage == ESkillFuncStage.Enter)
            {
                PoolObjHandle<ActorRoot> inTargetObj = inContext.inTargetObj;
                if (inTargetObj != 0)
                {
                    int skillFuncParam = inContext.GetSkillFuncParam(0, false);
                    inTargetObj.handle.BuffHolderComp.RemoveBuff(skillFuncParam);
                }
            }
            return true;
        }

        [SkillFuncHandler(0x20, new int[] {  })]
        public static bool OnSkillFuncReviveSoon(ref SSkillFuncContext inContext)
        {
            if (inContext.inTargetObj != 0)
            {
                if (inContext.inStage == ESkillFuncStage.Enter)
                {
                    int skillFuncParam = inContext.GetSkillFuncParam(0, false);
                    int num2 = inContext.GetSkillFuncParam(1, false);
                    int num3 = inContext.GetSkillFuncParam(2, false);
                    int num4 = inContext.GetSkillFuncParam(3, false);
                    int num5 = inContext.GetSkillFuncParam(4, false);
                    inContext.inBuffSkill.handle.CustomParams[0] = skillFuncParam;
                    inContext.inBuffSkill.handle.CustomParams[1] = num2;
                    inContext.inBuffSkill.handle.CustomParams[2] = num3;
                    inContext.inBuffSkill.handle.CustomParams[3] = num4;
                    inContext.inBuffSkill.handle.CustomParams[4] = num5;
                }
                else if (inContext.inStage == ESkillFuncStage.Leave)
                {
                }
            }
            return true;
        }

        [SkillFuncHandler(0x26, new int[] {  })]
        public static bool OnSkillFuncSightArea(ref SSkillFuncContext inContext)
        {
            if (inContext.inStage == ESkillFuncStage.Enter)
            {
                PoolObjHandle<ActorRoot> inTargetObj = inContext.inTargetObj;
                if (inTargetObj != 0)
                {
                    List<PoolObjHandle<ActorRoot>> heroActors = Singleton<GameObjMgr>.GetInstance().HeroActors;
                    for (int i = 0; i < heroActors.Count; i++)
                    {
                        PoolObjHandle<ActorRoot> handle3 = heroActors[i];
                        ActorRoot handle = handle3.handle;
                        if (inTargetObj.handle.TheActorMeta.ActorCamp != handle.TheActorMeta.ActorCamp)
                        {
                            handle.HorizonMarker.Enabled = false;
                        }
                    }
                }
            }
            else if (inContext.inStage == ESkillFuncStage.Leave)
            {
                PoolObjHandle<ActorRoot> handle2 = inContext.inTargetObj;
                if (handle2 != 0)
                {
                    List<PoolObjHandle<ActorRoot>> list2 = Singleton<GameObjMgr>.GetInstance().HeroActors;
                    for (int j = 0; j < list2.Count; j++)
                    {
                        PoolObjHandle<ActorRoot> handle4 = list2[j];
                        ActorRoot root2 = handle4.handle;
                        if (handle2.handle.TheActorMeta.ActorCamp != root2.TheActorMeta.ActorCamp)
                        {
                            root2.HorizonMarker.Enabled = true;
                        }
                    }
                }
            }
            return true;
        }

        private static void SkillFuncChangeSkillCDImpl(ref SSkillFuncContext inContext, int changeType, int slotMask, int value)
        {
            PoolObjHandle<ActorRoot> inTargetObj = inContext.inTargetObj;
            if (inTargetObj != 0)
            {
                SkillComponent skillControl = inTargetObj.handle.SkillControl;
                if (skillControl != null)
                {
                    SkillSlot slot = null;
                    for (int i = 0; i < 7; i++)
                    {
                        if (((((slotMask == 0) && (i != 0)) && ((i != 4) && (i != 5))) || ((slotMask & (((int) 1) << i)) > 0)) && (skillControl.TryGetSkillSlot((SkillSlotType) i, out slot) && (slot != null)))
                        {
                            if (changeType == 0)
                            {
                                slot.ChangeSkillCD(value);
                            }
                            else
                            {
                                slot.ChangeMaxCDRate(value);
                            }
                        }
                    }
                }
            }
        }
    }
}

