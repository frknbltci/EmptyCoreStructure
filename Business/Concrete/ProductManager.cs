using Business.Abstract;
using Business.BusinnesAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Trasaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {

        private IProductDal _productDal;
        ICategoryService _categoryService;  //Burada ProductDal dan başka dal çağıramazsınız Service çağırılabilir sadece
       

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
                 
        }


        // [Log]
        // Claim idddia etmek demektir admin veya  editor olma durumu ile alakaalı
         [SecuredOperation("admin")]
         [ValidationAspect(typeof(ProductValidator))]
         [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {

          IResult result=  BusinessRules.Run(CheckIfProductCountofCategoryCorrect(product.CategoryId), CheckIfProductSameName(product.ProductName), CheckIfCategoryLimitExceded());
            if (result!=null)
            {

                return new ErrorResult();
                // return result;
            }

            _productDal.Add(product);
            return new SuccessResult();

            
            //validation demek gelen parametrenin nasıl olacağı

            //businnen kurallar mesela 10 ürün olacak bir kategoride
            //burası loglanacak
            //validation fluent yazıldı zaten burada kullanıyoruz

            //ValidationTool.Validate(new ProductValidator(), product); gerek kalmadı valid ettik
            //loglama
            //cache
            //performance
            //transcation
            //yetkilendirme
            // böyle olmaz 
                        
        }

        [CacheAspect] // key,value da tutulur
        public IDataResult<List<Product>> GetAll()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList(),Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetListByCategory(int CategoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList(p => p.CategoryId == CategoryId));
        }

        [CacheAspect]
        [PerformanceAspect(5)] //5sn.den uzun çalışırsa bana haber ver  
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(x => x.ProductId == productId));
        }


        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            throw new NotImplementedException();
        }




        private IResult CheckIfProductCountofCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetList(p => p.CategoryId == categoryId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);

               
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductSameName(string productName)
        {
            var result = _productDal.GetList(x => x.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductSameNameError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetCategoryCount();
            if (result.Data>15)
            {
                return new ErrorResult(Messages.CategoryLimitError);
            }
            return new SuccessResult();
        }
        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Product product)
        {
            Add(product);
            if (product.UnitPrice<10)
            {
                throw new Exception("");
            }
            Add(product);

            return null;
        }
    }
}
