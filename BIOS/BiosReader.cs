using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VegaBiosEditor
{
    public class BiosReader
    {

        public string Filepath { get; private set; }
        public int FSLength { get; private set; }
        public bool FSLength_Safe { get; private set; }

        public byte[] buffer;


        //Some static info
        private string[] SupportedDeviceID = new string[] { "67DF", "1002" };
        private int[] SupportedRomLength = new int[] { 524288, 262144 };
        public  int ATOM_ROM_HEADER_ptr = 0x48;

        public BiosStruct.ATOM_ROM_DEVICEID atom_rom_deviceId;
        public BiosStruct.ATOM_ROM_HEADER atom_rom_header;
        public BiosStruct.ATOM_DATA_TABLES atom_data_table;
        public BiosStruct.ATOM_POWERPLAY_TABLE atom_powerplay_table;
        public BiosStruct.ATOM_POWERTUNE_TABLE atom_powertune_table;
        public BiosStruct.ATOM_FAN_TABLE atom_fan_table;
        public BiosStruct.ATOM_SCLK_TABLE atom_sclk_table;
        public BiosStruct.ATOM_SCLK_ENTRY[] atom_sclk_entries;

        public BiosReader(string filepath)
        {
            FileStream fs = new FileStream(filepath, FileMode.Open);
            //Get Bios Length

            this.Filepath = filepath;
            this.FSLength = (int)fs.Length;
            using (BinaryReader br = new BinaryReader(fs))
            {
                buffer = br.ReadBytes(FSLength);
                
                atom_rom_deviceId = Util.FromBytes<BiosStruct.ATOM_ROM_DEVICEID>(buffer.Skip(Util.GetValueAtPosition(buffer, 16, BiosStruct.rom_deviceId_pointer)).ToArray());
                atom_rom_header = Util.FromBytes<BiosStruct.ATOM_ROM_HEADER>(buffer.Skip(Util.GetValueAtPosition(buffer, 16, BiosStruct.rom_header_pointer)).ToArray());
                atom_data_table = Util.FromBytes<BiosStruct.ATOM_DATA_TABLES>(buffer.Skip(atom_rom_header.usMasterDataTableOffset).ToArray());

                atom_powerplay_table = Util.FromBytes<BiosStruct.ATOM_POWERPLAY_TABLE>(buffer.Skip(atom_data_table.PowerPlayInfo).ToArray());
                atom_powertune_table = Util.FromBytes<BiosStruct.ATOM_POWERTUNE_TABLE>(buffer.Skip(atom_data_table.PowerPlayInfo + atom_powerplay_table.usPowerTuneTableOffset).ToArray());
                atom_fan_table = Util.FromBytes<BiosStruct.ATOM_FAN_TABLE>(buffer.Skip(atom_data_table.PowerPlayInfo + atom_powerplay_table.usFanTableOffset).ToArray());
                atom_sclk_table = Util.FromBytes<BiosStruct.ATOM_SCLK_TABLE>(buffer.Skip(atom_data_table.PowerPlayInfo + atom_powerplay_table.usGfxclkDependencyTableOffset).ToArray());
                atom_sclk_entries = new BiosStruct.ATOM_SCLK_ENTRY[atom_sclk_table.ucNumEntries];
                for (var i = 0; i < atom_sclk_entries.Length; i++)
                {
                    atom_sclk_entries[i] = Util.FromBytes<BiosStruct.ATOM_SCLK_ENTRY>(buffer.Skip(atom_data_table.PowerPlayInfo + atom_powerplay_table.usGfxclkDependencyTableOffset + Marshal.SizeOf(typeof(BiosStruct.ATOM_SCLK_TABLE)) + Marshal.SizeOf(typeof(BiosStruct.ATOM_SCLK_ENTRY)) * i).ToArray());
                }
                br.Close();
            }
        }

    }
}
