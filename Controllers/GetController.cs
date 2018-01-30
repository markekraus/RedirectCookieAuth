using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Extensions;

namespace RedirectCookieAuth.Controllers
{
    [Route("api/[controller]")]
    public class GetController : Controller
    {
        [HttpGet]
        public JsonResult Get()
        {
            Hashtable args = new Hashtable();
            foreach (var key in Request.Query.Keys)
            {
                args.Add(key, String.Join(", ", Request.Query[key]));
            }
            Hashtable headers = new Hashtable();
            foreach (var key in Request.Headers.Keys)
            {
                headers.Add(key, String.Join(", ", Request.Headers[key]));
            }
            Hashtable output = new Hashtable
            {
                {"args"   , args},
                {"headers", headers},
                {"origin" , Request.HttpContext.Connection.RemoteIpAddress.ToString()},
                {"url"    , UriHelper.GetDisplayUrl(Request)},
                {"method" , Request.Method}
            };

            if (Request.HasFormContentType)
            {
                Hashtable form = new Hashtable();
                foreach (var key in Request.Form.Keys)
                {
                    form.Add(key,Request.Form[key]);
                }
                output["form"] = form;
            }

            string data = new StreamReader(Request.Body).ReadToEnd();
            if (!String.IsNullOrEmpty(data))
            {
                output["data"] = data;
            }

            return Json(output);
        }
    }
}
