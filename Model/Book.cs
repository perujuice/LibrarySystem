using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A7
{
    /// <summary>
    /// Class that represents a book in the library.
    /// </summary>
    internal class Book : LibraryItem
    {
        // private fields
        private string author;
        private string isbn;
        private int nrPages;

        /// <summary>
        /// Property that gets or sets the author of the book.
        /// </summary>
        public string Author
        {
            get { return author; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Author cannot be null or empty.");
                }
                author = value;
            }
        }

        /// <summary>
        /// Property that gets or sets the ISBN of the book.
        /// </summary>
        public string ISBN
        {
            get { return isbn; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("ISBN cannot be null or empty.");
                }
                isbn = value;
            }
        }

        /// <summary>
        /// Property that gets or sets the number of pages of the book.
        /// </summary>
        public int NrPages
        {
            get { return nrPages; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Number of pages must be greater than 0.");
                }
                nrPages = value;
            }
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Book() { }

        /// <summary>
        /// Constructor that initializes the book with the given values.
        /// </summary>
        /// <param name="title"></param> Name of the book.
        /// <param name="publishedDate"></param> Date when the book was published.
        /// <param name="description"></param> Description of the book.
        /// <param name="author"></param> Author of the book.
        /// <param name="isbn"></param> ISBN of the book.
        /// <param name="nrPages"></param> Number of pages of the book.
        public Book(string title, DateTime publishedDate, string description, string author, string isbn, int nrPages)
            : base(title, publishedDate, description)
        {
            Author = author;
            ISBN = isbn;
            NrPages = nrPages;
        }

        /// <summary>
        /// Method that returns a string with the information of the book.
        /// </summary>
        /// <returns></returns> String with the information of the book.
        public override string GetDisplayText()
        {
            string outStr = $"Book: {Title}\n";
            outStr += $"Author: {Author}\n";
            outStr += $"ISBN: {ISBN}\n";
            outStr += $"Published Date: {PublishedDate}\n";
            outStr += $"Number of Pages: {NrPages}\n";
            outStr += $"Description: {Description}\n";
            return outStr;
        }
    }
}
