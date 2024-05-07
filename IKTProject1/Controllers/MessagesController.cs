using IKTProject1.Data;
using IKTProject1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace IKTProject1.Controllers
{
    public class MessagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MessagesController(ApplicationDbContext context) 
        {
            _context = context; 
        }

        public IActionResult Index()
        {
            return View();
        }

        //on get display createForm
        [HttpGet]
        public IActionResult CreateGet()
        {
            return View("Create");
        }

        //on post create message and save to database
        [HttpPost]
        public IActionResult CreatePost(MessageModel mes)
        {
            try
            {
                mes.Owner = User.Identity.Name;
                _context.Messages.Add(mes);

            }
            catch (Exception ex)
            {
                _context.SaveChanges();
            }                       

            return View("Create");
        }
    }
}
