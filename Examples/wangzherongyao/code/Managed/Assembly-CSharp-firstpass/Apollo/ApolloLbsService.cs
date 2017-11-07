﻿namespace Apollo
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class ApolloLbsService : ApolloObject, IApolloLbsService, IApolloServiceBase
    {
        public static readonly ApolloLbsService Instance = new ApolloLbsService();

        public event OnLocationNotifyHandle onLocationEvent;

        private ApolloLbsService()
        {
        }

        [DllImport("MsdkAdapter", CallingConvention=CallingConvention.Cdecl)]
        private static extern bool Apollo_Lbs_CleanLocation(ulong objId);
        [DllImport("MsdkAdapter", CallingConvention=CallingConvention.Cdecl)]
        private static extern void Apollo_Lbs_GetNearbyPersonInfo(ulong objId);
        public bool CleanLocation()
        {
            return Apollo_Lbs_CleanLocation(base.ObjectId);
        }

        public void GetNearbyPersonInfo()
        {
            Apollo_Lbs_GetNearbyPersonInfo(base.ObjectId);
        }

        private void OnLocationNotify(string msg)
        {
            if (msg.Length > 0)
            {
                ApolloStringParser parser = new ApolloStringParser(msg);
                ApolloRelation aRelation = null;
                aRelation = parser.GetObject<ApolloRelation>("Relation");
                if (this.onLocationEvent != null)
                {
                    try
                    {
                        this.onLocationEvent(aRelation);
                    }
                    catch (Exception exception)
                    {
                        ADebug.Log("onLocationEvent:" + exception);
                    }
                }
            }
        }
    }
}

