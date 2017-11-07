﻿namespace CSProtocol
{
    using Assets.Scripts.Common;
    using System;
    using tsf4g_tdr_csharp;

    public class COMDT_REWARD_INFO : ProtocolObject
    {
        public static readonly uint BASEVERSION = 1;
        public byte bFromType;
        public byte bType;
        public static readonly int CLASS_ID = 0xb1;
        public static readonly uint CURRVERSION = 1;
        public COMDT_REWARDS_FROM stFromInfo = ((COMDT_REWARDS_FROM) ProtocolObjectPool.Get(COMDT_REWARDS_FROM.CLASS_ID));
        public COMDT_REWARDS_UNION stRewardInfo = ((COMDT_REWARDS_UNION) ProtocolObjectPool.Get(COMDT_REWARDS_UNION.CLASS_ID));

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
            this.bType = 0;
            if (this.stRewardInfo != null)
            {
                this.stRewardInfo.Release();
                this.stRewardInfo = null;
            }
            this.bFromType = 0;
            if (this.stFromInfo != null)
            {
                this.stFromInfo.Release();
                this.stFromInfo = null;
            }
        }

        public override void OnUse()
        {
            this.stRewardInfo = (COMDT_REWARDS_UNION) ProtocolObjectPool.Get(COMDT_REWARDS_UNION.CLASS_ID);
            this.stFromInfo = (COMDT_REWARDS_FROM) ProtocolObjectPool.Get(COMDT_REWARDS_FROM.CLASS_ID);
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
            type = destBuf.writeUInt8(this.bType);
            if (type == TdrError.ErrorType.TDR_NO_ERROR)
            {
                long bType = this.bType;
                type = this.stRewardInfo.pack(bType, ref destBuf, cutVer);
                if (type != TdrError.ErrorType.TDR_NO_ERROR)
                {
                    return type;
                }
                type = destBuf.writeUInt8(this.bFromType);
                if (type != TdrError.ErrorType.TDR_NO_ERROR)
                {
                    return type;
                }
                long bFromType = this.bFromType;
                type = this.stFromInfo.pack(bFromType, ref destBuf, cutVer);
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
            type = srcBuf.readUInt8(ref this.bType);
            if (type == TdrError.ErrorType.TDR_NO_ERROR)
            {
                long bType = this.bType;
                type = this.stRewardInfo.unpack(bType, ref srcBuf, cutVer);
                if (type != TdrError.ErrorType.TDR_NO_ERROR)
                {
                    return type;
                }
                type = srcBuf.readUInt8(ref this.bFromType);
                if (type != TdrError.ErrorType.TDR_NO_ERROR)
                {
                    return type;
                }
                long bFromType = this.bFromType;
                type = this.stFromInfo.unpack(bFromType, ref srcBuf, cutVer);
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

