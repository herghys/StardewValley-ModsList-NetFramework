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

namespace StardewValleyModList.Pages
{
    /// <summary>
    /// Interaction logic for MyModsPage.xaml
    /// </summary>
    public partial class MyModsPage : Page
    {
        public List<ModsDataModel> ModsData { get; private set; }
        public MyModsPage()
        {
            InitializeComponent();
        }

        #region Event Listeners
        private void button_SelectGamePath_Click(object sender, RoutedEventArgs e)
        {
            Globals.StardewDirectory = SelectStardewFolderPath();
            Globals.StardewModsDirectory = Path.Combine(Globals.StardewDirectory, AppContants.ModsFolder);

            if (!Directory.Exists(Globals.StardewModsDirectory))
                return;

            GetMods();
        }

        private void button_ExportModData_Click(object sender, RoutedEventArgs e)
        {
            var json = JsonConvert.SerializeObject(ModsData, Formatting.Indented);
            SaveFileDialog sfd = new SaveFileDialog()
            {
                FileName = "My Stardew Mods",
                RestoreDirectory = false,
                CheckPathExists = true,
                DefaultExt = "modslist",
                Filter = "Modslist File (*modslist)|*.modslist",
                Title = "Save Mods list",
                OverwritePrompt = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            DialogResult result = sfd.ShowDialog();
            if (result == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, json);
            }
        }
        #endregion

        private string SelectStardewFolderPath()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog()
            {
                RootFolder = Environment.SpecialFolder.MyComputer,
                Description = "Select Stardew Valley Directory",
            };

            folderBrowserDialog.ShowDialog();
            return folderBrowserDialog.SelectedPath;
        }

        private async void GetMods()
        {
            textbox_GamePath.Text = Globals.StardewModsDirectory;

            if (File.Exists(Path.Combine(Globals.StardewDirectory, "StardewModdingAPI.dll")))
                checkbox_SMAPIExist.IsChecked = true;

            checkbox_ModsFolderExist.IsChecked = true;
            var baseModsData = ModGetter.SearchMods(Globals.StardewModsDirectory);

            ModsData = baseModsData.ConvertAll(data => (ModsDataModel)data);
            Globals.MyMods = ModsData;

            foreach (var item in ModsData)
            {
                datagrid_MyMods.Items.Add(item);
                await Task.Yield();
            }

            GlobalEvents.OnListPopulated?.Invoke();
        }
    }
}
