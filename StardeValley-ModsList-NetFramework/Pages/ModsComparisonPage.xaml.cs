using StardewValleyModList.DataModels;
using StardewValleyModList.Events;
using StardewValleyModList.Helper;
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

namespace StardewValleyModList.Pages
{
    /// <summary>
    /// Interaction logic for ModsComparisonPage.xaml
    /// </summary>
    public partial class ModsComparisonPage : Page
    {
        List<ModsDataModel> comparedData = new List<ModsDataModel>();
        public ModsComparisonPage()
        {
            InitializeComponent();
        }

        private void EventSubs()
        {
            GlobalEvents.OnListPopulated += OnListPopulated;
        }

        private void OnListPopulated()
        {
            Populate();
        }

        public void Populate()
        {
            comparedData.Clear();
            comparedData = new List<ModsDataModel>();
            List<ModsDataModel> myData = new List<ModsDataModel>();
            List<ModsDataModel> otherPlayerData = new List<ModsDataModel>();

            if (Globals.MyMods.Count != 0)
            {
                myData.AddRange(Globals.MyMods);

                foreach (var item in myData)
                {
                    if (!comparedData.Exists(x => x.Name == item.Name))
                        comparedData.Add(new ModsDataModel()
                        {
                            Name = item.Name,
                            Author = item.Author,
                            Version = item.Version,
                            Description = item.Description,
                            UniqueID = item.UniqueID,
                            EntryDll = item.EntryDll,
                            MinimumApiVersion = item.MinimumApiVersion,
                            SMAPILink = item.SMAPILink,
                            NexusLink = item.NexusLink,
                            GithubLink = item.GithubLink,
                            Exist = true
                        });
                }
            }

            if (Globals.OtherPlayerMods.Count != 0)
            {
                otherPlayerData.AddRange(Globals.OtherPlayerMods);

                foreach (var item in otherPlayerData)
                {
                    if (!comparedData.Exists(x => x.Name == item.Name))
                    {
                        comparedData.Add(new ModsDataModel()
                        {
                            Name = item.Name,
                            Author = item.Author,
                            Version = item.Version,
                            Description = item.Description,
                            UniqueID = item.UniqueID,
                            EntryDll = item.EntryDll,
                            MinimumApiVersion = item.MinimumApiVersion,
                            SMAPILink = item.SMAPILink,
                            NexusLink = item.NexusLink,
                            GithubLink = item.GithubLink,
                            Exist = false
                        });
                    }
                }
            }

            comparedData.RemoveAll(x => x.Exist);
            datagrid_MyMods.ItemsSource = default;

            for (int i = 0; i < comparedData.Count; i++)
            {
                datagrid_MyMods.Items.Add(comparedData[i]);
            }

            RefreshUI();
        }

        public void RefreshUI()
        {
            for (int i = 0; i < comparedData.Count; i++)
            {
                var row = datagrid_MyMods.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                if (row != null)
                {
                    if (comparedData[i].Exist)
                        row.Background = new SolidColorBrush(Colors.PaleGreen);
                    else
                        row.Background = new SolidColorBrush(Colors.PaleVioletRed);
                }
            }
        }
    }
}
