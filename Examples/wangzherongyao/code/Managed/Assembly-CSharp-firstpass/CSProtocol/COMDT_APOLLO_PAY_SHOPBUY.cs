﻿namespace CSProtocol
{
    using Assets.Scripts.Common;
    using System;
    using tsf4g_tdr_csharp;

    public class COMDT_APOLLO_PAY_SHOPBUY : ProtocolObject
    {
        public static readonly uint BASEVERSION = 1;
        public static readonly int CLASS_ID = 0x178;
        public static readonly uint CURRVERSION = 1;
        public int iSubType;
        public int iType;
        public COMDT_SHOPBUY_EXTRA stExtraInfo = ((COMDT_SHOPBUY_EXTRA) ProtocolObjectPool.Get(COMDT_SHOPBUY_EXTRA.CLASS_ID));

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
            this.iType = 0;
            this.iSubType = 0;
            if (this.stExtraInfo != null)
            {
                this.stExtraInfo.Release();
                this.stExtraInfo = null;
            }
        }

        public override void OnUse()
        {
            this.stExtraInfo = (COMDT_SHOPBUY_EXTRA) ProtocolObjectPool.Get(COMDT_SHOPBUY_EXTRA.CLASS_ID);
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
            type = destBuf.writeInt32(this.iType);
            if (type == TdrError.ErrorType.TDR_NO_ERROR)
            {
                type = destBuf.writeInt32(this.iSubType);
                if (type != TdrError.ErrorType.TDR_NO_ERROR)
                {
                    return type;
                }
                long iType = this.iType;
                type = this.stExtraInfo.pack(iType, ref destBuf, cutVer);
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
            type = srcBuf.readInt32(ref this.iType);
            if (type == TdrError.ErrorType.TDR_NO_ERROR)
            {
                type = srcBuf.readInt32(ref this.iSubType);
                if (type != TdrError.ErrorType.TDR_NO_ERROR)
                {
                    return type;
                }
                long iType = this.iType;
                type = this.stExtraInfo.unpack(iType, ref srcBuf, cutVer);
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

