﻿namespace CSProtocol
{
    using Assets.Scripts.Common;
    using System;
    using tsf4g_tdr_csharp;

    public class COMDT_APOLLO_OPT_PRESENT : ProtocolObject
    {
        public static readonly uint BASEVERSION = 1;
        public byte bRealCoupons;
        public static readonly int CLASS_ID = 0x193;
        public static readonly uint CURRVERSION = 1;
        public uint dwCouponsCnt;
        public COMDT_GAME_EVENTINFO stEventInfo = ((COMDT_GAME_EVENTINFO) ProtocolObjectPool.Get(COMDT_GAME_EVENTINFO.CLASS_ID));

        public override TdrError.ErrorType construct()
        {
            return TdrError.ErrorType.TDR_NO_ERROR;
        }

        public override int GetClassID()
        {
            return CLASS_ID;
        }

        public override void OnRelease()
        {
            this.dwCouponsCnt = 0;
            this.bRealCoupons = 0;
            if (this.stEventInfo != null)
            {
                this.stEventInfo.Release();
                this.stEventInfo = null;
            }
        }

        public override void OnUse()
        {
            this.stEventInfo = (COMDT_GAME_EVENTINFO) ProtocolObjectPool.Get(COMDT_GAME_EVENTINFO.CLASS_ID);
        }

        public override TdrError.ErrorType pack(ref TdrWriteBuf destBuf, uint cutVer)
        {
            TdrError.ErrorType type = TdrError.ErrorType.TDR_NO_ERROR;
            if ((cutVer == 0) || (CURRVERSION < cutVer))
            {
                cutVer = CURRVERSION;
            }
            if (BASEVERSION > cutVer)
            {
                return TdrError.ErrorType.TDR_ERR_CUTVER_TOO_SMALL;
            }
            type = destBuf.writeUInt32(this.dwCouponsCnt);
            if (type == TdrError.ErrorType.TDR_NO_ERROR)
            {
                type = destBuf.writeUInt8(this.bRealCoupons);
                if (type != TdrError.ErrorType.TDR_NO_ERROR)
                {
                    return type;
                }
                type = this.stEventInfo.pack(ref destBuf, cutVer);
                if (type != TdrError.ErrorType.TDR_NO_ERROR)
                {
                    return type;
                }
            }
            return type;
        }

        public TdrError.ErrorType pack(ref byte[] buffer, int size, ref int usedSize, uint cutVer)
        {
            if (((buffer == null) || (buffer.GetLength(0) == 0)) || (size > buffer.GetLength(0)))
            {
                return TdrError.ErrorType.TDR_ERR_INVALID_BUFFER_PARAMETER;
            }
            TdrWriteBuf destBuf = ClassObjPool<TdrWriteBuf>.Get();
            destBuf.set(ref buffer, size);
            TdrError.ErrorType type = this.pack(ref destBuf, cutVer);
            if (type == TdrError.ErrorType.TDR_NO_ERROR)
            {
                buffer = destBuf.getBeginPtr();
                usedSize = destBuf.getUsedSize();
            }
            destBuf.Release();
            return type;
        }

        public override TdrError.ErrorType unpack(ref TdrReadBuf srcBuf, uint cutVer)
        {
            TdrError.ErrorType type = TdrError.ErrorType.TDR_NO_ERROR;
            if ((cutVer == 0) || (CURRVERSION < cutVer))
            {
                cutVer = CURRVERSION;
            }
            if (BASEVERSION > cutVer)
            {
                return TdrError.ErrorType.TDR_ERR_CUTVER_TOO_SMALL;
            }
            type = srcBuf.readUInt32(ref this.dwCouponsCnt);
            if (type == TdrError.ErrorType.TDR_NO_ERROR)
            {
                type = srcBuf.readUInt8(ref this.bRealCoupons);
                if (type != TdrError.ErrorType.TDR_NO_ERROR)
                {
                    return type;
                }
                type = this.stEventInfo.unpack(ref srcBuf, cutVer);
                if (type != TdrError.ErrorType.TDR_NO_ERROR)
                {
                    return type;
                }
            }
            return type;
        }

        public TdrError.ErrorType unpack(ref byte[] buffer, int size, ref int usedSize, uint cutVer)
        {
            if (((buffer == null) || (buffer.GetLength(0) == 0)) || (size > buffer.GetLength(0)))
            {
                return TdrError.ErrorType.TDR_ERR_INVALID_BUFFER_PARAMETER;
            }
            TdrReadBuf srcBuf = ClassObjPool<TdrReadBuf>.Get();
            srcBuf.set(ref buffer, size);
            TdrError.ErrorType type = this.unpack(ref srcBuf, cutVer);
            usedSize = srcBuf.getUsedSize();
            srcBuf.Release();
            return type;
        }
    }
}

