using GroupCoursework.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GroupCoursework.Controllers
{
    [Authorize(Roles ="Admin,Staff")]
    public class AssistantController : Controller
    {
        private readonly DatabaseContext _dbcontext;

        public AssistantController(DatabaseContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Index1()
        {
            return View();
        }

        //For the Function 3 of the coursework
        public IActionResult GetDateOut(int memberNumber)
        {

            DateTime currentDate = DateTime.Now.Date;
            DateTime lastDate = currentDate.Subtract(new TimeSpan(31, 0, 0, 0, 0));

            var dvdTitle = _dbcontext.DVDTitle.ToList();
            var dvdCopy = _dbcontext.DVDCopy.ToList();
            var castMember = _dbcontext.CastMember.ToList();
            var member = _dbcontext.Members.ToList();
            var loan = _dbcontext.Loan.ToList();


            var details = from d in dvdTitle
                          join dc in dvdCopy
                          on d.DVDNumber equals dc.DVDNumber into table1
                          from dc in table1.ToList().Distinct().Where(dc => dc.DVDNumber == d.DVDNumber)
                          join l in loan on dc.CopyNumber equals l.CopyNumber into table2
                          from l in table2.ToList().Distinct().Where(l => l.CopyNumber == dc.CopyNumber)
                          join c in castMember
                          on dc.DVDNumber equals c.DVDNumber into table3
                          from c in table3.ToList().Distinct().Where(c => c.DVDNumber == dc.DVDNumber)
                          join m in member
                          on l.MemberNumber equals m.MembershipNumber into table4
                          from m in table4.ToList().Distinct().Where(m => m.MembershipNumber == l.MemberNumber && m.MembershipNumber == memberNumber && DateTime.Parse(l.DateOut) >= lastDate)
                          select new { dvdTitle = d, castMember = c, dvdCopy = dc, loan = l, member = m };
            ViewBag.date = details;
            ViewBag.memberNumbers = _dbcontext.Members.ToArray();
            return View();
        }
        //For the Function 4 of the coursework
        public IActionResult GetList()
        {

            var dvdTitle = _dbcontext.DVDTitle.ToList();
            var producer = _dbcontext.Producers.ToList();
            var castMember = _dbcontext.CastMember.ToList();
            var studio = _dbcontext.Studio.ToList();
            var actor = _dbcontext.Actor.ToList();
            var listProducer = from dt in dvdTitle
                               join c in castMember on dt.DVDNumber equals c.DVDNumber into table1
                               from c in table1.ToList().Where(c => c.DVDNumber == dt.DVDNumber).ToList()

                               join p in producer on dt.ProducerNumber equals p.ProducerNumber into table2
                               from p in table2.ToList().Where(p => p.ProducerNumber == dt.ProducerNumber).ToList()

                               join s in studio on dt.StudioNumber equals s.StudioNumber into table3
                               from s in table3.ToList().Where(s => s.StudioNumber == dt.StudioNumber).ToList()

                               join a in actor on c.ActorNumber equals a.ActorNumber into table4
                               from a in table4.ToList().Where(a => a.ActorNumber == c.ActorNumber).ToList()
                               orderby dt.DateReleased ascending, a.ActorSurname ascending
                               select new { dvdTitle = dt, castMember = c, actorDetails = a, studio = s, producer = p };
        
            ViewBag.listProducer = listProducer;

            return View();
        }

        //For the Function 5 of the coursework
        public IActionResult GetLoanDetails(int copynumber)
        {
            var dvdTitle = _dbcontext.DVDTitle.ToList();
            var loan = _dbcontext.Loan.ToList();
            var lastloan = loan.LastOrDefault();
            var member = _dbcontext.Members.ToList();
            var dvdCopy = _dbcontext.DVDCopy.ToList();
            var loanDetails = (from l in loan
                               join m in member on l.MemberNumber equals m.MembershipNumber into table1
                               from m in table1.ToList().Where(m => m.MembershipNumber == l.MemberNumber).ToList()

                               join dc in dvdCopy on l.CopyNumber equals dc.CopyNumber into table2
                               from dc in table2.ToList().Where(dc => dc.CopyNumber == l.CopyNumber && l.CopyNumber == copynumber).ToList()

                               join dt in dvdTitle on dc.DVDNumber equals dt.DVDNumber into table3
                               from dt in table3.ToList().Where(dt => dt.DVDNumber == dc.DVDNumber).ToList()
                               group new { dt, l, m, dc } by new { l.CopyNumber, dt.DvdTitle, m.MembershipFirstName, m.MembershipLastName, m.MemberDOB, m.MembershipAddress, l.DateDue, l.DateOut, l.DateReturned }
                              into grp
                               select new
                               {
                                   grp.Key.CopyNumber,
                                   grp.Key.DvdTitle,
                                   grp.Key.MembershipFirstName,
                                   grp.Key.MembershipLastName,
                                   grp.Key.MemberDOB,
                                   grp.Key.MembershipAddress,
                                   grp.Key.DateDue,
                                   grp.Key.DateOut,
                                   grp.Key.DateReturned,
                               });

            ViewBag.loanDetails = loanDetails;
            ViewBag.copyNumber = _dbcontext.DVDCopy.ToArray();
            return View();
        }

        //For the Function 6 of the coursework
        public IActionResult AddDVDCopy() {
            var dvdcopy = _dbcontext.DVDCopy.ToList();
            var dvdtitle = _dbcontext.DVDTitle.ToList();

            var members = _dbcontext.Members.ToList();
            
            var loanType = _dbcontext.LoanType.ToList();

            ViewBag.member = members;
            ViewBag.loanType = loanType;

            var dvd = from dc in dvdcopy
                      join dt in dvdtitle on dc.DVDNumber equals dt.DVDNumber
                       select new { 
                       dvdtitle = dt,
                       dvdcopy = dc,
                       };
            ViewBag.dvd = dvd;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDVDCopy(Loan Loan, int member, int loantype, int copynumber)
        {
            var dvdcopy = _dbcontext.DVDCopy.ToList();
            var dvdtitle = _dbcontext.DVDTitle.ToList();

            var members = _dbcontext.Members.ToList();

            var loanType = _dbcontext.LoanType.ToList();

            ViewBag.member = members;
            ViewBag.loanType = loanType;

            var dvdcd = from dc in dvdcopy
                      join dt in dvdtitle on dc.DVDNumber equals dt.DVDNumber
                      select new
                      {
                          dvdtitle = dt,
                          dvdcopy = dc,
                      };
            ViewBag.dvd = dvdcd;


            var memberInfo = _dbcontext.Members.Where(x => x.MembershipNumber == member).First();
            String dob = memberInfo.MemberDOB;
            String todaysDate = DateTime.Now.ToShortDateString();

            var today = DateTime.Now.ToShortDateString();


       
            DateTime cDOB = DateTime.Parse(dob);
            DateTime ctodaysDate = DateTime.Parse(todaysDate);

            TimeSpan dayDiff = ctodaysDate.Subtract(cDOB);
            Console.Write(dayDiff.Days.ToString());
            var age = dayDiff.Days / 365;
            Console.Write(age);

            

            var dvd = _dbcontext.DVDTitle.ToList();
            var catogory = _dbcontext.DVDCategory.ToList();
    
            var dvdCopy = _dbcontext.DVDCopy.Where(x => x.CopyNumber == copynumber).First();
            var dvdInfo = _dbcontext.DVDTitle.Where(x => x.DVDNumber == dvdCopy.DVDNumber).First();

            var agerestriction = dvdInfo.Category.AgeRestricted;

            Loan.MemberNumber = member;
            Loan.LoanTypeNumber = loantype;
            Loan.CopyNumber = copynumber;
            Loan.DateOut = DateTime.Now.ToShortDateString();
            var loantypeinfo = _dbcontext.LoanType.Where(x => x.LoanTypeNumber == loantype).First();
          
            Loan.DateDue = DateTime.Now.AddMonths(int.Parse(loantypeinfo.LoanDuration)).ToShortDateString();
            Loan.DateReturned = "0";
            var price = int.Parse(loantypeinfo.LoanDuration) * int.Parse(dvdInfo.StandardCharge);
       
            if (!agerestriction)
            {
                _dbcontext.Loan.Add(Loan);
                await _dbcontext.SaveChangesAsync();
                ViewBag.Price = price;
                return View("AddDVDCopy2");
            }
            if (agerestriction) {
                if (age > 18)
                {
                    _dbcontext.Loan.Add(Loan);
                    await _dbcontext.SaveChangesAsync();
                    ViewBag.Price = price;

                    return View("AddDVDCopy2");

                }
                else {
                return RedirectToAction("AddDVDCopyMessage", "Assistant");
                    //cannot loan the dvd due to age restriction
                }
            }
                return RedirectToAction("AddDVDCopy","Assistant");

        }


        public IActionResult AddDVDCopyMessage()
        {
            var dvdcopy = _dbcontext.DVDCopy.ToList();
            var dvdtitle = _dbcontext.DVDTitle.ToList();

            var members = _dbcontext.Members.ToList();

            var loanType = _dbcontext.LoanType.ToList();

            ViewBag.member = members;
            ViewBag.loanType = loanType;

            var dvd = from dc in dvdcopy
                      join dt in dvdtitle on dc.DVDNumber equals dt.DVDNumber
                      select new
                      {
                          dvdtitle = dt,
                          dvdcopy = dc,
                      };
            ViewBag.dvd = dvd;

            return View();
        }

        //For the Function 7 of the coursework
        public IActionResult ListAllLoans()
        {
    

            DateTime currentDate = DateTime.Now.Date;
            DateTime lastDate = currentDate.Subtract(new TimeSpan(365, 0, 0, 0, 0));
            String d = "0";
            var loan = _dbcontext.Loan.ToList();
            var member = _dbcontext.Members.ToList();
            var loanDetail = (from l in loan
                              join m in member on l.MemberNumber equals m.MembershipNumber into table1
                              from m in table1.ToList().Where(m => m.MembershipNumber == l.MemberNumber && l.DateReturned == d)
                              orderby l.CopyNumber ascending
                              select new { loan = l, member = m });
            ViewBag.loanDetails = loanDetail;
            return View();
        }


        public IActionResult EditDVDCopyDetails(int CopyNumber)
        {
       
            ViewBag.UserLoanDetails = _dbcontext.Loan.Where(l => l.CopyNumber == CopyNumber).First();
            var cop = ViewBag.UserLoanDetails;


            ViewBag.CopyDVDNumber = _dbcontext.DVDCopy.Where(c => c.CopyNumber == CopyNumber).First();
            int copydvdnum = ViewBag.CopyDVDNumber.DVDNumber;


            ViewBag.DVDNumber = _dbcontext.DVDTitle.Where(d => d.DVDNumber == copydvdnum).First();
            ViewBag.PenaltyCharge = ViewBag.DVDNumber.PenaltyCharge;
            int pCharge = int.Parse(ViewBag.PenaltyCharge);

  
            DateTime dueDate = DateTime.Parse(ViewBag.UserLoanDetails.DateDue);
            DateTime returnDate = DateTime.Now.Date.Date;

   
            var onlydate = returnDate.ToShortDateString();

         
            TimeSpan difference = returnDate.Subtract(dueDate);
            int dueDay = difference.Days;

            ViewBag.ReturnDate = onlydate;
            if (dueDay < 0)
            {
                ViewBag.OverDue = "0";
                ViewBag.TotalCharge = "0";
            }
            else
            {
                ViewBag.OverDue = dueDay;
                int totalCharge = dueDay * pCharge;
                ViewBag.TotalCharge = totalCharge;
            }


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RecordDVDCopy
       (Loan loan, int loanNumber, int loantypenumber, int copynumber, int membernumber, string dateOut, string dateReturned, string dateDue)
        {
            loan = _dbcontext.Loan.Where(l => l.LoanTypeNumber == loantypenumber).First();

            loan.DateReturned = dateReturned;
                _dbcontext.Loan.Update(loan);
                var result = await _dbcontext.SaveChangesAsync();
                return RedirectToAction("ListAllLoans");
        }
        //For the Function 8 of the coursework
        //https://localhost:44344/Assistant/GetTotalLoans
        public IActionResult GetTotalLoans()
        {

            String c = "0";
            var member = _dbcontext.Members.ToList();
            var loan = _dbcontext.Loan.ToList();
            var membercat = _dbcontext.MembershipCategory.ToList();

            var dvd = (from m in member
                       join l in loan on m.MembershipNumber equals l.MemberNumber into table1
                       from l in table1.Distinct().ToList().Where(l => l.MemberNumber == m.MembershipNumber).Distinct().ToList()

                       join mc in membercat on m.MembershipCategoryNumber equals mc.MembershipCategoryNumber into table2
                       from mc in table2.Distinct().ToList().Where(mc => mc.MembershipCategoryNumber == m.MembershipCategoryNumber)
                       group new { l, m, mc } by new { m.MembershipFirstName, m.MembershipCategoryNumber, mc.MembershipCategoryTotalLoans }
                      into grp
                       select new
                       {
                           //
                           grp.Key.MembershipFirstName,
                           grp.Key.MembershipCategoryNumber,
                           grp.Key.MembershipCategoryTotalLoans,
                           TotalLoans = grp.Count(),
                       }).OrderBy(x => x.MembershipFirstName);
            ViewBag.totalloans = dvd;
            return View();
        }

        //For the Function 10 of Part 1 of the coursework
        //https://localhost:44344/Assistant/GetListOfDVDCopy
        public IActionResult GetListOfDVDCopy(bool copyDeleted = false)
        {
            ViewBag.copyDeleted = copyDeleted;

            DateTime currentDate = DateTime.Now.Date;
            DateTime lastDate = currentDate.Subtract(new TimeSpan(365, 0, 0, 0, 0));
            String d = "0";
            var loan = _dbcontext.Loan.ToList();
            var dvdCopy = _dbcontext.DVDCopy.ToList();

            var dvd = from dc in dvdCopy
                      join l in loan on dc.CopyNumber equals l.CopyNumber into table1
                      from l in table1.Distinct().Where(l => l.CopyNumber == dc.CopyNumber && l.DateReturned != d && dc.DatePurchased < lastDate)

                      select new { loan = l, dvdCopy = dc };
            ViewBag.dvdList = dvd;
            return View();
        }

        //Part 2
        [HttpGet]
        public async Task<IActionResult> DeleteCopy(int copynumber)
        {
            var copy = _dbcontext.DVDCopy.Where(l => l.CopyNumber == copynumber).First();
            _dbcontext.DVDCopy.Remove(copy);
            _dbcontext.SaveChanges();

            return RedirectToAction("GetListOfDVDCopy", new { copyDeleted = true });
        }


        //For the Function 11 of the coursework
        //https://localhost:44344/Assistant/GetDVDCopyListNotLoaned
        public IActionResult GetDVDCopyListNotLoaned()
        {

            String c = "0";
            var member = _dbcontext.Members.ToList();
            var loan = _dbcontext.Loan.ToList();
            var dvdTitle = _dbcontext.DVDTitle.ToList();
            var dvdCopy = _dbcontext.DVDCopy.ToList();

            var copyloan = (from l in loan
                            join m in member on l.MemberNumber equals m.MembershipNumber into table1
                            from m in table1.Distinct().ToList().Where(m => m.MembershipNumber == l.MemberNumber).Distinct().ToList()
                            join dc in dvdCopy on l.CopyNumber equals dc.CopyNumber into table2
                            from dc in table2.Distinct().ToList().Where(dc => dc.CopyNumber == l.CopyNumber).Distinct().ToList()
                            join dt in dvdTitle on dc.DVDNumber equals dt.DVDNumber into table3
                            from dt in table3.Distinct().ToList().Where(dt => dt.DVDNumber == dc.DVDNumber && l.DateReturned == c).Distinct().ToList()
                            group new { l, m, dc, dt } by new { dt.DvdTitle, dc.CopyNumber, m.MembershipFirstName, l.DateOut }
                           into grp
                            select new
                            {
                                TotalLoans = grp.Count(),
                                grp.Key.DvdTitle,
                                grp.Key.CopyNumber,
                                grp.Key.MembershipFirstName,
                                grp.Key.DateOut,

                            }).OrderBy(X => X.TotalLoans);
            ViewBag.totalloans = copyloan;
            return View();
        }


        //For the Function 12 of the coursework
        public IActionResult MemberListNotBorrowed()
        {

            var loan = _dbcontext.Loan.ToList();
            var maxDate = from l in loan
                          group l by l.MemberNumber
                          into g
                          select new
                          {
                              MaxDates = (from l in g select l.DateOut).Max(),
                          };
            ViewBag.dates = maxDate.ToList();
            var members = _dbcontext.Members.ToList();
            var dvdCopy = _dbcontext.DVDCopy.ToList();
            var dvdTitle = _dbcontext.DVDTitle.ToList();
            List<int> difference = new List<int>();
            dynamic details = null;


            foreach (var dd in ViewBag.dates)
            {

                DateTime today = DateTime.Now;
                var dates = DateTime.Parse(dd.MaxDates.ToString());
                var diff = (today - dates).Days;
                var data = (from l in loan
                            join m in members on l.MemberNumber equals m.MembershipNumber
                            join dc in dvdCopy on l.CopyNumber equals dc.CopyNumber
                            join dt in dvdTitle on dc.DVDNumber equals dt.DVDNumber
                            where (31 > diff)
                            group new { l, m, dc, dt } by new { m.MembershipFirstName, m.MembershipLastName, m.MembershipAddress, ViewBag.dates }
                            into grp
                            select new
                            {
                                grp.Key.MembershipLastName,
                                grp.Key.MembershipFirstName,
                                grp.Key.MembershipAddress,
                                MaxDates = (from l in grp select l.l.DateOut).Max(),
                                Difference = diff
                            }).OrderBy(x => x.MembershipFirstName);
                ViewBag.details = data;
                return View();
            }
            return View();
        }


        //For the Function 13 of the coursework
        //https://localhost:44344/Assistant/GetDVDofNoLoan
        public IActionResult GetDVDofNoLoan()
        {
   
            DateTime currentDate = DateTime.Now.Date;
            DateTime lastDate = currentDate.Subtract(new TimeSpan(31, 0, 0, 0, 0));
            String d = "0";
            var dvdTitle = _dbcontext.DVDTitle.ToList();
            var loan = _dbcontext.Loan.ToList();
            var dvdCopy = _dbcontext.DVDCopy.ToList();

            var dvd = from dt in dvdTitle
                      join dc in dvdCopy on dt.DVDNumber equals dc.DVDNumber into table1
                      from dc in table1
                      join l in loan on dc.CopyNumber equals l.CopyNumber into table2
                      from l in table2.Distinct().ToList().Where(l => l.CopyNumber == dc.CopyNumber && l.DateReturned == d && DateTime.Parse(l.DateOut) >= lastDate).Distinct().ToList()

                      select new { dvdTitle = dt, loan = l, dvdCopy = dc };

            ViewBag.dvd = dvd;
            return View();
        }
    }
}
