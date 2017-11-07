﻿namespace Assets.Scripts.GameLogic
{
    using ResData;

    [SkillBaseSelectTarget(SkillTargetRule.MyDirection)]
    public class SkillSelectMyDirection : SkillBaseSelectTarget
    {
        public override ActorRoot SelectTarget(SkillSlot UseSlot)
        {
            return Singleton<TargetSearcher>.GetInstance().GetNearestEnemy((ActorRoot) UseSlot.Actor, UseSlot.SkillObj.cfgData.iMaxSearchDistance, TargetPriority.TargetPriority_Hero, UseSlot.SkillObj.cfgData.dwSkillTargetFilter);
        }

        public override VInt3 SelectTargetDir(SkillSlot UseSlot)
        {
            return UseSlot.Actor.handle.forward;
        }
    }
}

