﻿using Assets.Scripts.GameSystem;
using Assets.Scripts.UI;
using System;
using UnityEngine;

internal class NewbieGuidePickSymbolManufacture : NewbieGuideBaseScript
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
                int index = base.currentConf.Param[0];
                string name = "Panel_SymbolMake/symbolMakeList";
                Transform transform = form.transform.FindChild(name);
                if (transform != null)
                {
                    CUIListScript component = transform.gameObject.GetComponent<CUIListScript>();
                    if (component != null)
                    {
                        CUIListElementScript elemenet = component.GetElemenet(index);
                        if (elemenet != null)
                        {
                            GameObject gameObject = elemenet.gameObject;
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

