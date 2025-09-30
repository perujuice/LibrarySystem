using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A7
{
    /// <summary>
    /// Represents a loan of a library item.
    /// </summary>
    internal class Loan
    {
        // Fields - private by default
        private int id;
        private string borrowerName;
        private string borrowerEmail;
        private DateTime loanDate;
        private DateTime dueDate;
        private int libraryItemId;

        /// <summary>
        /// Property for the Id field.
        /// </summary>
        public int Id 
        { 
            get => id;
            set => id = value;
        }

        /// <summary>
        /// Gets or sets the name of the borrower.
        /// </summary>
        public string BorrowerName
        {
            get { return borrowerName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Borrower name cannot be null or empty.");
                }
                borrowerName = value;
            }
        }

        /// <summary>
        /// Gets or sets the email of the borrower.
        /// </summary>
        public string BorrowerEmail
        {
            get { return borrowerEmail; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Borrower email cannot be null or empty.");
                }
                borrowerEmail = value;
            }
        }

        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public int LibraryItemId { get; set; }
        public LibraryItem LibraryItem { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Loan() 
        {
        }

        /// <summary>
        /// Constructor that initializes the fields.
        /// </summary>
        /// <param name="borrowerName"></param> Name of the borrower. 
        /// <param name="borrowerEmail"></param> Email of the borrower.
        /// <param name="loanDate"></param> Date the loan was made.
        /// <param name="dueDate"></param> Date the loan is due.
        /// <param name="libraryItemId"></param> Id of the library item.
        public Loan(string borrowerName, string borrowerEmail, DateTime loanDate, DateTime dueDate, int libraryItemId)
        {
            BorrowerName = borrowerName;
            BorrowerEmail = borrowerEmail;
            LoanDate = loanDate;
            DueDate = dueDate;
            LibraryItemId = libraryItemId;
        }

        /// <summary>
        /// Returns a string representation of the loan.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0,-20} {1,-20} {2,-20} {3,-20}}", this.BorrowerName, this.BorrowerEmail, this.LoanDate, this.DueDate);
        }
    }
}
