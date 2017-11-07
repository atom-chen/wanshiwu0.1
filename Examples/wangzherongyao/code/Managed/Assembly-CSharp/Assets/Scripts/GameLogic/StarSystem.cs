﻿namespace Assets.Scripts.GameLogic
{
    using Assets.Scripts.Framework;
    using CSProtocol;
    using ResData;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class StarSystem : Singleton<StarSystem>
    {
        protected IStarEvaluation FailureEvaluation;
        protected ListView<IStarEvaluation> StarEvaluations = new ListView<IStarEvaluation>(3);

        public event OnEvaluationChangedDelegate OnEvaluationChanged;

        public event OnEvaluationChangedDelegate OnFailureEvaluationChanged;

        protected void AddStarEvaluation(int CondID)
        {
            ResEvaluateStarInfo dataByKey = GameDataMgr.evaluateCondInfoDatabin.GetDataByKey((uint) CondID);
            DebugHelper.Assert(dataByKey != null);
            if (dataByKey != null)
            {
                IStarEvaluation item = this.CreateStar(dataByKey, this.StarEvaluations.Count);
                this.StarEvaluations.Add(item);
            }
        }

        public void Clear()
        {
            ListView<IStarEvaluation>.Enumerator enumerator = this.StarEvaluations.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current != null)
                {
                    enumerator.Current.Dispose();
                }
            }
            this.StarEvaluations.Clear();
            if (this.FailureEvaluation != null)
            {
                this.FailureEvaluation.Dispose();
                this.FailureEvaluation = null;
            }
            Singleton<GameEventSys>.instance.RmvEventHandler<DefaultGameEventParam>(GameEventDef.Event_PostActorDead, new RefAction<DefaultGameEventParam>(this.OnActorDeath));
        }

        protected IStarEvaluation CreateStar(ResEvaluateStarInfo ConditionDetail, int InIndex)
        {
            StarEvaluation evaluation = new StarEvaluation();
            evaluation.OnChanged += new OnEvaluationChangedDelegate(this.OnEvaluationChangedInner);
            evaluation.Index = InIndex;
            evaluation.Initialize(ConditionDetail);
            return evaluation;
        }

        public static ResLevelCfgInfo FindLevelConfig(int LevelID)
        {
            if (Singleton<BattleLogic>.instance.GetCurLvelContext().GameType == COM_GAME_TYPE.COM_SINGLE_GAME_OF_ACTIVITY)
            {
                return GameDataMgr.activityLevelDatabin.GetDataByKey(LevelID);
            }
            if (Singleton<BattleLogic>.instance.GetCurLvelContext().GameType == COM_GAME_TYPE.COM_SINGLE_GAME_OF_BURNING)
            {
                return GameDataMgr.burnMap.GetDataByKey(LevelID);
            }
            if (Singleton<BattleLogic>.instance.GetCurLvelContext().GameType == COM_GAME_TYPE.COM_SINGLE_GAME_OF_ARENA)
            {
                return GameDataMgr.arenaLevelDatabin.GetDataByKey(LevelID);
            }
            return GameDataMgr.levelDatabin.GetDataByKey(LevelID);
        }

        public IEnumerator<IStarEvaluation> GetEnumerator()
        {
            return this.StarEvaluations.GetEnumerator();
        }

        public IStarEvaluation GetEvaluationAt(int Index)
        {
            if (((Index >= 0) && (this.StarEvaluations != null)) && (Index < this.StarEvaluations.Count))
            {
                return this.StarEvaluations[Index];
            }
            return null;
        }

        public byte GetStarBits()
        {
            byte num = 0;
            byte num2 = 1;
            for (int i = 0; i < this.StarEvaluations.Count; i++)
            {
                if ((this.StarEvaluations[i] != null) && (this.StarEvaluations[i].status == StarEvaluationStatus.Success))
                {
                    num = (byte) (num | num2);
                }
                num2 = (byte) (num2 << 1);
            }
            return num;
        }

        private void OnActorDeath(ref DefaultGameEventParam prm)
        {
            for (int i = this.StarEvaluations.Count - 1; (i >= 0) && (i < this.StarEvaluations.Count); i--)
            {
                IStarEvaluation evaluation = this.StarEvaluations[i];
                if (evaluation != null)
                {
                    evaluation.OnActorDeath(ref prm);
                }
            }
            if (this.FailureEvaluation != null)
            {
                this.FailureEvaluation.OnActorDeath(ref prm);
            }
        }

        private void OnEvaluationChangedInner(IStarEvaluation InStarEvaluation, IStarCondition InStarCondition)
        {
            if (InStarEvaluation == this.FailureEvaluation)
            {
                if (this.OnFailureEvaluationChanged != null)
                {
                    this.OnFailureEvaluationChanged(InStarEvaluation, InStarCondition);
                }
            }
            else if (this.OnEvaluationChanged != null)
            {
                this.OnEvaluationChanged(InStarEvaluation, InStarCondition);
            }
        }

        public bool Reset(int LevelID)
        {
            this.Clear();
            ResLevelCfgInfo info = FindLevelConfig(LevelID);
            DebugHelper.Assert(info != null);
            if (info == null)
            {
                return false;
            }
            for (int i = 0; i < info.astStarDetail.Length; i++)
            {
                ResDT_IntParamArrayNode node = info.astStarDetail[i];
                if (node.iParam == 0)
                {
                    break;
                }
                this.AddStarEvaluation(node.iParam);
            }
            if (info.iLoseCondition != 0)
            {
                ResEvaluateStarInfo dataByKey = GameDataMgr.evaluateCondInfoDatabin.GetDataByKey((uint) info.iLoseCondition);
                DebugHelper.Assert(dataByKey != null);
                if (dataByKey == null)
                {
                    return false;
                }
                this.FailureEvaluation = this.CreateStar(dataByKey, 0);
                DebugHelper.Assert(this.FailureEvaluation != null, "我擦，怎会没有？");
            }
            Singleton<EventRouter>.instance.BroadCastEvent(EventID.StarSystemInitialized);
            return true;
        }

        public void Start()
        {
            ListView<IStarEvaluation>.Enumerator enumerator = this.StarEvaluations.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current != null)
                {
                    enumerator.Current.Start();
                }
            }
            if (this.FailureEvaluation != null)
            {
                this.FailureEvaluation.Start();
            }
            Singleton<GameEventSys>.instance.AddEventHandler<DefaultGameEventParam>(GameEventDef.Event_PostActorDead, new RefAction<DefaultGameEventParam>(this.OnActorDeath));
        }

        public IStarEvaluation failureEvaluation
        {
            get
            {
                return this.FailureEvaluation;
            }
        }

        public bool isFailure
        {
            get
            {
                return ((this.FailureEvaluation != null) && (this.FailureEvaluation.status == StarEvaluationStatus.Success));
            }
        }

        public bool isFirstStarCompleted
        {
            get
            {
                return ((this.StarEvaluations.Count > 0) && this.StarEvaluations[0].isSuccess);
            }
        }

        public int starCount
        {
            get
            {
                int num = 0;
                IEnumerator<IStarEvaluation> enumerator = this.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    if (enumerator.Current.isSuccess)
                    {
                        num++;
                    }
                }
                return num;
            }
        }

        public IStarEvaluation winEvaluation
        {
            get
            {
                return ((this.StarEvaluations.Count <= 0) ? null : this.StarEvaluations[0]);
            }
        }
    }
}

