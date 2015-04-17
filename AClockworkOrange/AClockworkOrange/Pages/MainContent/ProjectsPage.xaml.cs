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

using AClockworkOrange.Source.Data;

using FirstFloor.ModernUI.Presentation;

namespace AClockworkOrange.Pages.MainContent
{
    /// <summary>
    /// Interaction logic for ProjectsPage.xaml
    /// </summary>
    public partial class ProjectsPage : UserControl
    {
        public static Config MyConfig;
        public static Project TargetProject;
        public ProjectsPage()
        {
            InitializeComponent();

            Link AddProject = new Link();

            MyConfig = Config.LoadFromFile(Config.DirectoryName + "/" + Config.DefaultConfigFileName + Config.Extension);
            TargetProject = Project.LoadFromFile(MyConfig.TargetProjectFilePath);

            AddProject.DisplayName = TargetProject.ProjectName;
            ProjectsList.Links.Add(AddProject);

        }
    }
}
