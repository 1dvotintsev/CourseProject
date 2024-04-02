using Microsoft.AspNetCore.Mvc;
using API.Business.Objects;

namespace API.Frontend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : Controller
    {
        [Route("getall")]
        [HttpGet]
        public IActionResult GetAll()
        {
            Image parkImage = new Image();
            parkImage.location = "ссылка в сибирь";
            Park park = new Park("Парк Горького", parkImage, "Cool place", "23.12");
            
            Image musemImage = new Image();
            musemImage.location = "ссылочка";
            Museum museum = new Museum("Театр Театр", musemImage, "Cool place", "УлицаПушкина");

            return Json(Museum.museums);
        }

        [Route("get")]
        [HttpGet]
        public IActionResult GetSingle()
        {
            Image parkImage = new Image();
            parkImage.location = "ref";
            Park park = new Park("Gorkov", parkImage, "Cool place", "23.12");

            Image musemImage = new Image();
            musemImage.location = "ссылочка";
            Museum museum = new Museum("Театр Театр", musemImage, "Cool place", "УлицаПушкина");

            return Json(museum);
        }
    }
}
