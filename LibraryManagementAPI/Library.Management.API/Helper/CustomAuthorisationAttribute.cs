using Library.Management.API.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Library.Management.API.Helper
{
    /// <summary>
    /// CustomAuthorizeAttribute
    /// </summary>
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// AuthorizeAttribute
        /// </summary>
        /// <param name="role">role</param>
        public CustomAuthorizeAttribute(SecurityGroupType role)
    : base(typeof(AuthorizeActionFilter))
        {
            Arguments = new object[] { role };
        }
    }

    /// <summary>
    /// AuthorizeActionFilter
    /// </summary>
    public class AuthorizeActionFilter : IAuthorizationFilter
    {
        /// <summary>
        /// AuthorizeActionFilter
        /// </summary>
        public AuthorizeActionFilter()
        {

        }

        /// <summary>
        /// OnAuthorization
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isAuthorized = false;
            try
            {
                var userId = Convert.ToString(context.HttpContext.Request.Headers["UserId"]);

                isAuthorized = true;

                if (!isAuthorized)
                {
                    context.Result = new ForbidResult();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }

}
