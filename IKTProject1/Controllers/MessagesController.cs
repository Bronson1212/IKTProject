using IKTProject1.Data;
using IKTProject1.Models;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Text.Json;

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
            List<MessageModel>? mesList = _context.Messages.ToList();
            mesList.Reverse();

            //get comments list
            List<CommentModel> commList = _context.Comments.ToList();
            commList.Reverse();

            return View("Index", new {MessagesList = mesList, CommentsList = commList});
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult PublishComment(string comment, string messageId)
        {
            //check if user is authenticated
            if(!User.Identity.IsAuthenticated)
            {
                return Content(JsonSerializer.Serialize(new { status = "fail" }));
            }
            string userName = User.Identity.Name;

            try
            {//save to database

                //create comment object
                var comm = new CommentModel();
                comm.Text = comment;
                comm.Autor = userName;
                comm.MessageId = int.Parse(messageId);

                //save to database
                _context.Comments.Add(comm);
                _context.SaveChanges();
            }
            catch
            {
                //return error
                return Content(JsonSerializer.Serialize(new { status = "fail" }));
            }

            return Content(JsonSerializer.Serialize(new { status = "good", commText = comment, userName = userName }));
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
