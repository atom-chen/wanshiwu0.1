﻿using Assets.Scripts.GameLogic;
using Assets.Scripts.GameSystem;
using Assets.Scripts.UI;
using System;
using UnityEngine;

internal class NewbieGuideClickCaptainButton : NewbieGuideBaseScript
{
    protected override void Initialize()
    {
    }

    protected override bool IsDelegateClickEvent()
    {
        return true;
    }

    protected override bool IsDelegateModalControl()
    {
        return true;
    }

    protected override void Update()
    {
        if (base.isInitialize)
        {
            base.Update();
        }
        else
        {
            CUIFormScript form = Singleton<CUIManager>.GetInstance().GetForm(CBattleSystem.s_battleUIForm);
            if (form != null)
            {
                string name = string.Format("HeroHeadHud/HeroHead{0}", base.currentConf.Param[0]);
                Transform transform = form.transform.FindChild(name);
                if (transform != null)
                {
                    GameObject gameObject = transform.gameObject;
                    if (gameObject.activeInHierarchy)
                    {
                        base.AddHighLightGameObject(gameObject, true, form, true, new GameObject[0]);
                        if ((NewbieGuideBaseScript.ms_originalGo.Count > 0) && (NewbieGuideBaseScript.ms_highlitGo.Count > 0))
                        {
                            PlayerHead component = NewbieGuideBaseScript.ms_originalGo[0].GetComponent<PlayerHead>();
                            PlayerHead head2 = NewbieGuideBaseScript.ms_highlitGo[0].GetComponent<PlayerHead>();
                            if ((component != null) && (head2 != null))
                            {
                                head2.SetPrivates(component.state, component.MyHero, component.HudOwner);
                            }
                        }
                        base.Initialize();
                    }
                    else
                    {
                        this.CompleteHandler();
                    }
                }
            }
        }
    }
}

