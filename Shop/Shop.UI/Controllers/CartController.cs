﻿using Microsoft.AspNetCore.Mvc;
using Shop.Application.Cart;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.UI.Controllers
{
    [Route("{controller}/{action}")]
    public class CartController : Controller
    {
        [HttpPost("{stockId}")]
        public async Task<IActionResult> AddOne(
            int stockId,
            [FromServices] AddToCart addToCart)
        {
            var request = new AddToCart.Request
            {
                StockId = stockId,
                Qty = 1
            };

            var success = await addToCart.Do(request);

            if (success)
            {
                return Ok("Item added to Cart");
            }

            return BadRequest("Failed to add to Cart");

        }

        [HttpPost("{stockId}/{qty}")]
        public async Task<IActionResult> Remove(
            int stockId,
            int qty,
            [FromServices] RemoveFromCart removeFromCart )
        {
            var request = new RemoveFromCart.Request
            {
                StockId = stockId,
                Qty = qty
            };

            var success = await removeFromCart.Do(request);

            if (success)
            {
                return Ok("Item removed from Cart");
            }

            return BadRequest("Failed to remove from Cart");

        }
        [HttpGet]
        public IActionResult GetCartComponent([FromServices] GetCart getCart)
        {
            var totalValue = getCart.Do().Sum(x => x.RealValue * x.Qty);

            return PartialView("Components/Cart/Small", $"€{totalValue}");
        }

        [HttpGet]
        public IActionResult GetCartMain([FromServices] GetCart getCart)
        {
            var cart = getCart.Do();

            return PartialView("_CartPartial", cart);
        }

    }
}
