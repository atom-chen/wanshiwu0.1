﻿namespace CSProtocol
{
    using Assets.Scripts.Common;
    using System;
    using tsf4g_tdr_csharp;

    public class CSPkgBody : ProtocolObject
    {
        public static readonly uint BASEVERSION = 1;
        public static readonly int CLASS_ID = 0x489;
        public static readonly uint CURRVERSION = 0x62;
        public ProtocolObject dataObject;
        public byte[] szData;

        public TdrError.ErrorType construct(long selector)
        {
            TdrError.ErrorType type = TdrError.ErrorType.TDR_NO_ERROR;
            ProtocolObject obj2 = this.select(selector);
            if (obj2 != null)
            {
                return obj2.construct();
            }
            if (this.szData == null)
            {
                this.szData = new byte[0x3e800];
            }
            return type;
        }

        public override int GetClassID()
        {
            return CLASS_ID;
        }

        public override void OnRelease()
        {
            if (this.dataObject != null)
            {
                this.dataObject.Release();
                this.dataObject = null;
            }
        }

        public TdrError.ErrorType pack(long selector, ref TdrWriteBuf destBuf, uint cutVer)
        {
            if ((cutVer == 0) || (CURRVERSION < cutVer))
            {
                cutVer = CURRVERSION;
            }
            if (BASEVERSION > cutVer)
            {
                return TdrError.ErrorType.TDR_ERR_CUTVER_TOO_SMALL;
            }
            TdrError.ErrorType type = TdrError.ErrorType.TDR_NO_ERROR;
            ProtocolObject obj2 = this.select(selector);
            if (obj2 != null)
            {
                return obj2.pack(ref destBuf, cutVer);
            }
            for (int i = 0; i < 0x3e800; i++)
            {
                type = destBuf.writeUInt8(this.szData[i]);
                if (type != TdrError.ErrorType.TDR_NO_ERROR)
                {
                    return type;
                }
            }
            return type;
        }

        public TdrError.ErrorType pack(long selector, ref byte[] buffer, int size, ref int usedSize, uint cutVer)
        {
            if ((buffer.GetLength(0) == 0) || (size > buffer.GetLength(0)))
            {
                return TdrError.ErrorType.TDR_ERR_INVALID_BUFFER_PARAMETER;
            }
            TdrWriteBuf destBuf = ClassObjPool<TdrWriteBuf>.Get();
            destBuf.set(ref buffer, size);
            TdrError.ErrorType type = this.pack(selector, ref destBuf, cutVer);
            if (type == TdrError.ErrorType.TDR_NO_ERROR)
            {
                buffer = destBuf.getBeginPtr();
                usedSize = destBuf.getUsedSize();
            }
            destBuf.Release();
            return type;
        }

        public ProtocolObject select(long selector)
        {
            if (selector <= 0x43aL)
            {
                this.select_1000_1082(selector);
            }
            else if (selector <= 0x47aL)
            {
                this.select_1083_1146(selector);
            }
            else if (selector <= 0x4d0L)
            {
                this.select_1147_1232(selector);
            }
            else if (selector <= 0x708L)
            {
                this.select_1233_1800(selector);
            }
            else if (selector <= 0x899L)
            {
                this.select_1801_2201(selector);
            }
            else if (selector <= 0x8d5L)
            {
                this.select_2202_2261(selector);
            }
            else if (selector <= 0xaf1L)
            {
                this.select_2262_2801(selector);
            }
            else if (selector <= 0x11fcL)
            {
                this.select_2802_4604(selector);
            }
            else if (selector <= 0x1457L)
            {
                this.select_4605_5207(selector);
            }
            else if (this.dataObject != null)
            {
                this.dataObject.Release();
                this.dataObject = null;
            }
            return this.dataObject;
        }

        private void select_1000_1082(long selector)
        {
            long num = selector;
            if ((num >= 0x3e8L) && (num <= 0x43aL))
            {
                switch (((int) (num - 0x3e8L)))
                {
                    case 0:
                        if (!(this.dataObject is CSPKG_CMD_HEARTBEAT))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_HEARTBEAT) ProtocolObjectPool.Get(CSPKG_CMD_HEARTBEAT.CLASS_ID);
                        }
                        return;

                    case 1:
                        if (!(this.dataObject is SCPKG_GAMELOGINDISPATCH))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GAMELOGINDISPATCH) ProtocolObjectPool.Get(SCPKG_GAMELOGINDISPATCH.CLASS_ID);
                        }
                        return;

                    case 2:
                        if (!(this.dataObject is CSPKG_CMD_GAMELOGINREQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_GAMELOGINREQ) ProtocolObjectPool.Get(CSPKG_CMD_GAMELOGINREQ.CLASS_ID);
                        }
                        return;

                    case 3:
                        if (!(this.dataObject is SCPKG_CMD_GAMELOGINRSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_GAMELOGINRSP) ProtocolObjectPool.Get(SCPKG_CMD_GAMELOGINRSP.CLASS_ID);
                        }
                        return;

                    case 4:
                        if (!(this.dataObject is CSPKG_GAMING_CSSYNC))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GAMING_CSSYNC) ProtocolObjectPool.Get(CSPKG_GAMING_CSSYNC.CLASS_ID);
                        }
                        return;

                    case 5:
                        if (!(this.dataObject is CSPKG_GAMING_CCSYNC))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GAMING_CCSYNC) ProtocolObjectPool.Get(CSPKG_GAMING_CCSYNC.CLASS_ID);
                        }
                        return;

                    case 7:
                        if (!(this.dataObject is SCPKG_NTF_ACNT_REGISTER))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_NTF_ACNT_REGISTER) ProtocolObjectPool.Get(SCPKG_NTF_ACNT_REGISTER.CLASS_ID);
                        }
                        return;

                    case 8:
                        if (!(this.dataObject is CSPKG_ACNT_REGISTER_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_ACNT_REGISTER_REQ) ProtocolObjectPool.Get(CSPKG_ACNT_REGISTER_REQ.CLASS_ID);
                        }
                        return;

                    case 9:
                        if (!(this.dataObject is CSPKG_ACNT_REGISTER_RES))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_ACNT_REGISTER_RES) ProtocolObjectPool.Get(CSPKG_ACNT_REGISTER_RES.CLASS_ID);
                        }
                        return;

                    case 10:
                        if (!(this.dataObject is SCPKG_NTF_ACNT_INFO_UPD))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_NTF_ACNT_INFO_UPD) ProtocolObjectPool.Get(SCPKG_NTF_ACNT_INFO_UPD.CLASS_ID);
                        }
                        return;

                    case 11:
                        if (!(this.dataObject is SCPKG_NTF_ACNT_LEVELUP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_NTF_ACNT_LEVELUP) ProtocolObjectPool.Get(SCPKG_NTF_ACNT_LEVELUP.CLASS_ID);
                        }
                        return;

                    case 12:
                        if (!(this.dataObject is CSPKG_CMD_CHEATCMD))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_CHEATCMD) ProtocolObjectPool.Get(CSPKG_CMD_CHEATCMD.CLASS_ID);
                        }
                        return;

                    case 13:
                        if (!(this.dataObject is SCPKG_CMD_LOGINFINISHNTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_LOGINFINISHNTF) ProtocolObjectPool.Get(SCPKG_CMD_LOGINFINISHNTF.CLASS_ID);
                        }
                        return;

                    case 14:
                        if (!(this.dataObject is SCPKG_CMD_RELOGINNOW))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_RELOGINNOW) ProtocolObjectPool.Get(SCPKG_CMD_RELOGINNOW.CLASS_ID);
                        }
                        return;

                    case 15:
                        if (!(this.dataObject is SCPKG_NTF_ACNT_PVPLEVELUP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_NTF_ACNT_PVPLEVELUP) ProtocolObjectPool.Get(SCPKG_NTF_ACNT_PVPLEVELUP.CLASS_ID);
                        }
                        return;

                    case 0x10:
                        if (!(this.dataObject is CSPKG_CMD_GAMELOGOUTREQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_GAMELOGOUTREQ) ProtocolObjectPool.Get(CSPKG_CMD_GAMELOGOUTREQ.CLASS_ID);
                        }
                        return;

                    case 0x11:
                        if (!(this.dataObject is SCPKG_CMD_GAMELOGOUTRSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_GAMELOGOUTRSP) ProtocolObjectPool.Get(SCPKG_CMD_GAMELOGOUTRSP.CLASS_ID);
                        }
                        return;

                    case 0x12:
                        if (!(this.dataObject is SCPKG_CMD_LOGINSYN_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_LOGINSYN_REQ) ProtocolObjectPool.Get(SCPKG_CMD_LOGINSYN_REQ.CLASS_ID);
                        }
                        return;

                    case 0x13:
                        if (!(this.dataObject is CSPKG_CMD_LOGINSYN_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_LOGINSYN_RSP) ProtocolObjectPool.Get(CSPKG_CMD_LOGINSYN_RSP.CLASS_ID);
                        }
                        return;

                    case 20:
                        if (!(this.dataObject is CSPKG_CREATEULTIGAMEREQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CREATEULTIGAMEREQ) ProtocolObjectPool.Get(CSPKG_CREATEULTIGAMEREQ.CLASS_ID);
                        }
                        return;

                    case 0x16:
                        if (!(this.dataObject is SCPKG_JOINMULTIGAMERSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_JOINMULTIGAMERSP) ProtocolObjectPool.Get(SCPKG_JOINMULTIGAMERSP.CLASS_ID);
                        }
                        return;

                    case 0x17:
                        if (!(this.dataObject is CSPKG_QUITMULTIGAMEREQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_QUITMULTIGAMEREQ) ProtocolObjectPool.Get(CSPKG_QUITMULTIGAMEREQ.CLASS_ID);
                        }
                        return;

                    case 0x18:
                        if (!(this.dataObject is SCPKG_QUITMULTIGAMERSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_QUITMULTIGAMERSP) ProtocolObjectPool.Get(SCPKG_QUITMULTIGAMERSP.CLASS_ID);
                        }
                        return;

                    case 0x19:
                        if (!(this.dataObject is SCPKG_ROOMCHGNTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_ROOMCHGNTF) ProtocolObjectPool.Get(SCPKG_ROOMCHGNTF.CLASS_ID);
                        }
                        return;

                    case 0x1a:
                        if (!(this.dataObject is SCPKG_ASK_ACNT_TRANS_VISITORSVRDATA))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_ASK_ACNT_TRANS_VISITORSVRDATA) ProtocolObjectPool.Get(SCPKG_ASK_ACNT_TRANS_VISITORSVRDATA.CLASS_ID);
                        }
                        return;

                    case 0x1b:
                        if (!(this.dataObject is CSPKG_RSP_ACNT_TRANS_VISITORSVRDATA))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_RSP_ACNT_TRANS_VISITORSVRDATA) ProtocolObjectPool.Get(CSPKG_RSP_ACNT_TRANS_VISITORSVRDATA.CLASS_ID);
                        }
                        return;

                    case 30:
                        if (!(this.dataObject is SCPKG_GAMECONN_REDIRECT))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GAMECONN_REDIRECT) ProtocolObjectPool.Get(SCPKG_GAMECONN_REDIRECT.CLASS_ID);
                        }
                        return;

                    case 0x22:
                        if (!(this.dataObject is SCPKG_FRAPBOOT_SINGLE))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_FRAPBOOT_SINGLE) ProtocolObjectPool.Get(SCPKG_FRAPBOOT_SINGLE.CLASS_ID);
                        }
                        return;

                    case 0x23:
                        if (!(this.dataObject is SCPKG_FRAPBOOTINFO))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_FRAPBOOTINFO) ProtocolObjectPool.Get(SCPKG_FRAPBOOTINFO.CLASS_ID);
                        }
                        return;

                    case 0x24:
                        if (!(this.dataObject is CSPKG_REQUESTFRAPBOOTSINGLE))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_REQUESTFRAPBOOTSINGLE) ProtocolObjectPool.Get(CSPKG_REQUESTFRAPBOOTSINGLE.CLASS_ID);
                        }
                        return;

                    case 0x25:
                        if (!(this.dataObject is CSPKG_REQUESTFRAPBOOTTIMEOUT))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_REQUESTFRAPBOOTTIMEOUT) ProtocolObjectPool.Get(CSPKG_REQUESTFRAPBOOTTIMEOUT.CLASS_ID);
                        }
                        return;

                    case 40:
                        if (!(this.dataObject is SCPKG_OFFINGRESTART_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_OFFINGRESTART_REQ) ProtocolObjectPool.Get(SCPKG_OFFINGRESTART_REQ.CLASS_ID);
                        }
                        return;

                    case 0x29:
                        if (!(this.dataObject is CSPKG_OFFINGRESTART_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_OFFINGRESTART_RSP) ProtocolObjectPool.Get(CSPKG_OFFINGRESTART_RSP.CLASS_ID);
                        }
                        return;

                    case 0x2a:
                        if (!(this.dataObject is SCPKG_CMD_GAMELOGINLIMIT))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_GAMELOGINLIMIT) ProtocolObjectPool.Get(SCPKG_CMD_GAMELOGINLIMIT.CLASS_ID);
                        }
                        return;

                    case 0x2b:
                        if (!(this.dataObject is SCPKG_CMD_BANTIME_CHG))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_BANTIME_CHG) ProtocolObjectPool.Get(SCPKG_CMD_BANTIME_CHG.CLASS_ID);
                        }
                        return;

                    case 0x2c:
                        if (!(this.dataObject is SCPKG_ISACCEPT_AIPLAYER_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_ISACCEPT_AIPLAYER_REQ) ProtocolObjectPool.Get(SCPKG_ISACCEPT_AIPLAYER_REQ.CLASS_ID);
                        }
                        return;

                    case 0x2d:
                        if (!(this.dataObject is CSPKG_ISACCEPT_AIPLAYER_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_ISACCEPT_AIPLAYER_RSP) ProtocolObjectPool.Get(CSPKG_ISACCEPT_AIPLAYER_RSP.CLASS_ID);
                        }
                        return;

                    case 0x2e:
                        if (!(this.dataObject is SCPKG_NOTICE_HANGUP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_NOTICE_HANGUP) ProtocolObjectPool.Get(SCPKG_NOTICE_HANGUP.CLASS_ID);
                        }
                        return;

                    case 50:
                        if (!(this.dataObject is CSPKG_STARTSINGLEGAMEREQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_STARTSINGLEGAMEREQ) ProtocolObjectPool.Get(CSPKG_STARTSINGLEGAMEREQ.CLASS_ID);
                        }
                        return;

                    case 0x33:
                        if (!(this.dataObject is SCPKG_STARTSINGLEGAMERSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_STARTSINGLEGAMERSP) ProtocolObjectPool.Get(SCPKG_STARTSINGLEGAMERSP.CLASS_ID);
                        }
                        return;

                    case 0x34:
                        if (!(this.dataObject is CSPKG_SINGLEGAMEFINREQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_SINGLEGAMEFINREQ) ProtocolObjectPool.Get(CSPKG_SINGLEGAMEFINREQ.CLASS_ID);
                        }
                        return;

                    case 0x35:
                        if (!(this.dataObject is SCPKG_SINGLEGAMEFINRSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_SINGLEGAMEFINRSP) ProtocolObjectPool.Get(SCPKG_SINGLEGAMEFINRSP.CLASS_ID);
                        }
                        return;

                    case 0x36:
                        if (!(this.dataObject is CSPKG_SINGLEGAMESWEEPREQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_SINGLEGAMESWEEPREQ) ProtocolObjectPool.Get(CSPKG_SINGLEGAMESWEEPREQ.CLASS_ID);
                        }
                        return;

                    case 0x37:
                        if (!(this.dataObject is SCPKG_SINGLEGAMESWEEPRSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_SINGLEGAMESWEEPRSP) ProtocolObjectPool.Get(SCPKG_SINGLEGAMESWEEPRSP.CLASS_ID);
                        }
                        return;

                    case 0x38:
                        if (!(this.dataObject is CSPKG_GET_CHAPTER_REWARD_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GET_CHAPTER_REWARD_REQ) ProtocolObjectPool.Get(CSPKG_GET_CHAPTER_REWARD_REQ.CLASS_ID);
                        }
                        return;

                    case 0x39:
                        if (!(this.dataObject is SCPKG_GET_CHAPTER_REWARD_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GET_CHAPTER_REWARD_RSP) ProtocolObjectPool.Get(SCPKG_GET_CHAPTER_REWARD_RSP.CLASS_ID);
                        }
                        return;

                    case 0x3a:
                        if (!(this.dataObject is CSPKG_QUITSINGLEGAMEREQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_QUITSINGLEGAMEREQ) ProtocolObjectPool.Get(CSPKG_QUITSINGLEGAMEREQ.CLASS_ID);
                        }
                        return;

                    case 0x3b:
                        if (!(this.dataObject is SCPKG_QUITSINGLEGAMERSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_QUITSINGLEGAMERSP) ProtocolObjectPool.Get(SCPKG_QUITSINGLEGAMERSP.CLASS_ID);
                        }
                        return;

                    case 60:
                        if (!(this.dataObject is CSPKG_ASKINMULTGAME_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_ASKINMULTGAME_REQ) ProtocolObjectPool.Get(CSPKG_ASKINMULTGAME_REQ.CLASS_ID);
                        }
                        return;

                    case 0x3d:
                        if (!(this.dataObject is SCPKG_ASKINMULTGAME_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_ASKINMULTGAME_RSP) ProtocolObjectPool.Get(SCPKG_ASKINMULTGAME_RSP.CLASS_ID);
                        }
                        return;

                    case 0x3e:
                        if (!(this.dataObject is CSPKG_SECURE_INFO_START_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_SECURE_INFO_START_REQ) ProtocolObjectPool.Get(CSPKG_SECURE_INFO_START_REQ.CLASS_ID);
                        }
                        return;

                    case 70:
                        if (!(this.dataObject is SCPKG_MULTGAME_BEGINPICK))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_MULTGAME_BEGINPICK) ProtocolObjectPool.Get(SCPKG_MULTGAME_BEGINPICK.CLASS_ID);
                        }
                        return;

                    case 0x4b:
                        if (!(this.dataObject is SCPKG_MULTGAME_BEGINLOAD))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_MULTGAME_BEGINLOAD) ProtocolObjectPool.Get(SCPKG_MULTGAME_BEGINLOAD.CLASS_ID);
                        }
                        return;

                    case 0x4c:
                        if (!(this.dataObject is CSPKG_MULTGAME_LOADFIN))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_MULTGAME_LOADFIN) ProtocolObjectPool.Get(CSPKG_MULTGAME_LOADFIN.CLASS_ID);
                        }
                        return;

                    case 0x4d:
                        if (!(this.dataObject is SCPKG_MULTGAME_BEGINFIGHT))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_MULTGAME_BEGINFIGHT) ProtocolObjectPool.Get(SCPKG_MULTGAME_BEGINFIGHT.CLASS_ID);
                        }
                        return;

                    case 0x4e:
                        if (!(this.dataObject is SCPKG_MULTGAMEREADYNTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_MULTGAMEREADYNTF) ProtocolObjectPool.Get(SCPKG_MULTGAMEREADYNTF.CLASS_ID);
                        }
                        return;

                    case 0x4f:
                        if (!(this.dataObject is SCPKG_MULTGAMEABORTNTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_MULTGAMEABORTNTF) ProtocolObjectPool.Get(SCPKG_MULTGAMEABORTNTF.CLASS_ID);
                        }
                        return;

                    case 80:
                        if (!(this.dataObject is CSPKG_MULTGAME_GAMEOVER))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_MULTGAME_GAMEOVER) ProtocolObjectPool.Get(CSPKG_MULTGAME_GAMEOVER.CLASS_ID);
                        }
                        return;

                    case 0x51:
                        if (!(this.dataObject is SCPKG_MULTGAME_GAMEOVER))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_MULTGAME_GAMEOVER) ProtocolObjectPool.Get(SCPKG_MULTGAME_GAMEOVER.CLASS_ID);
                        }
                        return;

                    case 0x52:
                        if (!(this.dataObject is SCPKG_MULTGAME_SETTLEGAIN))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_MULTGAME_SETTLEGAIN) ProtocolObjectPool.Get(SCPKG_MULTGAME_SETTLEGAIN.CLASS_ID);
                        }
                        return;
                }
            }
            if (this.dataObject != null)
            {
                this.dataObject.Release();
                this.dataObject = null;
            }
        }

        private void select_1083_1146(long selector)
        {
            long num = selector;
            if ((num >= 0x43bL) && (num <= 0x47aL))
            {
                switch (((int) (num - 0x43bL)))
                {
                    case 0:
                        if (!(this.dataObject is CSPKG_MULTGAME_LOADPROCESS))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_MULTGAME_LOADPROCESS) ProtocolObjectPool.Get(CSPKG_MULTGAME_LOADPROCESS.CLASS_ID);
                        }
                        return;

                    case 1:
                        if (!(this.dataObject is SCPKG_MULTGAME_LOADPROCESS))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_MULTGAME_LOADPROCESS) ProtocolObjectPool.Get(SCPKG_MULTGAME_LOADPROCESS.CLASS_ID);
                        }
                        return;

                    case 2:
                        if (!(this.dataObject is SCPKG_MULTGAME_NTF_CLT_GAMEOVER))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_MULTGAME_NTF_CLT_GAMEOVER) ProtocolObjectPool.Get(SCPKG_MULTGAME_NTF_CLT_GAMEOVER.CLASS_ID);
                        }
                        return;

                    case 3:
                        if (!(this.dataObject is CSPKG_MULTGAME_RUNAWAY_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_MULTGAME_RUNAWAY_REQ) ProtocolObjectPool.Get(CSPKG_MULTGAME_RUNAWAY_REQ.CLASS_ID);
                        }
                        return;

                    case 4:
                        if (!(this.dataObject is SCPKG_MULTGAME_RUNAWAY_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_MULTGAME_RUNAWAY_RSP) ProtocolObjectPool.Get(SCPKG_MULTGAME_RUNAWAY_RSP.CLASS_ID);
                        }
                        return;

                    case 5:
                        if (!(this.dataObject is SCPKG_MULTGAME_RUNAWAY_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_MULTGAME_RUNAWAY_NTF) ProtocolObjectPool.Get(SCPKG_MULTGAME_RUNAWAY_NTF.CLASS_ID);
                        }
                        return;

                    case 6:
                        if (!(this.dataObject is SCPKG_MULTGAMERECOVERNTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_MULTGAMERECOVERNTF) ProtocolObjectPool.Get(SCPKG_MULTGAMERECOVERNTF.CLASS_ID);
                        }
                        return;

                    case 7:
                        if (!(this.dataObject is CSPKG_RECOVERGAMEFRAP_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_RECOVERGAMEFRAP_REQ) ProtocolObjectPool.Get(CSPKG_RECOVERGAMEFRAP_REQ.CLASS_ID);
                        }
                        return;

                    case 8:
                        if (!(this.dataObject is SCPKG_RECOVERGAMEFRAP_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_RECOVERGAMEFRAP_RSP) ProtocolObjectPool.Get(SCPKG_RECOVERGAMEFRAP_RSP.CLASS_ID);
                        }
                        return;

                    case 9:
                        if (!(this.dataObject is SCPKG_RECONNGAME_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_RECONNGAME_NTF) ProtocolObjectPool.Get(SCPKG_RECONNGAME_NTF.CLASS_ID);
                        }
                        return;

                    case 10:
                        if (!(this.dataObject is CSPKG_RECOVERGAMESUCC))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_RECOVERGAMESUCC) ProtocolObjectPool.Get(CSPKG_RECOVERGAMESUCC.CLASS_ID);
                        }
                        return;

                    case 11:
                        if (!(this.dataObject is SCPKG_MULTGAME_DISCONN_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_MULTGAME_DISCONN_NTF) ProtocolObjectPool.Get(SCPKG_MULTGAME_DISCONN_NTF.CLASS_ID);
                        }
                        return;

                    case 12:
                        if (!(this.dataObject is SCPKG_MULTGAME_RECONN_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_MULTGAME_RECONN_NTF) ProtocolObjectPool.Get(SCPKG_MULTGAME_RECONN_NTF.CLASS_ID);
                        }
                        return;

                    case 13:
                        if (!(this.dataObject is CSPKG_KFRAPLATERCHG_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_KFRAPLATERCHG_REQ) ProtocolObjectPool.Get(CSPKG_KFRAPLATERCHG_REQ.CLASS_ID);
                        }
                        return;

                    case 14:
                        if (!(this.dataObject is SCPKG_KFRAPLATERCHG_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_KFRAPLATERCHG_NTF) ProtocolObjectPool.Get(SCPKG_KFRAPLATERCHG_NTF.CLASS_ID);
                        }
                        return;

                    case 15:
                        if (!(this.dataObject is CSPKG_MULTGAME_DIE_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_MULTGAME_DIE_REQ) ProtocolObjectPool.Get(CSPKG_MULTGAME_DIE_REQ.CLASS_ID);
                        }
                        return;

                    case 0x10:
                        if (!(this.dataObject is SCPKG_HANGUP_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_HANGUP_NTF) ProtocolObjectPool.Get(SCPKG_HANGUP_NTF.CLASS_ID);
                        }
                        return;

                    case 0x12:
                        if (!(this.dataObject is CSPKG_CMD_ITEMSALE))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_ITEMSALE) ProtocolObjectPool.Get(CSPKG_CMD_ITEMSALE.CLASS_ID);
                        }
                        return;

                    case 0x13:
                        if (!(this.dataObject is SCPKG_CMD_ITEMADD))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_ITEMADD) ProtocolObjectPool.Get(SCPKG_CMD_ITEMADD.CLASS_ID);
                        }
                        return;

                    case 20:
                        if (!(this.dataObject is SCPKG_CMD_ITEMDEL))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_ITEMDEL) ProtocolObjectPool.Get(SCPKG_CMD_ITEMDEL.CLASS_ID);
                        }
                        return;

                    case 0x15:
                        if (!(this.dataObject is CSPKG_CMD_EQUIPWEAR))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_EQUIPWEAR) ProtocolObjectPool.Get(CSPKG_CMD_EQUIPWEAR.CLASS_ID);
                        }
                        return;

                    case 0x17:
                        if (!(this.dataObject is SCPKG_CMD_EQUIPCHG))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_EQUIPCHG) ProtocolObjectPool.Get(SCPKG_CMD_EQUIPCHG.CLASS_ID);
                        }
                        return;

                    case 0x18:
                        if (!(this.dataObject is CSPKG_CMD_PROPUSE))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_PROPUSE) ProtocolObjectPool.Get(CSPKG_CMD_PROPUSE.CLASS_ID);
                        }
                        return;

                    case 0x19:
                        if (!(this.dataObject is CSPKG_CMD_PKGQUERY))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_PKGQUERY) ProtocolObjectPool.Get(CSPKG_CMD_PKGQUERY.CLASS_ID);
                        }
                        return;

                    case 0x1a:
                        if (!(this.dataObject is SCPKG_CMD_PKGDETAIL))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_PKGDETAIL) ProtocolObjectPool.Get(SCPKG_CMD_PKGDETAIL.CLASS_ID);
                        }
                        return;

                    case 0x1b:
                        if (!(this.dataObject is CSPKG_CMD_ITEMCOMP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_ITEMCOMP) ProtocolObjectPool.Get(CSPKG_CMD_ITEMCOMP.CLASS_ID);
                        }
                        return;

                    case 0x1c:
                        if (!(this.dataObject is CSPKG_CMD_HEROADVANCE))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_HEROADVANCE) ProtocolObjectPool.Get(CSPKG_CMD_HEROADVANCE.CLASS_ID);
                        }
                        return;

                    case 0x1d:
                        if (!(this.dataObject is SCPKG_CMD_HEROADVANCE))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_HEROADVANCE) ProtocolObjectPool.Get(SCPKG_CMD_HEROADVANCE.CLASS_ID);
                        }
                        return;

                    case 30:
                        if (!(this.dataObject is CSPKG_CMD_SHOPBUY))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_SHOPBUY) ProtocolObjectPool.Get(CSPKG_CMD_SHOPBUY.CLASS_ID);
                        }
                        return;

                    case 0x1f:
                        if (!(this.dataObject is SCPKG_CMD_SHOPBUY))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_SHOPBUY) ProtocolObjectPool.Get(SCPKG_CMD_SHOPBUY.CLASS_ID);
                        }
                        return;

                    case 0x20:
                        if (!(this.dataObject is CSPKG_CMD_COINBUY))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_COINBUY) ProtocolObjectPool.Get(CSPKG_CMD_COINBUY.CLASS_ID);
                        }
                        return;

                    case 0x21:
                        if (!(this.dataObject is SCPKG_CMD_COINBUY))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_COINBUY) ProtocolObjectPool.Get(SCPKG_CMD_COINBUY.CLASS_ID);
                        }
                        return;

                    case 0x22:
                        if (!(this.dataObject is SCPKG_NTF_CLRSHOPBUYLIMIT))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_NTF_CLRSHOPBUYLIMIT) ProtocolObjectPool.Get(SCPKG_NTF_CLRSHOPBUYLIMIT.CLASS_ID);
                        }
                        return;

                    case 0x23:
                        if (!(this.dataObject is CSPKG_CMD_AUTOREFRESH))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_AUTOREFRESH) ProtocolObjectPool.Get(CSPKG_CMD_AUTOREFRESH.CLASS_ID);
                        }
                        return;

                    case 0x24:
                        if (!(this.dataObject is CSPKG_CMD_MANUALREFRESH))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_MANUALREFRESH) ProtocolObjectPool.Get(CSPKG_CMD_MANUALREFRESH.CLASS_ID);
                        }
                        return;

                    case 0x25:
                        if (!(this.dataObject is SCPKG_CMD_SHOPDETAIL))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_SHOPDETAIL) ProtocolObjectPool.Get(SCPKG_CMD_SHOPDETAIL.CLASS_ID);
                        }
                        return;

                    case 0x26:
                        if (!(this.dataObject is CSPKG_CMD_SYMBOLNAMECHG))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_SYMBOLNAMECHG) ProtocolObjectPool.Get(CSPKG_CMD_SYMBOLNAMECHG.CLASS_ID);
                        }
                        return;

                    case 0x27:
                        if (!(this.dataObject is SCPKG_CMD_SYMBOLNAMECHG))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_SYMBOLNAMECHG) ProtocolObjectPool.Get(SCPKG_CMD_SYMBOLNAMECHG.CLASS_ID);
                        }
                        return;

                    case 40:
                        if (!(this.dataObject is CSPKG_CMD_HORNUSE))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_HORNUSE) ProtocolObjectPool.Get(CSPKG_CMD_HORNUSE.CLASS_ID);
                        }
                        return;

                    case 0x29:
                        if (!(this.dataObject is SCPKG_CMD_HORNUSE))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_HORNUSE) ProtocolObjectPool.Get(SCPKG_CMD_HORNUSE.CLASS_ID);
                        }
                        return;

                    case 0x2a:
                        if (!(this.dataObject is CSPKG_CMD_ITEMBUY))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_ITEMBUY) ProtocolObjectPool.Get(CSPKG_CMD_ITEMBUY.CLASS_ID);
                        }
                        return;

                    case 0x2b:
                        if (!(this.dataObject is SCPKG_CMD_ITEMBUY))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_ITEMBUY) ProtocolObjectPool.Get(SCPKG_CMD_ITEMBUY.CLASS_ID);
                        }
                        return;

                    case 0x2c:
                        if (!(this.dataObject is SCPKG_NTF_SHOPTIMEOUT))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_NTF_SHOPTIMEOUT) ProtocolObjectPool.Get(SCPKG_NTF_SHOPTIMEOUT.CLASS_ID);
                        }
                        return;

                    case 0x2d:
                        if (!(this.dataObject is SCPKG_CMD_CLRSHOPREFRESH))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_CLRSHOPREFRESH) ProtocolObjectPool.Get(SCPKG_CMD_CLRSHOPREFRESH.CLASS_ID);
                        }
                        return;

                    case 0x2f:
                        if (!(this.dataObject is CSPKG_CMD_SYMBOLCOMP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_SYMBOLCOMP) ProtocolObjectPool.Get(CSPKG_CMD_SYMBOLCOMP.CLASS_ID);
                        }
                        return;

                    case 0x30:
                        if (!(this.dataObject is SCPKG_CMD_SYMBOLCOMP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_SYMBOLCOMP) ProtocolObjectPool.Get(SCPKG_CMD_SYMBOLCOMP.CLASS_ID);
                        }
                        return;

                    case 0x31:
                        if (!(this.dataObject is CSPKG_CMD_SYMBOLQUERY))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_SYMBOLQUERY) ProtocolObjectPool.Get(CSPKG_CMD_SYMBOLQUERY.CLASS_ID);
                        }
                        return;

                    case 50:
                        if (!(this.dataObject is SCPKG_CMD_SYMBOLDETAIL))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_SYMBOLDETAIL) ProtocolObjectPool.Get(SCPKG_CMD_SYMBOLDETAIL.CLASS_ID);
                        }
                        return;

                    case 0x33:
                        if (!(this.dataObject is CSPKG_CMD_SYMBOLWEAR))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_SYMBOLWEAR) ProtocolObjectPool.Get(CSPKG_CMD_SYMBOLWEAR.CLASS_ID);
                        }
                        return;

                    case 0x34:
                        if (!(this.dataObject is CSPKG_CMD_SYMBOLOFF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_SYMBOLOFF) ProtocolObjectPool.Get(CSPKG_CMD_SYMBOLOFF.CLASS_ID);
                        }
                        return;

                    case 0x35:
                        if (!(this.dataObject is SCPKG_CMD_SYMBOLCHG))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_SYMBOLCHG) ProtocolObjectPool.Get(SCPKG_CMD_SYMBOLCHG.CLASS_ID);
                        }
                        return;

                    case 0x36:
                        if (!(this.dataObject is CSPKG_CMD_SYMBOLPAGESEL))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_SYMBOLPAGESEL) ProtocolObjectPool.Get(CSPKG_CMD_SYMBOLPAGESEL.CLASS_ID);
                        }
                        return;

                    case 0x37:
                        if (!(this.dataObject is SCPKG_CMD_SYMBOLPAGESEL))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_SYMBOLPAGESEL) ProtocolObjectPool.Get(SCPKG_CMD_SYMBOLPAGESEL.CLASS_ID);
                        }
                        return;

                    case 0x39:
                        if (!(this.dataObject is CSPKG_CMD_EQUIPSMELT))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_EQUIPSMELT) ProtocolObjectPool.Get(CSPKG_CMD_EQUIPSMELT.CLASS_ID);
                        }
                        return;

                    case 0x3a:
                        if (!(this.dataObject is CSPKG_CMD_EQUIPENCHANT))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_EQUIPENCHANT) ProtocolObjectPool.Get(CSPKG_CMD_EQUIPENCHANT.CLASS_ID);
                        }
                        return;

                    case 0x3b:
                        if (!(this.dataObject is SCPKG_CMD_EQUIPENCHANT))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_EQUIPENCHANT) ProtocolObjectPool.Get(SCPKG_CMD_EQUIPENCHANT.CLASS_ID);
                        }
                        return;

                    case 60:
                        if (!(this.dataObject is SCPKG_COINDRAW_RESULT))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_COINDRAW_RESULT) ProtocolObjectPool.Get(SCPKG_COINDRAW_RESULT.CLASS_ID);
                        }
                        return;

                    case 0x3d:
                        if (!(this.dataObject is CSPKG_CMD_GEAR_LVLUP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_GEAR_LVLUP) ProtocolObjectPool.Get(CSPKG_CMD_GEAR_LVLUP.CLASS_ID);
                        }
                        return;

                    case 0x3e:
                        if (!(this.dataObject is CSPKG_CMD_GEAR_LVLUPALL))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_GEAR_LVLUPALL) ProtocolObjectPool.Get(CSPKG_CMD_GEAR_LVLUPALL.CLASS_ID);
                        }
                        return;

                    case 0x3f:
                        if (!(this.dataObject is SCPKG_CMD_GEAR_LEVELINFO))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_GEAR_LEVELINFO) ProtocolObjectPool.Get(SCPKG_CMD_GEAR_LEVELINFO.CLASS_ID);
                        }
                        return;
                }
            }
            if (this.dataObject != null)
            {
                this.dataObject.Release();
                this.dataObject = null;
            }
        }

        private void select_1147_1232(long selector)
        {
            long num = selector;
            if ((num >= 0x47bL) && (num <= 0x4d0L))
            {
                switch (((int) (num - 0x47bL)))
                {
                    case 0:
                        if (!(this.dataObject is CSPKG_CMD_GEAR_ADVANCE))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_GEAR_ADVANCE) ProtocolObjectPool.Get(CSPKG_CMD_GEAR_ADVANCE.CLASS_ID);
                        }
                        return;

                    case 1:
                        if (!(this.dataObject is SCPKG_CMD_GEAR_ADVANCE))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_GEAR_ADVANCE) ProtocolObjectPool.Get(SCPKG_CMD_GEAR_ADVANCE.CLASS_ID);
                        }
                        return;

                    case 2:
                        if (!(this.dataObject is SCPKG_CMD_PROPUSE_GIFTGET))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_PROPUSE_GIFTGET) ProtocolObjectPool.Get(SCPKG_CMD_PROPUSE_GIFTGET.CLASS_ID);
                        }
                        return;

                    case 3:
                        if (!(this.dataObject is CSPKG_CMD_ACNTCOUPONS))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_ACNTCOUPONS) ProtocolObjectPool.Get(CSPKG_CMD_ACNTCOUPONS.CLASS_ID);
                        }
                        return;

                    case 4:
                        if (!(this.dataObject is SCPKG_CMD_ACNTCOUPONS))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_ACNTCOUPONS) ProtocolObjectPool.Get(SCPKG_CMD_ACNTCOUPONS.CLASS_ID);
                        }
                        return;

                    case 5:
                        if (!(this.dataObject is SCPKG_CMD_SPECIAL_SALEINFO))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_SPECIAL_SALEINFO) ProtocolObjectPool.Get(SCPKG_CMD_SPECIAL_SALEINFO.CLASS_ID);
                        }
                        return;

                    case 6:
                        if (!(this.dataObject is CSPKG_CMD_SPECSALEBUY))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_SPECSALEBUY) ProtocolObjectPool.Get(CSPKG_CMD_SPECSALEBUY.CLASS_ID);
                        }
                        return;

                    case 7:
                        if (!(this.dataObject is SCPKG_CMD_SPECSALEBUY))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_SPECSALEBUY) ProtocolObjectPool.Get(SCPKG_CMD_SPECSALEBUY.CLASS_ID);
                        }
                        return;

                    case 8:
                        if (!(this.dataObject is CSPKG_CMD_SYMBOL_MAKE))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_SYMBOL_MAKE) ProtocolObjectPool.Get(CSPKG_CMD_SYMBOL_MAKE.CLASS_ID);
                        }
                        return;

                    case 9:
                        if (!(this.dataObject is CSPKG_CMD_SYMBOL_BREAK))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_SYMBOL_BREAK) ProtocolObjectPool.Get(CSPKG_CMD_SYMBOL_BREAK.CLASS_ID);
                        }
                        return;

                    case 10:
                        if (!(this.dataObject is SCPKG_CMD_SYMBOL_MAKE))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_SYMBOL_MAKE) ProtocolObjectPool.Get(SCPKG_CMD_SYMBOL_MAKE.CLASS_ID);
                        }
                        return;

                    case 11:
                        if (!(this.dataObject is SCPKG_CMD_SYMBOL_BREAK))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_SYMBOL_BREAK) ProtocolObjectPool.Get(SCPKG_CMD_SYMBOL_BREAK.CLASS_ID);
                        }
                        return;

                    case 13:
                        if (!(this.dataObject is CSPKG_CMD_COUPONS_REWARDGET))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_COUPONS_REWARDGET) ProtocolObjectPool.Get(CSPKG_CMD_COUPONS_REWARDGET.CLASS_ID);
                        }
                        return;

                    case 14:
                        if (!(this.dataObject is SCPKG_CMD_COUPONS_REWARDINFO))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_COUPONS_REWARDINFO) ProtocolObjectPool.Get(SCPKG_CMD_COUPONS_REWARDINFO.CLASS_ID);
                        }
                        return;

                    case 15:
                        if (!(this.dataObject is CSPKG_CMD_SYMBOLPAGE_CLR))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_SYMBOLPAGE_CLR) ProtocolObjectPool.Get(CSPKG_CMD_SYMBOLPAGE_CLR.CLASS_ID);
                        }
                        return;

                    case 0x10:
                        if (!(this.dataObject is SCPKG_CMD_SYMBOLPAGE_CLR))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_SYMBOLPAGE_CLR) ProtocolObjectPool.Get(SCPKG_CMD_SYMBOLPAGE_CLR.CLASS_ID);
                        }
                        return;

                    case 0x11:
                        if (!(this.dataObject is CSPKG_CMD_TALENT_BUY))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_TALENT_BUY) ProtocolObjectPool.Get(CSPKG_CMD_TALENT_BUY.CLASS_ID);
                        }
                        return;

                    case 0x12:
                        if (!(this.dataObject is SCPKG_CMD_TALENT_BUY))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_TALENT_BUY) ProtocolObjectPool.Get(SCPKG_CMD_TALENT_BUY.CLASS_ID);
                        }
                        return;

                    case 0x13:
                        if (!(this.dataObject is CSPKG_CMD_SKILLUNLOCK_SEL))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_SKILLUNLOCK_SEL) ProtocolObjectPool.Get(CSPKG_CMD_SKILLUNLOCK_SEL.CLASS_ID);
                        }
                        return;

                    case 20:
                        if (!(this.dataObject is SCPKG_CMD_SKILLUNLOCK_SEL))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_SKILLUNLOCK_SEL) ProtocolObjectPool.Get(SCPKG_CMD_SKILLUNLOCK_SEL.CLASS_ID);
                        }
                        return;

                    case 0x15:
                        if (!(this.dataObject is SCPKG_CMD_HERO_WAKECHG))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_HERO_WAKECHG) ProtocolObjectPool.Get(SCPKG_CMD_HERO_WAKECHG.CLASS_ID);
                        }
                        return;

                    case 0x16:
                        if (!(this.dataObject is CSPKG_CMD_HERO_WAKEOPT))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_HERO_WAKEOPT) ProtocolObjectPool.Get(CSPKG_CMD_HERO_WAKEOPT.CLASS_ID);
                        }
                        return;

                    case 0x17:
                        if (!(this.dataObject is SCPKG_CMD_HERO_WAKESTEP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_HERO_WAKESTEP) ProtocolObjectPool.Get(SCPKG_CMD_HERO_WAKESTEP.CLASS_ID);
                        }
                        return;

                    case 0x18:
                        if (!(this.dataObject is SCPKG_CMD_HEROWAKE_REWARD))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_HEROWAKE_REWARD) ProtocolObjectPool.Get(SCPKG_CMD_HEROWAKE_REWARD.CLASS_ID);
                        }
                        return;

                    case 0x19:
                        if (!(this.dataObject is SCPKG_CMD_PROPUSE))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_PROPUSE) ProtocolObjectPool.Get(SCPKG_CMD_PROPUSE.CLASS_ID);
                        }
                        return;

                    case 0x1c:
                        if (!(this.dataObject is CSPKG_CMD_SALERECMD_BUY))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_SALERECMD_BUY) ProtocolObjectPool.Get(CSPKG_CMD_SALERECMD_BUY.CLASS_ID);
                        }
                        return;

                    case 0x1d:
                        if (!(this.dataObject is SCPKG_CMD_SALERECMD_BUY))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_SALERECMD_BUY) ProtocolObjectPool.Get(SCPKG_CMD_SALERECMD_BUY.CLASS_ID);
                        }
                        return;

                    case 30:
                        if (!(this.dataObject is CSPKG_CMD_RANDDRAW_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_RANDDRAW_REQ) ProtocolObjectPool.Get(CSPKG_CMD_RANDDRAW_REQ.CLASS_ID);
                        }
                        return;

                    case 0x1f:
                        if (!(this.dataObject is SCPKG_CMD_RANDDRAW_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_RANDDRAW_RSP) ProtocolObjectPool.Get(SCPKG_CMD_RANDDRAW_RSP.CLASS_ID);
                        }
                        return;

                    case 0x20:
                        if (!(this.dataObject is SCPKG_NTF_RANDDRAW_SYNID))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_NTF_RANDDRAW_SYNID) ProtocolObjectPool.Get(SCPKG_NTF_RANDDRAW_SYNID.CLASS_ID);
                        }
                        return;

                    case 0x2b:
                        if (!(this.dataObject is SCPKG_NTF_ERRCODE))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_NTF_ERRCODE) ProtocolObjectPool.Get(SCPKG_NTF_ERRCODE.CLASS_ID);
                        }
                        return;

                    case 0x2c:
                        if (!(this.dataObject is SCPKG_NTF_NEWIEBITSYN))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_NTF_NEWIEBITSYN) ProtocolObjectPool.Get(SCPKG_NTF_NEWIEBITSYN.CLASS_ID);
                        }
                        return;

                    case 0x2d:
                        if (!(this.dataObject is SCPKG_NTF_NEWIEALLBITSYN))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_NTF_NEWIEALLBITSYN) ProtocolObjectPool.Get(SCPKG_NTF_NEWIEALLBITSYN.CLASS_ID);
                        }
                        return;

                    case 0x35:
                        if (!(this.dataObject is CSPKG_CMD_LIST_FRIEND))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_LIST_FRIEND) ProtocolObjectPool.Get(CSPKG_CMD_LIST_FRIEND.CLASS_ID);
                        }
                        return;

                    case 0x36:
                        if (!(this.dataObject is SCPKG_CMD_LIST_FRIEND))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_LIST_FRIEND) ProtocolObjectPool.Get(SCPKG_CMD_LIST_FRIEND.CLASS_ID);
                        }
                        return;

                    case 0x37:
                        if (!(this.dataObject is CSPKG_CMD_LIST_FRIENDREQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_LIST_FRIENDREQ) ProtocolObjectPool.Get(CSPKG_CMD_LIST_FRIENDREQ.CLASS_ID);
                        }
                        return;

                    case 0x38:
                        if (!(this.dataObject is SCPKG_CMD_LIST_FRIENDREQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_LIST_FRIENDREQ) ProtocolObjectPool.Get(SCPKG_CMD_LIST_FRIENDREQ.CLASS_ID);
                        }
                        return;

                    case 0x39:
                        if (!(this.dataObject is CSPKG_CMD_SEARCH_PLAYER))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_SEARCH_PLAYER) ProtocolObjectPool.Get(CSPKG_CMD_SEARCH_PLAYER.CLASS_ID);
                        }
                        return;

                    case 0x3a:
                        if (!(this.dataObject is SCPKG_CMD_SEARCH_PLAYER))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_SEARCH_PLAYER) ProtocolObjectPool.Get(SCPKG_CMD_SEARCH_PLAYER.CLASS_ID);
                        }
                        return;

                    case 0x3b:
                        if (!(this.dataObject is CSPKG_CMD_ADD_FRIEND))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_ADD_FRIEND) ProtocolObjectPool.Get(CSPKG_CMD_ADD_FRIEND.CLASS_ID);
                        }
                        return;

                    case 60:
                        if (!(this.dataObject is SCPKG_CMD_ADD_FRIEND))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_ADD_FRIEND) ProtocolObjectPool.Get(SCPKG_CMD_ADD_FRIEND.CLASS_ID);
                        }
                        return;

                    case 0x3d:
                        if (!(this.dataObject is CSPKG_CMD_DEL_FRIEND))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_DEL_FRIEND) ProtocolObjectPool.Get(CSPKG_CMD_DEL_FRIEND.CLASS_ID);
                        }
                        return;

                    case 0x3e:
                        if (!(this.dataObject is SCPKG_CMD_DEL_FRIEND))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_DEL_FRIEND) ProtocolObjectPool.Get(SCPKG_CMD_DEL_FRIEND.CLASS_ID);
                        }
                        return;

                    case 0x3f:
                        if (!(this.dataObject is CSPKG_CMD_ADD_FRIEND_CONFIRM))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_ADD_FRIEND_CONFIRM) ProtocolObjectPool.Get(CSPKG_CMD_ADD_FRIEND_CONFIRM.CLASS_ID);
                        }
                        return;

                    case 0x40:
                        if (!(this.dataObject is SCPKG_CMD_ADD_FRIEND_CONFIRM))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_ADD_FRIEND_CONFIRM) ProtocolObjectPool.Get(SCPKG_CMD_ADD_FRIEND_CONFIRM.CLASS_ID);
                        }
                        return;

                    case 0x41:
                        if (!(this.dataObject is CSPKG_CMD_ADD_FRIEND_DENY))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_ADD_FRIEND_DENY) ProtocolObjectPool.Get(CSPKG_CMD_ADD_FRIEND_DENY.CLASS_ID);
                        }
                        return;

                    case 0x42:
                        if (!(this.dataObject is SCPKG_CMD_ADD_FRIEND_DENY))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_ADD_FRIEND_DENY) ProtocolObjectPool.Get(SCPKG_CMD_ADD_FRIEND_DENY.CLASS_ID);
                        }
                        return;

                    case 0x43:
                        if (!(this.dataObject is CSPKG_CMD_INVITE_GAME))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_INVITE_GAME) ProtocolObjectPool.Get(CSPKG_CMD_INVITE_GAME.CLASS_ID);
                        }
                        return;

                    case 0x44:
                        if (!(this.dataObject is SCPKG_CMD_INVITE_GAME))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_INVITE_GAME) ProtocolObjectPool.Get(SCPKG_CMD_INVITE_GAME.CLASS_ID);
                        }
                        return;

                    case 0x45:
                        if (!(this.dataObject is CSPKG_CMD_INVITE_RECEIVE_ACHIEVE))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_INVITE_RECEIVE_ACHIEVE) ProtocolObjectPool.Get(CSPKG_CMD_INVITE_RECEIVE_ACHIEVE.CLASS_ID);
                        }
                        return;

                    case 70:
                        if (!(this.dataObject is SCPKG_CMD_INVITE_RECEIVE_ACHIEVE))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_INVITE_RECEIVE_ACHIEVE) ProtocolObjectPool.Get(SCPKG_CMD_INVITE_RECEIVE_ACHIEVE.CLASS_ID);
                        }
                        return;

                    case 0x47:
                        if (!(this.dataObject is CSPKG_CMD_DONATE_FRIEND_POINT))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_DONATE_FRIEND_POINT) ProtocolObjectPool.Get(CSPKG_CMD_DONATE_FRIEND_POINT.CLASS_ID);
                        }
                        return;

                    case 0x48:
                        if (!(this.dataObject is SCPKG_CMD_DONATE_FRIEND_POINT))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_DONATE_FRIEND_POINT) ProtocolObjectPool.Get(SCPKG_CMD_DONATE_FRIEND_POINT.CLASS_ID);
                        }
                        return;

                    case 0x4b:
                        if (!(this.dataObject is CSPKG_CMD_DONATE_FRIEND_POINT_ALL))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_DONATE_FRIEND_POINT_ALL) ProtocolObjectPool.Get(CSPKG_CMD_DONATE_FRIEND_POINT_ALL.CLASS_ID);
                        }
                        return;

                    case 0x4c:
                        if (!(this.dataObject is SCPKG_CMD_DONATE_FRIEND_POINT_ALL))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_DONATE_FRIEND_POINT_ALL) ProtocolObjectPool.Get(SCPKG_CMD_DONATE_FRIEND_POINT_ALL.CLASS_ID);
                        }
                        return;

                    case 0x51:
                        if (!(this.dataObject is CSPKG_CMD_GET_INVITE_INFO))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_GET_INVITE_INFO) ProtocolObjectPool.Get(CSPKG_CMD_GET_INVITE_INFO.CLASS_ID);
                        }
                        return;

                    case 0x52:
                        if (!(this.dataObject is SCPKG_CMD_GET_INVITE_INFO))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_GET_INVITE_INFO) ProtocolObjectPool.Get(SCPKG_CMD_GET_INVITE_INFO.CLASS_ID);
                        }
                        return;

                    case 0x53:
                        if (!(this.dataObject is SCPKG_CMD_NTF_FRIEND_REQUEST))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_NTF_FRIEND_REQUEST) ProtocolObjectPool.Get(SCPKG_CMD_NTF_FRIEND_REQUEST.CLASS_ID);
                        }
                        return;

                    case 0x54:
                        if (!(this.dataObject is SCPKG_CMD_NTF_FRIEND_ADD))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_NTF_FRIEND_ADD) ProtocolObjectPool.Get(SCPKG_CMD_NTF_FRIEND_ADD.CLASS_ID);
                        }
                        return;

                    case 0x55:
                        if (!(this.dataObject is SCPKG_CMD_NTF_FRIEND_DEL))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_NTF_FRIEND_DEL) ProtocolObjectPool.Get(SCPKG_CMD_NTF_FRIEND_DEL.CLASS_ID);
                        }
                        return;
                }
            }
            if (this.dataObject != null)
            {
                this.dataObject.Release();
                this.dataObject = null;
            }
        }

        private void select_1233_1800(long selector)
        {
            long num = selector;
            if ((num >= 0x578L) && (num <= 0x591L))
            {
                switch (((int) (num - 0x578L)))
                {
                    case 0:
                        if (!(this.dataObject is CSPKG_MAILOPT_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_MAILOPT_REQ) ProtocolObjectPool.Get(CSPKG_MAILOPT_REQ.CLASS_ID);
                        }
                        return;

                    case 1:
                        if (!(this.dataObject is SCPKG_MAILOPT_RES))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_MAILOPT_RES) ProtocolObjectPool.Get(SCPKG_MAILOPT_RES.CLASS_ID);
                        }
                        return;

                    case 2:
                        if (!(this.dataObject is CSPKG_FUNCUNLOCK_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_FUNCUNLOCK_REQ) ProtocolObjectPool.Get(CSPKG_FUNCUNLOCK_REQ.CLASS_ID);
                        }
                        return;

                    case 3:
                        if (!(this.dataObject is SCPKG_ACNTDETAILINFO_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_ACNTDETAILINFO_RSP) ProtocolObjectPool.Get(SCPKG_ACNTDETAILINFO_RSP.CLASS_ID);
                        }
                        return;

                    case 4:
                        if (!(this.dataObject is SCPKG_ACNT_HEAD_URL_CHG_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_ACNT_HEAD_URL_CHG_NTF) ProtocolObjectPool.Get(SCPKG_ACNT_HEAD_URL_CHG_NTF.CLASS_ID);
                        }
                        return;

                    case 5:
                        if (!(this.dataObject is SCPKG_ACNTSELFMSGINFO))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_ACNTSELFMSGINFO) ProtocolObjectPool.Get(SCPKG_ACNTSELFMSGINFO.CLASS_ID);
                        }
                        return;

                    case 6:
                        if (!(this.dataObject is SCPKG_AKALISHOP_INFO))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_AKALISHOP_INFO) ProtocolObjectPool.Get(SCPKG_AKALISHOP_INFO.CLASS_ID);
                        }
                        return;

                    case 7:
                        if (!(this.dataObject is CSPKG_AKALISHOPBUY_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_AKALISHOPBUY_REQ) ProtocolObjectPool.Get(CSPKG_AKALISHOPBUY_REQ.CLASS_ID);
                        }
                        return;

                    case 8:
                        if (!(this.dataObject is SCPKG_AKALISHOP_ERROR))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_AKALISHOP_ERROR) ProtocolObjectPool.Get(SCPKG_AKALISHOP_ERROR.CLASS_ID);
                        }
                        return;

                    case 9:
                        if (!(this.dataObject is CSPKG_AKALISHOP_ZHEKOUREQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_AKALISHOP_ZHEKOUREQ) ProtocolObjectPool.Get(CSPKG_AKALISHOP_ZHEKOUREQ.CLASS_ID);
                        }
                        return;

                    case 10:
                        if (!(this.dataObject is SCPKG_AKALISHOP_ZHEKOURSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_AKALISHOP_ZHEKOURSP) ProtocolObjectPool.Get(SCPKG_AKALISHOP_ZHEKOURSP.CLASS_ID);
                        }
                        return;

                    case 11:
                        if (!(this.dataObject is SCPKG_AKALISHOP_UPDATE))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_AKALISHOP_UPDATE) ProtocolObjectPool.Get(SCPKG_AKALISHOP_UPDATE.CLASS_ID);
                        }
                        return;

                    case 20:
                        if (!(this.dataObject is CSPKG_NOTICENEW_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_NOTICENEW_REQ) ProtocolObjectPool.Get(CSPKG_NOTICENEW_REQ.CLASS_ID);
                        }
                        return;

                    case 0x15:
                        if (!(this.dataObject is SCPKG_NOTICENEW_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_NOTICENEW_RSP) ProtocolObjectPool.Get(SCPKG_NOTICENEW_RSP.CLASS_ID);
                        }
                        return;

                    case 0x16:
                        if (!(this.dataObject is CSPKG_NOTICELIST_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_NOTICELIST_REQ) ProtocolObjectPool.Get(CSPKG_NOTICELIST_REQ.CLASS_ID);
                        }
                        return;

                    case 0x17:
                        if (!(this.dataObject is SCPKG_NOTICELIST_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_NOTICELIST_RSP) ProtocolObjectPool.Get(SCPKG_NOTICELIST_RSP.CLASS_ID);
                        }
                        return;

                    case 0x18:
                        if (!(this.dataObject is CSPKG_NOTICEINFO_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_NOTICEINFO_REQ) ProtocolObjectPool.Get(CSPKG_NOTICEINFO_REQ.CLASS_ID);
                        }
                        return;

                    case 0x19:
                        if (!(this.dataObject is SCPKG_NOTICEINFO_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_NOTICEINFO_RSP) ProtocolObjectPool.Get(SCPKG_NOTICEINFO_RSP.CLASS_ID);
                        }
                        return;
                }
            }
            if ((num >= 0x4d1L) && (num <= 0x4dcL))
            {
                switch (((int) (num - 0x4d1L)))
                {
                    case 0:
                        if (!(this.dataObject is SCPKG_CMD_NTF_FRIEND_LOGIN_STATUS))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_NTF_FRIEND_LOGIN_STATUS) ProtocolObjectPool.Get(SCPKG_CMD_NTF_FRIEND_LOGIN_STATUS.CLASS_ID);
                        }
                        return;

                    case 1:
                        if (!(this.dataObject is CSPKG_CMD_LIST_FREC))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_LIST_FREC) ProtocolObjectPool.Get(CSPKG_CMD_LIST_FREC.CLASS_ID);
                        }
                        return;

                    case 2:
                        if (!(this.dataObject is SCPKG_CMD_LIST_FREC))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_LIST_FREC) ProtocolObjectPool.Get(SCPKG_CMD_LIST_FREC.CLASS_ID);
                        }
                        return;

                    case 3:
                        if (!(this.dataObject is CSPKG_CMD_RECALL_FRIEND))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_RECALL_FRIEND) ProtocolObjectPool.Get(CSPKG_CMD_RECALL_FRIEND.CLASS_ID);
                        }
                        return;

                    case 4:
                        if (!(this.dataObject is SCPKG_CMD_RECALL_FRIEND))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_RECALL_FRIEND) ProtocolObjectPool.Get(SCPKG_CMD_RECALL_FRIEND.CLASS_ID);
                        }
                        return;

                    case 5:
                        if (!(this.dataObject is SCPKG_CMD_NTF_RECALL_FRIEND))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_NTF_RECALL_FRIEND) ProtocolObjectPool.Get(SCPKG_CMD_NTF_RECALL_FRIEND.CLASS_ID);
                        }
                        return;

                    case 7:
                        if (!(this.dataObject is CSPKG_OPER_HERO_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_OPER_HERO_REQ) ProtocolObjectPool.Get(CSPKG_OPER_HERO_REQ.CLASS_ID);
                        }
                        return;

                    case 8:
                        if (!(this.dataObject is SCPKG_OPER_HERO_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_OPER_HERO_NTF) ProtocolObjectPool.Get(SCPKG_OPER_HERO_NTF.CLASS_ID);
                        }
                        return;

                    case 9:
                        if (!(this.dataObject is CSPKG_CONFIRM_HERO))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CONFIRM_HERO) ProtocolObjectPool.Get(CSPKG_CONFIRM_HERO.CLASS_ID);
                        }
                        return;

                    case 10:
                        if (!(this.dataObject is SCPKG_CONFIRM_HERO_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CONFIRM_HERO_NTF) ProtocolObjectPool.Get(SCPKG_CONFIRM_HERO_NTF.CLASS_ID);
                        }
                        return;

                    case 11:
                        if (!(this.dataObject is SCPKG_DEFAULT_HERO_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_DEFAULT_HERO_NTF) ProtocolObjectPool.Get(SCPKG_DEFAULT_HERO_NTF.CLASS_ID);
                        }
                        return;
                }
            }
            if ((num >= 0x5dcL) && (num <= 0x5e3L))
            {
                switch (((int) (num - 0x5dcL)))
                {
                    case 0:
                        if (!(this.dataObject is CSPKG_USUALTASK_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_USUALTASK_REQ) ProtocolObjectPool.Get(CSPKG_USUALTASK_REQ.CLASS_ID);
                        }
                        return;

                    case 1:
                        if (!(this.dataObject is SCPKG_USUALTASK_RES))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_USUALTASK_RES) ProtocolObjectPool.Get(SCPKG_USUALTASK_RES.CLASS_ID);
                        }
                        return;

                    case 2:
                        if (!(this.dataObject is CSPKG_TASKSUBMIT_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_TASKSUBMIT_REQ) ProtocolObjectPool.Get(CSPKG_TASKSUBMIT_REQ.CLASS_ID);
                        }
                        return;

                    case 3:
                        if (!(this.dataObject is SCPKG_TASKSUBMIT_RES))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_TASKSUBMIT_RES) ProtocolObjectPool.Get(SCPKG_TASKSUBMIT_RES.CLASS_ID);
                        }
                        return;

                    case 4:
                        if (!(this.dataObject is SCPKG_TASKUPD_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_TASKUPD_NTF) ProtocolObjectPool.Get(SCPKG_TASKUPD_NTF.CLASS_ID);
                        }
                        return;

                    case 5:
                        if (!(this.dataObject is CSPKG_TASKDONE_CLIENTREPORT))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_TASKDONE_CLIENTREPORT) ProtocolObjectPool.Get(CSPKG_TASKDONE_CLIENTREPORT.CLASS_ID);
                        }
                        return;

                    case 6:
                        if (!(this.dataObject is SCPKG_USUALTASKDISCARD_RES))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_USUALTASKDISCARD_RES) ProtocolObjectPool.Get(SCPKG_USUALTASKDISCARD_RES.CLASS_ID);
                        }
                        return;

                    case 7:
                        if (!(this.dataObject is SCPKG_NEWTASKGET_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_NEWTASKGET_NTF) ProtocolObjectPool.Get(SCPKG_NEWTASKGET_NTF.CLASS_ID);
                        }
                        return;
                }
            }
            if ((num >= 0x514L) && (num <= 0x51aL))
            {
                switch (((int) (num - 0x514L)))
                {
                    case 0:
                        if (!(this.dataObject is CSPKG_CMD_CHAT_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_CHAT_REQ) ProtocolObjectPool.Get(CSPKG_CMD_CHAT_REQ.CLASS_ID);
                        }
                        return;

                    case 1:
                        if (!(this.dataObject is CSPKG_CMD_GET_CHAT_MSG_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_GET_CHAT_MSG_REQ) ProtocolObjectPool.Get(CSPKG_CMD_GET_CHAT_MSG_REQ.CLASS_ID);
                        }
                        return;

                    case 2:
                        if (!(this.dataObject is SCPKG_CMD_CHAT_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_CHAT_NTF) ProtocolObjectPool.Get(SCPKG_CMD_CHAT_NTF.CLASS_ID);
                        }
                        return;

                    case 5:
                        if (!(this.dataObject is CSPKG_CMD_GET_HORNMSG))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CMD_GET_HORNMSG) ProtocolObjectPool.Get(CSPKG_CMD_GET_HORNMSG.CLASS_ID);
                        }
                        return;

                    case 6:
                        if (!(this.dataObject is SCPKG_CMD_GET_HORNMSG))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_GET_HORNMSG) ProtocolObjectPool.Get(SCPKG_CMD_GET_HORNMSG.CLASS_ID);
                        }
                        return;
                }
            }
            if ((num >= 0x4ecL) && (num <= 0x4efL))
            {
                switch (((int) (num - 0x4ecL)))
                {
                    case 0:
                        if (!(this.dataObject is CSPKG_RELAYSVRPING))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_RELAYSVRPING) ProtocolObjectPool.Get(CSPKG_RELAYSVRPING.CLASS_ID);
                        }
                        return;

                    case 1:
                        if (!(this.dataObject is CSPKG_GAMESVRPING))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GAMESVRPING) ProtocolObjectPool.Get(CSPKG_GAMESVRPING.CLASS_ID);
                        }
                        return;

                    case 2:
                        if (!(this.dataObject is CSPKG_CLRCDLIMIT_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CLRCDLIMIT_REQ) ProtocolObjectPool.Get(CSPKG_CLRCDLIMIT_REQ.CLASS_ID);
                        }
                        return;

                    case 3:
                        if (!(this.dataObject is SCPKG_CLRCDLIMIT_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CLRCDLIMIT_RSP) ProtocolObjectPool.Get(SCPKG_CLRCDLIMIT_RSP.CLASS_ID);
                        }
                        return;
                }
            }
            if ((num >= 0x500L) && (num <= 0x503L))
            {
                switch (((int) (num - 0x500L)))
                {
                    case 0:
                        if (!(this.dataObject is CSPKG_RELAYHASHCHECK))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_RELAYHASHCHECK) ProtocolObjectPool.Get(CSPKG_RELAYHASHCHECK.CLASS_ID);
                        }
                        return;

                    case 1:
                        if (!(this.dataObject is CSPKG_NEXTFIRSTWINSEC_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_NEXTFIRSTWINSEC_NTF) ProtocolObjectPool.Get(CSPKG_NEXTFIRSTWINSEC_NTF.CLASS_ID);
                        }
                        return;

                    case 2:
                        if (!(this.dataObject is CSPKG_COINGETPATH_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_COINGETPATH_REQ) ProtocolObjectPool.Get(CSPKG_COINGETPATH_REQ.CLASS_ID);
                        }
                        return;

                    case 3:
                        if (!(this.dataObject is SCPKG_COINGETPATH_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_COINGETPATH_RSP) ProtocolObjectPool.Get(SCPKG_COINGETPATH_RSP.CLASS_ID);
                        }
                        return;
                }
            }
            if ((num >= 0x640L) && (num <= 0x643L))
            {
                switch (((int) (num - 0x640L)))
                {
                    case 0:
                        if (!(this.dataObject is SCPKG_NTF_HUOYUEDUINFO))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_NTF_HUOYUEDUINFO) ProtocolObjectPool.Get(SCPKG_NTF_HUOYUEDUINFO.CLASS_ID);
                        }
                        return;

                    case 1:
                        if (!(this.dataObject is CSPKG_GETHUOYUEDUREWARD_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GETHUOYUEDUREWARD_REQ) ProtocolObjectPool.Get(CSPKG_GETHUOYUEDUREWARD_REQ.CLASS_ID);
                        }
                        return;

                    case 2:
                        if (!(this.dataObject is SCPKG_GETHUOYUEDUREWARD_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GETHUOYUEDUREWARD_RSP) ProtocolObjectPool.Get(SCPKG_GETHUOYUEDUREWARD_RSP.CLASS_ID);
                        }
                        return;

                    case 3:
                        if (!(this.dataObject is SCPKG_HUOYUEDUREWARDERR_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_HUOYUEDUREWARDERR_NTF) ProtocolObjectPool.Get(SCPKG_HUOYUEDUREWARDERR_NTF.CLASS_ID);
                        }
                        return;
                }
            }
            if (num == 0x528L)
            {
                if (!(this.dataObject is CSPKG_CMD_GAIN_CHEST))
                {
                    if (this.dataObject != null)
                    {
                        this.dataObject.Release();
                    }
                    this.dataObject = (CSPKG_CMD_GAIN_CHEST) ProtocolObjectPool.Get(CSPKG_CMD_GAIN_CHEST.CLASS_ID);
                }
            }
            else if (num == 0x529L)
            {
                if (!(this.dataObject is SCPKG_CMD_GAIN_CHEST))
                {
                    if (this.dataObject != null)
                    {
                        this.dataObject.Release();
                    }
                    this.dataObject = (SCPKG_CMD_GAIN_CHEST) ProtocolObjectPool.Get(SCPKG_CMD_GAIN_CHEST.CLASS_ID);
                }
            }
            else if (num == 0x546L)
            {
                if (!(this.dataObject is CSPKG_CMD_LICENSE_REQ))
                {
                    if (this.dataObject != null)
                    {
                        this.dataObject.Release();
                    }
                    this.dataObject = (CSPKG_CMD_LICENSE_REQ) ProtocolObjectPool.Get(CSPKG_CMD_LICENSE_REQ.CLASS_ID);
                }
            }
            else if (num == 0x547L)
            {
                if (!(this.dataObject is SCPKG_CMD_LICENSE_RSP))
                {
                    if (this.dataObject != null)
                    {
                        this.dataObject.Release();
                    }
                    this.dataObject = (SCPKG_CMD_LICENSE_RSP) ProtocolObjectPool.Get(SCPKG_CMD_LICENSE_RSP.CLASS_ID);
                }
            }
            else if (num == 0x5aaL)
            {
                if (!(this.dataObject is SCPKG_ROLLINGMSG_NTF))
                {
                    if (this.dataObject != null)
                    {
                        this.dataObject.Release();
                    }
                    this.dataObject = (SCPKG_ROLLINGMSG_NTF) ProtocolObjectPool.Get(SCPKG_ROLLINGMSG_NTF.CLASS_ID);
                }
            }
            else if (num == 0x708L)
            {
                if (!(this.dataObject is CSPKG_ACHIEVEHERO_REQ))
                {
                    if (this.dataObject != null)
                    {
                        this.dataObject.Release();
                    }
                    this.dataObject = (CSPKG_ACHIEVEHERO_REQ) ProtocolObjectPool.Get(CSPKG_ACHIEVEHERO_REQ.CLASS_ID);
                }
            }
            else if (this.dataObject != null)
            {
                this.dataObject.Release();
                this.dataObject = null;
            }
        }

        private void select_1801_2201(long selector)
        {
            long num = selector;
            if ((num >= 0x709L) && (num <= 0x729L))
            {
                switch (((int) (num - 0x709L)))
                {
                    case 0:
                        if (!(this.dataObject is SCPKG_ACHIEVEHERO_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_ACHIEVEHERO_RSP) ProtocolObjectPool.Get(SCPKG_ACHIEVEHERO_RSP.CLASS_ID);
                        }
                        return;

                    case 1:
                        if (!(this.dataObject is SCPKG_ACNTHEROINFO_NTY))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_ACNTHEROINFO_NTY) ProtocolObjectPool.Get(SCPKG_ACNTHEROINFO_NTY.CLASS_ID);
                        }
                        return;

                    case 2:
                        if (!(this.dataObject is SCPKG_CMD_HEROEXP_ADD))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_HEROEXP_ADD) ProtocolObjectPool.Get(SCPKG_CMD_HEROEXP_ADD.CLASS_ID);
                        }
                        return;

                    case 3:
                        if (!(this.dataObject is SCPKG_ADDHERO_NTY))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_ADDHERO_NTY) ProtocolObjectPool.Get(SCPKG_ADDHERO_NTY.CLASS_ID);
                        }
                        return;

                    case 4:
                        if (!(this.dataObject is CSPKG_BATTLELIST_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_BATTLELIST_REQ) ProtocolObjectPool.Get(CSPKG_BATTLELIST_REQ.CLASS_ID);
                        }
                        return;

                    case 5:
                        if (!(this.dataObject is SCPKG_BATTLELIST_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_BATTLELIST_RSP) ProtocolObjectPool.Get(SCPKG_BATTLELIST_RSP.CLASS_ID);
                        }
                        return;

                    case 6:
                        if (!(this.dataObject is SCPKG_BATTLELIST_NTY))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_BATTLELIST_NTY) ProtocolObjectPool.Get(SCPKG_BATTLELIST_NTY.CLASS_ID);
                        }
                        return;

                    case 7:
                        if (!(this.dataObject is CSPKG_UPGRADESTAR_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_UPGRADESTAR_REQ) ProtocolObjectPool.Get(CSPKG_UPGRADESTAR_REQ.CLASS_ID);
                        }
                        return;

                    case 8:
                        if (!(this.dataObject is SCPKG_UPGRADESTAR_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_UPGRADESTAR_RSP) ProtocolObjectPool.Get(SCPKG_UPGRADESTAR_RSP.CLASS_ID);
                        }
                        return;

                    case 9:
                        if (!(this.dataObject is SCPKG_NTF_HERO_INFO_UPD))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_NTF_HERO_INFO_UPD) ProtocolObjectPool.Get(SCPKG_NTF_HERO_INFO_UPD.CLASS_ID);
                        }
                        return;

                    case 10:
                        if (!(this.dataObject is CSPKG_SKILLUPDATE_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_SKILLUPDATE_REQ) ProtocolObjectPool.Get(CSPKG_SKILLUPDATE_REQ.CLASS_ID);
                        }
                        return;

                    case 11:
                        if (!(this.dataObject is SCPKG_SKILLUPDATE_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_SKILLUPDATE_RSP) ProtocolObjectPool.Get(SCPKG_SKILLUPDATE_RSP.CLASS_ID);
                        }
                        return;

                    case 12:
                        if (!(this.dataObject is SCPKG_ACHIEVEHERO_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_ACHIEVEHERO_RSP) ProtocolObjectPool.Get(SCPKG_ACHIEVEHERO_RSP.CLASS_ID);
                        }
                        return;

                    case 13:
                        if (!(this.dataObject is CSPKG_FREEHERO_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_FREEHERO_REQ) ProtocolObjectPool.Get(CSPKG_FREEHERO_REQ.CLASS_ID);
                        }
                        return;

                    case 14:
                        if (!(this.dataObject is SCPKG_FREEHERO_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_FREEHERO_RSP) ProtocolObjectPool.Get(SCPKG_FREEHERO_RSP.CLASS_ID);
                        }
                        return;

                    case 15:
                        if (!(this.dataObject is SCPKG_GMUNLOGCKHEROPVP_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GMUNLOGCKHEROPVP_RSP) ProtocolObjectPool.Get(SCPKG_GMUNLOGCKHEROPVP_RSP.CLASS_ID);
                        }
                        return;

                    case 0x10:
                        if (!(this.dataObject is CSPKG_BUYHERO_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_BUYHERO_REQ) ProtocolObjectPool.Get(CSPKG_BUYHERO_REQ.CLASS_ID);
                        }
                        return;

                    case 0x11:
                        if (!(this.dataObject is SCPKG_BUYHERO_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_BUYHERO_RSP) ProtocolObjectPool.Get(SCPKG_BUYHERO_RSP.CLASS_ID);
                        }
                        return;

                    case 0x12:
                        if (!(this.dataObject is CSPKG_BUYHEROSKIN_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_BUYHEROSKIN_REQ) ProtocolObjectPool.Get(CSPKG_BUYHEROSKIN_REQ.CLASS_ID);
                        }
                        return;

                    case 0x13:
                        if (!(this.dataObject is SCPKG_BUYHEROSKIN_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_BUYHEROSKIN_RSP) ProtocolObjectPool.Get(SCPKG_BUYHEROSKIN_RSP.CLASS_ID);
                        }
                        return;

                    case 20:
                        if (!(this.dataObject is CSPKG_WEARHEROSKIN_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_WEARHEROSKIN_REQ) ProtocolObjectPool.Get(CSPKG_WEARHEROSKIN_REQ.CLASS_ID);
                        }
                        return;

                    case 0x15:
                        if (!(this.dataObject is SCPKG_WEARHEROSKIN_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_WEARHEROSKIN_RSP) ProtocolObjectPool.Get(SCPKG_WEARHEROSKIN_RSP.CLASS_ID);
                        }
                        return;

                    case 0x16:
                        if (!(this.dataObject is SCPKG_HEROSKIN_ADD))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_HEROSKIN_ADD) ProtocolObjectPool.Get(SCPKG_HEROSKIN_ADD.CLASS_ID);
                        }
                        return;

                    case 0x17:
                        if (!(this.dataObject is CSPKG_UPHEROLVL_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_UPHEROLVL_REQ) ProtocolObjectPool.Get(CSPKG_UPHEROLVL_REQ.CLASS_ID);
                        }
                        return;

                    case 0x18:
                        if (!(this.dataObject is SCPKG_UPHEROLVL_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_UPHEROLVL_RSP) ProtocolObjectPool.Get(SCPKG_UPHEROLVL_RSP.CLASS_ID);
                        }
                        return;

                    case 0x19:
                        if (!(this.dataObject is SCPKG_LIMITSKIN_ADD))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_LIMITSKIN_ADD) ProtocolObjectPool.Get(SCPKG_LIMITSKIN_ADD.CLASS_ID);
                        }
                        return;

                    case 0x1a:
                        if (!(this.dataObject is SCPKG_LIMITSKIN_DEL))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_LIMITSKIN_DEL) ProtocolObjectPool.Get(SCPKG_LIMITSKIN_DEL.CLASS_ID);
                        }
                        return;

                    case 0x1b:
                        if (!(this.dataObject is SCPKG_USEEXPCARD_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_USEEXPCARD_NTF) ProtocolObjectPool.Get(SCPKG_USEEXPCARD_NTF.CLASS_ID);
                        }
                        return;

                    case 0x1c:
                        if (!(this.dataObject is SCPKG_GM_ADDALLSKIN_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GM_ADDALLSKIN_RSP) ProtocolObjectPool.Get(SCPKG_GM_ADDALLSKIN_RSP.CLASS_ID);
                        }
                        return;

                    case 0x1d:
                        if (!(this.dataObject is CSPKG_PRESENTHERO_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_PRESENTHERO_REQ) ProtocolObjectPool.Get(CSPKG_PRESENTHERO_REQ.CLASS_ID);
                        }
                        return;

                    case 30:
                        if (!(this.dataObject is SCPKG_PRESENTHERO_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_PRESENTHERO_RSP) ProtocolObjectPool.Get(SCPKG_PRESENTHERO_RSP.CLASS_ID);
                        }
                        return;

                    case 0x1f:
                        if (!(this.dataObject is CSPKG_PRESENTSKIN_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_PRESENTSKIN_REQ) ProtocolObjectPool.Get(CSPKG_PRESENTSKIN_REQ.CLASS_ID);
                        }
                        return;

                    case 0x20:
                        if (!(this.dataObject is SCPKG_PERSENTSKIN_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_PERSENTSKIN_RSP) ProtocolObjectPool.Get(SCPKG_PERSENTSKIN_RSP.CLASS_ID);
                        }
                        return;
                }
            }
            if ((num >= 0x7daL) && (num <= 0x7f4L))
            {
                switch (((int) (num - 0x7daL)))
                {
                    case 0:
                        if (!(this.dataObject is CSPKG_MATCH_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_MATCH_REQ) ProtocolObjectPool.Get(CSPKG_MATCH_REQ.CLASS_ID);
                        }
                        return;

                    case 1:
                        if (!(this.dataObject is SCPKG_MATCH_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_MATCH_RSP) ProtocolObjectPool.Get(SCPKG_MATCH_RSP.CLASS_ID);
                        }
                        return;

                    case 2:
                        if (!(this.dataObject is SCPKG_ROOM_STARTSINGLEGAME_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_ROOM_STARTSINGLEGAME_NTF) ProtocolObjectPool.Get(SCPKG_ROOM_STARTSINGLEGAME_NTF.CLASS_ID);
                        }
                        return;

                    case 3:
                        if (!(this.dataObject is CSPKG_START_MULTI_GAME_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_START_MULTI_GAME_REQ) ProtocolObjectPool.Get(CSPKG_START_MULTI_GAME_REQ.CLASS_ID);
                        }
                        return;

                    case 4:
                        if (!(this.dataObject is SCPKG_START_MULTI_GAME_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_START_MULTI_GAME_RSP) ProtocolObjectPool.Get(SCPKG_START_MULTI_GAME_RSP.CLASS_ID);
                        }
                        return;

                    case 5:
                        if (!(this.dataObject is CSPKG_ADD_NPC_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_ADD_NPC_REQ) ProtocolObjectPool.Get(CSPKG_ADD_NPC_REQ.CLASS_ID);
                        }
                        return;

                    case 7:
                        if (!(this.dataObject is CSPKG_INVITE_FRIEND_JOIN_ROOM_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_INVITE_FRIEND_JOIN_ROOM_REQ) ProtocolObjectPool.Get(CSPKG_INVITE_FRIEND_JOIN_ROOM_REQ.CLASS_ID);
                        }
                        return;

                    case 8:
                        if (!(this.dataObject is SCPKG_INVITE_FRIEND_JOIN_ROOM_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_INVITE_FRIEND_JOIN_ROOM_RSP) ProtocolObjectPool.Get(SCPKG_INVITE_FRIEND_JOIN_ROOM_RSP.CLASS_ID);
                        }
                        return;

                    case 9:
                        if (!(this.dataObject is CSPKG_KICKOUT_ROOMMEMBER_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_KICKOUT_ROOMMEMBER_REQ) ProtocolObjectPool.Get(CSPKG_KICKOUT_ROOMMEMBER_REQ.CLASS_ID);
                        }
                        return;

                    case 10:
                        if (!(this.dataObject is SCPKG_INVITE_JOIN_GAME_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_INVITE_JOIN_GAME_REQ) ProtocolObjectPool.Get(SCPKG_INVITE_JOIN_GAME_REQ.CLASS_ID);
                        }
                        return;

                    case 11:
                        if (!(this.dataObject is CSPKG_INVITE_JOIN_GAME_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_INVITE_JOIN_GAME_RSP) ProtocolObjectPool.Get(CSPKG_INVITE_JOIN_GAME_RSP.CLASS_ID);
                        }
                        return;

                    case 12:
                        if (!(this.dataObject is CSPKG_CREATE_TEAM_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CREATE_TEAM_REQ) ProtocolObjectPool.Get(CSPKG_CREATE_TEAM_REQ.CLASS_ID);
                        }
                        return;

                    case 13:
                        if (!(this.dataObject is SCPKG_JOIN_TEAM_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_JOIN_TEAM_RSP) ProtocolObjectPool.Get(SCPKG_JOIN_TEAM_RSP.CLASS_ID);
                        }
                        return;

                    case 14:
                        if (!(this.dataObject is CSPKG_INVITE_FRIEND_JOIN_TEAM_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_INVITE_FRIEND_JOIN_TEAM_REQ) ProtocolObjectPool.Get(CSPKG_INVITE_FRIEND_JOIN_TEAM_REQ.CLASS_ID);
                        }
                        return;

                    case 15:
                        if (!(this.dataObject is SCPKG_INVITE_FRIEND_JOIN_TEAM_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_INVITE_FRIEND_JOIN_TEAM_RSP) ProtocolObjectPool.Get(SCPKG_INVITE_FRIEND_JOIN_TEAM_RSP.CLASS_ID);
                        }
                        return;

                    case 0x10:
                        if (!(this.dataObject is SCPKG_TEAM_CHG))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_TEAM_CHG) ProtocolObjectPool.Get(SCPKG_TEAM_CHG.CLASS_ID);
                        }
                        return;

                    case 0x11:
                        if (!(this.dataObject is CSPKG_LEAVL_TEAM))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_LEAVL_TEAM) ProtocolObjectPool.Get(CSPKG_LEAVL_TEAM.CLASS_ID);
                        }
                        return;

                    case 0x12:
                        if (!(this.dataObject is CSPKG_OPER_TEAM_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_OPER_TEAM_REQ) ProtocolObjectPool.Get(CSPKG_OPER_TEAM_REQ.CLASS_ID);
                        }
                        return;

                    case 0x13:
                        if (!(this.dataObject is SCPKG_SELF_BEKICK_FROM_TEAM))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_SELF_BEKICK_FROM_TEAM) ProtocolObjectPool.Get(SCPKG_SELF_BEKICK_FROM_TEAM.CLASS_ID);
                        }
                        return;

                    case 20:
                        if (!(this.dataObject is SCPKG_ACNT_LEAVE_TEAM_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_ACNT_LEAVE_TEAM_RSP) ProtocolObjectPool.Get(SCPKG_ACNT_LEAVE_TEAM_RSP.CLASS_ID);
                        }
                        return;

                    case 0x15:
                        if (!(this.dataObject is CSPKG_ROOM_CONFIRM_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_ROOM_CONFIRM_REQ) ProtocolObjectPool.Get(CSPKG_ROOM_CONFIRM_REQ.CLASS_ID);
                        }
                        return;

                    case 0x16:
                        if (!(this.dataObject is SCPKG_ROOM_CONFIRM_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_ROOM_CONFIRM_RSP) ProtocolObjectPool.Get(SCPKG_ROOM_CONFIRM_RSP.CLASS_ID);
                        }
                        return;

                    case 0x17:
                        if (!(this.dataObject is CSPKG_ROOM_CHGMEMBERPOS_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_ROOM_CHGMEMBERPOS_REQ) ProtocolObjectPool.Get(CSPKG_ROOM_CHGMEMBERPOS_REQ.CLASS_ID);
                        }
                        return;

                    case 0x18:
                        if (!(this.dataObject is CSPKG_INVITE_GUILD_MEMBER_JOIN_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_INVITE_GUILD_MEMBER_JOIN_REQ) ProtocolObjectPool.Get(CSPKG_INVITE_GUILD_MEMBER_JOIN_REQ.CLASS_ID);
                        }
                        return;

                    case 0x19:
                        if (!(this.dataObject is CSPKG_GET_GUILD_MEMBER_GAME_STATE_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GET_GUILD_MEMBER_GAME_STATE_REQ) ProtocolObjectPool.Get(CSPKG_GET_GUILD_MEMBER_GAME_STATE_REQ.CLASS_ID);
                        }
                        return;

                    case 0x1a:
                        if (!(this.dataObject is SCPKG_GET_GUILD_MEMBER_GAME_STATE_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GET_GUILD_MEMBER_GAME_STATE_RSP) ProtocolObjectPool.Get(SCPKG_GET_GUILD_MEMBER_GAME_STATE_RSP.CLASS_ID);
                        }
                        return;
                }
            }
            if (num == 0x899L)
            {
                if (!(this.dataObject is CSPKG_GET_GUILD_LIST_REQ))
                {
                    if (this.dataObject != null)
                    {
                        this.dataObject.Release();
                    }
                    this.dataObject = (CSPKG_GET_GUILD_LIST_REQ) ProtocolObjectPool.Get(CSPKG_GET_GUILD_LIST_REQ.CLASS_ID);
                }
            }
            else if (this.dataObject != null)
            {
                this.dataObject.Release();
                this.dataObject = null;
            }
        }

        private void select_2202_2261(long selector)
        {
            long num = selector;
            if ((num >= 0x89aL) && (num <= 0x8d5L))
            {
                switch (((int) (num - 0x89aL)))
                {
                    case 0:
                        if (!(this.dataObject is SCPKG_GET_GUILD_LIST_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GET_GUILD_LIST_RSP) ProtocolObjectPool.Get(SCPKG_GET_GUILD_LIST_RSP.CLASS_ID);
                        }
                        return;

                    case 1:
                        if (!(this.dataObject is CSPKG_GET_PREPARE_GUILD_LIST_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GET_PREPARE_GUILD_LIST_REQ) ProtocolObjectPool.Get(CSPKG_GET_PREPARE_GUILD_LIST_REQ.CLASS_ID);
                        }
                        return;

                    case 2:
                        if (!(this.dataObject is SCPKG_GET_PREPARE_GUILD_LIST_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GET_PREPARE_GUILD_LIST_RSP) ProtocolObjectPool.Get(SCPKG_GET_PREPARE_GUILD_LIST_RSP.CLASS_ID);
                        }
                        return;

                    case 3:
                        if (!(this.dataObject is CSPKG_GET_GUILD_INFO_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GET_GUILD_INFO_REQ) ProtocolObjectPool.Get(CSPKG_GET_GUILD_INFO_REQ.CLASS_ID);
                        }
                        return;

                    case 4:
                        if (!(this.dataObject is SCPKG_GET_GUILD_INFO_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GET_GUILD_INFO_RSP) ProtocolObjectPool.Get(SCPKG_GET_GUILD_INFO_RSP.CLASS_ID);
                        }
                        return;

                    case 5:
                        if (!(this.dataObject is CSPKG_GET_PREPARE_GUILD_INFO_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GET_PREPARE_GUILD_INFO_REQ) ProtocolObjectPool.Get(CSPKG_GET_PREPARE_GUILD_INFO_REQ.CLASS_ID);
                        }
                        return;

                    case 6:
                        if (!(this.dataObject is SCPKG_GET_PREPARE_GUILD_INFO_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GET_PREPARE_GUILD_INFO_RSP) ProtocolObjectPool.Get(SCPKG_GET_PREPARE_GUILD_INFO_RSP.CLASS_ID);
                        }
                        return;

                    case 7:
                        if (!(this.dataObject is CSPKG_CREATE_GUILD_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CREATE_GUILD_REQ) ProtocolObjectPool.Get(CSPKG_CREATE_GUILD_REQ.CLASS_ID);
                        }
                        return;

                    case 8:
                        if (!(this.dataObject is SCPKG_CREATE_GUILD_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CREATE_GUILD_RSP) ProtocolObjectPool.Get(SCPKG_CREATE_GUILD_RSP.CLASS_ID);
                        }
                        return;

                    case 9:
                        if (!(this.dataObject is CSPKG_JOIN_PREPARE_GUILD_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_JOIN_PREPARE_GUILD_REQ) ProtocolObjectPool.Get(CSPKG_JOIN_PREPARE_GUILD_REQ.CLASS_ID);
                        }
                        return;

                    case 10:
                        if (!(this.dataObject is SCPKG_JOIN_PREPARE_GUILD_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_JOIN_PREPARE_GUILD_RSP) ProtocolObjectPool.Get(SCPKG_JOIN_PREPARE_GUILD_RSP.CLASS_ID);
                        }
                        return;

                    case 11:
                        if (!(this.dataObject is SCPKG_JOIN_PREPARE_GUILD_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_JOIN_PREPARE_GUILD_NTF) ProtocolObjectPool.Get(SCPKG_JOIN_PREPARE_GUILD_NTF.CLASS_ID);
                        }
                        return;

                    case 12:
                        if (!(this.dataObject is SCPKG_ADD_GUILD_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_ADD_GUILD_NTF) ProtocolObjectPool.Get(SCPKG_ADD_GUILD_NTF.CLASS_ID);
                        }
                        return;

                    case 13:
                        if (!(this.dataObject is SCPKG_MEMBER_ONLINE_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_MEMBER_ONLINE_NTF) ProtocolObjectPool.Get(SCPKG_MEMBER_ONLINE_NTF.CLASS_ID);
                        }
                        return;

                    case 14:
                        if (!(this.dataObject is SCPKG_PREPARE_GUILD_BREAK_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_PREPARE_GUILD_BREAK_NTF) ProtocolObjectPool.Get(SCPKG_PREPARE_GUILD_BREAK_NTF.CLASS_ID);
                        }
                        return;

                    case 15:
                        if (!(this.dataObject is CSPKG_MODIFY_GUILD_SETTING_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_MODIFY_GUILD_SETTING_REQ) ProtocolObjectPool.Get(CSPKG_MODIFY_GUILD_SETTING_REQ.CLASS_ID);
                        }
                        return;

                    case 0x10:
                        if (!(this.dataObject is SCPKG_MODIFY_GUILD_SETTING_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_MODIFY_GUILD_SETTING_RSP) ProtocolObjectPool.Get(SCPKG_MODIFY_GUILD_SETTING_RSP.CLASS_ID);
                        }
                        return;

                    case 0x11:
                        if (!(this.dataObject is CSPKG_GET_GUILD_APPLY_LIST_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GET_GUILD_APPLY_LIST_REQ) ProtocolObjectPool.Get(CSPKG_GET_GUILD_APPLY_LIST_REQ.CLASS_ID);
                        }
                        return;

                    case 0x12:
                        if (!(this.dataObject is SCPKG_GET_GUILD_APPLY_LIST_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GET_GUILD_APPLY_LIST_RSP) ProtocolObjectPool.Get(SCPKG_GET_GUILD_APPLY_LIST_RSP.CLASS_ID);
                        }
                        return;

                    case 0x13:
                        if (!(this.dataObject is CSPKG_APPLY_JOIN_GUILD_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_APPLY_JOIN_GUILD_REQ) ProtocolObjectPool.Get(CSPKG_APPLY_JOIN_GUILD_REQ.CLASS_ID);
                        }
                        return;

                    case 20:
                        if (!(this.dataObject is SCPKG_APPLY_JOIN_GUILD_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_APPLY_JOIN_GUILD_RSP) ProtocolObjectPool.Get(SCPKG_APPLY_JOIN_GUILD_RSP.CLASS_ID);
                        }
                        return;

                    case 0x15:
                        if (!(this.dataObject is SCPKG_JOIN_GUILD_APPLY_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_JOIN_GUILD_APPLY_NTF) ProtocolObjectPool.Get(SCPKG_JOIN_GUILD_APPLY_NTF.CLASS_ID);
                        }
                        return;

                    case 0x16:
                        if (!(this.dataObject is SCPKG_NEW_MEMBER_JOIN_GULD_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_NEW_MEMBER_JOIN_GULD_NTF) ProtocolObjectPool.Get(SCPKG_NEW_MEMBER_JOIN_GULD_NTF.CLASS_ID);
                        }
                        return;

                    case 0x17:
                        if (!(this.dataObject is CSPKG_APPROVE_JOIN_GUILD_APPLY))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_APPROVE_JOIN_GUILD_APPLY) ProtocolObjectPool.Get(CSPKG_APPROVE_JOIN_GUILD_APPLY.CLASS_ID);
                        }
                        return;

                    case 0x18:
                        if (!(this.dataObject is CSPKG_QUIT_GUILD_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_QUIT_GUILD_REQ) ProtocolObjectPool.Get(CSPKG_QUIT_GUILD_REQ.CLASS_ID);
                        }
                        return;

                    case 0x19:
                        if (!(this.dataObject is SCPKG_QUIT_GUILD_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_QUIT_GUILD_RSP) ProtocolObjectPool.Get(SCPKG_QUIT_GUILD_RSP.CLASS_ID);
                        }
                        return;

                    case 0x1a:
                        if (!(this.dataObject is SCPKG_QUIT_GUILD_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_QUIT_GUILD_NTF) ProtocolObjectPool.Get(SCPKG_QUIT_GUILD_NTF.CLASS_ID);
                        }
                        return;

                    case 0x1b:
                        if (!(this.dataObject is CSPKG_GUILD_INVITE_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GUILD_INVITE_REQ) ProtocolObjectPool.Get(CSPKG_GUILD_INVITE_REQ.CLASS_ID);
                        }
                        return;

                    case 0x1c:
                        if (!(this.dataObject is SCPKG_GUILD_INVITE_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GUILD_INVITE_RSP) ProtocolObjectPool.Get(SCPKG_GUILD_INVITE_RSP.CLASS_ID);
                        }
                        return;

                    case 0x1d:
                        if (!(this.dataObject is CSPKG_SEARCH_GUILD_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_SEARCH_GUILD_REQ) ProtocolObjectPool.Get(CSPKG_SEARCH_GUILD_REQ.CLASS_ID);
                        }
                        return;

                    case 30:
                        if (!(this.dataObject is SCPKG_SEARCH_GUILD_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_SEARCH_GUILD_RSP) ProtocolObjectPool.Get(SCPKG_SEARCH_GUILD_RSP.CLASS_ID);
                        }
                        return;

                    case 0x1f:
                        if (!(this.dataObject is CSPKG_DEAL_GUILD_INVITE))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_DEAL_GUILD_INVITE) ProtocolObjectPool.Get(CSPKG_DEAL_GUILD_INVITE.CLASS_ID);
                        }
                        return;

                    case 0x20:
                        if (!(this.dataObject is CSPKG_GUILD_RECOMMEND_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GUILD_RECOMMEND_REQ) ProtocolObjectPool.Get(CSPKG_GUILD_RECOMMEND_REQ.CLASS_ID);
                        }
                        return;

                    case 0x21:
                        if (!(this.dataObject is SCPKG_GUILD_RECOMMEND_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GUILD_RECOMMEND_RSP) ProtocolObjectPool.Get(SCPKG_GUILD_RECOMMEND_RSP.CLASS_ID);
                        }
                        return;

                    case 0x22:
                        if (!(this.dataObject is SCPKG_GUILD_RECOMMEND_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GUILD_RECOMMEND_NTF) ProtocolObjectPool.Get(SCPKG_GUILD_RECOMMEND_NTF.CLASS_ID);
                        }
                        return;

                    case 0x23:
                        if (!(this.dataObject is CSPKG_GET_GUILD_RECOMMEND_LIST_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GET_GUILD_RECOMMEND_LIST_REQ) ProtocolObjectPool.Get(CSPKG_GET_GUILD_RECOMMEND_LIST_REQ.CLASS_ID);
                        }
                        return;

                    case 0x24:
                        if (!(this.dataObject is SCPKG_GET_GUILD_RECOMMEND_LIST_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GET_GUILD_RECOMMEND_LIST_RSP) ProtocolObjectPool.Get(SCPKG_GET_GUILD_RECOMMEND_LIST_RSP.CLASS_ID);
                        }
                        return;

                    case 0x25:
                        if (!(this.dataObject is CSPKG_REJECT_GUILD_RECOMMEND))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_REJECT_GUILD_RECOMMEND) ProtocolObjectPool.Get(CSPKG_REJECT_GUILD_RECOMMEND.CLASS_ID);
                        }
                        return;

                    case 0x26:
                        if (!(this.dataObject is SCPKG_DEAL_GUILD_INVITE_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_DEAL_GUILD_INVITE_RSP) ProtocolObjectPool.Get(SCPKG_DEAL_GUILD_INVITE_RSP.CLASS_ID);
                        }
                        return;

                    case 0x27:
                        if (!(this.dataObject is CSPKG_SEARCH_PREGUILD_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_SEARCH_PREGUILD_REQ) ProtocolObjectPool.Get(CSPKG_SEARCH_PREGUILD_REQ.CLASS_ID);
                        }
                        return;

                    case 40:
                        if (!(this.dataObject is SCPKG_SEARCH_PREGUILD_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_SEARCH_PREGUILD_RSP) ProtocolObjectPool.Get(SCPKG_SEARCH_PREGUILD_RSP.CLASS_ID);
                        }
                        return;

                    case 0x29:
                        if (!(this.dataObject is CSPKG_GUILD_BUILDING_UPGRADE_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GUILD_BUILDING_UPGRADE_REQ) ProtocolObjectPool.Get(CSPKG_GUILD_BUILDING_UPGRADE_REQ.CLASS_ID);
                        }
                        return;

                    case 0x2a:
                        if (!(this.dataObject is SCPKG_GUILD_BUILDING_UPGRADE_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GUILD_BUILDING_UPGRADE_RSP) ProtocolObjectPool.Get(SCPKG_GUILD_BUILDING_UPGRADE_RSP.CLASS_ID);
                        }
                        return;

                    case 0x2b:
                        if (!(this.dataObject is SCPKG_GUILD_BUILDING_LEVEL_CHANGE_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GUILD_BUILDING_LEVEL_CHANGE_NTF) ProtocolObjectPool.Get(SCPKG_GUILD_BUILDING_LEVEL_CHANGE_NTF.CLASS_ID);
                        }
                        return;

                    case 0x2c:
                        if (!(this.dataObject is SCPKG_GUILD_MONEY_CHANGE_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GUILD_MONEY_CHANGE_NTF) ProtocolObjectPool.Get(SCPKG_GUILD_MONEY_CHANGE_NTF.CLASS_ID);
                        }
                        return;

                    case 0x2d:
                        if (!(this.dataObject is SCPKG_GUILD_ACTIVE_CHANGE_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GUILD_ACTIVE_CHANGE_NTF) ProtocolObjectPool.Get(SCPKG_GUILD_ACTIVE_CHANGE_NTF.CLASS_ID);
                        }
                        return;

                    case 0x2e:
                        if (!(this.dataObject is SCPKG_DAILY_ACTIVE_CHANGE_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_DAILY_ACTIVE_CHANGE_NTF) ProtocolObjectPool.Get(SCPKG_DAILY_ACTIVE_CHANGE_NTF.CLASS_ID);
                        }
                        return;

                    case 0x2f:
                        if (!(this.dataObject is CSPKG_APPOINT_POSITION_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_APPOINT_POSITION_REQ) ProtocolObjectPool.Get(CSPKG_APPOINT_POSITION_REQ.CLASS_ID);
                        }
                        return;

                    case 0x30:
                        if (!(this.dataObject is SCPKG_APPOINT_POSITION_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_APPOINT_POSITION_RSP) ProtocolObjectPool.Get(SCPKG_APPOINT_POSITION_RSP.CLASS_ID);
                        }
                        return;

                    case 0x31:
                        if (!(this.dataObject is SCPKG_GUILD_POSITION_CHG_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GUILD_POSITION_CHG_NTF) ProtocolObjectPool.Get(SCPKG_GUILD_POSITION_CHG_NTF.CLASS_ID);
                        }
                        return;

                    case 50:
                        if (!(this.dataObject is CSPKG_FIRE_GUILD_MEMBER_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_FIRE_GUILD_MEMBER_REQ) ProtocolObjectPool.Get(CSPKG_FIRE_GUILD_MEMBER_REQ.CLASS_ID);
                        }
                        return;

                    case 0x33:
                        if (!(this.dataObject is SCPKG_FIRE_GUILD_MEMBER_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_FIRE_GUILD_MEMBER_RSP) ProtocolObjectPool.Get(SCPKG_FIRE_GUILD_MEMBER_RSP.CLASS_ID);
                        }
                        return;

                    case 0x34:
                        if (!(this.dataObject is SCPKG_FIRE_GUILD_MEMBER_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_FIRE_GUILD_MEMBER_NTF) ProtocolObjectPool.Get(SCPKG_FIRE_GUILD_MEMBER_NTF.CLASS_ID);
                        }
                        return;

                    case 0x35:
                        if (!(this.dataObject is CSPKG_GUILD_SELF_RECOMMEND_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GUILD_SELF_RECOMMEND_REQ) ProtocolObjectPool.Get(CSPKG_GUILD_SELF_RECOMMEND_REQ.CLASS_ID);
                        }
                        return;

                    case 0x36:
                        if (!(this.dataObject is SCPKG_GUILD_SELF_RECOMMEND_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GUILD_SELF_RECOMMEND_RSP) ProtocolObjectPool.Get(SCPKG_GUILD_SELF_RECOMMEND_RSP.CLASS_ID);
                        }
                        return;

                    case 0x37:
                        if (!(this.dataObject is SCPKG_GUILD_SELF_RECOMMEND_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GUILD_SELF_RECOMMEND_NTF) ProtocolObjectPool.Get(SCPKG_GUILD_SELF_RECOMMEND_NTF.CLASS_ID);
                        }
                        return;

                    case 0x38:
                        if (!(this.dataObject is CSPKG_DEAL_SELF_RECOMMEND_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_DEAL_SELF_RECOMMEND_REQ) ProtocolObjectPool.Get(CSPKG_DEAL_SELF_RECOMMEND_REQ.CLASS_ID);
                        }
                        return;

                    case 0x39:
                        if (!(this.dataObject is SCPKG_DEAL_SELF_RECOMMEND_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_DEAL_SELF_RECOMMEND_RSP) ProtocolObjectPool.Get(SCPKG_DEAL_SELF_RECOMMEND_RSP.CLASS_ID);
                        }
                        return;

                    case 0x3a:
                        if (!(this.dataObject is CSPKG_GUILD_DONATE_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GUILD_DONATE_REQ) ProtocolObjectPool.Get(CSPKG_GUILD_DONATE_REQ.CLASS_ID);
                        }
                        return;

                    case 0x3b:
                        if (!(this.dataObject is SCPKG_GUILD_DONATE_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GUILD_DONATE_RSP) ProtocolObjectPool.Get(SCPKG_GUILD_DONATE_RSP.CLASS_ID);
                        }
                        return;
                }
            }
            if (this.dataObject != null)
            {
                this.dataObject.Release();
                this.dataObject = null;
            }
        }

        private void select_2262_2801(long selector)
        {
            long num = selector;
            if ((num >= 0x8d6L) && (num <= 0x8ecL))
            {
                switch (((int) (num - 0x8d6L)))
                {
                    case 0:
                        if (!(this.dataObject is CSPKG_GET_GUILD_DIVIDEND_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GET_GUILD_DIVIDEND_REQ) ProtocolObjectPool.Get(CSPKG_GET_GUILD_DIVIDEND_REQ.CLASS_ID);
                        }
                        return;

                    case 1:
                        if (!(this.dataObject is SCPKG_GET_GUILD_DIVIDEND_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GET_GUILD_DIVIDEND_RSP) ProtocolObjectPool.Get(SCPKG_GET_GUILD_DIVIDEND_RSP.CLASS_ID);
                        }
                        return;

                    case 2:
                        if (!(this.dataObject is SCPKG_GUILD_CROSS_DAY_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GUILD_CROSS_DAY_NTF) ProtocolObjectPool.Get(SCPKG_GUILD_CROSS_DAY_NTF.CLASS_ID);
                        }
                        return;

                    case 3:
                        if (!(this.dataObject is SCPKG_GUILD_CROSS_WEEK_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GUILD_CROSS_WEEK_NTF) ProtocolObjectPool.Get(SCPKG_GUILD_CROSS_WEEK_NTF.CLASS_ID);
                        }
                        return;

                    case 4:
                        if (!(this.dataObject is SCPKG_MEMBER_TOP_KDA_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_MEMBER_TOP_KDA_NTF) ProtocolObjectPool.Get(SCPKG_MEMBER_TOP_KDA_NTF.CLASS_ID);
                        }
                        return;

                    case 5:
                        if (!(this.dataObject is SCPKG_MEMBER_RANK_POINT_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_MEMBER_RANK_POINT_NTF) ProtocolObjectPool.Get(SCPKG_MEMBER_RANK_POINT_NTF.CLASS_ID);
                        }
                        return;

                    case 6:
                        if (!(this.dataObject is SCPKG_GUILD_RANK_RESET_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GUILD_RANK_RESET_NTF) ProtocolObjectPool.Get(SCPKG_GUILD_RANK_RESET_NTF.CLASS_ID);
                        }
                        return;

                    case 7:
                        if (!(this.dataObject is CSPKG_GUILD_SYMBOL_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GUILD_SYMBOL_REQ) ProtocolObjectPool.Get(CSPKG_GUILD_SYMBOL_REQ.CLASS_ID);
                        }
                        return;

                    case 8:
                        if (!(this.dataObject is SCPKG_GUILD_SYMBOL_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GUILD_SYMBOL_RSP) ProtocolObjectPool.Get(SCPKG_GUILD_SYMBOL_RSP.CLASS_ID);
                        }
                        return;

                    case 9:
                        if (!(this.dataObject is SCPKG_GUILD_CONSTRUCT_CHG))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GUILD_CONSTRUCT_CHG) ProtocolObjectPool.Get(SCPKG_GUILD_CONSTRUCT_CHG.CLASS_ID);
                        }
                        return;

                    case 10:
                        if (!(this.dataObject is CSPKG_CHG_GUILD_HEADID_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CHG_GUILD_HEADID_REQ) ProtocolObjectPool.Get(CSPKG_CHG_GUILD_HEADID_REQ.CLASS_ID);
                        }
                        return;

                    case 11:
                        if (!(this.dataObject is SCPKG_CHG_GUILD_HEADID_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CHG_GUILD_HEADID_RSP) ProtocolObjectPool.Get(SCPKG_CHG_GUILD_HEADID_RSP.CLASS_ID);
                        }
                        return;

                    case 12:
                        if (!(this.dataObject is CSPKG_CHG_GUILD_NOTICE_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CHG_GUILD_NOTICE_REQ) ProtocolObjectPool.Get(CSPKG_CHG_GUILD_NOTICE_REQ.CLASS_ID);
                        }
                        return;

                    case 13:
                        if (!(this.dataObject is SCPKG_CHG_GUILD_NOTICE_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CHG_GUILD_NOTICE_RSP) ProtocolObjectPool.Get(SCPKG_CHG_GUILD_NOTICE_RSP.CLASS_ID);
                        }
                        return;

                    case 14:
                        if (!(this.dataObject is CSPKG_UPGRADE_GUILD_BY_COUPONS_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_UPGRADE_GUILD_BY_COUPONS_REQ) ProtocolObjectPool.Get(CSPKG_UPGRADE_GUILD_BY_COUPONS_REQ.CLASS_ID);
                        }
                        return;

                    case 15:
                        if (!(this.dataObject is SCPKG_UPGRADE_GUILD_BY_COUPONS_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_UPGRADE_GUILD_BY_COUPONS_RSP) ProtocolObjectPool.Get(SCPKG_UPGRADE_GUILD_BY_COUPONS_RSP.CLASS_ID);
                        }
                        return;

                    case 0x10:
                        if (!(this.dataObject is CSPKG_GUILD_SIGNIN_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GUILD_SIGNIN_REQ) ProtocolObjectPool.Get(CSPKG_GUILD_SIGNIN_REQ.CLASS_ID);
                        }
                        return;

                    case 0x11:
                        if (!(this.dataObject is SCPKG_GUILD_SIGNIN_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GUILD_SIGNIN_RSP) ProtocolObjectPool.Get(SCPKG_GUILD_SIGNIN_RSP.CLASS_ID);
                        }
                        return;

                    case 0x12:
                        if (!(this.dataObject is SCPKG_GUILD_SEASON_RESET_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GUILD_SEASON_RESET_NTF) ProtocolObjectPool.Get(SCPKG_GUILD_SEASON_RESET_NTF.CLASS_ID);
                        }
                        return;

                    case 0x13:
                        if (!(this.dataObject is CSPKG_GET_GROUP_GUILD_ID_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GET_GROUP_GUILD_ID_REQ) ProtocolObjectPool.Get(CSPKG_GET_GROUP_GUILD_ID_REQ.CLASS_ID);
                        }
                        return;

                    case 20:
                        if (!(this.dataObject is SCPKG_GET_GROUP_GUILD_ID_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GET_GROUP_GUILD_ID_NTF) ProtocolObjectPool.Get(SCPKG_GET_GROUP_GUILD_ID_NTF.CLASS_ID);
                        }
                        return;

                    case 0x15:
                        if (!(this.dataObject is CSPKG_SET_GUILD_GROUP_OPENID_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_SET_GUILD_GROUP_OPENID_REQ) ProtocolObjectPool.Get(CSPKG_SET_GUILD_GROUP_OPENID_REQ.CLASS_ID);
                        }
                        return;

                    case 0x16:
                        if (!(this.dataObject is SCPKG_SET_GUILD_GROUP_OPENID_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_SET_GUILD_GROUP_OPENID_NTF) ProtocolObjectPool.Get(SCPKG_SET_GUILD_GROUP_OPENID_NTF.CLASS_ID);
                        }
                        return;
                }
            }
            if ((num >= 0xa28L) && (num <= 0xa38L))
            {
                switch (((int) (num - 0xa28L)))
                {
                    case 0:
                        if (!(this.dataObject is SCPKG_CLASSOFRANKDETAIL_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CLASSOFRANKDETAIL_NTF) ProtocolObjectPool.Get(SCPKG_CLASSOFRANKDETAIL_NTF.CLASS_ID);
                        }
                        return;

                    case 1:
                        if (!(this.dataObject is SCPKG_UPDRANKINFO_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_UPDRANKINFO_NTF) ProtocolObjectPool.Get(SCPKG_UPDRANKINFO_NTF.CLASS_ID);
                        }
                        return;

                    case 2:
                        if (!(this.dataObject is CSPKG_GET_RANKING_LIST_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GET_RANKING_LIST_REQ) ProtocolObjectPool.Get(CSPKG_GET_RANKING_LIST_REQ.CLASS_ID);
                        }
                        return;

                    case 3:
                        if (!(this.dataObject is SCPKG_GET_RANKING_LIST_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GET_RANKING_LIST_RSP) ProtocolObjectPool.Get(SCPKG_GET_RANKING_LIST_RSP.CLASS_ID);
                        }
                        return;

                    case 4:
                        if (!(this.dataObject is CSPKG_GET_RANKING_ACNT_INFO_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GET_RANKING_ACNT_INFO_REQ) ProtocolObjectPool.Get(CSPKG_GET_RANKING_ACNT_INFO_REQ.CLASS_ID);
                        }
                        return;

                    case 5:
                        if (!(this.dataObject is SCPKG_GET_RANKING_ACNT_INFO_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GET_RANKING_ACNT_INFO_RSP) ProtocolObjectPool.Get(SCPKG_GET_RANKING_ACNT_INFO_RSP.CLASS_ID);
                        }
                        return;

                    case 6:
                        if (!(this.dataObject is CSPKG_GET_ACNT_DETAIL_INFO_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GET_ACNT_DETAIL_INFO_REQ) ProtocolObjectPool.Get(CSPKG_GET_ACNT_DETAIL_INFO_REQ.CLASS_ID);
                        }
                        return;

                    case 7:
                        if (!(this.dataObject is SCPKG_GET_ACNT_DETAIL_INFO_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GET_ACNT_DETAIL_INFO_RSP) ProtocolObjectPool.Get(SCPKG_GET_ACNT_DETAIL_INFO_RSP.CLASS_ID);
                        }
                        return;

                    case 8:
                        if (!(this.dataObject is CSPKG_SET_ACNT_NEWBIE_TYPE_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_SET_ACNT_NEWBIE_TYPE_REQ) ProtocolObjectPool.Get(CSPKG_SET_ACNT_NEWBIE_TYPE_REQ.CLASS_ID);
                        }
                        return;

                    case 9:
                        if (!(this.dataObject is SCPKG_SET_ACNT_NEWBIE_TYPE_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_SET_ACNT_NEWBIE_TYPE_RSP) ProtocolObjectPool.Get(SCPKG_SET_ACNT_NEWBIE_TYPE_RSP.CLASS_ID);
                        }
                        return;

                    case 10:
                        if (!(this.dataObject is CSPKG_GET_SELFRANKINFO))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GET_SELFRANKINFO) ProtocolObjectPool.Get(CSPKG_GET_SELFRANKINFO.CLASS_ID);
                        }
                        return;

                    case 11:
                        if (!(this.dataObject is SCPKG_ACNT_RANKINFO_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_ACNT_RANKINFO_RSP) ProtocolObjectPool.Get(SCPKG_ACNT_RANKINFO_RSP.CLASS_ID);
                        }
                        return;

                    case 12:
                        if (!(this.dataObject is SCPKG_MONTH_WEEK_CARD_USE_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_MONTH_WEEK_CARD_USE_RSP) ProtocolObjectPool.Get(SCPKG_MONTH_WEEK_CARD_USE_RSP.CLASS_ID);
                        }
                        return;

                    case 13:
                        if (!(this.dataObject is CSPKG_GET_RANKLIST_BY_SPECIAL_SCORE_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GET_RANKLIST_BY_SPECIAL_SCORE_REQ) ProtocolObjectPool.Get(CSPKG_GET_RANKLIST_BY_SPECIAL_SCORE_REQ.CLASS_ID);
                        }
                        return;

                    case 14:
                        if (!(this.dataObject is SCPKG_GET_RANKLIST_BY_SPECIAL_SCORE_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GET_RANKLIST_BY_SPECIAL_SCORE_RSP) ProtocolObjectPool.Get(SCPKG_GET_RANKLIST_BY_SPECIAL_SCORE_RSP.CLASS_ID);
                        }
                        return;

                    case 15:
                        if (!(this.dataObject is CSPKG_GET_SPECIAL_GUILD_RANK_INFO_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GET_SPECIAL_GUILD_RANK_INFO_REQ) ProtocolObjectPool.Get(CSPKG_GET_SPECIAL_GUILD_RANK_INFO_REQ.CLASS_ID);
                        }
                        return;

                    case 0x10:
                        if (!(this.dataObject is SCPKG_GET_SPECIAL_GUILD_RANK_INFO_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GET_SPECIAL_GUILD_RANK_INFO_RSP) ProtocolObjectPool.Get(SCPKG_GET_SPECIAL_GUILD_RANK_INFO_RSP.CLASS_ID);
                        }
                        return;
                }
            }
            if ((num >= 0x9c4L) && (num <= 0x9cfL))
            {
                switch (((int) (num - 0x9c4L)))
                {
                    case 0:
                        if (!(this.dataObject is CSPKG_ACNTACTIVITYINFO_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_ACNTACTIVITYINFO_REQ) ProtocolObjectPool.Get(CSPKG_ACNTACTIVITYINFO_REQ.CLASS_ID);
                        }
                        return;

                    case 1:
                        if (!(this.dataObject is SCPKG_ACNTACTIVITYINFO_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_ACNTACTIVITYINFO_RSP) ProtocolObjectPool.Get(SCPKG_ACNTACTIVITYINFO_RSP.CLASS_ID);
                        }
                        return;

                    case 2:
                        if (!(this.dataObject is SCPKG_ACTIVITYENDDEPLETION_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_ACTIVITYENDDEPLETION_NTF) ProtocolObjectPool.Get(SCPKG_ACTIVITYENDDEPLETION_NTF.CLASS_ID);
                        }
                        return;

                    case 3:
                        if (!(this.dataObject is CSPKG_DRAWWEAL_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_DRAWWEAL_REQ) ProtocolObjectPool.Get(CSPKG_DRAWWEAL_REQ.CLASS_ID);
                        }
                        return;

                    case 4:
                        if (!(this.dataObject is SCPKG_DRAWWEAL_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_DRAWWEAL_RSP) ProtocolObjectPool.Get(SCPKG_DRAWWEAL_RSP.CLASS_ID);
                        }
                        return;

                    case 5:
                        if (!(this.dataObject is SCPKG_WEALDETAIL_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_WEALDETAIL_NTF) ProtocolObjectPool.Get(SCPKG_WEALDETAIL_NTF.CLASS_ID);
                        }
                        return;

                    case 6:
                        if (!(this.dataObject is SCPKG_WEAL_CON_DATA_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_WEAL_CON_DATA_NTF) ProtocolObjectPool.Get(SCPKG_WEAL_CON_DATA_NTF.CLASS_ID);
                        }
                        return;

                    case 7:
                        if (!(this.dataObject is CSPKG_WEAL_DATA_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_WEAL_DATA_REQ) ProtocolObjectPool.Get(CSPKG_WEAL_DATA_REQ.CLASS_ID);
                        }
                        return;

                    case 8:
                        if (!(this.dataObject is SCPKG_WEAL_DATA_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_WEAL_DATA_NTF) ProtocolObjectPool.Get(SCPKG_WEAL_DATA_NTF.CLASS_ID);
                        }
                        return;

                    case 9:
                        if (!(this.dataObject is SCPKG_PROP_MULTIPLE_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_PROP_MULTIPLE_NTF) ProtocolObjectPool.Get(SCPKG_PROP_MULTIPLE_NTF.CLASS_ID);
                        }
                        return;

                    case 10:
                        if (!(this.dataObject is SCPKG_RES_DATA_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_RES_DATA_NTF) ProtocolObjectPool.Get(SCPKG_RES_DATA_NTF.CLASS_ID);
                        }
                        return;

                    case 11:
                        if (!(this.dataObject is SCPKG_WEAL_EXCHANGE_RES))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_WEAL_EXCHANGE_RES) ProtocolObjectPool.Get(SCPKG_WEAL_EXCHANGE_RES.CLASS_ID);
                        }
                        return;
                }
            }
            if ((num >= 0xa8cL) && (num <= 0xa91L))
            {
                switch (((int) (num - 0xa8cL)))
                {
                    case 0:
                        if (!(this.dataObject is CSPKG_GET_BURNING_PROGRESS_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GET_BURNING_PROGRESS_REQ) ProtocolObjectPool.Get(CSPKG_GET_BURNING_PROGRESS_REQ.CLASS_ID);
                        }
                        return;

                    case 1:
                        if (!(this.dataObject is SCPKG_GET_BURNING_PROGRESS_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GET_BURNING_PROGRESS_RSP) ProtocolObjectPool.Get(SCPKG_GET_BURNING_PROGRESS_RSP.CLASS_ID);
                        }
                        return;

                    case 2:
                        if (!(this.dataObject is CSPKG_GET_BURNING_REWARD_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GET_BURNING_REWARD_REQ) ProtocolObjectPool.Get(CSPKG_GET_BURNING_REWARD_REQ.CLASS_ID);
                        }
                        return;

                    case 3:
                        if (!(this.dataObject is SCPKG_GET_BURNING_REWARD_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GET_BURNING_REWARD_RSP) ProtocolObjectPool.Get(SCPKG_GET_BURNING_REWARD_RSP.CLASS_ID);
                        }
                        return;

                    case 4:
                        if (!(this.dataObject is CSPKG_RESET_BURNING_PROGRESS_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_RESET_BURNING_PROGRESS_REQ) ProtocolObjectPool.Get(CSPKG_RESET_BURNING_PROGRESS_REQ.CLASS_ID);
                        }
                        return;

                    case 5:
                        if (!(this.dataObject is SCPKG_RESET_BURNING_PROGRESS_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_RESET_BURNING_PROGRESS_RSP) ProtocolObjectPool.Get(SCPKG_RESET_BURNING_PROGRESS_RSP.CLASS_ID);
                        }
                        return;
                }
            }
            if (num == 0xaf0L)
            {
                if (!(this.dataObject is CSPKG_PVP_COMMINFO_REPORT))
                {
                    if (this.dataObject != null)
                    {
                        this.dataObject.Release();
                    }
                    this.dataObject = (CSPKG_PVP_COMMINFO_REPORT) ProtocolObjectPool.Get(CSPKG_PVP_COMMINFO_REPORT.CLASS_ID);
                }
            }
            else if (num == 0xaf1L)
            {
                if (!(this.dataObject is CSPKG_PVP_GAMEDATA_REPORT))
                {
                    if (this.dataObject != null)
                    {
                        this.dataObject.Release();
                    }
                    this.dataObject = (CSPKG_PVP_GAMEDATA_REPORT) ProtocolObjectPool.Get(CSPKG_PVP_GAMEDATA_REPORT.CLASS_ID);
                }
            }
            else if (this.dataObject != null)
            {
                this.dataObject.Release();
                this.dataObject = null;
            }
        }

        private void select_2802_4604(long selector)
        {
            long num = selector;
            if ((num >= 0xb54L) && (num <= 0xb66L))
            {
                switch (((int) (num - 0xb54L)))
                {
                    case 0:
                        if (!(this.dataObject is CSPKG_SETBATTLELISTOFARENA_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_SETBATTLELISTOFARENA_REQ) ProtocolObjectPool.Get(CSPKG_SETBATTLELISTOFARENA_REQ.CLASS_ID);
                        }
                        return;

                    case 1:
                        if (!(this.dataObject is SCPKG_SETBATTLELISTOFARENA_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_SETBATTLELISTOFARENA_RSP) ProtocolObjectPool.Get(SCPKG_SETBATTLELISTOFARENA_RSP.CLASS_ID);
                        }
                        return;

                    case 3:
                        if (!(this.dataObject is CSPKG_JOINARENA_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_JOINARENA_REQ) ProtocolObjectPool.Get(CSPKG_JOINARENA_REQ.CLASS_ID);
                        }
                        return;

                    case 4:
                        if (!(this.dataObject is SCPKG_JOINARENA_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_JOINARENA_RSP) ProtocolObjectPool.Get(SCPKG_JOINARENA_RSP.CLASS_ID);
                        }
                        return;

                    case 5:
                        if (!(this.dataObject is CSPKG_GETARENADATA_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GETARENADATA_REQ) ProtocolObjectPool.Get(CSPKG_GETARENADATA_REQ.CLASS_ID);
                        }
                        return;

                    case 6:
                        if (!(this.dataObject is SCPKG_GETARENADATA_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GETARENADATA_RSP) ProtocolObjectPool.Get(SCPKG_GETARENADATA_RSP.CLASS_ID);
                        }
                        return;

                    case 7:
                        if (!(this.dataObject is CSPKG_CHGARENAFIGHTERREQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CHGARENAFIGHTERREQ) ProtocolObjectPool.Get(CSPKG_CHGARENAFIGHTERREQ.CLASS_ID);
                        }
                        return;

                    case 8:
                        if (!(this.dataObject is SCPKG_CHGARENAFIGHTERRSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CHGARENAFIGHTERRSP) ProtocolObjectPool.Get(SCPKG_CHGARENAFIGHTERRSP.CLASS_ID);
                        }
                        return;

                    case 9:
                        if (!(this.dataObject is CSPKG_GETTOPFIGHTEROFARENA_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GETTOPFIGHTEROFARENA_REQ) ProtocolObjectPool.Get(CSPKG_GETTOPFIGHTEROFARENA_REQ.CLASS_ID);
                        }
                        return;

                    case 10:
                        if (!(this.dataObject is SCPKG_GETTOPFIGHTEROFARENA_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GETTOPFIGHTEROFARENA_RSP) ProtocolObjectPool.Get(SCPKG_GETTOPFIGHTEROFARENA_RSP.CLASS_ID);
                        }
                        return;

                    case 11:
                        if (!(this.dataObject is CSPKG_GETARENAFIGHTHISTORY_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GETARENAFIGHTHISTORY_REQ) ProtocolObjectPool.Get(CSPKG_GETARENAFIGHTHISTORY_REQ.CLASS_ID);
                        }
                        return;

                    case 12:
                        if (!(this.dataObject is SCPKG_GETARENAFIGHTHISTORY_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GETARENAFIGHTHISTORY_RSP) ProtocolObjectPool.Get(SCPKG_GETARENAFIGHTHISTORY_RSP.CLASS_ID);
                        }
                        return;

                    case 13:
                        if (!(this.dataObject is SCPKG_RANKCURSEASONHISTORY_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_RANKCURSEASONHISTORY_NTF) ProtocolObjectPool.Get(SCPKG_RANKCURSEASONHISTORY_NTF.CLASS_ID);
                        }
                        return;

                    case 14:
                        if (!(this.dataObject is SCPKG_RANKPASTSEASONHISTORY_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_RANKPASTSEASONHISTORY_NTF) ProtocolObjectPool.Get(SCPKG_RANKPASTSEASONHISTORY_NTF.CLASS_ID);
                        }
                        return;

                    case 15:
                        if (!(this.dataObject is CSPKG_GETRANKREWARD_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GETRANKREWARD_REQ) ProtocolObjectPool.Get(CSPKG_GETRANKREWARD_REQ.CLASS_ID);
                        }
                        return;

                    case 0x10:
                        if (!(this.dataObject is SCPKG_GETRANKREWARD_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GETRANKREWARD_RSP) ProtocolObjectPool.Get(SCPKG_GETRANKREWARD_RSP.CLASS_ID);
                        }
                        return;

                    case 0x11:
                        if (!(this.dataObject is SCPKG_NTF_ADDCURSEASONRECORD))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_NTF_ADDCURSEASONRECORD) ProtocolObjectPool.Get(SCPKG_NTF_ADDCURSEASONRECORD.CLASS_ID);
                        }
                        return;

                    case 0x12:
                        if (!(this.dataObject is SCPKG_NTF_ADDPASTSEASONRECORD))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_NTF_ADDPASTSEASONRECORD) ProtocolObjectPool.Get(SCPKG_NTF_ADDPASTSEASONRECORD.CLASS_ID);
                        }
                        return;
                }
            }
            if ((num >= 0x10cdL) && (num <= 0x10d6L))
            {
                switch (((int) (num - 0x10cdL)))
                {
                    case 0:
                        if (!(this.dataObject is CSPKG_DIRECT_BUY_ITEM_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_DIRECT_BUY_ITEM_REQ) ProtocolObjectPool.Get(CSPKG_DIRECT_BUY_ITEM_REQ.CLASS_ID);
                        }
                        return;

                    case 1:
                        if (!(this.dataObject is SCPKG_DIRECT_BUY_ITEM_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_DIRECT_BUY_ITEM_RSP) ProtocolObjectPool.Get(SCPKG_DIRECT_BUY_ITEM_RSP.CLASS_ID);
                        }
                        return;

                    case 2:
                        if (!(this.dataObject is CSPKG_PVE_REVIVE_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_PVE_REVIVE_REQ) ProtocolObjectPool.Get(CSPKG_PVE_REVIVE_REQ.CLASS_ID);
                        }
                        return;

                    case 3:
                        if (!(this.dataObject is SCPKG_PVE_REVIVE_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_PVE_REVIVE_RSP) ProtocolObjectPool.Get(SCPKG_PVE_REVIVE_RSP.CLASS_ID);
                        }
                        return;

                    case 4:
                        if (!(this.dataObject is CSPKG_USER_COMPLAINT_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_USER_COMPLAINT_REQ) ProtocolObjectPool.Get(CSPKG_USER_COMPLAINT_REQ.CLASS_ID);
                        }
                        return;

                    case 5:
                        if (!(this.dataObject is SCPKG_USER_COMPLAINT_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_USER_COMPLAINT_RSP) ProtocolObjectPool.Get(SCPKG_USER_COMPLAINT_RSP.CLASS_ID);
                        }
                        return;

                    case 6:
                        if (!(this.dataObject is CSPKG_SHARE_TLOG_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_SHARE_TLOG_REQ) ProtocolObjectPool.Get(CSPKG_SHARE_TLOG_REQ.CLASS_ID);
                        }
                        return;

                    case 7:
                        if (!(this.dataObject is SCPKG_SHARE_TLOG_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_SHARE_TLOG_RSP) ProtocolObjectPool.Get(SCPKG_SHARE_TLOG_RSP.CLASS_ID);
                        }
                        return;

                    case 8:
                        if (!(this.dataObject is CSPKG_DYE_INBATTLE_NEWBIEBIT_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_DYE_INBATTLE_NEWBIEBIT_REQ) ProtocolObjectPool.Get(CSPKG_DYE_INBATTLE_NEWBIEBIT_REQ.CLASS_ID);
                        }
                        return;

                    case 9:
                        if (!(this.dataObject is SCPKG_DYE_INBATTLE_NEWBIEBIT_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_DYE_INBATTLE_NEWBIEBIT_RSP) ProtocolObjectPool.Get(SCPKG_DYE_INBATTLE_NEWBIEBIT_RSP.CLASS_ID);
                        }
                        return;
                }
            }
            if ((num >= 0x1131L) && (num <= 0x1135L))
            {
                switch (((int) (num - 0x1131L)))
                {
                    case 0:
                        if (!(this.dataObject is SCPKG_ACHIEVEMENT_INFO_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_ACHIEVEMENT_INFO_NTF) ProtocolObjectPool.Get(SCPKG_ACHIEVEMENT_INFO_NTF.CLASS_ID);
                        }
                        return;

                    case 1:
                        if (!(this.dataObject is SCPKG_ACHIEVEMENT_STATE_CHG_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_ACHIEVEMENT_STATE_CHG_NTF) ProtocolObjectPool.Get(SCPKG_ACHIEVEMENT_STATE_CHG_NTF.CLASS_ID);
                        }
                        return;

                    case 2:
                        if (!(this.dataObject is SCPKG_ACHIEVEMENT_DONE_DATA_CHG_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_ACHIEVEMENT_DONE_DATA_CHG_NTF) ProtocolObjectPool.Get(SCPKG_ACHIEVEMENT_DONE_DATA_CHG_NTF.CLASS_ID);
                        }
                        return;

                    case 3:
                        if (!(this.dataObject is CSPKG_GET_ACHIEVEMENT_REWARD_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GET_ACHIEVEMENT_REWARD_REQ) ProtocolObjectPool.Get(CSPKG_GET_ACHIEVEMENT_REWARD_REQ.CLASS_ID);
                        }
                        return;

                    case 4:
                        if (!(this.dataObject is SCPKG_GET_ACHIEVEMENT_REWARD_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GET_ACHIEVEMENT_REWARD_RSP) ProtocolObjectPool.Get(SCPKG_GET_ACHIEVEMENT_REWARD_RSP.CLASS_ID);
                        }
                        return;
                }
            }
            if ((num >= 0x11f8L) && (num <= 0x11fcL))
            {
                switch (((int) (num - 0x11f8L)))
                {
                    case 0:
                        if (!(this.dataObject is SCPKG_GAME_VIP_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GAME_VIP_NTF) ProtocolObjectPool.Get(SCPKG_GAME_VIP_NTF.CLASS_ID);
                        }
                        return;

                    case 1:
                        if (!(this.dataObject is CSPKG_HEADIMG_CHG_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_HEADIMG_CHG_REQ) ProtocolObjectPool.Get(CSPKG_HEADIMG_CHG_REQ.CLASS_ID);
                        }
                        return;

                    case 2:
                        if (!(this.dataObject is SCPKG_HEADIMG_CHG_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_HEADIMG_CHG_RSP) ProtocolObjectPool.Get(SCPKG_HEADIMG_CHG_RSP.CLASS_ID);
                        }
                        return;

                    case 3:
                        if (!(this.dataObject is CSPKG_HEADIMG_FLAGCLR_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_HEADIMG_FLAGCLR_REQ) ProtocolObjectPool.Get(CSPKG_HEADIMG_FLAGCLR_REQ.CLASS_ID);
                        }
                        return;

                    case 4:
                        if (!(this.dataObject is SCPKG_HEADIMG_FLAGCLR_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_HEADIMG_FLAGCLR_RSP) ProtocolObjectPool.Get(SCPKG_HEADIMG_FLAGCLR_RSP.CLASS_ID);
                        }
                        return;
                }
            }
            if ((num >= 0xaf2L) && (num <= 0xaf5L))
            {
                switch (((int) (num - 0xaf2L)))
                {
                    case 0:
                        if (!(this.dataObject is CSPKG_PVP_GAMELOG_REPORT))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_PVP_GAMELOG_REPORT) ProtocolObjectPool.Get(CSPKG_PVP_GAMELOG_REPORT.CLASS_ID);
                        }
                        return;

                    case 1:
                        if (!(this.dataObject is SCPKG_PVP_NTF_CLIENT))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_PVP_NTF_CLIENT) ProtocolObjectPool.Get(SCPKG_PVP_NTF_CLIENT.CLASS_ID);
                        }
                        return;

                    case 2:
                        if (!(this.dataObject is CSPKG_PVP_GAMEDATA_REPORTOVER))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_PVP_GAMEDATA_REPORTOVER) ProtocolObjectPool.Get(CSPKG_PVP_GAMEDATA_REPORTOVER.CLASS_ID);
                        }
                        return;

                    case 3:
                        if (!(this.dataObject is CSPKG_PVP_GAMELOG_REPORTOVER))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_PVP_GAMELOG_REPORTOVER) ProtocolObjectPool.Get(CSPKG_PVP_GAMELOG_REPORTOVER.CLASS_ID);
                        }
                        return;
                }
            }
            if ((num >= 0xfa0L) && (num <= 0xfa3L))
            {
                switch (((int) (num - 0xfa0L)))
                {
                    case 0:
                        if (!(this.dataObject is CSPKG_UPDATECLIENTBITS_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_UPDATECLIENTBITS_NTF) ProtocolObjectPool.Get(CSPKG_UPDATECLIENTBITS_NTF.CLASS_ID);
                        }
                        return;

                    case 1:
                        if (!(this.dataObject is CSPKG_SERVERTIME_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_SERVERTIME_REQ) ProtocolObjectPool.Get(CSPKG_SERVERTIME_REQ.CLASS_ID);
                        }
                        return;

                    case 2:
                        if (!(this.dataObject is SCPKG_SERVERTIME_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_SERVERTIME_RSP) ProtocolObjectPool.Get(SCPKG_SERVERTIME_RSP.CLASS_ID);
                        }
                        return;

                    case 3:
                        if (!(this.dataObject is CSPKG_UPDNEWCLIENTBITS_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_UPDNEWCLIENTBITS_NTF) ProtocolObjectPool.Get(CSPKG_UPDNEWCLIENTBITS_NTF.CLASS_ID);
                        }
                        return;
                }
            }
            if ((num >= 0x1004L) && (num <= 0x1007L))
            {
                switch (((int) (num - 0x1004L)))
                {
                    case 0:
                        if (!(this.dataObject is SCPKG_CMD_NTF_FRIEND_GAME_STATE))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_NTF_FRIEND_GAME_STATE) ProtocolObjectPool.Get(SCPKG_CMD_NTF_FRIEND_GAME_STATE.CLASS_ID);
                        }
                        return;

                    case 1:
                        if (!(this.dataObject is SCPKG_NTF_SNS_FRIEND))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_NTF_SNS_FRIEND) ProtocolObjectPool.Get(SCPKG_NTF_SNS_FRIEND.CLASS_ID);
                        }
                        return;

                    case 2:
                        if (!(this.dataObject is SCPKG_CHG_SNS_FRIEND_PROFILE))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CHG_SNS_FRIEND_PROFILE) ProtocolObjectPool.Get(SCPKG_CHG_SNS_FRIEND_PROFILE.CLASS_ID);
                        }
                        return;

                    case 3:
                        if (!(this.dataObject is SCPKG_CMD_NTF_ACNT_SNSNAME))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CMD_NTF_ACNT_SNSNAME) ProtocolObjectPool.Get(SCPKG_CMD_NTF_ACNT_SNSNAME.CLASS_ID);
                        }
                        return;
                }
            }
            if ((num >= 0xc80L) && (num <= 0xc82L))
            {
                switch (((int) (num - 0xc80L)))
                {
                    case 0:
                        if (!(this.dataObject is CSPKG_CHANGE_NAME_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CHANGE_NAME_REQ) ProtocolObjectPool.Get(CSPKG_CHANGE_NAME_REQ.CLASS_ID);
                        }
                        return;

                    case 1:
                        if (!(this.dataObject is SCPKG_CHANGE_NAME_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_CHANGE_NAME_RSP) ProtocolObjectPool.Get(SCPKG_CHANGE_NAME_RSP.CLASS_ID);
                        }
                        return;

                    case 2:
                        if (!(this.dataObject is SCPKG_GUILD_NAME_CHG_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GUILD_NAME_CHG_NTF) ProtocolObjectPool.Get(SCPKG_GUILD_NAME_CHG_NTF.CLASS_ID);
                        }
                        return;
                }
            }
            if (num == 0xbb8L)
            {
                if (!(this.dataObject is CSPKG_ANTIDATA_REQ))
                {
                    if (this.dataObject != null)
                    {
                        this.dataObject.Release();
                    }
                    this.dataObject = (CSPKG_ANTIDATA_REQ) ProtocolObjectPool.Get(CSPKG_ANTIDATA_REQ.CLASS_ID);
                }
            }
            else if (num == 0xbb9L)
            {
                if (!(this.dataObject is SCPKG_ANTIDATA_SYN))
                {
                    if (this.dataObject != null)
                    {
                        this.dataObject.Release();
                    }
                    this.dataObject = (SCPKG_ANTIDATA_SYN) ProtocolObjectPool.Get(SCPKG_ANTIDATA_SYN.CLASS_ID);
                }
            }
            else if (num == 0x1068L)
            {
                if (!(this.dataObject is CSPKG_QQVIPINFO_REQ))
                {
                    if (this.dataObject != null)
                    {
                        this.dataObject.Release();
                    }
                    this.dataObject = (CSPKG_QQVIPINFO_REQ) ProtocolObjectPool.Get(CSPKG_QQVIPINFO_REQ.CLASS_ID);
                }
            }
            else if (num == 0x1069L)
            {
                if (!(this.dataObject is SCPKG_QQVIPINFO_RSP))
                {
                    if (this.dataObject != null)
                    {
                        this.dataObject.Release();
                    }
                    this.dataObject = (SCPKG_QQVIPINFO_RSP) ProtocolObjectPool.Get(SCPKG_QQVIPINFO_RSP.CLASS_ID);
                }
            }
            else if (num == 0xc1cL)
            {
                if (!(this.dataObject is SCPKG_CREATE_TVOIP_ROOM_NTF))
                {
                    if (this.dataObject != null)
                    {
                        this.dataObject.Release();
                    }
                    this.dataObject = (SCPKG_CREATE_TVOIP_ROOM_NTF) ProtocolObjectPool.Get(SCPKG_CREATE_TVOIP_ROOM_NTF.CLASS_ID);
                }
            }
            else if (num == 0x100eL)
            {
                if (!(this.dataObject is SCPKG_FUNCTION_SWITCH_NTF))
                {
                    if (this.dataObject != null)
                    {
                        this.dataObject.Release();
                    }
                    this.dataObject = (SCPKG_FUNCTION_SWITCH_NTF) ProtocolObjectPool.Get(SCPKG_FUNCTION_SWITCH_NTF.CLASS_ID);
                }
            }
            else if (num == 0x1194L)
            {
                if (!(this.dataObject is SCPKG_DAILY_CHECK_DATA_NTF))
                {
                    if (this.dataObject != null)
                    {
                        this.dataObject.Release();
                    }
                    this.dataObject = (SCPKG_DAILY_CHECK_DATA_NTF) ProtocolObjectPool.Get(SCPKG_DAILY_CHECK_DATA_NTF.CLASS_ID);
                }
            }
            else if (this.dataObject != null)
            {
                this.dataObject.Release();
                this.dataObject = null;
            }
        }

        private void select_4605_5207(long selector)
        {
            long num = selector;
            if ((num >= 0x13edL) && (num <= 0x13f3L))
            {
                switch (((int) (num - 0x13edL)))
                {
                    case 0:
                        if (!(this.dataObject is CSPKG_GETAWARDPOOL_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GETAWARDPOOL_REQ) ProtocolObjectPool.Get(CSPKG_GETAWARDPOOL_REQ.CLASS_ID);
                        }
                        return;

                    case 1:
                        if (!(this.dataObject is SCPKG_GETAWARDPOOL_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GETAWARDPOOL_RSP) ProtocolObjectPool.Get(SCPKG_GETAWARDPOOL_RSP.CLASS_ID);
                        }
                        return;

                    case 2:
                        if (!(this.dataObject is SCPKG_MATCHPOINT_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_MATCHPOINT_NTF) ProtocolObjectPool.Get(SCPKG_MATCHPOINT_NTF.CLASS_ID);
                        }
                        return;

                    case 3:
                        if (!(this.dataObject is CSPKG_BUY_MATCHTICKET_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_BUY_MATCHTICKET_REQ) ProtocolObjectPool.Get(CSPKG_BUY_MATCHTICKET_REQ.CLASS_ID);
                        }
                        return;

                    case 4:
                        if (!(this.dataObject is SCPKG_BUY_MATCHTICKET_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_BUY_MATCHTICKET_RSP) ProtocolObjectPool.Get(SCPKG_BUY_MATCHTICKET_RSP.CLASS_ID);
                        }
                        return;

                    case 5:
                        if (!(this.dataObject is CSPKG_GET_MATCHINFO_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GET_MATCHINFO_REQ) ProtocolObjectPool.Get(CSPKG_GET_MATCHINFO_REQ.CLASS_ID);
                        }
                        return;

                    case 6:
                        if (!(this.dataObject is SCPKG_GET_MATCHINFO_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_GET_MATCHINFO_RSP) ProtocolObjectPool.Get(SCPKG_GET_MATCHINFO_RSP.CLASS_ID);
                        }
                        return;
                }
            }
            if ((num >= 0x1451L) && (num <= 0x1457L))
            {
                switch (((int) (num - 0x1451L)))
                {
                    case 0:
                        if (!(this.dataObject is CSPKG_SELFDEFINE_HEROEQUIP_CHG_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_SELFDEFINE_HEROEQUIP_CHG_REQ) ProtocolObjectPool.Get(CSPKG_SELFDEFINE_HEROEQUIP_CHG_REQ.CLASS_ID);
                        }
                        return;

                    case 1:
                        if (!(this.dataObject is SCPKG_SELFDEFINE_HEROEQUIP_CHG_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_SELFDEFINE_HEROEQUIP_CHG_RSP) ProtocolObjectPool.Get(SCPKG_SELFDEFINE_HEROEQUIP_CHG_RSP.CLASS_ID);
                        }
                        return;

                    case 2:
                        if (!(this.dataObject is CSPKG_RECOVER_SYSTEMEQUIP_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_RECOVER_SYSTEMEQUIP_REQ) ProtocolObjectPool.Get(CSPKG_RECOVER_SYSTEMEQUIP_REQ.CLASS_ID);
                        }
                        return;

                    case 3:
                        if (!(this.dataObject is SCPKG_RECOVER_SYSTEMEQUIP_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_RECOVER_SYSTEMEQUIP_RSP) ProtocolObjectPool.Get(SCPKG_RECOVER_SYSTEMEQUIP_RSP.CLASS_ID);
                        }
                        return;

                    case 4:
                        if (!(this.dataObject is SCPKG_MATCHTEAM_DESTROY_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_MATCHTEAM_DESTROY_NTF) ProtocolObjectPool.Get(SCPKG_MATCHTEAM_DESTROY_NTF.CLASS_ID);
                        }
                        return;

                    case 5:
                        if (!(this.dataObject is CSPKG_GET_ACNT_CREDIT_VALUE))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_GET_ACNT_CREDIT_VALUE) ProtocolObjectPool.Get(CSPKG_GET_ACNT_CREDIT_VALUE.CLASS_ID);
                        }
                        return;

                    case 6:
                        if (!(this.dataObject is SCPKG_NTF_ACNT_CREDIT_VALUE))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_NTF_ACNT_CREDIT_VALUE) ProtocolObjectPool.Get(SCPKG_NTF_ACNT_CREDIT_VALUE.CLASS_ID);
                        }
                        return;
                }
            }
            if ((num >= 0x12c0L) && (num <= 0x12c4L))
            {
                switch (((int) (num - 0x12c0L)))
                {
                    case 0:
                        if (!(this.dataObject is SCPKG_LUCKYDRAW_DATA_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_LUCKYDRAW_DATA_NTF) ProtocolObjectPool.Get(SCPKG_LUCKYDRAW_DATA_NTF.CLASS_ID);
                        }
                        return;

                    case 1:
                        if (!(this.dataObject is CSPKG_LUCKYDRAW_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_LUCKYDRAW_REQ) ProtocolObjectPool.Get(CSPKG_LUCKYDRAW_REQ.CLASS_ID);
                        }
                        return;

                    case 2:
                        if (!(this.dataObject is SCPKG_LUCKYDRAW_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_LUCKYDRAW_RSP) ProtocolObjectPool.Get(SCPKG_LUCKYDRAW_RSP.CLASS_ID);
                        }
                        return;

                    case 3:
                        if (!(this.dataObject is CSPKG_LUCKYDRAW_EXTERN_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_LUCKYDRAW_EXTERN_REQ) ProtocolObjectPool.Get(CSPKG_LUCKYDRAW_EXTERN_REQ.CLASS_ID);
                        }
                        return;

                    case 4:
                        if (!(this.dataObject is SCPKG_LUCKYDRAW_EXTERN_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_LUCKYDRAW_EXTERN_RSP) ProtocolObjectPool.Get(SCPKG_LUCKYDRAW_EXTERN_RSP.CLASS_ID);
                        }
                        return;
                }
            }
            if ((num >= 0x11fdL) && (num <= 0x1200L))
            {
                switch (((int) (num - 0x11fdL)))
                {
                    case 0:
                        if (!(this.dataObject is SCPKG_NTF_HEADIMG_CHG))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_NTF_HEADIMG_CHG) ProtocolObjectPool.Get(SCPKG_NTF_HEADIMG_CHG.CLASS_ID);
                        }
                        return;

                    case 1:
                        if (!(this.dataObject is SCPKG_HEADIMG_LIST_SYNC))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_HEADIMG_LIST_SYNC) ProtocolObjectPool.Get(SCPKG_HEADIMG_LIST_SYNC.CLASS_ID);
                        }
                        return;

                    case 2:
                        if (!(this.dataObject is SCPKG_NTF_HEADIMG_ADD))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_NTF_HEADIMG_ADD) ProtocolObjectPool.Get(SCPKG_NTF_HEADIMG_ADD.CLASS_ID);
                        }
                        return;

                    case 3:
                        if (!(this.dataObject is SCPKG_NTF_HEADIMG_DEL))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_NTF_HEADIMG_DEL) ProtocolObjectPool.Get(SCPKG_NTF_HEADIMG_DEL.CLASS_ID);
                        }
                        return;
                }
            }
            if ((num >= 0x1388L) && (num <= 0x138bL))
            {
                switch (((int) (num - 0x1388L)))
                {
                    case 0:
                        if (!(this.dataObject is CSPKG_CLT_PERFORMANCE))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CLT_PERFORMANCE) ProtocolObjectPool.Get(CSPKG_CLT_PERFORMANCE.CLASS_ID);
                        }
                        return;

                    case 2:
                        if (!(this.dataObject is CSPKG_CLT_ACTION_STATISTICS))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_CLT_ACTION_STATISTICS) ProtocolObjectPool.Get(CSPKG_CLT_ACTION_STATISTICS.CLASS_ID);
                        }
                        return;

                    case 3:
                        if (!(this.dataObject is SCPKG_ENTERTAINMENT_SYN_RAND_HERO_CNT))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_ENTERTAINMENT_SYN_RAND_HERO_CNT) ProtocolObjectPool.Get(SCPKG_ENTERTAINMENT_SYN_RAND_HERO_CNT.CLASS_ID);
                        }
                        return;
                }
            }
            if ((num >= 0x1324L) && (num <= 0x1326L))
            {
                switch (((int) (num - 0x1324L)))
                {
                    case 0:
                        if (!(this.dataObject is CSPKG_SURRENDER_REQ))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (CSPKG_SURRENDER_REQ) ProtocolObjectPool.Get(CSPKG_SURRENDER_REQ.CLASS_ID);
                        }
                        return;

                    case 1:
                        if (!(this.dataObject is SCPKG_SURRENDER_RSP))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_SURRENDER_RSP) ProtocolObjectPool.Get(SCPKG_SURRENDER_RSP.CLASS_ID);
                        }
                        return;

                    case 2:
                        if (!(this.dataObject is SCPKG_SURRENDER_NTF))
                        {
                            if (this.dataObject != null)
                            {
                                this.dataObject.Release();
                            }
                            this.dataObject = (SCPKG_SURRENDER_NTF) ProtocolObjectPool.Get(SCPKG_SURRENDER_NTF.CLASS_ID);
                        }
                        return;
                }
            }
            if (this.dataObject != null)
            {
                this.dataObject.Release();
                this.dataObject = null;
            }
        }

        public TdrError.ErrorType unpack(long selector, ref TdrReadBuf srcBuf, uint cutVer)
        {
            if ((cutVer == 0) || (CURRVERSION < cutVer))
            {
                cutVer = CURRVERSION;
            }
            if (BASEVERSION > cutVer)
            {
                return TdrError.ErrorType.TDR_ERR_CUTVER_TOO_SMALL;
            }
            TdrError.ErrorType type = TdrError.ErrorType.TDR_NO_ERROR;
            ProtocolObject obj2 = this.select(selector);
            if (obj2 != null)
            {
                return obj2.unpack(ref srcBuf, cutVer);
            }
            for (int i = 0; i < 0x3e800; i++)
            {
                type = srcBuf.readUInt8(ref this.szData[i]);
                if (type != TdrError.ErrorType.TDR_NO_ERROR)
                {
                    return type;
                }
            }
            return type;
        }

        public TdrError.ErrorType unpack(long selector, ref byte[] buffer, int size, ref int usedSize, uint cutVer)
        {
            if ((buffer.GetLength(0) == 0) || (size > buffer.GetLength(0)))
            {
                return TdrError.ErrorType.TDR_ERR_INVALID_BUFFER_PARAMETER;
            }
            TdrReadBuf srcBuf = ClassObjPool<TdrReadBuf>.Get();
            srcBuf.set(ref buffer, size);
            TdrError.ErrorType type = this.unpack(selector, ref srcBuf, cutVer);
            usedSize = srcBuf.getUsedSize();
            srcBuf.Release();
            return type;
        }

        public CSPKG_ACHIEVEHERO_REQ stAchieveHeroReq
        {
            get
            {
                return (this.dataObject as CSPKG_ACHIEVEHERO_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_ACHIEVEHERO_RSP stAchieveHeroRsp
        {
            get
            {
                return (this.dataObject as SCPKG_ACHIEVEHERO_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_ACHIEVEMENT_DONE_DATA_CHG_NTF stAchievementDoneDataChgNtf
        {
            get
            {
                return (this.dataObject as SCPKG_ACHIEVEMENT_DONE_DATA_CHG_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_ACHIEVEMENT_STATE_CHG_NTF stAchievementStateChgNtf
        {
            get
            {
                return (this.dataObject as SCPKG_ACHIEVEMENT_STATE_CHG_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_ACNTACTIVITYINFO_REQ stAcntActivityInfoReq
        {
            get
            {
                return (this.dataObject as CSPKG_ACNTACTIVITYINFO_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_ACNTACTIVITYINFO_RSP stAcntActivityInfoRsp
        {
            get
            {
                return (this.dataObject as SCPKG_ACNTACTIVITYINFO_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_ACNTCOUPONS stAcntCouponsReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_ACNTCOUPONS);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_ACNTCOUPONS stAcntCouponsRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_ACNTCOUPONS);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_ACNTDETAILINFO_RSP stAcntDetailInfoRsp
        {
            get
            {
                return (this.dataObject as SCPKG_ACNTDETAILINFO_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_ACNT_HEAD_URL_CHG_NTF stAcntHeadUrlChgNtf
        {
            get
            {
                return (this.dataObject as SCPKG_ACNT_HEAD_URL_CHG_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_ACNTHEROINFO_NTY stAcntHeroInfoNty
        {
            get
            {
                return (this.dataObject as SCPKG_ACNTHEROINFO_NTY);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_ACNT_LEAVE_TEAM_RSP stAcntLeaveRsp
        {
            get
            {
                return (this.dataObject as SCPKG_ACNT_LEAVE_TEAM_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_ACNT_REGISTER_REQ stAcntRegisterReq
        {
            get
            {
                return (this.dataObject as CSPKG_ACNT_REGISTER_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_ACNT_REGISTER_RES stAcntRegisterRes
        {
            get
            {
                return (this.dataObject as CSPKG_ACNT_REGISTER_RES);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_ACNTSELFMSGINFO stAcntSelfMsgInfo
        {
            get
            {
                return (this.dataObject as SCPKG_ACNTSELFMSGINFO);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_ACTIVITYENDDEPLETION_NTF stActivityEndDepletionNtf
        {
            get
            {
                return (this.dataObject as SCPKG_ACTIVITYENDDEPLETION_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_ADD_GUILD_NTF stAddGuildNtf
        {
            get
            {
                return (this.dataObject as SCPKG_ADD_GUILD_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_ADDHERO_NTY stAddHeroNty
        {
            get
            {
                return (this.dataObject as SCPKG_ADDHERO_NTY);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_ADD_NPC_REQ stAddNpcReq
        {
            get
            {
                return (this.dataObject as CSPKG_ADD_NPC_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_AKALISHOPBUY_REQ stAkaliShopBuyReq
        {
            get
            {
                return (this.dataObject as CSPKG_AKALISHOPBUY_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_AKALISHOP_ERROR stAkaliShopError
        {
            get
            {
                return (this.dataObject as SCPKG_AKALISHOP_ERROR);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_AKALISHOP_INFO stAkaliShopInfo
        {
            get
            {
                return (this.dataObject as SCPKG_AKALISHOP_INFO);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_AKALISHOP_UPDATE stAkaliShopUpdate
        {
            get
            {
                return (this.dataObject as SCPKG_AKALISHOP_UPDATE);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_AKALISHOP_ZHEKOUREQ stAkaliShopZheKouReq
        {
            get
            {
                return (this.dataObject as CSPKG_AKALISHOP_ZHEKOUREQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_AKALISHOP_ZHEKOURSP stAkaliShopZheKouRsp
        {
            get
            {
                return (this.dataObject as SCPKG_AKALISHOP_ZHEKOURSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_ANTIDATA_REQ stAntiDataReq
        {
            get
            {
                return (this.dataObject as CSPKG_ANTIDATA_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_ANTIDATA_SYN stAntiDataSyn
        {
            get
            {
                return (this.dataObject as SCPKG_ANTIDATA_SYN);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_APPLY_JOIN_GUILD_REQ stApplyJoinGuildReq
        {
            get
            {
                return (this.dataObject as CSPKG_APPLY_JOIN_GUILD_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_APPLY_JOIN_GUILD_RSP stApplyJoinGuildRsp
        {
            get
            {
                return (this.dataObject as SCPKG_APPLY_JOIN_GUILD_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_APPOINT_POSITION_REQ stAppointPositionReq
        {
            get
            {
                return (this.dataObject as CSPKG_APPOINT_POSITION_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_APPOINT_POSITION_RSP stAppointPositionRsp
        {
            get
            {
                return (this.dataObject as SCPKG_APPOINT_POSITION_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_APPROVE_JOIN_GUILD_APPLY stApproveJoinGuildApply
        {
            get
            {
                return (this.dataObject as CSPKG_APPROVE_JOIN_GUILD_APPLY);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_ASK_ACNT_TRANS_VISITORSVRDATA stAskAcntTransVisitorSvrData
        {
            get
            {
                return (this.dataObject as SCPKG_ASK_ACNT_TRANS_VISITORSVRDATA);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_ASKINMULTGAME_REQ stAskInMultGameReq
        {
            get
            {
                return (this.dataObject as CSPKG_ASKINMULTGAME_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_ASKINMULTGAME_RSP stAskInMultGameRsp
        {
            get
            {
                return (this.dataObject as SCPKG_ASKINMULTGAME_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_AUTOREFRESH stAutoRefresh
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_AUTOREFRESH);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_BANTIME_CHG stBanTimeChg
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_BANTIME_CHG);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_BATTLELIST_NTY stBattleListNtf
        {
            get
            {
                return (this.dataObject as SCPKG_BATTLELIST_NTY);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_BATTLELIST_REQ stBattleListReq
        {
            get
            {
                return (this.dataObject as CSPKG_BATTLELIST_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_BATTLELIST_RSP stBattleListRsp
        {
            get
            {
                return (this.dataObject as SCPKG_BATTLELIST_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_BUYHERO_REQ stBuyHeroReq
        {
            get
            {
                return (this.dataObject as CSPKG_BUYHERO_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_BUYHERO_RSP stBuyHeroRsp
        {
            get
            {
                return (this.dataObject as SCPKG_BUYHERO_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_BUYHEROSKIN_REQ stBuyHeroSkinReq
        {
            get
            {
                return (this.dataObject as CSPKG_BUYHEROSKIN_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_BUYHEROSKIN_RSP stBuyHeroSkinRsp
        {
            get
            {
                return (this.dataObject as SCPKG_BUYHEROSKIN_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_BUY_MATCHTICKET_REQ stBuyMatchTicketReq
        {
            get
            {
                return (this.dataObject as CSPKG_BUY_MATCHTICKET_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_BUY_MATCHTICKET_RSP stBuyMatchTicketRsp
        {
            get
            {
                return (this.dataObject as SCPKG_BUY_MATCHTICKET_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CHANGE_NAME_REQ stChangeNameReq
        {
            get
            {
                return (this.dataObject as CSPKG_CHANGE_NAME_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CHANGE_NAME_RSP stChangeNameRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CHANGE_NAME_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_CHAT_NTF stChatNtf
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_CHAT_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_CHAT_REQ stChatReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_CHAT_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_CHEATCMD stCheatCmd
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_CHEATCMD);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CHGARENAFIGHTERREQ stChgArenaFighterReq
        {
            get
            {
                return (this.dataObject as CSPKG_CHGARENAFIGHTERREQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CHGARENAFIGHTERRSP stChgArenaFighterRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CHGARENAFIGHTERRSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CHG_GUILD_HEADID_REQ stChgGuildHeadIDReq
        {
            get
            {
                return (this.dataObject as CSPKG_CHG_GUILD_HEADID_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CHG_GUILD_HEADID_RSP stChgGuildHeadIDRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CHG_GUILD_HEADID_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CHG_GUILD_NOTICE_REQ stChgGuildNoticeReq
        {
            get
            {
                return (this.dataObject as CSPKG_CHG_GUILD_NOTICE_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CHG_GUILD_NOTICE_RSP stChgGuildNoticeRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CHG_GUILD_NOTICE_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_ROOM_CHGMEMBERPOS_REQ stChgMemberPosReq
        {
            get
            {
                return (this.dataObject as CSPKG_ROOM_CHGMEMBERPOS_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CLASSOFRANKDETAIL_NTF stClassOfRankDetailNtf
        {
            get
            {
                return (this.dataObject as SCPKG_CLASSOFRANKDETAIL_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_TASKDONE_CLIENTREPORT stClientReportTaskDone
        {
            get
            {
                return (this.dataObject as CSPKG_TASKDONE_CLIENTREPORT);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CLRCDLIMIT_REQ stClrCdLimitReq
        {
            get
            {
                return (this.dataObject as CSPKG_CLRCDLIMIT_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CLRCDLIMIT_RSP stClrCdLimitRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CLRCDLIMIT_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_NTF_CLRSHOPBUYLIMIT stClrShopBuyLimit
        {
            get
            {
                return (this.dataObject as SCPKG_NTF_CLRSHOPBUYLIMIT);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_CLRSHOPREFRESH stClrShopRefresh
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_CLRSHOPREFRESH);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CLT_ACTION_STATISTICS stCltActionStatistics
        {
            get
            {
                return (this.dataObject as CSPKG_CLT_ACTION_STATISTICS);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CLT_PERFORMANCE stCltPerformance
        {
            get
            {
                return (this.dataObject as CSPKG_CLT_PERFORMANCE);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_COINBUY stCoinBuyReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_COINBUY);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_COINBUY stCoinBuyRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_COINBUY);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_COINDRAW_RESULT stCoinDrawResult
        {
            get
            {
                return (this.dataObject as SCPKG_COINDRAW_RESULT);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_COINGETPATH_REQ stCoinGetPathReq
        {
            get
            {
                return (this.dataObject as CSPKG_COINGETPATH_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_COINGETPATH_RSP stCoinGetPathRsp
        {
            get
            {
                return (this.dataObject as SCPKG_COINGETPATH_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CONFIRM_HERO stConfirmHero
        {
            get
            {
                return (this.dataObject as CSPKG_CONFIRM_HERO);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CONFIRM_HERO_NTF stConfirmHeroNtf
        {
            get
            {
                return (this.dataObject as SCPKG_CONFIRM_HERO_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_COUPONS_REWARDGET stCouponsRewardReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_COUPONS_REWARDGET);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_COUPONS_REWARDINFO stCouponsRewardRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_COUPONS_REWARDINFO);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CREATE_GUILD_REQ stCreateGuildReq
        {
            get
            {
                return (this.dataObject as CSPKG_CREATE_GUILD_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CREATE_GUILD_RSP stCreateGuildRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CREATE_GUILD_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CREATEULTIGAMEREQ stCreateMultGameReq
        {
            get
            {
                return (this.dataObject as CSPKG_CREATEULTIGAMEREQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CREATE_TEAM_REQ stCreateTeamReq
        {
            get
            {
                return (this.dataObject as CSPKG_CREATE_TEAM_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CREATE_TVOIP_ROOM_NTF stCreateTvoipRoomNtf
        {
            get
            {
                return (this.dataObject as SCPKG_CREATE_TVOIP_ROOM_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_DAILY_ACTIVE_CHANGE_NTF stDailyActiveChgNtf
        {
            get
            {
                return (this.dataObject as SCPKG_DAILY_ACTIVE_CHANGE_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_DAILY_CHECK_DATA_NTF stDailyCheckDataNtf
        {
            get
            {
                return (this.dataObject as SCPKG_DAILY_CHECK_DATA_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_DEAL_GUILD_INVITE stDealGuildInvite
        {
            get
            {
                return (this.dataObject as CSPKG_DEAL_GUILD_INVITE);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_DEAL_GUILD_INVITE_RSP stDealGuildInviteRsp
        {
            get
            {
                return (this.dataObject as SCPKG_DEAL_GUILD_INVITE_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_DEAL_SELF_RECOMMEND_REQ stDealSelfRecommemdReq
        {
            get
            {
                return (this.dataObject as CSPKG_DEAL_SELF_RECOMMEND_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_DEAL_SELF_RECOMMEND_RSP stDealSelfRecommemdRsp
        {
            get
            {
                return (this.dataObject as SCPKG_DEAL_SELF_RECOMMEND_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_DEFAULT_HERO_NTF stDefaultHeroNtf
        {
            get
            {
                return (this.dataObject as SCPKG_DEFAULT_HERO_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_DIRECT_BUY_ITEM_REQ stDirectBuyItemReq
        {
            get
            {
                return (this.dataObject as CSPKG_DIRECT_BUY_ITEM_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_DIRECT_BUY_ITEM_RSP stDirectBuyItemRsp
        {
            get
            {
                return (this.dataObject as SCPKG_DIRECT_BUY_ITEM_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_DRAWWEAL_REQ stDrawWealReq
        {
            get
            {
                return (this.dataObject as CSPKG_DRAWWEAL_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_DRAWWEAL_RSP stDrawWealRsp
        {
            get
            {
                return (this.dataObject as SCPKG_DRAWWEAL_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_DYE_INBATTLE_NEWBIEBIT_REQ stDyeInBattleNewbieBitReq
        {
            get
            {
                return (this.dataObject as CSPKG_DYE_INBATTLE_NEWBIEBIT_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_DYE_INBATTLE_NEWBIEBIT_RSP stDyeInBattleNewbieBitRsp
        {
            get
            {
                return (this.dataObject as SCPKG_DYE_INBATTLE_NEWBIEBIT_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_ENTERTAINMENT_SYN_RAND_HERO_CNT stEntertainmentRandHeroCnt
        {
            get
            {
                return (this.dataObject as SCPKG_ENTERTAINMENT_SYN_RAND_HERO_CNT);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_EQUIPCHG stEquipChg
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_EQUIPCHG);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_EQUIPENCHANT stEquipEnchantReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_EQUIPENCHANT);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_EQUIPENCHANT stEquipEnchantRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_EQUIPENCHANT);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_EQUIPSMELT stEquipSmelt
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_EQUIPSMELT);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_EQUIPWEAR stEquipWear
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_EQUIPWEAR);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_SINGLEGAMEFINREQ stFinSingleGameReq
        {
            get
            {
                return (this.dataObject as CSPKG_SINGLEGAMEFINREQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_SINGLEGAMEFINRSP stFinSingleGameRsp
        {
            get
            {
                return (this.dataObject as SCPKG_SINGLEGAMEFINRSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_FIRE_GUILD_MEMBER_NTF stFireGuildMemberNtf
        {
            get
            {
                return (this.dataObject as SCPKG_FIRE_GUILD_MEMBER_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_FIRE_GUILD_MEMBER_REQ stFireGuildMemberReq
        {
            get
            {
                return (this.dataObject as CSPKG_FIRE_GUILD_MEMBER_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_FIRE_GUILD_MEMBER_RSP stFireGuildMemberRsp
        {
            get
            {
                return (this.dataObject as SCPKG_FIRE_GUILD_MEMBER_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_FRAPBOOTINFO stFrapBootInfo
        {
            get
            {
                return (this.dataObject as SCPKG_FRAPBOOTINFO);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_FRAPBOOT_SINGLE stFrapBootSingle
        {
            get
            {
                return (this.dataObject as SCPKG_FRAPBOOT_SINGLE);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_LIST_FREC stFRecReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_LIST_FREC);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_LIST_FREC stFRecRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_LIST_FREC);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_FREEHERO_REQ stFreeHeroReq
        {
            get
            {
                return (this.dataObject as CSPKG_FREEHERO_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_FREEHERO_RSP stFreeHeroRsp
        {
            get
            {
                return (this.dataObject as SCPKG_FREEHERO_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_ADD_FRIEND_CONFIRM stFriendAddConfirmReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_ADD_FRIEND_CONFIRM);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_ADD_FRIEND_CONFIRM stFriendAddConfirmRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_ADD_FRIEND_CONFIRM);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_ADD_FRIEND_DENY stFriendAddDenyReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_ADD_FRIEND_DENY);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_ADD_FRIEND_DENY stFriendAddDenyRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_ADD_FRIEND_DENY);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_ADD_FRIEND stFriendAddReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_ADD_FRIEND);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_ADD_FRIEND stFriendAddRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_ADD_FRIEND);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_DEL_FRIEND stFriendDelReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_DEL_FRIEND);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_DEL_FRIEND stFriendDelRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_DEL_FRIEND);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_DONATE_FRIEND_POINT_ALL stFriendDonatePointAllReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_DONATE_FRIEND_POINT_ALL);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_DONATE_FRIEND_POINT_ALL stFriendDonatePointAllRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_DONATE_FRIEND_POINT_ALL);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_DONATE_FRIEND_POINT stFriendDonatePointReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_DONATE_FRIEND_POINT);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_DONATE_FRIEND_POINT stFriendDonatePointRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_DONATE_FRIEND_POINT);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_INVITE_GAME stFriendInviteGameReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_INVITE_GAME);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_INVITE_GAME stFriendInviteGameRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_INVITE_GAME);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_GET_INVITE_INFO stFriendInviteInfoReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_GET_INVITE_INFO);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_GET_INVITE_INFO stFriendInviteInfoRSP
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_GET_INVITE_INFO);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_LIST_FRIEND stFriendListReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_LIST_FRIEND);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_LIST_FRIEND stFriendListRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_LIST_FRIEND);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_RECALL_FRIEND stFriendRecallReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_RECALL_FRIEND);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_RECALL_FRIEND stFriendRecallRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_RECALL_FRIEND);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_INVITE_RECEIVE_ACHIEVE stFriendReceiveAchieveReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_INVITE_RECEIVE_ACHIEVE);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_INVITE_RECEIVE_ACHIEVE stFriendReceiveAchieveRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_INVITE_RECEIVE_ACHIEVE);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_LIST_FRIENDREQ stFriendReqListReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_LIST_FRIENDREQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_LIST_FRIENDREQ stFriendReqListRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_LIST_FRIENDREQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_SEARCH_PLAYER stFriendSearchPlayerReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_SEARCH_PLAYER);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_SEARCH_PLAYER stFriendSearchPlayerRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_SEARCH_PLAYER);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_FUNCTION_SWITCH_NTF stFunctionSwitchNtf
        {
            get
            {
                return (this.dataObject as SCPKG_FUNCTION_SWITCH_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_FUNCUNLOCK_REQ stFuncUnlockReq
        {
            get
            {
                return (this.dataObject as CSPKG_FUNCUNLOCK_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_GAIN_CHEST stGainChestReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_GAIN_CHEST);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_GAIN_CHEST stGainChestRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_GAIN_CHEST);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GAMECONN_REDIRECT stGameConnRedirect
        {
            get
            {
                return (this.dataObject as SCPKG_GAMECONN_REDIRECT);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GAMELOGINDISPATCH stGameLoginDispatch
        {
            get
            {
                return (this.dataObject as SCPKG_GAMELOGINDISPATCH);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_LOGINFINISHNTF stGameLoginFinishNtf
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_LOGINFINISHNTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_GAMELOGINREQ stGameLoginReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_GAMELOGINREQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_GAMELOGINRSP stGameLoginRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_GAMELOGINRSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_GAMELOGOUTREQ stGameLogoutReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_GAMELOGOUTREQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_GAMELOGOUTRSP stGameLogoutRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_GAMELOGOUTRSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GAMESVRPING stGameSvrPing
        {
            get
            {
                return (this.dataObject as CSPKG_GAMESVRPING);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GAME_VIP_NTF stGameVipNtf
        {
            get
            {
                return (this.dataObject as SCPKG_GAME_VIP_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GAMING_CCSYNC stGamingCCSync
        {
            get
            {
                return (this.dataObject as CSPKG_GAMING_CCSYNC);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GAMING_CSSYNC stGamingCSSync
        {
            get
            {
                return (this.dataObject as CSPKG_GAMING_CSSYNC);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_GEAR_ADVANCE stGearAdvanceReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_GEAR_ADVANCE);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_GEAR_ADVANCE stGearAdvanceRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_GEAR_ADVANCE);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_GEAR_LEVELINFO stGearLevelInfo
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_GEAR_LEVELINFO);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_GEAR_LVLUP stGearLevelUp
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_GEAR_LVLUP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_GEAR_LVLUPALL stGearLvlUpAll
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_GEAR_LVLUPALL);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_ACHIEVEMENT_INFO_NTF stGetAchievememtInfoNtf
        {
            get
            {
                return (this.dataObject as SCPKG_ACHIEVEMENT_INFO_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GET_ACHIEVEMENT_REWARD_REQ stGetAchievementRewardReq
        {
            get
            {
                return (this.dataObject as CSPKG_GET_ACHIEVEMENT_REWARD_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GET_ACHIEVEMENT_REWARD_RSP stGetAchievementRewardRsp
        {
            get
            {
                return (this.dataObject as SCPKG_GET_ACHIEVEMENT_REWARD_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GET_ACNT_CREDIT_VALUE stGetAcntCreditValue
        {
            get
            {
                return (this.dataObject as CSPKG_GET_ACNT_CREDIT_VALUE);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GET_ACNT_DETAIL_INFO_REQ stGetAcntDetailInfoReq
        {
            get
            {
                return (this.dataObject as CSPKG_GET_ACNT_DETAIL_INFO_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GET_ACNT_DETAIL_INFO_RSP stGetAcntDetailInfoRsp
        {
            get
            {
                return (this.dataObject as SCPKG_GET_ACNT_DETAIL_INFO_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GETARENADATA_REQ stGetArenaDataReq
        {
            get
            {
                return (this.dataObject as CSPKG_GETARENADATA_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GETARENADATA_RSP stGetArenaDataRsp
        {
            get
            {
                return (this.dataObject as SCPKG_GETARENADATA_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GETARENAFIGHTHISTORY_REQ stGetArenaFightHistoryReq
        {
            get
            {
                return (this.dataObject as CSPKG_GETARENAFIGHTHISTORY_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GETARENAFIGHTHISTORY_RSP stGetArenaFightHistoryRsp
        {
            get
            {
                return (this.dataObject as SCPKG_GETARENAFIGHTHISTORY_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GETAWARDPOOL_REQ stGetAwardPoolReq
        {
            get
            {
                return (this.dataObject as CSPKG_GETAWARDPOOL_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GETAWARDPOOL_RSP stGetAwardPoolRsp
        {
            get
            {
                return (this.dataObject as SCPKG_GETAWARDPOOL_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GET_BURNING_PROGRESS_REQ stGetBurningProgressReq
        {
            get
            {
                return (this.dataObject as CSPKG_GET_BURNING_PROGRESS_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GET_BURNING_PROGRESS_RSP stGetBurningProgressRsp
        {
            get
            {
                return (this.dataObject as SCPKG_GET_BURNING_PROGRESS_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GET_BURNING_REWARD_REQ stGetBurningRewardReq
        {
            get
            {
                return (this.dataObject as CSPKG_GET_BURNING_REWARD_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GET_BURNING_REWARD_RSP stGetBurningRewardRsp
        {
            get
            {
                return (this.dataObject as SCPKG_GET_BURNING_REWARD_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GET_CHAPTER_REWARD_REQ stGetChapterRewardReq
        {
            get
            {
                return (this.dataObject as CSPKG_GET_CHAPTER_REWARD_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GET_CHAPTER_REWARD_RSP stGetChapterRewardRsp
        {
            get
            {
                return (this.dataObject as SCPKG_GET_CHAPTER_REWARD_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_GET_CHAT_MSG_REQ stGetChatMsgReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_GET_CHAT_MSG_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GET_GROUP_GUILD_ID_NTF stGetGroupGuildIDNtf
        {
            get
            {
                return (this.dataObject as SCPKG_GET_GROUP_GUILD_ID_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GET_GROUP_GUILD_ID_REQ stGetGroupGuildIDReq
        {
            get
            {
                return (this.dataObject as CSPKG_GET_GROUP_GUILD_ID_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GET_GUILD_APPLY_LIST_REQ stGetGuildApplyListReq
        {
            get
            {
                return (this.dataObject as CSPKG_GET_GUILD_APPLY_LIST_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GET_GUILD_APPLY_LIST_RSP stGetGuildApplyListRsp
        {
            get
            {
                return (this.dataObject as SCPKG_GET_GUILD_APPLY_LIST_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GET_GUILD_DIVIDEND_REQ stGetGuildDividendReq
        {
            get
            {
                return (this.dataObject as CSPKG_GET_GUILD_DIVIDEND_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GET_GUILD_DIVIDEND_RSP stGetGuildDividendRsp
        {
            get
            {
                return (this.dataObject as SCPKG_GET_GUILD_DIVIDEND_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GET_GUILD_INFO_REQ stGetGuildInfoReq
        {
            get
            {
                return (this.dataObject as CSPKG_GET_GUILD_INFO_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GET_GUILD_INFO_RSP stGetGuildInfoRsp
        {
            get
            {
                return (this.dataObject as SCPKG_GET_GUILD_INFO_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GET_GUILD_LIST_REQ stGetGuildListReq
        {
            get
            {
                return (this.dataObject as CSPKG_GET_GUILD_LIST_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GET_GUILD_LIST_RSP stGetGuildListRsp
        {
            get
            {
                return (this.dataObject as SCPKG_GET_GUILD_LIST_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GET_GUILD_MEMBER_GAME_STATE_REQ stGetGuildMemberGameStateReq
        {
            get
            {
                return (this.dataObject as CSPKG_GET_GUILD_MEMBER_GAME_STATE_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GET_GUILD_MEMBER_GAME_STATE_RSP stGetGuildMemberGameStateRsp
        {
            get
            {
                return (this.dataObject as SCPKG_GET_GUILD_MEMBER_GAME_STATE_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GET_GUILD_RECOMMEND_LIST_REQ stGetGuildRecommendListReq
        {
            get
            {
                return (this.dataObject as CSPKG_GET_GUILD_RECOMMEND_LIST_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GET_GUILD_RECOMMEND_LIST_RSP stGetGuildRecommendListRsp
        {
            get
            {
                return (this.dataObject as SCPKG_GET_GUILD_RECOMMEND_LIST_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_GET_HORNMSG stGetHornMsgReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_GET_HORNMSG);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_GET_HORNMSG stGetHornMsgRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_GET_HORNMSG);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GET_MATCHINFO_REQ stGetMatchInfoReq
        {
            get
            {
                return (this.dataObject as CSPKG_GET_MATCHINFO_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GET_MATCHINFO_RSP stGetMatchInfoRsp
        {
            get
            {
                return (this.dataObject as SCPKG_GET_MATCHINFO_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GET_PREPARE_GUILD_INFO_REQ stGetPrepareGuildInfoReq
        {
            get
            {
                return (this.dataObject as CSPKG_GET_PREPARE_GUILD_INFO_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GET_PREPARE_GUILD_INFO_RSP stGetPrepareGuildInfoRsp
        {
            get
            {
                return (this.dataObject as SCPKG_GET_PREPARE_GUILD_INFO_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GET_PREPARE_GUILD_LIST_REQ stGetPrepareGuildListReq
        {
            get
            {
                return (this.dataObject as CSPKG_GET_PREPARE_GUILD_LIST_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GET_PREPARE_GUILD_LIST_RSP stGetPrepareGuildListRsp
        {
            get
            {
                return (this.dataObject as SCPKG_GET_PREPARE_GUILD_LIST_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GET_RANKING_ACNT_INFO_REQ stGetRankingAcntInfoReq
        {
            get
            {
                return (this.dataObject as CSPKG_GET_RANKING_ACNT_INFO_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GET_RANKING_ACNT_INFO_RSP stGetRankingAcntInfoRsp
        {
            get
            {
                return (this.dataObject as SCPKG_GET_RANKING_ACNT_INFO_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GET_RANKING_LIST_REQ stGetRankingListReq
        {
            get
            {
                return (this.dataObject as CSPKG_GET_RANKING_LIST_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GET_RANKING_LIST_RSP stGetRankingListRsp
        {
            get
            {
                return (this.dataObject as SCPKG_GET_RANKING_LIST_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GET_RANKLIST_BY_SPECIAL_SCORE_REQ stGetRankListBySpecialScoreReq
        {
            get
            {
                return (this.dataObject as CSPKG_GET_RANKLIST_BY_SPECIAL_SCORE_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GET_RANKLIST_BY_SPECIAL_SCORE_RSP stGetRankListBySpecialScoreRsp
        {
            get
            {
                return (this.dataObject as SCPKG_GET_RANKLIST_BY_SPECIAL_SCORE_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GETRANKREWARD_REQ stGetRankRewardReq
        {
            get
            {
                return (this.dataObject as CSPKG_GETRANKREWARD_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GETRANKREWARD_RSP stGetRankRewardRsp
        {
            get
            {
                return (this.dataObject as SCPKG_GETRANKREWARD_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GET_SELFRANKINFO stGetSelfRankInfo
        {
            get
            {
                return (this.dataObject as CSPKG_GET_SELFRANKINFO);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GET_SPECIAL_GUILD_RANK_INFO_REQ stGetSpecialGuildRankInfoReq
        {
            get
            {
                return (this.dataObject as CSPKG_GET_SPECIAL_GUILD_RANK_INFO_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GET_SPECIAL_GUILD_RANK_INFO_RSP stGetSpecialGuildRankInfoRsp
        {
            get
            {
                return (this.dataObject as SCPKG_GET_SPECIAL_GUILD_RANK_INFO_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GETTOPFIGHTEROFARENA_REQ stGetTopFighterOfArenaReq
        {
            get
            {
                return (this.dataObject as CSPKG_GETTOPFIGHTEROFARENA_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GETTOPFIGHTEROFARENA_RSP stGetTopFighterOfArenaRsp
        {
            get
            {
                return (this.dataObject as SCPKG_GETTOPFIGHTEROFARENA_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_PROPUSE_GIFTGET stGiftUseGet
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_PROPUSE_GIFTGET);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GM_ADDALLSKIN_RSP stGMAddAllSkillRsp
        {
            get
            {
                return (this.dataObject as SCPKG_GM_ADDALLSKIN_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_ACHIEVEHERO_RSP stGMAddHeroRsp
        {
            get
            {
                return (this.dataObject as SCPKG_ACHIEVEHERO_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GMUNLOGCKHEROPVP_RSP stGMUnlockHeroPVPRsp
        {
            get
            {
                return (this.dataObject as SCPKG_GMUNLOGCKHEROPVP_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GUILD_ACTIVE_CHANGE_NTF stGuildActiveChgNtf
        {
            get
            {
                return (this.dataObject as SCPKG_GUILD_ACTIVE_CHANGE_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GUILD_BUILDING_LEVEL_CHANGE_NTF stGuildBuildingLvChgNtf
        {
            get
            {
                return (this.dataObject as SCPKG_GUILD_BUILDING_LEVEL_CHANGE_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GUILD_BUILDING_UPGRADE_REQ stGuildBuildingUpgradeReq
        {
            get
            {
                return (this.dataObject as CSPKG_GUILD_BUILDING_UPGRADE_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GUILD_BUILDING_UPGRADE_RSP stGuildBuildingUpgradeRsp
        {
            get
            {
                return (this.dataObject as SCPKG_GUILD_BUILDING_UPGRADE_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GUILD_CONSTRUCT_CHG stGuildConstructChg
        {
            get
            {
                return (this.dataObject as SCPKG_GUILD_CONSTRUCT_CHG);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GUILD_CROSS_DAY_NTF stGuildCrossDayNtf
        {
            get
            {
                return (this.dataObject as SCPKG_GUILD_CROSS_DAY_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GUILD_CROSS_WEEK_NTF stGuildCrossWeekNtf
        {
            get
            {
                return (this.dataObject as SCPKG_GUILD_CROSS_WEEK_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GUILD_DONATE_REQ stGuildDonateReq
        {
            get
            {
                return (this.dataObject as CSPKG_GUILD_DONATE_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GUILD_DONATE_RSP stGuildDonateRsp
        {
            get
            {
                return (this.dataObject as SCPKG_GUILD_DONATE_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GUILD_INVITE_REQ stGuildInviteReq
        {
            get
            {
                return (this.dataObject as CSPKG_GUILD_INVITE_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GUILD_INVITE_RSP stGuildInviteRsp
        {
            get
            {
                return (this.dataObject as SCPKG_GUILD_INVITE_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GUILD_MONEY_CHANGE_NTF stGuildMoneyChgNtf
        {
            get
            {
                return (this.dataObject as SCPKG_GUILD_MONEY_CHANGE_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GUILD_NAME_CHG_NTF stGuildNameChgNtf
        {
            get
            {
                return (this.dataObject as SCPKG_GUILD_NAME_CHG_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GUILD_POSITION_CHG_NTF stGuildPositionChgNtf
        {
            get
            {
                return (this.dataObject as SCPKG_GUILD_POSITION_CHG_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GUILD_RANK_RESET_NTF stGuildRankResetNtf
        {
            get
            {
                return (this.dataObject as SCPKG_GUILD_RANK_RESET_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GUILD_RECOMMEND_NTF stGuildRecommendNtf
        {
            get
            {
                return (this.dataObject as SCPKG_GUILD_RECOMMEND_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GUILD_RECOMMEND_REQ stGuildRecommendReq
        {
            get
            {
                return (this.dataObject as CSPKG_GUILD_RECOMMEND_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GUILD_RECOMMEND_RSP stGuildRecommendRsp
        {
            get
            {
                return (this.dataObject as SCPKG_GUILD_RECOMMEND_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GUILD_SEASON_RESET_NTF stGuildSeasonResetNtf
        {
            get
            {
                return (this.dataObject as SCPKG_GUILD_SEASON_RESET_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GUILD_SELF_RECOMMEND_NTF stGuildSelfRecommemdNtf
        {
            get
            {
                return (this.dataObject as SCPKG_GUILD_SELF_RECOMMEND_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GUILD_SELF_RECOMMEND_REQ stGuildSelfRecommemdReq
        {
            get
            {
                return (this.dataObject as CSPKG_GUILD_SELF_RECOMMEND_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GUILD_SELF_RECOMMEND_RSP stGuildSelfRecommemdRsp
        {
            get
            {
                return (this.dataObject as SCPKG_GUILD_SELF_RECOMMEND_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GUILD_SIGNIN_REQ stGuildSignInReq
        {
            get
            {
                return (this.dataObject as CSPKG_GUILD_SIGNIN_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GUILD_SIGNIN_RSP stGuildSignInRsp
        {
            get
            {
                return (this.dataObject as SCPKG_GUILD_SIGNIN_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GUILD_SYMBOL_REQ stGuildSymbolReq
        {
            get
            {
                return (this.dataObject as CSPKG_GUILD_SYMBOL_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GUILD_SYMBOL_RSP stGuildSymbolRsp
        {
            get
            {
                return (this.dataObject as SCPKG_GUILD_SYMBOL_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_HANGUP_NTF stHangUpNtf
        {
            get
            {
                return (this.dataObject as SCPKG_HANGUP_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_NTF_HEADIMG_ADD stHeadImgAddNtf
        {
            get
            {
                return (this.dataObject as SCPKG_NTF_HEADIMG_ADD);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_NTF_HEADIMG_CHG stHeadImgChgNtf
        {
            get
            {
                return (this.dataObject as SCPKG_NTF_HEADIMG_CHG);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_HEADIMG_CHG_REQ stHeadImgChgReq
        {
            get
            {
                return (this.dataObject as CSPKG_HEADIMG_CHG_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_HEADIMG_CHG_RSP stHeadImgChgRsp
        {
            get
            {
                return (this.dataObject as SCPKG_HEADIMG_CHG_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_NTF_HEADIMG_DEL stHeadImgDelNtf
        {
            get
            {
                return (this.dataObject as SCPKG_NTF_HEADIMG_DEL);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_HEADIMG_FLAGCLR_REQ stHeadImgFlagClrReq
        {
            get
            {
                return (this.dataObject as CSPKG_HEADIMG_FLAGCLR_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_HEADIMG_FLAGCLR_RSP stHeadImgFlagClrRsp
        {
            get
            {
                return (this.dataObject as SCPKG_HEADIMG_FLAGCLR_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_HEADIMG_LIST_SYNC stHeadImgListSync
        {
            get
            {
                return (this.dataObject as SCPKG_HEADIMG_LIST_SYNC);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_HEARTBEAT stHeartBeat
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_HEARTBEAT);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_HEROADVANCE stHeroAdvanceReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_HEROADVANCE);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_HEROADVANCE stHeroAdvanceRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_HEROADVANCE);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_HEROEXP_ADD stHeroExpAdd
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_HEROEXP_ADD);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_NTF_HERO_INFO_UPD stHeroInfoUpdNtf
        {
            get
            {
                return (this.dataObject as SCPKG_NTF_HERO_INFO_UPD);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_HEROSKIN_ADD stHeroSkinAdd
        {
            get
            {
                return (this.dataObject as SCPKG_HEROSKIN_ADD);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_HERO_WAKECHG stHeroWakeChg
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_HERO_WAKECHG);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_HERO_WAKEOPT stHeroWakeOpt
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_HERO_WAKEOPT);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_HEROWAKE_REWARD stHeroWakeReward
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_HEROWAKE_REWARD);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_HERO_WAKESTEP stHeroWakeStep
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_HERO_WAKESTEP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_HORNUSE stHornUseReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_HORNUSE);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_HORNUSE stHornUseRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_HORNUSE);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_HUOYUEDUREWARDERR_NTF stHuoYueDuRewardErr
        {
            get
            {
                return (this.dataObject as SCPKG_HUOYUEDUREWARDERR_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_GETHUOYUEDUREWARD_REQ stHuoYueDuRewardReq
        {
            get
            {
                return (this.dataObject as CSPKG_GETHUOYUEDUREWARD_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_GETHUOYUEDUREWARD_RSP stHuoYueDuRewardRsp
        {
            get
            {
                return (this.dataObject as SCPKG_GETHUOYUEDUREWARD_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_INVITE_FRIEND_JOIN_ROOM_REQ stInviteFriendJoinRoomReq
        {
            get
            {
                return (this.dataObject as CSPKG_INVITE_FRIEND_JOIN_ROOM_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_INVITE_FRIEND_JOIN_ROOM_RSP stInviteFriendJoinRoomRsp
        {
            get
            {
                return (this.dataObject as SCPKG_INVITE_FRIEND_JOIN_ROOM_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_INVITE_FRIEND_JOIN_TEAM_REQ stInviteFriendJoinTeamReq
        {
            get
            {
                return (this.dataObject as CSPKG_INVITE_FRIEND_JOIN_TEAM_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_INVITE_FRIEND_JOIN_TEAM_RSP stInviteFriendJoinTeamRsp
        {
            get
            {
                return (this.dataObject as SCPKG_INVITE_FRIEND_JOIN_TEAM_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_INVITE_GUILD_MEMBER_JOIN_REQ stInviteGuildMemberJoinReq
        {
            get
            {
                return (this.dataObject as CSPKG_INVITE_GUILD_MEMBER_JOIN_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_INVITE_JOIN_GAME_REQ stInviteJoinGameReq
        {
            get
            {
                return (this.dataObject as SCPKG_INVITE_JOIN_GAME_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_INVITE_JOIN_GAME_RSP stInviteJoinGameRsp
        {
            get
            {
                return (this.dataObject as CSPKG_INVITE_JOIN_GAME_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_ISACCEPT_AIPLAYER_REQ stIsAcceptAiPlayerReq
        {
            get
            {
                return (this.dataObject as SCPKG_ISACCEPT_AIPLAYER_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_ISACCEPT_AIPLAYER_RSP stIsAcceptAiPlayerRsp
        {
            get
            {
                return (this.dataObject as CSPKG_ISACCEPT_AIPLAYER_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_ITEMADD stItemAdd
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_ITEMADD);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_ITEMBUY stItemBuyReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_ITEMBUY);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_ITEMBUY stItemBuyRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_ITEMBUY);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_ITEMCOMP stItemComp
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_ITEMCOMP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_ITEMDEL stItemDel
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_ITEMDEL);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_ITEMSALE stItemSale
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_ITEMSALE);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_JOINARENA_REQ stJoinArenaReq
        {
            get
            {
                return (this.dataObject as CSPKG_JOINARENA_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_JOINARENA_RSP stJoinArenaRsp
        {
            get
            {
                return (this.dataObject as SCPKG_JOINARENA_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_JOIN_GUILD_APPLY_NTF stJoinGuildApplyNtf
        {
            get
            {
                return (this.dataObject as SCPKG_JOIN_GUILD_APPLY_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_JOINMULTIGAMERSP stJoinMultGameRsp
        {
            get
            {
                return (this.dataObject as SCPKG_JOINMULTIGAMERSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_JOIN_PREPARE_GUILD_NTF stJoinPrepareGuildNtf
        {
            get
            {
                return (this.dataObject as SCPKG_JOIN_PREPARE_GUILD_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_JOIN_PREPARE_GUILD_REQ stJoinPrepareGuildReq
        {
            get
            {
                return (this.dataObject as CSPKG_JOIN_PREPARE_GUILD_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_JOIN_PREPARE_GUILD_RSP stJoinPrepareGuildRsp
        {
            get
            {
                return (this.dataObject as SCPKG_JOIN_PREPARE_GUILD_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_JOIN_TEAM_RSP stJoinTeamRsp
        {
            get
            {
                return (this.dataObject as SCPKG_JOIN_TEAM_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_KFRAPLATERCHG_NTF stKFrapsLaterChgNtf
        {
            get
            {
                return (this.dataObject as SCPKG_KFRAPLATERCHG_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_KFRAPLATERCHG_REQ stKFrapsLaterChgReq
        {
            get
            {
                return (this.dataObject as CSPKG_KFRAPLATERCHG_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_KICKOUT_ROOMMEMBER_REQ stKickoutRoomMemberReq
        {
            get
            {
                return (this.dataObject as CSPKG_KICKOUT_ROOMMEMBER_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_LEAVL_TEAM stLeaveTeam
        {
            get
            {
                return (this.dataObject as CSPKG_LEAVL_TEAM);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_LICENSE_REQ stLicenseGetReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_LICENSE_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_LICENSE_RSP stLicenseGetRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_LICENSE_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_LIMITSKIN_ADD stLimitSkinAdd
        {
            get
            {
                return (this.dataObject as SCPKG_LIMITSKIN_ADD);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_LIMITSKIN_DEL stLimitSkinDel
        {
            get
            {
                return (this.dataObject as SCPKG_LIMITSKIN_DEL);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_GAMELOGINLIMIT stLoginLimitRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_GAMELOGINLIMIT);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_LOGINSYN_REQ stLoginSynReq
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_LOGINSYN_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_LOGINSYN_RSP stLoginSynRsp
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_LOGINSYN_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_LUCKYDRAW_DATA_NTF stLuckyDrawDataNtf
        {
            get
            {
                return (this.dataObject as SCPKG_LUCKYDRAW_DATA_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_LUCKYDRAW_EXTERN_REQ stLuckyDrawExternReq
        {
            get
            {
                return (this.dataObject as CSPKG_LUCKYDRAW_EXTERN_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_LUCKYDRAW_EXTERN_RSP stLuckyDrawExternRsp
        {
            get
            {
                return (this.dataObject as SCPKG_LUCKYDRAW_EXTERN_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_LUCKYDRAW_REQ stLuckyDrawReq
        {
            get
            {
                return (this.dataObject as CSPKG_LUCKYDRAW_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_LUCKYDRAW_RSP stLuckyDrawRsp
        {
            get
            {
                return (this.dataObject as SCPKG_LUCKYDRAW_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_MAILOPT_REQ stMailOptReq
        {
            get
            {
                return (this.dataObject as CSPKG_MAILOPT_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_MAILOPT_RES stMailOptRes
        {
            get
            {
                return (this.dataObject as SCPKG_MAILOPT_RES);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_MANUALREFRESH stManualRefresh
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_MANUALREFRESH);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_MATCHPOINT_NTF stMatchPointNtf
        {
            get
            {
                return (this.dataObject as SCPKG_MATCHPOINT_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_MATCH_REQ stMatchReq
        {
            get
            {
                return (this.dataObject as CSPKG_MATCH_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_MATCH_RSP stMatchRsp
        {
            get
            {
                return (this.dataObject as SCPKG_MATCH_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_MATCHTEAM_DESTROY_NTF stMatchTeamDestroyNtf
        {
            get
            {
                return (this.dataObject as SCPKG_MATCHTEAM_DESTROY_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_MEMBER_ONLINE_NTF stMemberOnlineNtf
        {
            get
            {
                return (this.dataObject as SCPKG_MEMBER_ONLINE_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_MEMBER_RANK_POINT_NTF stMemberRankPointNtf
        {
            get
            {
                return (this.dataObject as SCPKG_MEMBER_RANK_POINT_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_MEMBER_TOP_KDA_NTF stMemberTopKDANtf
        {
            get
            {
                return (this.dataObject as SCPKG_MEMBER_TOP_KDA_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_MODIFY_GUILD_SETTING_REQ stModifyGuildSettingReq
        {
            get
            {
                return (this.dataObject as CSPKG_MODIFY_GUILD_SETTING_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_MODIFY_GUILD_SETTING_RSP stModifyGuildSettingRsp
        {
            get
            {
                return (this.dataObject as SCPKG_MODIFY_GUILD_SETTING_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_MONTH_WEEK_CARD_USE_RSP stMonthWeekCardUseRsp
        {
            get
            {
                return (this.dataObject as SCPKG_MONTH_WEEK_CARD_USE_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_MULTGAMEABORTNTF stMultGameAbortNtf
        {
            get
            {
                return (this.dataObject as SCPKG_MULTGAMEABORTNTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_MULTGAME_BEGINFIGHT stMultGameBeginFight
        {
            get
            {
                return (this.dataObject as SCPKG_MULTGAME_BEGINFIGHT);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_MULTGAME_BEGINLOAD stMultGameBeginLoad
        {
            get
            {
                return (this.dataObject as SCPKG_MULTGAME_BEGINLOAD);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_MULTGAME_BEGINPICK stMultGameBeginPick
        {
            get
            {
                return (this.dataObject as SCPKG_MULTGAME_BEGINPICK);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_MULTGAME_DIE_REQ stMultGameDieReq
        {
            get
            {
                return (this.dataObject as CSPKG_MULTGAME_DIE_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_MULTGAME_DISCONN_NTF stMultGameDisconnNtf
        {
            get
            {
                return (this.dataObject as SCPKG_MULTGAME_DISCONN_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_MULTGAME_LOADFIN stMultGameLoadFin
        {
            get
            {
                return (this.dataObject as CSPKG_MULTGAME_LOADFIN);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_MULTGAME_LOADPROCESS stMultGameLoadProcessReq
        {
            get
            {
                return (this.dataObject as CSPKG_MULTGAME_LOADPROCESS);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_MULTGAME_LOADPROCESS stMultGameLoadProcessRsp
        {
            get
            {
                return (this.dataObject as SCPKG_MULTGAME_LOADPROCESS);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_MULTGAME_GAMEOVER stMultGameOverReq
        {
            get
            {
                return (this.dataObject as CSPKG_MULTGAME_GAMEOVER);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_MULTGAME_GAMEOVER stMultGameOverRsp
        {
            get
            {
                return (this.dataObject as SCPKG_MULTGAME_GAMEOVER);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_MULTGAMEREADYNTF stMultGameReadyNtf
        {
            get
            {
                return (this.dataObject as SCPKG_MULTGAMEREADYNTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_MULTGAME_RECONN_NTF stMultGameReconnNtf
        {
            get
            {
                return (this.dataObject as SCPKG_MULTGAME_RECONN_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_MULTGAMERECOVERNTF stMultGameRecoverNtf
        {
            get
            {
                return (this.dataObject as SCPKG_MULTGAMERECOVERNTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_MULTGAME_SETTLEGAIN stMultGameSettleGain
        {
            get
            {
                return (this.dataObject as SCPKG_MULTGAME_SETTLEGAIN);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_NTF_NEWIEALLBITSYN stNewieAllBitSyn
        {
            get
            {
                return (this.dataObject as SCPKG_NTF_NEWIEALLBITSYN);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_NTF_NEWIEBITSYN stNewieBitSyn
        {
            get
            {
                return (this.dataObject as SCPKG_NTF_NEWIEBITSYN);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_NEW_MEMBER_JOIN_GULD_NTF stNewMemberJoinGuildNtf
        {
            get
            {
                return (this.dataObject as SCPKG_NEW_MEMBER_JOIN_GULD_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_NEWTASKGET_NTF stNewTaskGet
        {
            get
            {
                return (this.dataObject as SCPKG_NEWTASKGET_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_NEXTFIRSTWINSEC_NTF stNextFirstWinSecNtf
        {
            get
            {
                return (this.dataObject as CSPKG_NEXTFIRSTWINSEC_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_NOTICE_HANGUP stNoticeHangup
        {
            get
            {
                return (this.dataObject as SCPKG_NOTICE_HANGUP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_NOTICEINFO_REQ stNoticeInfoReq
        {
            get
            {
                return (this.dataObject as CSPKG_NOTICEINFO_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_NOTICEINFO_RSP stNoticeInfoRsp
        {
            get
            {
                return (this.dataObject as SCPKG_NOTICEINFO_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_NOTICELIST_REQ stNoticeListReq
        {
            get
            {
                return (this.dataObject as CSPKG_NOTICELIST_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_NOTICELIST_RSP stNoticeListRsp
        {
            get
            {
                return (this.dataObject as SCPKG_NOTICELIST_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_NOTICENEW_REQ stNoticeNewReq
        {
            get
            {
                return (this.dataObject as CSPKG_NOTICENEW_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_NOTICENEW_RSP stNoticeNewRsp
        {
            get
            {
                return (this.dataObject as SCPKG_NOTICENEW_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_NTF_ACNT_CREDIT_VALUE stNtfAcntCreditValue
        {
            get
            {
                return (this.dataObject as SCPKG_NTF_ACNT_CREDIT_VALUE);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_NTF_ACNT_INFO_UPD stNtfAcntInfoUpd
        {
            get
            {
                return (this.dataObject as SCPKG_NTF_ACNT_INFO_UPD);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_NTF_ACNT_LEVELUP stNtfAcntLevelUp
        {
            get
            {
                return (this.dataObject as SCPKG_NTF_ACNT_LEVELUP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_NTF_ACNT_PVPLEVELUP stNtfAcntPvpLevelUp
        {
            get
            {
                return (this.dataObject as SCPKG_NTF_ACNT_PVPLEVELUP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_NTF_ACNT_REGISTER stNtfAcntRegiter
        {
            get
            {
                return (this.dataObject as SCPKG_NTF_ACNT_REGISTER);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_NTF_ACNT_SNSNAME stNtfAcntSnsName
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_NTF_ACNT_SNSNAME);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_NTF_ADDCURSEASONRECORD stNtfAddCurSeasonRecord
        {
            get
            {
                return (this.dataObject as SCPKG_NTF_ADDCURSEASONRECORD);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_NTF_ADDPASTSEASONRECORD stNtfAddPastSeasonRecord
        {
            get
            {
                return (this.dataObject as SCPKG_NTF_ADDPASTSEASONRECORD);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_PVP_NTF_CLIENT stNtfClient
        {
            get
            {
                return (this.dataObject as SCPKG_PVP_NTF_CLIENT);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_MULTGAME_NTF_CLT_GAMEOVER stNtfCltGameOver
        {
            get
            {
                return (this.dataObject as SCPKG_MULTGAME_NTF_CLT_GAMEOVER);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_NTF_ERRCODE stNtfErr
        {
            get
            {
                return (this.dataObject as SCPKG_NTF_ERRCODE);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_NTF_FRIEND_ADD stNtfFriendAdd
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_NTF_FRIEND_ADD);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_NTF_FRIEND_DEL stNtfFriendDel
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_NTF_FRIEND_DEL);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_NTF_FRIEND_GAME_STATE stNtfFriendGameState
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_NTF_FRIEND_GAME_STATE);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_NTF_FRIEND_LOGIN_STATUS stNtfFriendLoginStatus
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_NTF_FRIEND_LOGIN_STATUS);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_NTF_FRIEND_REQUEST stNtfFriendRequest
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_NTF_FRIEND_REQUEST);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_NTF_HUOYUEDUINFO stNtfHuoYueDuInfo
        {
            get
            {
                return (this.dataObject as SCPKG_NTF_HUOYUEDUINFO);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_ACNT_RANKINFO_RSP stNtfRankInfoRsp
        {
            get
            {
                return (this.dataObject as SCPKG_ACNT_RANKINFO_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_NTF_RECALL_FRIEND stNtfRecallFirend
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_NTF_RECALL_FRIEND);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_NTF_SNS_FRIEND stNtfSnsFriend
        {
            get
            {
                return (this.dataObject as SCPKG_NTF_SNS_FRIEND);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_OFFINGRESTART_REQ stOffingRestartReq
        {
            get
            {
                return (this.dataObject as SCPKG_OFFINGRESTART_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_OFFINGRESTART_RSP stOffingRestartRsp
        {
            get
            {
                return (this.dataObject as CSPKG_OFFINGRESTART_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_OPER_HERO_REQ stOperHeroReq
        {
            get
            {
                return (this.dataObject as CSPKG_OPER_HERO_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_OPER_HERO_NTF stOperHeroRsp
        {
            get
            {
                return (this.dataObject as SCPKG_OPER_HERO_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_OPER_TEAM_REQ stOperTeamReq
        {
            get
            {
                return (this.dataObject as CSPKG_OPER_TEAM_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_PKGDETAIL stPkgDetail
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_PKGDETAIL);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_PKGQUERY stPkgQuery
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_PKGQUERY);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_PREPARE_GUILD_BREAK_NTF stPrepareGuildBreakNtf
        {
            get
            {
                return (this.dataObject as SCPKG_PREPARE_GUILD_BREAK_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_PRESENTHERO_REQ stPresentHeroReq
        {
            get
            {
                return (this.dataObject as CSPKG_PRESENTHERO_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_PRESENTHERO_RSP stPresentHeroRsp
        {
            get
            {
                return (this.dataObject as SCPKG_PRESENTHERO_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_PRESENTSKIN_REQ stPresentSkinReq
        {
            get
            {
                return (this.dataObject as CSPKG_PRESENTSKIN_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_PERSENTSKIN_RSP stPresentSkinRsp
        {
            get
            {
                return (this.dataObject as SCPKG_PERSENTSKIN_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_PROP_MULTIPLE_NTF stPropMultipleNtf
        {
            get
            {
                return (this.dataObject as SCPKG_PROP_MULTIPLE_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_PROPUSE stPropUse
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_PROPUSE);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_PROPUSE stPropUseRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_PROPUSE);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_PVE_REVIVE_REQ stPveReviveReq
        {
            get
            {
                return (this.dataObject as CSPKG_PVE_REVIVE_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_PVE_REVIVE_RSP stPveReviveRsp
        {
            get
            {
                return (this.dataObject as SCPKG_PVE_REVIVE_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_PVP_COMMINFO_REPORT stPVPCommonInfoReport
        {
            get
            {
                return (this.dataObject as CSPKG_PVP_COMMINFO_REPORT);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_PVP_GAMEDATA_REPORT stPVPGameDataReport
        {
            get
            {
                return (this.dataObject as CSPKG_PVP_GAMEDATA_REPORT);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_PVP_GAMEDATA_REPORTOVER stPVPGameDataReportOver
        {
            get
            {
                return (this.dataObject as CSPKG_PVP_GAMEDATA_REPORTOVER);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_PVP_GAMELOG_REPORT stPVPGameLogReport
        {
            get
            {
                return (this.dataObject as CSPKG_PVP_GAMELOG_REPORT);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_PVP_GAMELOG_REPORTOVER stPVPGameLogReportOver
        {
            get
            {
                return (this.dataObject as CSPKG_PVP_GAMELOG_REPORTOVER);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_QQVIPINFO_REQ stQQVIPInfoReq
        {
            get
            {
                return (this.dataObject as CSPKG_QQVIPINFO_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_QQVIPINFO_RSP stQQVIPInfoRsp
        {
            get
            {
                return (this.dataObject as SCPKG_QQVIPINFO_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_QUIT_GUILD_NTF stQuitGuildNtf
        {
            get
            {
                return (this.dataObject as SCPKG_QUIT_GUILD_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_QUIT_GUILD_REQ stQuitGuildReq
        {
            get
            {
                return (this.dataObject as CSPKG_QUIT_GUILD_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_QUIT_GUILD_RSP stQuitGuildRsp
        {
            get
            {
                return (this.dataObject as SCPKG_QUIT_GUILD_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_QUITMULTIGAMEREQ stQuitMultGameReq
        {
            get
            {
                return (this.dataObject as CSPKG_QUITMULTIGAMEREQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_QUITMULTIGAMERSP stQuitMultGameRsp
        {
            get
            {
                return (this.dataObject as SCPKG_QUITMULTIGAMERSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_QUITSINGLEGAMEREQ stQuitSingleGameReq
        {
            get
            {
                return (this.dataObject as CSPKG_QUITSINGLEGAMEREQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_QUITSINGLEGAMERSP stQuitSingleGameRsp
        {
            get
            {
                return (this.dataObject as SCPKG_QUITSINGLEGAMERSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_RANDDRAW_REQ stRandDrawReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_RANDDRAW_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_RANDDRAW_RSP stRandDrawRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_RANDDRAW_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_RANKCURSEASONHISTORY_NTF stRankCurSeasonHistory
        {
            get
            {
                return (this.dataObject as SCPKG_RANKCURSEASONHISTORY_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_RANKPASTSEASONHISTORY_NTF stRankPastSeasonHistory
        {
            get
            {
                return (this.dataObject as SCPKG_RANKPASTSEASONHISTORY_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_RECONNGAME_NTF stReconnGameNtf
        {
            get
            {
                return (this.dataObject as SCPKG_RECONNGAME_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_RECOVERGAMEFRAP_REQ stRecoverFrapReq
        {
            get
            {
                return (this.dataObject as CSPKG_RECOVERGAMEFRAP_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_RECOVERGAMEFRAP_RSP stRecoverFrapRsp
        {
            get
            {
                return (this.dataObject as SCPKG_RECOVERGAMEFRAP_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_RECOVERGAMESUCC stRecoverGameSuccReq
        {
            get
            {
                return (this.dataObject as CSPKG_RECOVERGAMESUCC);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_RECOVER_SYSTEMEQUIP_REQ stRecoverSystemEquipChgReq
        {
            get
            {
                return (this.dataObject as CSPKG_RECOVER_SYSTEMEQUIP_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_RECOVER_SYSTEMEQUIP_RSP stRecoverSystemEquipChgRsp
        {
            get
            {
                return (this.dataObject as SCPKG_RECOVER_SYSTEMEQUIP_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_REJECT_GUILD_RECOMMEND stRejectGuildRecommend
        {
            get
            {
                return (this.dataObject as CSPKG_REJECT_GUILD_RECOMMEND);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_RELAYHASHCHECK stRelayHashChk
        {
            get
            {
                return (this.dataObject as CSPKG_RELAYHASHCHECK);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_RELAYSVRPING stRelaySvrPing
        {
            get
            {
                return (this.dataObject as CSPKG_RELAYSVRPING);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_RELOGINNOW stReloginNow
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_RELOGINNOW);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_REQUESTFRAPBOOTSINGLE stReqFrapBootSingle
        {
            get
            {
                return (this.dataObject as CSPKG_REQUESTFRAPBOOTSINGLE);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_REQUESTFRAPBOOTTIMEOUT stReqFrapBootTimeout
        {
            get
            {
                return (this.dataObject as CSPKG_REQUESTFRAPBOOTTIMEOUT);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_RES_DATA_NTF stResDataNtf
        {
            get
            {
                return (this.dataObject as SCPKG_RES_DATA_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_RESET_BURNING_PROGRESS_REQ stResetBurningProgressReq
        {
            get
            {
                return (this.dataObject as CSPKG_RESET_BURNING_PROGRESS_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_RESET_BURNING_PROGRESS_RSP stResetBurningProgressRsp
        {
            get
            {
                return (this.dataObject as SCPKG_RESET_BURNING_PROGRESS_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_ROLLINGMSG_NTF stRollingMsgNtf
        {
            get
            {
                return (this.dataObject as SCPKG_ROLLINGMSG_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_ROOMCHGNTF stRoomChgNtf
        {
            get
            {
                return (this.dataObject as SCPKG_ROOMCHGNTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_ROOM_CONFIRM_REQ stRoomConfirmReq
        {
            get
            {
                return (this.dataObject as CSPKG_ROOM_CONFIRM_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_ROOM_CONFIRM_RSP stRoomConfirmRsp
        {
            get
            {
                return (this.dataObject as SCPKG_ROOM_CONFIRM_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_ROOM_STARTSINGLEGAME_NTF stRoomStateSingleGameNtf
        {
            get
            {
                return (this.dataObject as SCPKG_ROOM_STARTSINGLEGAME_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_RSP_ACNT_TRANS_VISITORSVRDATA stRspAcntTransVisitorSvrData
        {
            get
            {
                return (this.dataObject as CSPKG_RSP_ACNT_TRANS_VISITORSVRDATA);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_MULTGAME_RUNAWAY_NTF stRunAwayNtf
        {
            get
            {
                return (this.dataObject as SCPKG_MULTGAME_RUNAWAY_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_MULTGAME_RUNAWAY_REQ stRunAwayReq
        {
            get
            {
                return (this.dataObject as CSPKG_MULTGAME_RUNAWAY_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_MULTGAME_RUNAWAY_RSP stRunAwayRsp
        {
            get
            {
                return (this.dataObject as SCPKG_MULTGAME_RUNAWAY_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_SALERECMD_BUY stSaleRecmdBuyReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_SALERECMD_BUY);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_SALERECMD_BUY stSaleRecmdBuyRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_SALERECMD_BUY);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_SEARCH_GUILD_REQ stSearchGuildReq
        {
            get
            {
                return (this.dataObject as CSPKG_SEARCH_GUILD_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_SEARCH_GUILD_RSP stSearchGuildRsp
        {
            get
            {
                return (this.dataObject as SCPKG_SEARCH_GUILD_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_SEARCH_PREGUILD_REQ stSearchPreGuildReq
        {
            get
            {
                return (this.dataObject as CSPKG_SEARCH_PREGUILD_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_SEARCH_PREGUILD_RSP stSearchPreGuildRsp
        {
            get
            {
                return (this.dataObject as SCPKG_SEARCH_PREGUILD_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_SECURE_INFO_START_REQ stSecureInfoStartReq
        {
            get
            {
                return (this.dataObject as CSPKG_SECURE_INFO_START_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_SELF_BEKICK_FROM_TEAM stSelfBeKickFromTeam
        {
            get
            {
                return (this.dataObject as SCPKG_SELF_BEKICK_FROM_TEAM);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_SELFDEFINE_HEROEQUIP_CHG_REQ stSelfDefineHeroEquipChgReq
        {
            get
            {
                return (this.dataObject as CSPKG_SELFDEFINE_HEROEQUIP_CHG_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_SELFDEFINE_HEROEQUIP_CHG_RSP stSelfDefineHeroEquipChgRsp
        {
            get
            {
                return (this.dataObject as SCPKG_SELFDEFINE_HEROEQUIP_CHG_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_SERVERTIME_REQ stServerTimeReq
        {
            get
            {
                return (this.dataObject as CSPKG_SERVERTIME_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_SERVERTIME_RSP stServerTimeRsp
        {
            get
            {
                return (this.dataObject as SCPKG_SERVERTIME_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_SET_ACNT_NEWBIE_TYPE_REQ stSetAcntNewbieTypeReq
        {
            get
            {
                return (this.dataObject as CSPKG_SET_ACNT_NEWBIE_TYPE_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_SET_ACNT_NEWBIE_TYPE_RSP stSetAcntNewbieTypeRsp
        {
            get
            {
                return (this.dataObject as SCPKG_SET_ACNT_NEWBIE_TYPE_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_SETBATTLELISTOFARENA_REQ stSetBattleListOfArenaReq
        {
            get
            {
                return (this.dataObject as CSPKG_SETBATTLELISTOFARENA_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_SETBATTLELISTOFARENA_RSP stSetBattleListOfArenaRsp
        {
            get
            {
                return (this.dataObject as SCPKG_SETBATTLELISTOFARENA_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_SET_GUILD_GROUP_OPENID_NTF stSetGuildGroupOpenIDNtf
        {
            get
            {
                return (this.dataObject as SCPKG_SET_GUILD_GROUP_OPENID_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_SET_GUILD_GROUP_OPENID_REQ stSetGuildGroupOpenIDReq
        {
            get
            {
                return (this.dataObject as CSPKG_SET_GUILD_GROUP_OPENID_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_SHARE_TLOG_REQ stShareTLogReq
        {
            get
            {
                return (this.dataObject as CSPKG_SHARE_TLOG_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_SHARE_TLOG_RSP stShareTLogRsp
        {
            get
            {
                return (this.dataObject as SCPKG_SHARE_TLOG_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_SHOPBUY stShopBuyReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_SHOPBUY);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_SHOPBUY stShopBuyRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_SHOPBUY);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_SHOPDETAIL stShopDetail
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_SHOPDETAIL);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_NTF_SHOPTIMEOUT stShopTimeOut
        {
            get
            {
                return (this.dataObject as SCPKG_NTF_SHOPTIMEOUT);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_SKILLUPDATE_REQ stSkillUpdateReq
        {
            get
            {
                return (this.dataObject as CSPKG_SKILLUPDATE_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_SKILLUPDATE_RSP stSkillUpdateRsp
        {
            get
            {
                return (this.dataObject as SCPKG_SKILLUPDATE_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CHG_SNS_FRIEND_PROFILE stSnsFriendChgProfile
        {
            get
            {
                return (this.dataObject as SCPKG_CHG_SNS_FRIEND_PROFILE);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_SPECIAL_SALEINFO stSPecialSaleDetail
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_SPECIAL_SALEINFO);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_SPECSALEBUY stSpecSaleBuyReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_SPECSALEBUY);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_SPECSALEBUY stSpecSaleBuyRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_SPECSALEBUY);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_START_MULTI_GAME_REQ stStartMultiGameReq
        {
            get
            {
                return (this.dataObject as CSPKG_START_MULTI_GAME_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_START_MULTI_GAME_RSP stStartMultiGameRsp
        {
            get
            {
                return (this.dataObject as SCPKG_START_MULTI_GAME_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_STARTSINGLEGAMEREQ stStartSingleGameReq
        {
            get
            {
                return (this.dataObject as CSPKG_STARTSINGLEGAMEREQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_STARTSINGLEGAMERSP stStartSingleGameRsp
        {
            get
            {
                return (this.dataObject as SCPKG_STARTSINGLEGAMERSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_TASKSUBMIT_REQ stSubmitTaskReq
        {
            get
            {
                return (this.dataObject as CSPKG_TASKSUBMIT_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_TASKSUBMIT_RES stSubmitTaskRes
        {
            get
            {
                return (this.dataObject as SCPKG_TASKSUBMIT_RES);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_SURRENDER_NTF stSurrenderNtf
        {
            get
            {
                return (this.dataObject as SCPKG_SURRENDER_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_SURRENDER_REQ stSurrenderReq
        {
            get
            {
                return (this.dataObject as CSPKG_SURRENDER_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_SURRENDER_RSP stSurrenderRsp
        {
            get
            {
                return (this.dataObject as SCPKG_SURRENDER_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_SINGLEGAMESWEEPREQ stSweepSingleGameReq
        {
            get
            {
                return (this.dataObject as CSPKG_SINGLEGAMESWEEPREQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_SINGLEGAMESWEEPRSP stSweepSingleGameRsp
        {
            get
            {
                return (this.dataObject as SCPKG_SINGLEGAMESWEEPRSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_SYMBOL_BREAK stSymbolBreak
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_SYMBOL_BREAK);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_SYMBOL_BREAK stSymbolBreakRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_SYMBOL_BREAK);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_SYMBOLCHG stSymbolChg
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_SYMBOLCHG);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_SYMBOLCOMP stSymbolCompReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_SYMBOLCOMP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_SYMBOLCOMP stSymbolCompRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_SYMBOLCOMP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_SYMBOLDETAIL stSymbolDetail
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_SYMBOLDETAIL);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_SYMBOL_MAKE stSymbolMake
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_SYMBOL_MAKE);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_SYMBOL_MAKE stSymbolMakeRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_SYMBOL_MAKE);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_SYMBOLNAMECHG stSymbolNameChgReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_SYMBOLNAMECHG);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_SYMBOLNAMECHG stSymbolNameChgRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_SYMBOLNAMECHG);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_SYMBOLOFF stSymbolOff
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_SYMBOLOFF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_SYMBOLPAGESEL stSymbolPageChgReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_SYMBOLPAGESEL);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_SYMBOLPAGESEL stSymbolPageChgRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_SYMBOLPAGESEL);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_SYMBOLPAGE_CLR stSymbolPageClrReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_SYMBOLPAGE_CLR);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_SYMBOLPAGE_CLR stSymbolPageClrRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_SYMBOLPAGE_CLR);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_SYMBOLQUERY stSymbolQuery
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_SYMBOLQUERY);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_SYMBOLWEAR stSymbolWear
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_SYMBOLWEAR);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_NTF_RANDDRAW_SYNID stSyncRandDraw
        {
            get
            {
                return (this.dataObject as SCPKG_NTF_RANDDRAW_SYNID);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_TALENT_BUY stTalentBuyReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_TALENT_BUY);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_TALENT_BUY stTalentBuyRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_TALENT_BUY);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_TASKUPD_NTF stTaskUdpNtf
        {
            get
            {
                return (this.dataObject as SCPKG_TASKUPD_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_TEAM_CHG stTeamChgNtf
        {
            get
            {
                return (this.dataObject as SCPKG_TEAM_CHG);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_CMD_SKILLUNLOCK_SEL stUnlockSkillSelReq
        {
            get
            {
                return (this.dataObject as CSPKG_CMD_SKILLUNLOCK_SEL);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_CMD_SKILLUNLOCK_SEL stUnlockSkillSelRsp
        {
            get
            {
                return (this.dataObject as SCPKG_CMD_SKILLUNLOCK_SEL);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_UPDATECLIENTBITS_NTF stUpdateClientBitsNtf
        {
            get
            {
                return (this.dataObject as CSPKG_UPDATECLIENTBITS_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_UPDRANKINFO_NTF stUpdateRankInfo
        {
            get
            {
                return (this.dataObject as SCPKG_UPDRANKINFO_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_UPDNEWCLIENTBITS_NTF stUpdNewClientBits
        {
            get
            {
                return (this.dataObject as CSPKG_UPDNEWCLIENTBITS_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_UPGRADE_GUILD_BY_COUPONS_REQ stUpgradeGuildByCouponsReq
        {
            get
            {
                return (this.dataObject as CSPKG_UPGRADE_GUILD_BY_COUPONS_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_UPGRADE_GUILD_BY_COUPONS_RSP stUpgradeGuildByCouponsRsp
        {
            get
            {
                return (this.dataObject as SCPKG_UPGRADE_GUILD_BY_COUPONS_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_UPGRADESTAR_REQ stUpgradeStarReq
        {
            get
            {
                return (this.dataObject as CSPKG_UPGRADESTAR_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_UPGRADESTAR_RSP stUpgradeStarRsp
        {
            get
            {
                return (this.dataObject as SCPKG_UPGRADESTAR_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_UPHEROLVL_REQ stUpHeroLvlReq
        {
            get
            {
                return (this.dataObject as CSPKG_UPHEROLVL_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_UPHEROLVL_RSP stUpHeroLvlRsp
        {
            get
            {
                return (this.dataObject as SCPKG_UPHEROLVL_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_USEEXPCARD_NTF stUseExpCardNtf
        {
            get
            {
                return (this.dataObject as SCPKG_USEEXPCARD_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_USER_COMPLAINT_REQ stUserComplaintReq
        {
            get
            {
                return (this.dataObject as CSPKG_USER_COMPLAINT_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_USER_COMPLAINT_RSP stUserComplaintRsp
        {
            get
            {
                return (this.dataObject as SCPKG_USER_COMPLAINT_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_USUALTASKDISCARD_RES stUsualDiscardTaskRes
        {
            get
            {
                return (this.dataObject as SCPKG_USUALTASKDISCARD_RES);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_USUALTASK_REQ stUsualTaskReq
        {
            get
            {
                return (this.dataObject as CSPKG_USUALTASK_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_USUALTASK_RES stUsualTaskRes
        {
            get
            {
                return (this.dataObject as SCPKG_USUALTASK_RES);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_WEAL_CON_DATA_NTF stWealConDataNtf
        {
            get
            {
                return (this.dataObject as SCPKG_WEAL_CON_DATA_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_WEAL_DATA_NTF stWealDataNtf
        {
            get
            {
                return (this.dataObject as SCPKG_WEAL_DATA_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_WEAL_DATA_REQ stWealDataReq
        {
            get
            {
                return (this.dataObject as CSPKG_WEAL_DATA_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_WEALDETAIL_NTF stWealDetailNtf
        {
            get
            {
                return (this.dataObject as SCPKG_WEALDETAIL_NTF);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_WEAL_EXCHANGE_RES stWealExchangeRes
        {
            get
            {
                return (this.dataObject as SCPKG_WEAL_EXCHANGE_RES);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public CSPKG_WEARHEROSKIN_REQ stWearHeroSkinReq
        {
            get
            {
                return (this.dataObject as CSPKG_WEARHEROSKIN_REQ);
            }
            set
            {
                this.dataObject = value;
            }
        }

        public SCPKG_WEARHEROSKIN_RSP stWearHeroSkinRsp
        {
            get
            {
                return (this.dataObject as SCPKG_WEARHEROSKIN_RSP);
            }
            set
            {
                this.dataObject = value;
            }
        }
    }
}

