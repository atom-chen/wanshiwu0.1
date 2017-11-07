﻿namespace CSProtocol
{
    using Assets.Scripts.Common;
    using System;
    using tsf4g_tdr_csharp;

    public class COMDT_TEAM_INFO : ProtocolObject
    {
        public static readonly uint BASEVERSION = 1;
        public static readonly int CLASS_ID = 0x102;
        public static readonly uint CURRVERSION = 0x5c;
        public COMDT_TEAMMEMBER stMemInfo = ((COMDT_TEAMMEMBER) ProtocolObjectPool.Get(COMDT_TEAMMEMBER.CLASS_ID));
        public COMDT_TEAM_MEMBER_UNIQ stSelfInfo = ((COMDT_TEAM_MEMBER_UNIQ) ProtocolObjectPool.Get(COMDT_TEAM_MEMBER_UNIQ.CLASS_ID));
        public COMDT_TEAM_BASE stTeamInfo = ((COMDT_TEAM_BASE) ProtocolObjectPool.Get(COMDT_TEAM_BASE.CLASS_ID));
        public COMDT_TEAM_MEMBER_UNIQ stTeamMaster = ((COMDT_TEAM_MEMBER_UNIQ) ProtocolObjectPool.Get(COMDT_TEAM_MEMBER_UNIQ.CLASS_ID));

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
            if (this.stSelfInfo != null)
            {
                this.stSelfInfo.Release();
                this.stSelfInfo = null;
            }
            if (this.stTeamMaster != null)
            {
                this.stTeamMaster.Release();
                this.stTeamMaster = null;
            }
            if (this.stTeamInfo != null)
            {
                this.stTeamInfo.Release();
                this.stTeamInfo = null;
            }
            if (this.stMemInfo != null)
            {
                this.stMemInfo.Release();
                this.stMemInfo = null;
            }
        }

        public override void OnUse()
        {
            this.stSelfInfo = (COMDT_TEAM_MEMBER_UNIQ) ProtocolObjectPool.Get(COMDT_TEAM_MEMBER_UNIQ.CLASS_ID);
            this.stTeamMaster = (COMDT_TEAM_MEMBER_UNIQ) ProtocolObjectPool.Get(COMDT_TEAM_MEMBER_UNIQ.CLASS_ID);
            this.stTeamInfo = (COMDT_TEAM_BASE) ProtocolObjectPool.Get(COMDT_TEAM_BASE.CLASS_ID);
            this.stMemInfo = (COMDT_TEAMMEMBER) ProtocolObjectPool.Get(COMDT_TEAMMEMBER.CLASS_ID);
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
            type = this.stSelfInfo.pack(ref destBuf, cutVer);
            if (type == TdrError.ErrorType.TDR_NO_ERROR)
            {
                type = this.stTeamMaster.pack(ref destBuf, cutVer);
                if (type != TdrError.ErrorType.TDR_NO_ERROR)
                {
                    return type;
                }
                type = this.stTeamInfo.pack(ref destBuf, cutVer);
                if (type != TdrError.ErrorType.TDR_NO_ERROR)
                {
                    return type;
                }
                type = this.stMemInfo.pack(ref destBuf, cutVer);
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
            type = this.stSelfInfo.unpack(ref srcBuf, cutVer);
            if (type == TdrError.ErrorType.TDR_NO_ERROR)
            {
                type = this.stTeamMaster.unpack(ref srcBuf, cutVer);
                if (type != TdrError.ErrorType.TDR_NO_ERROR)
                {
                    return type;
                }
                type = this.stTeamInfo.unpack(ref srcBuf, cutVer);
                if (type != TdrError.ErrorType.TDR_NO_ERROR)
                {
                    return type;
                }
                type = this.stMemInfo.unpack(ref srcBuf, cutVer);
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

