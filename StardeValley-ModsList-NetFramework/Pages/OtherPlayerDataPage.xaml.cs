using Newtonsoft.Json;
using StardewValleyModList.DataModels;
using StardewValleyModList.Events;
using StardewValleyModList.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StardewValleyModList.Pages
{
    /// <summary>
    /// Interaction logic for OtherPlayerDataPage.xaml
    /// </summary>
    public partial class OtherPlayerModsPage : Page
    {
        public OtherPlayerModsPage()
        {
            InitializeComponent();
        }

        private void button_SelectOtherPlayerDataPath_Click(object sender, RoutedEventArgs e)
        {
            SearchMods();
        }

        private void SearchMods()
        {
            string jsonData = string.Empty;
            OpenFileDialog ofd = new()
            {
                RestoreDirectory = false,
                CheckPathExists = true,
                DefaultExt = "modslist",
                Filter = "Modslist File (*modslist)|*.modslist;*.modslist",
                Title = "Open Mods list",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                jsonData = File.ReadAllText(ofd.FileName);
            }

            if (string.IsNullOrEmpty(jsonData))
                return;

            LoadData(jsonData);
        }

        private void LoadData(string rawData)
        {
            List<ModsDataModel> data = JsonConvert.DeserializeObject<List<ModsDataModel>>(rawData);
            if (data == null && data.Count <= 0)
                return;

            Globals.OtherPlayerMods = data;
            foreach (var item in data)
            {
                datagrid_OtherPlayerData.Items.Add(item);
            }

            GlobalEvents.OnListPopulated?.Invoke();
        }
    }
}
