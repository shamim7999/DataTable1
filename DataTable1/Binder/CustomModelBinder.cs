using DataTable1.Helper_Class;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataTable1.Binder
{
    public class CustomModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request;
            var values = request.Headers.GetValues("params");

            return JsonConvert.DeserializeObject<FilterParameters>(values.First());
        }
    }
}