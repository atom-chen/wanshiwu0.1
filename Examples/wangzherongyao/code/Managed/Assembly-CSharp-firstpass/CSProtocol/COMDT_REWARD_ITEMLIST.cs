﻿namespace CSProtocol
{
    using Assets.Scripts.Common;
    using System;
    using tsf4g_tdr_csharp;

    public class COMDT_REWARD_ITEMLIST : ProtocolObject
    {
        public COMDT_REWARD_ITEMOBJ[] astRewardList = new COMDT_REWARD_ITEMOBJ[20];
        public static readonly uint BASEVERSION = 1;
        public static readonly int CLASS_ID = 0x1e8;
        public static readonly uint CURRVERSION = 1;
        public ushort wRewardCnt;

        public COMDT_REWARD_ITEMLIST()
        {
            for (int i = 0; i < 20; i++)
            {
                this.astRewardList[i] = (COMDT_REWARD_ITEMOBJ) ProtocolObjectPool.Get(COMDT_REWARD_ITEMOBJ.CLASS_ID);
            }
        }

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
            this.wRewardCnt = 0;
            if (this.astRewardList != null)
            {
                for (int i = 0; i < this.astRewardList.Length; i++)
                {
                    if (this.astRewardList[i] != null)
                    {
                        this.astRewardList[i].Release();
                        this.astRewardList[i] = null;
                    }
                }
            }
        }

        public override void OnUse()
        {
            if (this.astRewardList != null)
            {
                for (int i = 0; i < this.astRewardList.Length; i++)
                {
                    this.astRewardList[i] = (COMDT_REWARD_ITEMOBJ) ProtocolObjectPool.Get(COMDT_REWARD_ITEMOBJ.CLASS_ID);
                }
            }
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
            type = destBuf.writeUInt16(this.wRewardCnt);
            if (type == TdrError.ErrorType.TDR_NO_ERROR)
            {
                if (20 < this.wRewardCnt)
                {
                    return TdrError.ErrorType.TDR_ERR_REFER_SURPASS_COUNT;
                }
                if (this.astRewardList.Length < this.wRewardCnt)
                {
                    return TdrError.ErrorType.TDR_ERR_VAR_ARRAY_CONFLICT;
                }
                for (int i = 0; i < this.wRewardCnt; i++)
                {
                    type = this.astRewardList[i].pack(ref destBuf, cutVer);
                    if (type != TdrError.ErrorType.TDR_NO_ERROR)
                    {
                        return type;
                    }
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
            type = srcBuf.readUInt16(ref this.wRewardCnt);
            if (type == TdrError.ErrorType.TDR_NO_ERROR)
            {
                if (20 < this.wRewardCnt)
                {
                    return TdrError.ErrorType.TDR_ERR_REFER_SURPASS_COUNT;
                }
                for (int i = 0; i < this.wRewardCnt; i++)
                {
                    type = this.astRewardList[i].unpack(ref srcBuf, cutVer);
                    if (type != TdrError.ErrorType.TDR_NO_ERROR)
                    {
                        return type;
                    }
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

