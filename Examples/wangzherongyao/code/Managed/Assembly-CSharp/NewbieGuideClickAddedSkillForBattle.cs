﻿using Assets.Scripts.GameSystem;
using Assets.Scripts.UI;
using System;
using UnityEngine;

internal class NewbieGuideClickAddedSkillForBattle : NewbieGuideBaseScript
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
                int index = base.currentConf.Param[0];
                Transform transform = form.transform.FindChild("PanelAddSkill/ToggleList");
                if (transform != null)
                {
                    CUIToggleListScript component = transform.gameObject.GetComponent<CUIToggleListScript>();
                    if (component != null)
                    {
                        component.MoveElementInScrollArea(index, true);
                        CUIToggleListElementScript elemenet = component.GetElemenet(index) as CUIToggleListElementScript;
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

