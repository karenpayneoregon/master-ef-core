using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NorthWindLibrary.Data;
using NorthWindLibrary.Models;

namespace NorthWindLibrary.Classes
{
    public class OrderOperations
    {
        public static async Task<Orders> GetOrder(int orderId = 10248)
        {
            await using var context = new NorthwindContext();

            return await context
                .Orders
                .Include(ord => ord.OrderDetails)
                .ThenInclude(ord => ord.Product)
                .Where(ord => ord.OrderId == orderId)
                .FirstOrDefaultAsync();
        }

    }
}
