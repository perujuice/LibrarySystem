using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A7
{
    /// <summary>
    /// Represents a library item.
    /// </summary>
    internal abstract class LibraryItem
    {
        // Fields - private by default
        private int id;
        private string title;
        private DateTime publishedDate;
        private string description;
        private bool isAvailable;

        /// <summary>
        /// Property for the Id field.
        /// </summary>
        public int Id 
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Gets or sets the title of the library item.
        /// </summary>
        public string Title
        {
            get { return title; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Title cannot be null or empty.");
                }
                title = value;
            }
        }

        // Telling the database to store the date only, not the time
        [Column(TypeName = "date")]
        public DateTime PublishedDate 
        {
            get { return publishedDate; }
            set { publishedDate = value; }
        }

        /// <summary>
        /// Property for the Description field.
        /// </summary>
        public string Description
        {
            get { return description; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Description cannot be null or empty.");
                }
                description = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the library item is available.
        /// </summary>
        public bool IsAvailable { get; set; } = true;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public LibraryItem() { }

        /// <summary>
        /// Constructor that initializes the title, published date, and description.
        /// </summary>
        /// <param name="title"></param> Title of the library item.
        /// <param name="publishedDate"></param> Date the library item was published.
        /// <param name="description"></param> Description of the library item.
        public LibraryItem(string title, DateTime publishedDate, string description)
        {
            Title = title;
            PublishedDate = publishedDate;
            Description = description;
        }

        /// <summary>
        /// Returns a string representation of the library item.
        /// </summary>
        /// <returns></returns> A string representation of the library item.
        public override string ToString()
        {
            string itemType = this.GetType().Name;
            string availability = IsAvailable ? "Available" : "Not Available";
            return string.Format("{0,-22} {1,-17} {2,-20}", this.Title, itemType, availability);
        }

        /// <summary>
        /// Returns a string representation of the library item.
        /// </summary>
        /// <returns></returns> A string representation of the library item.
        public abstract string GetDisplayText();
    }
}