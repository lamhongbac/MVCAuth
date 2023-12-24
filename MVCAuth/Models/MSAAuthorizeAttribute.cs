using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Filters;
using NuGet.Protocol;

namespace MVCAuth.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class MSAAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        //danh sach role trong 1 action method
        // [MSAAuthorizeAttribute(Roles = "Admin,IT")]=> thiet lap Roles Property value
        public string Roles { get; set; }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            HttpContext htppContext = context.HttpContext;
            if (htppContext != null)
            {
                //htppContext.Items la tap hop cac doi tuong gan vao khi login

                Account account=(Account)htppContext.Items["account"];
                if (account != null)
                {
                    string[] roles = Roles.Split(',');
                    if (!roles.Any(x => account.Roles.Contains(x)))
                    {
                        context.HttpContext.Response.Redirect("/Login/AccessDenied");
                    }
                }
                else
                {
                    context.HttpContext.Response.Redirect("/Login/Login");
                }

            }
        }
    }
}
