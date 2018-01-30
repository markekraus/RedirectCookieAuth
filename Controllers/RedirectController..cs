using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Primitives;

namespace RedirectCookieAuth.Controllers
{
    [Route("api/[controller]")]
    public class RedirectController : Controller
    {
        [HttpGet]
        public JsonResult Get()
        {
            Response.Cookies.Append("special","test123");
            Response.StatusCode = 302;
            string url = Regex.Replace(input: Request.GetDisplayUrl(), pattern: "\\/Redirect.*", replacement: "", options: RegexOptions.IgnoreCase);
            url = $"{url}/Get/";
            Response.Headers.Add("Location", url);
            return Json(null);
        }
    }
}
