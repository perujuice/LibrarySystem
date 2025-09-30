using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A7
{
    /// <summary>
    /// Form for loaning a book to a borrower.
    /// </summary>
    public partial class LoanForm : Form
    {

        public string BorrowerName { get; private set; }
        public string BorrowerEmail { get; private set; }

        /// <summary>
        /// Constructor for LoanForm.
        /// </summary>
        public LoanForm()
        {
            InitializeComponent();
            InitializeGUI();
        }

        /// <summary>
        /// Initializes the GUI components.
        /// </summary>
        private void InitializeGUI()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// Event handler for the OK button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButton_Click(object sender, EventArgs e)
        {
            // Validate input fields
            if (string.IsNullOrWhiteSpace(nameBox.Text) || string.IsNullOrWhiteSpace(emailBox.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Incomplete Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BorrowerName = nameBox.Text;
            BorrowerEmail = emailBox.Text;

            DialogResult = DialogResult.OK; // Indicate success
            Close();
        }

        /// <summary>
        /// Event handler for the Cancel button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
