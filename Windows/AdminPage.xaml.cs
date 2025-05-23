﻿using System.Collections.ObjectModel;
using System.Windows;
using Gymplanner.CS;    
using Gymplanner;      
namespace Gymplanner.Windows
{
    public partial class AdminPage : Window
    {
        private readonly Data data = new Data();

        public ObservableCollection<User> Users { get; }
        public ObservableCollection<Exercise> Exercises { get; } 

        public AdminPage()
        {
            InitializeComponent();

            Users = new ObservableCollection<User>();
            Exercises = new ObservableCollection<Exercise>();
            DataContext = this;

            AddUserButton.Click += AddUserButton_Click;
            AddExerciseButton.Click += AddExerciseButton_Click;

            LoadUsers();
            LoadExercises();
        }

        private void LoadUsers()
        {
            Users.Clear();
            foreach (var u in data.GetUsers())
                Users.Add(u);
        }

        private void LoadExercises()
        {
            Exercises.Clear();
            foreach (var ex in data.GetExercises())
                Exercises.Add(ex);
        }

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
            if ((s as FrameworkElement)?.DataContext is User u &&
                MessageBox.Show($"Delete {u.Username}?", "Confirm",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (data.DeleteUser(u.Id))
                    LoadUsers();
            }
        }

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
            if ((s as FrameworkElement)?.DataContext is Exercise ex &&
                MessageBox.Show($"Delete {ex.Name}?", "Confirm",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (data.DeleteExercise(ex.Id))
                    LoadExercises();
            }
        }
    }
}
