﻿namespace behaviac
{
    using Assets.Scripts.GameLogic;
    using System;

    internal class Compute_bt_WrapperAI_Hero_HeroWarmSimpleAI_node142 : Compute
    {
        private uint opr2_p0 = 40;

        protected override EBTStatus update_impl(Agent pAgent, EBTStatus childStatus)
        {
            EBTStatus status = EBTStatus.BT_SUCCESS;
            int num = 180;
            int randomInt = ((BTBaseAgent) pAgent).GetRandomInt(this.opr2_p0);
            int num3 = num + randomInt;
            pAgent.SetVariable<int>("p_startWaitFrames", num3, 0xd04a5f43);
            return status;
        }
    }
}
