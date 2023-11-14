public struct DigitalProductId3
        {
            public UInt32 uiSize;
            public UInt16 MajorVersion;
            public UInt16 MinorVersion;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x24)]
            public byte[] ProductId;
            public UInt32 uiKeyIdx;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x16)]
            public byte[] EditionId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x16)]
            public byte[] bCdKey;
            public UInt32 uiCloneStatus;
            public UInt32 uiTime;
            public UInt32 uiRandom;
            public UInt32 uiLt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] uiLicenseData;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] sOemId;
            public UInt32 uiBundleId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] sHardwareIdStatic;
            public UInt32 uiHardwareIdTypeStatic;
            public UInt32 uiBiosChecksumStatic;
            public UInt32 uiVolSerStatic;
            public UInt32 uiTotalRamStatic;
            public UInt32 uiVideoBiosChecksumStatic;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] sHardwareIdDynamic;
            public UInt32 uiHardwareIdTypeDynamic;
            public UInt32 uiBiosChecksumDynamic;
            public UInt32 uiVolSerDynamic;
            public UInt32 uiTotalRamDynamic;
            public UInt32 uiVideoBiosChecksumDynamic;
            public UInt32 uiCRC32;
        }

        public  struct DigitalProductId4
        {
            public UInt32 uiSize;
            public UInt16 MajorVersion;
            public UInt16 MinorVersion;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst =0x64)]
            public byte[] szAdvancedPid;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 0x64)]
            public byte[] szActivationId;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] szOemID;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 0x260)]
            public byte[] szEditionType;
            public byte bIsUpgrade;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] bReserved;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 0x16)]
            public byte[] bCDKey;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 0x32)]
            public byte[] bCDKey256Hash;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 0x32)]
            public byte[] b256Hash;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 0x64)]
            public byte[] szEditionId;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 0x64)]
            public byte[] szKeyType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 0x64)]
            public byte[] szEULA;
        }

        [DllImport("pidgenx.dll", EntryPoint = "PidGenX2", CharSet = CharSet.Auto)]
        static extern int PidGenX2( string ProductKey,  string PkeyPath,  string pid_start, int unknown1, int unknown2, IntPtr pid, IntPtr s1, IntPtr s2);
        [DllImport("pidgenx.dll", EntryPoint = "GetPKeyData", CharSet = CharSet.Auto)]
        static extern int GetPKeyData(string ProductKey, string PkeyPath, byte[]  Unk, string pid_start, string pwszPKeyAlgorithm, ref uint Unk5, byte[] pOutsize,  byte[] pOutAddr);

        [DllImport("pidgenx.dll",EntryPoint = "PidGenX", CharSet = CharSet.Auto)]
        static extern int PidGenX(string ProductKey, string PkeyPath, string MSPID, int UnknownUsage, IntPtr ProductID, IntPtr DPID3, IntPtr DPID4);



            IntPtr PID = Marshal.AllocHGlobal(0x32);
            IntPtr DPID3 = Marshal.AllocHGlobal(0xA4);
            IntPtr DPID4 = Marshal.AllocHGlobal(0x04F8);
            RetID = PidGenX(ProductKey, PKeyPath, MSPID, 0, PID, DPID3, DPID4);
            if (RetID == 0)
            {

                DigitalProductId3 pid3 = (DigitalProductId3)Marshal.PtrToStructure(DPID3, typeof(DigitalProductId3));
                DigitalProductId4 pid4 = (DigitalProductId4)Marshal.PtrToStructure(DPID4, typeof(DigitalProductId4));
                string szProductId = Encoding.UTF8.GetString(pid3.ProductId,0,0x17);
                string EditionType = Encoding.UTF8.GetString(pid3.ProductId, 0x1C, 8);
                string EditionId = Encoding.UTF8.GetString(pid3.EditionId);
                string szActivationId = Encoding.Unicode.GetString(pid4.szActivationId).Replace("\0", "");
                string szAdvancedPid = Encoding.Unicode.GetString(pid4.szAdvancedPid).Replace("\0", "");
                string szEditionId = Encoding.Unicode.GetString(pid4.szEditionId).Replace("\0", "");
                string szEditionType = Encoding.Unicode.GetString(pid4.szEditionType,0,0x128).Replace("\0", "");
                string szKeyType = Encoding.Unicode.GetString(pid4.szKeyType).Replace("\0", "");
                byte[] bCDKey =pid4.bCDKey;
                byte[] b256Hash =pid4.b256Hash;
                byte[] bCDKey256Hash = pid4.bCDKey256Hash;
                string szEULA = Encoding.Unicode.GetString(pid4.szEULA).Replace("\0", "");
            }
