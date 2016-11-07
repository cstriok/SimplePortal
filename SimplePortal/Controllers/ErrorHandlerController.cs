using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimplePortal.Controllers
{
    public class ErrorHandlerController : Controller
    {
        public ActionResult ShowError(string errorMessage)
        {
            return View(errorMessage);
        }
    }
}