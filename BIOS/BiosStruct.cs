using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VegeBiosEditor
{
    public class BiosStruct
    {
        //same as polaris, etc
        public static int rom_header_pointer = 0x48;
        //this pointer also exists in polaris, but the data isnt packed at the end of ATOM_ROM_HEADER anymore
        public static int rom_deviceId_pointer = 0x18;

        //Was as end of ROM_HEADER, now (correctly) using pointer
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ATOM_ROM_DEVICEID
        {
            public UInt32 ulPSPDirTableOffset;
            public UInt16 usVendorID;
            public UInt16 usDeviceID;
        }

        //Same as Polaris, Etc
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ATOM_COMMON_TABLE_HEADER
        {
            public Int16 usStructureSize;
            public Byte ucTableFormatRevision;
            public Byte ucTableContentRevision;
        }
        //Uint32 padded at end to make correct
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ATOM_ROM_HEADER
        {
            public ATOM_COMMON_TABLE_HEADER sHeader;
            public UInt32 uaFirmWareSignature;
            public UInt16 usBiosRuntimeSegmentAddress;
            public UInt16 usProtectedModeInfoOffset;
            public UInt16 usConfigFilenameOffset;
            public UInt16 usCRC_BlockOffset;
            public UInt16 usBIOS_BootupMessageOffset;
            public UInt16 usInt10Offset;
            public UInt16 usPciBusDevInitCode;
            public UInt16 usIoBaseAddress;
            public UInt16 usSubsystemVendorID;
            public UInt16 usSubsystemID;
            public UInt16 usPCI_InfoOffset;
            public UInt16 usMasterCommandTableOffset;
            public UInt16 usMasterDataTableOffset;
            public Byte ucExtendedFunctionCode;
            public Byte ucReserved;
            public UInt32 UnknownBytes;
        }

        //Same as Polaris, Etc
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ATOM_DATA_TABLES
        {
            public ATOM_COMMON_TABLE_HEADER sHeader;
            public UInt16 UtilityPipeLine;
            public UInt16 MultimediaCapabilityInfo;
            public UInt16 MultimediaConfigInfo;
            public UInt16 StandardVESA_Timing;
            public UInt16 FirmwareInfo;
            public UInt16 PaletteData;
            public UInt16 LCD_Info;
            public UInt16 DIGTransmitterInfo;
            public UInt16 SMU_Info;
            public UInt16 SupportedDevicesInfo;
            public UInt16 GPIO_I2C_Info;
            public UInt16 VRAM_UsageByFirmware;
            public UInt16 GPIO_Pin_LUT;
            public UInt16 VESA_ToInternalModeLUT;
            public UInt16 GFX_Info;
            public UInt16 PowerPlayInfo;
            public UInt16 GPUVirtualizationInfo;
            public UInt16 SaveRestoreInfo;
            public UInt16 PPLL_SS_Info;
            public UInt16 OemInfo;
            public UInt16 XTMDS_Info;
            public UInt16 MclkSS_Info;
            public UInt16 Object_Header;
            public UInt16 IndirectIOAccess;
            public UInt16 MC_InitParameter;
            public UInt16 ASIC_VDDC_Info;
            public UInt16 ASIC_InternalSS_Info;
            public UInt16 TV_VideoMode;
            public UInt16 VRAM_Info;
            public UInt16 MemoryTrainingInfo;
            public UInt16 IntegratedSystemInfo;
            public UInt16 ASIC_ProfilingInfo;
            public UInt16 VoltageObjectInfo;
            public UInt16 PowerSourceInfo;
            public UInt16 ServiceInfo;
        };

        //http://www.overclock.net/t/1633446/preliminary-view-of-amd-vega-bios
        //Better than tones of unknown bytes lol
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ATOM_POWERPLAY_TABLE
        {
            public ATOM_COMMON_TABLE_HEADER sHeader;
            public Byte ucTableRevision;
            public UInt16 usTableSize;
            public UInt32 ulGoldenPPID;
            public UInt32 ulGoldenRevision;
            public UInt16 usFormatID;
            public UInt32 ulPlatformCaps;
            public UInt32 ulMaxODEngineClock;
            public UInt32 ulMaxODMemoryClock;
            public UInt16 usPowerControlLimit;
            public UInt16 usUlvVoltageOffset;
            public UInt16 usUlvSmnclkDid;
            public UInt16 usUlvMp1clkDid;
            public UInt16 usUlvGfxclkBypass;
            public UInt16 usGfxclkSlewRate;
            public Byte ucGfxVoltageMode;
            public Byte ucSocVoltageMode;
            public Byte ucUclkVoltageMode;
            public Byte ucUvdVoltageMode;
            public Byte ucVceVoltageMode;
            public Byte ucMp0VoltageMode;
            public Byte ucDcefVoltageMode;
            public UInt16 usStateArrayOffset;
            public UInt16 usFanTableOffset;
            public UInt16 usThermalControllerOffset;
            public UInt16 usSocclkDependencyTableOffset;
            public UInt16 usMclkDependencyTableOffset;
            public UInt16 usGfxclkDependencyTableOffset;
            public UInt16 usDcefclkDependencyTableOffset;
            public UInt16 usVddcLookupTableOffset;
            public UInt16 usVddmemLookupTableOffset;
            public UInt16 usMMDependencyTableOffset;
            public UInt16 usVCEStateTableOffset;
            public UInt16 usReserv;
            public UInt16 usPowerTuneTableOffset;
            public UInt16 usHardLimitTableOffset;
            public UInt16 usVddciLookupTableOffset;
            public UInt16 usPCIETableOffset;
            public UInt16 usPixclkDependencyTableOffset;
            public UInt16 usDispClkDependencyTableOffset;
            public UInt16 usPhyClkDependencyTableOffset;
        };

        //http://www.overclock.net/t/1633446/preliminary-view-of-amd-vega-bios
        //seems to be 2 versions, maybe take a look for some identifier?
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ATOM_POWERTUNE_TABLE
        {
            public Byte ucRevId;
            public UInt16 usSocketPowerLimit;
            public UInt16 usBatteryPowerLimit;
            public UInt16 usSmallPowerLimit;
            public UInt16 usTdcLimit;
            public UInt16 usEdcLimit;
            public UInt16 usSoftwareShutdownTemp;
            public UInt16 usTemperatureLimitHotSpot;
            public UInt16 usTemperatureLimitLiquid1;
            public UInt16 usTemperatureLimitLiquid2;
            public UInt16 usTemperatureLimitHBM;
            public UInt16 usTemperatureLimitVrSoc;
            public UInt16 usTemperatureLimitVrMem;
            public UInt16 usTemperatureLimitPlx;
            public UInt16 usLoadLineResistance;
            public Byte ucLiquid1_I2C_address;
            public Byte ucLiquid2_I2C_address;
            //Ver A
            public Byte ucVr_I2C_address;
            public Byte ucPlx_I2C_address;
            public Byte ucLiquid_I2C_LineSCL;
            public Byte ucLiquid_I2C_LineSDA;
            public Byte ucVr_I2C_LineSCL;
            public Byte ucVr_I2C_LineSDA;
            public Byte ucPlx_I2C_LineSCL;
            public Byte ucPlx_I2C_LineSDA;
            //Ver B
            //public Byte ucLiquid_I2C_Line;
            //public Byte ucVr_I2C_address;
            //public Byte ucVr_I2C_Line;
            //public Byte ucPlx_I2C_address;
            //public Byte ucPlx_I2C_Line;
            public UInt16 usTemperatureLimitTedge;
        };

        //well i got 2 things right ¯\_(ツ)_/¯
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ATOM_FAN_TABLE
        {
            public Byte ucRevId;
            public UInt16 usFanOutputSensitivity;

            /*
            public Byte ucTHyst;
            public UInt16 usTMin;
            public UInt16 usTMed;
            public UInt16 usTHigh;
            public UInt16 usPWMMin;
            public UInt16 usPWMMed;
            public UInt16 usPWMHigh;
            public UInt16 usTMax;
            public Byte ucFanControlMode;
            public UInt16 usFanPWMMax;
            public UInt16 usFanRPMMax;
            public UInt32 ulMinFanSCLKAcousticLimit;
            public Byte ucTargetTemperature;
            public Byte ucMinimumPWMLimit;
            public UInt16 usFanGainEdge;
            public UInt16 usFanGainHotspot;
            public UInt16 usFanGainLiquid;
            public UInt16 usFanGainVrVddc;
            public UInt16 usFanGainVrMvdd;
            public UInt16 usFanGainPlx;
            public UInt16 usFanGainHbm;
            public UInt16 usReserved;
            */
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ATOM_SCLK_TABLE
        {
            public Byte ucRevId;
            public Byte ucNumEntries;
        };

        //Need sleep >.>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ATOM_SCLK_ENTRY
        {
            public UInt32 ulSclk;
            public Byte usEdcCurrent;
            public UInt16 ucCKSVOffsetandDisable;
            public UInt16 ulSclkOffset;
            public UInt32 EXTRABYTES;
        };

    }
}
