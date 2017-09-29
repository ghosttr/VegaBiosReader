using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VegaBiosEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BiosReader reader;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void OpenFileDialog_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "BIOS (.rom)|*.rom|All Files (*.*)|*.*",
                FilterIndex = 1,
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == true)
            {
                //fileName = openFileDialog.SafeFileName;
                reader = new BiosReader(openFileDialog.FileName);

                Display_TableRom();
                Display_TablePowerPlay();
                Display_DebugText();
                Display_TablePowerTune();
                Display_TableFan();
                Display_TableGPU();



            }
        }

        private void Display_TableRom()
        {
            tableROM.Items.Clear();
            tableROM.Items.Add(new
            {
                NAME = "VendorID",
                VALUE = "0x" + reader.atom_rom_deviceId.usVendorID.ToString("X")
            });
            tableROM.Items.Add(new
            {
                NAME = "DeviceID",
                VALUE = "0x" + reader.atom_rom_deviceId.usDeviceID.ToString("X")
            });
            tableROM.Items.Add(new
            {
                NAME = "Sub ID",
                VALUE = "0x" + reader.atom_rom_header.usSubsystemID.ToString("X")
            });
            tableROM.Items.Add(new
            {
                NAME = "Sub VendorID",
                VALUE = "0x" + reader.atom_rom_header.usSubsystemVendorID.ToString("X")
            });
            tableROM.Items.Add(new
            {
                NAME = "Firmware Signature",
                VALUE = "0x" + reader.atom_rom_header.uaFirmWareSignature.ToString("X")
            });
        }
        private void Display_TablePowerPlay()
        {
            tablePOWERPLAY.Items.Clear();
            tablePOWERPLAY.Items.Add(new
            {
                NAME = "Max GPU Freq. (MHz)",
                VALUE = reader.atom_powerplay_table.ulMaxODEngineClock / 100
            });
            tablePOWERPLAY.Items.Add(new
            {
                NAME = "Max Memory Freq. (MHz)",
                VALUE = reader.atom_powerplay_table.ulMaxODMemoryClock / 100
            });
            tablePOWERPLAY.Items.Add(new
            {
                NAME = "Power Control Limit (%)",
                VALUE = reader.atom_powerplay_table.usPowerControlLimit
            });
        }
        private void Display_TablePowerTune()
        {
            tablePOWERTUNE.Items.Clear();
            tablePOWERTUNE.Items.Add(new
            {
                NAME = "SocketPowerLimit (W)",
                VALUE = reader.atom_powertune_table.usSocketPowerLimit
            });
            tablePOWERTUNE.Items.Add(new
            {
                NAME = "BatteryPowerLimit (W)",
                VALUE = reader.atom_powertune_table.usBatteryPowerLimit
            });
            tablePOWERTUNE.Items.Add(new
            {
                NAME = "SmallPowerLimit (W)",
                VALUE = reader.atom_powertune_table.usSmallPowerLimit
            });
            tablePOWERTUNE.Items.Add(new
            {
                NAME = "TDC Limit (A)",
                VALUE = reader.atom_powertune_table.usTdcLimit
            });
            tablePOWERTUNE.Items.Add(new
            {
                NAME = "EDC Limit (A)",
                VALUE = reader.atom_powertune_table.usEdcLimit
            });
            tablePOWERTUNE.Items.Add(new
            {
                NAME = "Software Shutdown Temp. (C)",
                VALUE = reader.atom_powertune_table.usSoftwareShutdownTemp
            });
        }
        private void Display_TableFan()
        {
            tableFAN.Items.Clear();
            
            tableFAN.Items.Add(new
            {
                NAME = "Sensitivity",
                VALUE = reader.atom_fan_table.usFanOutputSensitivity
            });
            
        }
        private void Display_TableGPU()
        {
            tableGPU.Items.Clear();
            for (var i = 0; i < reader.atom_sclk_table.ucNumEntries; i++)
            {
                tableGPU.Items.Add(new
                {
                    MHZ = reader.atom_sclk_entries[i].ulSclk / 100,
                    MV = "test"//reader.atom_vddc_entries[atom_sclk_entries[i].ucVddInd].usVdd
                });
            }

        }
        private void Display_DebugText()
        {
            DebugText.Clear();
            //BiosStruct.ATOM_COMMON_TABLE_HEADER temp_hdr = reader.atom_rom_header.sHeader;
            DebugText.Text += "ATOM_ROM_HEADER" + Environment.NewLine;
            DebugText.Text += Tab + "ATOM_COMMON_TABLE_HEADER" + Environment.NewLine;
            DebugText.Text += Tab + Tab + "ucTableContentRevision" + Tab +  reader.atom_rom_header.sHeader.ucTableContentRevision + Environment.NewLine;
            DebugText.Text += Tab + Tab + "ucTableFormatRevision" + Tab + reader.atom_rom_header.sHeader.ucTableFormatRevision + Environment.NewLine;
            DebugText.Text += Tab + Tab + "usStructureSize" + Tab + reader.atom_rom_header.sHeader.usStructureSize + Environment.NewLine;
            DebugText.Text += "ATOM_ROM_HEADER.usMasterDataTableOffset" + Tab + reader.atom_rom_header.usMasterDataTableOffset.ToString("X4") + Environment.NewLine;
            DebugText.Text += "ATOM_POWERPLAY_TABLE Loc:" + Tab + reader.atom_data_table.PowerPlayInfo.ToString("X4") + Environment.NewLine;
            DebugText.Text += "ATOM_POWERPLAY_TABLE ucTableContentRevision:" + Tab + reader.atom_powerplay_table.sHeader.usStructureSize.ToString("X4") + Environment.NewLine;
            DebugText.Text += "fan table" + (reader.atom_data_table.PowerPlayInfo + reader.atom_powerplay_table.usFanTableOffset).ToString("X4") + Environment.NewLine;
            DebugText.Text += "sclk table" + (reader.atom_data_table.PowerPlayInfo + reader.atom_powerplay_table.usGfxclkDependencyTableOffset).ToString("X4") + Environment.NewLine;

        }
        public static string Tab = "     ";
        
    }
}
