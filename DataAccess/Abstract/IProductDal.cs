using Core.DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
   public interface IProductDal : IEntityRepository<Product>
    {

        //Generic yapmak varlıkları dinamik yaparak Repository Pattern
        //Veri tabanında yapmak istediğimiz işlemler 

        //Yalnızca imzaları tabi interface burası

        //Listeleme
    }
}
