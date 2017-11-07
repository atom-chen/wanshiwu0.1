﻿using Assets.Scripts.GameSystem;
using Assets.Scripts.UI;
using System;
using UnityEngine;

public class NewbieGuideClickJungleSword : NewbieGuideBaseScript
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
            CUIFormScript form = Singleton<CUIManager>.GetInstance().GetForm(CBattleEquipSystem.s_equipFormPath);
            if (form != null)
            {
                GameObject gameObject = form.GetWidget(1).transform.FindChild("equipItem0").gameObject;
                DebugHelper.Assert(gameObject != null, "Can't find equipItem0~!!");
                base.AddHighLightGameObject(gameObject, true, form, true, new GameObject[0]);
                base.Initialize();
            }
        }
    }
}

