﻿namespace Assets.Scripts.GameLogic
{
    using Assets.Scripts.Common;
    using Assets.Scripts.GameLogic.DataCenter;
    using CSProtocol;
    using System;

    public class HeroKDA : KDAStat
    {
        public PoolObjHandle<ActorRoot> actorHero;
        public uint commonSkillID;
        public stEquipInfo[] Equips = new stEquipInfo[6];
        public int HeroId;
        public uint SkinId;
        public int SoulLevel = 1;
        public COMDT_SETTLE_TALENT_INFO[] TalentArr = new COMDT_SETTLE_TALENT_INFO[5];

        public void Initialize(PoolObjHandle<ActorRoot> actorRoot)
        {
            this.actorHero = actorRoot;
            this.HeroId = this.actorHero.handle.TheActorMeta.ConfigId;
            ActorServerData actorData = new ActorServerData();
            IGameActorDataProvider actorDataProvider = Singleton<ActorDataCenter>.instance.GetActorDataProvider(GameActorDataProviderType.ServerDataProvider);
            if (actorDataProvider.GetActorServerData(ref actorRoot.handle.TheActorMeta, ref actorData))
            {
                this.SkinId = actorData.SkinId;
            }
            this.commonSkillID = 0;
            actorDataProvider.GetActorServerCommonSkillData(ref this.actorHero.handle.TheActorMeta, out this.commonSkillID);
            for (int i = 0; i < 5; i++)
            {
                this.TalentArr[i] = new COMDT_SETTLE_TALENT_INFO();
            }
            base.m_numKill = 0;
            base.m_numAssist = 0;
            base.m_numDead = 0;
            base._totalCoin = 0;
        }

        public void OnActorBattleCoinChanged(PoolObjHandle<ActorRoot> actor, int changeValue, int currentValue, bool isIncome)
        {
            if (isIncome && object.ReferenceEquals(actor.handle, this.actorHero.handle))
            {
                uint num = (uint) (Singleton<FrameSynchr>.instance.LogicFrameTick / ((ulong) 0x3e8L));
                if (num >= 60)
                {
                    uint num2 = (num - 60) / 30;
                    if (base.uiLastRecordCoinIndex != num2)
                    {
                        base.coinInfos.Add(num2 - 1, (uint) base._totalCoin);
                        base.uiLastRecordCoinIndex = num2;
                    }
                }
                base._totalCoin += changeValue;
            }
        }

        public void OnActorBeChosen(ref SkillChooseTargetEventParam prm)
        {
            if ((prm.src != 0) && object.ReferenceEquals(prm.src.handle, this.actorHero.handle))
            {
                if ((prm.atker != 0) && prm.src.handle.IsEnemyCamp(prm.atker.handle))
                {
                    base.m_uiBeChosenAsAttackTargetNum++;
                }
                else
                {
                    base.m_uiBeChosenAsHealTargetNum++;
                }
            }
        }

        public void OnActorDamage(ref HurtEventResultInfo prm)
        {
            if (object.ReferenceEquals(prm.src.handle, this.actorHero.handle))
            {
                if (prm.hurtInfo.hurtType != HurtTypeDef.Therapic)
                {
                    base.m_uiBeAttackedNum++;
                    base.m_hurtTakenByEnemy += prm.hurtTotal;
                    if ((prm.atker != 0) && (prm.atker.handle.TheActorMeta.ActorType == ActorTypeDef.Actor_Type_Hero))
                    {
                        base.m_hurtTakenByHero += prm.hurtTotal;
                    }
                    base.m_BeHurtMax = (base.m_BeHurtMax >= prm.hurtTotal) ? base.m_BeHurtMax : prm.hurtTotal;
                    if (base.m_BeHurtMin == -1)
                    {
                        base.m_BeHurtMin = prm.hurtTotal;
                    }
                    else
                    {
                        base.m_BeHurtMin = (base.m_BeHurtMin <= prm.hurtTotal) ? base.m_BeHurtMin : prm.hurtTotal;
                    }
                }
                else
                {
                    base.m_uiHealNum++;
                    base.m_heal += prm.hurtTotal;
                    base.m_BeHealMax = (base.m_BeHealMax >= prm.hurtTotal) ? base.m_BeHealMax : prm.hurtTotal;
                    if (base.m_BeHealMin == -1)
                    {
                        base.m_BeHealMin = prm.hurtTotal;
                    }
                    else
                    {
                        base.m_BeHealMin = (base.m_BeHealMin <= prm.hurtTotal) ? base.m_BeHealMin : prm.hurtTotal;
                    }
                    if ((prm.atker != 0) && object.ReferenceEquals(prm.atker.handle, this.actorHero.handle))
                    {
                        base.m_iSelfHealNum++;
                        base.m_iSelfHealCount += prm.hurtTotal;
                        base.m_iSelfHealMax = (base.m_iSelfHealMax >= prm.hurtTotal) ? base.m_iSelfHealMax : prm.hurtTotal;
                        if (base.m_iSelfHealMin == -1)
                        {
                            base.m_iSelfHealMin = prm.hurtTotal;
                        }
                        else
                        {
                            base.m_iSelfHealMin = (base.m_iSelfHealMin <= prm.hurtTotal) ? base.m_iSelfHealMin : prm.hurtTotal;
                        }
                    }
                }
            }
            if ((prm.atker != 0) && object.ReferenceEquals(prm.atker.handle, this.actorHero.handle))
            {
                if (prm.hurtInfo.hurtType != HurtTypeDef.Therapic)
                {
                    if (prm.src.handle.TheActorMeta.ActorType == ActorTypeDef.Actor_Type_Hero)
                    {
                        base.m_hurtToHero += prm.hurtTotal;
                        base.m_uiHurtToHeroNum++;
                    }
                    base.m_hurtToEnemy += prm.hurtTotal;
                    base.m_uiHurtToEnemyNum++;
                    if ((prm.atker.handle.SkillControl.CurUseSkill != null) && (prm.atker.handle.SkillControl.CurUseSkill.SlotType == SkillSlotType.SLOT_SKILL_0))
                    {
                        base.m_Skill0HurtToEnemy += prm.hurtTotal;
                    }
                }
                this.StatisticSkillInfo(prm);
                this.StatisticActorInfo(prm);
            }
        }

        public void OnActorDead(ref DefaultGameEventParam prm)
        {
            if (object.ReferenceEquals(prm.src.handle, this.actorHero.handle))
            {
                base.recordDead(prm.src, prm.orignalAtker);
            }
            else if (prm.src.handle.TheActorMeta.ActorType == ActorTypeDef.Actor_Type_Hero)
            {
                HeroWrapper actorControl = prm.src.handle.ActorControl as HeroWrapper;
                PoolObjHandle<ActorRoot> killer = new PoolObjHandle<ActorRoot>();
                bool flag = false;
                if ((prm.orignalAtker != 0) && (prm.orignalAtker.handle.TheActorMeta.ActorType == ActorTypeDef.Actor_Type_Hero))
                {
                    flag = true;
                    killer = prm.orignalAtker;
                }
                else if (actorControl.IsKilledByHero())
                {
                    flag = true;
                    killer = actorControl.LastHeroAtker;
                }
                if (flag)
                {
                    if (object.ReferenceEquals(killer.handle, this.actorHero.handle))
                    {
                        base.m_numKill++;
                    }
                    base.recordAssist(prm.src, prm.orignalAtker, this.actorHero, killer);
                }
            }
            else if ((prm.orignalAtker != 0) && object.ReferenceEquals(prm.orignalAtker.handle, this.actorHero.handle))
            {
                if (prm.src.handle.TheActorMeta.ActorType == ActorTypeDef.Actor_Type_Monster)
                {
                    if (prm.src.handle.ActorControl.GetActorSubType() == 2)
                    {
                        base.m_numKillMonster++;
                        MonsterWrapper wrapper2 = prm.src.handle.AsMonster();
                        if ((wrapper2 != null) && (wrapper2.cfgInfo != null))
                        {
                            if (wrapper2.cfgInfo.bSoldierType == 7)
                            {
                                base.m_numKillDragon++;
                            }
                            else if (wrapper2.cfgInfo.bSoldierType == 9)
                            {
                                base.m_numKillDragon++;
                            }
                            else if (wrapper2.cfgInfo.bSoldierType == 8)
                            {
                                base.m_numKillBaron++;
                            }
                        }
                    }
                    else if (prm.src.handle.ActorControl.GetActorSubType() == 1)
                    {
                        base.m_numKillSoldier++;
                    }
                    if (prm.src.handle.TheActorMeta.ConfigId != prm.src.handle.TheActorMeta.EnCId)
                    {
                        base.m_numKillFakeMonster++;
                    }
                }
                else if (prm.src.handle.TheActorMeta.ActorType == ActorTypeDef.Actor_Type_Organ)
                {
                    if ((prm.src.handle.TheStaticData.TheOrganOnlyInfo.OrganType == 1) || (prm.src.handle.TheStaticData.TheOrganOnlyInfo.OrganType == 4))
                    {
                        base.m_numKillOrgan++;
                    }
                    else if (prm.src.handle.TheStaticData.TheOrganOnlyInfo.OrganType == 2)
                    {
                        base.m_numDestroyBase++;
                    }
                }
            }
        }

        public void OnActorDoubleKill(ref DefaultGameEventParam prm)
        {
            if ((prm.atker != 0) && object.ReferenceEquals(prm.atker.handle, this.actorHero.handle))
            {
                base.m_DoubleKillNum++;
            }
        }

        public void OnActorOdyssey(ref DefaultGameEventParam prm)
        {
            if ((prm.atker != 0) && object.ReferenceEquals(prm.atker.handle, this.actorHero.handle))
            {
                base.m_LegendaryNum++;
                if (base.m_LegendaryNum > 1)
                {
                    base.m_LegendaryNum = 1;
                }
            }
        }

        public void OnActorPentaKill(ref DefaultGameEventParam prm)
        {
            if ((prm.atker != 0) && object.ReferenceEquals(prm.atker.handle, this.actorHero.handle))
            {
                base.m_PentaKillNum++;
            }
        }

        public void OnActorQuataryKill(ref DefaultGameEventParam prm)
        {
            if ((prm.atker != 0) && object.ReferenceEquals(prm.atker.handle, this.actorHero.handle))
            {
                base.m_QuataryKillNum++;
            }
        }

        public void OnActorTripleKill(ref DefaultGameEventParam prm)
        {
            if ((prm.atker != 0) && object.ReferenceEquals(prm.atker.handle, this.actorHero.handle))
            {
                base.m_TripleKillNum++;
            }
        }

        public void OnHitTrigger(ref SkillChooseTargetEventParam prm)
        {
            if (prm.atker.handle.SkillControl.CurUseSkillSlot != null)
            {
                SkillSlot curUseSkillSlot = prm.atker.handle.SkillControl.CurUseSkillSlot;
                curUseSkillSlot.SkillStatistictInfo.iHitCount = prm.iTargetCount;
                curUseSkillSlot.SkillStatistictInfo.iTmpHitAllHurtCountIndex = 0;
                curUseSkillSlot.SkillStatistictInfo.iTmpHitAllHurtTotal = 0;
                curUseSkillSlot.SkillStatistictInfo.iHitCountMax = Math.Max(curUseSkillSlot.SkillStatistictInfo.iHitCountMax, prm.iTargetCount);
                if (curUseSkillSlot.SkillStatistictInfo.iHitCountMin == -1)
                {
                    curUseSkillSlot.SkillStatistictInfo.iHitCountMin = prm.iTargetCount;
                }
                else
                {
                    curUseSkillSlot.SkillStatistictInfo.iHitCountMin = Math.Min(curUseSkillSlot.SkillStatistictInfo.iHitCountMin, prm.iTargetCount);
                }
            }
        }

        public void StatisticActorInfo(HurtEventResultInfo prm)
        {
            ActorValueStatistic objValueStatistic = this.actorHero.handle.ValueComponent.ObjValueStatistic;
            if (objValueStatistic != null)
            {
                objValueStatistic.iActorLvl = Math.Max(objValueStatistic.iActorLvl, prm.hurtInfo.attackInfo.iActorLvl);
                objValueStatistic.iActorATT = Math.Max(objValueStatistic.iActorATT, prm.hurtInfo.attackInfo.iActorATT);
                objValueStatistic.iActorINT = Math.Max(objValueStatistic.iActorINT, prm.hurtInfo.attackInfo.iActorINT);
                objValueStatistic.iActorMaxHp = Math.Max(objValueStatistic.iActorMaxHp, prm.hurtInfo.attackInfo.iActorMaxHp);
                objValueStatistic.iActorMinHp = Math.Max(objValueStatistic.iActorMinHp, prm.src.handle.ValueComponent.actorHp);
                objValueStatistic.iDEFStrike = Math.Max(objValueStatistic.iDEFStrike, prm.hurtInfo.attackInfo.iDEFStrike);
                objValueStatistic.iRESStrike = Math.Max(objValueStatistic.iRESStrike, prm.hurtInfo.attackInfo.iRESStrike);
                objValueStatistic.iFinalHurt = Math.Max(objValueStatistic.iFinalHurt, prm.hurtInfo.attackInfo.iFinalHurt);
                objValueStatistic.iCritStrikeRate = Math.Max(objValueStatistic.iCritStrikeRate, prm.hurtInfo.attackInfo.iCritStrikeRate);
                objValueStatistic.iCritStrikeValue = Math.Max(objValueStatistic.iCritStrikeValue, prm.hurtInfo.attackInfo.iCritStrikeValue);
                objValueStatistic.iReduceCritStrikeRate = Math.Max(objValueStatistic.iReduceCritStrikeRate, prm.hurtInfo.attackInfo.iReduceCritStrikeRate);
                objValueStatistic.iReduceCritStrikeValue = Math.Max(objValueStatistic.iReduceCritStrikeValue, prm.hurtInfo.attackInfo.iReduceCritStrikeValue);
                objValueStatistic.iCritStrikeEff = Math.Max(objValueStatistic.iCritStrikeEff, prm.hurtInfo.attackInfo.iCritStrikeEff);
                objValueStatistic.iPhysicsHemophagiaRate = Math.Max(objValueStatistic.iPhysicsHemophagiaRate, prm.hurtInfo.attackInfo.iPhysicsHemophagiaRate);
                objValueStatistic.iMagicHemophagiaRate = Math.Max(objValueStatistic.iMagicHemophagiaRate, prm.hurtInfo.attackInfo.iMagicHemophagiaRate);
                objValueStatistic.iPhysicsHemophagia = Math.Max(objValueStatistic.iPhysicsHemophagia, prm.hurtInfo.attackInfo.iPhysicsHemophagia);
                objValueStatistic.iMagicHemophagia = Math.Max(objValueStatistic.iMagicHemophagia, prm.hurtInfo.attackInfo.iMagicHemophagia);
                objValueStatistic.iHurtOutputRate = Math.Max(objValueStatistic.iHurtOutputRate, prm.hurtInfo.attackInfo.iHurtOutputRate);
            }
        }

        public void StatisticSkillInfo(HurtEventResultInfo prm)
        {
            if (prm.hurtInfo.atkSlot < SkillSlotType.SLOT_SKILL_COUNT)
            {
                SkillSlot slot = this.actorHero.handle.SkillControl.SkillSlotArray[(int) prm.hurtInfo.atkSlot];
                if ((slot != null) && (prm.hurtInfo.hurtType != HurtTypeDef.Therapic))
                {
                    slot.SkillStatistictInfo.iHurtTotal += prm.hurtTotal;
                    if ((prm.src != 0) && (prm.src.handle.TheActorMeta.ActorType == ActorTypeDef.Actor_Type_Hero))
                    {
                        slot.SkillStatistictInfo.iHurtToHeroTotal += prm.hurtTotal;
                    }
                }
                if (((slot != null) && (slot.SkillObj != null)) && (slot.SkillObj.cfgData.iCfgID == slot.SkillStatistictInfo.iSkillCfgID))
                {
                    if (prm.hurtInfo.hurtType != HurtTypeDef.Therapic)
                    {
                        slot.SkillStatistictInfo.iHurtMax = Math.Max(slot.SkillStatistictInfo.iHurtMax, prm.hurtTotal);
                        if (slot.SkillStatistictInfo.iHurtMin == -1)
                        {
                            slot.SkillStatistictInfo.iHurtMin = prm.hurtTotal;
                        }
                        else
                        {
                            slot.SkillStatistictInfo.iHurtMin = Math.Min(slot.SkillStatistictInfo.iHurtMin, prm.hurtTotal);
                        }
                        if (slot.SkillStatistictInfo.iTmpHitAllHurtCountIndex++ < slot.SkillStatistictInfo.iHitCount)
                        {
                            slot.SkillStatistictInfo.iTmpHitAllHurtTotal += prm.hurtTotal;
                        }
                        if (slot.SkillStatistictInfo.iTmpHitAllHurtCountIndex == slot.SkillStatistictInfo.iHitCount)
                        {
                            slot.SkillStatistictInfo.iHitAllHurtTotalMax = Math.Max(slot.SkillStatistictInfo.iHitAllHurtTotalMax, slot.SkillStatistictInfo.iTmpHitAllHurtTotal);
                            if (slot.SkillStatistictInfo.iHitAllHurtTotalMin == -1)
                            {
                                slot.SkillStatistictInfo.iHitAllHurtTotalMin = slot.SkillStatistictInfo.iTmpHitAllHurtTotal;
                            }
                            else
                            {
                                slot.SkillStatistictInfo.iHitAllHurtTotalMin = Math.Min(slot.SkillStatistictInfo.iHitAllHurtTotalMin, slot.SkillStatistictInfo.iTmpHitAllHurtTotal);
                            }
                        }
                    }
                    slot.SkillStatistictInfo.iadValue = Math.Max(slot.SkillStatistictInfo.iadValue, prm.hurtInfo.adValue);
                    slot.SkillStatistictInfo.iapValue = Math.Max(slot.SkillStatistictInfo.iapValue, prm.hurtInfo.apValue);
                    slot.SkillStatistictInfo.ihemoFadeRate = Math.Max(slot.SkillStatistictInfo.ihemoFadeRate, prm.hurtInfo.hemoFadeRate);
                    slot.SkillStatistictInfo.ihpValue = Math.Max(slot.SkillStatistictInfo.ihpValue, prm.hurtInfo.hpValue);
                    slot.SkillStatistictInfo.ihurtCount = Math.Max(slot.SkillStatistictInfo.ihurtCount, prm.hurtInfo.hurtCount);
                    slot.SkillStatistictInfo.ihurtValue = Math.Max(slot.SkillStatistictInfo.ihurtValue, prm.hurtInfo.hurtValue);
                    slot.SkillStatistictInfo.iloseHpValue = Math.Max(slot.SkillStatistictInfo.iloseHpValue, prm.hurtInfo.loseHpValue);
                }
            }
        }

        public void unInit()
        {
            this.HeroId = 0;
            this.SoulLevel = 1;
        }
    }
}

