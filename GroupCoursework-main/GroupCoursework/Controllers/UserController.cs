using GroupCoursework.Models;
using Microsoft.AspNetCore.Mvc;

namespace GroupCoursework.Controllers
{
    public class UserController : Controller
    {
        private readonly DatabaseContext _context;

        public UserController(DatabaseContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }



        //For the Function 1 of the coursework
        public IActionResult GetActorDetails(String name)
        {

            var dvdTitle = _context.DVDTitle.ToList();
            var castMember = _context.CastMember.ToList();
            var actorDetails = _context.Actor.ToList();
            var details =
                from d in dvdTitle
                join c in castMember
                on d.DVDNumber equals c.DVDNumber into table1
                from c in table1.Distinct().ToList().Where(c => c.DVDNumber == d.DVDNumber)
                join a in actorDetails on c.ActorNumber equals a.ActorNumber into table2
                from a in table2.Distinct().ToList().Where(a => a.ActorNumber == c.ActorNumber && a.ActorSurname == name)
                select new { dvdTitle = d, castMember = c, actorDetails = a };
            var r = _context.Actor.FirstOrDefault();
            ViewBag.last = r;
            ViewBag.name = details;
            ViewBag.Actors = actorDetails;
            return View();
        }


        //For the Function 2 of the coursework
        public IActionResult GetActorCopy(int actorID)
        {

            ViewBag.actorName = _context.Actor.ToArray();
            String n = "0";
            var dvdTitle = _context.DVDTitle.ToList();
            var dvdCopy = _context.DVDCopy.ToList();
            var castMember = _context.CastMember.ToList();
            var actorDetails = _context.Actor.ToList();
            var loan = _context.Loan.ToList();
            var details =
                (from l in loan
                 join dc in dvdCopy on l.CopyNumber equals dc.CopyNumber
                 join dt in dvdTitle on dc.DVDNumber equals dt.DVDNumber
                 join c in castMember on dt.DVDNumber equals c.DVDNumber
                 group new { l, dc, dt, c } by new { l.CopyNumber, dc.Stock, dt.DvdTitle, c.ActorNumber }
                          into grp
                 select new
                 {
                     DVDTitleName = grp.Key.DvdTitle,
                     Stock = grp.Key.Stock,
                     CastActorNumber = grp.Key.ActorNumber,
                     LoanCount = grp.Count(),
                 }).Where(x => x.CastActorNumber == actorID);
            ViewBag.name = details;
            return View(details);
        }


    }
}
