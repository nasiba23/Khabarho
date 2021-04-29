using System;
using System.Threading.Tasks;
using Khabarho.Extensions;
using Khabarho.Services.TypeService;
using Khabarho.Utilities;
using Khabarho.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Khabarho.Controllers
{
    public class TypeController : Controller
    {
        private ITypeService _service;

        public TypeController(ITypeService service)
        {
            _service = service;
        }
        
        [HttpGet]
        [Authorize (Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(TypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var result = await _service.CreateAsync(model);

            return RedirectToAction("GetAll", "Type");
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var types = await _service.GetAllAsync();

            types.CustomNullCheck(ErrorMessages.NotFoundError);
            
            return View(types);
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(string id)
        {
            id.CustomNullCheck(ErrorMessages.NullParameterError);

            var type = await _service.GetAsync(id);
            
            type.CustomNullCheck(ErrorMessages.NotFoundError);
            
            return View(type);
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(string id)
        {
            return View();
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(TypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            await _service.UpdateAsync(model);

            return RedirectToAction("GetAll", "Type");
        }
        
                
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            id.CustomNullCheck(ErrorMessages.NullParameterError);
            
            await _service.DeleteAsync(id);

            return RedirectToAction("GetAll", "Type");
        }
    }
}