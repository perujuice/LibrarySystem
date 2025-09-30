using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A7
{
    /// <summary>
    /// Represents a newspaper in the library.
    /// </summary>
    internal class NewsPaper : LibraryItem
    {
        private string publisher;
        private string edition;

        /// <summary>
        /// Publisher of the newspaper.
        /// </summary>
        public string Publisher
        {
            get { return publisher; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Publisher cannot be null or empty.");
                }
                publisher = value;
            }
        }

        /// <summary>
        /// Edition of the newspaper.
        /// </summary>
        public string Edition
        {
            get { return edition; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Edition cannot be null or empty.");
                }
                edition = value;
            }
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NewsPaper() { }

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="title"></param> Name of the newspaper.
        /// <param name="publishedDate"></param> Date of publication.
        /// <param name="description"></param> Description of the newspaper.
        /// <param name="publisher"></param> Publisher of the newspaper.
        /// <param name="edition"></param> Edition of the newspaper.
        public NewsPaper(string title, DateTime publishedDate, string description, string publisher, string edition)
            : base(title, publishedDate, description)
        {
            Publisher = publisher;
            Edition = edition;
        }

        /// <summary>
        /// Returns the display text of the newspaper.
        /// </summary>
        /// <returns></returns> Display text of the newspaper.
        public override string GetDisplayText()
        {
            string outStr = $"Newspaper: {Title}\n";
            outStr += $"Published date: {PublishedDate}\n";
            outStr += $"Publisher: {Publisher}\n";
            outStr += $"Edition: {Edition}\n";
            outStr += $"Description: {Description}\n";
            return outStr;
        }
    }
}
