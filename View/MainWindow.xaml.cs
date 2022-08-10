namespace Wpf_MVVM.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ListView? AllDepartments;
        public static ListView? AllPositions;
        public static ListView? AllUsers;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new DataManage();
            AllDepartments = ViewAllDepartments;
            AllPositions = ViewAllPositions;
            AllUsers = ViewAllUsers;
        }
    }
}
