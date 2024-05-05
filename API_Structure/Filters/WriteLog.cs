using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
 

namespace API_Structure.Filters
{
    public class WriteLog : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionRequest = Convert.ToString(FormatRequestBody(filterContext.ActionArguments));
            LogError(filterContext.RouteData, "Request", actionRequest);
        }
        public string FormatRequestBody(IDictionary<string, object> actionArguments)
        {
            try
            {



                if (actionArguments != null)
                    return $"{JsonConvert.SerializeObject(actionArguments)}";
            }
            catch (Exception ex)
            {



            }
            return "";
        }

        public string FormatResponseBody(object result)
        {
            try
            {
                if (result != null)
                    return $"{JsonConvert.SerializeObject(result)}";
            }
            catch (Exception ex)
            {
                // _logger.Error("Error in LogServiceCallFilter", ex);
            }
            return "";
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var actionResponse = Convert.ToString(FormatResponseBody(filterContext.Result));
            LogError(filterContext.RouteData, "Response", actionResponse);
        }

        public void LogError(RouteData routeData, string actionDetails, string actionData)
        {
            try
            {
                var controllerName = routeData.Values["controller"];
                var actionName = routeData.Values["action"];
                string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
                message += Environment.NewLine;
                message += "-----------------------------------------------------------";
                message += Environment.NewLine;
                message += string.Format("ControllerName: {0}", controllerName);
                message += Environment.NewLine;
                message += string.Format("ActionName: {0}", actionName);
                message += Environment.NewLine;
                message += string.Format(String.Concat(actionDetails, ":{0}"), actionData);
                message += Environment.NewLine;
                message += "-----------------------------------------------------------";
                message += Environment.NewLine;
                string strPath = "Logs";
                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), strPath)))
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), strPath));
                }
                strPath = strPath + "/" + DateTime.Now.ToString("yyyyMM");
                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), strPath)))
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), strPath));
                }
                strPath = strPath + "/" + DateTime.Now.ToString("yyyyMMdd");
                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), strPath)))
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), strPath));
                }
                strPath = strPath + "/" + string.Format("{0}Controller", controllerName);
                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), strPath)))
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), strPath));
                }
                strPath = strPath + "/" + DateTime.Now.ToString("yyyyMMdd") + string.Format("_{0}Controller_", controllerName) + ".txt";



                string path = Path.Combine(Directory.GetCurrentDirectory(), strPath);
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(message);
                    writer.Close();
                }
            }
            catch (Exception ex)
            {



            }
        }
    }
}


