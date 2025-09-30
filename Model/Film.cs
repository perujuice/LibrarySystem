using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A7
{
    /// <summary>
    /// Represents a film in the library.
    /// </summary>
    internal class Film : LibraryItem
    {
        private string director;
        private int duration;

        /// <summary>
        /// Property for the director of the film.
        /// </summary>
        public string Director 
        { 
            get {  return director; }
            set
            { 
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Director cannot be null or empty.");
                }
                director = value;
            }
        }

        /// <summary>
        /// Property for the duration of the film.
        /// </summary>
        public int Duration 
        { 
            get { return duration; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Duration must be greater than 0.");
                }
                duration = value;
            }
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Film() { }

        /// <summary>
        /// Constructor that initializes the film with the given values.
        /// </summary>
        /// <param name="title"></param> Title for the film.
        /// <param name="publishedDate"></param> Date the film was published.
        /// <param name="description"></param> Description of the film.
        /// <param name="director"></param> Director of the film.
        /// <param name="duration"></param> Duration of the film.
        public Film(string title, DateTime publishedDate, string description, string director, int duration)
            : base(title, publishedDate, description)
        {
            Director = director;
            Duration = duration;
        }

        /// <summary>
        /// Returns a string representation of the film.
        /// </summary>
        /// <returns></returns> String representation of the film.
        public override string GetDisplayText()
        {
            string outStr = $"Film: {Title}\n";
            outStr += $"Published Date: {PublishedDate}\n";
            outStr += $"Director: {Director}\n";
            outStr += $"Duration: {Duration}\n";
            outStr += $"Description: {Description}\n";
            return outStr;
        }
    }
}
