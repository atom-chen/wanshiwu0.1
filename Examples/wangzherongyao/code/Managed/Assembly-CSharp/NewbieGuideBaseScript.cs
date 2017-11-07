﻿using Assets.Scripts.GameSystem;
using Assets.Scripts.UI;
using ResData;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class NewbieGuideBaseScript : MonoBehaviour
{
    public const float CLICK_GO_ENABLE_TIME_OUT = 5f;
    public const string HighlighterPath = "UGUI/Form/System/Dialog/WeakGuideHighlighter.prefab";
    public const float INITIALIZE_TIME_OUT = 3f;
    private ECompleteType m_completeType;
    protected static List<GameObject> ms_guideTextList = new List<GameObject>();
    protected static List<GameObject> ms_highlighter = new List<GameObject>();
    protected static List<GameObject> ms_highlitGo = new List<GameObject>();
    private static CUIFormScript ms_originalForm = null;
    protected static List<GameObject> ms_originalGo = new List<GameObject>();
    private static Vector3 s_FlipNone = Vector3.one;
    private static Vector3 s_FlipX = new Vector3(-1f, 1f, 1f);
    private static Vector3 s_FlipXY = new Vector3(-1f, -1f, 1f);
    private static Vector3 s_FlipY = new Vector3(1f, -1f, 1f);

    public event NewbieGuideBaseScriptDelegate CheckSaveEvent;

    public event NewbieGuideBaseScriptDelegate CompleteEvent;

    public event NewbieGuideBaseScriptDelegate onCompleteAll;

    private void AddDelegate()
    {
        if (this.IsDelegateDisableFinger())
        {
            this.TryToDisableFinger();
        }
        if (this.IsDelegateClickEvent())
        {
            this.TryToAddClickEvent();
        }
        if (this.IsDelegatePressEvent())
        {
            this.TryToAddPressEvent();
        }
        if (this.IsDelegateModalControl())
        {
            this.EnableModalControl();
        }
        if (this.IsDelegateSpecialTip())
        {
            this.AddSpecialTip();
        }
        if (!this.IsDelegateScreenLock())
        {
            this.RemoveSceenLock();
        }
        if (!this.IsDelegateShowGuideMask())
        {
            this.ShowGuideMask(false);
        }
    }

    protected void AddHighLightAnyWhere()
    {
        this.PreHighlight();
        this.m_completeType = ECompleteType.ClickAnyWhere;
        this.OpenGuideForm();
    }

    protected void AddHighLightGameObject(GameObject baseGo, bool isUI, CUIFormScript inOriginalForm, bool cloneEvent = true, params GameObject[] addGo)
    {
        this.AddHighlightInternal(baseGo, inOriginalForm, cloneEvent, addGo);
    }

    private void AddHighlightInternal(GameObject baseGo, CUIFormScript inOriginalForm, bool cloneEvent, params GameObject[] addGo)
    {
        this.PreHighlight();
        if (baseGo != null)
        {
            ms_originalGo.Add(baseGo);
        }
        if (addGo != null)
        {
            ms_originalGo.AddRange(addGo);
        }
        ms_originalForm = inOriginalForm;
        this.OpenGuideForm();
        if (NewbieGuideScriptControl.FormGuideMask == null)
        {
            NewbieGuideScriptControl.OpenGuideForm();
        }
        List<GameObject>.Enumerator enumerator = ms_originalGo.GetEnumerator();
        int num = 0;
        while (enumerator.MoveNext())
        {
            GameObject current = enumerator.Current;
            if (current != null)
            {
                GameObject widget = UnityEngine.Object.Instantiate(current) as GameObject;
                if (widget != null)
                {
                    RectTransform transform = widget.transform as RectTransform;
                    transform.SetParent(NewbieGuideScriptControl.FormGuideMask.transform);
                    transform.SetSiblingIndex(1);
                    transform.localScale = current.transform.localScale;
                    RectTransform transform2 = current.transform as RectTransform;
                    transform.pivot = transform2.pivot;
                    transform.sizeDelta = transform2.sizeDelta;
                    LayoutElement component = current.GetComponent<LayoutElement>();
                    if ((component != null) && (transform.sizeDelta == Vector2.zero))
                    {
                        transform.sizeDelta = new Vector2(component.preferredWidth, component.preferredHeight);
                    }
                    transform.position = current.transform.position;
                    Vector2 screenPoint = CUIUtility.WorldToScreenPoint(inOriginalForm.GetCamera(), current.transform.position);
                    Vector3 worldPosition = CUIUtility.ScreenToWorldPoint(NewbieGuideScriptControl.FormGuideMask.GetCamera(), screenPoint, transform.position.z);
                    NewbieGuideScriptControl.FormGuideMask.InitializeWidgetPosition(widget, worldPosition);
                    widget.CustomSetActive(false);
                    if (cloneEvent)
                    {
                        CUIEventScript script = current.GetComponent<CUIEventScript>();
                        CUIEventScript script2 = widget.GetComponent<CUIEventScript>();
                        if ((script != null) && (script2 != null))
                        {
                            script2.m_onDownEventParams = script.m_onDownEventParams;
                            script2.m_onUpEventParams = script.m_onUpEventParams;
                            script2.m_onClickEventParams = script.m_onClickEventParams;
                            script2.m_onHoldStartEventParams = script.m_onHoldStartEventParams;
                            script2.m_onHoldEventParams = script.m_onHoldEventParams;
                            script2.m_onHoldEndEventParams = script.m_onHoldEndEventParams;
                            script2.m_onDragStartEventParams = script.m_onDragStartEventParams;
                            script2.m_onDragEventParams = script.m_onDragEventParams;
                            script2.m_onDragEndEventParams = script.m_onDragEndEventParams;
                            script2.m_onDropEventParams = script.m_onDropEventParams;
                            script2.m_closeFormWhenClicked = script.m_closeFormWhenClicked;
                            script2.m_belongedFormScript = script.m_belongedFormScript;
                            script2.m_belongedListScript = script.m_belongedListScript;
                            script2.m_indexInlist = script.m_indexInlist;
                        }
                        CUIMiniEventScript script3 = current.GetComponent<CUIMiniEventScript>();
                        CUIMiniEventScript script4 = widget.GetComponent<CUIMiniEventScript>();
                        if ((script3 != null) && (script4 != null))
                        {
                            script4.m_onDownEventParams = script3.m_onDownEventParams;
                            script4.m_onUpEventParams = script3.m_onUpEventParams;
                            script4.m_onClickEventParams = script3.m_onClickEventParams;
                            script4.m_closeFormWhenClicked = script3.m_closeFormWhenClicked;
                            script4.m_belongedFormScript = script3.m_belongedFormScript;
                            script4.m_belongedListScript = script3.m_belongedListScript;
                            script4.m_indexInlist = script3.m_indexInlist;
                        }
                    }
                    else
                    {
                        CUIEventScript script5 = widget.GetComponent<CUIEventScript>();
                        if (script5 != null)
                        {
                            script5.enabled = false;
                        }
                        CUIMiniEventScript script6 = widget.GetComponent<CUIMiniEventScript>();
                        if (script6 != null)
                        {
                            script6.enabled = false;
                        }
                    }
                    widget.CustomSetActive(true);
                    CUIAnimatorScript script7 = current.GetComponent<CUIAnimatorScript>();
                    if (script7 != null)
                    {
                        CUICommonSystem.PlayAnimator(widget, script7.m_currentAnimatorStateName);
                    }
                    ms_highlitGo.Add(widget);
                    GameObject content = Singleton<CResourceManager>.GetInstance().GetResource("UGUI/Form/System/Dialog/WeakGuideHighlighter.prefab", typeof(GameObject), enResourceType.UIPrefab, false, false).m_content as GameObject;
                    if (content != null)
                    {
                        GameObject item = UnityEngine.Object.Instantiate(content) as GameObject;
                        if (item != null)
                        {
                            item.transform.SetParent(widget.transform);
                            Transform transform3 = item.transform;
                            switch (this.currentConf.wFlipType)
                            {
                                case 0:
                                    transform3.localScale = s_FlipNone;
                                    break;

                                case 1:
                                    transform3.localScale = s_FlipX;
                                    break;

                                case 2:
                                    transform3.localScale = s_FlipY;
                                    break;

                                case 3:
                                    transform3.localScale = s_FlipXY;
                                    break;
                            }
                            item.transform.position = widget.transform.position;
                            (item.transform as RectTransform).anchoredPosition = new Vector2((float) this.currentConf.iOffsetHighLightX, (float) this.currentConf.iOffsetHighLightY);
                            if (!this.DoesShowArrow())
                            {
                                item.transform.FindChild("Panel/ImageFinger").gameObject.CustomSetActive(false);
                            }
                            ms_highlighter.Add(item);
                        }
                    }
                    if ((num == 0) && (this.currentConf.wSpecialTip != 0))
                    {
                        NewbieGuideSpecialTipConf specialTipConfig = Singleton<NewbieGuideDataManager>.GetInstance().GetSpecialTipConfig(this.currentConf.wSpecialTip);
                        if ((specialTipConfig != null) && (specialTipConfig.bGuideTextPos > 0))
                        {
                            GameObject obj6 = InstantiateGuideText(specialTipConfig, widget, NewbieGuideScriptControl.FormGuideMask, inOriginalForm);
                            if (obj6 != null)
                            {
                                ms_guideTextList.Add(obj6);
                                obj6.CustomSetActive(false);
                            }
                        }
                    }
                    num++;
                }
            }
        }
    }

    protected void AddHighlightWaiting()
    {
        this.PreHighlight();
        this.m_completeType = ECompleteType.WaitOneWhile;
        this.OpenGuideForm();
    }

    protected void AddScreenLock()
    {
    }

    private void AddSpecialTip()
    {
        if (NewbieGuideScriptControl.FormGuideMask == null)
        {
            NewbieGuideScriptControl.OpenGuideForm();
        }
        GameObject gameObject = NewbieGuideScriptControl.FormGuideMask.transform.FindChild("GuideTextStatic").transform.gameObject;
        gameObject.CustomSetActive(false);
        if (this.currentConf.wSpecialTip != 0)
        {
            NewbieGuideSpecialTipConf specialTipConfig = Singleton<NewbieGuideDataManager>.GetInstance().GetSpecialTipConfig(this.currentConf.wSpecialTip);
            if ((specialTipConfig != null) && (specialTipConfig.bGuideTextPos == 0))
            {
                gameObject.CustomSetActive(true);
                gameObject.transform.FindChild("RightSpecial/Text").transform.gameObject.GetComponent<Text>().text = StringHelper.UTF8BytesToString(ref specialTipConfig.szTipText);
            }
        }
    }

    private void AnyWhereClick(CUIEvent inUiEvent)
    {
        this.ClickHandler(inUiEvent);
    }

    private void Awake()
    {
    }

    private void CheckCameraMove()
    {
    }

    private void CheckClickGOEnableTimeOut()
    {
    }

    public void CheckComplete(NewbieGuideScriptType type, uint[] param)
    {
        if ((type == ((NewbieGuideScriptType) this.currentConf.wType)) && this.CheckPram(param))
        {
            this.CompleteHandler();
        }
    }

    private void CheckInitializeTimeOut()
    {
    }

    protected virtual bool CheckPram(uint[] param)
    {
        return false;
    }

    private void CheckSave()
    {
        if (this.CheckSaveEvent != null)
        {
            this.CheckSaveEvent();
        }
    }

    protected virtual void Clear()
    {
        this.isGuiding = false;
        this.TryToResumeGame();
        this.ClearDelegate();
        this.ClearHighLightGameObject();
    }

    private void ClearDelegate()
    {
        if (this.IsDelegateDisableFinger())
        {
            this.TryToEnableFinger();
        }
        if (this.IsDelegateClickEvent())
        {
            this.TryToRemoveClickEvent();
        }
        if (this.IsDelegatePressEvent())
        {
            this.TryToRemovePressEvent();
        }
        if (this.IsDelegateSpecialTip())
        {
            this.RemoveSpecialTip();
        }
        if (this.IsDelegateModalControl())
        {
            this.DisableModalControl();
        }
        if (!this.IsDelegateScreenLock())
        {
            this.AddScreenLock();
        }
        if (!this.IsDelegateShowGuideMask())
        {
            this.ShowGuideMask(true);
        }
    }

    private void ClearHighLightGameObject()
    {
        this.ClearHighlightInternal();
    }

    private void ClearHighlightInternal()
    {
        this.SetBackgroundTransparency(EOpacityLadder.Transparent);
        this.ClearHighlitObjs();
        ms_originalGo.Clear();
        ms_originalForm = null;
        this.m_completeType = ECompleteType.ClickButton;
    }

    private void ClearHighlitObjs()
    {
        List<GameObject> list = new List<GameObject>();
        list.AddRange(ms_highlitGo);
        list.AddRange(ms_highlighter);
        List<GameObject>.Enumerator enumerator = list.GetEnumerator();
        while (enumerator.MoveNext())
        {
            GameObject current = enumerator.Current;
            if (current != null)
            {
                current.transform.SetParent(null);
                UnityEngine.Object.Destroy(current);
            }
        }
        ms_highlitGo.Clear();
        ms_highlighter.Clear();
        int count = ms_guideTextList.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject obj3 = ms_guideTextList[i];
            if (obj3 != null)
            {
                obj3.CustomSetActive(false);
            }
        }
        ms_guideTextList.Clear();
    }

    protected virtual void ClickHandler(CUIEvent uiEvent)
    {
        this.CompleteHandler();
    }

    protected virtual void CompleteAllHandler()
    {
        this.Clear();
        if (this.IsDelegateAutoDispatchCompleteEvent())
        {
            this.DispatchCompleteAllEvent();
        }
    }

    protected virtual void CompleteHandler()
    {
        this.Clear();
        if (this.IsDelegateAutoDispatchCompleteEvent())
        {
            this.DispatchCompleteEvent();
        }
    }

    private void DisableModalControl()
    {
    }

    protected void DispatchCompleteAllEvent()
    {
        if (this.onCompleteAll != null)
        {
            this.onCompleteAll();
        }
    }

    protected void DispatchCompleteEvent()
    {
        if (this.CompleteEvent != null)
        {
            this.CompleteEvent();
        }
    }

    private bool DoesShowArrow()
    {
        return (this.currentConf.bNotShowArrow == 0);
    }

    private void EnableModalControl()
    {
    }

    protected void Error(string value)
    {
        if (value != null)
        {
        }
        this.isError = true;
    }

    private void ErrorHandler()
    {
    }

    protected virtual void ForceClear()
    {
    }

    protected void HideHighlighterAndText()
    {
        this.SetBackgroundTransparency(EOpacityLadder.Transparent);
        List<GameObject> list = new List<GameObject>();
        list.AddRange(ms_highlighter);
        List<GameObject>.Enumerator enumerator = list.GetEnumerator();
        while (enumerator.MoveNext())
        {
            GameObject current = enumerator.Current;
            if (current != null)
            {
                current.CustomSetActive(false);
            }
        }
        int count = ms_guideTextList.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject obj3 = ms_guideTextList[i];
            if (obj3 != null)
            {
                obj3.CustomSetActive(false);
            }
        }
        ms_guideTextList.Clear();
    }

    protected virtual void Initialize()
    {
        if (!this.isInitialize)
        {
            this.TryToPauseGame();
            this.TryToPlayerSound();
            this.AddDelegate();
            this.isInitialize = true;
            this.isGuiding = true;
        }
    }

    public static GameObject InstantiateGuideText(NewbieGuideSpecialTipConf tipConf, GameObject inParentObj, CUIFormScript inGuideForm, CUIFormScript inOriginalForm)
    {
        if (((tipConf == null) || (inParentObj == null)) || ((inGuideForm == null) || (inOriginalForm == null)))
        {
            return null;
        }
        GameObject gameObject = inGuideForm.gameObject.transform.Find("GuideText").gameObject;
        if (gameObject != null)
        {
            Transform transform = gameObject.transform.FindChild("RightSpecial/Text").transform;
            if (transform != null)
            {
                Text component = transform.gameObject.GetComponent<Text>();
                if (component != null)
                {
                    component.text = StringHelper.UTF8BytesToString(ref tipConf.szTipText);
                }
            }
            gameObject.CustomSetActive(true);
        }
        return gameObject;
    }

    protected virtual bool IsDelegateAutoDispatchCompleteEvent()
    {
        return true;
    }

    protected virtual bool IsDelegateCameraMove()
    {
        return false;
    }

    protected virtual bool IsDelegateCheckTimeOut()
    {
        return true;
    }

    protected virtual bool IsDelegateClickEvent()
    {
        return false;
    }

    protected virtual bool IsDelegateDisableFinger()
    {
        return true;
    }

    protected virtual bool IsDelegateModalControl()
    {
        return false;
    }

    protected virtual bool IsDelegatePressEvent()
    {
        return false;
    }

    protected virtual bool IsDelegateScreenLock()
    {
        return true;
    }

    protected virtual bool IsDelegateShowGuideMask()
    {
        return true;
    }

    protected virtual bool IsDelegateSpecialTip()
    {
        return true;
    }

    public virtual bool IsTimeOutSkip()
    {
        return true;
    }

    private void LateUpdate()
    {
        if (!this.isInitialize && this.IsDelegateCheckTimeOut())
        {
            this.CheckInitializeTimeOut();
        }
        if (this.isInitialize && this.isGuiding)
        {
            this.CheckClickGOEnableTimeOut();
            this.CheckCameraMove();
        }
        if (this.isError)
        {
            this.ErrorHandler();
        }
    }

    protected virtual bool ModalDisableClick()
    {
        return true;
    }

    protected virtual void OnPause()
    {
    }

    protected virtual void OnResume()
    {
    }

    protected virtual void OnSelectHandler()
    {
    }

    private void OpenGuideForm()
    {
        this.SetBackgroundTransparency((EOpacityLadder) this.currentConf.bShowTransparency);
    }

    public void Pause()
    {
        this.OnPause();
        this.Clear();
        base.enabled = false;
    }

    private void PreHighlight()
    {
        this.m_completeType = ECompleteType.ClickButton;
        ms_originalGo.Clear();
        ms_originalForm = null;
        this.ClearHighlitObjs();
        if ((NewbieGuideScriptControl.FormGuideMask != null) && (NewbieGuideScriptControl.FormGuideMask.gameObject != null))
        {
            Transform transform = NewbieGuideScriptControl.FormGuideMask.gameObject.transform.Find("GuideText");
            if ((transform != null) && (transform.gameObject != null))
            {
                transform.gameObject.CustomSetActive(false);
            }
        }
    }

    protected virtual void PressHandler(GameObject go, bool state)
    {
        if (state)
        {
            this.CompleteHandler();
            this.TransferPressEvent(go, state);
        }
    }

    protected void RemoveSceenLock()
    {
    }

    private void RemoveSpecialTip()
    {
        if (this.currentConf.wSpecialTip != 0)
        {
        }
    }

    public void Resume()
    {
        this.isInitialize = false;
        this.OnResume();
        this.Initialize();
        base.enabled = true;
    }

    protected void SetBackgroundTransparency(EOpacityLadder inLadder)
    {
        if ((NewbieGuideScriptControl.FormGuideMask != null) && (NewbieGuideScriptControl.FormGuideMask.transform != null))
        {
            float a = 0f;
            switch (inLadder)
            {
                case EOpacityLadder.Standard:
                    a = 0.4f;
                    break;

                case EOpacityLadder.Transparent:
                    a = 0f;
                    break;

                case EOpacityLadder.Darker:
                    a = 0.65f;
                    break;
            }
            Transform transform = NewbieGuideScriptControl.FormGuideMask.transform.FindChild("Bg");
            if (transform != null)
            {
                Image component = transform.gameObject.GetComponent<Image>();
                if (component != null)
                {
                    component.color = new Color(0f, 0f, 0f, a);
                }
            }
        }
    }

    public void SetData(NewbieGuideScriptConf conf)
    {
        this.currentConf = conf;
    }

    private void SetLayer(GameObject go, DictionaryObjectView<GameObject, int> layerDic, int targetLayer)
    {
        layerDic.Add(go, go.layer);
        go.layer = targetLayer;
        Transform transform = go.transform;
        int index = 0;
        int childCount = transform.childCount;
        while (index < childCount)
        {
            Transform child = transform.GetChild(index);
            this.SetLayer(child.gameObject, layerDic, targetLayer);
            index++;
        }
    }

    private void ShowGuideMask(bool bShow)
    {
        if (NewbieGuideScriptControl.FormGuideMask != null)
        {
            NewbieGuideScriptControl.CloseGuideForm();
        }
    }

    protected virtual void SpeicalTipConfirmHandler()
    {
        this.CompleteHandler();
    }

    private void Start()
    {
        this.highLightGameObjectList = new ListView<HightLightItem>();
        this.Initialize();
    }

    public void Stop()
    {
        this.Clear();
        this.ForceClear();
    }

    protected virtual void TransferPressEvent(GameObject go, bool state)
    {
    }

    private void TryToAddClickEvent()
    {
        List<GameObject>.Enumerator enumerator = ms_highlitGo.GetEnumerator();
        while (enumerator.MoveNext())
        {
            GameObject current = enumerator.Current;
            if (current != null)
            {
                CUIEventScript component = current.GetComponent<CUIEventScript>();
                if (component != null)
                {
                    component.onClick = (CUIEventScript.OnUIEventHandler) Delegate.Combine(component.onClick, new CUIEventScript.OnUIEventHandler(this.ClickHandler));
                }
                CUIMiniEventScript script2 = current.GetComponent<CUIMiniEventScript>();
                if (script2 != null)
                {
                    script2.onClick = (CUIMiniEventScript.OnUIEventHandler) Delegate.Combine(script2.onClick, new CUIMiniEventScript.OnUIEventHandler(this.ClickHandler));
                }
            }
        }
        if (this.m_completeType == ECompleteType.ClickAnyWhere)
        {
            Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Dialogue_AnyWhereClick, new CUIEventManager.OnUIEventHandler(this.AnyWhereClick));
        }
    }

    private void TryToAddPressEvent()
    {
    }

    private void TryToDisableFinger()
    {
    }

    private void TryToEnableFinger()
    {
    }

    private void TryToPauseGame()
    {
        if (this.currentConf.bStopGame == 1)
        {
            Singleton<CBattleGuideManager>.GetInstance().PauseGame(this, false);
        }
        else if (this.currentConf.bStopGame == 2)
        {
        }
    }

    private void TryToPlayerSound()
    {
        if (!string.IsNullOrEmpty(this.currentConf.szSoundFileName))
        {
            Singleton<CSoundManager>.GetInstance().PostEvent(this.currentConf.szSoundFileName, null);
        }
    }

    private void TryToRemoveClickEvent()
    {
        bool flag = false;
        int num = 0;
        List<GameObject>.Enumerator enumerator = ms_highlitGo.GetEnumerator();
        while (enumerator.MoveNext())
        {
            GameObject current = enumerator.Current;
            if (current != null)
            {
                CUIEventScript component = current.GetComponent<CUIEventScript>();
                if (component != null)
                {
                    component.onClick = (CUIEventScript.OnUIEventHandler) Delegate.Remove(component.onClick, new CUIEventScript.OnUIEventHandler(this.ClickHandler));
                    flag = true;
                }
                CUIMiniEventScript script2 = current.GetComponent<CUIMiniEventScript>();
                if (script2 != null)
                {
                    script2.onClick = (CUIMiniEventScript.OnUIEventHandler) Delegate.Remove(script2.onClick, new CUIMiniEventScript.OnUIEventHandler(this.ClickHandler));
                    flag = true;
                }
                Toggle toggle = current.GetComponent<Toggle>();
                if (toggle != null)
                {
                    toggle.isOn = false;
                    Toggle toggle2 = ms_originalGo[num].GetComponent<Toggle>();
                    if (toggle2 != null)
                    {
                        toggle2.isOn = true;
                    }
                }
                if (!flag)
                {
                }
                num++;
            }
        }
        if (this.m_completeType == ECompleteType.ClickAnyWhere)
        {
            Singleton<CUIEventManager>.GetInstance().RemoveUIEventListener(enUIEventID.Dialogue_AnyWhereClick, new CUIEventManager.OnUIEventHandler(this.AnyWhereClick));
        }
    }

    private void TryToRemovePressEvent()
    {
    }

    private void TryToResumeGame()
    {
        if (this.isInitialize)
        {
            if (this.currentConf.bStopGame == 1)
            {
                Singleton<CBattleGuideManager>.GetInstance().ResumeGame(this);
            }
            else if (this.currentConf.bStopGame == 2)
            {
            }
        }
    }

    protected virtual void Update()
    {
        CUIFormScript formGuideMask = NewbieGuideScriptControl.FormGuideMask;
        if ((ms_originalForm != null) && (formGuideMask != null))
        {
            int count = ms_highlitGo.Count;
            DebugHelper.Assert(count <= ms_originalGo.Count);
            for (int i = 0; i < count; i++)
            {
                GameObject obj2 = ms_highlitGo[i];
                GameObject inParentObj = ms_originalGo[i];
                if ((obj2 != null) && (inParentObj != null))
                {
                    obj2.CustomSetActive(inParentObj.activeSelf);
                    RectTransform transform = obj2.transform as RectTransform;
                    RectTransform transform2 = inParentObj.transform as RectTransform;
                    transform.localScale = transform2.localScale;
                    transform.pivot = transform2.pivot;
                    transform.sizeDelta = transform2.sizeDelta;
                    LayoutElement component = transform2.GetComponent<LayoutElement>();
                    if ((component != null) && (transform.sizeDelta == Vector2.zero))
                    {
                        transform.sizeDelta = new Vector2(component.preferredWidth, component.preferredHeight);
                    }
                    transform.position = transform2.position;
                    Vector2 screenPoint = CUIUtility.WorldToScreenPoint(ms_originalForm.GetCamera(), transform2.position);
                    Vector3 vector2 = CUIUtility.ScreenToWorldPoint(NewbieGuideScriptControl.FormGuideMask.GetCamera(), screenPoint, transform.position.z);
                    if (i < ms_guideTextList.Count)
                    {
                        NewbieGuideSpecialTipConf specialTipConfig = Singleton<NewbieGuideDataManager>.GetInstance().GetSpecialTipConfig(this.currentConf.wSpecialTip);
                        if ((specialTipConfig != null) && (specialTipConfig.bGuideTextPos > 0))
                        {
                            ms_guideTextList[i].CustomSetActive(obj2.activeSelf);
                            UpdateGuideTextPos(specialTipConfig, inParentObj, formGuideMask, ms_originalForm, ms_guideTextList[i]);
                        }
                    }
                }
            }
        }
    }

    public static void UpdateGuideTextPos(NewbieGuideSpecialTipConf tipConf, GameObject inParentObj, CUIFormScript inGuideForm, CUIFormScript inOriginalForm, GameObject rootPanel)
    {
        if (((tipConf != null) && (inParentObj != null)) && ((inGuideForm != null) && (inOriginalForm != null)))
        {
            CUIFormScript script = inGuideForm;
            RectTransform transform = (RectTransform) rootPanel.transform.FindChild("RightSpecial").transform;
            RectTransform transform2 = (RectTransform) rootPanel.transform;
            Vector3 position = inParentObj.transform.position;
            Vector2 vector2 = CUIUtility.WorldToScreenPoint(inOriginalForm.GetCamera(), position);
            float x = vector2.x;
            float y = vector2.y;
            float num3 = 142f;
            float num4 = 85f;
            Vector2 vector3 = new Vector2(0f, 0f);
            switch (tipConf.bSpecialTipPos)
            {
                case 0:
                    vector3 = new Vector2(-num3, num4);
                    break;

                case 1:
                    vector3 = new Vector2(-num3, -num4);
                    break;

                case 2:
                    vector3 = new Vector2(num3, num4);
                    break;

                case 3:
                    vector3 = new Vector2(num3, -num4);
                    break;
            }
            if (tipConf.iOffsetX != 0)
            {
                vector3.x += tipConf.iOffsetX;
            }
            if (tipConf.iOffsetY != 0)
            {
                vector3.y += tipConf.iOffsetY;
            }
            float num5 = Mathf.Max(transform2.rect.width, transform.rect.width) / 2f;
            num5 = script.ChangeFormValueToScreen(num5) + 3f;
            float num6 = Mathf.Max(transform2.rect.height, transform.rect.height) / 2f;
            num6 = script.ChangeFormValueToScreen(num6) + 3f;
            x += vector3.x;
            y += vector3.y;
            if (x < num5)
            {
                x = num5;
            }
            else if ((x + num5) > Screen.width)
            {
                x = Screen.width - num5;
            }
            if (y < num6)
            {
                y = num6;
            }
            else if ((y + num6) > Screen.height)
            {
                y = Screen.height - num6;
            }
            x = script.ChangeScreenValueToForm(x);
            y = script.ChangeScreenValueToForm(y);
            transform2.anchoredPosition = new Vector2(x, y);
        }
    }

    protected void Warning(string value)
    {
        if (value != null)
        {
        }
    }

    public float clickGOEnableWaitTime { get; private set; }

    public NewbieGuideScriptConf currentConf { get; protected set; }

    public float currentInitializeWaitTime { get; private set; }

    public ListView<HightLightItem> highLightGameObjectList { get; protected set; }

    public bool isClickGOEnableError { get; private set; }

    public bool isDisableFinger { get; protected set; }

    public bool isError { get; private set; }

    public bool isGuiding { get; protected set; }

    public bool isInitialize { get; protected set; }

    protected string logTitle
    {
        get
        {
            return ("[<color=cyan>新手引导</color>][<color=green>" + this.currentConf.dwID + "</color>]");
        }
    }

    public float timeScale { get; protected set; }

    private enum ECompleteType
    {
        ClickButton,
        ClickAnyWhere,
        WaitOneWhile
    }

    public enum eFlipType
    {
        FlipNone,
        FlipX,
        FlipY,
        FlipXY
    }

    public enum EOpacityLadder
    {
        Standard,
        Transparent,
        Darker,
        Count
    }

    public class HightLightItem
    {
        public Camera camera;
        public int depth;
        public GameObject go;
        public bool hasPanel;
        public bool isUI;
        public int layer;
        public DictionaryObjectView<GameObject, int> layerDic;
    }

    public delegate void NewbieGuideBaseScriptDelegate();
}

