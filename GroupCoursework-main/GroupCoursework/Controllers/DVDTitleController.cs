using GroupCoursework.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GroupCoursework.Controllers
{
    [Authorize(Roles = "Admin,Staff")]
    public class DVDTitleController : Controller
    {
        private readonly DatabaseContext _context;

        public DVDTitleController(DatabaseContext context)
        {
            _context = context;
        }
       
        public async Task<IActionResult> Index()
        {
            return View(await _context.DVDTitle.ToListAsync());
        }


        public ActionResult Details(int id)
        {
            return View();
        }

        //For the Function 9 Part 1 of the coursework
 
        public IActionResult Create()
        {
            var Studio = _context.Studio.ToList();
            var Producer = _context.Producers.ToList();
            var Category = _context.DVDCategory.ToList();
            var Actors = _context.Actor.ToList();
            ViewBag.Studio = Studio;
            ViewBag.Producer = Producer;  
            ViewBag.Category = Category;
            ViewBag.Actors = Actors;
            return View();
        }
        //For the Function 9 Part 2 of the coursework
        // Post Method to create data in DVDTitle 
        [HttpPost]
        public async Task<IActionResult> Create(DVDTitle DVD, CastMember cm, String DVDname, int producer, int category, int studio, DateTime DateReleased, string StandardCharge, string PenaltyCharge, int[] actors)
        {
            DVD.DvdTitle = DVDname;
            DVD.ProducerNumber = producer;
            DVD.CategoryNumber = category;
            DVD.StudioNumber = studio;
            DVD.DateReleased = DateReleased;
            DVD.StandardCharge = StandardCharge;
            DVD.PenaltyCharge = PenaltyCharge;
            var Studio_Details = _context.Studio.ToList();
            DVD.Studio = Studio_Details.Where(o => o.StudioNumber == studio).First();
            var Producer_Details = _context.Producers.ToList();
            DVD.Producer = Producer_Details.Where(_o => _o.ProducerNumber == producer).First();
            var DVDCategory_Details = _context.DVDCategory.ToList();
            DVD.Category = DVDCategory_Details.Where(p => p.CategoryNumber == category).First();


            int[] actornumbers = actors.Distinct().ToArray();
            try
            {
                _context.DVDTitle.Add(DVD);
                await _context.SaveChangesAsync();
                var DVD_Details = _context.DVDTitle.ToList();
                var DVD_Detail = DVD_Details.Last();

                foreach (var actor in actornumbers) { 
                    cm.ActorNumber = actor;
                    cm.DVDNumber = DVD_Detail.DVDNumber;
                    cm.DVDTitle = DVD_Detail;
                    var Actor_Details = _context.Actor.ToList();
                    cm.Actor = Actor_Details.Where(o => o.ActorNumber == actor).First();

                    _context.CastMember.Add(cm);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("", new { Issuccess = true });
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Edit(int id)
        {
            return View();
        }

        // Post Method to edit data in DVDTitle 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

      
        public ActionResult Delete(int id)
        {
            return View();
        }
        // Post Method to delete data in DVDTitle 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
