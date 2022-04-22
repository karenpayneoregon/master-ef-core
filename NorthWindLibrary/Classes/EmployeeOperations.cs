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
    public class EmployeeOperations
    {
        public static async Task<IGrouping<int, Employees>> HighCountInOrders()
        {
            await using var context =  new NorthwindContext();
            var employeeList = await context
                .Orders
                .Where(order => order.EmployeeId != null)
                .Select(order => order.Employee)
                .ToListAsync();

            /*
             * Group employees in descending order which will provide high order count
             * for a single employee.
             */
            return employeeList
                .GroupBy(employee => employee.EmployeeId)
                // reverse order it on count
                .OrderByDescending(grp => grp.Count())
                .FirstOrDefault();



        }
    }
}
