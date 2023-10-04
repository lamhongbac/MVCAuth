using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MVCAuth.Models
{
    /// <summary>
    /// Middle ware nay co nhiem vu gan account vao context
    /// sau do khi viet atttribue se su dung account de kiem tra 
    /// 
    /// </summary>
    public class MSAAuthorizationMW
    {
        private readonly RequestDelegate _next;
        AccountService accountService;
        public MSAAuthorizationMW(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context,AccountService    accountService)
        {
            this.accountService= accountService;
            string email=context.Session.GetString("userName");
            if (!string.IsNullOrEmpty(email))
            {
                context.Items["account"] = accountService.GetAccount(email);
            }
            await _next(context);

        }
    }
}
