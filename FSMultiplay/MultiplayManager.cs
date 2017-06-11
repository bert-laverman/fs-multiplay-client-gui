using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace FSMultiplay
{
    static class MultiplayManager
    {
        public delegate void CsMultiplayStatusHandler(int iEvent);
        public delegate void CsMultiplayDataChangedHandler();

        [DllImport("MultiPlayDll.dll", EntryPoint = "mpmgr_addMultiplayHandler")]
        public static extern void addStatusHandler(CsMultiplayStatusHandler callback);
        [DllImport("MultiPlayDll.dll", EntryPoint = "mpmgr_addMultiplayDataChangedHandler")]
        public static extern void addDataChangedHandler(CsMultiplayDataChangedHandler callback);

        [DllImport("MultiPlayDll.dll", EntryPoint = "mpmgr_connect")]
        public static extern void  connect();
        [DllImport("MultiPlayDll.dll", EntryPoint = "mpmgr_disconnect")]
        public static extern void  disconnect();
        [DllImport("MultiPlayDll.dll", EntryPoint = "mpmgr_getServerUrl")]
        public static extern IntPtr  mpmgr_getServerUrl();
        [DllImport("MultiPlayDll.dll", EntryPoint = "mpmgr_setServerUrl")]
        public static extern void  setServerUrl([MarshalAs(UnmanagedType.LPStr)]string serverUrl);
        [DllImport("MultiPlayDll.dll", EntryPoint = "mpmgr_getWsServerUrl")]
        public static extern IntPtr  mpmgr_getWsServerUrl();
        [DllImport("MultiPlayDll.dll", EntryPoint = "mpmgr_setWsServerUrl")]
        public static extern void  setWsServerUrl([MarshalAs(UnmanagedType.LPStr)]string wsServerUrl);
        [DllImport("MultiPlayDll.dll", EntryPoint = "mpmgr_getUsername")]
        public static extern IntPtr  mpmgr_getUsername();
        [DllImport("MultiPlayDll.dll", EntryPoint = "mpmgr_setUsername")]
        public static extern void  setUsername([MarshalAs(UnmanagedType.LPStr)]string username);
        [DllImport("MultiPlayDll.dll", EntryPoint = "mpmgr_getPassword")]
        public static extern IntPtr  mpmgr_getPassword();
        [DllImport("MultiPlayDll.dll", EntryPoint = "mpmgr_setPassword")]
        public static extern void  setPassword([MarshalAs(UnmanagedType.LPStr)]string password);
        [DllImport("MultiPlayDll.dll", EntryPoint = "mpmgr_getSession")]
        public static extern IntPtr  mpmgr_getSession();
        [DllImport("MultiPlayDll.dll", EntryPoint = "mpmgr_setSession")]
        public static extern void  setSession([MarshalAs(UnmanagedType.LPStr)]string session);
        [DllImport("MultiPlayDll.dll", EntryPoint = "mpmgr_getCallsign")]
        public static extern IntPtr mpmgr_getCallsign();
        [DllImport("MultiPlayDll.dll", EntryPoint = "mpmgr_setCallsign")]
        public static extern void  setCallsign([MarshalAs(UnmanagedType.LPStr)]string callsign);
        [DllImport("MultiPlayDll.dll", EntryPoint = "mpmgr_getOverrideCallsign")]
        public static extern int mpmgr_getOverrideCallsign();
        [DllImport("MultiPlayDll.dll", EntryPoint = "mpmgr_setOverrideCallsign")]
        public static extern void  setOverrideCallsign(int overrideCallsign);

        public static string getServerUrl()
        {
            return Marshal.PtrToStringAnsi(mpmgr_getServerUrl());
        }

        public static string getWsServerUrl()
        {
            return Marshal.PtrToStringAnsi(mpmgr_getWsServerUrl());
        }

        public static string getUsername()
        {
            return Marshal.PtrToStringAnsi(mpmgr_getUsername());
        }

        public static string getPassword()
        {
            return Marshal.PtrToStringAnsi(mpmgr_getPassword());
        }

        public static string getSession()
        {
            return Marshal.PtrToStringAnsi(mpmgr_getSession());
        }

        public static bool isOverrideCallsign()
        {
            return mpmgr_getOverrideCallsign() != 0;
        }

        public static string getCallsign()
        {
            return Marshal.PtrToStringAnsi(mpmgr_getCallsign());
        }
    }
}
