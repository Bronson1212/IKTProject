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
            //get list of all messages
            var mesList = _context.Messages.ToList();

            return View("Index", new {MessagesList = mesList});
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
            //check if i am registred
            if(User.Identity.Name == null)
            {
                return View("Create");
            }

            try
            {
                //if there is no error save to database
                mes.Owner = User.Identity.Name;
                _context.Messages.Add(mes);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                return View("Create");
            }

            return RedirectToAction("Index");
        }
    }
}
