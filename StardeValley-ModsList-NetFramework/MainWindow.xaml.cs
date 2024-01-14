using StardewValleyModList.Pages;
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

namespace StardewValleyModList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MyModsPage myModsPage = new MyModsPage();
        OtherPlayerModsPage otherPlayerModsPage = new OtherPlayerModsPage();

        public MainWindow()
        {
            InitializeComponent();
            OpenMyModsPage();
        }

        private void OpenMyModsPage()
        {
            MainContent.Content = myModsPage;
        }

        private void OpenOtherPage()
        {
            MainContent.Content = otherPlayerModsPage;
        }
    }
}
