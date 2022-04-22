using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using WindowsFormsLibrary.Classes;
using WorkingWithDates.Classes;
using WorkingWithDates.Data;
using WorkingWithDates.LanguageExtensions;
using WorkingWithDates.Models;
using static BaseClassProject.Classes.Helpers;

namespace WorkingWithDates
{
    public partial class EventsForm : Form
    {
        private readonly BindingSource _bindingSource = new ();

        public EventsForm()
        {
            InitializeComponent();

            dataGridView1.AutoGenerateColumns = false;

            Shown += OnShown;
        }

        /// <summary>
        /// Load data via Entity Framework Core 5
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnShown(object sender, EventArgs e)
        {

            _bindingSource.DataSource = await DataOperations.PeopleLocal();
            dataGridView1.DataSource = _bindingSource;
        }

        /// <summary>
        /// Demonstrates accessing the current record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentPersonButton_Click(object sender, EventArgs e)
        {

            if (DataIsNotAccessible()) return;

            var current = (Person)_bindingSource.Current;

            Dialogs.Information(this, $"{current.FirstName} {current.LastName}\n" + $"{current.BirthDate.Value:yyyy-MM-dd}");

        }

        /// <summary>
        /// If there are modified records, show the id and change, deleted
        /// records are marked for deletion but not shown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowChangesButton_Click(object sender, EventArgs e)
        {
            if (_bindingSource is null) return;

            var changes = DataOperations.ShowEventsChangesPlain();

            ChangeResultsTextBox.Text = changes.Any() ?
                GetPropertyData(changes) : 
                "No changes";
            
        }

        /// <summary>
        /// Determine if underlying data is accessible 
        /// </summary>
        /// <returns></returns>
        private bool DataIsNotAccessible() => (_bindingSource?.Current is null);
    }
}
