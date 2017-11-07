﻿namespace Assets.Scripts.Framework
{
    using Assets.Scripts.GameSystem;
    using CSProtocol;
    using ResData;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using tsf4g_tdr_csharp;

    [MessageHandlerClass]
    public class GameDataMgr : Singleton<GameDataMgr>, IGameModule
    {
        public static DatabinTable<ResAchievement, uint> achieveDatabin = null;
        public static DatabinTable<ResAcntExpInfo, uint> acntExpDatabin = null;
        public static DatabinTable<ResAcntPvpExpInfo, byte> acntPvpExpDatabin = null;
        public static DatabinTableMulti<ResActivity, uint> activityDatabin = null;
        public static DatabinTable<ResLevelCfgInfo, int> activityLevelDatabin = null;
        public static DatabinTableMulti<ResActorLinesInfo, int> actorLinesDatabin = null;
        public static DatabinTable<ResSkillUnlock, ushort> addedSkiilDatabin = null;
        public static DatabinTable<ResEvaluateStarInfo, uint> addWinLoseCondDatabin = null;
        public static DatabinTable<ResCouponsBuyInfo, uint> androidDianQuanBuyInfo = null;
        public static DatabinTable<ResLevelCfgInfo, int> arenaLevelDatabin = null;
        public static DatabinTable<ResArenaOneDayReward, uint> arenaRewardDatabin = null;
        public static DatabinTable<ResBanHeroConf, long> banHeroBin = null;
        public static DatabinTable<ResBattleDynamicDifficulty, uint> battleDynamicDifficultyDB = null;
        public static DatabinTable<ResBattleDynamicProperty, uint> battleDynamicPropertyDB = null;
        public static DatabinTable<ResBattleParam, uint> battleParam = null;
        public static DictionaryView<uint, ResBoutiqueConf> boutiqueDict = new DictionaryView<uint, ResBoutiqueConf>();
        public static DatabinTable<ResBufDropInfo, uint> bufDropInfoDatabin = null;
        public static DatabinTable<ResBulletCfgInfo, int> bulletDatabin = null;
        public static DatabinTable<ResBurningBuff, int> burnBuffMap = null;
        public static DatabinTable<ResLevelCfgInfo, int> burnMap = null;
        public static DatabinTableMulti<ResBurningReward, uint> burnRewrad = null;
        public static DatabinTable<ResCallMonster, ushort> callMonsterDatabin = null;
        public static DatabinTable<ResClrCD, uint> cdDatabin = null;
        public static DatabinTable<ResChangeName, uint> changeNameDatabin = null;
        public static DatabinTable<ResChapterInfo, uint> chapterInfoDatabin = null;
        public static DatabinTable<CharmLib, uint> charmLib = null;
        public static DatabinTableMulti<ResClashAddition, uint> clashAdditionDB = null;
        public static DatabinTable<ResCommReward, uint> commonRewardDatabin = null;
        public static DatabinTable<ResComposeInfo, uint> composeInfoDatabin = null;
        public static DatabinTable<ResCoinBuyInfo, ushort> coninBuyDatabin = null;
        public static DatabinTable<ResCounterPartLevelInfo, uint> cpLevelDatabin = null;
        public static DatabinTable<ResCreditLevelInfo, uint> creditLevelDatabin = null;
        public static DatabinTable<ResEntertainmentLevelInfo, uint> entertainLevelDatabin = null;
        public static DatabinTable<ResEquipInfo, uint> equipInfoDatabin = null;
        public static DatabinTable<ResEvaluateStarInfo, uint> evaluateCondInfoDatabin = null;
        public static DatabinTable<ResGameTask, uint> gameTaskDatabin = null;
        public static DatabinTable<ResGameTaskGroup, uint> gameTaskGroupDatabin = null;
        public static DatabinTable<ResGearInfo, uint> gearInfoDatabin = null;
        public static DatabinTable<ResGearLvlUp, uint> gearLvlUpDatabin = null;
        public static DatabinTable<ResGlobalInfo, uint> globalInfoDatabin = null;
        public static DatabinTable<ResGuideTipInfo, int> guideTipDatabin = null;
        public static DatabinTable<ResGuildBuilding, byte> guildBuildingDatabin = null;
        public static DatabinTable<ResGuildDonate, byte> guildDonateDatabin = null;
        public static DatabinTable<ResGuildGradeConf, byte> guildGradeDatabin = null;
        public static DatabinTable<ResGuildIcon, uint> guildIconDatabin = null;
        public static DatabinTable<ResGuildLevel, byte> guildLevelDatabin = null;
        public static DatabinTable<ResGuildMisc, uint> guildMiscDatabin = null;
        public static DatabinTable<ResGuildRankReward, int> guildRankRewardDatabin = null;
        public static DatabinTable<ResGuildShopStarIndexConf, uint> guildStarLevel = null;
        public static DictionaryView<uint, ResHeadImage> headImageDict = new DictionaryView<uint, ResHeadImage>();
        public static DatabinTable<ResHeroAdvanceInfo, uint> heroAdvanceDatabin = null;
        public static DatabinTable<ResHeroWakeInfo, long> heroAwakDatabin = null;
        public static DatabinTable<ResHeroBalanceInfo, uint> heroBalanceDatabin = null;
        public static DatabinTable<ResHeroCfgInfo, uint> heroDatabin = null;
        public static DatabinTable<ResHeroEnergyInfo, uint> heroEnergyDatabin = null;
        public static DictionaryView<uint, ResHeroSimpleInfo> heroInfoDict = new DictionaryView<uint, ResHeroSimpleInfo>();
        public static DatabinTable<ResHeroLvlUpInfo, uint> heroLvlUpDatabin = null;
        public static DatabinTableMulti<ResHeroProficiency, byte> heroProficiencyDatabin = null;
        public static DictionaryView<uint, ResHeroPromotion> heroPromotionDict = new DictionaryView<uint, ResHeroPromotion>();
        public static DatabinTableMulti<ResHeroQualityPicInfo, int> heroQualityPicDatabin = null;
        public static DatabinTable<ResHeroSkin, uint> heroSkinDatabin = null;
        public static DatabinTable<ResHeroStarLvlUpInfo, uint> heroStarLvlUpDatabin = null;
        public static DatabinTable<ResHeroSymbolLvl, ushort> heroSymbolLvlDatabin = null;
        public static DictionaryView<ushort, ResHuoYueDuReward> huoyueduDict = new DictionaryView<ushort, ResHuoYueDuReward>();
        public static DatabinTable<ResInBatChannelCfg, byte> inBattleChannelDatabin = null;
        public static DatabinTable<ResInBatMsgHeroActCfg, uint> inBattleHeroActDatabin = null;
        public static DatabinTable<ResInBatMsgCfg, uint> inBattleMsgDatabin = null;
        public static DatabinTable<ResCouponsBuyInfo, uint> iosDianQuanBuyInfo = null;
        public static DatabinTable<ResPropInfo, uint> itemDatabin = null;
        public static DatabinTable<ResLevelCfgInfo, int> levelDatabin = null;
        public static DatabinTable<ResLicenseInfo, uint> licenseDatabin = null;
        public static DatabinTable<ResEquipInBattle, ushort> m_equipInBattleDatabin = null;
        public static DatabinTable<ResRecommendEquipInBattle, ushort> m_recommendEquipInBattleDatabin = null;
        public static DatabinTable<ResTextData, uint> m_selectHeroChatDatabin = null;
        public static DictionaryView<uint, ResLuckyDrawExternReward> mallRouletteExternRewardDict = new DictionaryView<uint, ResLuckyDrawExternReward>();
        public static DictionaryView<enPayType, ResLuckyDrawPrice> mallRoulettePriceDict = new DictionaryView<enPayType, ResLuckyDrawPrice>();
        public static DictionaryView<long, ResLuckyDrawRewardForClient> mallRouletteRewardDict = new DictionaryView<long, ResLuckyDrawRewardForClient>();
        public static DictionaryView<long, ResRewardMatchTimeInfo> matchTimeInfoDict = new DictionaryView<long, ResRewardMatchTimeInfo>();
        public static DatabinTable<ResMiShuInfo, uint> miShuLib = null;
        public static DatabinTable<ResMonsterCfgInfo, long> monsterDatabin = null;
        public static DatabinTable<ResMonsterOrganLevelDynamicInfo, int> monsterOrganLvDynamicInfobin = null;
        public static DictionaryView<uint, ResAkaliShopCtrl> mysteryShopCtlDict = new DictionaryView<uint, ResAkaliShopCtrl>();
        public static DictionaryView<uint, ResAkaliShopGoods> mysteryShopProductDict = new DictionaryView<uint, ResAkaliShopGoods>();
        public static DatabinTable<NewbieGuideMainLineConf, uint> newbieMainLineDatabin = null;
        public static DatabinTableMulti<NewbieGuideScriptConf, uint> newbieScriptDatabin = null;
        public static DatabinTable<NewbieGuideSpecialTipConf, uint> newbieSpecialTipDatabin = null;
        public static DatabinTable<NewbieGuideWeakConf, uint> newbieWeakDatabin = null;
        public static DatabinTable<NewbieWeakGuideMainLineConf, uint> newbieWeakMainLineDataBin = null;
        public static DatabinTable<ResNpcOfArena, uint> npcOfArena = null;
        public static DatabinTable<ResOrganCfgInfo, long> organDatabin = null;
        public static DatabinTable<ResPropertyValueInfo, byte> propertyValInfo = null;
        public static DatabinTable<ResAcntBattleLevelInfo, uint> pvpLevelDatabin = null;
        public static DictionaryView<uint, ResPVPRatio> pvpRatioDict = new DictionaryView<uint, ResPVPRatio>();
        public static DictionaryView<uint, ResPVPSpecItem> pvpSpecialItemDict = new DictionaryView<uint, ResPVPSpecItem>();
        public static DatabinTable<ResRandomSkillPassiveRule, int> randomSkillPassiveDatabin = null;
        public static DatabinTable<ResRandomRewardStore, uint> randowmRewardDB = null;
        public static DatabinTable<ResRankGradeConf, byte> rankGradeDatabin = null;
        public static DatabinTable<ResRankLevelInfo, uint> rankLevelDatabin = null;
        public static DatabinTable<ResRankRewardConf, uint> rankRewardDatabin = null;
        public static DatabinTable<ResRankSeasonConf, uint> rankSeasonDatabin = null;
        public static DictionaryView<uint, ResRandDrawInfo> recommendLotteryCtrlDict = new DictionaryView<uint, ResRandDrawInfo>();
        public static DictionaryView<uint, ResSaleRecommend> recommendProductDict = new DictionaryView<uint, ResSaleRecommend>();
        public static DictionaryView<long, ResRewardPoolInfo> recommendRewardDict = new DictionaryView<long, ResRewardPoolInfo>();
        public static DictionaryView<uint, ResRedDotInfo> redDotInfoDict = new DictionaryView<uint, ResRedDotInfo>();
        public static DatabinTable<ResNobeInfo, byte> resNobeInfoDatabin = null;
        public static DatabinTable<ResShopInfo, uint> resShopInfoDatabin = null;
        public static DatabinTable<ResVIPCoupons, uint> resVipDianQuan = null;
        public static DictionaryView<uint, ResRewardMatchConf> rewardMatchRewardDict = new DictionaryView<uint, ResRewardMatchConf>();
        public static DatabinTable<ResRobotBattleList, uint> robotBattleListInfo = null;
        public static DatabinTable<ResRobotPower, uint> robotHeroInfo = null;
        public static DatabinTable<ResRobotName, uint> robotName = null;
        public static DatabinTable<ResFakeAcntSkill, ushort> robotPlayerSkillDatabin = null;
        public static DatabinTable<ResFakeAcntHero, ushort> robotRookieHeroSkinDatabin = null;
        public static DatabinTable<ResRobotSubNameA, uint> robotSubNameA = null;
        public static DatabinTable<ResRobotSubNameB, uint> robotSubNameB = null;
        public static DatabinTable<ResFakeAcntHero, ushort> robotVeteranHeroSkinDatabin = null;
        public static DatabinTable<ResRuleText, ushort> s_ruleTextDatabin = null;
        public static DatabinTable<ResText, ushort> s_textDatabin = null;
        public static DatabinTable<ResCommonSettle, uint> settleDatabin = null;
        public static DatabinTable<ShenFuInfo, uint> shenfuBin = null;
        public static DatabinTable<ResShopRefreshCost, uint> shopRefreshCostDatabin = null;
        public static DatabinTable<ResShopType, ushort> shopTypeDatabin = null;
        public static DatabinTable<ResSignalInfo, byte> signalDatabin = null;
        public static DatabinTable<ResSkillCombineCfgInfo, int> skillCombineDatabin = null;
        public static DatabinTable<ResSkillCfgInfo, int> skillDatabin = null;
        public static DatabinTable<ResSkillFuncCfgInfo, uint> skillFuncDatabin = null;
        public static DatabinTable<ResSkillLvlUpInfo, uint> skillLvlUpDatabin = null;
        public static DatabinTable<ResSkillMarkCfgInfo, int> skillMarkDatabin = null;
        public static DatabinTable<ResSkillPassiveCfgInfo, int> skillPassiveDatabin = null;
        public static DictionaryView<uint, ResHeroSkinSimple> skinInfoDict = new DictionaryView<uint, ResHeroSkinSimple>();
        public static DictionaryView<uint, ResSkinPromotion> skinPromotionDict = new DictionaryView<uint, ResSkinPromotion>();
        public static DatabinTable<ResSkinQualityPicInfo, byte> skinQualityPicDatabin = null;
        public static DatabinTable<ResSoldierWaveInfo, uint> soldierWaveDatabin = null;
        public static DatabinTable<ResSoulAddition, int> soulAdditionDatabin = null;
        public static DatabinTable<ResSoulExpAllocRule, uint> soulExpAllocDatabin = null;
        public static DatabinTableMulti<ResSoulLvlUpInfo, uint> soulLvlUpDatabin = null;
        public static DatabinTable<ResSoulPropEft, uint> soulPropEftDatabin = null;
        public static DatabinTable<ResHornInfo, uint> speakerDatabin = null;
        public static DatabinTable<ResSpecialFucUnlock, uint> specialFunUnlockDatabin = null;
        public static DictionaryView<uint, ResSpecSale> specSaleDict = new DictionaryView<uint, ResSpecSale>();
        public static DictionaryView<uint, ResBannerImage> svr2BannerImageDict = new DictionaryView<uint, ResBannerImage>();
        public static DictionaryView<uint, ResGlobalInfo> svr2CltCfgDict = new DictionaryView<uint, ResGlobalInfo>();
        public static DatabinTable<ResSymbolComp, uint> symbolCompDatabin = null;
        public static DatabinTable<ResSymbolInfo, uint> symbolInfoDatabin = null;
        public static DatabinTable<ResSymbolPos, byte> symbolPosDatabin = null;
        public static DatabinTable<ResTalentHero, uint> talentHero = null;
        public static DatabinTable<ResTalentLib, uint> talentLib = null;
        public static DatabinTable<ResTalentRule, byte> talentRule = null;
        public static DatabinTable<ResTask, uint> taskDatabin = null;
        public static DatabinTable<ResPrerequisite, uint> taskPrerequisiteDatabin = null;
        public static DatabinTable<ResTaskReward, uint> taskRewardDatabin = null;
        public static DatabinTable<ResTextData, uint> textBubbleDatabin = null;
        public static DatabinTable<TowerHitConf, byte> towerHitDatabin = null;
        public static DatabinTable<ResRewardMatchLevelInfo, uint> uinionBattleLevelDatabin = null;
        public static DatabinTable<ResRewardMatchDetailConf, uint> unionRankRewardDetailDatabin = null;
        public static DatabinTable<ResUnlockCondition, uint> unlockConditionDatabin = null;
        public static DictionaryView<uint, ResFillInPrice> wealCheckFillDict = new DictionaryView<uint, ResFillInPrice>();
        public static DictionaryView<uint, ResWealCheckIn> wealCheckInDict = new DictionaryView<uint, ResWealCheckIn>();
        public static DictionaryView<uint, ResWealCondition> wealConditionDict = new DictionaryView<uint, ResWealCondition>();
        public static DictionaryView<uint, ResCltWealExchange> wealExchangeDict = new DictionaryView<uint, ResCltWealExchange>();
        public static DictionaryView<uint, ResWealFixedTime> wealFixtimeDict = new DictionaryView<uint, ResWealFixedTime>();
        public static DictionaryView<uint, ResWealMultiple> wealMultipleDict = new DictionaryView<uint, ResWealMultiple>();
        public static DictionaryView<uint, ResWealText> wealNoticeDict = new DictionaryView<uint, ResWealText>();
        public static DictionaryView<uint, ResRewardForWeal> wealRewardDict = new DictionaryView<uint, ResRewardForWeal>();

        public static void ClearServerResData()
        {
            wealCheckInDict.Clear();
            wealFixtimeDict.Clear();
            wealMultipleDict.Clear();
            wealExchangeDict.Clear();
            wealConditionDict.Clear();
            wealNoticeDict.Clear();
            wealRewardDict.Clear();
            wealCheckFillDict.Clear();
            pvpRatioDict.Clear();
            specSaleDict.Clear();
            heroPromotionDict.Clear();
            skinPromotionDict.Clear();
            mallRoulettePriceDict.Clear();
            mallRouletteRewardDict.Clear();
            mallRouletteExternRewardDict.Clear();
            mysteryShopCtlDict.Clear();
            mysteryShopProductDict.Clear();
            redDotInfoDict.Clear();
            heroInfoDict.Clear();
            skinInfoDict.Clear();
            recommendProductDict.Clear();
            recommendLotteryCtrlDict.Clear();
            recommendRewardDict.Clear();
            rewardMatchRewardDict.Clear();
            boutiqueDict.Clear();
            headImageDict.Clear();
            svr2CltCfgDict.Clear();
            svr2BannerImageDict.Clear();
            MonoSingleton<BannerImageSys>.GetInstance().ClearSeverData();
        }

        public static long GetDoubleKey(uint key1, uint key2)
        {
            ulong num = Convert.ToUInt64(key1) << 0x20;
            return (long) (num + key2);
        }

        public override void Init()
        {
        }

        [DebuggerHidden]
        public IEnumerator LoadDataBin()
        {
            return new <LoadDataBin>c__Iterator4();
        }

        [MessageHandler(0x9ce)]
        public static void OnServerResDataNtf(CSPkg msg)
        {
            TdrReadBuf srcBuf = new TdrReadBuf(ref msg.stPkgData.stResDataNtf.szResData, (int) msg.stPkgData.stResDataNtf.dwDataLen);
            byte dest = 0;
            ushort num2 = 0;
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            bool flag4 = false;
            bool flag5 = false;
            bool flag6 = false;
            while (srcBuf.readUInt8(ref dest) == TdrError.ErrorType.TDR_NO_ERROR)
            {
                ResHuoYueDuReward reward2;
                CS_RES_DATA_TYPE cs_res_data_type = (CS_RES_DATA_TYPE) dest;
                if (srcBuf.readUInt16(ref num2) != TdrError.ErrorType.TDR_NO_ERROR)
                {
                    return;
                }
                switch (cs_res_data_type)
                {
                    case CS_RES_DATA_TYPE.CS_RES_DATA_WEAL_CHECKIN:
                    {
                        ResWealCheckIn @in = new ResWealCheckIn();
                        if (@in.unpack(ref srcBuf, 0) != TdrError.ErrorType.TDR_NO_ERROR)
                        {
                            return;
                        }
                        if (!wealCheckInDict.ContainsKey(@in.dwID))
                        {
                            wealCheckInDict.Add(@in.dwID, @in);
                        }
                        continue;
                    }
                    case CS_RES_DATA_TYPE.CS_RES_DATA_WEAL_FILLINPRICE:
                    {
                        ResFillInPrice price = new ResFillInPrice();
                        if (price.unpack(ref srcBuf, 0) != TdrError.ErrorType.TDR_NO_ERROR)
                        {
                            return;
                        }
                        if (!wealCheckFillDict.ContainsKey(price.dwID))
                        {
                            wealCheckFillDict.Add(price.dwID, price);
                        }
                        continue;
                    }
                    case CS_RES_DATA_TYPE.CS_RES_DATA_WEAL_FIXEDTIME:
                    {
                        ResWealFixedTime time = new ResWealFixedTime();
                        if (time.unpack(ref srcBuf, 0) != TdrError.ErrorType.TDR_NO_ERROR)
                        {
                            return;
                        }
                        if (!wealFixtimeDict.ContainsKey(time.dwID))
                        {
                            wealFixtimeDict.Add(time.dwID, time);
                        }
                        continue;
                    }
                    case CS_RES_DATA_TYPE.CS_RES_DATA_WEAL_MULTIPLE:
                    {
                        ResWealMultiple multiple = new ResWealMultiple();
                        if (multiple.unpack(ref srcBuf, 0) != TdrError.ErrorType.TDR_NO_ERROR)
                        {
                            return;
                        }
                        if (!wealMultipleDict.ContainsKey(multiple.dwID))
                        {
                            wealMultipleDict.Add(multiple.dwID, multiple);
                        }
                        continue;
                    }
                    case CS_RES_DATA_TYPE.CS_RES_DATA_WEAL_CONDITION:
                    {
                        ResWealCondition condition = new ResWealCondition();
                        if (condition.unpack(ref srcBuf, 0) != TdrError.ErrorType.TDR_NO_ERROR)
                        {
                            return;
                        }
                        if (!wealConditionDict.ContainsKey(condition.dwID))
                        {
                            wealConditionDict.Add(condition.dwID, condition);
                        }
                        continue;
                    }
                    case CS_RES_DATA_TYPE.CS_RES_DATA_WEAL_TEXT:
                    {
                        ResWealText text = new ResWealText();
                        if (text.unpack(ref srcBuf, 0) != TdrError.ErrorType.TDR_NO_ERROR)
                        {
                            return;
                        }
                        if (!wealNoticeDict.ContainsKey(text.dwID))
                        {
                            wealNoticeDict.Add(text.dwID, text);
                        }
                        continue;
                    }
                    case CS_RES_DATA_TYPE.CS_RES_DATA_WEAL_REWARD:
                    {
                        ResRewardForWeal weal = new ResRewardForWeal();
                        if (weal.unpack(ref srcBuf, 0) != TdrError.ErrorType.TDR_NO_ERROR)
                        {
                            return;
                        }
                        if (!wealRewardDict.ContainsKey(weal.dwRewardID))
                        {
                            wealRewardDict.Add(weal.dwRewardID, weal);
                        }
                        continue;
                    }
                    case CS_RES_DATA_TYPE.CS_RES_DATA_SPEC_SALE:
                    {
                        ResSpecSale sale = new ResSpecSale();
                        if (sale.unpack(ref srcBuf, 0) != TdrError.ErrorType.TDR_NO_ERROR)
                        {
                            return;
                        }
                        if (!specSaleDict.ContainsKey(sale.dwId))
                        {
                            specSaleDict.Add(sale.dwId, sale);
                        }
                        continue;
                    }
                    case CS_RES_DATA_TYPE.CS_RES_DATA_HERO_PROMOTION:
                    {
                        ResHeroPromotion promotion = new ResHeroPromotion();
                        if (promotion.unpack(ref srcBuf, 0) != TdrError.ErrorType.TDR_NO_ERROR)
                        {
                            return;
                        }
                        if (!heroPromotionDict.ContainsKey(promotion.dwPromotionID))
                        {
                            heroPromotionDict.Add(promotion.dwPromotionID, promotion);
                        }
                        continue;
                    }
                    case CS_RES_DATA_TYPE.CS_RES_DATA_SKIN_PROMOTION:
                    {
                        ResSkinPromotion promotion2 = new ResSkinPromotion();
                        if (promotion2.unpack(ref srcBuf, 0) != TdrError.ErrorType.TDR_NO_ERROR)
                        {
                            return;
                        }
                        if (!skinPromotionDict.ContainsKey(promotion2.dwPromotionID))
                        {
                            skinPromotionDict.Add(promotion2.dwPromotionID, promotion2);
                        }
                        continue;
                    }
                    case CS_RES_DATA_TYPE.CS_RES_DATA_LUCKYDRAW_PRICE:
                    {
                        ResLuckyDrawPrice price2 = new ResLuckyDrawPrice();
                        if (price2.unpack(ref srcBuf, 0) != TdrError.ErrorType.TDR_NO_ERROR)
                        {
                            return;
                        }
                        enPayType key = CMallSystem.ResBuyTypeToPayType(price2.bMoneyType);
                        if (!mallRoulettePriceDict.ContainsKey(key))
                        {
                            mallRoulettePriceDict.Add(key, price2);
                        }
                        continue;
                    }
                    case CS_RES_DATA_TYPE.CS_RES_DATA_LUCKYDRAW_REWARD:
                    {
                        ResLuckyDrawRewardForClient client = new ResLuckyDrawRewardForClient();
                        if (client.unpack(ref srcBuf, 0) != TdrError.ErrorType.TDR_NO_ERROR)
                        {
                            return;
                        }
                        long doubleKey = GetDoubleKey(client.dwRewardPoolID, client.bRewardIndex);
                        if (!mallRouletteRewardDict.ContainsKey(doubleKey))
                        {
                            mallRouletteRewardDict.Add(doubleKey, client);
                        }
                        continue;
                    }
                    case CS_RES_DATA_TYPE.CS_RES_DATA_LUCKYDRAW_EXTERNREWARD:
                    {
                        ResLuckyDrawExternReward reward = new ResLuckyDrawExternReward();
                        if (reward.unpack(ref srcBuf, 0) != TdrError.ErrorType.TDR_NO_ERROR)
                        {
                            return;
                        }
                        if (!mallRouletteExternRewardDict.ContainsKey(reward.bMoneyType))
                        {
                            mallRouletteExternRewardDict.Add(reward.bMoneyType, reward);
                        }
                        continue;
                    }
                    case CS_RES_DATA_TYPE.CS_RES_DATA_AKALISHOPCTRL:
                    {
                        ResAkaliShopCtrl ctrl = new ResAkaliShopCtrl();
                        if (ctrl.unpack(ref srcBuf, 0) != TdrError.ErrorType.TDR_NO_ERROR)
                        {
                            return;
                        }
                        if (!mysteryShopCtlDict.ContainsKey(ctrl.dwShopID))
                        {
                            mysteryShopCtlDict.Add(ctrl.dwShopID, ctrl);
                        }
                        continue;
                    }
                    case CS_RES_DATA_TYPE.CS_RES_DATA_AKALISHOPGOODS:
                    {
                        ResAkaliShopGoods goods = new ResAkaliShopGoods();
                        if (goods.unpack(ref srcBuf, 0) != TdrError.ErrorType.TDR_NO_ERROR)
                        {
                            return;
                        }
                        if (!mysteryShopProductDict.ContainsKey(goods.dwID))
                        {
                            mysteryShopProductDict.Add(goods.dwID, goods);
                        }
                        flag4 = true;
                        continue;
                    }
                    case CS_RES_DATA_TYPE.CS_RES_DATA_SALE_RECOMMEND:
                    {
                        ResSaleRecommend recommend = new ResSaleRecommend();
                        if (recommend.unpack(ref srcBuf, 0) != TdrError.ErrorType.TDR_NO_ERROR)
                        {
                            return;
                        }
                        if (!recommendProductDict.ContainsKey(recommend.dwID))
                        {
                            recommendProductDict.Add(recommend.dwID, recommend);
                        }
                        continue;
                    }
                    case CS_RES_DATA_TYPE.CS_RES_DATA_RAND_DRAW:
                    {
                        ResRandDrawInfo info5 = new ResRandDrawInfo();
                        if (info5.unpack(ref srcBuf, 0) != TdrError.ErrorType.TDR_NO_ERROR)
                        {
                            return;
                        }
                        if (!recommendLotteryCtrlDict.ContainsKey(info5.dwDrawID))
                        {
                            recommendLotteryCtrlDict.Add(info5.dwDrawID, info5);
                        }
                        continue;
                    }
                    case CS_RES_DATA_TYPE.CS_RES_DATA_REWARDPOOL:
                    {
                        ResRewardPoolInfo info6 = new ResRewardPoolInfo();
                        if (info6.unpack(ref srcBuf, 0) != TdrError.ErrorType.TDR_NO_ERROR)
                        {
                            return;
                        }
                        long num5 = GetDoubleKey(info6.dwPoolID, info6.stRewardInfo.bIndex);
                        if (!recommendRewardDict.ContainsKey(num5))
                        {
                            recommendRewardDict.Add(num5, info6);
                        }
                        continue;
                    }
                    case CS_RES_DATA_TYPE.CS_RES_DATA_REDDOTTIPINFO:
                    {
                        ResRedDotInfo info = new ResRedDotInfo();
                        if (info.unpack(ref srcBuf, 0) != TdrError.ErrorType.TDR_NO_ERROR)
                        {
                            return;
                        }
                        if (!redDotInfoDict.ContainsKey(info.dwIndex))
                        {
                            redDotInfoDict.Add(info.dwIndex, info);
                        }
                        flag4 = true;
                        continue;
                    }
                    case CS_RES_DATA_TYPE.CS_RES_DATA_HEROINFO:
                    {
                        ResHeroSimpleInfo info3 = new ResHeroSimpleInfo();
                        if (info3.unpack(ref srcBuf, 0) != TdrError.ErrorType.TDR_NO_ERROR)
                        {
                            return;
                        }
                        if (!heroInfoDict.ContainsKey(info3.dwCfgID))
                        {
                            heroInfoDict.Add(info3.dwCfgID, info3);
                        }
                        heroDatabin.isAllowUnLoad = false;
                        ResHeroCfgInfo dataByKey = heroDatabin.GetDataByKey(info3.dwCfgID);
                        if (dataByKey != null)
                        {
                            dataByKey.bIsPlayerUse = info3.bIsPlayerUse;
                            dataByKey.dwSortId = info3.dwSortId;
                            dataByKey.bIsBuyCoupons = info3.bIsBuyCoupons;
                            dataByKey.dwBuyCoupons = info3.dwBuyCoupons;
                            dataByKey.bIsBuyCoin = info3.bIsBuyCoin;
                            dataByKey.dwBuyCoin = info3.dwBuyCoin;
                            dataByKey.bIsBuyDiamond = info3.bIsBuyDiamond;
                            dataByKey.dwBuyDiamond = info3.dwBuyDiamond;
                            dataByKey.bIsBuyMixPay = info3.bIsBuyMixPay;
                        }
                        continue;
                    }
                    case CS_RES_DATA_TYPE.CS_RES_DATA_HEROSKININFO:
                    {
                        ResHeroSkinSimple simple = new ResHeroSkinSimple();
                        if (simple.unpack(ref srcBuf, 0) != TdrError.ErrorType.TDR_NO_ERROR)
                        {
                            return;
                        }
                        flag = true;
                        if (!skinInfoDict.ContainsKey(simple.dwID))
                        {
                            skinInfoDict.Add(simple.dwID, simple);
                        }
                        heroSkinDatabin.isAllowUnLoad = false;
                        ResHeroSkin skin = heroSkinDatabin.GetDataByKey(simple.dwID);
                        if (skin != null)
                        {
                            skin.dwSortId = simple.dwSortId;
                            skin.bIsBuyCoupons = simple.bIsBuyCoupons;
                            skin.dwBuyCoupons = simple.dwBuyCoupons;
                            skin.bIsShow = simple.bIsShow;
                        }
                        continue;
                    }
                    case CS_RES_DATA_TYPE.CS_RES_DATA_WEAL_EXCHANGE:
                    {
                        ResCltWealExchange exchange = new ResCltWealExchange();
                        if (exchange.unpack(ref srcBuf, 0) != TdrError.ErrorType.TDR_NO_ERROR)
                        {
                            return;
                        }
                        if (!wealExchangeDict.ContainsKey(exchange.dwID))
                        {
                            wealExchangeDict.Add(exchange.dwID, exchange);
                        }
                        continue;
                    }
                    case CS_RES_DATA_TYPE.CS_RES_DATA_PVP_SPECITEM:
                    {
                        ResPVPSpecItem item = new ResPVPSpecItem();
                        if (item.unpack(ref srcBuf, 0) != TdrError.ErrorType.TDR_NO_ERROR)
                        {
                            return;
                        }
                        if (!pvpSpecialItemDict.ContainsKey(item.dwID))
                        {
                            pvpSpecialItemDict.Add(item.dwID, item);
                        }
                        continue;
                    }
                    case CS_RES_DATA_TYPE.CS_RES_DATA_REWARDMATCH_TIME:
                    {
                        ResRewardMatchTimeInfo info2 = new ResRewardMatchTimeInfo();
                        if (info2.unpack(ref srcBuf, 0) != TdrError.ErrorType.TDR_NO_ERROR)
                        {
                            return;
                        }
                        long num4 = GetDoubleKey(info2.bMapType, info2.dwMapId);
                        if (!matchTimeInfoDict.ContainsKey(num4))
                        {
                            matchTimeInfoDict.Add(num4, info2);
                        }
                        flag5 = true;
                        continue;
                    }
                    case CS_RES_DATA_TYPE.CS_RES_DATA_HUOYUEDU_REWARD:
                        reward2 = new ResHuoYueDuReward();
                        if (reward2.unpack(ref srcBuf, 0) != TdrError.ErrorType.TDR_NO_ERROR)
                        {
                            return;
                        }
                        if (huoyueduDict.ContainsKey(reward2.wID))
                        {
                            break;
                        }
                        huoyueduDict.Add(reward2.wID, reward2);
                        goto Label_09B8;

                    case CS_RES_DATA_TYPE.CS_RES_DATA_BOUTIQUE:
                    {
                        ResBoutiqueConf conf2 = new ResBoutiqueConf();
                        if (conf2.unpack(ref srcBuf, 0) != TdrError.ErrorType.TDR_NO_ERROR)
                        {
                            return;
                        }
                        if (!boutiqueDict.ContainsKey(conf2.dwID))
                        {
                            boutiqueDict.Add(conf2.dwID, conf2);
                        }
                        continue;
                    }
                    case CS_RES_DATA_TYPE.CS_RES_DATA_HEADIMAGE:
                    {
                        ResHeadImage image = new ResHeadImage();
                        if (image.unpack(ref srcBuf, 0) != TdrError.ErrorType.TDR_NO_ERROR)
                        {
                            return;
                        }
                        if (!boutiqueDict.ContainsKey(image.dwID))
                        {
                            headImageDict.Add(image.dwID, image);
                        }
                        flag2 = true;
                        continue;
                    }
                    case CS_RES_DATA_TYPE.CS_RES_DATA_SRV2CLT_GLOBAL_CONF:
                    {
                        ResGlobalInfo info7 = new ResGlobalInfo();
                        if (info7.unpack(ref srcBuf, 0) != TdrError.ErrorType.TDR_NO_ERROR)
                        {
                            return;
                        }
                        if (!svr2CltCfgDict.ContainsKey(info7.dwConfType))
                        {
                            svr2CltCfgDict.Add(info7.dwConfType, info7);
                        }
                        flag3 = true;
                        continue;
                    }
                    case CS_RES_DATA_TYPE.CS_RES_DATA_BANNERIMAGE:
                    {
                        ResBannerImage image2 = new ResBannerImage();
                        if (image2.unpack(ref srcBuf, 0) != TdrError.ErrorType.TDR_NO_ERROR)
                        {
                            return;
                        }
                        if (!svr2BannerImageDict.ContainsKey(image2.dwID))
                        {
                            svr2BannerImageDict.Add(image2.dwID, image2);
                        }
                        flag6 = true;
                        continue;
                    }
                    case CS_RES_DATA_TYPE.CS_RES_DATA_REWARDMATCH:
                    {
                        ResRewardMatchConf conf = new ResRewardMatchConf();
                        if (conf.unpack(ref srcBuf, 0) != TdrError.ErrorType.TDR_NO_ERROR)
                        {
                            return;
                        }
                        if (!rewardMatchRewardDict.ContainsKey(conf.dwId))
                        {
                            rewardMatchRewardDict.Add(conf.dwId, conf);
                        }
                        continue;
                    }
                    default:
                        goto Label_0B0A;
                }
                huoyueduDict[reward2.wID] = reward2;
            Label_09B8:
                Singleton<CTaskSys>.instance.model.huoyue_data.ParseHuoyuedu(reward2);
                continue;
            Label_0B0A:
                if (srcBuf.skipForward(num2) != TdrError.ErrorType.TDR_NO_ERROR)
                {
                    return;
                }
            }
            if (flag5)
            {
                Singleton<CMiShuSystem>.instance.CheckActPlayModeTipsForLobby();
            }
            if (flag4)
            {
                Singleton<EventRouter>.instance.BroadCastEvent(EventID.Mall_Entry_Add_RedDotCheck);
            }
            if (flag)
            {
                Singleton<EventRouter>.instance.BroadCastEvent(EventID.SERVER_SKIN_DATABIN_READY);
            }
            if (flag2)
            {
                Singleton<EventRouter>.instance.BroadCastEvent(EventID.NOBE_STATE_CHANGE);
            }
            if (flag6)
            {
                MonoSingleton<BannerImageSys>.GetInstance().LoadConfigServer();
            }
            if (flag3)
            {
                Singleton<CRoleInfoManager>.GetInstance().GetMasterRoleInfo().SetCoinGetLimitDailyCnt();
            }
        }

        public void UnloadDataBin()
        {
            monsterOrganLvDynamicInfobin.Unload();
            skillFuncDatabin.Unload();
            heroStarLvlUpDatabin.Unload();
            itemDatabin.Unload();
            rankLevelDatabin.Unload();
            taskDatabin.Unload();
            taskPrerequisiteDatabin.Unload();
            taskRewardDatabin.Unload();
            equipInfoDatabin.Unload();
            composeInfoDatabin.Unload();
            bufDropInfoDatabin.Unload();
            addWinLoseCondDatabin.Unload();
            textBubbleDatabin.Unload();
            resShopInfoDatabin.Unload();
            coninBuyDatabin.Unload();
            skillLvlUpDatabin.Unload();
            s_textDatabin.Unload();
            s_ruleTextDatabin.Unload();
            specialFunUnlockDatabin.Unload();
            licenseDatabin.Unload();
            newbieMainLineDatabin.Unload();
            newbieWeakMainLineDataBin.Unload();
            newbieWeakDatabin.Unload();
            newbieScriptDatabin.Unload();
            newbieSpecialTipDatabin.Unload();
            shopTypeDatabin.Unload();
            shopRefreshCostDatabin.Unload();
            heroProficiencyDatabin.Unload();
            rankGradeDatabin.Unload();
            rankSeasonDatabin.Unload();
            rankRewardDatabin.Unload();
            symbolCompDatabin.Unload();
            gameTaskDatabin.Unload();
            gameTaskGroupDatabin.Unload();
            charmLib.Unload();
            guildMiscDatabin.Unload();
            guildIconDatabin.Unload();
            guildBuildingDatabin.Unload();
            guildLevelDatabin.Unload();
            guildDonateDatabin.Unload();
            guildRankRewardDatabin.Unload();
            guildGradeDatabin.Unload();
            guildStarLevel.Unload();
            burnRewrad.Unload();
            burnMap.Unload();
            burnBuffMap.Unload();
            skinQualityPicDatabin.Unload();
            heroQualityPicDatabin.Unload();
            npcOfArena.Unload();
            robotBattleListInfo.Unload();
            robotHeroInfo.Unload();
            robotName.Unload();
            robotSubNameA.Unload();
            robotSubNameB.Unload();
            arenaLevelDatabin.Unload();
            arenaRewardDatabin.Unload();
            randowmRewardDB.Unload();
            cdDatabin.Unload();
            gearLvlUpDatabin.Unload();
            iosDianQuanBuyInfo.Unload();
            androidDianQuanBuyInfo.Unload();
            achieveDatabin.Unload();
            m_selectHeroChatDatabin.Unload();
            m_recommendEquipInBattleDatabin.Unload();
            changeNameDatabin.Unload();
            robotPlayerSkillDatabin.Unload();
            robotRookieHeroSkinDatabin.Unload();
            robotVeteranHeroSkinDatabin.Unload();
        }

        public void UpdateFrame()
        {
        }

        [CompilerGenerated]
        private sealed class <LoadDataBin>c__Iterator4 : IDisposable, IEnumerator, IEnumerator<object>
        {
            internal object $current;
            internal int $PC;

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
            }

            public bool MoveNext()
            {
                uint num = (uint) this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        GameDataMgr.heroDatabin = new DatabinTable<ResHeroCfgInfo, uint>("Databin/Public/Actor/hero.bytes", "dwCfgID");
                        GameDataMgr.settleDatabin = new DatabinTable<ResCommonSettle, uint>("Databin/Public/Global/CommonSettle.bytes", "dwSettleID");
                        GameDataMgr.heroEnergyDatabin = new DatabinTable<ResHeroEnergyInfo, uint>("Databin/Public/Actor/heroEnergy.bytes", "dwEnergyID");
                        GameDataMgr.skillDatabin = new DatabinTable<ResSkillCfgInfo, int>("Databin/Public/Skill/skill.bytes", "iCfgID");
                        GameDataMgr.bulletDatabin = new DatabinTable<ResBulletCfgInfo, int>("Databin/Client/Skill/bullet.bytes", "iCfgID");
                        GameDataMgr.monsterDatabin = new DatabinTable<ResMonsterCfgInfo, long>("Databin/Client/Actor/normalMonster.bytes", "iCfgID", "iDifficultyLevel");
                        this.$current = null;
                        this.$PC = 1;
                        goto Label_0CDE;

                    case 1:
                        GameDataMgr.organDatabin = new DatabinTable<ResOrganCfgInfo, long>("Databin/Client/Actor/organ.bytes", "iCfgID", "iDifficultyLevel");
                        GameDataMgr.monsterOrganLvDynamicInfobin = new DatabinTable<ResMonsterOrganLevelDynamicInfo, int>("Databin/Client/Actor/monsterOrganLevelDynamicInfo.bytes", "iLevel");
                        GameDataMgr.skillCombineDatabin = new DatabinTable<ResSkillCombineCfgInfo, int>("Databin/Public/Skill/skillcombine.bytes", "iCfgID");
                        this.$current = null;
                        this.$PC = 2;
                        goto Label_0CDE;

                    case 2:
                        GameDataMgr.skillFuncDatabin = new DatabinTable<ResSkillFuncCfgInfo, uint>("Databin/Client/Skill/skillfunc.bytes", "dwSkillFuncType");
                        GameDataMgr.soldierWaveDatabin = new DatabinTable<ResSoldierWaveInfo, uint>("Databin/Client/Actor/soldierwave.bytes", "dwSoldierWaveID");
                        GameDataMgr.heroAdvanceDatabin = new DatabinTable<ResHeroAdvanceInfo, uint>("Databin/Public/Actor/heroAdvance.bytes", "dwCfgID");
                        GameDataMgr.heroStarLvlUpDatabin = new DatabinTable<ResHeroStarLvlUpInfo, uint>("Databin/Public/Actor/heroStarLvlUp.bytes", "dwStarID");
                        this.$current = null;
                        this.$PC = 3;
                        goto Label_0CDE;

                    case 3:
                        GameDataMgr.heroLvlUpDatabin = new DatabinTable<ResHeroLvlUpInfo, uint>("Databin/Public/Actor/heroLvlUp.bytes", "dwLevelID");
                        GameDataMgr.itemDatabin = new DatabinTable<ResPropInfo, uint>("Databin/Public/Item/Item.bytes", "dwID");
                        GameDataMgr.levelDatabin = new DatabinTable<ResLevelCfgInfo, int>("Databin/Public/Level/Level.bytes", "iCfgID");
                        GameDataMgr.towerHitDatabin = new DatabinTable<TowerHitConf, byte>("Databin/Client/Actor/TowerHit.bytes", "bOrganType");
                        GameDataMgr.pvpLevelDatabin = new DatabinTable<ResAcntBattleLevelInfo, uint>("Databin/Public/Level/AcntBattleLevel.bytes", "dwMapId");
                        GameDataMgr.entertainLevelDatabin = new DatabinTable<ResEntertainmentLevelInfo, uint>("Databin/Public/Level/EntertainmentLevel.bytes", "dwMapId");
                        this.$current = null;
                        this.$PC = 4;
                        goto Label_0CDE;

                    case 4:
                        GameDataMgr.rankLevelDatabin = new DatabinTable<ResRankLevelInfo, uint>("Databin/Public/Level/RankLevel.bytes", "dwMapId");
                        GameDataMgr.battleDynamicPropertyDB = new DatabinTable<ResBattleDynamicProperty, uint>("Databin/Client/Level/BattleDynamicProperty.bytes", "dwKey");
                        GameDataMgr.battleDynamicDifficultyDB = new DatabinTable<ResBattleDynamicDifficulty, uint>("Databin/Public/Level/ResBattleDynamicDifficulty.bytes", "dwID");
                        GameDataMgr.clashAdditionDB = new DatabinTableMulti<ResClashAddition, uint>("Databin/Client/Level/ClashAddition.bytes", "dwAttackerMark");
                        this.$current = null;
                        this.$PC = 5;
                        goto Label_0CDE;

                    case 5:
                        GameDataMgr.actorLinesDatabin = new DatabinTableMulti<ResActorLinesInfo, int>("Databin/Client/Level/ActorLines.bytes", "iCfgID");
                        GameDataMgr.acntExpDatabin = new DatabinTable<ResAcntExpInfo, uint>("Databin/Public/Global/AcntExpInfo.bytes", "dwLevel");
                        GameDataMgr.globalInfoDatabin = new DatabinTable<ResGlobalInfo, uint>("Databin/Public/Global/GlobalConfInfo.bytes", "dwConfType");
                        GameDataMgr.taskDatabin = new DatabinTable<ResTask, uint>("Databin/Public/Task/task.bytes", "dwTaskID");
                        this.$current = null;
                        this.$PC = 6;
                        goto Label_0CDE;

                    case 6:
                        GameDataMgr.taskPrerequisiteDatabin = new DatabinTable<ResPrerequisite, uint>("Databin/Public/Task/prerequisite.bytes", "dwPrerequisiteType");
                        GameDataMgr.taskRewardDatabin = new DatabinTable<ResTaskReward, uint>("Databin/Public/Task/taskreward.bytes", "dwTaskRewardID");
                        GameDataMgr.equipInfoDatabin = new DatabinTable<ResEquipInfo, uint>("Databin/Public/Item/Equip.bytes", "dwID");
                        GameDataMgr.composeInfoDatabin = new DatabinTable<ResComposeInfo, uint>("Databin/Public/Item/ItemComp.bytes", "dwCompID");
                        this.$current = null;
                        this.$PC = 7;
                        goto Label_0CDE;

                    case 7:
                        GameDataMgr.bufDropInfoDatabin = new DatabinTable<ResBufDropInfo, uint>("Databin/Client/Level/BufDrop.bytes", "dwID");
                        GameDataMgr.evaluateCondInfoDatabin = new DatabinTable<ResEvaluateStarInfo, uint>("Databin/Public/Level/EvaluateCond.bytes", "dwCondID");
                        GameDataMgr.addWinLoseCondDatabin = new DatabinTable<ResEvaluateStarInfo, uint>("Databin/Public/Level/AddWinLoseCond.bytes", "dwCondID");
                        GameDataMgr.chapterInfoDatabin = new DatabinTable<ResChapterInfo, uint>("Databin/Public/Level/Chapter.bytes", "dwChapterId");
                        GameDataMgr.textBubbleDatabin = new DatabinTable<ResTextData, uint>("Databin/Client/Level/TextBubble.bytes", "dwTag");
                        this.$current = null;
                        this.$PC = 8;
                        goto Label_0CDE;

                    case 8:
                        GameDataMgr.guideTipDatabin = new DatabinTable<ResGuideTipInfo, int>("Databin/Client/Level/GuideTip.bytes", "iCfgID");
                        GameDataMgr.resShopInfoDatabin = new DatabinTable<ResShopInfo, uint>("Databin/Public/Shop/ShopBuy.bytes", "dwID");
                        GameDataMgr.coninBuyDatabin = new DatabinTable<ResCoinBuyInfo, ushort>("Databin/Public/Shop/CoinBuy.bytes", "wBuyFreq");
                        GameDataMgr.skillLvlUpDatabin = new DatabinTable<ResSkillLvlUpInfo, uint>("Databin/Public/Skill/skillLvlUp.bytes", "dwLevelID");
                        this.$current = null;
                        this.$PC = 9;
                        goto Label_0CDE;

                    case 9:
                        GameDataMgr.s_textDatabin = new DatabinTable<ResText, ushort>("Databin/Client/Text/Text.bytes", "wID");
                        GameDataMgr.s_ruleTextDatabin = new DatabinTable<ResRuleText, ushort>("Databin/Client/Text/RuleText.bytes", "wID");
                        GameDataMgr.randomSkillPassiveDatabin = new DatabinTable<ResRandomSkillPassiveRule, int>("Databin/Client/Skill/randompassive.bytes", "iRandomSkillPassiveKey");
                        GameDataMgr.skillPassiveDatabin = new DatabinTable<ResSkillPassiveCfgInfo, int>("Databin/Client/Skill/skillpassive.bytes", "iCfgID");
                        GameDataMgr.skillMarkDatabin = new DatabinTable<ResSkillMarkCfgInfo, int>("Databin/Client/Skill/skillmark.bytes", "iCfgID");
                        GameDataMgr.heroBalanceDatabin = new DatabinTable<ResHeroBalanceInfo, uint>("Databin/Client/Actor/heroBalanceInfo.bytes", "dwID");
                        this.$current = null;
                        this.$PC = 10;
                        goto Label_0CDE;

                    case 10:
                        GameDataMgr.unlockConditionDatabin = new DatabinTable<ResUnlockCondition, uint>("Databin/Public/Global/unlockcondition.bytes", "dwUnlockID");
                        GameDataMgr.specialFunUnlockDatabin = new DatabinTable<ResSpecialFucUnlock, uint>("Databin/Public/Global/specialfucunlock.bytes", "dwFucID");
                        GameDataMgr.licenseDatabin = new DatabinTable<ResLicenseInfo, uint>("Databin/Public/Global/license.bytes", "dwLicenseID");
                        this.$current = null;
                        this.$PC = 11;
                        goto Label_0CDE;

                    case 11:
                        GameDataMgr.newbieMainLineDatabin = new DatabinTable<NewbieGuideMainLineConf, uint>("Databin/Client/NewbieGuide/NewbieGuideMainLineConf.bytes", "dwID");
                        GameDataMgr.newbieWeakMainLineDataBin = new DatabinTable<NewbieWeakGuideMainLineConf, uint>("Databin/Client/NewbieGuide/NewbieWeakGuideMainLineConf.bytes", "dwID");
                        GameDataMgr.newbieWeakDatabin = new DatabinTable<NewbieGuideWeakConf, uint>("Databin/Client/NewbieGuide/NewbieGuideWeakConf.bytes", "dwID");
                        GameDataMgr.newbieScriptDatabin = new DatabinTableMulti<NewbieGuideScriptConf, uint>("Databin/Client/NewbieGuide/NewbieGuideScriptConf.bytes", "dwID");
                        this.$current = null;
                        this.$PC = 12;
                        goto Label_0CDE;

                    case 12:
                        GameDataMgr.newbieSpecialTipDatabin = new DatabinTable<NewbieGuideSpecialTipConf, uint>("Databin/Client/NewbieGuide/NewbieGuideSpecialTipConf.bytes", "dwID");
                        this.$current = null;
                        this.$PC = 13;
                        goto Label_0CDE;

                    case 13:
                        GameDataMgr.soulPropEftDatabin = new DatabinTable<ResSoulPropEft, uint>("Databin/Client/Soul/SoulPropEft.bytes", "dwID");
                        GameDataMgr.soulLvlUpDatabin = new DatabinTableMulti<ResSoulLvlUpInfo, uint>("Databin/Client/Soul/SoulLvlUp.bytes", "dwSoulAllocId");
                        GameDataMgr.soulExpAllocDatabin = new DatabinTable<ResSoulExpAllocRule, uint>("Databin/Client/Soul/SoulExpAllocRule.bytes", "dwID");
                        GameDataMgr.shopTypeDatabin = new DatabinTable<ResShopType, ushort>("Databin/Public/Shop/ShopType.bytes", "wShopType");
                        this.$current = null;
                        this.$PC = 14;
                        goto Label_0CDE;

                    case 14:
                        GameDataMgr.shopRefreshCostDatabin = new DatabinTable<ResShopRefreshCost, uint>("Databin/Public/Shop/RefreshCost.bytes", "dwID");
                        GameDataMgr.heroProficiencyDatabin = new DatabinTableMulti<ResHeroProficiency, byte>("Databin/Public/Actor/heroProficiency.bytes", "bLevel");
                        GameDataMgr.rankGradeDatabin = new DatabinTable<ResRankGradeConf, byte>("Databin/Public/Rank/RankGrade.bytes", "bGrade");
                        GameDataMgr.rankSeasonDatabin = new DatabinTable<ResRankSeasonConf, uint>("Databin/Public/Rank/RankSeason.bytes", "dwSeasonId");
                        GameDataMgr.rankRewardDatabin = new DatabinTable<ResRankRewardConf, uint>("Databin/Public/Rank/RankSeasonReward.bytes", "dwGradeId");
                        GameDataMgr.soulAdditionDatabin = new DatabinTable<ResSoulAddition, int>("Databin/Client/Soul/SoulAddition.bytes", "iHeroKillType");
                        this.$current = null;
                        this.$PC = 15;
                        goto Label_0CDE;

                    case 15:
                        GameDataMgr.symbolInfoDatabin = new DatabinTable<ResSymbolInfo, uint>("Databin/Public/Item/Symbol.bytes", "dwID");
                        GameDataMgr.symbolPosDatabin = new DatabinTable<ResSymbolPos, byte>("Databin/Public/Item/SymbolPos.bytes", "bSymbolPos");
                        GameDataMgr.heroSymbolLvlDatabin = new DatabinTable<ResHeroSymbolLvl, ushort>("Databin/Public/Item/HeroSymbol.bytes", "wPvpLevel");
                        GameDataMgr.symbolCompDatabin = new DatabinTable<ResSymbolComp, uint>("Databin/Public/Item/SymbolComp.bytes", "dwID");
                        this.$current = null;
                        this.$PC = 0x10;
                        goto Label_0CDE;

                    case 0x10:
                        GameDataMgr.gameTaskDatabin = new DatabinTable<ResGameTask, uint>("Databin/Client/Battle/GameTask.bytes", "dwID");
                        GameDataMgr.gameTaskGroupDatabin = new DatabinTable<ResGameTaskGroup, uint>("Databin/Client/Battle/GameTaskGroup.bytes", "dwKey");
                        GameDataMgr.shenfuBin = new DatabinTable<ShenFuInfo, uint>("Databin/Client/Battle/ShenFuInfo.bytes", "dwShenFuId");
                        GameDataMgr.charmLib = new DatabinTable<CharmLib, uint>("Databin/Client/Battle/CharmLib.bytes", "dwPoolId");
                        GameDataMgr.guildMiscDatabin = new DatabinTable<ResGuildMisc, uint>("Databin/Public/Guild/GuildMisc.bytes", "dwConfType");
                        GameDataMgr.guildIconDatabin = new DatabinTable<ResGuildIcon, uint>("Databin/Client/Guild/GuildIcon.bytes", "dwIcon");
                        this.$current = null;
                        this.$PC = 0x11;
                        goto Label_0CDE;

                    case 0x11:
                        GameDataMgr.guildBuildingDatabin = new DatabinTable<ResGuildBuilding, byte>("Databin/Public/Guild/GuildBuilding.bytes", "bBuildingType");
                        GameDataMgr.guildLevelDatabin = new DatabinTable<ResGuildLevel, byte>("Databin/Public/Guild/GuildLevel.bytes", "bGuildLevel");
                        GameDataMgr.guildDonateDatabin = new DatabinTable<ResGuildDonate, byte>("Databin/Public/Guild/GuildDonate.bytes", "bDonateType");
                        GameDataMgr.guildRankRewardDatabin = new DatabinTable<ResGuildRankReward, int>("Databin/Public/Guild/GuildRankReward.bytes", "iStartRankNo");
                        GameDataMgr.guildGradeDatabin = new DatabinTable<ResGuildGradeConf, byte>("Databin/Public/Guild/GuildGrade.bytes", "bIndex");
                        GameDataMgr.guildStarLevel = new DatabinTable<ResGuildShopStarIndexConf, uint>("Databin/Public/Guild/GuildShopStarIndex.bytes", "dwBeginStar");
                        this.$current = null;
                        this.$PC = 0x12;
                        goto Label_0CDE;

                    case 0x12:
                        GameDataMgr.talentRule = new DatabinTable<ResTalentRule, byte>("Databin/Public/Talent/TalentRule.bytes", "bID");
                        GameDataMgr.talentLib = new DatabinTable<ResTalentLib, uint>("Databin/Client/Talent/TalentLib.bytes", "dwID");
                        GameDataMgr.talentHero = new DatabinTable<ResTalentHero, uint>("Databin/Public/Talent/TalentHero.bytes", "dwID");
                        GameDataMgr.miShuLib = new DatabinTable<ResMiShuInfo, uint>("Databin/Client/Talent/MiShuInfo.bytes", "dwID");
                        GameDataMgr.acntPvpExpDatabin = new DatabinTable<ResAcntPvpExpInfo, byte>("Databin/Public/Global/AcntPvpExpInfo.bytes", "bLevel");
                        this.$current = null;
                        this.$PC = 0x13;
                        goto Label_0CDE;

                    case 0x13:
                        GameDataMgr.burnRewrad = new DatabinTableMulti<ResBurningReward, uint>("Databin/Public/Global/BurningReward.bytes", "dwAcntLevel");
                        GameDataMgr.burnMap = new DatabinTable<ResLevelCfgInfo, int>("Databin/Public/Level/BurningLevel.bytes", "iCfgID");
                        GameDataMgr.burnBuffMap = new DatabinTable<ResBurningBuff, int>("Databin/Public/Skill/BurningBuff.bytes", "iSkillCombineID");
                        this.$current = null;
                        this.$PC = 20;
                        goto Label_0CDE;

                    case 20:
                        GameDataMgr.battleParam = new DatabinTable<ResBattleParam, uint>("Databin/Client/Actor/battleParam.bytes", "dwID");
                        GameDataMgr.propertyValInfo = new DatabinTable<ResPropertyValueInfo, byte>("Databin/Client/Actor/PropertyValueInfo.bytes", "bPropertyType");
                        GameDataMgr.heroSkinDatabin = new DatabinTable<ResHeroSkin, uint>("Databin/Public/Actor/heroSkin.bytes", "dwID");
                        GameDataMgr.skinQualityPicDatabin = new DatabinTable<ResSkinQualityPicInfo, byte>("Databin/Client/Actor/skinQualityPic.bytes", "bQualityId");
                        GameDataMgr.heroQualityPicDatabin = new DatabinTableMulti<ResHeroQualityPicInfo, int>("Databin/Client/Actor/qualityPic.bytes", "iQualityId");
                        GameDataMgr.npcOfArena = new DatabinTable<ResNpcOfArena, uint>("Databin/Public/Arena/NpcOfArena", "dwObjId");
                        this.$current = null;
                        this.$PC = 0x15;
                        goto Label_0CDE;

                    case 0x15:
                        GameDataMgr.robotBattleListInfo = new DatabinTable<ResRobotBattleList, uint>("Databin/Public/Global/RobotBattleList", "dwBattleListID");
                        GameDataMgr.robotHeroInfo = new DatabinTable<ResRobotPower, uint>("Databin/Public/Global/RobotPower", "dwId");
                        GameDataMgr.robotName = new DatabinTable<ResRobotName, uint>("Databin/Public/Global/RobotName", "dwObjId");
                        GameDataMgr.robotSubNameA = new DatabinTable<ResRobotSubNameA, uint>("Databin/Public/Global/RobotSubNameA", "dwObjId");
                        this.$current = null;
                        this.$PC = 0x16;
                        goto Label_0CDE;

                    case 0x16:
                        GameDataMgr.robotSubNameB = new DatabinTable<ResRobotSubNameB, uint>("Databin/Public/Global/RobotSubNameB", "dwObjId");
                        GameDataMgr.arenaLevelDatabin = new DatabinTable<ResLevelCfgInfo, int>("Databin/Public/Level/ArenaLevel", "iCfgID");
                        GameDataMgr.arenaRewardDatabin = new DatabinTable<ResArenaOneDayReward, uint>("Databin/Public/Arena/ArenaOneDayReward", "dwRankStart");
                        GameDataMgr.randowmRewardDB = new DatabinTable<ResRandomRewardStore, uint>("Databin/Public/Global/RandomRewardStore", "dwRewardID");
                        this.$current = null;
                        this.$PC = 0x17;
                        goto Label_0CDE;

                    case 0x17:
                        GameDataMgr.signalDatabin = new DatabinTable<ResSignalInfo, byte>("Databin/Client/Battle/SignalPanel", "bSignalID");
                        GameDataMgr.cdDatabin = new DatabinTable<ResClrCD, uint>("Databin/Public/Shop/ClrCd", "dwType");
                        GameDataMgr.inBattleMsgDatabin = new DatabinTable<ResInBatMsgCfg, uint>("Databin/Public/Battle/InBattleMsgCfg.bytes", "dwID");
                        GameDataMgr.inBattleChannelDatabin = new DatabinTable<ResInBatChannelCfg, byte>("Databin/Public/Battle/InBattleChannelCfg.bytes", "bID");
                        GameDataMgr.inBattleHeroActDatabin = new DatabinTable<ResInBatMsgHeroActCfg, uint>("Databin/Client/Battle/InBattleMsgHeroAct.bytes", "dwID");
                        GameDataMgr.gearInfoDatabin = new DatabinTable<ResGearInfo, uint>("Databin/Public/Item/Gear.bytes", "dwID");
                        GameDataMgr.gearLvlUpDatabin = new DatabinTable<ResGearLvlUp, uint>("Databin/Public/Item/GearLvlUp.bytes", "dwLevel");
                        this.$current = null;
                        this.$PC = 0x18;
                        goto Label_0CDE;

                    case 0x18:
                        GameDataMgr.iosDianQuanBuyInfo = new DatabinTable<ResCouponsBuyInfo, uint>("Databin/Client/Pay/IOSDiamondBuyInfo", "dwID");
                        GameDataMgr.androidDianQuanBuyInfo = new DatabinTable<ResCouponsBuyInfo, uint>("Databin/Client/Pay/AndroidDiamondBuyInfo", "dwID");
                        GameDataMgr.cpLevelDatabin = new DatabinTable<ResCounterPartLevelInfo, uint>("Databin/Public/Level/CounterPartLevel", "dwMapId");
                        GameDataMgr.achieveDatabin = new DatabinTable<ResAchievement, uint>("Databin/Public/Achievement/Achievement.bytes", "dwID");
                        GameDataMgr.addedSkiilDatabin = new DatabinTable<ResSkillUnlock, ushort>("Databin/Public/Skill/SkillUnlock.bytes", "wAcntLevel");
                        GameDataMgr.callMonsterDatabin = new DatabinTable<ResCallMonster, ushort>("Databin/Public/Skill/CallMonster.bytes", "wConfigID");
                        GameDataMgr.resVipDianQuan = new DatabinTable<ResVIPCoupons, uint>("Databin/Public/Global/VIPCoupons.bytes", "dwVIPLevel");
                        GameDataMgr.heroAwakDatabin = new DatabinTable<ResHeroWakeInfo, long>("Databin/Public/Actor/herowake.bytes", "dwHeroID", "dwStepID");
                        GameDataMgr.resNobeInfoDatabin = new DatabinTable<ResNobeInfo, byte>("Databin/Public/Global/ResNobeInfo.bytes", "dwID");
                        GameDataMgr.m_selectHeroChatDatabin = new DatabinTable<ResTextData, uint>("Databin/Client/Level/SelectHeroChat.bytes", "dwTag");
                        GameDataMgr.m_equipInBattleDatabin = new DatabinTable<ResEquipInBattle, ushort>("Databin/Public/Battle/EquipInBattle", "wID");
                        GameDataMgr.m_recommendEquipInBattleDatabin = new DatabinTable<ResRecommendEquipInBattle, ushort>("Databin/Public/Battle/RecommendEquipInBattle", "wHeroID");
                        GameDataMgr.changeNameDatabin = new DatabinTable<ResChangeName, uint>("Databin/Public/Global/ChangeName", "dwIndex");
                        GameDataMgr.banHeroBin = new DatabinTable<ResBanHeroConf, long>("Databin/Public/Level/BanHero.bytes", "bMapType", "dwMapId");
                        GameDataMgr.uinionBattleLevelDatabin = new DatabinTable<ResRewardMatchLevelInfo, uint>("Databin/Public/Level/RewardMatchLevel.bytes", "dwMapId");
                        GameDataMgr.commonRewardDatabin = new DatabinTable<ResCommReward, uint>("Databin/Public/Global/CommReward.bytes", "dwRewardID");
                        GameDataMgr.unionRankRewardDetailDatabin = new DatabinTable<ResRewardMatchDetailConf, uint>("Databin/Public/Global/RewardMatchDetail.bytes", "dwId");
                        GameDataMgr.speakerDatabin = new DatabinTable<ResHornInfo, uint>("Databin/Public/Item/Horn.bytes", "dwID");
                        this.$current = null;
                        this.$PC = 0x19;
                        goto Label_0CDE;

                    case 0x19:
                        GameDataMgr.robotPlayerSkillDatabin = new DatabinTable<ResFakeAcntSkill, ushort>("Databin/Public/WarmBattle/FakeAcntSkill", "dwLevel");
                        GameDataMgr.robotRookieHeroSkinDatabin = new DatabinTable<ResFakeAcntHero, ushort>("Databin/Public/WarmBattle/ResFakeAcntHeroLowLevel", "dwHeroID");
                        GameDataMgr.robotVeteranHeroSkinDatabin = new DatabinTable<ResFakeAcntHero, ushort>("Databin/Public/WarmBattle/ResFakeAcntHeroHighLevel", "dwHeroID");
                        this.$current = null;
                        this.$PC = 0x1a;
                        goto Label_0CDE;

                    case 0x1a:
                        GameDataMgr.creditLevelDatabin = new DatabinTable<ResCreditLevelInfo, uint>("Databin/Client/Credit/creditLevel.bytes", "dwID");
                        this.$PC = -1;
                        break;
                }
                return false;
            Label_0CDE:
                return true;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }
    }
}

