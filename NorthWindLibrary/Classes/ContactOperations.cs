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
    public class ContactOperations
    {
        public static async Task<List<Contacts>> ByType(List<int> identifiers)
        {
            return await Task.Run(async () =>
            {
                await using var context = new NorthwindContext();
                return await context.Contacts
                    .Where(currentContact =>
                        currentContact.ContactTypeIdentifier.HasValue &&
                        identifiers.Contains(currentContact.ContactTypeIdentifier ?? 0))
                    .ToListAsync();

            });
        }


    }
}
