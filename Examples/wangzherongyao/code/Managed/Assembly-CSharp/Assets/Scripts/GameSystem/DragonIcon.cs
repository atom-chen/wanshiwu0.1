﻿namespace Assets.Scripts.GameSystem
{
    using Assets.Scripts.GameLogic;
    using Assets.Scripts.GameLogic.DataCenter;
    using ResData;
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public class DragonIcon
    {
        public const string Dragon_born = "Dragon_born";
        public const string Dragon_dead = "Dragon_dead";
        private bool m_b5v5;
        private ListView<DragonNode> node_ary = new ListView<DragonNode>();

        public static void Check_Dragon_Born_Evt(ActorRoot actor, bool bThrow_Born_Evt)
        {
            if (actor != null)
            {
                switch (actor.ActorControl.GetActorSubSoliderType())
                {
                    case 8:
                    case 9:
                    case 7:
                    case 13:
                        if (bThrow_Born_Evt)
                        {
                            Singleton<EventRouter>.GetInstance().BroadCastEvent<ActorRoot>("Dragon_born", actor);
                        }
                        else
                        {
                            Singleton<EventRouter>.GetInstance().BroadCastEvent<ActorRoot>("Dragon_dead", actor);
                        }
                        break;
                }
            }
        }

        public void Clear()
        {
            for (int i = 0; i < this.node_ary.Count; i++)
            {
                DragonNode node = this.node_ary[i];
                if (node != null)
                {
                    node.Clear();
                }
            }
            this.node_ary.Clear();
            this.node_ary = null;
            Singleton<EventRouter>.GetInstance().RemoveEventHandler<ActorRoot>("Dragon_born", new Action<ActorRoot>(this.onDragon_Born));
            Singleton<EventRouter>.GetInstance().RemoveEventHandler<ActorRoot>("Dragon_dead", new Action<ActorRoot>(this.onDragon_Dead));
            this.m_b5v5 = false;
        }

        private DragonNode getDragonNode(byte type = 0)
        {
            for (int i = 0; i < this.node_ary.Count; i++)
            {
                DragonNode node = this.node_ary[i];
                if ((node != null) && node.IsType(type))
                {
                    return node;
                }
            }
            return null;
        }

        private DragonNode getDragonNode(uint objid, byte type)
        {
            for (int i = 0; i < this.node_ary.Count; i++)
            {
                DragonNode node = this.node_ary[i];
                if (((node != null) && node.IsType(type)) && (node.objid == objid))
                {
                    return node;
                }
            }
            for (int j = 0; j < this.node_ary.Count; j++)
            {
                DragonNode node2 = this.node_ary[j];
                if (((node2 != null) && node2.IsType(type)) && (node2.objid == 0))
                {
                    node2.objid = objid;
                    return node2;
                }
            }
            return null;
        }

        public void Init(GameObject node, GameObject bigNode, bool b5V5)
        {
            this.m_b5v5 = b5V5;
            Singleton<EventRouter>.GetInstance().AddEventHandler<ActorRoot>("Dragon_born", new Action<ActorRoot>(this.onDragon_Born));
            Singleton<EventRouter>.GetInstance().AddEventHandler<ActorRoot>("Dragon_dead", new Action<ActorRoot>(this.onDragon_Dead));
            for (int i = 0; i < node.transform.childCount; i++)
            {
                node.transform.GetChild(i).gameObject.CustomSetActive(false);
            }
            for (int j = 0; j < bigNode.transform.childCount; j++)
            {
                bigNode.transform.GetChild(j).gameObject.CustomSetActive(false);
            }
            this.node_ary.Add(new DragonNode(node, bigNode, "d_3", 7, 0));
            this.node_ary.Add(new DragonNode(node, bigNode, "d_5_big", 8, 0));
            this.node_ary.Add(new DragonNode(node, bigNode, "d_5_small_1", 9, 13));
            this.node_ary.Add(new DragonNode(node, bigNode, "d_5_small_2", 9, 13));
            SpawnGroup group = null;
            ListView<SpawnGroup> spawnGroups = Singleton<BattleLogic>.instance.mapLogic.GetSpawnGroups();
            for (int k = 0; k < spawnGroups.Count; k++)
            {
                group = spawnGroups[k];
                if ((group != null) && (group.NextGroups.Length == 0))
                {
                    ActorMeta meta = group.TheActorsMeta[0];
                    ResMonsterCfgInfo dataCfgInfoByCurLevelDiff = MonsterDataHelper.GetDataCfgInfoByCurLevelDiff(meta.ConfigId);
                    if ((((dataCfgInfoByCurLevelDiff.bSoldierType == 8) || (dataCfgInfoByCurLevelDiff.bSoldierType == 9)) || (dataCfgInfoByCurLevelDiff.bSoldierType == 7)) || (dataCfgInfoByCurLevelDiff.bSoldierType == 13))
                    {
                        DragonNode node2 = this.getDragonNode(dataCfgInfoByCurLevelDiff.bSoldierType);
                        if (node2 != null)
                        {
                            node2.SetData(group.gameObject.transform.position, dataCfgInfoByCurLevelDiff.bSoldierType, 0, this.m_b5v5);
                            node2.ShowDead(true);
                            MiniMapSysUT.RefreshMapPointerBig(node2.node_in_bigMap);
                        }
                    }
                }
            }
        }

        private void onDragon_Born(ActorRoot actor)
        {
            DragonNode node = this.getDragonNode(actor.ObjID, actor.ActorControl.GetActorSubSoliderType());
            DebugHelper.Assert(node != null, "onDragon_Born node == null, check out...");
            if (node != null)
            {
                node.SetData(actor.gameObject.transform.position, actor.ActorControl.GetActorSubSoliderType(), actor.ObjID, this.m_b5v5);
                node.ShowDead(actor.ActorControl.IsDeadState);
                switch (actor.ActorControl.GetActorSubSoliderType())
                {
                    case 8:
                    case 9:
                    case 13:
                    {
                        bool flag = Singleton<CBattleSystem>.instance.GetMinimapSys().CurMapType() == MinimapSys.EMapType.Mini;
                        if (flag)
                        {
                            TowerHit._play_effect("Prefab_Skill_Effects/tongyong_effects/Indicator/blin_01_c.prefab", 3f, !flag ? node.node_in_bigMap : node.node_in_smallMap);
                        }
                        Singleton<CSoundManager>.GetInstance().PlayBattleSound("Play_BaoJun_VO_Anger", null);
                        MiniMapSysUT.RefreshMapPointerBig(node.node_in_bigMap);
                        break;
                    }
                }
            }
        }

        private void onDragon_Dead(ActorRoot actor)
        {
            DragonNode node = this.getDragonNode(actor.ObjID, actor.ActorControl.GetActorSubSoliderType());
            if (node != null)
            {
                node.SetData(actor.gameObject.transform.position, actor.ActorControl.GetActorSubSoliderType(), actor.ObjID, this.m_b5v5);
                node.ShowDead(actor.ActorControl.IsDeadState);
                node.objid = 0;
                SLevelContext curLvelContext = Singleton<BattleLogic>.GetInstance().GetCurLvelContext();
                if ((curLvelContext != null) && (curLvelContext.iLevelID == 0x4e29))
                {
                    node.Recycle();
                }
            }
        }

        private class DragonNode
        {
            public GameObject dragon_dead_icon_bigMap;
            public GameObject dragon_dead_icon_smallMap;
            public GameObject dragon_live_icon_bigMap;
            public GameObject dragon_live_icon_smallMap;
            public GameObject node_in_bigMap;
            public GameObject node_in_smallMap;
            public uint objid;
            public byte optType;
            public byte type;

            public DragonNode(GameObject small, GameObject big, string path, byte type, byte optType = 0)
            {
                this._init(small.transform.Find(path).gameObject, big.transform.Find(path).gameObject, type, optType);
            }

            public void _init(GameObject nodeInSmallMap, GameObject nodeInBigMap, byte type, byte optType)
            {
                this.type = type;
                this.optType = optType;
                this.node_in_smallMap = nodeInSmallMap;
                this.dragon_live_icon_smallMap = this.node_in_smallMap.transform.Find("live").gameObject;
                this.dragon_dead_icon_smallMap = this.node_in_smallMap.transform.Find("dead").gameObject;
                this.node_in_bigMap = nodeInBigMap;
                this.dragon_live_icon_bigMap = this.node_in_bigMap.transform.Find("live").gameObject;
                this.dragon_dead_icon_bigMap = this.node_in_bigMap.transform.Find("dead").gameObject;
                if (type == 7)
                {
                    MiniMapSysUT.SetMapElement_EventParam(nodeInBigMap, false, MinimapSys.ElementType.Dragon_3, 0, 0);
                }
                else if (type == 8)
                {
                    MiniMapSysUT.SetMapElement_EventParam(nodeInBigMap, false, MinimapSys.ElementType.Dragon_5_big, 0, 0);
                }
                else if (type == 9)
                {
                    MiniMapSysUT.SetMapElement_EventParam(nodeInBigMap, false, MinimapSys.ElementType.Dragon_5_small, 0, 0);
                }
            }

            public void Clear()
            {
                this.dragon_live_icon_smallMap = null;
                this.dragon_dead_icon_smallMap = null;
                this.node_in_smallMap = null;
                this.dragon_live_icon_bigMap = null;
                this.dragon_dead_icon_bigMap = null;
                this.node_in_bigMap = null;
                this.objid = 0;
                this.type = 0;
                this.optType = 0;
            }

            public bool IsType(byte type)
            {
                return ((this.type == type) || (this.optType == type));
            }

            public void Recycle()
            {
                this.node_in_smallMap.CustomSetActive(false);
                this.node_in_bigMap.CustomSetActive(false);
            }

            public void SetData(Vector3 worldpos, int type, uint id, bool b5v5 = false)
            {
                if (b5v5)
                {
                    if (type == 8)
                    {
                        this.SetScale(false);
                    }
                    else if (type == 9)
                    {
                        this.SetScale(true);
                    }
                }
                RectTransform transform = this.node_in_smallMap.transform as RectTransform;
                transform.anchoredPosition = new Vector2(worldpos.x * Singleton<CBattleSystem>.instance.world_UI_Factor_Small.x, worldpos.z * Singleton<CBattleSystem>.instance.world_UI_Factor_Small.y);
                RectTransform transform2 = this.node_in_bigMap.transform as RectTransform;
                transform2.anchoredPosition = new Vector2(worldpos.x * Singleton<CBattleSystem>.instance.world_UI_Factor_Big.x, worldpos.z * Singleton<CBattleSystem>.instance.world_UI_Factor_Big.y);
                this.objid = id;
            }

            public void SetScale(bool bSmall = false)
            {
                if (bSmall)
                {
                    this.node_in_smallMap.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
                }
                else
                {
                    this.node_in_smallMap.transform.localScale = new Vector3(1f, 1f, 1f);
                }
            }

            public void ShowDead(bool bDead)
            {
                this.node_in_smallMap.CustomSetActive(true);
                this.node_in_bigMap.CustomSetActive(true);
                if (!bDead)
                {
                    this.dragon_live_icon_smallMap.CustomSetActive(true);
                    this.dragon_live_icon_bigMap.CustomSetActive(true);
                    this.dragon_dead_icon_smallMap.CustomSetActive(false);
                    this.dragon_dead_icon_bigMap.CustomSetActive(false);
                }
                else
                {
                    this.dragon_live_icon_smallMap.CustomSetActive(false);
                    this.dragon_live_icon_bigMap.CustomSetActive(false);
                    this.dragon_dead_icon_smallMap.CustomSetActive(true);
                    this.dragon_dead_icon_bigMap.CustomSetActive(true);
                }
            }
        }
    }
}

