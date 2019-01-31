﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using APIMS.Domain;
using APIMS.WebUI.Infrastructure.Abstract;
using APIMS.WebUI.Infrastructure.Concrete;
using Newtonsoft.Json;

namespace APIMS.WebUI.Controllers
{
    public class AccountController : BaseController
    {
        private IAuthProvider _authProvider = new FormsAuthProvider();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            User u = new Domain.User();
            //获取Cookie
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                if (ticket.Expired == false)
                {
                    u.ULoginName = ticket.Name;
                    u.UPassword = "***";
                }
            }
            //系统设置
            List<Dictionary> dicSys = db.Dictionary.Where(d=> d.DTypeCode == "SysSettings" && d.DEnable == 1).ToList();
            ViewBag.SystemSettings = dicSys;
            return View(u);
        }
        [HttpPost]
        public ActionResult Login(User user, string rememberme = "")
        {
            JsonResult ret = new JsonResult();
            try
            {
                bool bRememerMe = rememberme == "on";
                string msg = "";
                bool cookieOK = false;
                int code = -1;
                //获取Cookie
                var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (cookie != null)
                {
                    var ticket = FormsAuthentication.Decrypt(cookie.Value);
                    //是否为本用户
                    if(ticket.Name == user.ULoginName)
                    {
                        if (ticket.Expired)
                        {
                            msg = "用户信息已过期！";
                        }
                        else
                        {
                            if(_authProvider.IsUserExisted(ticket.Name))
                                cookieOK = true;
                            if (_authProvider.IsUserLocked(ticket.Name))
                            {
                                throw new InvalidOperationException("用户已锁定！");
                            }
                        }
                    }
                }
                if(cookieOK == false)
                {
                    if (_authProvider.Authenticate(user.ULoginName, user.UPassword))
                    {
                        if (_authProvider.IsUserLocked(user.ULoginName))
                        {
                            throw new InvalidOperationException("用户已锁定！");
                        }
                        if (bRememerMe)
                        {
                            _authProvider.SetAuthCookie(Response, user.ULoginName);
                        }
                        else
                        {
                            /*为用户名创建一个身份验证票据，并将其添加到响应的Cookie中
                        * SetAuthCookie的第一个参数为已验证的用户的名称。 
                            *SetAuthCookie的第二个参数为true时代表创建持久Cookie（跨浏览器会话保存的 Cookie），为false则关闭浏览器后要重新验证身份
                        */
                            FormsAuthentication.SetAuthCookie(user.ULoginName, false);
                        }
                        code = 0;
                        msg = "登录成功";
                    }
                    else
                    {
                        code = 1;
                        msg = "用户名或密码错误";
                    }
                }
                else
                {
                    code = 0;
                    msg = "Cookie验证成功";
                }
                ret.Data = new
                {
                    status = code,
                    message = msg
                };
            }
            catch(Exception ex)
            {
                ret.Data = new
                {
                    status = 2,
                    message = ex.Message
                };
            }
            return ret;
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult ModifyPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ModifyPassword(string oldpwd, string newpwd, string newpwdrepeat)
        {
            JsonResult ret = new JsonResult();
            try
            {
                if (LoginUser.UPassword != oldpwd)
                {
                    ret.Data = JsonConvert.SerializeObject(new
                    {
                        status = 2,
                        message = "原密码不正确"
                    });
                }
                else
                {
                    var user = db.User.Find(LoginUser.ID);
                    user.UPassword = newpwd;
                    db.SaveChanges();
                    ret.Data = JsonConvert.SerializeObject(new
                    {
                        status = 0,
                        message = ""
                    });
                }
            }
            catch (Exception ex)
            {
                ret.Data = JsonConvert.SerializeObject(new
                {
                    status = 1,
                    message = ex.Message
                });
                RecordException(ex);
            }
            return ret;
        }
    }
}