﻿namespace Assets.Scripts.Framework
{
    using AGE;
    using Assets.Scripts.GameLogic;
    using Assets.Scripts.GameSystem;
    using System;
    using UnityEngine;

    [GameState]
    public class BattleState : BaseState
    {
        private BlendWeights m_originalBlendWeight;

        public override void OnStateEnter()
        {
            this.m_originalBlendWeight = QualitySettings.blendWeights;
            if (GameSettings.RenderQuality == SGameRenderQuality.Low)
            {
                QualitySettings.blendWeights = BlendWeights.OneBone;
            }
            else
            {
                QualitySettings.blendWeights = BlendWeights.TwoBones;
            }
            ActionManager.Instance.frameMode = true;
            SLevelContext curLvelContext = Singleton<BattleLogic>.instance.GetCurLvelContext();
            string eventName = ((curLvelContext == null) || string.IsNullOrEmpty(curLvelContext.musicStartEvent)) ? "PVP01_Play" : curLvelContext.musicStartEvent;
            Singleton<CSoundManager>.GetInstance().PostEvent(eventName, null);
            string str2 = (curLvelContext == null) ? string.Empty : curLvelContext.ambientSoundEvent;
            if (!string.IsNullOrEmpty(str2))
            {
                Singleton<CSoundManager>.instance.PostEvent(str2, null);
            }
            CUICommonSystem.OpenFps();
            Singleton<CUIParticleSystem>.GetInstance().Open();
            CResourceManager.isBattleState = true;
            switch (Singleton<CNewbieAchieveSys>.GetInstance().trackFlag)
            {
                case CNewbieAchieveSys.TrackFlag.SINGLE_COMBAT_3V3_ENTER:
                    MonoSingleton<NewbieGuideManager>.GetInstance().SetNewbieBit(10, true);
                    break;

                case CNewbieAchieveSys.TrackFlag.SINGLE_MATCH_3V3_ENTER:
                    MonoSingleton<NewbieGuideManager>.GetInstance().SetNewbieBit(11, true);
                    break;

                case CNewbieAchieveSys.TrackFlag.PVE_1_1_1_Enter:
                    MonoSingleton<NewbieGuideManager>.GetInstance().SetNewbieBit(13, true);
                    break;
            }
        }

        public override void OnStateLeave()
        {
            QualitySettings.blendWeights = this.m_originalBlendWeight;
            CResourceManager.isBattleState = false;
            ActionManager.Instance.frameMode = false;
            SLevelContext curLvelContext = Singleton<BattleLogic>.instance.GetCurLvelContext();
            string eventName = ((curLvelContext == null) || string.IsNullOrEmpty(curLvelContext.musicEndEvent)) ? "PVP01_Stop" : curLvelContext.musicEndEvent;
            Singleton<CSoundManager>.GetInstance().PostEvent(eventName, null);
            string[] exceptFormNames = new string[] { CSettleSystem.PATH_PVP_SETTLE_PVP, Singleton<SettlementSystem>.instance.SettlementFormName, PVESettleSys.PATH_LOSE };
            Singleton<CUIManager>.GetInstance().CloseAllForm(exceptFormNames, true, true);
            MonoSingleton<ShareSys>.instance.m_bShowTimeline = false;
            Singleton<CGameObjectPool>.GetInstance().ClearPooledObjects();
            enResourceType[] resourceTypes = new enResourceType[5];
            resourceTypes[1] = enResourceType.UI3DImage;
            resourceTypes[2] = enResourceType.UIForm;
            resourceTypes[3] = enResourceType.UIPrefab;
            resourceTypes[4] = enResourceType.UISprite;
            Singleton<CResourceManager>.GetInstance().RemoveCachedResources(resourceTypes);
        }
    }
}

