using System.Linq;
using System.Windows;
using System.Collections.Generic;
using Gymplanner.CS;   // where your Exercise, MuscleGroup, DifficultyLevel live

namespace Gymplanner.Windows
{
    public partial class ExerciseDialog : Window
    {
        private readonly Data _data = new Data();
        private readonly bool _isEdit;

        public string DialogTitle { get; set; }

        // look-ups for the list controls:
        private List<DifficultyLevel> _difficulties;
        private List<MuscleGroup> _allMuscleGroups;

        /// <summary>
        /// When the dialog closes OK, this is the newly inserted or edited Exercise.
        /// </summary>
        public Exercise Current { get; private set; }

        public ExerciseDialog()
        {
            InitializeComponent();
            _isEdit = false;
            DialogTitle = "Add Exercise";
            LoadLookups();
            DataContext = this;
        }

        public ExerciseDialog(Exercise toEdit) : this()
        {
            _isEdit = true;
            Current = toEdit;
            DialogTitle = "Edit Exercise";

            // pre-fill text fields:
            NameText.Text = toEdit.Name;
            DescText.Text = toEdit.Description;

            // pick the correct difficulty:
            DiffBox.SelectedItem = _difficulties
                .FirstOrDefault(d => d.Id == toEdit.DifficultyId);

            // select the muscle-groups in the multi-select list:
            var names = toEdit.MuscleGroupNames
                .Split(',', ' ')
                .Select(s => s.Trim())
                .Where(s => !string.IsNullOrEmpty(s))
                .ToArray();

            foreach (var mg in _allMuscleGroups
                         .Where(m => names.Contains(m.Name)))
            {
                MuscleList.SelectedItems.Add(mg);
            }
        }

        private void LoadLookups()
        {
            _difficulties = _data.GetDifficultyLevels();
            _allMuscleGroups = _data.GetMuscleGroups();

            DiffBox.ItemsSource = _difficulties;
            MuscleList.ItemsSource = _allMuscleGroups;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            // gather inputs
            var name = NameText.Text.Trim();
            var desc = DescText.Text.Trim();
            var diff = (DifficultyLevel)DiffBox.SelectedItem;
            var selectedMGs = MuscleList.SelectedItems
                                  .Cast<MuscleGroup>()
                                  .ToList();

            if (string.IsNullOrEmpty(name)
             || diff == null
             || selectedMGs.Count == 0)
            {
                MessageBox.Show(
                  "Name, difficulty and at least one muscle group are required.",
                  "Validation",
                  MessageBoxButton.OK,
                  MessageBoxImage.Warning
                );
                return;
            }

            // build the comma-list for display
            var mgNames = string.Join(
                ", ",
                selectedMGs.Select(m => m.Name)
            );
            var mgIds = selectedMGs
                .Select(m => m.Id)
                .ToList();

            if (_isEdit)
            {
                // update the Current instance
                Current.Name = name;
                Current.Description = desc;
                Current.DifficultyId = diff.Id;
                Current.Difficulty = diff.Name;
                Current.MuscleGroupIds = mgIds;
                Current.MuscleGroupNames = mgNames;

                // persist via Data.cs
                _data.UpdateExercise(
                  Current,
                  diff.Id,
                  mgIds
                );
            }
            else
            {
                // create a fresh Exercise
                var ex = new Exercise
                {
                    Name = name,
                    Description = desc,
                    DifficultyId = diff.Id,
                    Difficulty = diff.Name,
                    MuscleGroupIds = mgIds,
                    MuscleGroupNames = mgNames
                };

                // insert and get the new PK
                int newId = _data.InsertExercise(
                  ex,
                  diff.Id,
                  mgIds
                );
                ex.Id = newId;
                Current = ex;
            }

            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}

