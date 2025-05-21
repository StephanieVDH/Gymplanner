using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymplanner.CS
{
    public class Exercise
    {
        /// <summary>
        /// Primary key identifier.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The name of the exercise.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The primary muscle group targeted by this exercise.
        /// </summary>
        public string MuscleGroup { get; set; }

        /// <summary>
        /// The difficulty level (e.g., Beginner, Intermediate, Advanced).
        /// </summary>
        public string Difficulty { get; set; }
        public string Description { get; set; }
    }
}

