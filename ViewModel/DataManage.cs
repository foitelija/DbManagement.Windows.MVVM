
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
                allUsers = value;
                NotifyPropertyChanged("AllUsers");
            }
        }


        private void SetCenterAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }

        #region КОМАНДЫ ДЛЯ ДОБАВЛЕНИЯ
        public string? DepartmentName { get; set; } // свойство для отдела

        //свойства для должности
        public string? PositionName { get; set; }
        public decimal PositionSalary { get; set; }
        public int PositionMaxNumber { get; set; }
        public Department? PostionDepartment { get; set; }

        //свойства для сотрудников
        public string? Username { get; set; }
        public string? Usersurname { get; set; }
        public Position? userPosition { get; set; }

        private RelayCommands addNewDepartment;
        public RelayCommands AddNewDepartment
        {
            get
            {
                return addNewDepartment ?? new RelayCommands(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (DepartmentName == null || DepartmentName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "NameBlock");
                    }
                    else
                    {
                        resultStr = DatabaseCommands.CreateDepartment(DepartmentName);
                        UpdateAllDatas();
                        ShowMessageToUser(resultStr);
                        SetNullValues();
                        wnd.Close();
                    }
                });
            }
        }

        private RelayCommands addNewPosition;
        public RelayCommands AddNewPosition
        {
            get
            {
                return addNewPosition ?? new RelayCommands(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (PositionName == null)
                    {
                        SetRedBlockControll(wnd, "NameBlock");
                    }
                    if(PositionSalary == 0)
                    {
                        SetRedBlockControll(wnd, "SalaryBlock");
                    }
                    else
                    {
                        resultStr = DatabaseCommands.CreatePosition(PositionName, PositionSalary, PositionMaxNumber, PostionDepartment);
                        UpdateAllDatas();
                        ShowMessageToUser(resultStr);
                        SetNullValues();
                        wnd.Close();
                    }
                });
            }
        }

        private RelayCommands addNewUser;
        public RelayCommands AddNewUser
        {
            get
            {
                return addNewUser ?? new RelayCommands(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if(Usersurname == null || Username == null)
                    {
                        SetRedBlockControll(wnd, "UserNameBlock");
                    }
                    else
                    {
                        resultStr = DatabaseCommands.CreateUser(Username, Usersurname, userPosition);
                        UpdateAllDatas();
                        ShowMessageToUser(resultStr);
                        SetNullValues();
                        wnd.Close();
                    }
                });
            }
        }

        #endregion



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


        #region ОБНОВЛЕНИЕ ДАННЫХ

        private void SetNullValues()
        {
            // для пользователя
            Username = null;
            Usersurname = null;
            userPosition = null;
            //для  должности
            PositionName = null;
            PostionDepartment = null;
            PositionSalary = PositionMaxNumber = 0;
            //для отдела
            DepartmentName = null;
        }

        private void UpdateAllDatas()
        {
            UpdateAllDepartments();
            UpdateAllPositions();
            UpdateAllUsers();
        }

        private void UpdateAllDepartments()
        {
            AllDepartments = DatabaseCommands.GetDepartments();
            MainWindow.AllDepartments.ItemsSource = null;
            MainWindow.AllDepartments.Items.Clear();
            MainWindow.AllDepartments.ItemsSource = AllDepartments;
            MainWindow.AllDepartments.Items.Refresh();
        }

        private void UpdateAllPositions()
        {
            AllPositions = DatabaseCommands.GetPositions();
            MainWindow.AllPositions.ItemsSource = null;
            MainWindow.AllPositions.Items.Clear();
            MainWindow.AllPositions.ItemsSource= AllPositions;
            MainWindow.AllPositions.Items.Refresh();
        }

        private void UpdateAllUsers()
        {
            AllUsers = DatabaseCommands.GetUsers();
            MainWindow.AllUsers.ItemsSource = null;
            MainWindow.AllUsers.Items.Clear();
            MainWindow.AllUsers.ItemsSource = AllUsers;
            MainWindow.AllUsers.Items.Refresh();
        }

        #endregion




        private void SetRedBlockControll(Window wnd, string blockName)
        {
            Control block = wnd.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }
        private void ShowMessageToUser(string message)
        {
            MessageBoxView messageBoxView = new MessageBoxView(message);
            SetCenterAndOpen(messageBoxView);
        }

        public void NotifyPropertyChanged(String propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
