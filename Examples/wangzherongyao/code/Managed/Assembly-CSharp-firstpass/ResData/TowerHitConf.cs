﻿namespace ResData
{
    using System;
    using tsf4g_tdr_csharp;

    public class TowerHitConf : IUnpackable, tsf4g_csharp_interface
    {
        public static readonly uint BASEVERSION = 1;
        public byte bOrganType;
        public static readonly uint CURRVERSION = 1;
        public uint dwCdTime;
        public uint dwLastTime;
        public static readonly uint LENGTH_szEffect = 0x40;
        public static readonly uint LENGTH_szVoice = 0x40;
        public string szEffect = string.Empty;
        public byte[] szEffect_ByteArray = new byte[1];
        public string szVoice = string.Empty;
        public byte[] szVoice_ByteArray = new byte[1];

        public TdrError.ErrorType construct()
        {
            return TdrError.ErrorType.TDR_NO_ERROR;
        }

        public TdrError.ErrorType load(ref TdrReadBuf srcBuf, uint cutVer)
        {
            srcBuf.disableEndian();
            TdrError.ErrorType type = TdrError.ErrorType.TDR_NO_ERROR;
            if ((cutVer == 0) || (CURRVERSION < cutVer))
            {
                cutVer = CURRVERSION;
            }
            if (BASEVERSION > cutVer)
            {
                return TdrError.ErrorType.TDR_ERR_CUTVER_TOO_SMALL;
            }
            type = srcBuf.readUInt8(ref this.bOrganType);
            if (type == TdrError.ErrorType.TDR_NO_ERROR)
            {
                type = srcBuf.readUInt32(ref this.dwCdTime);
                if (type != TdrError.ErrorType.TDR_NO_ERROR)
                {
                    return type;
                }
                int count = 0x40;
                if (this.szVoice_ByteArray.GetLength(0) < count)
                {
                    this.szVoice_ByteArray = new byte[LENGTH_szVoice];
                }
                type = srcBuf.readCString(ref this.szVoice_ByteArray, count);
                if (type != TdrError.ErrorType.TDR_NO_ERROR)
                {
                    return type;
                }
                int num2 = 0x40;
                if (this.szEffect_ByteArray.GetLength(0) < num2)
                {
                    this.szEffect_ByteArray = new byte[LENGTH_szEffect];
                }
                type = srcBuf.readCString(ref this.szEffect_ByteArray, num2);
                if (type != TdrError.ErrorType.TDR_NO_ERROR)
                {
                    return type;
                }
                type = srcBuf.readUInt32(ref this.dwLastTime);
                if (type != TdrError.ErrorType.TDR_NO_ERROR)
                {
                    return type;
                }
                this.TransferData();
            }
            return type;
        }

        public TdrError.ErrorType load(ref byte[] buffer, int size, ref int usedSize, uint cutVer)
        {
            if (((buffer == null) || (buffer.GetLength(0) == 0)) || (size > buffer.GetLength(0)))
            {
                return TdrError.ErrorType.TDR_ERR_INVALID_BUFFER_PARAMETER;
            }
            TdrReadBuf srcBuf = new TdrReadBuf(ref buffer, size);
            TdrError.ErrorType type = this.load(ref srcBuf, cutVer);
            usedSize = srcBuf.getUsedSize();
            return type;
        }

        private void TransferData()
        {
            this.szVoice = StringHelper.UTF8BytesToString(ref this.szVoice_ByteArray);
            this.szVoice_ByteArray = null;
            this.szEffect = StringHelper.UTF8BytesToString(ref this.szEffect_ByteArray);
            this.szEffect_ByteArray = null;
        }

        public TdrError.ErrorType unpack(ref TdrReadBuf srcBuf, uint cutVer)
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
            type = srcBuf.readUInt8(ref this.bOrganType);
            if (type == TdrError.ErrorType.TDR_NO_ERROR)
            {
                type = srcBuf.readUInt32(ref this.dwCdTime);
                if (type != TdrError.ErrorType.TDR_NO_ERROR)
                {
                    return type;
                }
                uint dest = 0;
                type = srcBuf.readUInt32(ref dest);
                if (type != TdrError.ErrorType.TDR_NO_ERROR)
                {
                    return type;
                }
                if (dest > srcBuf.getLeftSize())
                {
                    return TdrError.ErrorType.TDR_ERR_SHORT_BUF_FOR_READ;
                }
                if (dest > this.szVoice_ByteArray.GetLength(0))
                {
                    if (dest > LENGTH_szVoice)
                    {
                        return TdrError.ErrorType.TDR_ERR_STR_LEN_TOO_BIG;
                    }
                    this.szVoice_ByteArray = new byte[dest];
                }
                if (1 > dest)
                {
                    return TdrError.ErrorType.TDR_ERR_STR_LEN_TOO_SMALL;
                }
                type = srcBuf.readCString(ref this.szVoice_ByteArray, (int) dest);
                if (type != TdrError.ErrorType.TDR_NO_ERROR)
                {
                    return type;
                }
                if (this.szVoice_ByteArray[((int) dest) - 1] != 0)
                {
                    return TdrError.ErrorType.TDR_ERR_STR_LEN_CONFLICT;
                }
                int num2 = TdrTypeUtil.cstrlen(this.szVoice_ByteArray) + 1;
                if (dest != num2)
                {
                    return TdrError.ErrorType.TDR_ERR_STR_LEN_CONFLICT;
                }
                uint num3 = 0;
                type = srcBuf.readUInt32(ref num3);
                if (type != TdrError.ErrorType.TDR_NO_ERROR)
                {
                    return type;
                }
                if (num3 > srcBuf.getLeftSize())
                {
                    return TdrError.ErrorType.TDR_ERR_SHORT_BUF_FOR_READ;
                }
                if (num3 > this.szEffect_ByteArray.GetLength(0))
                {
                    if (num3 > LENGTH_szEffect)
                    {
                        return TdrError.ErrorType.TDR_ERR_STR_LEN_TOO_BIG;
                    }
                    this.szEffect_ByteArray = new byte[num3];
                }
                if (1 > num3)
                {
                    return TdrError.ErrorType.TDR_ERR_STR_LEN_TOO_SMALL;
                }
                type = srcBuf.readCString(ref this.szEffect_ByteArray, (int) num3);
                if (type == TdrError.ErrorType.TDR_NO_ERROR)
                {
                    if (this.szEffect_ByteArray[((int) num3) - 1] != 0)
                    {
                        return TdrError.ErrorType.TDR_ERR_STR_LEN_CONFLICT;
                    }
                    int num4 = TdrTypeUtil.cstrlen(this.szEffect_ByteArray) + 1;
                    if (num3 != num4)
                    {
                        return TdrError.ErrorType.TDR_ERR_STR_LEN_CONFLICT;
                    }
                    type = srcBuf.readUInt32(ref this.dwLastTime);
                    if (type != TdrError.ErrorType.TDR_NO_ERROR)
                    {
                        return type;
                    }
                    this.TransferData();
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
            TdrReadBuf srcBuf = new TdrReadBuf(ref buffer, size);
            TdrError.ErrorType type = this.unpack(ref srcBuf, cutVer);
            usedSize = srcBuf.getUsedSize();
            return type;
        }
    }
}

