using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace GroupMaker
{
    /// <summary>
    /// Interaction logic for GroupPage.xaml
    /// </summary>
    public partial class GroupPage : Page
    {
        private List<StudentGroup> studentGroups;
        private Properties properties;

        public GroupPage(List<StudentGroup> studentGroups, Properties properties)
        {
            this.studentGroups = studentGroups;
            this.properties = properties;
            InitializeComponent();
            fillTreeView();
        }

        private void fillTreeView()
        {
            foreach (StudentGroup group in this.studentGroups)
            {
                TreeViewItem groupItem = new TreeViewItem();
                groupItem.Header = group.ToString();
                groupItem.IsExpanded = true;
                this.groupsTreeView.Items.Add(groupItem);

                foreach (Student member in group.getMembers())
                {
                    TreeViewItem memberItem = new TreeViewItem();
                    memberItem.Header = member.ToString();
                    groupItem.Items.Add(memberItem);
                }
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
