﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APIMS.WebUI.Controllers
{
    public class ModuleController : BaseController
    {
        // GET: Module
        public ActionResult Index()
        {
            return View();
        }

        // GET: Module/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Module/Create
        public ActionResult Create()
        {
            CreateAcion();
            Domain.Module m = new Domain.Module();
            return View(m);
        }

        // POST: Module/Create
        [HttpPost]
        public ActionResult Create(Domain.Module module, FormCollection collection)
        {
            return CreateModel(module);
            //JsonResult ret = new JsonResult();
            //try
            //{
            //    module.CreatorID = (int)LoginUser.ID;
            //    module.CreationTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            //    db.Module.Add(module);
            //    db.SaveChanges();
            //    ret.Data = JsonConvert.SerializeObject(new
            //    {
            //        status = 0,
            //        message = ""
            //    });
            //}
            //catch (Exception ex)
            //{
            //    ret.Data = JsonConvert.SerializeObject(new
            //    {
            //        status = 1,
            //        message = ex.Message
            //    });
            //}
            //return ret;
        }

        // GET: Module/Edit/5
        public ActionResult Edit(int id)
        {
            EditAcion();
            var m = db.Module.Find(id);
            return View("Create", m);
        }

        // POST: Module/Edit/5
        [HttpPost]
        public ActionResult Edit(Domain.Module module, FormCollection collection)
        {
            return EditModel(module);
            //JsonResult ret = new JsonResult();
            //try
            //{
            //    var editModule = db.Module.Find(module.ID);

            //    editModule.MName = module.MName;
            //    editModule.MUrl = module.MUrl;
            //    editModule.MType = module.MType;
            //    editModule.IsEnabled = module.IsEnabled;
            //    editModule.MIconType = module.MIconType;
            //    editModule.MIcon = module.MIcon;
            //    editModule.MLevel = module.MLevel;
            //    editModule.MParentID = module.MParentID;
            //    editModule.MSortingNumber = module.MSortingNumber;

            //    editModule.EditorID = (int)LoginUser.ID;
            //    editModule.EditingTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            //    db.SaveChanges();
            //    ret.Data = JsonConvert.SerializeObject(new
            //    {
            //        status = 0,
            //        message = ""
            //    });
            //}
            //catch (Exception ex)
            //{
            //    ret.Data = JsonConvert.SerializeObject(new
            //    {
            //        status = 1,
            //        message = ex.Message
            //    });
            //}
            //return ret;
        }

        // GET: Module/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Module/Delete/5
        [HttpPost]
        public ActionResult Delete(List<int> idList)
        {
            return DeleteModel<Domain.Module>(idList);
            //JsonResult ret = new JsonResult();
            //try
            //{
            //    foreach (var id in idList)
            //    {
            //        var r = db.Module.Find(id);
            //        db.Module.Remove(r);
            //    }
            //    db.SaveChanges();
            //    ret.Data = JsonConvert.SerializeObject(new
            //    {
            //        status = 0,
            //        message = ""
            //    });
            //}
            //catch (Exception ex)
            //{
            //    ret.Data = JsonConvert.SerializeObject(new
            //    {
            //        status = 1,
            //        message = ex.Message
            //    });
            //}
            //return ret;
        }

        public ActionResult List(int page = 1, int limit = 20, string type = "")
        {
            JsonResult ret = new JsonResult();
            ret.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            var modules = db.Module.ToList();
            if (!string.IsNullOrWhiteSpace(type))
            {
                modules = modules.Where(m => m.MName.Contains(type) || m.MType.Contains(type)).ToList();
            }
            if (!string.IsNullOrWhiteSpace(type))
            {
                modules = modules.Where(m => m.MType.Contains(type)).ToList();
            }
            int cnt = modules.Count();
            modules = modules.Skip((page - 1) * limit)
                        .Take(limit)
                        .ToList();
            try
            {
                ret.Data = JsonConvert.SerializeObject(new
                {
                    status = 0,
                    message = "",
                    total = cnt,
                    data = modules
                });
            }
            catch (Exception ex)
            {
                ret.Data = JsonConvert.SerializeObject(new
                {
                    status = 1,
                    message = "发生异常：" + ex.Message,
                    total = 0,
                    data = ""
                });
                RecordException(ex);
            }
            return ret;
        }

        public ActionResult AuthorityAllocation(int id)
        {
            var auth = db.Authority.ToList();
            ViewBag.MID = id;
            var ownCode = (from ma in db.ModuleAuthority
                          join m in db.Module on ma.MID equals m.ID
                          join a in db.Authority on ma.AID equals a.ID
                          where ma.MID == id
                          select a.ACode).ToList();
            ViewBag.OwnCode = ownCode;//JsonConvert.SerializeObject(ownCode);
            return View(auth);
        }
        [HttpPost]
        public ActionResult AuthorityAllocation(int mId, FormCollection collection)
        {            
            JsonResult ret = new JsonResult();
            try
            {
                var allAutho = db.Authority.ToList();
                //对于只提交选中项的问题，全部删除，再增加
               
                var mas = db.ModuleAuthority.Where(ma => ma.MID == mId);
                db.ModuleAuthority.RemoveRange(mas);
                //立即保存
                foreach (var a in allAutho)
                {
                    var formPara = collection[a.ACode];
                    if (formPara == "1")
                    {
                        db.ModuleAuthority.Add(new Domain.ModuleAuthority {
                            MID = mId,
                            AID = a.ID
                        });
                        /*
                        var ma = (from m in db.ModuleAuthority
                                  where m.AID == a.ID && m.MID == mId
                                  select m).FirstOrDefault();
                        if (formPara == "1")
                        {
                            //选中不存在则添加
                            if (ma == null)
                                db.ModuleAuthority.Add(new Domain.ModuleAuthority
                                {
                                    MID = mId,
                                    AID = a.ID
                                });
                        }
                        else if (formPara == "0")
                        {
                            //取消存在则删除
                            if (ma != null)
                            {
                                db.ModuleAuthority.Remove(ma);
                            }
                        }*/                      
                        
                    }
                }
                db.SaveChanges();
                ret.Data = JsonConvert.SerializeObject(new
                {
                    status = 0,
                    message = ""
                });
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
