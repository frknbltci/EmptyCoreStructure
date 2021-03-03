using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
   public interface IProductService
    {
        IDataResult<List<Product>> GetAll();

        IDataResult<List<Product>> GetListByCategory(int CategoryId);

        IDataResult<Product> GetById(int productId);

        IResult Add(Product product);

        IResult Update(Product product);

        //Uygulamalar tutarlılığı sağlamak için kullanılan yöntem (tutarlılık). Bankalardan para aktarım olayı 2 işlem olayı para aktarım ve para diğer tarafa ekleme olayı bir işlem yapılırken diğer taraf hata alırsa işlem geri alınmalıdır. Bu işleme transaction denir.
        IResult AddTransactionalTest(Product product);

    }
}
