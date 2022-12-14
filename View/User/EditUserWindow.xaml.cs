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
using System.Windows.Shapes;

namespace Wpf_MVVM.View.User
{
    /// <summary>
    /// Логика взаимодействия для EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        public EditUserWindow(Models.User userToEdit)
        {
            InitializeComponent();
            DataContext = new DataManage();
            DataManage.selectedUser = userToEdit;
            DataManage.Usersurname = userToEdit.Name;
            DataManage.Usersurname = userToEdit.Surname;
            //DataManage.userPosition = userToEdit.Position;
        }
    }
}
