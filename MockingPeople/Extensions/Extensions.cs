﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;

namespace MockingPeopleLibrary.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// List of model names
        /// </summary>
        /// <returns>Model name as a list</returns>
        /// <param name="context">created DbContext</param>
        /// <remarks>
        /// var names = await HelperOperations.ModelNameList();
        /// </remarks>
        public static async Task<List<string>> ModelNameList(DbContext context) =>
            await Task.Run(() => context.Model
                .GetEntityTypes()
                .Select(entityType => entityType.ClrType)
                .Select(type => type.Name)
                .ToList());

        /// <summary>
        /// Does database exists for the DbContext
        /// </summary>
        /// <param name="context">created DbContext</param>
        /// <returns></returns>
        public static async Task<bool> DatabaseExistsAsync(DbContext context) => 
            await ((context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator)!)
                .ExistsAsync();
    }
}
