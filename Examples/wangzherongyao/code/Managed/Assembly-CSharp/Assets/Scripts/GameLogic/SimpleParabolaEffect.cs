﻿namespace Assets.Scripts.GameLogic
{
    using System;
    using System.Runtime.CompilerServices;

    public class SimpleParabolaEffect : IDropDownEffect
    {
        private bool bIsFinished;
        private int Height;
        private int Total;

        public SimpleParabolaEffect(VInt3 InStartPos, VInt3 InEndPos)
        {
            this.StartPos = InStartPos;
            this.EndPos = InEndPos;
            this.TimeDelta = 0;
            this.Total = MonoSingleton<GlobalConfig>.instance.DropItemFlyTime;
            this.Height = MonoSingleton<GlobalConfig>.instance.DropItemFlyHeight;
            DebugHelper.Assert(this.Total > 0);
            this.bIsFinished = false;
        }

        public void Bind(DropItem item)
        {
            this.Item = item;
            DebugHelper.Assert(this.Item != null);
            this.Item.SetLocation(this.StartPos);
            this.Current = this.StartPos;
        }

        private void Finish()
        {
            this.Current = this.EndPos;
            this.bIsFinished = true;
            if (this.Item != null)
            {
                this.Item.SetLocation(this.EndPos);
            }
        }

        public void OnUpdate(int delta)
        {
            this.TimeDelta += delta;
            if (this.TimeDelta >= this.Total)
            {
                this.Finish();
            }
            else
            {
                int num = IntMath.Lerp(this.StartPos.x, this.EndPos.x, this.TimeDelta, this.Total);
                int num2 = IntMath.Lerp(this.StartPos.z, this.EndPos.z, this.TimeDelta, this.Total);
                int num3 = 0;
                if ((this.TimeDelta << 1) < this.Total)
                {
                    num3 = IntMath.Lerp(this.StartPos.y, this.StartPos.y + this.Height, this.TimeDelta << 1, this.Total);
                }
                else
                {
                    num3 = IntMath.Lerp(this.StartPos.y + this.Height, this.EndPos.y, (this.TimeDelta << 1) - this.Total, this.Total);
                }
                this.Current = new VInt3(num, num3, num2);
                if (this.Item != null)
                {
                    this.Item.SetLocation(this.Current);
                }
            }
        }

        public VInt3 Current { get; protected set; }

        public VInt3 EndPos { get; protected set; }

        public bool isFinished
        {
            get
            {
                return this.bIsFinished;
            }
        }

        public DropItem Item { get; protected set; }

        public VInt3 location
        {
            get
            {
                return this.Current;
            }
        }

        public VInt3 StartPos { get; protected set; }

        public int TimeDelta { get; protected set; }
    }
}

