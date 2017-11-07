﻿namespace Assets.Scripts.GameSystem
{
    using Assets.Scripts.Common;
    using Assets.Scripts.Framework;
    using Assets.Scripts.GameLogic.GameKernal;
    using Assets.Scripts.UI;
    using ResData;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class CSignal
    {
        public bool BFollow;
        public bool bSmall = true;
        public bool bUseCfgSound;
        private const float c_heroNameInSceneEndTime = 2f;
        private const float c_heroNameInSceneStartTime = 0.4f;
        private float m_duringTime;
        private GameObject m_effectInScene;
        public uint m_heroID;
        private float m_maxDuringTime;
        public uint m_playerID;
        public int m_signalID;
        public ResSignalInfo m_signalInfo;
        private CUIContainerScript m_signalInUIContainer;
        private UIParticleInfo m_signalInUIEffect;
        private int m_signalInUISequence = -1;
        private PoolObjHandle<ActorRoot> m_signalRelatedActor;
        private MinimapSys.ElementType m_type;
        public Vector3 m_worldPosition;

        public CSignal(uint playerID, int signalID, uint heroID, int worldPositionX, int worldPositionY, int worldPositionZ, CUIContainerScript signalInUIContainer, CUIContainerScript heroNameContainer, bool bSmall, bool bFollow, bool bUseCfgSound, MinimapSys.ElementType type)
        {
            this.m_playerID = playerID;
            this.m_signalID = signalID;
            this.m_heroID = heroID;
            this.bSmall = bSmall;
            this.BFollow = bFollow;
            this.bUseCfgSound = bUseCfgSound;
            this.m_worldPosition = new Vector3((float) worldPositionX, (float) worldPositionY, (float) worldPositionZ);
            this.m_signalInUIContainer = signalInUIContainer;
            this.m_signalInUISequence = -1;
            this.m_effectInScene = null;
            this.m_signalInUIEffect = null;
            this.m_type = type;
        }

        public void Dispose()
        {
            if (this.m_effectInScene != null)
            {
                Singleton<CGameObjectPool>.GetInstance().RecycleGameObject(this.m_effectInScene);
                this.m_effectInScene = null;
            }
            if ((this.m_signalInUISequence >= 0) && (this.m_signalInUIContainer != null))
            {
                this.m_signalInUIContainer.RecycleElement(this.m_signalInUISequence);
            }
            this.m_signalInUISequence = -1;
            this.m_signalInUIContainer = null;
            this.m_effectInScene = null;
            this.m_signalInUIEffect = null;
        }

        public void Initialize(CUIFormScript formScript)
        {
            Player player = Singleton<GamePlayerCenter>.GetInstance().GetPlayer(this.m_playerID);
            if ((player != null) && ((((this.m_type != MinimapSys.ElementType.Dragon_5_small) && (this.m_type != MinimapSys.ElementType.Dragon_5_big)) && ((this.m_type != MinimapSys.ElementType.Dragon_3) && (this.m_type != MinimapSys.ElementType.Base))) && (this.m_type != MinimapSys.ElementType.Tower)))
            {
                this.m_signalRelatedActor = player.Captain;
            }
            this.m_signalInfo = GameDataMgr.signalDatabin.GetDataByKey(this.m_signalID);
            if ((this.m_signalInfo == null) || (formScript == null))
            {
                this.m_duringTime = 0f;
                this.m_maxDuringTime = 0f;
            }
            else
            {
                this.m_duringTime = 0f;
                this.m_maxDuringTime = this.m_signalInfo.bTime;
                if ((this.m_signalInfo.bSignalType == 0) && !string.IsNullOrEmpty(this.m_signalInfo.szSceneEffect))
                {
                    this.m_effectInScene = MonoSingleton<SceneMgr>.GetInstance().GetPooledGameObjLOD(this.m_signalInfo.szSceneEffect, true, SceneObjType.Temp, this.m_worldPosition);
                    this.m_effectInScene.CustomSetActive(true);
                }
                if ((this.m_signalInUIContainer != null) && !string.IsNullOrEmpty(this.m_signalInfo.szUIIcon))
                {
                    this.m_signalInUISequence = this.m_signalInUIContainer.GetElement();
                    GameObject element = this.m_signalInUIContainer.GetElement(this.m_signalInUISequence);
                    if (element != null)
                    {
                        Image component = element.GetComponent<Image>();
                        if (component != null)
                        {
                            component.SetSprite(this.m_signalInfo.szUIIcon, formScript, true, false, false);
                            component.color = new Color(1f, 1f, 1f, (this.m_signalInfo.bSignalType != 1) ? ((float) 1) : ((float) 0));
                        }
                        Vector3 worldPosition = this.m_worldPosition;
                        if ((this.m_signalInfo.bSignalType == 1) && (this.m_signalRelatedActor != 0))
                        {
                            worldPosition = (Vector3) this.m_signalRelatedActor.handle.location;
                        }
                        if (this.bSmall)
                        {
                            (element.transform as RectTransform).anchoredPosition = new Vector2(worldPosition.x * Singleton<CBattleSystem>.GetInstance().world_UI_Factor_Small.x, worldPosition.z * Singleton<CBattleSystem>.GetInstance().world_UI_Factor_Small.y);
                        }
                        else
                        {
                            (element.transform as RectTransform).anchoredPosition = new Vector2(worldPosition.x * Singleton<CBattleSystem>.GetInstance().world_UI_Factor_Big.x, worldPosition.z * Singleton<CBattleSystem>.GetInstance().world_UI_Factor_Big.y);
                        }
                        if ((!string.IsNullOrEmpty(this.m_signalInfo.szRealEffect) && (this.m_signalInUISequence >= 0)) && (Singleton<CBattleSystem>.instance.GetMinimapSys().CurMapType() == MinimapSys.EMapType.Mini))
                        {
                            Vector2 sreenLoc = CUIUtility.WorldToScreenPoint(formScript.GetCamera(), element.transform.position);
                            this.m_signalInUIEffect = Singleton<CUIParticleSystem>.instance.AddParticle(this.m_signalInfo.szRealEffect, (float) this.m_signalInfo.bTime, sreenLoc);
                        }
                    }
                }
                if (this.bUseCfgSound)
                {
                    string str = StringHelper.UTF8BytesToString(ref this.m_signalInfo.szSound);
                    if (!string.IsNullOrEmpty(str))
                    {
                        Singleton<CSoundManager>.GetInstance().PostEvent(str, null);
                    }
                }
            }
        }

        public bool IsNeedDisposed()
        {
            return (this.m_duringTime >= this.m_maxDuringTime);
        }

        public void Update(CUIFormScript formScript, float deltaTime)
        {
            if (this.m_duringTime < this.m_maxDuringTime)
            {
                this.m_duringTime += deltaTime;
                if (((this.m_signalInfo != null) && (this.m_signalInfo.bSignalType == 1)) && (this.m_signalRelatedActor != 0))
                {
                    Vector3 location = (Vector3) this.m_signalRelatedActor.handle.location;
                    if (this.m_signalInUISequence >= 0)
                    {
                        GameObject element = this.m_signalInUIContainer.GetElement(this.m_signalInUISequence);
                        if (element != null)
                        {
                            RectTransform transform = element.transform as RectTransform;
                            CBattleSystem instance = Singleton<CBattleSystem>.GetInstance();
                            if (this.bSmall)
                            {
                                transform.anchoredPosition = new Vector2(location.x * instance.world_UI_Factor_Small.x, location.z * instance.world_UI_Factor_Small.y);
                            }
                            else
                            {
                                transform.anchoredPosition = new Vector2(location.x * instance.world_UI_Factor_Big.x, location.z * instance.world_UI_Factor_Big.y);
                            }
                            if ((this.m_signalInUIEffect != null) && (this.m_signalInUIEffect.parObj != null))
                            {
                                Vector2 screenPosition = CUIUtility.WorldToScreenPoint(formScript.GetCamera(), element.transform.position);
                                Singleton<CUIParticleSystem>.GetInstance().SetParticleScreenPosition(this.m_signalInUIEffect, ref screenPosition);
                            }
                        }
                    }
                }
            }
        }
    }
}

