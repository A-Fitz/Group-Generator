using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GroupMaker
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        private Properties properties;
        private MatchPage matchPage;

        public SettingsPage(Properties properties)
        {
            this.properties = properties;
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public void setProperties(Properties properties)
        {
            this.properties = properties;
        }

        private void continueButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.matchPage == null)
                this.matchPage = new MatchPage(this.properties);

            this.matchPage.setProperties(this.properties);
            this.NavigationService.Navigate(matchPage);

            if (this.groupSizeRadio.IsChecked == true)
            {
                this.properties.groupSize = Int32.Parse(this.groupSizeTextBox.Text);
            }
            else if (this.numberGroupsRadio.IsChecked == true)
            {
                this.properties.numberGroups = Int32.Parse(this.numberGroupsTextBox.Text);
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void groupSizeRadio_Checked(object sender, RoutedEventArgs e)
        {
            this.properties.useGroupSize = true;
        }

        private void numberGroupsRadio_Checked(object sender, RoutedEventArgs e)
        {
            this.properties.useGroupSize = false;
        }
    }
}
