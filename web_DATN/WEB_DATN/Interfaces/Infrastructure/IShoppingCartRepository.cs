using MISA_WEBHAUI_AMIS_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure
{
    public interface IShoppingCartRepository:IBaseRepository<Cart>
    {
        Task<int> AddShoppingCart(Cart product);
        Task<int> UpdateShoppingCart(Cart product);
        Task<Cart> GetCartByUP(Guid userId, Guid productId);

        Task<Product> GetProductById(Guid productId);

        object GetCartByUserId(Guid userId);
       // object DeleteCart(Guid cartId);
    }
}
