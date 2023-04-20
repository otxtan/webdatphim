using datphim.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace datphim.Controllers
{
    [customFilter]
    public class AccountController : Controller
    {
        private datphimchuanEntities db = new datphimchuanEntities();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [OverrideAuthorization]
        public ActionResult Login(string username, string password)
        {
            if (Session["UserName"] != null)
            {
                return RedirectToAction("Trangchu", "Home");
            }
            else
                return View();
        }

        //Post: Login

        [OverrideAuthorization]
        [HttpPost]
        public ActionResult Login([Bind(Include = "UserName,PassWord")] Tb_NguoiDung tb_NguoiDung)
        {
            string hashedPassword = "";
            string salt = "";
            if (ModelState.IsValidField("UserName") && ModelState.IsValidField("PassWord"))
            {
                string pass = tb_NguoiDung.PassWord.ToString().Trim();
                salt = (db.Tb_NguoiDung.Where(s => s.UserName == tb_NguoiDung.UserName).Select(s => s.Salt).FirstOrDefault());
                if (salt != null)
                    hashedPassword = BCrypt.Net.BCrypt.HashPassword(pass, salt).ToString();
                var data = db.Tb_NguoiDung.Where(s => s.UserName.Equals(tb_NguoiDung.UserName.ToString().Trim()) && s.PassWord.Equals(hashedPassword)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["UserName"] = data.FirstOrDefault().UserName;
                    ViewBag.dangnhap = "thành công";
                    if ((tb_NguoiDung.UserName.ToString().Trim()).Equals("admin"))
                        return RedirectToAction("Index", "admin/Tb_Phim");
                    return RedirectToAction("Trangchu", "Home");
                }
                else
                {
                    ViewBag.dangnhap = "Đăng nhập thất bại";
                    return View();
                }
            }

            return View("Login");
        }
        // get register user 

        [OverrideAuthorization]
        public ActionResult Register()
        {
            return RedirectToAction("Login");
        }
        //Post register user
        [HttpPost]
        /*[OverrideAuthorization]*/
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "UserName,PassWord,TenKH,Email")] Tb_NguoiDung tb_NguoiDung)
        {
            /*  try
              {
  */

            if (ModelState.IsValidField("UserName") && ModelState.IsValidField("PassWord") && ModelState.IsValidField("TenKH") && ModelState.IsValidField("Email"))
            {
                ViewBag.dangki = "";
                string pass = tb_NguoiDung.PassWord.ToString().Trim();
                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(pass, salt).ToString();
                tb_NguoiDung.PassWord = hashedPassword;
                tb_NguoiDung.Salt = salt;
                var data = db.Tb_NguoiDung.Where(s => s.UserName.Equals(tb_NguoiDung.UserName.ToString().Trim())).ToList();
                if (data.Count() == 0)
                {
                    //add session
                    TempData["dangki"] = "đăng kí thành công";

                    db.Tb_NguoiDung.Add(tb_NguoiDung);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["dangki"] = "username đã tồn tại";
                    return View("Login");
                }
            }

            return View("Login");
        }

        //get delete session 
        [OverrideAuthorization]
        public ActionResult Logout()
        {
            Session.Remove("UserName");
            return RedirectToAction("TrangChu", "Home");
        }
        public ActionResult EditPass()
        {
            Tb_NguoiDung nguoidung = new Tb_NguoiDung();
            return View();
        }

        //post change password 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPass([Bind(Include = "PassWord")] Tb_NguoiDung nguoidung)
        {
            if (ModelState.IsValidField("PassWord"))
            {
                /* try
                 {*/
                string user = Session["UserName"].ToString().Trim();
                string salt = "";
                string pass = "";
                salt = db.Tb_NguoiDung.Where(s => s.UserName == user).Select(s => s.Salt).FirstOrDefault();
                if (salt != null)
                    pass = BCrypt.Net.BCrypt.HashPassword(nguoidung.PassWord.Trim(), salt.Trim()).ToString();
                Tb_NguoiDung tb_NguoiDung = db.Tb_NguoiDung.Find(user);

                tb_NguoiDung.TenKH = tb_NguoiDung.TenKH.Trim();
                tb_NguoiDung.Email = tb_NguoiDung.Email.Trim();
                tb_NguoiDung.PassWord = pass;
                db.SaveChanges();
                return RedirectToAction("Logout");
                /*  }
                  catch (DbEntityValidationException ex)
                  {
                      foreach (var error in ex.EntityValidationErrors)
                      {
                          foreach (var validationError in error.ValidationErrors)
                          {
                              System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                          }
                      }
                  }*/

            }
            /*return RedirectToAction("Users");*/
            return View();


        }

        //get user
        public ActionResult Users()
        {

            string user = Session["UserName"].ToString();
            Tb_NguoiDung tb_NguoiDung = db.Tb_NguoiDung.Find(user);
            var queryHoaDon = (from s in db.Tb_HoaDon.Where(s => s.UserName == user).Where(s => s.TrangThai == true) select s);
            var lichsu1 = (from v in db.Tb_Ve join h in queryHoaDon on v.Ma_HoaDon equals h.Ma_HoaDon select v);
            ViewBag.ls = lichsu1.ToList();
            tb_NguoiDung.SDT = tb_NguoiDung.SDT;
            tb_NguoiDung.Email = tb_NguoiDung.Email.Trim();
            return View(tb_NguoiDung);

        }
        //get editUser
        public ActionResult EditUser()
        {

            string user = Session["UserName"].ToString();
            Tb_NguoiDung tb_NguoiDung = db.Tb_NguoiDung.Find(user);

            tb_NguoiDung.TenKH = tb_NguoiDung.TenKH.Trim();
            tb_NguoiDung.SDT = tb_NguoiDung.SDT.Trim();
            tb_NguoiDung.Email = tb_NguoiDung.Email.Trim();
            return View(tb_NguoiDung);
        }
        //post change information user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser([Bind(Include = "TenKH,SDT,Email")] Tb_NguoiDung tb_nguoiDung)
        {
            if (ModelState.IsValidField("TenKH") && ModelState.IsValidField("Email"))
            {
                string user = Session["UserName"].ToString().Trim();
                Tb_NguoiDung nguoiDung = db.Tb_NguoiDung.Find(user);
                nguoiDung.TenKH = tb_nguoiDung.TenKH.Trim();
                nguoiDung.SDT = tb_nguoiDung.SDT;
                nguoiDung.Email = tb_nguoiDung.Email.Trim();
                /*db.Entry(nguoiDung).State = EntityState.Modified;*/
                db.SaveChanges();
            }
            return RedirectToAction("Users");

        }
        //get history payment
        public ActionResult History()
        {

            return View();
        }


    }
}