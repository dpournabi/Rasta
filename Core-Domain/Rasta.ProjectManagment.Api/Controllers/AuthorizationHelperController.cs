using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using net.sf.mpxj.ganttproject.schema;
using System.Reflection;

namespace Rasta.ProjectManagment.Api.Controllers
{
    //[Authorize(Roles = "Root")]
    public class AuthorizationHelperController : BaseApiController
    {
        [HttpGet]
        public IActionResult GetAllActions()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            var controllers = assembly.GetTypes()
                .Where(x => x.BaseType != null &&
                           x.BaseType.Name == "BaseApiController" &&
                           x.Name != "AuthorizationHelperController")
                .Select(x => x.Name.Replace("Controller", ""))
                .ToList();

            var methods = new List<string>();
            foreach (var controllerName in controllers)
            {
                var controllerObject = assembly.GetTypes().First(x => x.Name == string.Concat(controllerName, "Controller"));
                methods.AddRange(controllerObject.GetMethods()
                .Where(method => method.IsPublic &&
                       !method.IsDefined(typeof(NonActionAttribute)) &&
                       (method.ReturnType != null && method.ReturnType.FullName.Contains("IActionResult")))
                .Select(x => $"{controllerName}.{x.Name.Replace("Async", "")}")
                .ToList());
            }
            return Ok(methods);
        }
    }
}
