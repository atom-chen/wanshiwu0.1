﻿namespace CSProtocol
{
    using Assets.Scripts.Common;
    using System;
    using tsf4g_tdr_csharp;

    public class COMDT_FREEHERO_LIST : ProtocolObject
    {
        public static readonly uint BASEVERSION = 1;
        public static readonly int CLASS_ID = 0x67;
        public static readonly uint CURRVERSION = 1;
        public uint[] FreeHeroID = new uint[200];
        public ushort wFreeCnt;

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
            this.wFreeCnt = 0;
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
            type = destBuf.writeUInt16(this.wFreeCnt);
            if (type == TdrError.ErrorType.TDR_NO_ERROR)
            {
                if (200 < this.wFreeCnt)
                {
                    return TdrError.ErrorType.TDR_ERR_REFER_SURPASS_COUNT;
                }
                if (this.FreeHeroID.Length < this.wFreeCnt)
                {
                    return TdrError.ErrorType.TDR_ERR_VAR_ARRAY_CONFLICT;
                }
                for (int i = 0; i < this.wFreeCnt; i++)
                {
                    type = destBuf.writeUInt32(this.FreeHeroID[i]);
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
            type = srcBuf.readUInt16(ref this.wFreeCnt);
            if (type == TdrError.ErrorType.TDR_NO_ERROR)
            {
                if (200 < this.wFreeCnt)
                {
                    return TdrError.ErrorType.TDR_ERR_REFER_SURPASS_COUNT;
                }
                this.FreeHeroID = new uint[this.wFreeCnt];
                for (int i = 0; i < this.wFreeCnt; i++)
                {
                    type = srcBuf.readUInt32(ref this.FreeHeroID[i]);
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

