﻿namespace CSProtocol
{
    using Assets.Scripts.Common;
    using System;
    using tsf4g_tdr_csharp;

    public class COMDT_ARENA_FIGHTER_DETAIL : ProtocolObject
    {
        public static readonly uint BASEVERSION = 1;
        public static readonly int CLASS_ID = 0x1ac;
        public static readonly uint CURRVERSION = 0x43;
        public uint dwRank;
        public COMDT_ARENA_BLOCKDATA stFigterData = ((COMDT_ARENA_BLOCKDATA) ProtocolObjectPool.Get(COMDT_ARENA_BLOCKDATA.CLASS_ID));

        public override TdrError.ErrorType construct()
        {
            TdrError.ErrorType type = TdrError.ErrorType.TDR_NO_ERROR;
            this.dwRank = 0;
            type = this.stFigterData.construct();
            if (type != TdrError.ErrorType.TDR_NO_ERROR)
            {
                return type;
            }
            return type;
        }

        public override int GetClassID()
        {
            return CLASS_ID;
        }

        public override void OnRelease()
        {
            this.dwRank = 0;
            if (this.stFigterData != null)
            {
                this.stFigterData.Release();
                this.stFigterData = null;
            }
        }

        public override void OnUse()
        {
            this.stFigterData = (COMDT_ARENA_BLOCKDATA) ProtocolObjectPool.Get(COMDT_ARENA_BLOCKDATA.CLASS_ID);
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
            type = destBuf.writeUInt32(this.dwRank);
            if (type == TdrError.ErrorType.TDR_NO_ERROR)
            {
                type = this.stFigterData.pack(ref destBuf, cutVer);
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
            type = srcBuf.readUInt32(ref this.dwRank);
            if (type == TdrError.ErrorType.TDR_NO_ERROR)
            {
                type = this.stFigterData.unpack(ref srcBuf, cutVer);
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

