using SimplePortal.Areas.Admin.Models;
using SimplePortal.Db.Ef;
using SimplePortal.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimplePortal.DomainModel.Crypto;

namespace SimplePortal.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private readonly IEfRepository<User> _userRepository = null;

        private readonly IPasswordHasher _passwordHasher = null;

        private readonly IPasswordChecker _passwordChecker = null;

        public UserController(IEfRepository<User> userRepository, IPasswordHasher passwordHasher, IPasswordChecker passwordChecker)
        {
            if (userRepository == null)
            {
                throw new ArgumentNullException(nameof(userRepository));
            }

            if (passwordHasher == null)
            {
                throw new ArgumentNullException(nameof(passwordHasher));
            }

            if (passwordChecker == null)
            {
                throw new ArgumentNullException(nameof(passwordChecker));
            }

            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _passwordChecker = passwordChecker;
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
                _passwordHasher.ClearTextPassword = entity.Password;

                entity.Password = _passwordHasher.HashedPassword;
                
                _userRepository.Create(entity);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditUser(Guid uid)
        {
            User dbRecord = _userRepository.FindRecord(uid);

            if (dbRecord != null)
            {
                User userToEdit = new User
                {
                    Password = null,
                    Role = dbRecord.Role,
                    Login = dbRecord.Login,
                    LastName = dbRecord.LastName,
                    Mail = dbRecord.Mail,
                    FirstName = dbRecord.FirstName,
                    Uid = dbRecord.Uid,
                    Id = dbRecord.Id
                };

                ViewBag.UserRoleListItems = GetUserRoleListItems();
                return View(userToEdit);
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
                if (CheckPasswordsAreEqual(dbRecord.Password, model.OldPassword))
                {
                    _passwordHasher.ClearTextPassword = model.NewPassword;
                    dbRecord.Password = _passwordHasher.HashedPassword;

                    _userRepository.Update(dbRecord);
                }
                else
                {
                    return RedirectToAction("ChangePassword", new {uid = model.Uid});
                }
            }

            return RedirectToAction("Index");
        }

        private bool CheckPasswordsAreEqual(string passwordHash, string passwordClearText)
        {
            if (!string.IsNullOrEmpty(passwordHash) &&
                !string.IsNullOrEmpty(passwordClearText))
            {
                _passwordChecker.ClearTextPassword = passwordClearText;
                _passwordChecker.HashedPassword = passwordHash;
                return _passwordChecker.PasswordCheckOk;
            }

            if (string.IsNullOrEmpty(passwordHash) && 
                string.IsNullOrEmpty(passwordClearText))
            {
                return true;
            }

            return false;
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