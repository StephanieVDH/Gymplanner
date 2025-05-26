using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Gymplanner.Wizard
{
    public enum Questions
    {
        Level,        // fitnessniveau
        Goal,         // afvallen, conditie, …
        Sessions,     // aantal sessies per week
        Duration,     // duur van de sessies
        Focus        // lichaamsdeel of spiergroepen kiezen
    }

    public class WizardViewModel : INotifyPropertyChanged
    {
        // The list of steps in order
        public ObservableCollection<Questions> Steps { get; }
            = new ObservableCollection<Questions>(
                Enum.GetValues(typeof(Questions)).Cast<Questions>());

        // Current step index & derived enum
        private int _currentIndex;
        public int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                if (_currentIndex == value) return;
                _currentIndex = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CurrentStep));
            }
        }
        public Questions CurrentStep => Steps[CurrentIndex];

        // Navigation commands
        public ICommand NextCommand { get; }
        public ICommand PrevCommand { get; }

        // Selection commands for each question
        public ICommand SelectLevelCommand { get; }
        public ICommand SelectGoalCommand { get; }
        public ICommand SelectSessionsCommand { get; }
        public ICommand SelectDurationCommand { get; }
        public ICommand SelectFocusCommand { get; }
        public ICommand SelectMusclesCommand { get; }

        // Properties to hold the user’s answers
        public string SelectedLevel { get; private set; }
        public string SelectedGoal { get; private set; }
        public string SelectedSessions { get; private set; }
        public string SelectedDuration { get; private set; }
        public string SelectedFocus { get; private set; }
        public string SelectedMuscles { get; private set; }

        public WizardViewModel()
        {
            // Initialize selection commands to capture answer & advance
            SelectLevelCommand = new RelayCommand(p => { SelectedLevel = p?.ToString(); AdvanceStep(); });
            SelectGoalCommand = new RelayCommand(p => { SelectedGoal = p?.ToString(); AdvanceStep(); });
            SelectSessionsCommand = new RelayCommand(p => { SelectedSessions = p?.ToString(); AdvanceStep(); });
            SelectDurationCommand = new RelayCommand(p => { SelectedDuration = p?.ToString(); AdvanceStep(); });
            SelectFocusCommand = new RelayCommand(p => { SelectedFocus = p?.ToString(); AdvanceStep(); });
            SelectMusclesCommand = new RelayCommand(p => { SelectedMuscles = p?.ToString(); AdvanceStep(); });

            // Navigation commands
            NextCommand = new RelayCommand(_ => AdvanceStep(),
                                           _ => CurrentIndex < Steps.Count - 1);
            PrevCommand = new RelayCommand(_ => RetreatStep(),
                                           _ => CurrentIndex > 0);
        }

        // Move forward one step
        private void AdvanceStep()
        {
            if (CurrentIndex < Steps.Count - 1)
                CurrentIndex++;
        }

        // Move backward one step
        private void RetreatStep()
        {
            if (CurrentIndex > 0)
                CurrentIndex--;
        }

        // INotifyPropertyChanged boilerplate
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }

    // Simple ICommand implementation
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;
        public RelayCommand(Action<object> execute, Predicate<object>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? (_ => true);
        }

        public bool CanExecute(object? parameter) => _canExecute(parameter!);
        public void Execute(object? parameter) => _execute(parameter!);
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value!;
            remove => CommandManager.RequerySuggested -= value!;
        }
    }
}


