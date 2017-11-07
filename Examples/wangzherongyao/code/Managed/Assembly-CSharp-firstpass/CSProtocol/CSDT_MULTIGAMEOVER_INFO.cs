﻿namespace CSProtocol
{
    using Assets.Scripts.Common;
    using System;
    using tsf4g_tdr_csharp;

    public class CSDT_MULTIGAMEOVER_INFO : ProtocolObject
    {
        public static readonly uint BASEVERSION = 1;
        public static readonly int CLASS_ID = 0x239;
        public static readonly uint CURRVERSION = 1;
        public uint dwAcntObjID;
        public COMDT_MULTI_GAME_PARAM stBattleParam = ((COMDT_MULTI_GAME_PARAM) ProtocolObjectPool.Get(COMDT_MULTI_GAME_PARAM.CLASS_ID));
        public COMDT_SETTLE_COMMON_DATA stCommonData = ((COMDT_SETTLE_COMMON_DATA) ProtocolObjectPool.Get(COMDT_SETTLE_COMMON_DATA.CLASS_ID));

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
            this.dwAcntObjID = 0;
            if (this.stBattleParam != null)
            {
                this.stBattleParam.Release();
                this.stBattleParam = null;
            }
            if (this.stCommonData != null)
            {
                this.stCommonData.Release();
                this.stCommonData = null;
            }
        }

        public override void OnUse()
        {
            this.stBattleParam = (COMDT_MULTI_GAME_PARAM) ProtocolObjectPool.Get(COMDT_MULTI_GAME_PARAM.CLASS_ID);
            this.stCommonData = (COMDT_SETTLE_COMMON_DATA) ProtocolObjectPool.Get(COMDT_SETTLE_COMMON_DATA.CLASS_ID);
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
            type = destBuf.writeUInt32(this.dwAcntObjID);
            if (type == TdrError.ErrorType.TDR_NO_ERROR)
            {
                type = this.stBattleParam.pack(ref destBuf, cutVer);
                if (type != TdrError.ErrorType.TDR_NO_ERROR)
                {
                    return type;
                }
                type = this.stCommonData.pack(ref destBuf, cutVer);
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
            type = srcBuf.readUInt32(ref this.dwAcntObjID);
            if (type == TdrError.ErrorType.TDR_NO_ERROR)
            {
                type = this.stBattleParam.unpack(ref srcBuf, cutVer);
                if (type != TdrError.ErrorType.TDR_NO_ERROR)
                {
                    return type;
                }
                type = this.stCommonData.unpack(ref srcBuf, cutVer);
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

