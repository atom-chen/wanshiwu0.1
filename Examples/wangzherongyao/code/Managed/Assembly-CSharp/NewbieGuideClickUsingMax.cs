﻿using Assets.Scripts.GameSystem;
using Assets.Scripts.UI;
using System;
using UnityEngine;

internal class NewbieGuideClickUsingMax : NewbieGuideBaseScript
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
            CUIFormScript form = Singleton<CUIManager>.GetInstance().GetForm(CBagSystem.s_bagUseFormPath);
            if (form != null)
            {
                Transform transform = form.transform.FindChild("Panel_Left/Button_Count_Max");
                if (transform != null)
                {
                    GameObject gameObject = transform.gameObject;
                    if (gameObject.activeInHierarchy)
                    {
                        base.AddHighLightGameObject(gameObject, true, form, true, new GameObject[0]);
                        base.Initialize();
                    }
                }
            }
        }
    }
}

