﻿using System;
using UnityEngine;

public class NewbieGuideSctiptFactory
{
    public static NewbieGuideBaseScript AddScript(NewbieGuideScriptType type, GameObject gameObject)
    {
        switch (type)
        {
            case NewbieGuideScriptType.clickWaiting:
                return gameObject.AddComponent<NewbieGuideWaitSomeTime>();

            case NewbieGuideScriptType.openForm:
                return gameObject.AddComponent<NewbieGuideOpenForm>();

            case NewbieGuideScriptType.clickTrialBtn:
                return gameObject.AddComponent<NewbieGuideClickTrial>();

            case NewbieGuideScriptType.clickZhuangziBtn:
                return gameObject.AddComponent<NewbieGuideClickZhuangzi>();

            case NewbieGuideScriptType.clickBurningBtn:
                return gameObject.AddComponent<NewbieGuideClickBurning>();

            case NewbieGuideScriptType.clickEliteBtn:
                return gameObject.AddComponent<NewbieGuideClickElitePvE>();

            case NewbieGuideScriptType.clickGuildBtn:
                return gameObject.AddComponent<NewbieGuideClickHallGuild>();

            case NewbieGuideScriptType.clickNewbieTalent:
                return gameObject.AddComponent<NewbieGuideClickTalent>();

            case NewbieGuideScriptType.clickNewbieTalentBtn:
                return gameObject.AddComponent<NewbieGuideClickTalentBtn>();

            case NewbieGuideScriptType.clickNewbieTalentGuide1:
                return gameObject.AddComponent<NewbieGuideTalentShow1>();

            case NewbieGuideScriptType.clickNewbieTalentGuide2:
                return gameObject.AddComponent<NewbieGuideTalentShow2>();

            case NewbieGuideScriptType.clickNewbieTalentSelect:
                return gameObject.AddComponent<NewbieGuideClickTalentItem>();

            case NewbieGuideScriptType.clickNewbieTalentConfirm:
                return gameObject.AddComponent<NewbieGuideClickTalentConfirm>();

            case NewbieGuideScriptType.clickNewbieDragonIcon:
                return gameObject.AddComponent<NewbieGuideClickDragonIcon>();

            case NewbieGuideScriptType.clickNewbieTalentSecondTime:
                return gameObject.AddComponent<NewbieGuideClickTalentSecondTime>();

            case NewbieGuideScriptType.clickBuyEquip:
                return gameObject.AddComponent<NewbieGuideClickBuyEquip>();

            case NewbieGuideScriptType.clickFocus:
                return gameObject.AddComponent<NewbieGuideClickFocus>();

            case NewbieGuideScriptType.clickAttack:
                return gameObject.AddComponent<NewbieGuideClickAttack>();

            case NewbieGuideScriptType.clickDefense:
                return gameObject.AddComponent<NewbieGuideClickDefense>();

            case NewbieGuideScriptType.clickGather:
                return gameObject.AddComponent<NewbieGuideClickGather>();

            case NewbieGuideScriptType.clickVoice:
                return gameObject.AddComponent<NewbieGuideClickVoice>();

            case NewbieGuideScriptType.clickMap:
                return gameObject.AddComponent<NewbieGuideClickMap>();

            case NewbieGuideScriptType.clickEquipBuyBtn:
                return gameObject.AddComponent<NewbieGuideClickEquipBuyBtn>();

            case NewbieGuideScriptType.clickEquipPanel:
                return gameObject.AddComponent<NewbieGuideClickEquipPanel>();

            case NewbieGuideScriptType.clickJungleSword:
                return gameObject.AddComponent<NewbieGuideClickJungleSword>();

            case NewbieGuideScriptType.clickJungleEquipPanel:
                return gameObject.AddComponent<NewbieGuideClickJungleEquipPanel>();

            case NewbieGuideScriptType.clickEuipPanelClose:
                return gameObject.AddComponent<NewbieGuideClickEquipPanelClose>();

            case NewbieGuideScriptType.autoCloseEquipForm:
                return gameObject.AddComponent<NewbieGuideCloseEquipForm>();

            case NewbieGuideScriptType.bigMapSignGuide:
                return gameObject.AddComponent<NewbieGuideBigMapGign>();

            case NewbieGuideScriptType.selectModeGuide:
                return gameObject.AddComponent<NewbieGuideShowSelectModeGuide>();

            case NewbieGuideScriptType.prefabeTextGuide:
                return gameObject.AddComponent<NewbieGuideShowHighlightPrefabeText>();

            case NewbieGuideScriptType.clickPvpHumanAi:
                return gameObject.AddComponent<NewbieGuideClickPvpHumanAi>();

            case NewbieGuideScriptType.clickPvpHumanAiSingle33:
                return gameObject.AddComponent<NewbieGuideClickPvpHumanAiSingle33>();

            case NewbieGuideScriptType.clickPvpHuman:
                return gameObject.AddComponent<NewbieGuideClickPvpHuman>();

            case NewbieGuideScriptType.clickPvpHumanSingle33:
                return gameObject.AddComponent<NewbieGuideClickPvpHumanSingle33>();

            case NewbieGuideScriptType.clickMatching:
                return gameObject.AddComponent<NewbieGuideClickMatching>();

            case NewbieGuideScriptType.clickPvpHumanAiSingle33Difficulty:
                return gameObject.AddComponent<NewbieGuideClickPvpHumanAiSingle33Difficulty>();

            case NewbieGuideScriptType.guide5v5Confirm:
                return gameObject.AddComponent<NewbieGuide5v5GuideConfirm>();

            case NewbieGuideScriptType.clickPvpHuman11:
                return gameObject.AddComponent<NewbieGuideClickPvPHuman11>();

            case NewbieGuideScriptType.clickTrain:
                return gameObject.AddComponent<NewbieGuideClickTrain>();

            case NewbieGuideScriptType.clickTrain55:
                return gameObject.AddComponent<NewbieGuideClickTrain55>();

            case NewbieGuideScriptType.clickTrainWheelDisc:
                return gameObject.AddComponent<NewbieGuideClickWheelDisc>();

            case NewbieGuideScriptType.guide3v3Confirm:
                return gameObject.AddComponent<NewbieGuide3v3Confirm>();

            case NewbieGuideScriptType.clickTrain33:
                return gameObject.AddComponent<NewbieGuideClickTrain33>();

            case NewbieGuideScriptType.oldPlayerGuide5v5Confirm:
                return gameObject.AddComponent<NewbieGuideOldPlayer55Confirm>();

            case NewbieGuideScriptType.clickTrainLevelBack:
                return gameObject.AddComponent<NewbieGuideTrainLevelClickBack>();

            case NewbieGuideScriptType.clickPvpHuman55:
                return gameObject.AddComponent<NewbieGuideClickPvPHuman55>();

            case NewbieGuideScriptType.clickMatchingConfirm:
                return gameObject.AddComponent<NewbieGuideClickMatchingConfirm>();

            case NewbieGuideScriptType.introfireMatch:
                return gameObject.AddComponent<NewbieGuideIntroFireMatch>();

            case NewbieGuideScriptType.clickHallSymbolBtn:
                return gameObject.AddComponent<NewbieGuideClickHallSymbolBtn>();

            case NewbieGuideScriptType.clickSymbolSlot:
                return gameObject.AddComponent<NewbieGuideClickSymbolSlot>();

            case NewbieGuideScriptType.clickSymbol:
                return gameObject.AddComponent<NewbieGuideClickSymbol>();

            case NewbieGuideScriptType.clickPvEBackToHall:
                return gameObject.AddComponent<NewbieGuideClickAdvToLobbyScript>();

            case NewbieGuideScriptType.clickGetSymbol:
                return gameObject.AddComponent<NewbieGuideClickGetSymbol>();

            case NewbieGuideScriptType.clickSymbolRewardBack:
                return gameObject.AddComponent<NewbieGuideClickSymbolRewardBack>();

            case NewbieGuideScriptType.clickCloseSymbolIntro:
                return gameObject.AddComponent<NewbieGuideClickCloseSymbolIntro>();

            case NewbieGuideScriptType.clickSymbolManufacture:
                return gameObject.AddComponent<NewbieGuideClickSymbolManufacture>();

            case NewbieGuideScriptType.clickSymbolEquip:
                return gameObject.AddComponent<NewbieGuideClickSymbolEquip>();

            case NewbieGuideScriptType.clickSymbolSpecific:
                return gameObject.AddComponent<NewbieGuideClickSymbolSpecific>();

            case NewbieGuideScriptType.clickHeroSymbolPage:
                return gameObject.AddComponent<NewbieGuideClickHeroSymbolPage>();

            case NewbieGuideScriptType.pickSymbolManufacture:
                return gameObject.AddComponent<NewbieGuidePickSymbolManufacture>();

            case NewbieGuideScriptType.clickSymbolManufactureConfirm:
                return gameObject.AddComponent<NewbieGuideClickSymbolManufactureConfirm>();

            case NewbieGuideScriptType.clickSymbolLottery:
                return gameObject.AddComponent<NewbieGuideClickSymbolLottery>();

            case NewbieGuideScriptType.clickSymbolLotteryBack:
                return gameObject.AddComponent<NewbieGuideClickSymbolLotteryBack>();

            case NewbieGuideScriptType.clickBattleHeroAddedSkillSwitch:
                return gameObject.AddComponent<NewbieGuideClickBattleHeroAddedSkillSwitch>();

            case NewbieGuideScriptType.clickAddedSkillForBattle:
                return gameObject.AddComponent<NewbieGuideClickAddedSkillForBattle>();

            case NewbieGuideScriptType.clickAddedSkillArrow:
                return gameObject.AddComponent<NewbieGuideClickAddedSkillArrow>();

            case NewbieGuideScriptType.clickAddedSkillConfirm:
                return gameObject.AddComponent<NewbieGuideClickAddedSkillConfirm>();

            case NewbieGuideScriptType.clickHeroSkill:
                return gameObject.AddComponent<NewbieGuideClickHeroSkill>();

            case NewbieGuideScriptType.clickChangeTeam:
                return gameObject.AddComponent<NewbieGuideClickChangeTeam>();

            case NewbieGuideScriptType.clickBackToHall:
                return gameObject.AddComponent<NewbieGuideClickBackToHall>();

            case NewbieGuideScriptType.clickBattleHero:
                return gameObject.AddComponent<NewbieGuideClickBattleHero>();

            case NewbieGuideScriptType.clickBattleHeroConfirm:
                return gameObject.AddComponent<NewbieGuideClickBattleHeroConfirm>();

            case NewbieGuideScriptType.clickFullStarAward:
                return gameObject.AddComponent<NewbieGuideClickFullStarAward>();

            case NewbieGuideScriptType.clickFullStartAwardConfirm:
                return gameObject.AddComponent<NewbieGuideClickFullStartAwardConfirm>();

            case NewbieGuideScriptType.clickPvpBackToHall:
                return gameObject.AddComponent<NewbieGuideClickPvpBackToHall>();

            case NewbieGuideScriptType.clickChallenge:
                return gameObject.AddComponent<NewbieGuideClickChallenge>();

            case NewbieGuideScriptType.clickChallengeNext:
                return gameObject.AddComponent<NewbieGuideClickChallengeNext>();

            case NewbieGuideScriptType.clickMoreHero:
                return gameObject.AddComponent<NewbieGuideClickMoreHero>();

            case NewbieGuideScriptType.clickTaskFinish:
                return gameObject.AddComponent<NewbieGuideClickTaskFinish>();

            case NewbieGuideScriptType.clickTournamentDefense:
                return gameObject.AddComponent<NewbieGuideClickTournamentDefense>();

            case NewbieGuideScriptType.closeMoreHeroFolder:
                return gameObject.AddComponent<NewbieGuideCloseMoreHeroFolder>();

            case NewbieGuideScriptType.clickPvPEntryToHall:
                return gameObject.AddComponent<NewbieGuideClickPvPEntryToHall>();

            case NewbieGuideScriptType.clickAdvSelectToHall:
                return gameObject.AddComponent<NewbieGuideClickAdvToLobbyScript>();

            case NewbieGuideScriptType.clickPvPProfit:
                return gameObject.AddComponent<NewbieGuideClickPvPProfit>();

            case NewbieGuideScriptType.clickHallHero:
                return gameObject.AddComponent<NewbieGuideClickHallHero>();

            case NewbieGuideScriptType.clickOneHero:
                return gameObject.AddComponent<NewbieGuideClickOneHero>();

            case NewbieGuideScriptType.clickUpgradeHero:
                return gameObject.AddComponent<NewbieGuideClickUpgradeHero>();

            case NewbieGuideScriptType.clickUpgradeHeroActual:
                return gameObject.AddComponent<NewbieGuideClickUpgradeOneButton>();

            case NewbieGuideScriptType.clickBackToHero:
                return gameObject.AddComponent<NewbieGuideClickBackToHero>();

            case NewbieGuideScriptType.clickHeroToHall:
                return gameObject.AddComponent<NewbieGuideClickHeroToHall>();

            case NewbieGuideScriptType.clickEquippment:
                return gameObject.AddComponent<NewbieGuideClickEquippment>();

            case NewbieGuideScriptType.clickUpgradeEquipment:
                return gameObject.AddComponent<NewbieGuideClickUpgradeEquipOneButton>();

            case NewbieGuideScriptType.clickDiamondBuyHero:
                return gameObject.AddComponent<NewbieGuideClickDiamondBuyHero>();

            case NewbieGuideScriptType.clickHeroStepUp:
                return gameObject.AddComponent<NewbieGuideClickHeroStepUp>();

            case NewbieGuideScriptType.clickHeroStepUpActual:
                return gameObject.AddComponent<NewbieGuideClickHeroStepUpActual>();

            case NewbieGuideScriptType.clickEquipAdvanceTab:
                return gameObject.AddComponent<NewbieGuideClickEquipAdvanceTab>();

            case NewbieGuideScriptType.clickEquipAdvanceButton:
                return gameObject.AddComponent<NewbieGuideClickEquipAdvanceBtn>();

            case NewbieGuideScriptType.clickHonorBuyHero:
                return gameObject.AddComponent<NewbieGuideClickHonorBuyHero>();

            case NewbieGuideScriptType.clickHeroStarUp:
                return gameObject.AddComponent<NewbieGuideClickHeroStarUp>();

            case NewbieGuideScriptType.clickHeroStarUpAdv:
                return gameObject.AddComponent<NewbieGuideClickHeroStarUpAdv>();

            case NewbieGuideScriptType.clickSettingMenu:
                return gameObject.AddComponent<NewbieGuideClickSettingMenu>();

            case NewbieGuideScriptType.clickSettingOp:
                return gameObject.AddComponent<NewbieGuideClickSettingOp>();

            case NewbieGuideScriptType.clickAutoCasting:
                return gameObject.AddComponent<NewbieGuideClickAutoCasting>();

            case NewbieGuideScriptType.clickCadCasting:
                return gameObject.AddComponent<NewbieGuideClickCadCasting>();

            case NewbieGuideScriptType.clickAttackNearest:
                return gameObject.AddComponent<NewbieGuideClickAttackNearest>();

            case NewbieGuideScriptType.clickAttackWeakest:
                return gameObject.AddComponent<NewbieGuideClickAttackWeakest>();

            case NewbieGuideScriptType.clickCloseSettingMenu:
                return gameObject.AddComponent<NewbieGuideClickCloseSettingMenu>();

            case NewbieGuideScriptType.clickCaptainButton:
                return gameObject.AddComponent<NewbieGuideClickCaptainButton>();

            case NewbieGuideScriptType.clickCommonAttackType1:
                return gameObject.AddComponent<NewbieGuideCommonAttackType1>();

            case NewbieGuideScriptType.clickCommonAttackType2:
                return gameObject.AddComponent<NewbieGuideCommonAttackType2>();

            case NewbieGuideScriptType.clickHallNewbie:
                return gameObject.AddComponent<NewbieGuideClickHallTask>();

            case NewbieGuideScriptType.clickMainTask:
                return gameObject.AddComponent<NewbieGuideClickMainTask>();

            case NewbieGuideScriptType.clickTaskReward:
                return gameObject.AddComponent<NewbieGuideClickTaskReward>();

            case NewbieGuideScriptType.clickConfirmReward:
                return gameObject.AddComponent<NewbieGuideClickConfirmReward>();

            case NewbieGuideScriptType.clickRewardToHall:
                return gameObject.AddComponent<NewbieGuideClickRewardToHall>();

            case NewbieGuideScriptType.clickMistreatCom:
                return gameObject.AddComponent<NewbieGuideClickMistreatCom>();

            case NewbieGuideScriptType.clickBackBag:
                return gameObject.AddComponent<NewbieGuideClickBackBag>();

            case NewbieGuideScriptType.clickUsingItem:
                return gameObject.AddComponent<NewbieGuideClickUsingItem>();

            case NewbieGuideScriptType.clickBagToHall:
                return gameObject.AddComponent<NewbieGuideClickBagToHall>();

            case NewbieGuideScriptType.clickUsingMax:
                return gameObject.AddComponent<NewbieGuideClickUsingMax>();

            case NewbieGuideScriptType.clickUsingConfirm:
                return gameObject.AddComponent<NewbieGuideClickUsingConfirm>();

            case NewbieGuideScriptType.clickOneItem:
                return gameObject.AddComponent<NewbieGuideClickOneItem>();

            case NewbieGuideScriptType.clickPveOrPvpEntry:
                return gameObject.AddComponent<NewbieGuideClickPveOrPvpEntry>();

            case NewbieGuideScriptType.clickPvpEntry:
                return gameObject.AddComponent<NewbieGuideClickPvpEntry>();

            case NewbieGuideScriptType.clickPveEntry:
                return gameObject.AddComponent<NewbieGuideClickPveEntry>();

            case NewbieGuideScriptType.clickExploring:
                return gameObject.AddComponent<NewbieGuideClickExploring>();

            case NewbieGuideScriptType.clickHallMall:
                return gameObject.AddComponent<NewbieGuideClickHallMall>();

            case NewbieGuideScriptType.clickBuyOne:
                return gameObject.AddComponent<NewbieGuideClickBuyOne>();

            case NewbieGuideScriptType.clickHeroPackage:
                return gameObject.AddComponent<NewbieGuideClickHeroPackage>();

            case NewbieGuideScriptType.clickTournament:
                return gameObject.AddComponent<NewbieGuideClickTournament>();

            case NewbieGuideScriptType.clickChallenging:
                return gameObject.AddComponent<NewbieGuideClickChallenging>();

            case NewbieGuideScriptType.clickAnyWhereScreen:
                return gameObject.AddComponent<NewbieGuideClickAnyWhereScreen>();

            case NewbieGuideScriptType.clickHonorMall:
                return gameObject.AddComponent<NewbieGuideClickHonorMall>();

            case NewbieGuideScriptType.clickMsgBox:
                return gameObject.AddComponent<NewbieGuideClickMsgBox>();

            case NewbieGuideScriptType.clickCombat3v3:
                return gameObject.AddComponent<NewbieGuideClickSingleCombat>();

            case NewbieGuideScriptType.clickPlayerSkillBtn:
                return gameObject.AddComponent<NewbieGuideClickPlayerSkillBtn>();
        }
        return null;
    }
}

