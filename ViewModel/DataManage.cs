
namespace Wpf_MVVM.ViewModel
{
    public class DataManage : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        //все отделы
        private List<Department> allDepartments = DatabaseCommands.GetDepartments();
        public List<Department> AllDepartments 
        { 
            get { return allDepartments; }
            set
            {
                allDepartments = value;
                NotifyPropertyChanged("AllDepartments");
            }
        }
        //все должности
        private List<Position> allPositions = DatabaseCommands.GetPositions();
        public List<Position> AllPositions
        {
            get { return allPositions; }
            set
            {
                allPositions = value;
                NotifyPropertyChanged("AllPositions");
            }
        }
        //все сотрудники
        private List<User> allUsers = DatabaseCommands.GetUsers();
        public List<User> AllUsers
        {
            get { return allUsers; }
            set
            {
                AllUsers = value;
                NotifyPropertyChanged("AllUsers");
            }
        }


        private void SetCenterAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }

        #region КОМАНДЫ ДЛЯ ОТКРЫТИЯ ОКОН
        private RelayCommands openAddDapartment;
        public RelayCommands OpenAddDepartments
        {
            get
            {
                return openAddDapartment ?? new RelayCommands(obj =>
                {
                    OpenAddDepartmentWin();
                });
            }
        }

        private RelayCommands openAddPosition;
        public RelayCommands OpenAddPosition
        {
            get
            {
                return openAddPosition ?? new RelayCommands(obj =>
                {
                    OpenAddPosWin();
                });
            }
        }

        private RelayCommands openAddUser;
        public RelayCommands OpenAddUser
        {
            get
            {
                return openAddUser ?? new RelayCommands(obj =>
                {
                    OpenAddUserWin();
                });
            }
        }
        #endregion




        #region МЕТОДЫ ДЛЯ ОТКРЫТИЯ ОКОН
        //методы открытий окон для добавления
        private void OpenAddDepartmentWin()
        {
            AddNewDepartmentWindow newDepartmentWindow = new AddNewDepartmentWindow();
            SetCenterAndOpen(newDepartmentWindow);
        }
        private void OpenAddUserWin()
        {
            AddNewUserWindow newUserWindow = new AddNewUserWindow();
            SetCenterAndOpen(newUserWindow);
        }
        private void OpenAddPosWin()
        {
            AddNewPositionWindow newPositionWindow = new AddNewPositionWindow();
            SetCenterAndOpen(newPositionWindow);
        }
        //методы открытий окон для изменения
        private void OpenEditDepartmentWin()
        {
            EditDepartmentWindow  editDepartmentWindow = new EditDepartmentWindow();
            SetCenterAndOpen(editDepartmentWindow);
        }
        private void OpenEditUserWin()
        {
            EditUserWindow editUserWindow = new EditUserWindow();
            SetCenterAndOpen(editUserWindow);
        }
        private void OpenEditPosWin()
        {
            EditPositionWindwo editPositionWindow = new EditPositionWindwo();
            SetCenterAndOpen(editPositionWindow);
        }
        #endregion

        public void NotifyPropertyChanged(String propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
