﻿using Assets.Scripts.GameSystem;
using Assets.Scripts.UI;
using System;
using UnityEngine;

public class NewbieGuideCloseMoreHeroFolder : NewbieGuideBaseScript
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
            CUIFormScript form = Singleton<CUIManager>.GetInstance().GetForm(CHeroSelectSystem.s_heroSelectFormPath);
            if (form != null)
            {
                Transform transform = form.gameObject.transform.Find("PanelLeft/ListHostHeroInfo");
                Transform transform2 = form.gameObject.transform.Find("PanelLeft/ListHostHeroInfoFull");
                if ((transform2 != null) && transform2.gameObject.activeInHierarchy)
                {
                    Transform transform3 = transform2.FindChild("btnOpenFullHeroPanel");
                    if (transform3 != null)
                    {
                        GameObject gameObject = transform3.gameObject;
                        if (gameObject.activeInHierarchy)
                        {
                            base.AddHighLightGameObject(gameObject, true, form, true, new GameObject[0]);
                            base.Initialize();
                        }
                    }
                }
                else if ((transform != null) && transform.gameObject.activeInHierarchy)
                {
                    this.CompleteHandler();
                }
            }
        }
    }
}

