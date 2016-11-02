using SimplePortal.Areas.Admin.Models;
using SimplePortal.Db.Ef;
using SimplePortal.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimplePortal.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private readonly IEfRepository<User> _userRepository = null;

        public UserController(IEfRepository<User> userRepository)
        {
            if (userRepository == null)
            {
                throw new ArgumentNullException(nameof(userRepository));
            }

            _userRepository = userRepository;
        }
        

        public ActionResult Index()
        {
            return View(_userRepository.Items);
        }

        [HttpGet]
        public ActionResult CreateUser()
        {
            ViewBag.UserRoleListItems = GetUserRoleListItems();
            return View(new User());
        }

        [HttpPost]
        public ActionResult CreateUser(User entity)
        {
            if (entity != null)
            {
                entity.HashPassword();
                _userRepository.Create(entity);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditUser(Guid uid)
        {
            User entity = _userRepository.FindRecord(uid);

            if (entity != null)
            {
                ViewBag.UserRoleListItems = GetUserRoleListItems();
                entity.Password = string.Empty;
                return View(entity);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditUser(User entity)
        {
            if (entity != null)
            {
                User dbRecord = _userRepository.FindRecord(entity.Uid);
                entity.Password = dbRecord.Password;
                _userRepository.Update(entity);
            }

            ViewBag.UserRoleListItems = GetUserRoleListItems();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ChangePassword(Guid uid)
        {
            User dbRecord = _userRepository.FindRecord(uid);
            if (dbRecord != null)
            {
                ChangePasswordViewModel model = new ChangePasswordViewModel
                {
                    Uid = dbRecord.Uid
                };

                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            User dbRecord = _userRepository.FindRecord(model.Uid);
            if (dbRecord != null)
            {
                dbRecord.Password = model.NewPassword;
                dbRecord.HashPassword();
                _userRepository.Update(dbRecord);
            }

            return RedirectToAction("Index");
        }

        private List<SelectListItem> GetUserRoleListItems()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Admin", Value = ((int)UserRole.Admin).ToString() },
                new SelectListItem { Text = "Author", Value = ((int) UserRole.Author).ToString() },
                new SelectListItem { Text = "Reader", Value = ((int) UserRole.Reader).ToString() }
            };
        }
    }
}