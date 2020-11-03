using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Management.API.Controllers
{
    public abstract class BaseController : ControllerBase
    {
       

        /// <summary>
        /// Invokes given func and tracks exceptions if any
        /// </summary>
        /// <param name="fn">Function to be invoked</param>
        /// <param name="msg">msg</param>
        /// <typeparam name="T">Type of the result</typeparam>
        /// <returns>Result wrapped in action result</returns>
        protected async Task<ActionResult<T>> InvokeAsync<T>(Func<Task<T>> fn, string msg)
        {
            try
            {
                var value = await fn();

                return StatusCode(StatusCodes.Status200OK, value);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

    }
}
