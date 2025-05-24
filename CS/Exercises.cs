﻿using System;
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
        public int DifficultyId { get; set; }
        public string Difficulty { get; set; } = string.Empty;
        public string MuscleGroupNames { get; set; } = string.Empty;
        public List<int> MuscleGroupIds { get; set; } = new();
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

