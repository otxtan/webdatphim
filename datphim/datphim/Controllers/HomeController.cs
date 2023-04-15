using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using datphim.Models;
using System.Data.Entity;

namespace datphim.Controllers
{
    public class HomeController : Controller
    {
        private datphimchuanEntities db = new datphimchuanEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Trangchu()
        {
            var tb_phim = from s in db.Tb_phim where s.available==true select s;
            return View(tb_phim.ToList());

        }
        public ActionResult Details(long id)
        {
            if ( id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Tb_phim tb_phim = db.Tb_phim.Find( id);
             var sxNgayChieu= ((from s in db.Tb_LichChieu_PhongChieu orderby s.NgayChieu  where s.Ma_Phim == id && s.available==true select s.NgayChieu ).Distinct()).ToList();
            var sxGioChieu = (from s in db.Tb_LichChieu_PhongChieu orderby s.NgayChieu,s.GioChieu where s.Ma_Phim == id && s.available == true select s).ToList();
            ViewBag.LichChieu = sxNgayChieu;
            ViewBag.GioChieu = sxGioChieu;
            if (tb_phim == null)
            {
                return HttpNotFound();
            }
           
            return View(tb_phim);
        }
  
        public ActionResult ChonGhe(long Ma_LichChieu_PhongChieu,string Ma_PhongChieu)
        {
            TempData["Ma_LichChieu_PhongChieu"] = Ma_LichChieu_PhongChieu;
            TempData["Ma_PhongChieu"] = Ma_PhongChieu;

            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login");
            }
            var tb_maghe = from s in db.Tb_PhongGhe where s.Ma_PhongChieu == Ma_PhongChieu select s;
            ViewBag.tbmaghe = (from s in db.Tb_PhongGhe where s.Ma_PhongChieu == Ma_PhongChieu select s).ToList();

                var ghedat= (from s in db.Tb_Ve where s.Ma_LichChieu_PhongChieu==Ma_LichChieu_PhongChieu select s.Ma_PhongGhe).ToList();
            List<long> lst = new List<long>();
          
            foreach(long item in ghedat.Cast<long>().ToList())
            {
                lst.Add(item);
            }   
            
            ViewBag.gheDaDat= lst;
            return View(tb_maghe.ToList());
            
        }  
        public ActionResult DetailsLichChieu(long id)
        {
            
            var tb_lichchieutheomaphim = from s in db.Tb_LichChieu_PhongChieu where s.Ma_Phim==id  select s;
           
            return PartialView();
        }
        // GET: admin/Tb_NguoiDung/Register
        public ActionResult Register()
        {
            return RedirectToAction("Login");
        }

        // POST: admin/Tb_NguoiDung/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "UserName,PassWord,TenKH,Email")] Tb_NguoiDung tb_NguoiDung)
        {
            if (ModelState.IsValid)
            {
                ViewBag.dangki = "";
                string pass = tb_NguoiDung.PassWord == null ? "" : tb_NguoiDung.PassWord.ToString().Trim();
                string f_password = GetMD5(pass);
                tb_NguoiDung.PassWord = f_password;
                var data = db.Tb_NguoiDung.Where(s => s.UserName.Equals(tb_NguoiDung.UserName.ToString().Trim()) ).ToList();
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
                    return RedirectToAction("Login");
                }
            }

            return View(tb_NguoiDung);
        }

        public ActionResult Login()
        {
            if (Session["UserName"] != null)
            {
                return RedirectToAction("Trangchu");
            }
            else
            return View();
        }

        public ActionResult Users()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login");
            }
            
            string user = Session["UserName"].ToString();
            Tb_NguoiDung tb_NguoiDung = db.Tb_NguoiDung.Find(user);
            var lichsu = db.Tb_Ve.Include(t => t.Tb_LichChieu_PhongChieu).Include(t=>t.Tb_HoaDon).Include(t=>t.Tb_PhongGhe).Where(t=>t.Tb_HoaDon.UserName==user);
            ViewBag.ls = lichsu.ToList();
            return View(tb_NguoiDung);

        }
        public ActionResult History()
        {
            string user = Session["UserName"].ToString();
            var tb_HoaDon = db.Tb_HoaDon.Include(t => t.Tb_LichChieu_PhongChieu).Include(t => t.Tb_NguoiDung).Where(s => s.UserName == user);

            return View(tb_HoaDon.ToList());

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser([Bind(Include = "TenKH,SDT,Email")] Tb_NguoiDung tb_NguoiDung)
        {
            if (Session["UserName"] == null)
                return RedirectToAction("Login", "Home", new { area = "" });
            string user = Session["UserName"].ToString().Trim();
            
            if (ModelState.IsValid)
            {
                Tb_NguoiDung tb_NguoiDung1= db.Tb_NguoiDung.Find(user);
                tb_NguoiDung1.TenKH = tb_NguoiDung.TenKH;
                tb_NguoiDung1.SDT = tb_NguoiDung.SDT;
                tb_NguoiDung1.Email = tb_NguoiDung.Email;
                db.Entry(tb_NguoiDung1).State = EntityState.Modified;
                db.SaveChanges();
                
            }
            return RedirectToAction("Users");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPass(string passcu,string passmoi)
        {
            if (Session["UserName"] == null)
                return RedirectToAction("Login", "Home", new { area = "" });
            string user = Session["UserName"].ToString().Trim();
           
            var f_password = GetMD5(passcu.Trim());
            var f_password1 = GetMD5(passmoi.Trim());
            var data = db.Tb_NguoiDung.Where(s => s.UserName.Equals(user) && s.PassWord.Equals(f_password)).ToList();
            if (data.Count() > 0)
            {
                
                    Tb_NguoiDung tb_NguoiDung = db.Tb_NguoiDung.Find(user);

                    tb_NguoiDung.PassWord = f_password1;
                    db.Entry(tb_NguoiDung).State = EntityState.Modified;
                    db.SaveChanges();


                
                return RedirectToAction("Logout");
            }
            else
         
            
            
            return RedirectToAction("Users");
        }
        public ActionResult Logout()
        {
            Session.Remove("UserName");
            return RedirectToAction("TrangChu");
        }



        [HttpPost]
        public ActionResult Login([Bind (Include ="UserName,PassWord")] Tb_NguoiDung tb_NguoiDung)
        {

            if (ModelState.IsValid)
            {
                string pass=tb_NguoiDung.PassWord==null? "": tb_NguoiDung.PassWord.ToString().Trim();
                var f_password = GetMD5(pass);
                var data = db.Tb_NguoiDung.Where(s => s.UserName.Equals(tb_NguoiDung.UserName.ToString().Trim()) && s.PassWord.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                  
                    Session["UserName"] = data.FirstOrDefault().UserName;
                    ViewBag.dangnhap = "thành công";
                    if ((tb_NguoiDung.UserName.ToString().Trim()).Equals("admin"))
                        return RedirectToAction("Index", "admin/Tb_Phim");
                    return RedirectToAction("Trangchu");
                }
                else
                {
                    ViewBag.dangnhap = "Đăng nhập thất bại";
                    return View();
                }
            }
           
            return View();
        }
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

    }
}