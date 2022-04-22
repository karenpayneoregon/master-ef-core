
using WorkingWithDates.Controls;

namespace WorkingWithDates
{
    partial class EventsForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GtCurrentPersonButton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.FirstNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BirthDateColumn = new WorkingWithDates.Controls.CalendarColumn();
            this.AgeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BirthDateDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.ShowChangesButton = new System.Windows.Forms.Button();
            this.ChangeResultsTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // GtCurrentPersonButton
            // 
            this.GtCurrentPersonButton.Location = new System.Drawing.Point(12, 130);
            this.GtCurrentPersonButton.Name = "GtCurrentPersonButton";
            this.GtCurrentPersonButton.Size = new System.Drawing.Size(225, 23);
            this.GtCurrentPersonButton.TabIndex = 0;
            this.GtCurrentPersonButton.Text = "Current person";
            this.GtCurrentPersonButton.UseVisualStyleBackColor = true;
            this.GtCurrentPersonButton.Click += new System.EventHandler(this.CurrentPersonButton_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FirstNameColumn,
            this.LastNameColumn,
            this.BirthDateColumn,
            this.AgeColumn});
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(542, 108);
            this.dataGridView1.TabIndex = 1;
            // 
            // FirstNameColumn
            // 
            this.FirstNameColumn.DataPropertyName = "FirstName";
            this.FirstNameColumn.HeaderText = "First";
            this.FirstNameColumn.Name = "FirstNameColumn";
            // 
            // LastNameColumn
            // 
            this.LastNameColumn.DataPropertyName = "LastName";
            this.LastNameColumn.HeaderText = "Last";
            this.LastNameColumn.Name = "LastNameColumn";
            // 
            // BirthDateColumn
            // 
            this.BirthDateColumn.DataPropertyName = "BirthDate";
            dataGridViewCellStyle1.Format = "d";
            this.BirthDateColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.BirthDateColumn.HeaderText = "Date";
            this.BirthDateColumn.Name = "BirthDateColumn";
            this.BirthDateColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.BirthDateColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // AgeColumn
            // 
            this.AgeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.AgeColumn.DataPropertyName = "Age";
            this.AgeColumn.HeaderText = "Age";
            this.AgeColumn.Name = "AgeColumn";
            // 
            // BirthDateDateTimePicker
            // 
            this.BirthDateDateTimePicker.CustomFormat = "yyyy-MM-dd";
            this.BirthDateDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.BirthDateDateTimePicker.Location = new System.Drawing.Point(354, 130);
            this.BirthDateDateTimePicker.Name = "BirthDateDateTimePicker";
            this.BirthDateDateTimePicker.Size = new System.Drawing.Size(200, 23);
            this.BirthDateDateTimePicker.TabIndex = 2;
            // 
            // ShowChangesButton
            // 
            this.ShowChangesButton.Location = new System.Drawing.Point(15, 161);
            this.ShowChangesButton.Name = "ShowChangesButton";
            this.ShowChangesButton.Size = new System.Drawing.Size(222, 23);
            this.ShowChangesButton.TabIndex = 3;
            this.ShowChangesButton.Text = "Changes";
            this.ShowChangesButton.UseVisualStyleBackColor = true;
            this.ShowChangesButton.Click += new System.EventHandler(this.ShowChangesButton_Click);
            // 
            // ChangeResultsTextBox
            // 
            this.ChangeResultsTextBox.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ChangeResultsTextBox.Location = new System.Drawing.Point(69, 215);
            this.ChangeResultsTextBox.Multiline = true;
            this.ChangeResultsTextBox.Name = "ChangeResultsTextBox";
            this.ChangeResultsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ChangeResultsTextBox.Size = new System.Drawing.Size(431, 180);
            this.ChangeResultsTextBox.TabIndex = 4;
            // 
            // EventsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 407);
            this.Controls.Add(this.ChangeResultsTextBox);
            this.Controls.Add(this.ShowChangesButton);
            this.Controls.Add(this.BirthDateDateTimePicker);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.GtCurrentPersonButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "EventsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EF Core (Formatting dates)";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button GtCurrentPersonButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker BirthDateDateTimePicker;
        private System.Windows.Forms.DataGridViewTextBoxColumn FirstNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastNameColumn;
        private CalendarColumn BirthDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AgeColumn;
        private System.Windows.Forms.Button ShowChangesButton;
        private System.Windows.Forms.TextBox ChangeResultsTextBox;
    }
}

