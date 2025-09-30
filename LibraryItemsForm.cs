using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A7
{
    /// <summary>
    /// CLass to represent the form to add or update LibraryItems.
    /// </summary>
    public partial class LibraryItemsForm : Form
    {
        private LibraryItem libraryItem;

        /// <summary>
        /// Property to get or set the LibraryItem object.
        /// </summary>
        internal LibraryItem LibraryItem
        {
            get { return libraryItem; }
            set { libraryItem = value; }
        }

        /// <summary>
        /// Main constructor to initialize the form.
        /// </summary>
        public LibraryItemsForm()
        {
            InitializeComponent();
            InitializeGUI();
        }

        /// <summary>
        /// Overloaded constructor to initialize the form with an existing LibraryItem.
        /// </summary>
        /// <param name="libraryItem"></param>
        internal LibraryItemsForm(LibraryItem libraryItem)
        {
            InitializeComponent();
            InitializeGUI();
            LibraryItem = libraryItem;
            if (libraryItem is Book)
            {
                var book = libraryItem as Book;
                typeCmb.SelectedIndex = 0;
                titleTextBox.Text = book.Title;
                dateTimePicker.Value = book.PublishedDate;
                authorTextBox.Text = book.Author;
                isbnTextBox.Text = book.ISBN;
                numericUpDown.Value = book.NrPages;
                descTextBox.Text = book.Description;
            }
            else if (libraryItem is Film)
            {
                var film = libraryItem as Film;
                typeCmb.SelectedIndex = 1;
                titleTextBox.Text = film.Title;
                dateTimePicker.Value = film.PublishedDate;
                directorTextBox.Text = film.Director;
                numericUpDown1.Value = film.Duration;
                descTextBox.Text = film.Description;
            }
            else if (libraryItem is Article)
            {
                var article = libraryItem as Article;
                typeCmb.SelectedIndex = 2;
                titleTextBox.Text = article.Title;
                dateTimePicker.Value = article.PublishedDate;
                articleAuthorTextBox.Text = article.Author;
                journalTextBox.Text = article.Journal;
                descTextBox.Text = article.Description;
            }
            else if (libraryItem is NewsPaper)
            {
                var newspaper = libraryItem as NewsPaper;
                typeCmb.SelectedIndex = 3;
                titleTextBox.Text = newspaper.Title;
                dateTimePicker.Value = newspaper.PublishedDate;
                editorTextBox.Text = newspaper.Publisher;
                editionTextBox.Text = newspaper.Edition;
                descTextBox.Text = newspaper.Description;
            }
        }

        /// <summary>
        /// Method to initialize the GUI.
        /// </summary>
        public void InitializeGUI()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.LightGray;

            // Using a list of types to populate the ComboBox with types of LibraryItems.
            var types = new List<string> { "Book", "Film", "Article", "Newspaper" };
            typeCmb.DataSource = types;
            typeCmb.SelectedIndex = 0;
        }

        /// <summary>
        /// Method to update an existing LibraryItem in the database.
        /// </summary>
        /// <param name="updatedItem">The updated LibraryItem object.</param>
        internal void UpdateLibraryItem(LibraryItem updatedItem)
        {
            using (var context = new LibraryContext())
            {
                context.LibraryItems.Attach(updatedItem); // Attach the updated item to the context
                context.Entry(updatedItem).State = EntityState.Modified; // Set the state to modified

                context.SaveChanges(); // Save changes to the database
                MessageBox.Show("Item updated successfully.");
            }
        }

        /// <summary>
        /// Method to handle the OK button click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButton_Click(object sender, EventArgs e)
        {
            var selectedType = typeCmb.SelectedItem.ToString();

            // Validate the required fields based on the selected type
            if (!ValidateFields(selectedType))
            {
                return; // Stay on the form if validation fails
            }

            // If all required fields are valid, create the new item
            LibraryItem newItem = null;

            // Determine if we're editing an existing item or creating a new one
            bool isEditMode = libraryItem != null; // Check if we're in edit mode (libraryItem should not be null)

            // Handle item creation or editing based on the type selected
            switch (selectedType)
            {
                case "Book":
                    newItem = new Book
                    {
                        Title = titleTextBox.Text,
                        PublishedDate = dateTimePicker.Value.Date,
                        Author = authorTextBox.Text,
                        ISBN = isbnTextBox.Text,
                        NrPages = (int)numericUpDown.Value,
                        Description = descTextBox.Text
                    };
                    if (isEditMode)
                    {
                        newItem.Id = libraryItem.Id; // Set ID only if in edit mode
                        newItem.IsAvailable = libraryItem.IsAvailable; // Preserve the availability status
                    }
                    break;

                case "Film":
                    newItem = new Film
                    {
                        Title = titleTextBox.Text,
                        PublishedDate = dateTimePicker.Value.Date,
                        Director = directorTextBox.Text,
                        Duration = (int)numericUpDown1.Value,
                        Description = descTextBox.Text
                    };
                    if (isEditMode)
                    {
                        newItem.Id = libraryItem.Id;
                        newItem.IsAvailable = libraryItem.IsAvailable; // Preserve the availability status
                    }
                    break;

                case "Article":
                    newItem = new Article
                    {
                        Title = titleTextBox.Text,
                        PublishedDate = dateTimePicker.Value.Date,
                        Author = articleAuthorTextBox.Text,
                        Journal = journalTextBox.Text,
                        Description = descTextBox.Text
                    };
                    if (isEditMode)
                    {
                        newItem.Id = libraryItem.Id;
                        newItem.IsAvailable = libraryItem.IsAvailable; // Preserve the availability status
                    }
                    break;

                case "Newspaper":
                    newItem = new NewsPaper
                    {
                        Title = titleTextBox.Text,
                        PublishedDate = dateTimePicker.Value.Date,
                        Publisher = editorTextBox.Text,
                        Edition = editionTextBox.Text,
                        Description = descTextBox.Text
                    };
                    if (isEditMode)
                    {
                        newItem.Id = libraryItem.Id;
                        newItem.IsAvailable = libraryItem.IsAvailable; // Preserve the availability status
                    }
                    break;
            }

            // If newItem is valid, assign it to the LibraryItem property and set DialogResult to OK
            if (newItem != null)
            {
                LibraryItem = newItem;
                DialogResult = DialogResult.OK;  // Proceed and close the form only if valid
                Close();
            }
            else
            {
                MessageBox.Show("An error occurred while creating the item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        /// <summary>
        /// Method to validate the input fields before saving the item.
        /// </summary>
        /// <param name="selectedType"></param> The selected type of LibraryItem.
        /// <returns></returns> True if all required fields are valid; otherwise, false.
        private bool ValidateFields(string selectedType)
        {
            // Reset the field background colors before validation
            titleTextBox.BackColor = Color.White;
            authorTextBox.BackColor = Color.White;
            isbnTextBox.BackColor = Color.White;

            // Validate common required fields (e.g., Title)
            if (string.IsNullOrWhiteSpace(titleTextBox.Text))
            {
                titleTextBox.BackColor = Color.LightCoral;
                DialogResult result = MessageBox.Show("Title is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    this.DialogResult = DialogResult.None; // Keep the form open for correction
                    return false; // Return false if validation fails.
                }
            }

            else {
                // Additional validation based on the selected type
                switch (selectedType)
                {
                    case "Book":
                        if (string.IsNullOrWhiteSpace(authorTextBox.Text) || string.IsNullOrWhiteSpace(isbnTextBox.Text))
                        {
                            authorTextBox.BackColor = Color.LightCoral;
                            isbnTextBox.BackColor = Color.LightCoral;
                            MessageBox.Show("Author and ISBN are required for books.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.DialogResult = DialogResult.None;
                            return false;
                        }
                        break;

                    case "Film":
                        if (string.IsNullOrWhiteSpace(directorTextBox.Text))
                        {
                            directorTextBox.BackColor = Color.LightCoral;
                            MessageBox.Show("Director is required for films.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.DialogResult = DialogResult.None;
                            return false;
                        }
                        break;

                    case "Article":
                        if (string.IsNullOrWhiteSpace(articleAuthorTextBox.Text) || string.IsNullOrWhiteSpace(journalTextBox.Text))
                        {
                            articleAuthorTextBox.BackColor = Color.LightCoral;
                            journalTextBox.BackColor = Color.LightCoral;
                            MessageBox.Show("Author and Journal are required for articles.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.DialogResult = DialogResult.None; // Mark as invalid
                            return false;
                        }
                        break;

                    case "Newspaper":
                        if (string.IsNullOrWhiteSpace(editorTextBox.Text) || string.IsNullOrWhiteSpace(editionTextBox.Text))
                        {
                            editorTextBox.BackColor = Color.LightCoral;
                            editionTextBox.BackColor = Color.LightCoral;
                            MessageBox.Show("Publisher and Edition are required for newspapers.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.DialogResult = DialogResult.None; // Mark as invalid
                            return false;
                        }
                        break;
                }
            }
            return true;
        }

        /// <summary>
        /// Method to handle the Cancel button click event.
        /// </summary>
        /// <param name="sender"></param> The sender object.
        /// <param name="e"></param> The event arguments.
        private void cancelButton_Click(object sender, EventArgs e)
        {
            // Prompt user if they want to cancel the operation.
            // If the user selects No, keep the form open; if Yes, close without saving.
            DialogResult result = MessageBox.Show("Are you sure you want to cancel?", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            else
            {
                this.DialogResult = DialogResult.None;
            }
        }
    }
}
