using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VegaBiosEditor
{
    public class BiosStruct
    {
        public static int rom_header_pointer = 0x48;
        public static int rom_deviceId_pointer = 0x18;

        //Was as end of ROM_HEADER, use rom_deviceId_pointer instead
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public unsafe struct ATOM_ROM_DEVICEID
        {
            public UInt32 PSPDirTableOffset;
            public UInt16 VendorID;
            public UInt16 DeviceID;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public unsafe struct ATOM_COMMON_TABLE_HEADER
        {
            public Int16 StructureSize;
            public Byte TableFormatRevision;
            public Byte TableContentRevision;
        }

        //Uint32 padded at end to make correct
        [Serializable][StructLayout(LayoutKind.Sequential, Pack = 1)]
        public unsafe struct ATOM_ROM_HEADER
        {
            public ATOM_COMMON_TABLE_HEADER Header;
            public UInt32 FirmWareSignature;
            public UInt16 BiosRuntimeSegmentAddress;
            public UInt16 ProtectedModeInfoOffset;
            public UInt16 ConfigFilenameOffset;
            public UInt16 CRC_BlockOffset;
            public UInt16 BIOS_BootupMessageOffset;
            public UInt16 Int10Offset;
            public UInt16 PciBusDevInitCode;
            public UInt16 IoBaseAddress;
            public UInt16 SubsystemVendorID;
            public UInt16 SubsystemID;
            public UInt16 PCI_InfoOffset;
            public UInt16 MasterCommandTableOffset;
            public UInt16 MasterDataTableOffset;
            public Byte ExtendedFunctionCode;
            public Byte Reserved;
            public UInt32 UnknownBytes;
        }

        //Same as Polaris, Etc
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public unsafe struct ATOM_DATA_TABLES
        {
            public ATOM_COMMON_TABLE_HEADER Header;
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

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public unsafe struct ATOM_POWERPLAY_TABLE
        {
            public ATOM_COMMON_TABLE_HEADER Header;
            public Byte TableRevision;
            public UInt16 TableSize;
            public UInt32 GoldenPPID;
            public UInt32 GoldenRevision;
            public UInt16 FormatID;
            public UInt32 PlatformCaps;
            public UInt32 MaxODEngineClock;
            public UInt32 MaxODMemoryClock;
            public UInt16 PowerControlLimit;
            public UInt16 UlvVoltageOffset;
            public UInt16 UlvSmnclkDid;
            public UInt16 UlvMp1clkDid;
            public UInt16 UlvGfxclkBypass;
            public UInt16 GfxclkSlewRate;
            public Byte GfxVoltageMode;
            public Byte SocVoltageMode;
            public Byte UclkVoltageMode;
            public Byte UvdVoltageMode;
            public Byte VceVoltageMode;
            public Byte Mp0VoltageMode;
            public Byte DcefVoltageMode;
            public UInt16 StateArrayOffset;
            public UInt16 FanTableOffset;
            public UInt16 ThermalControllerOffset;
            public UInt16 SocclkDependencyTableOffset;
            public UInt16 MclkDependencyTableOffset;
            public UInt16 GfxclkDependencyTableOffset;
            public UInt16 DcefclkDependencyTableOffset;
            public UInt16 VddcLookupTableOffset;
            public UInt16 VddmemLookupTableOffset;
            public UInt16 MMDependencyTableOffset;
            public UInt16 VCEStateTableOffset;
            public UInt16 Reserv;
            public UInt16 PowerTuneTableOffset;
            public UInt16 HardLimitTableOffset;
            public UInt16 VddciLookupTableOffset;
            public UInt16 PCIETableOffset;
            public UInt16 PixclkDependencyTableOffset;
            public UInt16 DispClkDependencyTableOffset;
            public UInt16 PhyClkDependencyTableOffset;
        };

        //http://www.overclock.net/t/1633446/preliminary-view-of-amd-vega-bios
        //seems to be 2 versions, maybe take a look for some identifier?
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public unsafe struct ATOM_POWERTUNE_TABLE
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

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public unsafe struct ATOM_FAN_TABLE
        {
            public Byte ucRevId;
            public UInt16 usFanOutputSensitivity;
            public UInt16 usFanRPMMax;
            public UInt16 usThrottlingRPM;
            public UInt16 usFanAcousticLimit;
            public UInt16 usTargetTemperature;
            public UInt16 usMinimumPWMLimit;
            public UInt16 usTargetGfxClk;
            public UInt16 usFanGainEdge;
            public UInt16 usFanGainHotspot;
            public UInt16 usFanGainLiquid;
            public UInt16 usFanGainVrVddc;
            public UInt16 usFanGainVrMvdd;
            public UInt16 usFanGainPlx;
            public UInt16 usFanGainHbm;
            public Byte ucEnableZeroRPM;
            public UInt16 usFanStopTemperature;
            public UInt16 usFanStartTemperature;
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public unsafe struct ATOM_SCLK_TABLE
        {
            public Byte ucRevId;
            public Byte ucNumEntries;
        };

        //Need sleep >.>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public unsafe struct ATOM_SCLK_ENTRY
        {
            public UInt32 ulSclk;
            public Byte usEdcCurrent;
            public UInt16 ucCKSVOffsetandDisable;
            public UInt16 ulSclkOffset;
            public UInt32 EXTRABYTES;
        };

    }
}
