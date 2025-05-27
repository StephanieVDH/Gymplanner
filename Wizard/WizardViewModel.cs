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
        Level,
        Goal,
        Sessions,
        Duration,
        Focus,       // we merged Focus + FocusMuscles into one step
    }

    public class WizardViewModel : INotifyPropertyChanged
    {
        private readonly Data _repo = new Data();
        private readonly int _userId;
        // 1) The list of steps
        public ObservableCollection<Questions> Steps { get; }
            = new ObservableCollection<Questions>(
                Enum.GetValues(typeof(Questions)).Cast<Questions>());

        // 2) Current index / step
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

        public int SelectedGoalId { get; private set; }
        public int SelectedLevelId { get; private set; }
        public Questions CurrentStep => Steps[CurrentIndex];

        // 3) Navigation commands
        public ICommand NextCommand { get; }
        public ICommand PrevCommand { get; }

        // 4) Selection commands for the first half of the wizard
        public ICommand SelectGoalCommand =>
        new RelayCommand(p => { SelectedGoalId = Convert.ToInt32(p); AdvanceStep(); });

        public ICommand SelectLevelCommand =>
        new RelayCommand(p => { SelectedLevelId = Convert.ToInt32(p); AdvanceStep(); });
        public ICommand SelectSessionsCommand { get; }
        public ICommand SelectDurationCommand { get; }
        public ICommand SelectFocusCommand { get; }
        public ICommand CommitMuscleSelectionCommand { get; }
        public ICommand ToggleMuscleCommand { get; }
        public ICommand FinishCommand { get; }

        // 6) Data holders for the user’s answers
        public string SelectedLevel { get; private set; }
        public string SelectedGoal { get; private set; }
        public string SelectedSessions { get; private set; }
        public string SelectedDuration { get; private set; }
        public string SelectedFocus { get; private set; }

        // 7) The full list of muscle‐group options
        public ObservableCollection<string> AllMuscleOptions { get; }
            = new ObservableCollection<string>
            {
                "Back",
                "Chest",
                "Shoulders",
                "Core/Abs",
                "Biceps",
                "Forearms",
                "Triceps",
                "Calves",
                "Glutes",
                "Hamstrings",
                "Quadriceps",
                "Hip Abductors",
                "Hip Adductors"
            };

        // 8) Which ones the user has checked
        public ObservableCollection<string> SelectedMuscles { get; }
            = new ObservableCollection<string>();

        public WizardViewModel(int userId)
        {
            _userId = userId;
            // wire up the basic selection commands (auto‐advance)
            SelectSessionsCommand = new RelayCommand(p => { SelectedSessions = p?.ToString(); AdvanceStep(); });
            SelectDurationCommand = new RelayCommand(p => { SelectedDuration = p?.ToString(); AdvanceStep(); });
            SelectFocusCommand = new RelayCommand(p => { SelectedFocus = p?.ToString(); /* do NOT advance here if you want them to pick muscles too */ });
            ToggleMuscleCommand = new RelayCommand(p =>
            {
                var muscle = p?.ToString();
                if (muscle == null) return;
                if (SelectedMuscles.Contains(muscle))
                    SelectedMuscles.Remove(muscle);
                else
                    SelectedMuscles.Add(muscle);
            });

            // CommitMuscleSelection only advances once they hit “Done”
            CommitMuscleSelectionCommand = new RelayCommand(_ => AdvanceStep());

            // standard next/prev (if you still need them)
            NextCommand = new RelayCommand(_ => AdvanceStep(), _ => CurrentIndex < Steps.Count - 1);
            PrevCommand = new RelayCommand(_ => RetreatStep(), _ => CurrentIndex > 0);

            FinishCommand = new RelayCommand(_ =>
            {
                int prefId = _repo.InsertUserPreferences(
                    userId: _userId,
                    goalId: SelectedGoalId,
                    levelId: SelectedLevelId,
                    availableDaysPerWeek: int.Parse(SelectedSessions),
                    sessionDurationMinutes: int.Parse(SelectedDuration.Split(' ')[0]),
                    muscleGroupNames: SelectedMuscles
                );


                if (prefId > 0)
                {
                    // success handling
                }
                else
                {
                    // error handling
                }
            },
            _ => CurrentStep == Questions.Focus);
        }

        private void AdvanceStep()
        {
            if (CurrentIndex < Steps.Count - 1)
                CurrentIndex++;
        }

        private void RetreatStep()
        {
            if (CurrentIndex > 0)
                CurrentIndex--;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }

    // Simple ICommand implementation
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? (_ => true);
        }

        public bool CanExecute(object parameter) => _canExecute(parameter);
        public void Execute(object parameter) => _execute(parameter);
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }


    }
}



