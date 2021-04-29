using Khabarho.Services.CategoryService;
using Khabarho.Services.TypeService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Khabarho.Utilities
{
    public class CustomFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var categoryService = context.HttpContext.RequestServices.GetRequiredService<ICategoryService>();
            var typeService = context.HttpContext.RequestServices.GetRequiredService<ITypeService>();
        
            var controller = context.Controller as Controller; 
            
            controller.ViewBag.Categories =  categoryService.GetAllAsync().Result; 
            
            controller.ViewBag.Types =  typeService.GetAllAsync().Result;
            base.OnResultExecuting(context);
        }
    }
}