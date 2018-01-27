using Microsoft.AspNet.Identity;
using System;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;
using System.Net.Mail;
using System.Web.Http;
using System.Security.Principal;
using System.Reflection;
using Biz.Filters;

namespace Biz.Base
{
    public class _MvcController : Controller
    {
         public _MvcController()
        {
        }

        protected virtual string GetAppName()
        {
            throw new Exception("GetAppName has not implemented!");
        }

        protected virtual string GetAppSite()
        {
            throw new Exception("GetGetAppSite has not implemented!");
        }

        protected virtual string GetDisplayVersion()
        {
            throw new Exception("GetDisplayVersion has not implemented!");
        }

        protected virtual List<IISMeta> GetMasterList()
        {
            throw new Exception("GetMasterList has not implemented!");
        }

        protected virtual List<IISMeta> GetMetaList()
        {
            throw new Exception("GetMetaList has not implemented!");
        }

        protected string GetConfigParam(string name)
        {
            try
            {
                return ConfigurationManager.AppSettings[name].ToString();
            }
            catch (Exception ee)
            {
                _HttpApplication.IIS_LicenseIssue = ee.Message;
                throw new Exception("Load App Settings : " + name + " - " + ee.Message);
            }
        }
    }
}