namespace API.Business.Objects
{
    public class Museum: Place
    {
        protected string adress;
        public static LinkedList<Museum> museums = new LinkedList<Museum>();

        public string Adress
        {
            get
            {
                return adress;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))  // возвращает true, если строка состоит только из пробелов или null
                {
                    throw new ArgumentException("Расположение не заполнено");
                }
                adress = value;
            }
        }

        public Museum(string name, Image image, string description, string adress) : base(name, image, description)
        {
            Adress = adress;

            //list.AddLast(this);
            museums.AddLast(this);
        }
    }
}
