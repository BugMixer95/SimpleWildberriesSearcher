using System;
using System.Collections.Generic;
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
        /// <summary>
        /// Sets window icon. Just that simple.
        /// </summary>
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
            using (OpenFileDialog dialog = new() { Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*" })
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    this.TxtCategoriesFile.Text = dialog.FileName;
            }
        }

        private void BtnOpenOutputFolder_Click(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog dialog = new())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    this.TxtOutputFolder.Text = dialog.SelectedPath + System.IO.Path.DirectorySeparatorChar;
            }
        }

        private void InputTextBoxes_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool areTextBoxesFilled = this.TxtCategoriesFile.Text.Trim().Length > 0 && this.TxtOutputFolder.Text.Trim().Length > 0;
            this.BtnExport.IsEnabled = areTextBoxesFilled;
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
