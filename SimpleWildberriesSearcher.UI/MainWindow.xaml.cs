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

namespace SimpleWildberriesSearcher.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetIcon();
        }

        #region Assistants
        private void SetIcon()
        {
            string iconPath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "Resources\\wb_logo.ico");
            Uri iconUri = new Uri(iconPath, UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
        }
        #endregion

        #region Event Handlers
        private void BtnOpenCategoriesFile_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BtnOpenOutputFolder_Click(object sender, RoutedEventArgs e)
        {

        }
        private void InputTextBoxes_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
