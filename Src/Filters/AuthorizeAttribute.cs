using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Biz.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.Result = new System.Web.Mvc.HttpStatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
            }
            else
            {
                var isAjax = filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
                if (isAjax)
                {
                    if (filterContext.ActionDescriptor.ActionName.Equals("PendingJournals"))
                    {
                        var urlHelper = new UrlHelper(filterContext.RequestContext);
                        filterContext.HttpContext.Response.StatusCode = 200; // SHould show small message
                        //Because its a exception raised after ajax invocation
                        //Lets return Json
                        filterContext.Result = new JsonResult()
                        {
                            Data = "<span style='color:white'>Not Authorized!</span>",
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
                    else
                    {
                        base.HandleUnauthorizedRequest(filterContext);
                    }
                }
                else
                {
                    base.HandleUnauthorizedRequest(filterContext);
                }
            }
        }
    }
}