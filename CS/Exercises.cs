using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymplanner.CS
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // FK to difficulty_levels
        public int DifficultyId { get; set; }
        public string Difficulty { get; set; } = string.Empty;

        // Display string
        public string MuscleGroupNames { get; set; } = string.Empty;

        // **The important bit**: all selected group IDs
        public List<int> MuscleGroupIds { get; set; } = new List<int>();
    }

    public class MuscleGroup
    {
        public int Id { get; set; }
        public string Name { get; set; } 
    }
    public class DifficultyLevel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}

