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
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult Create()
        {
            BigSchoolContext context = new BigSchoolContext();
            Course objCourse = new Course();
            objCourse.ListCategory = context.Categories.ToList();
            return View(objCourse);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Create(Course objCourse)
        {
            BigSchoolContext context = new BigSchoolContext();

            //lay login  user tu id
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().
                FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            objCourse.LectureId = user.Id;
            // add vao csdl
            context.Courses.Add(objCourse);
            context.SaveChanges();
            //context
            ///asdhasdjasjkdasjdjasjkdas
            return RedirectToAction("Index", "Home");
        }
    }
}