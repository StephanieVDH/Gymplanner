using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Gymplanner.CS;     // Exercise, User, DifficultyLevel, MuscleGroup

namespace Gymplanner.Windows
{
    public partial class AdminPage : Window
    {
        private readonly Data _data = new Data();

        // Users
        public ObservableCollection<User> Users { get; }

        // Exercises and lookups
        public ObservableCollection<Exercise> Exercises { get; }
        public List<DifficultyLevel> Difficulties { get; }
        public List<MuscleGroup> MuscleGroups { get; }

        // For filtering
        private readonly ICollectionView _exercisesView;

        public AdminPage()
        {
            InitializeComponent();

            // 1) Users
            Users = new ObservableCollection<User>();
            DataContext = this;
            LoadUsers();

            // 2) Exercises + lookups
            Exercises = new ObservableCollection<Exercise>();
            Difficulties = _data.GetDifficultyLevels();
            MuscleGroups = _data.GetMuscleGroups();

            // 3) Wrap for filtering
            _exercisesView = CollectionViewSource.GetDefaultView(Exercises);
            ExercisesItemsControl.ItemsSource = _exercisesView;

            // 4) Populate filter combos
            DifficultyFilterCombo.Items.Add(new DifficultyLevel { Id = 0, Name = "All" });
            foreach (var d in Difficulties) DifficultyFilterCombo.Items.Add(d);
            DifficultyFilterCombo.SelectedIndex = 0;
            DifficultyFilterCombo.SelectionChanged += OnFilterChanged;

            MuscleFilterCombo.Items.Add(new MuscleGroup { Id = 0, Name = "All" });
            foreach (var m in MuscleGroups) MuscleFilterCombo.Items.Add(m);
            MuscleFilterCombo.SelectedIndex = 0;
            MuscleFilterCombo.SelectionChanged += OnFilterChanged;

            // 5) Wire Add buttons
            AddUserButton.Click += AddUserButton_Click;
            AddExerciseButton.Click += AddExerciseButton_Click;

            // 6) Initial load
            LoadExercises();
        }

        #region Load Data

        private void LoadUsers()
        {
            Users.Clear();
            foreach (var u in _data.GetUsers())
                Users.Add(u);
        }

        private void LoadExercises()
        {
            Exercises.Clear();
            foreach (var ex in _data.GetExercises())
                Exercises.Add(ex);
            _exercisesView.Refresh();
        }

        #endregion

        #region User Actions

        private void AddUserButton_Click(object s, RoutedEventArgs e)
        {
            var dlg = new UserDialog();
            if (dlg.ShowDialog() == true)
                LoadUsers();
        }

        private void EditUserButton_Click(object s, RoutedEventArgs e)
        {
            if ((s as FrameworkElement)?.DataContext is User u)
            {
                var dlg = new UserDialog(u);
                if (dlg.ShowDialog() == true)
                    LoadUsers();
            }
        }

        private void DeleteUserButton_Click(object s, RoutedEventArgs e)
        {
            if ((s as FrameworkElement)?.DataContext is User u
             && MessageBox.Show($"Delete user {u.Username}?", "Confirm",
                                 MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (_data.DeleteUser(u.Id))
                    LoadUsers();
            }
        }

        #endregion

        #region Exercise Actions

        private void AddExerciseButton_Click(object s, RoutedEventArgs e)
        {
            var dlg = new ExerciseDialog();
            if (dlg.ShowDialog() == true)
                LoadExercises();
        }

        private void EditExerciseButton_Click(object s, RoutedEventArgs e)
        {
            if ((s as FrameworkElement)?.DataContext is Exercise ex)
            {
                var dlg = new ExerciseDialog(ex);
                if (dlg.ShowDialog() == true)
                    LoadExercises();
            }
        }

        private void DeleteExerciseButton_Click(object s, RoutedEventArgs e)
        {
            if ((s as FrameworkElement)?.DataContext is Exercise ex
             && MessageBox.Show($"Delete exercise {ex.Name}?", "Confirm",
                                 MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (_data.DeleteExercise(ex.Id))
                    LoadExercises();
            }
        }

        #endregion

        #region Filtering

        private void OnFilterChanged(object sender, SelectionChangedEventArgs e)
        {
            _exercisesView.Filter = obj =>
            {
                var ex = (Exercise)obj;

                // Difficulty filter
                if (DifficultyFilterCombo.SelectedItem is DifficultyLevel df
                 && df.Id != 0)
                {
                    if (ex.DifficultyId != df.Id)
                        return false;
                }

                // Muscle-group filter
                if (MuscleFilterCombo.SelectedItem is MuscleGroup mg
                 && mg.Id != 0)
                {
                    var names = ex.MuscleGroupNames?
                                .Split(',')
                                .Select(n => n.Trim())
                                .ToArray()
                                ?? Array.Empty<string>();

                    if (!names.Contains(mg.Name))
                        return false;
                }

                return true;
            };

            _exercisesView.Refresh();
        }

        #endregion
    }
}
