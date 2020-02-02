using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace GroupMaker
{
    /// <summary>
    /// Interaction logic for MatchPage.xaml
    /// </summary>
    public partial class MatchPage : Page
    {
        private Properties properties;

        public MatchPage(Properties properties)
        {
            this.properties = properties;
            InitializeComponent();
            populateMatchTypeComboBox();
            fillName1ComboBox();
        }

        public void setProperties(Properties properties)
        {
            this.properties = properties;
        }

        private void populateMatchTypeComboBox()
        {
            foreach (object item in Enum.GetValues(typeof(RelationshipTypeEnum)))
            {
                this.matchTypeComboBox.Items.Add(item);
            }
        }

        private bool relationshipListContains(Student student1, RelationshipTypeEnum relType, Student student2)
        {
            foreach (StudentRelationship sr in properties.studentRelationships)
            {
                if (sr.student1.Equals(student1) && sr.relType == relType && sr.student2.Equals(student2))
                    return true;
            }
            return false;
        }

        private void fillName1ComboBox()
        {
            this.name1ComboBox.Items.Clear();
            foreach (Student student in properties.studentList)
            {
                this.name1ComboBox.Items.Add(student);
            }
        }

        private void fillName2ComboBox()
        {
            this.name2ComboBox.Items.Clear();

            Student selectedStudent1 = properties.studentList[this.name1ComboBox.SelectedIndex];

            if (this.matchTypeComboBox.SelectedIndex == 0)
            {
                foreach (Student student in properties.studentList)
                {
                    if (student.getId() != selectedStudent1.getId() && !(relationshipListContains(selectedStudent1, RelationshipTypeEnum.MATCH, student)))
                        this.name2ComboBox.Items.Add(student);
                }
            }
            else
            {
                foreach (Student student in properties.studentList)
                {
                    if (student.getId() != selectedStudent1.getId() && !(relationshipListContains(selectedStudent1, RelationshipTypeEnum.DO_NOT_MATCH, student)))
                        this.name2ComboBox.Items.Add(student);
                }
            }

        }

        private void continueButton_Click(object sender, RoutedEventArgs e)
        {
            GroupGenerator generator = new GroupGenerator(this.properties);
            generator.makeGroups();
            List<StudentGroup> studentGroups = generator.studentGroups;

            this.NavigationService.Navigate(new GroupPage(studentGroups, this.properties));
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void name1ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.name2ComboBox.IsEnabled = true;
            fillName2ComboBox();
        }

        private void name2ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.addRelationshipButton.IsEnabled = true;
        }

        private void updateMatchListBox()
        {
            this.matchListBox.Items.Clear();
            foreach (StudentRelationship sr in properties.studentRelationships)
            {
                this.matchListBox.Items.Add(sr);
            }
        }

        private void addRelationshipButton_Click(object sender, RoutedEventArgs e)
        {
            Student student1 = (Student)(this.name1ComboBox.SelectedItem);
            RelationshipTypeEnum relType = (RelationshipTypeEnum)(this.matchTypeComboBox.SelectedItem);
            Student student2 = (Student)(this.name2ComboBox.SelectedItem);

            if (!(relationshipListContains(student1, relType, student2)))
                properties.studentRelationships.Add(new StudentRelationship(student1, relType, student2));

            updateMatchListBox();
            fillName2ComboBox();
            this.addRelationshipButton.IsEnabled = false;
        }

        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            if (this.matchListBox.SelectedIndex == -1)
            {
                return;
            }

            properties.studentRelationships.Remove((StudentRelationship)(this.matchListBox.SelectedItem));
            updateMatchListBox();
            fillName2ComboBox();
        }
    }
}
