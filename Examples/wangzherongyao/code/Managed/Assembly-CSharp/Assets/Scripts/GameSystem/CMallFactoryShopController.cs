﻿namespace Assets.Scripts.GameSystem
{
    using Assets.Scripts.Framework;
    using Assets.Scripts.UI;
    using CSProtocol;
    using ResData;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    [MessageHandlerClass]
    public class CMallFactoryShopController : Singleton<CMallFactoryShopController>
    {
        private bool _inited;
        private GameObject _root;
        private DictionaryView<uint, ShopProduct> _spDict;
        private ListView<ShopProduct> _spList;
        private ListView<ShopProductWidget> _widgetList;
        [CompilerGenerated]
        private static Comparison<ShopProduct> <>f__am$cache5;

        public void BuyShopProduct(ShopProduct shopProduct, uint count, bool needConfirm, CUIEvent uiEvent = null)
        {
            CUseable useable = CUseableManager.CreateUseable(shopProduct.Type, shopProduct.ID, 0);
            enPayType payType = CMallSystem.ResBuyTypeToPayType((int) shopProduct.CoinType);
            uint payValue = ((useable.GetBuyPrice(shopProduct.CoinType) * count) * shopProduct.Discount) / 100;
            if (uiEvent == null)
            {
                uiEvent = Singleton<CUIEventManager>.GetInstance().GetUIEvent();
                uiEvent.m_eventID = enUIEventID.Mall_Product_Confirm_Buy;
                uiEvent.m_eventParams.tag = (int) shopProduct.Key;
                uiEvent.m_eventParams.commonUInt32Param1 = count;
            }
            else
            {
                uiEvent.m_eventParams.commonUInt32Param1 = count;
            }
            CMallSystem.TryToPay(enPayPurpose.Buy, string.Format("{0}{1}", useable.m_name, (count <= 1) ? string.Empty : ("x" + count)), payType, payValue, uiEvent.m_eventID, ref uiEvent.m_eventParams, enUIEventID.None, needConfirm, true);
        }

        public void Clear()
        {
            if ((this._spDict != null) && (this._spDict.Count != 0))
            {
                this._spDict.Clear();
                this._spDict = null;
            }
            if ((this._spList != null) && (this._spList.Count != 0))
            {
                this._spList.Clear();
                this._spList = null;
            }
            this._inited = false;
        }

        public void Close()
        {
            if (null != this._root)
            {
                Singleton<CUIEventManager>.GetInstance().RemoveUIEventListener(enUIEventID.Mall_Product_Buy, new CUIEventManager.OnUIEventHandler(this.OnClickBuy));
                Singleton<CUIEventManager>.GetInstance().RemoveUIEventListener(enUIEventID.Mall_Product_Confirm_Buy, new CUIEventManager.OnUIEventHandler(this.OnClickConfirmBuy));
                Singleton<EventRouter>.GetInstance().RemoveEventHandler(EventID.Mall_Change_Tab, new Action(this, (IntPtr) this.Close));
                Singleton<EventRouter>.GetInstance().RemoveEventHandler(EventID.Mall_Close_Mall, new Action(this, (IntPtr) this.Close));
                for (int i = 0; i < this._widgetList.Count; i++)
                {
                    this._widgetList[i].Clear();
                }
                this._widgetList = null;
                this._root = null;
            }
        }

        public void Draw(CUIFormScript form)
        {
            this.Close();
            this.InitData();
            this.Open(form);
        }

        public ShopProduct GetProduct(uint key)
        {
            this.InitData();
            if ((key != 0) && this._spDict.ContainsKey(key))
            {
                return this._spDict[key];
            }
            return null;
        }

        public int GetProductIndex(ShopProduct product)
        {
            if (this._spList != null)
            {
                int count = this._spList.Count;
                for (int i = 0; i < count; i++)
                {
                    if (this._spList[i].Key == product.Key)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public ListView<ShopProduct> GetProducts()
        {
            this.InitData();
            return this._spList;
        }

        public void InitData()
        {
            if (!this._inited)
            {
                this._inited = true;
                this._spDict = new DictionaryView<uint, ShopProduct>();
                this._spList = new ListView<ShopProduct>();
                DictionaryView<uint, ResSpecSale>.Enumerator enumerator = GameDataMgr.specSaleDict.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    KeyValuePair<uint, ResSpecSale> current = enumerator.Current;
                    ResSpecSale config = current.Value;
                    if (config != null)
                    {
                        CUseable useable = CUseableManager.CreateUseable((COM_ITEM_TYPE) config.dwSpecSaleType, config.dwSpecSaleId, 0);
                        if ((useable != null) && (useable.m_baseID != 0))
                        {
                            ShopProduct item = new ShopProduct(config);
                            if (item.IsOnSale)
                            {
                                this._spList.Add(item);
                                this._spDict.Add(item.Key, item);
                            }
                        }
                    }
                }
                if (<>f__am$cache5 == null)
                {
                    <>f__am$cache5 = (l, r) => r.RecommendIndex - l.RecommendIndex;
                }
                this._spList.Sort(<>f__am$cache5);
            }
        }

        public void Load(CUIFormScript form)
        {
            Singleton<CMallSystem>.GetInstance().LoadUIPrefab("UGUI/Form/System/Mall/FactoryShop", "pnlFactoryShop", form.GetWidget(3), form);
        }

        public bool Loaded(CUIFormScript form)
        {
            if (Utility.FindChild(form.GetWidget(3), "pnlFactoryShop") == null)
            {
                return false;
            }
            return true;
        }

        private void OnBuyReturn(CSPkg msg)
        {
            SCPKG_CMD_SPECSALEBUY stSpecSaleBuyRsp = msg.stPkgData.stSpecSaleBuyRsp;
            if (stSpecSaleBuyRsp.iErrCode == 0)
            {
                CSDT_SPECSALEBUYINFO stSpecSaleBuyInfo = stSpecSaleBuyRsp.stSpecSaleBuyInfo;
                if (this._spDict.ContainsKey(stSpecSaleBuyInfo.dwId))
                {
                    ShopProduct shopProduct = this._spDict[stSpecSaleBuyInfo.dwId];
                    shopProduct.BoughtCount += stSpecSaleBuyInfo.dwNum;
                    if ((shopProduct.Type != COM_ITEM_TYPE.COM_OBJTYPE_HERO) && (((shopProduct.Type != COM_ITEM_TYPE.COM_OBJTYPE_ITEMPROP) || !shopProduct.IsPropGift) || !shopProduct.IsPropGiftUseImm))
                    {
                        CUseable useable = CUseableManager.CreateUseable(shopProduct.Type, shopProduct.ID, (int) stSpecSaleBuyInfo.dwNum);
                        CUseable[] items = new CUseable[] { useable };
                        Singleton<CUIManager>.GetInstance().OpenAwardTip(items, Singleton<CTextManager>.GetInstance().GetText("Buy_Ok"), false, enUIEventID.None, false, false, "Form_Award");
                    }
                    if ((Singleton<CMallSystem>.GetInstance().CurTab == CMallSystem.Tab.Factory_Shop) && !shopProduct.IsOnSale)
                    {
                        this.Remove(shopProduct);
                        this.Draw(Singleton<CMallSystem>.GetInstance().m_MallForm);
                    }
                    else
                    {
                        if (!shopProduct.IsOnSale)
                        {
                            this.Remove(shopProduct);
                        }
                        Singleton<EventRouter>.GetInstance().BroadCastEvent<ShopProduct>(EventID.Mall_Factory_Shop_Product_Bought_Success, shopProduct);
                    }
                }
            }
            else
            {
                Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.GetInstance().GetText("buySpecSaleFailed") + stSpecSaleBuyRsp.iErrCode, false);
            }
        }

        [MessageHandler(0x482)]
        public static void OnBuySpecSaleRsp(CSPkg msg)
        {
            Singleton<CUIManager>.GetInstance().CloseSendMsgAlert();
            Singleton<CMallFactoryShopController>.GetInstance().OnBuyReturn(msg);
        }

        private void OnClickBuy(CUIEvent uiEvent)
        {
            if (uiEvent.m_srcWidgetIndexInBelongedList >= this._spList.Count)
            {
                Singleton<CUIManager>.GetInstance().OpenMessageBox("internal error: out of index range.", false);
            }
            else
            {
                this.StartShopProduct(this._spList[uiEvent.m_srcWidgetIndexInBelongedList]);
            }
        }

        private void OnClickConfirmBuy(CUIEvent uiEvent)
        {
            uint tag = (uint) uiEvent.m_eventParams.tag;
            uint count = uiEvent.m_eventParams.commonUInt32Param1;
            if (tag != 0)
            {
                ShopProduct product = null;
                if (this._spDict.TryGetValue(tag, out product))
                {
                    this.RequestBuy(product, count);
                }
            }
        }

        [MessageHandler(0x480)]
        public static void OnInitialSpecSaleInfo(CSPkg msg)
        {
            Singleton<CMallFactoryShopController>.GetInstance().UpdateInfo(msg);
        }

        public void Open(CUIFormScript form)
        {
            if (null == this._root)
            {
                this._root = form.transform.Find("pnlBodyBg/pnlFactoryShop").gameObject;
                DebugHelper.Assert(this._root != null);
                if (this._root != null)
                {
                    this._root.CustomSetActive(true);
                    this._widgetList = new ListView<ShopProductWidget>();
                    CUIListScript componetInChild = Utility.GetComponetInChild<CUIListScript>(this._root, "List");
                    if (componetInChild != null)
                    {
                        componetInChild.SetElementAmount(this._spList.Count);
                        for (int i = 0; i < this._spList.Count; i++)
                        {
                            CUIListElementScript elemenet = componetInChild.GetElemenet(i);
                            if ((elemenet != null) && (elemenet.gameObject != null))
                            {
                                ShopProductWidget item = new ShopProductWidget(elemenet.gameObject, this._spList[i]);
                                this._widgetList.Add(item);
                            }
                        }
                    }
                    Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Mall_Product_Buy, new CUIEventManager.OnUIEventHandler(this.OnClickBuy));
                    Singleton<CUIEventManager>.GetInstance().AddUIEventListener(enUIEventID.Mall_Product_Confirm_Buy, new CUIEventManager.OnUIEventHandler(this.OnClickConfirmBuy));
                    Singleton<EventRouter>.GetInstance().AddEventHandler(EventID.Mall_Change_Tab, new Action(this, (IntPtr) this.Close));
                    Singleton<EventRouter>.GetInstance().AddEventHandler(EventID.Mall_Close_Mall, new Action(this, (IntPtr) this.Close));
                    Singleton<EventRouter>.GetInstance().RemoveEventHandler(EventID.GLOBAL_REFRESH_TIME, new Action(this, (IntPtr) this.ResetLimitBuyDaily));
                    Singleton<EventRouter>.GetInstance().AddEventHandler(EventID.GLOBAL_REFRESH_TIME, new Action(this, (IntPtr) this.ResetLimitBuyDaily));
                    Singleton<EventRouter>.GetInstance().BroadCastEvent(EventID.Mall_Sub_Module_Loaded);
                }
            }
        }

        public void Remove(ShopProduct shopProduct)
        {
            if (shopProduct != null)
            {
                int count = this._spList.Count;
                for (int i = 0; i < count; i++)
                {
                    if (this._spList[i].Key == shopProduct.Key)
                    {
                        this._spList.RemoveAt(i);
                        break;
                    }
                }
                this._spDict.Remove(shopProduct.Key);
                Singleton<EventRouter>.GetInstance().BroadCastEvent<ShopProduct>(EventID.Mall_Factory_Shop_Product_Off_Sale, shopProduct);
            }
        }

        public void RequestBuy(ShopProduct shopProduct, uint count)
        {
            ShopProduct product = shopProduct;
            CSPkg msg = NetworkModule.CreateDefaultCSPKG(0x481);
            CSPKG_CMD_SPECSALEBUY stSpecSaleBuyReq = msg.stPkgData.stSpecSaleBuyReq;
            stSpecSaleBuyReq.stSpecSaleBuyInfo.dwId = product.Key;
            stSpecSaleBuyReq.stSpecSaleBuyInfo.dwNum = count;
            Singleton<NetworkModule>.GetInstance().SendLobbyMsg(ref msg, true);
        }

        private void ResetLimitBuyDaily()
        {
            for (int i = 0; i < this._spList.Count; i++)
            {
                ShopProduct product = this._spList[i];
                if ((product.IsOnSale && (product.LimitCount > 0)) && (product.LimitCycle == 1))
                {
                    product.BoughtCount = 0;
                }
            }
        }

        public void StartShopProduct(ShopProduct theSp)
        {
            if ((theSp != null) && theSp.CanBuy())
            {
                if ((theSp.Type == COM_ITEM_TYPE.COM_OBJTYPE_HERO) || (theSp.Type == COM_ITEM_TYPE.COM_OBJTYPE_HEROSKIN))
                {
                    this.BuyShopProduct(theSp, 1, false, null);
                }
                else
                {
                    CUseable useableByBaseID = Singleton<CRoleInfoManager>.GetInstance().GetMasterRoleInfo().GetUseableContainer(enCONTAINER_TYPE.ITEM).GetUseableByBaseID(theSp.Type, theSp.ID);
                    uint maxCount = 0;
                    if (theSp.LimitCount > 0)
                    {
                        maxCount = theSp.LimitCount - theSp.BoughtCount;
                    }
                    if (useableByBaseID != null)
                    {
                        uint num2 = (uint) (useableByBaseID.m_stackMax - useableByBaseID.m_stackCount);
                        if ((num2 < maxCount) || (maxCount == 0))
                        {
                            maxCount = num2;
                        }
                    }
                    BuyPickDialog.Show(theSp.Type, theSp.ID, theSp.CoinType, theSp.Discount, maxCount, new BuyPickDialog.OnConfirmBuyDelegate(this.BuyShopProduct), theSp, null, null);
                }
            }
        }

        public void UpdateInfo(CSPkg msg)
        {
            this.InitData();
            SCPKG_CMD_SPECIAL_SALEINFO stSPecialSaleDetail = msg.stPkgData.stSPecialSaleDetail;
            if (stSPecialSaleDetail.stSpecialSaleInfo.bSpecSaleCnt == 0)
            {
                this.ResetLimitBuyDaily();
            }
            else
            {
                for (int i = 0; i < stSPecialSaleDetail.stSpecialSaleInfo.bSpecSaleCnt; i++)
                {
                    COMDT_SPECSALE comdt_specsale = stSPecialSaleDetail.stSpecialSaleInfo.astSpecSaleDetail[i];
                    if (this._spDict.ContainsKey(comdt_specsale.dwId))
                    {
                        this._spDict[comdt_specsale.dwId].BoughtCount = comdt_specsale.dwNum;
                        ShopProduct product = this._spDict[comdt_specsale.dwId];
                        if (!this._spDict[comdt_specsale.dwId].IsOnSale)
                        {
                            this.Remove(this._spDict[comdt_specsale.dwId]);
                        }
                    }
                }
            }
        }

        public class ShopProduct
        {
            private uint _boughtCount;
            private ResSpecSale _config;
            private bool _isPropGift;
            private COM_PROP_GIFT_USE_TYPE _propGiftUseType;
            private bool propGiftSet;

            public event StateChangeDelegate OnStateChange;

            public ShopProduct(ResSpecSale config)
            {
                this._config = config;
            }

            public bool CanBuy()
            {
                if ((this.LimitCount > 0) && (this.BoughtCount >= this.LimitCount))
                {
                    Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.GetInstance().GetText("specProdOutLimit"), false);
                    return false;
                }
                if (!this.IsOnSale)
                {
                    Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.GetInstance().GetText("specProdOffSale"), false);
                    return false;
                }
                if ((this.Type == COM_ITEM_TYPE.COM_OBJTYPE_HERO) || (this.Type == COM_ITEM_TYPE.COM_OBJTYPE_HEROSKIN))
                {
                    CRoleInfo masterRoleInfo = Singleton<CRoleInfoManager>.GetInstance().GetMasterRoleInfo();
                    if (this.Type == COM_ITEM_TYPE.COM_OBJTYPE_HERO)
                    {
                        CHeroInfo info2;
                        if (masterRoleInfo.GetHeroInfo(this.ID, out info2, false))
                        {
                            Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.GetInstance().GetText("prodOutOfStackLimit"), false);
                            return false;
                        }
                    }
                    else if (this.Type == COM_ITEM_TYPE.COM_OBJTYPE_HEROSKIN)
                    {
                        CHeroSkin skin = (CHeroSkin) CUseableManager.CreateUseable(this.Type, this.ID, 0);
                        if (masterRoleInfo.IsHaveHeroSkin(skin.m_heroId, skin.m_skinId, false))
                        {
                            Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.GetInstance().GetText("prodOutOfStackLimit"), false);
                            return false;
                        }
                    }
                }
                else if (this.Type == COM_ITEM_TYPE.COM_OBJTYPE_ITEMPROP)
                {
                    CRoleInfo info3 = Singleton<CRoleInfoManager>.GetInstance().GetMasterRoleInfo();
                    int useableStackCount = info3.GetUseableContainer(enCONTAINER_TYPE.ITEM).GetUseableStackCount(this.Type, this.ID);
                    CUseable useable = CUseableManager.CreateUseable(this.Type, this.ID, 1);
                    if (useableStackCount >= useable.m_stackMax)
                    {
                        Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.GetInstance().GetText("prodOutOfStackLimit"), false);
                        return false;
                    }
                    if (CItem.IsHeroExperienceCard(this.ID) && info3.IsHaveHero(CItem.GetExperienceCardHeroOrSkinId(this.ID), false))
                    {
                        Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.GetInstance().GetText("ExpCard_AlreadyHaveHero"), false);
                        return false;
                    }
                    if (CItem.IsSkinExperienceCard(this.ID) && info3.IsHaveHeroSkin(CItem.GetExperienceCardHeroOrSkinId(this.ID), false))
                    {
                        Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.GetInstance().GetText("ExpCard_AlreadyHaveSkin"), false);
                        return false;
                    }
                }
                else
                {
                    CUseable useableByBaseID = Singleton<CRoleInfoManager>.GetInstance().GetMasterRoleInfo().GetUseableContainer(enCONTAINER_TYPE.ITEM).GetUseableByBaseID(this.Type, this.ID);
                    if ((useableByBaseID != null) && (useableByBaseID.m_stackCount >= useableByBaseID.m_stackMax))
                    {
                        Singleton<CUIManager>.GetInstance().OpenMessageBox(Singleton<CTextManager>.GetInstance().GetText("prodOutOfStackLimit"), false);
                        return false;
                    }
                }
                return true;
            }

            public uint BoughtCount
            {
                get
                {
                    return this._boughtCount;
                }
                set
                {
                    if (value != this._boughtCount)
                    {
                        this._boughtCount = value;
                        if (this.OnStateChange != null)
                        {
                            this.OnStateChange(this);
                        }
                    }
                }
            }

            public RES_SHOPBUY_COINTYPE CoinType
            {
                get
                {
                    return (RES_SHOPBUY_COINTYPE) this._config.bCostType;
                }
            }

            public uint Discount
            {
                get
                {
                    return this._config.dwDiscount;
                }
            }

            public uint DiscountForShow
            {
                get
                {
                    return this._config.dwDiscountForDisplay;
                }
            }

            public long DoubleKey
            {
                get
                {
                    return GameDataMgr.GetDoubleKey(this._config.dwSpecSaleType, this._config.dwSpecSaleId);
                }
            }

            public uint ID
            {
                get
                {
                    return this._config.dwSpecSaleId;
                }
            }

            public bool IsOnSale
            {
                get
                {
                    bool flag = false;
                    CRoleInfo masterRoleInfo = Singleton<CRoleInfoManager>.instance.GetMasterRoleInfo();
                    if (masterRoleInfo == null)
                    {
                        return false;
                    }
                    DateTime time = Utility.ToUtcTime2Local((long) masterRoleInfo.getCurrentTimeSinceLogin());
                    flag = (time.CompareTo(this.OnSaleTime) >= 0) && (time.CompareTo(this.OffSaleTime) <= 0);
                    if (((this.LimitCount > 0) && (this.LimitCycle <= 0)) && (this.BoughtCount == this.LimitCount))
                    {
                        return false;
                    }
                    return flag;
                }
            }

            public bool IsPropGift
            {
                get
                {
                    if (!this.propGiftSet)
                    {
                        if (this._config.dwSpecSaleType == 2)
                        {
                            ResPropInfo dataByKey = GameDataMgr.itemDatabin.GetDataByKey(this.ID);
                            if (dataByKey.bType == 4)
                            {
                                this._isPropGift = true;
                                if (((int) dataByKey.EftParam[1]) == 1)
                                {
                                    this._propGiftUseType = COM_PROP_GIFT_USE_TYPE.COM_PROP_GIFT_USE_TYPE_IMMI;
                                }
                                else
                                {
                                    this._propGiftUseType = COM_PROP_GIFT_USE_TYPE.COM_PROP_GIFT_USE_TYPE_DELAY;
                                }
                            }
                            else
                            {
                                this._isPropGift = false;
                            }
                        }
                        else
                        {
                            this._isPropGift = false;
                        }
                        this.propGiftSet = true;
                    }
                    return this._isPropGift;
                }
            }

            public bool IsPropGiftUseImm
            {
                get
                {
                    if (this.propGiftSet)
                    {
                        return (this._propGiftUseType == COM_PROP_GIFT_USE_TYPE.COM_PROP_GIFT_USE_TYPE_IMMI);
                    }
                    return (this.IsPropGift && (this._propGiftUseType == COM_PROP_GIFT_USE_TYPE.COM_PROP_GIFT_USE_TYPE_IMMI));
                }
            }

            public bool IsShowLimitBuy
            {
                get
                {
                    return (this._config.bShowLimitBuy > 0);
                }
            }

            public uint Key
            {
                get
                {
                    return this._config.dwId;
                }
            }

            public int LimitBuyDays
            {
                get
                {
                    CRoleInfo masterRoleInfo = Singleton<CRoleInfoManager>.GetInstance().GetMasterRoleInfo();
                    if (masterRoleInfo == null)
                    {
                        return 0;
                    }
                    DateTime time = Utility.ToUtcTime2Local((long) masterRoleInfo.getCurrentTimeSinceLogin());
                    DateTime time2 = new DateTime(time.Year, time.Month, time.Day, 0, 0, 0, DateTimeKind.Local);
                    if (this.OnSaleTime.CompareTo(time2) >= 0)
                    {
                        return (int) this.OffSaleTime.Subtract(this.OnSaleTime).TotalDays;
                    }
                    if (this.OffSaleTime.CompareTo(time2) >= 0)
                    {
                        return (int) this.OffSaleTime.Subtract(time2).TotalDays;
                    }
                    return -1;
                }
            }

            public uint LimitCount
            {
                get
                {
                    return this._config.dwBuyLimitNum;
                }
            }

            public uint LimitCycle
            {
                get
                {
                    return (this._config.dwPurchaseCycle / 0x15180);
                }
            }

            public DateTime OffSaleTime
            {
                get
                {
                    return Utility.StringToDateTime(Utility.UTF8Convert(this._config.szOffTime), DateTime.MaxValue);
                }
            }

            public DateTime OnSaleTime
            {
                get
                {
                    return Utility.StringToDateTime(Utility.UTF8Convert(this._config.szOnTime), DateTime.MinValue);
                }
            }

            public int RecommendIndex
            {
                get
                {
                    return this._config.iRecommend;
                }
            }

            public byte Tag
            {
                get
                {
                    return this._config.bTag;
                }
            }

            public COM_ITEM_TYPE Type
            {
                get
                {
                    return (COM_ITEM_TYPE) this._config.dwSpecSaleType;
                }
            }

            public delegate void StateChangeDelegate(CMallFactoryShopController.ShopProduct sp);
        }

        private class ShopProductWidget
        {
            public Text coinCost;
            public Image coinType;
            public CMallFactoryShopController.ShopProduct data;
            public Text desc;
            public Image icon;
            public Image iconFrame;
            public Image iconMark;
            public Text limitCount;
            public Text limitCycle;
            public GameObject limitObj;
            public Text LimitTimeCount;
            public GameObject LimitTimeObj;
            public Text name;
            public GameObject OldPriceObj;
            public Text OldPriceText;
            public GameObject root;
            public Image tagImage;
            public GameObject TagObj;
            public Text tagText;

            public ShopProductWidget(GameObject node, CMallFactoryShopController.ShopProduct spData)
            {
                this.root = node;
                this.data = spData;
                this.icon = Utility.GetComponetInChild<Image>(node, "Icon");
                this.iconFrame = Utility.GetComponetInChild<Image>(node, "IconFrame");
                this.iconMark = Utility.GetComponetInChild<Image>(node, "IconMark");
                this.coinType = Utility.GetComponetInChild<Image>(node, "CoinPL/CoinType");
                this.coinCost = Utility.GetComponetInChild<Text>(node, "CoinPL/CoinCost");
                this.name = Utility.GetComponetInChild<Text>(node, "Name");
                this.desc = Utility.GetComponetInChild<Text>(node, "Desc");
                this.limitObj = Utility.FindChild(node, "Limit");
                this.limitCount = Utility.GetComponetInChild<Text>(this.limitObj, "Count");
                this.limitCycle = Utility.GetComponetInChild<Text>(this.limitObj, "Text");
                this.LimitTimeObj = Utility.FindChild(node, "LimitTime");
                this.LimitTimeCount = Utility.GetComponetInChild<Text>(this.LimitTimeObj, "Count");
                this.TagObj = Utility.FindChild(node, "New");
                this.tagImage = Utility.GetComponetInChild<Image>(node, "New");
                this.tagText = Utility.GetComponetInChild<Text>(node, "New/Text");
                this.OldPriceObj = Utility.FindChild(node, "CoinPL/CoinOldCost");
                this.OldPriceText = Utility.GetComponetInChild<Text>(node, "CoinPL/CoinOldCost");
                this.data.OnStateChange += new CMallFactoryShopController.ShopProduct.StateChangeDelegate(this.OnProductStateChange);
                this.Validate();
            }

            public void Clear()
            {
                this.data.OnStateChange -= new CMallFactoryShopController.ShopProduct.StateChangeDelegate(this.OnProductStateChange);
            }

            private void OnProductStateChange(CMallFactoryShopController.ShopProduct sp)
            {
                this.Validate();
            }

            public void Validate()
            {
                if (!Singleton<CMallSystem>.GetInstance().m_IsMallFormOpen)
                {
                    return;
                }
                if (this.data == null)
                {
                    return;
                }
                CUseable useable = CUseableManager.CreateCoinUseable(this.data.CoinType, 0);
                CUseable useable2 = CUseableManager.CreateUseable(this.data.Type, this.data.ID, 0);
                if ((this.icon != null) && (useable2 != null))
                {
                    this.icon.SetSprite(CUIUtility.GetSpritePrefeb(useable2.GetIconPath(), false, false));
                }
                if ((this.coinType != null) && (useable != null))
                {
                    this.coinType.SetSprite(CUIUtility.GetSpritePrefeb(useable.GetIconPath(), false, false));
                }
                if (((this.coinCost != null) && (useable2 != null)) && (this.data != null))
                {
                    this.coinCost.text = ((useable2.GetBuyPrice(this.data.CoinType) * this.data.Discount) / 100).ToString();
                }
                if ((this.iconFrame != null) && (this.iconMark != null))
                {
                    if (this.data.Type == COM_ITEM_TYPE.COM_OBJTYPE_ITEMPROP)
                    {
                        if (CItem.IsHeroExperienceCard(this.data.ID))
                        {
                            this.iconFrame.gameObject.CustomSetActive(true);
                            this.iconFrame.SetSprite(CUIUtility.GetSpritePrefeb(CExperienceCardSystem.HeroExperienceCardFramePath, false, false));
                            this.iconMark.gameObject.CustomSetActive(true);
                            this.iconMark.SetSprite(CUIUtility.GetSpritePrefeb(CExperienceCardSystem.HeroExperienceCardMarkPath, false, false));
                        }
                        else if (CItem.IsSkinExperienceCard(this.data.ID))
                        {
                            this.iconFrame.gameObject.CustomSetActive(true);
                            this.iconFrame.SetSprite(CUIUtility.GetSpritePrefeb(CExperienceCardSystem.SkinExperienceCardFramePath, false, false));
                            this.iconMark.gameObject.CustomSetActive(true);
                            this.iconMark.SetSprite(CUIUtility.GetSpritePrefeb(CExperienceCardSystem.SkinExperienceCardMarkPath, false, false));
                        }
                        else
                        {
                            this.iconFrame.gameObject.CustomSetActive(false);
                            this.iconMark.gameObject.CustomSetActive(false);
                        }
                    }
                    else
                    {
                        this.iconFrame.gameObject.CustomSetActive(false);
                        this.iconMark.gameObject.CustomSetActive(false);
                    }
                }
                if (((this.OldPriceObj != null) && (useable2 != null)) && (this.data != null))
                {
                    if (this.data.Discount < 100)
                    {
                        this.OldPriceObj.CustomSetActive(true);
                        if (this.OldPriceText != null)
                        {
                            this.OldPriceText.text = useable2.GetBuyPrice(this.data.CoinType).ToString();
                        }
                    }
                    else
                    {
                        this.OldPriceObj.CustomSetActive(false);
                    }
                }
                if ((this.name != null) && (useable2 != null))
                {
                    this.name.text = useable2.m_name;
                }
                if ((this.desc != null) && (useable2 != null))
                {
                    this.desc.text = useable2.m_description;
                }
                if ((this.data != null) && (this.data.LimitCount > 0))
                {
                    DebugHelper.Assert(this.limitCount != null);
                    this.limitObj.CustomSetActive(true);
                    if (this.limitCount != null)
                    {
                        this.limitCount.text = string.Format("{0}/{1}", this.data.BoughtCount, this.data.LimitCount);
                    }
                    if (this.limitCycle != null)
                    {
                        if (this.data.LimitCycle > 0)
                        {
                            switch (this.data.LimitCycle)
                            {
                                case 1:
                                    this.limitCycle.text = "今日限购";
                                    goto Label_044E;

                                case 7:
                                    this.limitCycle.text = "本周限购";
                                    goto Label_044E;
                            }
                            this.limitCycle.text = "今日限购";
                        }
                        else
                        {
                            this.limitCycle.text = "永久限购";
                        }
                    }
                }
                else
                {
                    this.limitObj.CustomSetActive(false);
                }
            Label_044E:
                if (((this.data != null) && this.data.IsShowLimitBuy) && (this.data.LimitBuyDays > 0))
                {
                    DebugHelper.Assert(this.LimitTimeCount != null);
                    if (this.LimitTimeCount != null)
                    {
                        this.LimitTimeCount.text = this.data.LimitBuyDays.ToString();
                    }
                    this.LimitTimeObj.CustomSetActive(true);
                }
                else
                {
                    this.LimitTimeObj.CustomSetActive(false);
                }
                string productTagIconPath = CMallSystem.GetProductTagIconPath(this.data.Tag, false);
                RES_LUCKYDRAW_ITEMTAG tag = (RES_LUCKYDRAW_ITEMTAG) this.data.Tag;
                if (productTagIconPath == null)
                {
                    this.TagObj.CustomSetActive(false);
                }
                else
                {
                    this.TagObj.CustomSetActive(true);
                    this.tagImage.SetSprite(productTagIconPath, Singleton<CMallSystem>.GetInstance().m_MallForm, true, false, false);
                    if (this.tagText != null)
                    {
                        string text = string.Empty;
                        switch (tag)
                        {
                            case RES_LUCKYDRAW_ITEMTAG.RES_LUCKYDRAW_ITEMTAG_UNUSUAL:
                            case RES_LUCKYDRAW_ITEMTAG.RES_LUCKYDRAW_ITEMTAG_DISCOUNT:
                            {
                                float num = ((float) this.data.DiscountForShow) / 10f;
                                if (Math.Abs((float) (num % 1f)) >= float.Epsilon)
                                {
                                    text = string.Format("{0}折", num.ToString("0.0"));
                                    break;
                                }
                                text = string.Format("{0}折", ((int) num).ToString("D"));
                                break;
                            }
                            case RES_LUCKYDRAW_ITEMTAG.RES_LUCKYDRAW_ITEMTAG_NEW:
                                text = Singleton<CTextManager>.GetInstance().GetText("Common_Tag_New");
                                break;

                            case RES_LUCKYDRAW_ITEMTAG.RES_LUCKYDRAW_ITEMTAG_HOT:
                                text = Singleton<CTextManager>.GetInstance().GetText("Common_Tag_Hot");
                                break;

                            default:
                                text = string.Empty;
                                break;
                        }
                        this.tagText.text = text;
                    }
                }
            }
        }
    }
}

