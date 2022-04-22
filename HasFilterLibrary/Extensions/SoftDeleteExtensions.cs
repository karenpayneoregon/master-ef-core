using HasFilterLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HasFilterLibrary.Extensions
{
    /// <summary>
    /// Alternate method to code in the dbContext SaveChanges
    /// </summary>
    public static class SoftDeleteExtensions 
    {
        /// <summary>
        /// Soft delete by marking IsDeleted property
        /// </summary>
        /// <param name="sender"></param>
        public static void SoftDelete(this EntityEntry sender)
        {
            if (sender.Entity is ISoftDelete entity)
            {
                entity.IsDeleted = true;
                sender.State = EntityState.Modified;
            }
            // ReSharper disable once RedundantIfElseBlock
            else
            {
                // do nothing or throw exception
                //throw new InvalidCastException("Entity does not implement ISoftDelete");
            }
        }
    }
}
