using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Host.SystemWeb;
using System.Web.Http;
using System.Security.Principal;
using System.Web.Http.Results;
using System.Net.Http.Headers;
using System.Configuration;
using System.Net;
using System.Net.Http;

namespace Biz.Base
{
    public class _ApiController : ApiController
    {
        public string CurentUserName { 
            get{
                return User.Identity.Name;
            } 
        }


        public IPrincipal User
        {
            get
            {
                return base.User;
            }
        }
    }
}