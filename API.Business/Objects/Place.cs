namespace API.Business.Objects
{
    public class Image
    {
        public string location;

        public override string ToString()
        {
            return location.ToString();
        }
    }

    public class IdNumber
    {
        public static int overallCount = 0;
        public int number;
        public IdNumber()
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

    public class Place
    {
        protected IdNumber id;
        protected string name;
        protected Image image;
        protected string description;
        
        public static List<Place> allPlaces = new List<Place>();
        

        public string Id
        {
            get { return id.ToString(); }
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
                    throw new ArgumentException("Название не заполнено");
                }
                name = value;
            }
        }

        //public string Img
        //{
        //    get
        //    {
        //        return image.ToString();
        //    }
        //    set
        //    {
        //        if (string.IsNullOrWhiteSpace(value.ToString()))  // возвращает true, если строка состоит только из пробелов или null
        //        {
        //            throw new ArgumentException("Сылка на изображение отсутствует");
        //        }
        //        image.location = value;
        //    }
        //}

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))  // возвращает true, если строка состоит только из пробелов или null
                {
                    throw new ArgumentException("Описание не заполнено");
                }
                description = value;
            }
        }

        public Place(string name, Image image, string description)
        {
            this.id = new IdNumber();
            Name = name;
            Description = description;
            //Img = image.ToString();

            allPlaces.Add(this);
        }
    }
}
