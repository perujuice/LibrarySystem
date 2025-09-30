using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A7
{
    /// <summary>
    /// Represents an article in the library.
    /// </summary>
    internal class Article : LibraryItem
    {
        private string journal;
        private string author;

        /// <summary>
        /// Property for the journal field.
        /// </summary>
        public string Journal
        {
            get { return journal; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Journal cannot be null or empty.");
                }
                journal = value;
            }
        }

        /// <summary>
        /// Property for the author field.
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
        /// Default constructor.
        /// </summary>
        public Article()
        {
        }

        /// <summary>
        /// Constructor that initializes the fields.
        /// </summary>
        /// <param name="title"></param> The title of the article.
        /// <param name="publishedDate"></param> The date the article was published.
        /// <param name="description"></param> A brief description of the article.
        /// <param name="journal"></param> The journal the article was published in.
        /// <param name="author"></param> The author of the article.
        public Article(string title, DateTime publishedDate, string description, string journal, string author)
            : base(title, publishedDate, description)
        {
            Journal = journal;
            Author = author;
        }

        /// <summary>
        /// Returns a string representation of the article.
        /// </summary>
        /// <returns></returns> A string representation of the article.
        public override string GetDisplayText()
        {
            string outStr = $"Article: {Title}\n";
            outStr += $"Published date: {PublishedDate}\n";
            outStr += $"Journal: {Journal}\n";
            outStr += $"Author: {Author}\n";
            outStr += $"Description: {Description}\n";
            return outStr;
        }
    }
}