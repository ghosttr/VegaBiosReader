using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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

                ROM_HEADER.Items.Clear();
                foreach (var field in typeof(BiosStruct.ATOM_ROM_HEADER).GetFields())
                {
                    if (field.Name != "Header")
                    {
                        ROM_HEADER.Items.Add(new
                        {
                            NAME = field.Name,
                            VALUE = Convert.ToInt32(field.GetValue(reader.atom_rom_header)).ToString("X2"),
                            RAWVALUE = Convert.ToInt32(field.GetValue(reader.atom_rom_header))
                        });
                    }
                    else
                    {
                        foreach (var headerItem in typeof(BiosStruct.ATOM_COMMON_TABLE_HEADER).GetFields())
                        {
                            ROM_HEADER.Items.Add(new
                            {
                                NAME = field.Name + headerItem.Name,
                                VALUE = Convert.ToInt32(headerItem.GetValue(reader.atom_rom_header.Header)).ToString("X2"),
                                RAWVALUE = Convert.ToInt32(headerItem.GetValue(reader.atom_rom_header.Header))
                            });
                        }
                    }
                }

                DATA_TABLES.Items.Clear();
                foreach (var field in typeof(BiosStruct.ATOM_DATA_TABLES).GetFields())
                {
                    if (field.Name != "Header")
                    {
                        DATA_TABLES.Items.Add(new
                        {
                            NAME = field.Name,
                            VALUE = Convert.ToInt32(field.GetValue(reader.atom_data_table)).ToString("X2"),
                            RAWVALUE = Convert.ToInt32(field.GetValue(reader.atom_data_table))
                        });
                    }
                    else
                    {
                        foreach (var headerItem in typeof(BiosStruct.ATOM_COMMON_TABLE_HEADER).GetFields())
                        {
                            DATA_TABLES.Items.Add(new
                            {
                                NAME = field.Name + headerItem.Name,
                                VALUE = Convert.ToInt32(headerItem.GetValue(reader.atom_data_table.Header)).ToString("X2"),
                                RAWVALUE = Convert.ToInt32(headerItem.GetValue(reader.atom_data_table.Header))

                            });
                        }
                    }
                }

                POWERPLAY_TABLE.Items.Clear();
                foreach (var field in typeof(BiosStruct.ATOM_POWERPLAY_TABLE).GetFields())
                {
                    if (field.Name != "Header")
                    {
                        POWERPLAY_TABLE.Items.Add(new
                        {
                            NAME = field.Name,
                            VALUE = Convert.ToInt32(field.GetValue(reader.atom_powerplay_table)).ToString("X2"),
                            RAWVALUE = Convert.ToInt32(field.GetValue(reader.atom_powerplay_table))
                        });
                    }
                    else
                    {
                        foreach (var headerItem in typeof(BiosStruct.ATOM_COMMON_TABLE_HEADER).GetFields())
                        {
                            POWERPLAY_TABLE.Items.Add(new
                            {
                                NAME = field.Name + headerItem.Name,
                                VALUE = Convert.ToInt32(headerItem.GetValue(reader.atom_powerplay_table.Header)).ToString("X2"),
                                RAWVALUE = Convert.ToInt32(headerItem.GetValue(reader.atom_powerplay_table.Header))

                            });
                        }
                    }
                }

                FAN_TABLE.Items.Clear();
                foreach (var field in typeof(BiosStruct.ATOM_FAN_TABLE).GetFields())
                {
                
                        FAN_TABLE.Items.Add(new
                        {
                            NAME = field.Name,
                            VALUE = Convert.ToInt32(field.GetValue(reader.atom_fan_table)).ToString("X2"),
                            RAWVALUE = Convert.ToInt32(field.GetValue(reader.atom_fan_table))
                        });
                    
                }



            }
        }

        private void OpenOptionsDialog_Click(object sender, RoutedEventArgs e)
        {

            SettingsWindow settingsWindow = new SettingsWindow();

            settingsWindow.ShowDialog();
            //this.Close();
            //this.Focusable = false;
            //MessageBox.Show(Properties.Settings.Default.ShowNagScreen.ToString());
        }




    }
}
