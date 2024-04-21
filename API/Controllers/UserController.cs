using Microsoft.AspNetCore.Mvc;
using API.Business.Users;
using System.Xml.Linq;

namespace API.Frontend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        [HttpPost]
        [Route("register/{login}/{password}")]
        public bool Register(string login, string passwod)
        {
            try
            {
                User user = new User("", login, passwod);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpGet]
        [Route("login/{login}/{password}")]
        public bool Login(string login, string passwod)
        {
            User user = new User("", "test", "test");
            
            bool result = user.LogIn(login,passwod);

            user.Delete();

            return result;
        }

        [HttpPost]
        [Route("setname/{id}/{name}")]
        public bool SetName(int id, string name)
        {
            try
            {
                User user = new User("", "test", "test");
                user.SetName(id, name);
                user.Delete();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        [HttpPost]
        [Route("setphoto/{id}/{photo}")]
        public bool SetPhoto(int id, string photo)
        {
            try
            {
                User user = new User("", "test", "test");
                user.SetPhoto(id, photo);
                user.Delete();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
