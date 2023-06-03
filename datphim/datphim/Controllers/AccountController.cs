using datphim.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Caching;



namespace datphim.Controllers
{
    public class CodeInfo
    {
        public string Code { get; set; }
        public DateTime Expiration { get; set; }
    }
    [customFilter]
    public class AccountController : Controller
    {
        private datphimchuanEntities db = new datphimchuanEntities();
        private Dictionary<string, CodeInfo> userCodeMap = new Dictionary<string, CodeInfo>();
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
                    /*ViewBag.dangnhap = "thành công";*/
                    if ((tb_NguoiDung.UserName.ToString().Trim()).Equals("admin"))
                        return RedirectToAction("Index", "admin/Tb_Phim");
                    return RedirectToAction("Trangchu", "Home");
                }
                else
                {
                    TempData["notification"] = "Đăng nhập thất bại";
                    /*ViewBag.dangnhap = "Đăng nhập thất bại";*/
                    return View();
                }
            }

            return View("Login");
        }
        // get register user 

        [OverrideAuthorization]
        public ActionResult Register()
        {
            return View("Login");
        }
        //Post register user
        [HttpPost]
        [OverrideAuthorization]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "UserName,PassWord,TenKH,Email")] Tb_NguoiDung tb_NguoiDung)
        {
            if (ModelState.IsValidField("UserName") && ModelState.IsValidField("PassWord") && ModelState.IsValidField("TenKH") && ModelState.IsValidField("Email"))
            {
                ViewBag.dangki = "";
                string pass = tb_NguoiDung.PassWord.ToString().Trim();
                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(pass, salt).ToString();
                tb_NguoiDung.PassWord = hashedPassword;
                tb_NguoiDung.Salt = salt;
                var data = db.Tb_NguoiDung.Where(s => s.UserName.Equals(tb_NguoiDung.UserName.ToString().Trim()));
                if (data.Count() < 0)
                {
                    TempData["notification"] = "username đã tồn tại";
                    return View("Login");

                }
                data = db.Tb_NguoiDung.Where(s => s.Email.Equals(tb_NguoiDung.Email.ToString().Trim()));
                if (data.Count() > 0)
                {
                    TempData["notification"] = "email đã tồn tại";
                    return View("Login");

                }
                TempData["notification"] = "đăng kí thành công";
                db.Tb_NguoiDung.Add(tb_NguoiDung);
                db.SaveChanges();
                return RedirectToAction("Login");
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
        // get change password
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
        // post forget password
        [HttpPost]
        [OverrideAuthorization]
        public ActionResult SendCode(string email)
        {
            try
            {
                //check mail exist
                if (email == null)
                {
                    TempData["notification"] = "Vui lòng nhập email";
                    return RedirectToAction("Login");
                }
                var data = db.Tb_NguoiDung.Where(s => s.Email.Equals(email.Trim())).ToList();
                if (data.Count() >= 1)
                {
                    string fromEmail = ConfigurationManager.AppSettings["from_Mail"];
                    string fromPassword = ConfigurationManager.AppSettings["password_Mail"];
                    string subject = "change password";
                    string userCode = GenerateCode();
                    DateTime expiration = DateTime.Now.AddMinutes(15); // Thời hạn là 15 phút
                    CodeInfo codeInfo = new CodeInfo { Code = userCode, Expiration = expiration };
                    userCodeMap.Add(email, codeInfo);

                    var cachedDictionary = HttpContext.Cache["Dictionary"] as Dictionary<string, CodeInfo>;
                    if (cachedDictionary != null)
                    {
                        foreach (var entry in cachedDictionary.ToList())
                        {

                            CodeInfo cI = entry.Value;
                            string code = codeInfo.Code;
                            DateTime expirationTime = cI.Expiration;

                            if (expirationTime <= DateTime.Now)
                            {
                                cachedDictionary.Remove(entry.Key);
                            }
                        }
                        if (!cachedDictionary.ContainsKey(email))
                        {
                            cachedDictionary.Add(email, codeInfo);
                        }
                        else
                        {
                            cachedDictionary.Remove(email);
                            cachedDictionary.Add(email, codeInfo);
                        }

                        HttpContext.Cache["Dictionary"] = cachedDictionary;
                    }
                    else
                    {
                        HttpContext.Cache["Dictionary"] = userCodeMap;
                    }
                    // body contain link forget

                    string emailBody = @" <!DOCTYPE html> <html> <head> <title>Đặt lại mật khẩu</title> <style> body { font-family: Arial, sans-serif; } h1 { color: #333; } p { line-height: 1.5; } .security-code { display: inline-block; padding: 5px 10px; background-color: #f5f5f5; border: 1px solid #ccc; font-family: monospace; font-size: 16px; color: #333; } </style> </head> <body> <h1>Đặt lại mật khẩu</h1> <p>Chúng tôi đã nhận được yêu cầu đặt lại mật khẩu cho tài khoản của bạn.</p> <p>Vui lòng sử dụng mã bảo mật sau để đặt lại mật khẩu mới:</p> <hr> <p>Mã bảo mật: <span class=""security-code"">" + userCode + @"</span></p> <hr> <p>Vui lòng nhập mã bảo mật trên vào trang đặt lại mật khẩu của ứng dụng.</p> <p>Nếu bạn không yêu cầu đặt lại mật khẩu, vui lòng bỏ qua email này.</p> <p>Cảm ơn bạn và chúc một ngày tốt lành!</p> </body> </html>";
                    SmtpClient smtpClient = new SmtpClient("smtp.office365.com", 587);
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(fromEmail, fromPassword);

                    MailMessage mail = new MailMessage(fromEmail, email, subject, emailBody);
                    mail.IsBodyHtml = true;
                    smtpClient.Send(mail);

                }
                else
                {
                    TempData["notification"] = "Mail chưa chính xác!";
                    return RedirectToAction("Login");
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }



            return Json(new { success = true });

        }
        // get forget password
        [HttpPost]
        [OverrideAuthorization]
        public ActionResult ForgetPassWord(string emailInput, string confirmCode, string newPassword, string confirmPassword)
        {

            if (emailInput == null || confirmCode == null || newPassword == null || confirmPassword == null)
                return RedirectToAction("login");
            string patternEmail = @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}";
            string patternPassword = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,60}$";

            // Kiểm tra khớp mẫu
            bool isMatchEmail = Regex.IsMatch(emailInput, patternEmail);
            bool isMatchPassword = Regex.IsMatch(confirmPassword, patternPassword);
            if (!isMatchEmail)
            {
                TempData["notification"] = "Email không hợp lệ";
                return RedirectToAction("login");
            }
            if (!isMatchPassword)
            {
                TempData["notification"] = "Mật khẩu không hợp lệ";
                return RedirectToAction("login");
            }
            var cachedDictionary = HttpContext.Cache["Dictionary"] as Dictionary<string, CodeInfo>;
            if (cachedDictionary != null)
            {
                if (cachedDictionary.ContainsKey(emailInput))
                {
                    CodeInfo codeInfo = cachedDictionary[emailInput];
                    string code = codeInfo.Code;
                    DateTime expirationTime = codeInfo.Expiration;
                    if (!confirmCode.Equals(code))
                    {
                        TempData["notification"] = "mã không hợp lệ";
                        return RedirectToAction("login");
                    }
                    if (expirationTime <= DateTime.Now)
                    {
                        TempData["notification"] = "mã hết hạn";
                        return RedirectToAction("login");
                    }
                    if (!confirmPassword.Equals(newPassword))
                    {
                        TempData["notification"] = "Mật khẩu không khớp";
                        return RedirectToAction("login");
                    }
                    cachedDictionary.Remove(emailInput);
                    HttpContext.Cache["Dictionary"] = cachedDictionary;

                    string salt = "";
                    string pass = "";

                    Tb_NguoiDung user = db.Tb_NguoiDung.Where(s => s.Email == emailInput.Trim()).FirstOrDefault();
                    salt = user.Salt;
                    if (salt != null)
                        pass = BCrypt.Net.BCrypt.HashPassword(newPassword.Trim(), salt.Trim()).ToString();
                    user.TenKH = user.TenKH.Trim();
                    user.Email = user.Email.Trim();

                    user.PassWord = pass.Trim();
                    db.SaveChanges();


                }
                else
                {
                    TempData["notification"] = "mã không hợp lệ";
                    return RedirectToAction("login");
                }

            }
            else
            {
                TempData["notification"] = "mã không hợp lệ";
                return RedirectToAction("login");
            }


            TempData["notification"] = "khôi phục mật khẩu thành công";
            return RedirectToAction("login");
        }

        //get user
        public ActionResult Users()
        {

            string user = Session["UserName"].ToString();
            Tb_NguoiDung tb_NguoiDung = db.Tb_NguoiDung.Find(user);
            var queryHoaDon = (from s in db.Tb_HoaDon.Where(s => s.UserName == user).Where(s => s.TrangThai == true) select s);
            var lichsu1 = (from v in db.Tb_Ve join h in queryHoaDon on v.Ma_HoaDon equals h.Ma_HoaDon select v).OrderByDescending(t => t.Ma_HoaDon);
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


        //create code 8 digit
        public static string GenerateCode()
        {
            Random random = new Random();
            int codeLength = 8;
            string code = "";

            for (int i = 0; i < codeLength; i++)
            {
                code += random.Next(0, 10); // Tạo số ngẫu nhiên từ 0 đến 9 và thêm vào chuỗi
            }

            return code;
        }


    }
}