namespace API.Business.Objects
{
    public class Park: Place
    {
        protected string geo;
        public static LinkedList<Park> parks = new LinkedList<Park>();

        public string Geo
        {
            get 
            { 
                return geo; 
            } 
            set 
            {
                if (string.IsNullOrWhiteSpace(value))  // возвращает true, если строка состоит только из пробелов или null
                {
                    throw new ArgumentException("Расположение не заполнено");
                }
                geo = value;               
            }
        }

        public Park(string name, Image image, string description, string geo): base(name, image, description)
        {
            Geo = geo;

            //list.AddLast(this);
            parks.AddLast(this);
        }
    }
}
