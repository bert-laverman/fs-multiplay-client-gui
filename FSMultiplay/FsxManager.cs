using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace FSMultiplay
{
    static class FsxManager
    {
        public delegate void CsSimConnectEventHandler(int iEvent);

        [DllImport("MultiPlayDll.dll", EntryPoint = "scmgr_addSimconnectHandler")]
        public static extern void addSimconnectHandler(CsSimConnectEventHandler callback);

        [DllImport("MultiPlayDll.dll", EntryPoint = "scmgr_isConnected")]
        public static extern int scmgr_isConnected();

        [DllImport("MultiPlayDll.dll", EntryPoint = "scmgr_getSimName")]
        private static extern IntPtr scmgr_getSimName();
        [DllImport("MultiPlayDll.dll", EntryPoint = "scmgr_getSimVersionMajor")]
        public static extern int getSimVersionMajor();
        [DllImport("MultiPlayDll.dll", EntryPoint = "scmgr_getSimVersionMinor")]
        public static extern int getSimVersionMinor();
        [DllImport("MultiPlayDll.dll", EntryPoint = "scmgr_getSimBuildMajor")]
        public static extern int getSimBuildMajor();
        [DllImport("MultiPlayDll.dll", EntryPoint = "scmgr_getSimBuildMinor")]
        public static extern int getSimBuildMinor();
        [DllImport("MultiPlayDll.dll", EntryPoint = "scmgr_getLibVersionMajor")]
        public static extern int getLibVersionMajor();
        [DllImport("MultiPlayDll.dll", EntryPoint = "scmgr_getLibVersionMinor")]
        public static extern int getLibVersionMinor();
        [DllImport("MultiPlayDll.dll", EntryPoint = "scmgr_getLibBuildMajor")]
        public static extern int getLibBuildMajor();
        [DllImport("MultiPlayDll.dll", EntryPoint = "scmgr_getLibBuildMinor")]
        public static extern int getLibBuildMinor();

        [DllImport("MultiPlayDll.dll", EntryPoint = "scmgr_getAirfile")]
        private static extern IntPtr scmgr_getAirfile();
        [DllImport("MultiPlayDll.dll", EntryPoint = "scmgr_getTitle")]
        private static extern IntPtr scmgr_getTitle();
        [DllImport("MultiPlayDll.dll", EntryPoint = "scmgr_getAtcType")]
        private static extern IntPtr scmgr_getAtcType();
        [DllImport("MultiPlayDll.dll", EntryPoint = "scmgr_getAtcModel")]
        private static extern IntPtr scmgr_getAtcModel();
        [DllImport("MultiPlayDll.dll", EntryPoint = "scmgr_getAtcId")]
        private static extern IntPtr scmgr_getAtcId();
        [DllImport("MultiPlayDll.dll", EntryPoint = "scmgr_getAtcAirline")]
        private static extern IntPtr scmgr_getAtcAirline();
        [DllImport("MultiPlayDll.dll", EntryPoint = "scmgr_getAtcFlightnumber")]
        private static extern IntPtr scmgr_getAtcFlightnumber();

        /**
         * 
         */
         public static bool isConnected()
        {
            return scmgr_isConnected() != 0;
        }

        /**
         * getSimName()
         * 
         * NOTE: Converts unmanaged string from C++ DLL to C# string
         */
        public static string getSimName()
        {
            return Marshal.PtrToStringAnsi(scmgr_getSimName());
        }

        /**
         * getAirfile()
         * 
         * NOTE: Converts unmanaged string from C++ DLL to C# string
         */
        public static string getAirfile()
        {
            return Marshal.PtrToStringAnsi(scmgr_getAirfile());
        }

        /**
         * getTitle()
         * 
         * NOTE: Converts unmanaged string from C++ DLL to C# string
         */
        public static string getTitle()
        {
            return Marshal.PtrToStringAnsi(scmgr_getTitle());
        }

        /**
         * getAtcType()
         * 
         * NOTE: Converts unmanaged string from C++ DLL to C# string
         */
        public static string getAtcType()
        {
            return Marshal.PtrToStringAnsi(scmgr_getAtcType());
        }

        /**
         * getAtcModel()
         * 
         * NOTE: Converts unmanaged string from C++ DLL to C# string
         */
        public static string getAtcModel()
        {
            return Marshal.PtrToStringAnsi(scmgr_getAtcModel());
        }

        /**
         * getAtcId()
         * 
         * NOTE: Converts unmanaged string from C++ DLL to C# string
         */
        public static string getAtcId()
        {
            return Marshal.PtrToStringAnsi(scmgr_getAtcId());
        }

        /**
         * getAtcAirline()
         * 
         * NOTE: Converts unmanaged string from C++ DLL to C# string
         */
        public static string getAtcAirline()
        {
            return Marshal.PtrToStringAnsi(scmgr_getAtcAirline());
        }

        /**
         * getAtcFlightnumber()
         * 
         * NOTE: Converts unmanaged string from C++ DLL to C# string
         */
        public static string getAtcFlightnumber()
        {
            return Marshal.PtrToStringAnsi(scmgr_getAtcFlightnumber());
        }

    }
}
