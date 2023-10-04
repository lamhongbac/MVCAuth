using System.Globalization;

namespace MVCAuth.Models
{
    /// <summary>
    /// kiem tra user khac null thi cho qua
    /// </summary>
    public class MSAAuthenticationMW
    {
        private readonly RequestDelegate _next;

        public MSAAuthenticationMW(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string path=context.Request.Path;
            if (context.Session.GetString("userName")==null)
            {
                //context.Response.Redirect("/Login/Login");


            }
            //var cultureQuery = context.Request.Query["culture"];
            //if (!string.IsNullOrWhiteSpace(cultureQuery))
            //{
            //    var culture = new CultureInfo(cultureQuery);

            //    CultureInfo.CurrentCulture = culture;
            //    CultureInfo.CurrentUICulture = culture;
            //}

            await _next(context);

        }
    }
}
