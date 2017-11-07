﻿namespace AGE
{
    using Assets.Scripts.Common;
    using Assets.Scripts.Framework;
    using Assets.Scripts.GameLogic;
    using ResData;
    using System;
    using System.Collections.Generic;

    public class HitTriggerDurationContext
    {
        private SceneManagement.Process _coordHandler;
        private PoolObjHandle<ActorRoot> _coordInActor = new PoolObjHandle<ActorRoot>();
        private VCollisionShape _coordShape = null;
        private PoolObjHandle<ActorRoot> attackActor = new PoolObjHandle<ActorRoot>();
        public int attackerId = 0;
        public bool bEdgeCheck = false;
        public bool bExtraBuff = false;
        public bool bFileterMonter = false;
        public bool bFileterOrgan = false;
        public bool bFilterDead = true;
        public bool bFilterEnemy = false;
        public bool bFilterFriend = true;
        public bool bFilterHero = false;
        public bool bFilterMyself = true;
        private bool bFirstProcess = true;
        private bool bHitTargetHero = false;
        public bool bTriggerBullet = false;
        public string BulletActionName = null;
        public bool bUseTriggerObj = true;
        private Dictionary<uint, int> collideCountMap = new Dictionary<uint, int>();
        private List<PoolObjHandle<ActorRoot>> collidedActors = new List<PoolObjHandle<ActorRoot>>();
        public int CollideMaxCount = -1;
        private Dictionary<uint, int> collideTimeMap = new Dictionary<uint, int>();
        private int deltaTime = 0;
        public bool hit = false;
        private VInt3 HitTargetHeroPos = VInt3.zero;
        private int lastTime = 0;
        private int localTime = 0;
        private int residueActorCount = 0;
        public int SelfSkillCombineID_1 = 0;
        public int SelfSkillCombineID_2 = 0;
        public int SelfSkillCombineID_3 = 0;
        public int TargetSkillCombine_1 = 0;
        public int TargetSkillCombine_2 = 0;
        public int TargetSkillCombine_3 = 0;
        private PoolObjHandle<ActorRoot> triggerActor = new PoolObjHandle<ActorRoot>();
        public int TriggerActorCount = -1;
        public int TriggerActorInterval = 30;
        private List<PoolObjHandle<ActorRoot>> triggerHeroList = new List<PoolObjHandle<ActorRoot>>();
        public int triggerId = 0;
        public int triggerInterval = 30;
        private List<PoolObjHandle<ActorRoot>> triggerMonsterList = new List<PoolObjHandle<ActorRoot>>();
        private List<PoolObjHandle<ActorRoot>> triggerOrganList = new List<PoolObjHandle<ActorRoot>>();
        private List<PoolObjHandle<ActorRoot>> triggerPriority = new List<PoolObjHandle<ActorRoot>>();
        private List<PoolObjHandle<ActorRoot>>[] type_actorList = null;
        private bool[] type_Filters = null;

        public HitTriggerDurationContext()
        {
            this._coordHandler = new SceneManagement.Process(this.FilterCoordActor);
        }

        public void CopyData(ref HitTriggerDurationContext r)
        {
            this.triggerHeroList.Clear();
            this.triggerMonsterList.Clear();
            this.triggerOrganList.Clear();
            this.triggerPriority.Clear();
            this.collidedActors.Clear();
            this.hit = r.hit;
            this.residueActorCount = r.residueActorCount;
            this.collideCountMap.Clear();
            this.collideTimeMap.Clear();
            this.attackActor = r.attackActor;
            this.triggerActor = r.triggerActor;
            this.lastTime = r.lastTime;
            this.localTime = r.localTime;
            this.deltaTime = r.deltaTime;
            this.bFirstProcess = r.bFirstProcess;
            this.type_Filters = null;
            this.type_actorList = null;
            this.bUseTriggerObj = r.bUseTriggerObj;
        }

        private void CopyTargetList(List<PoolObjHandle<ActorRoot>> _srcList, List<PoolObjHandle<ActorRoot>> _destList, int _count)
        {
            for (int i = 0; i < _count; i++)
            {
                _destList.Add(_srcList[i]);
            }
        }

        public void Enter(Action _action, Track _track)
        {
            this.hit = false;
            this.collideCountMap.Clear();
            this.collideTimeMap.Clear();
            this.type_Filters = new bool[] { this.bFilterHero, this.bFileterMonter, this.bFileterOrgan };
            this.type_actorList = new List<PoolObjHandle<ActorRoot>>[] { this.triggerHeroList, this.triggerMonsterList, this.triggerOrganList };
            this.triggerActor = _action.GetActorHandle(this.triggerId);
            if (this.bUseTriggerObj)
            {
                if (this.triggerActor == 0)
                {
                    return;
                }
                if (AGE_Helper.GetCollisionShape(this.triggerActor.handle) == null)
                {
                    return;
                }
            }
            this.attackActor = _action.GetActorHandle(this.attackerId);
        }

        private void FilterCoordActor(ref PoolObjHandle<ActorRoot> actorPtr)
        {
            ActorRoot handle = actorPtr.handle;
            if (((handle.shape != null) && !this.TargetObjTypeFilter(ref this._coordInActor, handle)) && ((Intersects(handle, this._coordShape, this.bEdgeCheck) && !this.TargetCollideTimeFiler(handle)) && !this.TargetCollideCountFilter(handle)))
            {
                this.collidedActors.Add(actorPtr);
                this.type_actorList[(int) handle.TheActorMeta.ActorType].Add(actorPtr);
            }
        }

        public List<PoolObjHandle<ActorRoot>> GetCollidedActorList(Action _action, PoolObjHandle<ActorRoot> InActor, PoolObjHandle<ActorRoot> triggerActor)
        {
            VCollisionShape shape = null;
            if (triggerActor != 0)
            {
                shape = triggerActor.handle.shape;
            }
            this.triggerHeroList.Clear();
            this.triggerMonsterList.Clear();
            this.triggerOrganList.Clear();
            this.triggerPriority.Clear();
            this.collidedActors.Clear();
            if ((shape == null) && this.bUseTriggerObj)
            {
                return null;
            }
            if (this.bUseTriggerObj)
            {
                this._coordInActor = InActor;
                this._coordShape = shape;
                SceneManagement instance = Singleton<SceneManagement>.GetInstance();
                SceneManagement.Coordinate coord = new SceneManagement.Coordinate();
                instance.GetCoord(ref coord, shape);
                instance.UpdateDirtyNodes();
                instance.ForeachActors(coord, this._coordHandler);
                this._coordInActor.Release();
                this._coordShape = null;
            }
            else
            {
                List<PoolObjHandle<ActorRoot>> gameActors = Singleton<GameObjMgr>.instance.GameActors;
                int count = gameActors.Count;
                for (int i = 0; i < count; i++)
                {
                    PoolObjHandle<ActorRoot> item = gameActors[i];
                    if (item != 0)
                    {
                        ActorRoot handle = item.handle;
                        if ((!this.TargetObjTypeFilter(ref InActor, (ActorRoot) item) && !this.TargetCollideTimeFiler(handle)) && !this.TargetCollideCountFilter(handle))
                        {
                            this.collidedActors.Add(item);
                            this.type_actorList[(int) handle.TheActorMeta.ActorType].Add(item);
                        }
                    }
                }
            }
            return this.collidedActors;
        }

        private void HitTrigger(Action _action)
        {
            if (this.attackActor != 0)
            {
                this.GetCollidedActorList(_action, this.attackActor, this.triggerActor);
                if ((this.collidedActors != null) && (this.collidedActors.Count != 0))
                {
                    SkillChooseTargetEventParam prm = new SkillChooseTargetEventParam(this.attackActor, this.attackActor, this.collidedActors.Count);
                    Singleton<GameEventSys>.instance.SendEvent<SkillChooseTargetEventParam>(GameEventDef.Event_HitTrigger, ref prm);
                    if ((this.TriggerActorCount > 0) && (this.TriggerActorCount < this.collidedActors.Count))
                    {
                        this.PriorityTrigger(_action);
                    }
                    else
                    {
                        for (int i = 0; i < this.collidedActors.Count; i++)
                        {
                            PoolObjHandle<ActorRoot> target = this.collidedActors[i];
                            this.TriggerAction(_action, ref target);
                        }
                    }
                }
            }
        }

        private static bool Intersects(ActorRoot _actor, VCollisionShape _shape, bool bEdge)
        {
            if (bEdge)
            {
                return _actor.shape.EdgeIntersects(_shape);
            }
            return _actor.shape.Intersects(_shape);
        }

        public void OnUse()
        {
            this.triggerHeroList.Clear();
            this.triggerMonsterList.Clear();
            this.triggerOrganList.Clear();
            this.triggerPriority.Clear();
            this.collidedActors.Clear();
            this.hit = false;
            this.residueActorCount = 0;
            this.bEdgeCheck = false;
            this.collideCountMap.Clear();
            this.collideTimeMap.Clear();
            this.attackActor.Release();
            this.triggerActor.Release();
            this.lastTime = 0;
            this.localTime = 0;
            this.deltaTime = 0;
            this.bFirstProcess = true;
            this.type_Filters = null;
            this.type_actorList = null;
            this.bUseTriggerObj = true;
            this.bHitTargetHero = false;
            this.HitTargetHeroPos = VInt3.zero;
        }

        private bool PriorityFindTarget(List<PoolObjHandle<ActorRoot>> triggerList)
        {
            if (this.residueActorCount < triggerList.Count)
            {
                this.RandomFindTarget(triggerList, this.residueActorCount);
                return true;
            }
            this.CopyTargetList(triggerList, this.triggerPriority, triggerList.Count);
            this.residueActorCount -= triggerList.Count;
            return (this.residueActorCount == 0);
        }

        private void PriorityTrigger(Action _action)
        {
            this.triggerPriority.Clear();
            this.residueActorCount = this.TriggerActorCount;
            if (!this.PriorityFindTarget(this.triggerHeroList) && !this.PriorityFindTarget(this.triggerMonsterList))
            {
                this.PriorityFindTarget(this.triggerOrganList);
            }
            for (int i = 0; i < this.triggerPriority.Count; i++)
            {
                PoolObjHandle<ActorRoot> target = this.triggerPriority[i];
                this.TriggerAction(_action, ref target);
            }
        }

        public void Process(Action _action, Track _track, int _localTime)
        {
            if (this.attackActor != 0)
            {
                this.hit = false;
                this.localTime = _localTime;
                if (this.bFirstProcess)
                {
                    this.bFirstProcess = false;
                    this.HitTrigger(_action);
                }
                else
                {
                    this.deltaTime += _localTime - this.lastTime;
                    if (this.deltaTime >= this.triggerInterval)
                    {
                        this.HitTrigger(_action);
                        this.deltaTime -= this.triggerInterval;
                    }
                }
                this.lastTime = _localTime;
                _action.refParams.SetRefParam("_HitTargetHero", this.bHitTargetHero);
                if (this.bHitTargetHero)
                {
                    _action.refParams.SetRefParam("_HitTargetHeroPos", this.HitTargetHeroPos);
                }
            }
        }

        private void RandomFindTarget(List<PoolObjHandle<ActorRoot>> _srcList, int _count)
        {
            ushort num = FrameRandom.Random((uint) _srcList.Count);
            for (int i = 0; i < _count; i++)
            {
                this.triggerPriority.Add(_srcList[num]);
                num = (ushort) (num + 1);
                num = (ushort) (num % ((ushort) _srcList.Count));
            }
        }

        public void Reset(BulletTriggerDuration InBulletTrigger)
        {
            this.triggerId = InBulletTrigger.triggerId;
            this.attackerId = InBulletTrigger.attackerId;
            this.triggerInterval = InBulletTrigger.triggerInterval;
            this.bFilterEnemy = InBulletTrigger.bFilterEnemy;
            this.bFilterFriend = InBulletTrigger.bFilterFriend;
            this.bFilterHero = InBulletTrigger.bFilterHero;
            this.bFileterMonter = InBulletTrigger.bFileterMonter;
            this.bFileterOrgan = InBulletTrigger.bFileterOrgan;
            this.bFilterDead = InBulletTrigger.bFilterDead;
            this.bFilterMyself = InBulletTrigger.bFilterMyself;
            this.TriggerActorCount = InBulletTrigger.TriggerActorCount;
            this.TriggerActorInterval = InBulletTrigger.TriggerActorInterval;
            this.CollideMaxCount = InBulletTrigger.CollideMaxCount;
            this.bEdgeCheck = InBulletTrigger.bEdgeCheck;
            this.bExtraBuff = InBulletTrigger.bExtraBuff;
            this.SelfSkillCombineID_1 = InBulletTrigger.SelfSkillCombineID_1;
            this.SelfSkillCombineID_2 = InBulletTrigger.SelfSkillCombineID_2;
            this.SelfSkillCombineID_3 = InBulletTrigger.SelfSkillCombineID_3;
            this.TargetSkillCombine_1 = InBulletTrigger.TargetSkillCombine_1;
            this.TargetSkillCombine_2 = InBulletTrigger.TargetSkillCombine_2;
            this.TargetSkillCombine_3 = InBulletTrigger.TargetSkillCombine_3;
            this.bTriggerBullet = InBulletTrigger.bTriggerBullet;
            this.BulletActionName = InBulletTrigger.BulletActionName;
        }

        public void Reset(HitTriggerDuration InTriggerDuration)
        {
            this.triggerId = InTriggerDuration.triggerId;
            this.attackerId = InTriggerDuration.attackerId;
            this.triggerInterval = InTriggerDuration.triggerInterval;
            this.bFilterEnemy = InTriggerDuration.bFilterEnemy;
            this.bFilterFriend = InTriggerDuration.bFilterFriend;
            this.bFilterHero = InTriggerDuration.bFilterHero;
            this.bFileterMonter = InTriggerDuration.bFileterMonter;
            this.bFileterOrgan = InTriggerDuration.bFileterOrgan;
            this.bFilterDead = InTriggerDuration.bFilterDead;
            this.bFilterMyself = InTriggerDuration.bFilterMyself;
            this.TriggerActorCount = InTriggerDuration.TriggerActorCount;
            this.TriggerActorInterval = InTriggerDuration.TriggerActorInterval;
            this.CollideMaxCount = InTriggerDuration.CollideMaxCount;
            this.bEdgeCheck = InTriggerDuration.bEdgeCheck;
            this.bExtraBuff = InTriggerDuration.bExtraBuff;
            this.SelfSkillCombineID_1 = InTriggerDuration.SelfSkillCombineID_1;
            this.SelfSkillCombineID_2 = InTriggerDuration.SelfSkillCombineID_2;
            this.SelfSkillCombineID_3 = InTriggerDuration.SelfSkillCombineID_3;
            this.TargetSkillCombine_1 = InTriggerDuration.TargetSkillCombine_1;
            this.TargetSkillCombine_2 = InTriggerDuration.TargetSkillCombine_2;
            this.TargetSkillCombine_3 = InTriggerDuration.TargetSkillCombine_3;
            this.bTriggerBullet = InTriggerDuration.bTriggerBullet;
            this.BulletActionName = InTriggerDuration.BulletActionName;
            this.bUseTriggerObj = InTriggerDuration.bUseTriggerObj;
        }

        private bool TargetCollideCountFilter(ActorRoot actor)
        {
            int num;
            return (((this.CollideMaxCount > 0) && this.collideCountMap.TryGetValue(actor.ObjID, out num)) && (num >= this.CollideMaxCount));
        }

        private bool TargetCollideTimeFiler(ActorRoot actor)
        {
            int num = 0;
            uint objID = actor.ObjID;
            if (!this.collideTimeMap.TryGetValue(objID, out num))
            {
                return false;
            }
            if ((this.localTime - num) > this.TriggerActorInterval)
            {
                return false;
            }
            this.collideTimeMap[objID] = num;
            return true;
        }

        private bool TargetObjTypeFilter(ref PoolObjHandle<ActorRoot> InActor, ActorRoot actor)
        {
            if (((!actor.ActorControl.IsDeadState || !this.bFilterDead) && (((InActor == 0) || !actor.IsSelfCamp(InActor.handle)) || !this.bFilterFriend)) && (((((InActor == 0) || !actor.IsEnemyCamp(InActor.handle)) || (!this.bFilterEnemy && (this.bFilterEnemy || !actor.ObjLinker.Invincible))) && (((actor.TheActorMeta.ActorType <= ActorTypeDef.Actor_Type_Organ) && !this.type_Filters[(int) actor.TheActorMeta.ActorType]) && (((InActor == 0) || (actor.ObjID != InActor.handle.ObjID)) || !this.bFilterMyself))) && ((actor.TheActorMeta.ActorType != ActorTypeDef.Actor_Type_Organ) || actor.AttackOrderReady)))
            {
                return false;
            }
            return true;
        }

        private void TriggerAction(Action _action, ref PoolObjHandle<ActorRoot> target)
        {
            if (this.attackActor != 0)
            {
                int num;
                uint objID = target.handle.ObjID;
                if (this.collideCountMap.TryGetValue(objID, out num))
                {
                    num++;
                    this.collideCountMap[objID] = num;
                }
                else
                {
                    this.collideCountMap.Add(objID, 1);
                }
                int num3 = 0;
                if (this.collideTimeMap.TryGetValue(objID, out num3))
                {
                    this.collideTimeMap[objID] = this.localTime;
                }
                else
                {
                    this.collideTimeMap.Add(objID, this.localTime);
                }
                SkillUseContext refParamObject = _action.refParams.GetRefParamObject<SkillUseContext>("SkillContext");
                if ((refParamObject != null) && (_action.refParams.GetRefParamObject<BaseSkill>("SkillObj") != null))
                {
                    this.attackActor.handle.SkillControl.SpawnBuff(refParamObject.Originator, refParamObject, this.SelfSkillCombineID_1, false);
                    this.attackActor.handle.SkillControl.SpawnBuff(refParamObject.Originator, refParamObject, this.SelfSkillCombineID_2, false);
                    this.attackActor.handle.SkillControl.SpawnBuff(refParamObject.Originator, refParamObject, this.SelfSkillCombineID_3, false);
                    if (target != 0)
                    {
                        this.hit = true;
                        if ((target.handle.TheActorMeta.ActorType == ActorTypeDef.Actor_Type_Hero) && !this.bHitTargetHero)
                        {
                            this.bHitTargetHero = true;
                            this.HitTargetHeroPos = target.handle.location;
                        }
                        refParamObject.EffectDir = this.attackActor.handle.forward;
                        bool flag = false;
                        bool introduced6 = this.attackActor.handle.SkillControl.SpawnBuff(target, refParamObject, this.TargetSkillCombine_1, this.bExtraBuff);
                        flag = introduced6 | this.attackActor.handle.SkillControl.SpawnBuff(target, refParamObject, this.TargetSkillCombine_2, this.bExtraBuff);
                        if (flag | this.attackActor.handle.SkillControl.SpawnBuff(target, refParamObject, this.TargetSkillCombine_3, this.bExtraBuff))
                        {
                            target.handle.ActorControl.BeAttackHit(this.attackActor);
                        }
                    }
                    if ((this.bTriggerBullet && (this.BulletActionName != null)) && (this.BulletActionName.Length > 0))
                    {
                        refParamObject.AppointType = SkillRangeAppointType.Target;
                        refParamObject.TargetActor = target;
                        this.attackActor.handle.SkillControl.SpawnBullet(refParamObject, this.BulletActionName, false);
                    }
                }
            }
        }
    }
}

