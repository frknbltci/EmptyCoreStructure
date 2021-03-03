using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using MvcWebUI.Models;

namespace MvcWebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        public IActionResult Index(int categoryid)  
        {
            var model = new ProductListViewModel
            {
                Products = (List<Entities.Concrete.Product>)(categoryid > 0 ? _productService.GetListByCategory(categoryid) : _productService.GetAll()),
                
            };

            return View(model);
        } 
    }
}
