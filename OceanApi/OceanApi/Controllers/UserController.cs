using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using OceanApi.Token;

namespace OceanApi.Controllers
{
    public class UserController : Controller
    {
        public object Login(string strUserName, string strPassword)
        {
            /*
             * Net Framework WebApi验证步骤
             * 1、用户登录的用户名和密码用“&”拼接起来然后进行加密
             * 2、加密后的Token储存在Cookie中，用户名储存在Session中
             * 3、把储存在Cookie中的Token传给前端页面，前端页面通过ajax请求数据的时候把Token传给Action
             * 4、因为Action有继续自AuthenticationAttribute的自定义特性，所以会去先验证Token是否错误，过期等
             * 5、错误或者过期跳到登录页面，否则返回正常的数据
             */
            /*
            FormAuthenticationTicket ticket = new FormAuthenticationTicket(0, strUserName, DateTime.Now, DateTime.Now.AddHours(1), true, string.Format("{0}&{1}", strUserName, strPassword), FormAuthentication.FormCookiePath);
            //返回登录结果、用户信息、用户验证票据信息
            var oUser = new UserInfo { bRes = true, UserName = strUserName, Password = strPassword, Ticket = FormAuthentication.Encrypt(ticket) };
            //
            HttpContext.Current.Session[strUser] = oUser;
            return oUser;
            */
            return null;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string username, string password, string role)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsASecretKeyForAspNetCoreAPIToken"));
            var options = new TokenProviderOptions
            {
                Audience = "audience",
                Issuer = "issuer",
                Credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            };
            var tpm = new TokenProvider(options);
            var token = await tpm.GenerateToken(HttpContext, username, password, role);
            if (null != token)
            {
                return new JsonResult(token);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// 验证请求
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View();
        }
    }
}