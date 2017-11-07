﻿using Assets.Scripts.GameSystem;
using Assets.Scripts.UI;
using System;
using UnityEngine;

internal class NewbieGuideClickChallenging : NewbieGuideBaseScript
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
            CUIFormScript form = Singleton<CUIManager>.GetInstance().GetForm(CArenaSystem.s_arenaFormPath);
            if (form != null)
            {
                int num = base.currentConf.Param[0];
                string name = string.Format("Root/panelCenter/List/ScrollRect/Content/ListElement_{0}/btnFight", num);
                GameObject gameObject = form.transform.FindChild(name).gameObject;
                base.AddHighLightGameObject(gameObject, true, form, true, new GameObject[0]);
                base.Initialize();
            }
        }
    }
}

