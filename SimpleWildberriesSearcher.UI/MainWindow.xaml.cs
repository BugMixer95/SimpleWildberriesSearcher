using SimpleWildberriesSearcher.Core.Services.ExportService;
using SimpleWildberriesSearcher.Core.Services.SearchService;
using SimpleWildberriesSearcher.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SimpleWildberriesSearcher.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ISearchService _searchService;
        private readonly IExportService _exportService;

        public MainWindow(ISearchService searchService, IExportService exportService)
        {
            _searchService = searchService;
            _exportService = exportService;

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

        /// <summary>
        /// Enables or disables button controls depending on provided value.
        /// </summary>
        private void ToggleButtons(bool buttonState)
        {
            this.BtnOpenCategoriesFile.IsEnabled = buttonState;
            this.BtnOpenOutputFolder.IsEnabled = buttonState;
            this.BtnExport.IsEnabled = buttonState;
        }
        #endregion

        #region Event Handlers
        private void BtnOpenCategoriesFile_Click(object sender, RoutedEventArgs e)
        {
            using (OpenFileDialog dialog = new() { Filter = "Text files (*.txt)|*.txt" })
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
                {
                    var textBlock = dialog.SelectedPath;

                    int lastIndex = textBlock.Length - 1;
                    if (textBlock[lastIndex] != System.IO.Path.DirectorySeparatorChar)
                        textBlock += System.IO.Path.DirectorySeparatorChar;

                    textBlock += "SearchOutput.xlsx";

                    this.TxtOutputFolder.Text = textBlock;
                }
            }
        }

        private void InputTextBoxes_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool areTextBoxesFilled = this.TxtCategoriesFile.Text.Trim().Length > 0 && this.TxtOutputFolder.Text.Trim().Length > 0;
            this.BtnExport.IsEnabled = areTextBoxesFilled;
        }

        private async void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            ToggleButtons(false);
            this.LblProcessState.Foreground = Brushes.Blue;
            this.LblProcessState.Content = "Loading cards from Wildberries...";

            try
            {
                // reading file
                IEnumerable<string> parsedCategories = FileReaderHelper.ReadFile(this.TxtCategoriesFile.Text);

                // getting cards
                var cards = await _searchService.SearchAsync(parsedCategories);

                this.LblProcessState.Content = "Exporting to Excel...";

                // exporting to Excel
                IExportResult exportResult = await _exportService.ExportAsync(this.TxtOutputFolder.Text, cards);

                _ = exportResult.StatusCode switch
                {
                    ExportStatusCode.Done => this.LblProcessState.Content = "Export done successfully!",
                    ExportStatusCode.DoneWithNuances => this.LblProcessState.Content = exportResult.Nuance,
                    ExportStatusCode.Failed => throw exportResult.Exception
                };
            }
            catch (Exception ex)
            {
                this.LblProcessState.Foreground = Brushes.Red;
                this.LblProcessState.Content = string.Format(
                    "{0}. {1}",
                    ex.Message,
                    ex.InnerException?.Message);
            }
            finally
            {
                ToggleButtons(true);
            }
        }
        #endregion
    }
}
