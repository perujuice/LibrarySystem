using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ActivationContext;

namespace A7
{
    /// <summary>
    /// Class to represent the main form of the application.
    /// </summary>
    public partial class MainForm : Form
    {
        private LibraryItem items; // LibraryItem object to store the selected item

        /// <summary>
        /// Constructor to initialize the MainForm.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            InitializeGUI();
        }

        /// <summary>
        /// Initialize the GUI properties.
        /// </summary>
        private void InitializeGUI()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            DisplayLibraryItems(); // Display all library items in the ListBox
        }

        /// <summary>
        /// Add item button click event handler to open the LibraryItemsForm.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBtn_Click(object sender, EventArgs e)
        {
            using (var libraryItemsForm = new LibraryItemsForm())
            {
                libraryItemsForm.ShowDialog(); // Show the form

                if (libraryItemsForm.DialogResult == DialogResult.OK)
                {
                    using (var context = new LibraryContext())
                    {
                        context.LibraryItems.Add(libraryItemsForm.LibraryItem);
                        context.SaveChanges();
                        MessageBox.Show("Item added successfully.");
                    }

                    DisplayLibraryItems(); // Refresh the ListBox after adding
                }
                else if (libraryItemsForm.DialogResult == DialogResult.Cancel)
                {
                    MessageBox.Show("Item addition cancelled.");
                }
            }
        }

        /// <summary>
        /// Method to delete the selected item from the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (ItemsListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an item to delete.");
                return;
            }

            // Retrieve the selected LibraryItem object
            var selectedItem = (LibraryItem)ItemsListBox.SelectedItem;

            using (var context = new LibraryContext())
            {
                // Find the selected LibraryItem by its Id
                var itemToDelete = context.LibraryItems.Find(selectedItem.Id);

                if (itemToDelete != null)
                {
                    // Find all loans associated with the item
                    var relatedLoans = context.Loans.Where(l => l.LibraryItemId == itemToDelete.Id).ToList();

                    // Remove all related loans first
                    foreach (var loan in relatedLoans)
                    {
                        context.Loans.Remove(loan);
                    }

                    // Now remove the LibraryItem
                    context.LibraryItems.Remove(itemToDelete);

                    // Save changes to the database
                    context.SaveChanges();
                    MessageBox.Show("Item and associated loans deleted successfully.");
                }
                else
                {
                    MessageBox.Show("Item not found in the database.");
                }
            }

            DisplayLibraryItems(); // Refresh the ListBox after deletion
            ItemsListBox.SelectedIndex = -1;
            DesclistBox.Items.Clear();

        }

        /// <summary>
        /// Method to display all library items in the ListBox.
        /// </summary>
        private void DisplayLibraryItems()
        {
            ItemsListBox.Items.Clear();
            using (var context = new LibraryContext())
            {
                var libraryItems = context.LibraryItems.ToList();
                foreach (var item in libraryItems)
                {
                    ItemsListBox.Items.Add(item); // Add the actual object
                }
            }
        }

        /// <summary>
        /// Method to display the detailed information of the selected item.
        /// </summary>
        private void DisplayDetailedInfo()
        {
            // Clear existing items from DescListBox
            DesclistBox.Items.Clear();

            // Check if an item is selected in the ItemsListBox
            if (ItemsListBox.SelectedIndex != -1)
            {
                // Get the selected item
                var selectedItem = (LibraryItem)ItemsListBox.SelectedItem;

                // Format and display the basic details of the selected item
                string detailedText = selectedItem.GetDisplayText();
                string[] lines = detailedText.Split('\n');
                foreach (string line in lines)
                {
                    if (line.Length > 35)
                    {
                        var subLines = SplitLongLine(line, 35);
                        foreach (var subLine in subLines)
                            DesclistBox.Items.Add(subLine);
                    }
                    else
                    {
                        DesclistBox.Items.Add(line);
                    }
                }

                // Check if the item is borrowed and display loan details if applicable
                if (!selectedItem.IsAvailable)
                {
                    // Display loan details using the helper method
                    DisplayLoanDetails(selectedItem.Id);
                }
            }
        }

        /// <summary>
        /// Method to split a long line into smaller chunks to make sure
        /// that the description fits in the ListBox's width.
        /// </summary>
        /// <param name="line"></param> Each line of text.
        /// <param name="maxLength"></param> Maximum length of each line.
        /// <returns></returns>
        private List<string> SplitLongLine(string line, int maxLength)
        {
            var result = new List<string>();
            var words = line.Split(' ');

            var currentLine = new StringBuilder();

            foreach (var word in words)
            {
                if (currentLine.Length + word.Length + 1 > maxLength)
                {
                    result.Add(currentLine.ToString());
                    currentLine.Clear();
                }

                if (currentLine.Length > 0)
                {
                    currentLine.Append(' ');
                }

                currentLine.Append(word);
            }

            if (currentLine.Length > 0)
            {
                result.Add(currentLine.ToString());
            }
            return result;
        }

        /// <summary>
        /// Method to display the loan details of the selected item.
        /// </summary>
        /// <param name="itemId"></param> Id of the selected item.
        private void DisplayLoanDetails(int itemId)
        {
            using (var context = new LibraryContext())
            {
                // Fetch the active loan for the given item
                var loan = context.Loans.FirstOrDefault(l => l.LibraryItemId == itemId && l.DueDate > DateTime.Now);

                if (loan != null)
                {
                    DesclistBox.Items.Add(""); // Add a blank line for spacing
                    DesclistBox.Items.Add("Loan Details:");
                    DesclistBox.Items.Add($"Borrower: {loan.BorrowerName}");
                    DesclistBox.Items.Add($"Email: {loan.BorrowerEmail}");
                    DesclistBox.Items.Add($"Loan Date: {loan.LoanDate.ToShortDateString()}");
                    DesclistBox.Items.Add($"Due Date: {loan.DueDate.ToShortDateString()}");
                }
            }
        }

        /// <summary>
        /// Method to display the detailed description of the selected item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayDetailedInfo();
        }

        /// <summary>
        /// Method to edit the selected item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editBtn_Click(object sender, EventArgs e)
        {
            if (ItemsListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an item to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get the selected item from the ListBox
            var selectedItem = (LibraryItem)ItemsListBox.SelectedItem;

            using (LibraryItemsForm libraryItemsForm = new LibraryItemsForm(selectedItem))
            {
                libraryItemsForm.ShowDialog();

                if (libraryItemsForm.DialogResult == DialogResult.OK)
                {
                    // Use the updated item from the form
                    var updatedItem = libraryItemsForm.LibraryItem;

                    // Update the database with the new values
                    libraryItemsForm.UpdateLibraryItem(updatedItem);

                    // Refresh the displayed list
                    DisplayLibraryItems();

                    // Clear previous loan details
                    DesclistBox.Items.Clear();

                    // Display the updated item details in the ListBox
                    DisplayDetailedInfo(); // Reload item details, including loan if the item is borrowed

                    // Explicitly reload and display loan information for the updated item if borrowed
                    if (!updatedItem.IsAvailable)
                    {
                        // Display the updated loan details using the helper method
                        DisplayLoanDetails(updatedItem.Id);
                    }
                }
            }

            // After marking the item as borrowed and updating the database
            ItemsListBox.Refresh();  // Ensure the availability status is updated in the ListBox

            ItemsListBox.SelectedIndex = -1;
        }

        /// <summary>
        /// Method to borrow the selected item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BorrowBtn_Click(object sender, EventArgs e)
        {
            // Ensure an item is selected
            if (ItemsListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an item to borrow.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get the selected LibraryItem
            var selectedItem = (LibraryItem)ItemsListBox.SelectedItem;

            // Check if the item is already borrowed
            if (!selectedItem.IsAvailable)
            {
                MessageBox.Show("This item is already borrowed.", "Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Open the LoanForm to collect borrower details
            using (var loanForm = new LoanForm())
            {
                if (loanForm.ShowDialog() == DialogResult.OK)
                {
                    // Create a new Loan object with data from the LoanForm
                    var newLoan = new Loan
                    {
                        BorrowerName = loanForm.BorrowerName,
                        BorrowerEmail = loanForm.BorrowerEmail,
                        LoanDate = DateTime.Now.Date,
                        DueDate = DateTime.Now.Date.AddDays(7),
                        LibraryItemId = selectedItem.Id
                    };

                    // Save the loan to the database and mark the item as unavailable
                    using (var context = new LibraryContext())
                    {
                        context.Loans.Add(newLoan);                 // Add the new loan
                        context.LibraryItems.Attach(selectedItem);  // Attach the selected item to the context
                        context.Entry(selectedItem).State = EntityState.Modified;  // Mark it as modified
                        selectedItem.IsAvailable = false;           // Mark the item as unavailable
                        context.SaveChanges();                      // Commit changes to the database
                    }

                    // Refresh the ListBox to update the display
                    ItemsListBox.Refresh();  // This will trigger the ToString() method again

                    MessageBox.Show("The item has been successfully borrowed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            DisplayLibraryItems();
        }

        /// <summary>
        /// Method to return the selected item.
        /// </summary>
        /// <param name="sender"></param> The sender object.
        /// <param name="e"></param> The event arguments.
        private void returnBtn_Click(object sender, EventArgs e)
        {
            // Ensure an item is selected
            if (ItemsListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an item to return.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Retrieve the selected LibraryItem object
            var selectedItem = (LibraryItem)ItemsListBox.SelectedItem;

            using (var context = new LibraryContext())
            {
                // Find the item in the database
                var itemToReturn = context.LibraryItems.Find(selectedItem.Id);

                if (itemToReturn != null)
                {
                    // Find the active loan for the item (if it exists)
                    var loanToRemove = context.Loans.FirstOrDefault(l => l.LibraryItemId == itemToReturn.Id && l.DueDate > DateTime.Now);

                    if (loanToRemove != null)
                    {
                        // Remove the loan from the database
                        context.Loans.Remove(loanToRemove);

                        // Mark the item as available again
                        itemToReturn.IsAvailable = true;

                        // Save the changes to the database
                        context.SaveChanges();

                        MessageBox.Show("The item has been successfully returned.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("This item is not currently borrowed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Item not found in the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Refresh the display
            DisplayLibraryItems();
            ItemsListBox.SelectedIndex = -1;
            DesclistBox.Items.Clear();
        }
    }
}
