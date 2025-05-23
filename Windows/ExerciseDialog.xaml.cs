using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Gymplanner;
using Gymplanner.CS;

namespace Gymplanner.Windows
{
    public partial class ExerciseDialog : Window
    {
        private readonly Data _dataService = new Data();
        private readonly Exercise _exercise;
        private readonly bool _isEdit;

        public ExerciseDialog()
        {
            InitializeComponent();
            Title = "Add Exercise";
            _exercise = new Exercise();
            OkButton.Click += OkButton_Click;
            CancelButton.Click += (s, e) => DialogResult = false;
        }

        public ExerciseDialog(Exercise exercise) : this()
        {
            Title = "Edit Exercise";
            _isEdit = true;
            _exercise = exercise;
            NameTextBox.Text = exercise.Name;
            DescriptionTextBox.Text = exercise.Description;
            MuscleGroupTextBox.Text = exercise.MuscleGroup;
            DifficultyComboBox.SelectedItem = DifficultyComboBox.Items.Cast<ComboBoxItem>()
                .FirstOrDefault(i => i.Content.ToString() == exercise.Difficulty);
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            _exercise.Name = NameTextBox.Text;
            _exercise.Description = DescriptionTextBox.Text;
            _exercise.MuscleGroup = MuscleGroupTextBox.Text;
            _exercise.Difficulty = (DifficultyComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (_isEdit)
            {
                // TODO: implement Data.UpdateExercise(_exercise)
            }
            else
            {
                _dataService.InsertExercise(_exercise);
            }
            DialogResult = true;
        }
    }
}