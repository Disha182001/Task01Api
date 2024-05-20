using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tak01Api.Repository.Interface_Repo;

namespace Tak01Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ICRUD _cRUD;
        public TaskController(ICRUD cRUD) 
        {
        
        _cRUD = cRUD;
        //cRUD=_cRUD;
        
        }
        [HttpGet]
        [Route("getvalue")]
        public ActionResult Get(string str1) 
        {
            string str = _cRUD.GetString(str1);
            if(str ==null)
            {
                return NotFound();
            }
            return Ok(str);

        }

    }

}
