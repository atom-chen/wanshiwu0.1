﻿using Assets.Scripts.GameSystem;
using Assets.Scripts.UI;
using System;
using UnityEngine;

public class NewbieGuideClickSymbolManufacture : NewbieGuideBaseScript
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
            CUIFormScript form = Singleton<CUIManager>.GetInstance().GetForm(CSymbolSystem.s_symbolFormPath);
            if (form != null)
            {
                Transform transform = form.transform.FindChild("TopCommon/Panel_Menu/ListMenu");
                if (transform != null)
                {
                    CUIListScript component = transform.gameObject.GetComponent<CUIListScript>();
                    if (component != null)
                    {
                        CUIListElementScript elemenet = component.GetElemenet(1);
                        if (elemenet != null)
                        {
                            GameObject gameObject = elemenet.transform.gameObject;
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
    }
}

