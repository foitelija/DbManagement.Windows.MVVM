namespace Wpf_MVVM.Models
{
    //static в данном случае - мы не будем никак изменять свойства данного класса, тут будут храниться только 
    // все необходимые команды для работы  с БД (CRUD operations)
    public static class DatabaseCommands
    {
        //получить все отделы
        public static List<Department> GetDepartments()
        {
            using(DataContext context = new DataContext())
            {
                var result = context.Departments.ToList();
                return result;
            }
            
        }
        //получить все должности
        public static List<Position> GetPositions()
        {
            using(DataContext context = new DataContext())
            {
                var result = context.Positions.ToList();
                return result;
            }
        }

        //получить всех сотрудников
        public static List<User> GetUsers()
        {
            using (DataContext context = new DataContext())
            {
                var result = context.Users.ToList();
                return result;
            }
        }


        //создать отдел
        public static string CreateDepartment(string name)
        {
            string result = "Такой отдел уже существует.";
            using(DataContext db = new DataContext())
            {
                //существует ли отдел
                bool chek = db.Departments.Any(el => el.Name == name);
                if(!chek)
                {
                    Department department = new Department
                    {
                        Name = name
                    };
                    db.Departments.Add(department);
                    db.SaveChanges();
                    result = "Добавлено!";
                }
            }
            return result;
        }
        //создать должность
        public static string CreatePosition(string name, decimal salary, int maxNumber, Department department)
        {
            string result = "Такая позиция уже существует.";
            using(DataContext db = new DataContext())
            {
                bool chek = db.Positions.Any(el => el.Name == name && el.Salary == salary);
                if(!chek)
                {
                    Position position = new Position
                    {
                        Name = name,
                        Salary = salary,
                        MaxNumber = maxNumber,
                        DepartmentId = department.Id
                    };
                    db.Positions.Add(position);
                    db.SaveChanges();
                    result = "Добавлено!";
                }
            }       
            return result;
         }
        //создать сотрудника
        public static string CreateUser(string name, string surname, Position position)
        {
            string result = "Такой пользователь уже сущетсвует.";
            using(DataContext db = new DataContext())
            {
                bool checkUser = db.Users.Any(u => u.Name == name && u.Surname == surname && u.Position == position);
                if(!checkUser)
                {
                    User user = new User
                    {
                        Name = name,
                        Surname = surname,
                        PositionId = position.Id
                    };
                    db.Users.Add(user);
                    db.SaveChanges();
                    result = "Добавлено!";
                }
            }
            return result;
        }

        //удалить отдел
        public static string DeleteDepartment(Department department)
        {
            string result = "Отдел не найден.";
            using (DataContext context = new DataContext())
            {
                context.Departments.Remove(department);
                context.SaveChanges();
                result = "Сделано " + department.Name + " удалён";
            }
            return result;
        }

        //удалить должность
        public static string DeletePosition(Position position)
        {
            string result = "Позиция не найдена.";
            using (DataContext context = new DataContext())
            {
                context.Positions.Remove(position);
                context.SaveChanges();
                result = "Сделано " + position.Name + " удалён"; 
            }
            return result;
        }
        //удалить сотрудника
        public static string DeleteUser(User user)
        {
            string result = "Пользователь не найден.";
            using (DataContext context = new DataContext())
            {
                context.Users.Remove(user);
                context.SaveChanges();
                result = "Сделано" + user.Name + " удалён";
            }
            return result;
        }

        //изменить отдел
        public static string EditDepartment(Department department)
        {
            string result = "Отдел не существуетю.";
            using(DataContext context = new DataContext())
            {
                Department departmentId = context.Departments.FirstOrDefault(d => d.Id == department.Id);
                departmentId.Name = department.Name;
                context.SaveChanges();
                result = "Отдел" + departmentId.Name + " изменён!";
            }
            return result;
        }
        //изменить должность
        public static string EditPosition(Position newPosition)
        {
            string result = "Должность не существует";
            using(DataContext context = new DataContext())
            {
                Position position = context.Positions.FirstOrDefault(p => p.Id == newPosition.Id);
                position.Name = newPosition.Name;
                position.Salary = newPosition.Salary;
                position.MaxNumber = newPosition.MaxNumber;
                position.DepartmentId = newPosition.DepartmentId;
                context.SaveChanges();
                result = "Должность" + position.Name + " изменена";
            }
            return result;
        }

        //изменить сотрудника
        public static string EditUser(User newUser)
        {
            string result = "Пользователь не существует";
            using(DataContext context = new DataContext())
            {
                User user = context.Users.FirstOrDefault(u => u.Id == newUser.Id);
                user.Name = newUser.Name;
                user.Surname = newUser.Surname;
                user.PositionId = newUser.PositionId;
                context.SaveChanges();
                result = "Пользователь " + user.Name + " " + user.Surname + " изменён";
            }
            return result;
        }

        //получение должностей по ID
        public static Position GetPositionId(int Id)
        {
            using(DataContext context = new DataContext())
            {
                Position position = context.Positions.FirstOrDefault(p => p.Id == Id);
                return position;
            }
        }

        //получение отдела по ID
        public static Department GetDepartmentId(int Id)
        {
            using(DataContext data = new DataContext())
            {
                Department department = data.Departments.FirstOrDefault(dp => dp.Id == Id);
                return department;
            }
        }

        //получение всех юзеров по ID должности
        public static List<User> GetAllUserByPosId(int id)
        {
            using (DataContext context = new DataContext())
            {
                List<User> users = (from user in GetUsers() where user.PositionId == id select user).ToList();
                return users;
            }
        }

        //получение всех должностей по ID отдела
        public static List<Position> GetAllPosByDepId(int id)
        {
            using (DataContext context = new DataContext())
            {
                List<Position> positions = (from position in GetPositions() where position.DepartmentId == id select position).ToList();
                return positions;
            }
        }
    }
}
