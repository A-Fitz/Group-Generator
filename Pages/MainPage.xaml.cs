using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace GroupMaker
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private Properties properties;

        private List<Student> studentList;
        private SettingsPage settingsPage;

        public MainPage()
        {
            this.properties = new Properties();
            this.studentList = new List<Student>();
            InitializeComponent();
        }

        private void loadStudentListButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv";

            if (openFileDialog.ShowDialog() == true)
            {
                bool success = populateStudentList(openFileDialog.FileName);
                if (success)
                {
                    this.properties.file = openFileDialog.FileName;
                    this.properties.studentList = this.studentList;
                    populateStudentListBox();
                }
                else
                {
                    MessageBox.Show("CSV file has incorrect formatting.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void populateStudentListBox()
        {
            this.studentListBox.Items.Clear();
            foreach (Student student in studentList)
            {
                studentListBox.Items.Add(string.Concat(student.getFirstName(), " ", student.getLastName()));
            }

            this.continueButton.IsEnabled = true;
        }

        private bool populateStudentList(string fileName)
        {
            this.studentList.Clear();
            int id = 0;

            string[] lines = File.ReadAllLines(fileName);
            foreach (string item in lines)
            {
                string[] values = item.Split(',');
                if (values.Length != 2)
                    return false;
                string firstName = values[0];
                string lastName = values[1];
                this.studentList.Add(new Student(firstName, lastName, id++));
            }

            return true;
        }

        private void continueButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.settingsPage == null)
                this.settingsPage = new SettingsPage(this.properties);

            this.settingsPage.setProperties(this.properties);
            this.NavigationService.Navigate(settingsPage);
        }
    }
}
