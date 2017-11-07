﻿namespace Assets.Scripts.GameLogic
{
    using Assets.Scripts.Framework;
    using Assets.Scripts.GameSystem;
    using ResData;
    using System;
    using System.Runtime.CompilerServices;

    public class WinLoseByStarSys : Singleton<WinLoseByStarSys>
    {
        public IStarEvaluation LoserEvaluation;
        public IStarEvaluation WinnerEvaluation;

        public event OnEvaluationChangedDelegate OnEvaluationChanged;

        public event OnEvaluationChangedDelegate OnFailureEvaluationChanged;

        public void Clear()
        {
            if (this.WinnerEvaluation != null)
            {
                this.WinnerEvaluation.Dispose();
                this.WinnerEvaluation = null;
            }
            if (this.LoserEvaluation != null)
            {
                this.LoserEvaluation.Dispose();
                this.LoserEvaluation = null;
            }
            Singleton<GameEventSys>.instance.RmvEventHandler<DefaultGameEventParam>(GameEventDef.Event_PostActorDead, new RefAction<DefaultGameEventParam>(this.OnActorDeath));
            Singleton<GameEventSys>.instance.RmvEventHandler<SCampScoreUpdateParam>(GameEventDef.Event_CampScoreUpdated, new RefAction<SCampScoreUpdateParam>(this.OnCampScoreUpdated));
            this.CurLevelTimeDuration = 0;
            this.bStarted = false;
        }

        private IStarEvaluation CreateStar(ResEvaluateStarInfo ConditionDetail)
        {
            StarEvaluation evaluation = new StarEvaluation();
            evaluation.OnChanged += new OnEvaluationChangedDelegate(this.OnEvaluationChangedInner);
            evaluation.Index = 0;
            evaluation.Initialize(ConditionDetail);
            return evaluation;
        }

        private void OnActorDeath(ref DefaultGameEventParam prm)
        {
            if (this.WinnerEvaluation != null)
            {
                this.WinnerEvaluation.OnActorDeath(ref prm);
            }
            if (this.LoserEvaluation != null)
            {
                this.LoserEvaluation.OnActorDeath(ref prm);
            }
        }

        private void OnCampScoreUpdated(ref SCampScoreUpdateParam prm)
        {
            if (this.WinnerEvaluation != null)
            {
                this.WinnerEvaluation.OnCampScoreUpdated(ref prm);
            }
            if (this.LoserEvaluation != null)
            {
                this.LoserEvaluation.OnCampScoreUpdated(ref prm);
            }
        }

        private void OnEvaluationChangedInner(IStarEvaluation InStarEvaluation, IStarCondition InStarCondition)
        {
            if (InStarEvaluation == this.WinnerEvaluation)
            {
                if (this.OnEvaluationChanged != null)
                {
                    this.OnEvaluationChanged(InStarEvaluation, InStarCondition);
                }
            }
            else if ((InStarEvaluation == this.LoserEvaluation) && (this.OnFailureEvaluationChanged != null))
            {
                this.OnFailureEvaluationChanged(InStarEvaluation, InStarCondition);
            }
        }

        public bool Reset(int LevelID, bool bMultiGame)
        {
            this.Clear();
            ResDT_LevelCommonInfo outLevelComInfo = null;
            if (bMultiGame)
            {
                outLevelComInfo = CLevelCfgLogicManager.FindLevelConfigMultiGame(LevelID);
            }
            else
            {
                ResLevelCfgInfo outLevelCfg = null;
                CLevelCfgLogicManager.FindLevelConfigSingleGame(LevelID, out outLevelCfg, out outLevelComInfo);
            }
            bool flag = false;
            if (outLevelComInfo != null)
            {
                this.CurLevelTimeDuration = outLevelComInfo.dwTimeDuration;
                if (outLevelComInfo.dwAddWinCondStarId != 0)
                {
                    ResEvaluateStarInfo dataByKey = GameDataMgr.addWinLoseCondDatabin.GetDataByKey(outLevelComInfo.dwAddWinCondStarId);
                    DebugHelper.Assert(dataByKey != null);
                    if (dataByKey != null)
                    {
                        this.WinnerEvaluation = this.CreateStar(dataByKey);
                        DebugHelper.Assert(this.WinnerEvaluation != null, "我擦，怎会没有？");
                        flag = true;
                    }
                }
                if (outLevelComInfo.dwAddLoseCondStarId != 0)
                {
                    ResEvaluateStarInfo conditionDetail = GameDataMgr.addWinLoseCondDatabin.GetDataByKey(outLevelComInfo.dwAddLoseCondStarId);
                    DebugHelper.Assert(conditionDetail != null);
                    if (conditionDetail != null)
                    {
                        this.LoserEvaluation = this.CreateStar(conditionDetail);
                        DebugHelper.Assert(this.LoserEvaluation != null, "我擦，怎会没有？");
                        flag = true;
                    }
                }
            }
            return flag;
        }

        public void Start()
        {
            if (this.WinnerEvaluation != null)
            {
                this.WinnerEvaluation.Start();
            }
            if (this.LoserEvaluation != null)
            {
                this.LoserEvaluation.Start();
            }
            Singleton<GameEventSys>.instance.AddEventHandler<DefaultGameEventParam>(GameEventDef.Event_PostActorDead, new RefAction<DefaultGameEventParam>(this.OnActorDeath));
            Singleton<GameEventSys>.instance.AddEventHandler<SCampScoreUpdateParam>(GameEventDef.Event_CampScoreUpdated, new RefAction<SCampScoreUpdateParam>(this.OnCampScoreUpdated));
            this.bStarted = true;
        }

        public bool bStarted { get; private set; }

        public uint CurLevelTimeDuration { get; private set; }

        public bool isFailure
        {
            get
            {
                return ((this.LoserEvaluation != null) && (this.LoserEvaluation.status == StarEvaluationStatus.Success));
            }
        }

        public bool isSuccess
        {
            get
            {
                return ((this.WinnerEvaluation != null) && (this.WinnerEvaluation.status == StarEvaluationStatus.Success));
            }
        }
    }
}

