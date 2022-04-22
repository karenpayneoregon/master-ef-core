using System;
using System.Windows.Forms;
using Birthdays = WorkingWithDates.Models;

namespace WorkingWithDates.LanguageExtensions
{
    public static class ListViewExtensions
    {
        /// <summary>
        /// Provides access to ListView current item as a <see cref="Birthdays.Birthdays"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static Birthdays.Birthdays CurrentBirthday(this ListViewItem sender)
            => new()
            {
                Id = Convert.ToInt32(sender.Tag),
                FirstName = sender.SubItems[0].Text,
                LastName = sender.SubItems[1].Text,
                BirthDate = Convert.ToDateTime(sender.SubItems[2].Text)

            };
    }
}
