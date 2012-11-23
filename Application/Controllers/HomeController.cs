//-----------------------------------------------------------------------
// <author>Sidharth Balakrishnan</author>
// <summary>
//     Create Date : 23 Nov 2012 
// </summary>
//-----------------------------------------------------------------------

namespace Application.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// Controller for homepage
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Action for homepage
        /// </summary>
        /// <returns>Returns homeview</returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}
