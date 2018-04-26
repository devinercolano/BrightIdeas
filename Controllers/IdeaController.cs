using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Belt.Models;
using Microsoft.EntityFrameworkCore;

namespace Belt.Controllers
{
    public class IdeaController : Controller
    {
        private BeltContext _context;
 
        public IdeaController(BeltContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("BrightIdeas")]
        public IActionResult BrightIdeas(){
            if (HttpContext.Session.GetInt32("userId") == null) {
                return RedirectToAction("Index", "Home");
            }
            List<Idea> AllIdeas = _context.Ideas.Include(a => a.Likes).ThenInclude(p => p.User).Include(u => u.User).ToList();
            ViewBag.AllIdeas = AllIdeas;
            int? UserId = HttpContext.Session.GetInt32("userId");
            
            ViewBag.UserId = UserId;
            return View("BrightIdeas");
        }
        [HttpPost]
        [Route("CreateIdea")]
        public IActionResult CreateIdea(IdeaFormViewModel model){
           int? UserId = HttpContext.Session.GetInt32("userId");
            Idea idea = new Idea{
                IdeaText = model.IdeaText,
                UserId = (int)UserId,
            };

            _context.Ideas.Add(idea);
            _context.SaveChanges();

            Idea Idea = _context.Ideas.SingleOrDefault(i => idea.IdeaText == i.IdeaText);
            int IdeaId = idea.IdeaId;

            return RedirectToAction("Idea", new { IdeaId = IdeaId });

        }
        [HttpGet]
        [Route("Idea/{IdeaId}")]
        public IActionResult Idea(int IdeaId){
            if (HttpContext.Session.GetInt32("userId") == null) {
                return RedirectToAction("Index", "Home");
            }

            Idea Idea = _context.Ideas.Include(l => l.Likes).ThenInclude(a => a.User).Include(u => u.User).SingleOrDefault(i => i.IdeaId == IdeaId);
            List<Like> likeData = _context.Likes.Where(l => l.IdeaId == IdeaId).ToList();
    
            ViewBag.Idea = Idea;
            ViewBag.likeData = likeData;
            return View("Idea");

        }

        [HttpPost]
        [Route("Like")]
        public IActionResult Like(int hiddenIdeaId){
            if (HttpContext.Session.GetInt32("userId") == null) {
                return RedirectToAction("Index", "Home");
            }
            
            int? UserId = HttpContext.Session.GetInt32("userId");
            Like like = new Like{
                UserId = (int)UserId,
                IdeaId = hiddenIdeaId,
            };

            _context.Likes.Add(like);
            _context.SaveChanges();
            return RedirectToAction("BrightIdeas");
        }
        [HttpPost]
        [Route("User/{hiddenUserId}")]
        public IActionResult ShowUser(int hiddenUserId){
            User user = _context.Users.SingleOrDefault(u => u.UserId == hiddenUserId);
            List <Idea> idea = _context.Ideas.Where(u => u.UserId == hiddenUserId).ToList();
            List <Like> like = _context.Likes.Where(u => u.UserId == hiddenUserId).ToList();
            ViewBag.User = user;
            ViewBag.Idea = idea;
            ViewBag.Like = like;
            return View("User");
        }
        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(int hiddenIdeaId){
            if (HttpContext.Session.GetInt32("userId") == null) {
                return RedirectToAction("Index", "Home");
            }
            Idea ideaToDelete = _context.Ideas.SingleOrDefault(a => a.IdeaId == hiddenIdeaId);
            _context.Ideas.Remove(ideaToDelete);
            _context.SaveChanges();
            return Redirect("BrightIdeas");
        } 
    }
}
