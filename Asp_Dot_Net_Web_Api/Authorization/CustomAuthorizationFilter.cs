//using System;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.AspNetCore.Authorization;

//namespace Asp_Dot_Net_Web_Api.Authorization
//{
//    public class CustomAuthorizationFilter : AuthorizationFilterAttribute
//    {
//        public override void OnAuthorization(AuthorizationFilterContext context)
//        {
//            var isAdmin = context.HttpContext.User.HasClaim(claim => claim.Type == "isAdmin" && claim.Value == "true");
//            var isCustomer = context.HttpContext.User.HasClaim(claim => claim.Type == "isCustomer" && claim.Value == "true");
//            var isStaff = context.HttpContext.User.HasClaim(claim => claim.Type == "isStaff" && claim.Value == "true");

//            if (!isAdmin && !isCustomer && !isStaff)
//            {
//                // The user does not have any of the required attributes, so return a 401 Unauthorized response
//                context.Result = new UnauthorizedResult();
//            }
//        }
//    }
//}

