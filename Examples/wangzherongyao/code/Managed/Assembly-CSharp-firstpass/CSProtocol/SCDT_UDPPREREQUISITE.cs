﻿namespace CSProtocol
{
    using Assets.Scripts.Common;
    using System;
    using tsf4g_tdr_csharp;

    public class SCDT_UDPPREREQUISITE : ProtocolObject
    {
        public COMDT_PREREQUISITE_DETAIL[] astPrerequisiteInfo = new COMDT_PREREQUISITE_DETAIL[2];
        public static readonly uint BASEVERSION = 1;
        public byte bPrerequisiteNum;
        public byte bTaskState;
        public static readonly int CLASS_ID = 0x31f;
        public static readonly uint CURRVERSION = 1;
        public uint dwTaskID;

        public SCDT_UDPPREREQUISITE()
        {
            for (int i = 0; i < 2; i++)
            {
                this.astPrerequisiteInfo[i] = (COMDT_PREREQUISITE_DETAIL) ProtocolObjectPool.Get(COMDT_PREREQUISITE_DETAIL.CLASS_ID);
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
            this.dwTaskID = 0;
            this.bTaskState = 0;
            this.bPrerequisiteNum = 0;
            if (this.astPrerequisiteInfo != null)
            {
                for (int i = 0; i < this.astPrerequisiteInfo.Length; i++)
                {
                    if (this.astPrerequisiteInfo[i] != null)
                    {
                        this.astPrerequisiteInfo[i].Release();
                        this.astPrerequisiteInfo[i] = null;
                    }
                }
            }
        }

        public override void OnUse()
        {
            if (this.astPrerequisiteInfo != null)
            {
                for (int i = 0; i < this.astPrerequisiteInfo.Length; i++)
                {
                    this.astPrerequisiteInfo[i] = (COMDT_PREREQUISITE_DETAIL) ProtocolObjectPool.Get(COMDT_PREREQUISITE_DETAIL.CLASS_ID);
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
            type = destBuf.writeUInt32(this.dwTaskID);
            if (type == TdrError.ErrorType.TDR_NO_ERROR)
            {
                type = destBuf.writeUInt8(this.bTaskState);
                if (type != TdrError.ErrorType.TDR_NO_ERROR)
                {
                    return type;
                }
                type = destBuf.writeUInt8(this.bPrerequisiteNum);
                if (type != TdrError.ErrorType.TDR_NO_ERROR)
                {
                    return type;
                }
                if (2 < this.bPrerequisiteNum)
                {
                    return TdrError.ErrorType.TDR_ERR_REFER_SURPASS_COUNT;
                }
                if (this.astPrerequisiteInfo.Length < this.bPrerequisiteNum)
                {
                    return TdrError.ErrorType.TDR_ERR_VAR_ARRAY_CONFLICT;
                }
                for (int i = 0; i < this.bPrerequisiteNum; i++)
                {
                    type = this.astPrerequisiteInfo[i].pack(ref destBuf, cutVer);
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
            type = srcBuf.readUInt32(ref this.dwTaskID);
            if (type == TdrError.ErrorType.TDR_NO_ERROR)
            {
                type = srcBuf.readUInt8(ref this.bTaskState);
                if (type != TdrError.ErrorType.TDR_NO_ERROR)
                {
                    return type;
                }
                type = srcBuf.readUInt8(ref this.bPrerequisiteNum);
                if (type != TdrError.ErrorType.TDR_NO_ERROR)
                {
                    return type;
                }
                if (2 < this.bPrerequisiteNum)
                {
                    return TdrError.ErrorType.TDR_ERR_REFER_SURPASS_COUNT;
                }
                for (int i = 0; i < this.bPrerequisiteNum; i++)
                {
                    type = this.astPrerequisiteInfo[i].unpack(ref srcBuf, cutVer);
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

