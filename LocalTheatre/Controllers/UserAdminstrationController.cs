using LocalTheatre.Models;
using LocalTheatre.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LocalTheatre.Controllers
///<summary>
/// This controller manages the users roles and general administration
/// 08/02/2018
/// 
/// ********
/// Error Log
/// ********
/// |
/// |
/// |
/// |
/// |
/// |
/// </summary>
{
    [Authorize(Roles = "Admin")]
    public class UserAdminstrationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // Gets user administration rights
        public ActionResult Index()
        {
            List<UserViewModel> theUsers = new List<UserViewModel>();
            foreach(ApplicationUser aUser in db.Users)
            {
                UserViewModel uViewModel = new UserViewModel();
                uViewModel.ID = aUser.Id;
                uViewModel.UserName = aUser.UserName;
                uViewModel.Email = aUser.Email;
                uViewModel.IsAdmin = aUser.IsAdmin;
                uViewModel.IsSuspended = aUser.IsSuspended;
                theUsers.Add(uViewModel);
            }
            return View(theUsers);
        }

        // Gets user admin details        
        public ActionResult Details(string id)
        {
            ApplicationUser aUser = db.Users.Single(m => m.Id == id);
            UserViewModel uvm = new UserViewModel();
            uvm.UserName = aUser.UserName;
            uvm.UserName = aUser.Email;
            uvm.IsAdmin = aUser.IsAdmin;
            uvm.IsSuspended = aUser.IsSuspended;

            return View(uvm);
        }

        // Get the useradmin creates   
        public ActionResult Create()
        {
            return View();
        }

        // Posts the user admin create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // Gets the user admin rights
        public ActionResult Edit(string id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserViewModel uvm = new ViewModels.UserViewModel();
            ApplicationUser applicationUser = db.Users.Find(id);
            if(applicationUser == null)
            {
                return HttpNotFound();
            }
            else
            {
                uvm.UserName = applicationUser.UserName;
                uvm.Email = applicationUser.Email;
                uvm.IsAdmin = applicationUser.IsAdmin;
                uvm.IsSuspended = applicationUser.IsSuspended;
            }
            return View(uvm);
        }

        // Posts user admin editing
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserViewModel userViewModel)
        {
            var UserManager = new UserManager<ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<ApplicationUser>(db));
            ApplicationUser au = db.Users.Find(userViewModel.ID);
            au.Id = userViewModel.ID;
            au.UserName = userViewModel.UserName;
            au.Email = userViewModel.Email;
            au.IsAdmin = userViewModel.IsAdmin;
            au.IsSuspended = userViewModel.IsSuspended;

            if(ModelState.IsValid)
            {
                db.Entry(au).State = EntityState.Modified;
                if ((au.IsAdmin) && (UserManager.IsInRole(au.Id, "Admin")))
                    UserManager.AddToRole(au.Id, "Admin");
                else if ((!au.IsAdmin) && (UserManager.IsInRole(au.Id, "Admin")))
                    UserManager.RemoveFromRoles(au.Id, "Admin");
                db.SaveChanges();
                if ((au.IsSuspended) && (!UserManager.IsInRole(au.Id, "Restricted")))
                    UserManager.AddToRole(au.Id, "Restricted");
                else if ((!au.IsSuspended) && (UserManager.IsInRole(au.Id, "Restricted")))
                    UserManager.RemoveFromRoles(au.Id, "Restricted");

                return RedirectToAction("Index");
            }
            return View(userViewModel);
        }

        // Get the user admin delete
        public ActionResult Delete(string id)
        {
            UserViewModel uvm = new ViewModels.UserViewModel();
            ApplicationUser applicationUser = db.Users.Find(id);
            if(applicationUser == null)
            {
                return HttpNotFound();
            }
            else
            {
                uvm.ID = applicationUser.Id;
                uvm.UserName = applicationUser.UserName;
                uvm.Email = applicationUser.Email;
                uvm.IsAdmin = applicationUser.IsAdmin;
                uvm.IsSuspended = applicationUser.IsSuspended;
            }
            return View(uvm);
        }

        // Posts the user admin delete.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            db.Users.Remove(applicationUser);
            db.SaveChanges();

                return RedirectToAction("Index");
            
        }
    }
}
