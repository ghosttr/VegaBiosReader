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
using System.Windows.Shapes;

namespace VegaBiosEditor
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            Populate();
        }

        private void Populate()
        {
            ToggleNag.IsChecked = Properties.Settings.Default.ShowNag;
            ToggleFriendlyTableName.IsChecked = Properties.Settings.Default.ShowFriendlyTableName;
            ToggleFriendlyRowName.IsChecked = Properties.Settings.Default.ShowFriendlyRowName;
            ToggleShowRawValue.IsChecked = Properties.Settings.Default.ShowRawValues;
        }

        private void SaveCloseSetting_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ShowNag = ToggleNag.IsChecked.Value;
            Properties.Settings.Default.ShowFriendlyTableName = ToggleFriendlyTableName.IsChecked.Value;
            Properties.Settings.Default.ShowFriendlyRowName = ToggleFriendlyRowName.IsChecked.Value;
            Properties.Settings.Default.ShowRawValues = ToggleShowRawValue.IsChecked.Value;
            this.Close();
        }
    }
}
