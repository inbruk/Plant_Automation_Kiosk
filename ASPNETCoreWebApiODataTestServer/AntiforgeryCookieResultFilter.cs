﻿using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreWebApiODataTestServer
{
    //public class AntiforgeryCookieResultFilter : ResultFilterAttribute
    //{
    //    private IAntiforgery antiforgery;
    //    public AntiforgeryCookieResultFilter(IAntiforgery antiforgery)
    //    {
    //        this.antiforgery = antiforgery;
    //    }

    //    public override void OnResultExecuting(ResultExecutingContext context)
    //    {
    //        if (context.Result is ViewResult)
    //        {
    //            var tokens = antiforgery.GetAndStoreTokens(context.HttpContext);
    //            context.HttpContext.Response.Cookies.Append
    //            (
    //                "XSRF-TOKEN",
    //                tokens.RequestToken,
    //                new CookieOptions()
    //                {
    //                    HttpOnly = false
    //                }
    //            );
    //        }
    //    }
    //}
}
