#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkingWithDates.Classes;
using WorkingWithDates.LanguageExtensions;
using WorkingWithDates.Models;


namespace WorkingWithDates
{
    public partial class BirthdayForm : Form
    {
        private readonly BindingSource _bindingSource = new();
        private BindingList<Birthdays> _birthdaysList = new BindingList<Birthdays>();

        public BirthdayForm()
        {
            InitializeComponent();

            Shown += OnShown;
        }

        private async void OnShown(object? sender, EventArgs e)
        {

            var startDate = new DateTime(1953, 1, 2);
            var endDate = new DateTime(1956, 9, 24);

            _birthdaysList = new BindingList<Birthdays>(
                await DataOperations.GetBirthdaysList(startDate, endDate));

            listView1.BeginUpdate();

            try
            {

                foreach (var birthDayItem in _birthdaysList)
                {
                    listView1.Items.Add(
                        new ListViewItem(birthDayItem.ItemArray)
                        {
                            Tag = birthDayItem.Id
                        });
                }

                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            }
            finally
            {
                listView1.EndUpdate();
            }


            listView1.Items[0].Selected = true;
            listView1.EnsureVisible(0);

            listView1.MouseDoubleClick += ListView1OnMouseDoubleClick;

            ActiveControl = listView1;

            Text = $"Birthdates between {startDate.Date:d} and {endDate.Date:d} is {_birthdaysList.Count}";


        }

        private void ListView1OnMouseDoubleClick(object? sender, MouseEventArgs e)
        {
            if (_birthdaysList.Count >0)
            {
                var item = listView1.SelectedItems[0].CurrentBirthday();
                MessageBox.Show($"{item.Id}\n{item.FullName}\n{item.BirthDate?.ToString("d")}");
            }
            
            

        }
    }
}
