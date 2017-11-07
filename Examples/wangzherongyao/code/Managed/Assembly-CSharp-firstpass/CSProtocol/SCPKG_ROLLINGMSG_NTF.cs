﻿namespace CSProtocol
{
    using Assets.Scripts.Common;
    using System;
    using tsf4g_tdr_csharp;

    public class SCPKG_ROLLINGMSG_NTF : ProtocolObject
    {
        public CSDT_ROLLING_MSG[] astRollingMsg = new CSDT_ROLLING_MSG[50];
        public static readonly uint BASEVERSION = 1;
        public byte bMsgCnt;
        public static readonly int CLASS_ID = 0x34c;
        public static readonly uint CURRVERSION = 1;

        public SCPKG_ROLLINGMSG_NTF()
        {
            for (int i = 0; i < 50; i++)
            {
                this.astRollingMsg[i] = (CSDT_ROLLING_MSG) ProtocolObjectPool.Get(CSDT_ROLLING_MSG.CLASS_ID);
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
            this.bMsgCnt = 0;
            if (this.astRollingMsg != null)
            {
                for (int i = 0; i < this.astRollingMsg.Length; i++)
                {
                    if (this.astRollingMsg[i] != null)
                    {
                        this.astRollingMsg[i].Release();
                        this.astRollingMsg[i] = null;
                    }
                }
            }
        }

        public override void OnUse()
        {
            if (this.astRollingMsg != null)
            {
                for (int i = 0; i < this.astRollingMsg.Length; i++)
                {
                    this.astRollingMsg[i] = (CSDT_ROLLING_MSG) ProtocolObjectPool.Get(CSDT_ROLLING_MSG.CLASS_ID);
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
            type = destBuf.writeUInt8(this.bMsgCnt);
            if (type == TdrError.ErrorType.TDR_NO_ERROR)
            {
                if (50 < this.bMsgCnt)
                {
                    return TdrError.ErrorType.TDR_ERR_REFER_SURPASS_COUNT;
                }
                if (this.astRollingMsg.Length < this.bMsgCnt)
                {
                    return TdrError.ErrorType.TDR_ERR_VAR_ARRAY_CONFLICT;
                }
                for (int i = 0; i < this.bMsgCnt; i++)
                {
                    type = this.astRollingMsg[i].pack(ref destBuf, cutVer);
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
            type = srcBuf.readUInt8(ref this.bMsgCnt);
            if (type == TdrError.ErrorType.TDR_NO_ERROR)
            {
                if (50 < this.bMsgCnt)
                {
                    return TdrError.ErrorType.TDR_ERR_REFER_SURPASS_COUNT;
                }
                for (int i = 0; i < this.bMsgCnt; i++)
                {
                    type = this.astRollingMsg[i].unpack(ref srcBuf, cutVer);
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

