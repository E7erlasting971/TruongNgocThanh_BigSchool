using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TruongNgocThanh_BigSchool.Models;

namespace TruongNgocThanh_BigSchool.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string SearchString)
        {
            BigSchoolContext context = new BigSchoolContext();
            var upcommingCourse = context.Courses.Where(p => p.Datetime >
            DateTime.Now).OrderBy(p => p.Datetime).ToList();
            //lấy user login hiện tại
            if(SearchString != null)
            {
                //var tp = context.Courses.Where();
                ApplicationUser currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()
                .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                var courses = context.Courses.Where(c => c.LecturerId == currentUser.Id || c.Place.Contains(SearchString)).ToList();
                foreach (Course i in courses)
                {
                    i.LectureName = currentUser.Name;
                }
                return View(courses);
            }
           
            var userID = User.Identity.GetUserId();
            foreach (Course i in upcommingCourse)
            {
                //tìm Name của user từ lectureid
                ApplicationUser user =
                System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(
                ).FindById(i.LecturerId);
                i.Name = user.Name;
                //lấy ds tham gia khóa học
                if (userID != null)
                {
                    i.IsLogin = true;
                    //ktra user đó chưa tham gia khóa học
                    Attendance find = context.Attendances.FirstOrDefault(p =>
                    p.CourseId == i.Id && p.Attendee == userID);
                    if (find == null)
                        i.IsShowGoing = true;
                    //ktra user đã theo dõi giảng viên của khóa học ?
                    Following findFollow = context.Followings.FirstOrDefault(p =>
                    p.FollowerId == userID && p.FolloweeId == i.LecturerId);

                    if (findFollow == null)
                        i.IsShowFollow = true;
                }
            }
            return View(upcommingCourse);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}