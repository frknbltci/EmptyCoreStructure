using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Mvc;
using MvcWebUI.Models;

namespace MvcWebUI.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService )
        {
            _categoryService = categoryService;
        }



        //public IActionResult Index()
        //{
        //    CategoryListViewModel model = new CategoryListViewModel()
        //   {
        //        Categories = return new SuccessDataResult<CategoryListViewModel>(_categoryService.GetAll())
        //    };

        //    return View(model);
        //}


    }
}
