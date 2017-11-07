﻿namespace Assets.Scripts.GameLogic
{
    using Assets.Scripts.Common;
    using Assets.Scripts.Framework;
    using ResData;
    using System;
    using System.Runtime.CompilerServices;

    public class Skill : BaseSkill
    {
        public SkillRangeAppointType AppointType;
        private ResBattleParam battleParam;
        public bool bDelayAbortSkill;
        public bool bProtectAbortSkill;
        public ResSkillCfgInfo cfgData;
        public string EffectPrefabName;
        public string EffectWarnPrefabName;
        public string FixedPrefabName;
        public string FixedWarnPrefabName;
        public string GuidePrefabName;
        public string GuideWarnPrefabName;
        public string IconName;
        public SkillAbort skillAbort;
        public float SkillCD;
        public int SkillCost;

        public Skill(int id)
        {
            base.SkillID = id;
            this.skillAbort = new SkillAbort();
            this.skillAbort.InitAbort(false);
            this.bDelayAbortSkill = false;
            this.bProtectAbortSkill = false;
            this.cfgData = GameDataMgr.skillDatabin.GetDataByKey(id);
            if (this.cfgData != null)
            {
                base.ActionName = StringHelper.UTF8BytesToString(ref this.cfgData.szPrefab);
                object[] inParameters = new object[] { id };
                DebugHelper.Assert(base.ActionName != null, "Action name is null in skill databin id = {0}", inParameters);
                this.GuidePrefabName = StringHelper.UTF8BytesToString(ref this.cfgData.szGuidePrefab);
                this.GuideWarnPrefabName = StringHelper.UTF8BytesToString(ref this.cfgData.szGuideWarnPrefab);
                this.EffectPrefabName = StringHelper.UTF8BytesToString(ref this.cfgData.szEffectPrefab);
                this.EffectWarnPrefabName = StringHelper.UTF8BytesToString(ref this.cfgData.szEffectWarnPrefab);
                this.FixedPrefabName = StringHelper.UTF8BytesToString(ref this.cfgData.szFixedPrefab);
                this.FixedWarnPrefabName = StringHelper.UTF8BytesToString(ref this.cfgData.szFixedWarnPrefab);
                this.IconName = StringHelper.UTF8BytesToString(ref this.cfgData.szIconPath);
                this.SkillCD = 5f;
                this.AppointType = (SkillRangeAppointType) this.cfgData.dwRangeAppointType;
            }
            this.battleParam = GameDataMgr.battleParam.GetAnyData();
        }

        public bool canAbort(SkillAbortType _type)
        {
            return this.skillAbort.Abort(_type);
        }

        private void SetSkillSpeed(PoolObjHandle<ActorRoot> user)
        {
            int totalValue = 0;
            int num2 = 0;
            int num3 = 0;
            ValueDataInfo info = null;
            if (base.curAction != null)
            {
                info = user.handle.ValueComponent.mActorValue[RES_FUNCEFT_TYPE.RES_PROPERTY_ATTACKSPEED];
                totalValue = info.totalValue;
                num3 = (int) ((totalValue + (user.handle.ValueComponent.mActorValue.actorLvl * this.battleParam.dwM_AttackSpeed)) + this.battleParam.dwN_AttackSpeed);
                if (num3 != 0)
                {
                    num2 = (totalValue * 0x2710) / num3;
                }
                num2 += user.handle.ValueComponent.mActorValue[RES_FUNCEFT_TYPE.RES_FUNCEFT_ATKSPDADD].totalValue;
                VFactor factor = new VFactor((long) (0x2710 + num2), 0x2710L);
                base.curAction.SetPlaySpeed(factor);
            }
        }

        public override bool Use(PoolObjHandle<ActorRoot> user, SkillUseContext context)
        {
            if (context != null)
            {
                context.SetOriginator(user);
                context.Instigator = user;
            }
            if (!base.Use(user, context))
            {
                return false;
            }
            this.skillAbort.InitAbort(false);
            this.bDelayAbortSkill = false;
            this.bProtectAbortSkill = false;
            if ((context != null) && (context.SlotType == SkillSlotType.SLOT_SKILL_0))
            {
                this.SetSkillSpeed(user);
            }
            return true;
        }

        public SkillSlotType SlotType { get; set; }
    }
}

