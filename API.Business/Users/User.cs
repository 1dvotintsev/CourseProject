using API.Business.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Business.Users
{
    public class UserId
    {
        public static int overallCount = 0;
        public int number;

        public UserId()
        {
            this.number = overallCount;
            overallCount++;
        }
        public override string ToString()
        {
            return number.ToString();
        }
        public override bool Equals(object? obj)
        {
            if (obj is IdNumber em)
                return this.number == em.number;
            return false;
        }

        public override int GetHashCode()
        {
            return number.GetHashCode();
        }
    }

    public class User
    {
        protected string photo;
        protected string name;
        protected string login;
        protected string password;
        protected UserId id;

        public static LinkedList<User> users = new LinkedList<User>();
        public static Dictionary<int, User> _users= new Dictionary<int, User>();

        public List<int> likes;      //хранение айдишников у лайкнутых
        public List<int> dislikes;   //хранение айдишников у дизлайкнутых
        public List<int> reactions;  //хранение айдишников у всех просмотренных
        
        public static readonly Dictionary<string, string> _registeredUsers = new Dictionary<string, string>();

        public bool Register(string login, string password)
        {
            if (!_registeredUsers.ContainsKey(login))
            {
                _registeredUsers[login] = password;
                return true;
            }
            return false; // Пользователь с таким именем уже существует
        }

        public bool LogIn(string login, string password)
        {
            if (_registeredUsers.ContainsKey(login))
            {
                return _registeredUsers[login] == password;
            }
            return false; // Пользователь с таким именем не найден
        }
        
        public void Delete()
        {
            _registeredUsers.Remove(login);
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))  // возвращает true, если строка состоит только из пробелов или null
                {
                    throw new ArgumentException("Имя не заполнено");
                }
                name = value;
            }
        }

        public string Photo
        {
            get
            {
                return photo;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))  // возвращает true, если строка состоит только из пробелов или null
                {
                    throw new ArgumentException("Фото нет");
                }
                photo = value;
            }
        }

        public string Login
        {
            get
            {
                return login;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))  // возвращает true, если строка состоит только из пробелов или null
                {
                    throw new ArgumentException("Логин не заполнен");
                }
                login = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))  // возвращает true, если строка состоит только из пробелов или null
                {
                    throw new ArgumentException("Пароль не заполнен");
                }
                password = value;
            }
        }

        public string Id
        {
            get {return id.ToString(); }
        }

        public User(string name, string login, string password)
        {
            //Name = name;
            if (Register(login, password))
            {
                Login = login;
                Password = password;
                id = new UserId();
                Name = $"user{id.ToString()}";
                User.users.AddLast(this);

                _registeredUsers.Add(login, password);
                _users.Add(int.Parse(Id), this);
            }
            else { throw new ArgumentException("Логин уже занят"); }
        }

        public void SetPhoto(int id, string photo)
        {
            _users[id].Photo = photo;
        }

        public void Like(int id)
        {
            likes.Add(id);
            reactions.Add(id);
        }

        public void Dislike(int id) 
        {
            dislikes.Add(id);
            reactions.Add(id);
        }

        public void Skip(int id)
        {
            reactions.Add(id);
        }

        public void SetName(int id, string name)
        {
            _users[id].Name = name;
        }

        //public Place GetRecomendation()
        //{
        //    return;
        //}
    }    
}
