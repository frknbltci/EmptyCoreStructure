using Entities.DomainModels;
using Microsoft.AspNetCore.Http;
using MvcWebUI.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcWebUI.Helpers
{
    public class CartSessionHelper : ICartSessionHelper
    {
        IHttpContextAccessor _httpContextAccesor;


        public CartSessionHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccesor = httpContextAccessor;
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public Cart GetCart(string key)
        {
            Cart cartToCheck = _httpContextAccesor.HttpContext.Session.GetObject<Cart>("cart");
            if (cartToCheck==null)
            {
                SetCart("cart",new Cart());
                cartToCheck= _httpContextAccesor.HttpContext.Session.GetObject<Cart>("cart");
            }
            return cartToCheck;
        }

        public void SetCart(string key,Cart cart)
        {
            _httpContextAccesor.HttpContext.Session.SetObject("cart", cart);
        }
    }
}
