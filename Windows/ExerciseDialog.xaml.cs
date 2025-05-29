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
            // Mark as editing and swap the title
            _isEdit = true;
            Current = toEdit;
            DialogTitle = "Edit Exercise";

            // Because we just changed DialogTitle, re‐apply DataContext so the window title updates:
            DataContext = null;
            DataContext = this;

            // 1) Pre-fill text fields:
            NameText.Text = toEdit.Name;
            DescText.Text = toEdit.Description;

            // 2) Pick the correct difficulty by matching the ID
            DiffBox.SelectedValuePath = nameof(DifficultyLevel.Id);
            DiffBox.DisplayMemberPath = nameof(DifficultyLevel.Name);
            DiffBox.SelectedValue = toEdit.DifficultyId;

            // 3) Split only on commas to preserve names with spaces
            var selectedNames = toEdit.MuscleGroupNames
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(n => n.Trim())
                .ToHashSet();

            // 4) Select each matching MuscleGroup in our ListBox
            foreach (var mg in _allMuscleGroups
                .Where(m => selectedNames.Contains(m.Name)))
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
            // 1) Gather inputs
            var name = NameText.Text.Trim();
            var desc = DescText.Text.Trim();
            var diff = DiffBox.SelectedItem as DifficultyLevel;
            var selectedMGs = MuscleList.SelectedItems
                                  .Cast<MuscleGroup>()
                                  .ToList();

            // 2) Validate
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

            // 3) Prepare lists for display & storage
            var mgNames = string.Join(", ", selectedMGs.Select(m => m.Name));
            var mgIds = selectedMGs.Select(m => m.Id).ToList();

            if (_isEdit)
            {
                // 4a) Edit existing Exercise
                Current.Name = name;
                Current.Description = desc;
                Current.DifficultyId = diff.Id;
                Current.Difficulty = diff.Name;
                Current.MuscleGroupIds = mgIds;
                Current.MuscleGroupNames = mgNames;

                // 5a) Persist changes
                _data.UpdateExercise(Current, diff.Id, mgIds);
            }
            else
            {
                // 4b) Create new Exercise instance
                var ex = new Exercise
                {
                    Name = name,
                    Description = desc,
                    DifficultyId = diff.Id,
                    Difficulty = diff.Name,
                    MuscleGroupIds = mgIds,
                    MuscleGroupNames = mgNames
                };

                // 5b) Insert and capture new primary key
                int newId = _data.InsertExercise(ex, diff.Id, mgIds);
                ex.Id = newId;
                Current = ex;
            }

            // 6) Close dialog with success
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}

