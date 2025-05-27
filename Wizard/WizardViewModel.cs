using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Gymplanner.Wizard
{
    // 1) Wizard steps
    public enum Questions
    {
        Level,
        Goal,
        Sessions,
        Duration,
        Focus   // now handles both broad focus and muscle selection
    }

    // 2) Simple DTO to pair muscle_group_id with its display name
    public class MuscleOption
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString() => Name; // handy for any default bindings
    }

    public class WizardViewModel : INotifyPropertyChanged
    {
        // ── fields ───────────────────────────────────────────────────────────
        private readonly Data _repo = new Data();
        private readonly int _userId;

        // ── steps & navigation ────────────────────────────────────────────────
        public ObservableCollection<Questions> Steps { get; }
            = new ObservableCollection<Questions>(
                Enum.GetValues(typeof(Questions)).Cast<Questions>());

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

        public ICommand NextCommand { get; }
        public ICommand PrevCommand { get; }

        // ── scalar selections ─────────────────────────────────────────────────
        public int SelectedLevelId { get; private set; }
        public int SelectedGoalId { get; private set; }
        public string SelectedSessions { get; private set; }
        public string SelectedDuration { get; private set; }
        private int _selectedFocusId;
        public int SelectedFocusId
        {
            get => _selectedFocusId;
            private set
            {
                if (_selectedFocusId == value) return;
                _selectedFocusId = value;
                OnPropertyChanged();
            }
        }

        // ── multi‐select muscles ───────────────────────────────────────────────
        public ObservableCollection<MuscleOption> AllMuscleOptions { get; }
            = new ObservableCollection<MuscleOption>
            {
                new MuscleOption { Id = 1,  Name = "Back" },
                new MuscleOption { Id = 2,  Name = "Chest" },
                new MuscleOption { Id = 3,  Name = "Shoulders" },
                new MuscleOption { Id = 4,  Name = "Core/Abs" },
                new MuscleOption { Id = 5,  Name = "Biceps" },
                new MuscleOption { Id = 6,  Name = "Forearms" },
                new MuscleOption { Id = 7,  Name = "Triceps" },
                new MuscleOption { Id = 8,  Name = "Calves" },
                new MuscleOption { Id = 9,  Name = "Glutes" },
                new MuscleOption { Id = 10, Name = "Hamstrings" },
                new MuscleOption { Id = 11, Name = "Quadriceps" },
                new MuscleOption { Id = 12, Name = "Hip Abductors" },
                new MuscleOption { Id = 13, Name = "Hip Adductors" }
            };

        public ObservableCollection<int> SelectedMuscleIds { get; }
            = new ObservableCollection<int>();

        // ── commands ──────────────────────────────────────────────────────────
        public ICommand SelectLevelCommand { get; }
        public ICommand SelectGoalCommand { get; }
        public ICommand SelectSessionsCommand { get; }
        public ICommand SelectDurationCommand { get; }
        public ICommand SelectFocusCommand { get; }
        public ICommand ToggleMuscleCommand { get; }
        public ICommand CommitMuscleSelectionCommand { get; }
        public ICommand FinishCommand { get; }
        public Action CloseAction { get; set; }

        // ── ctor ───────────────────────────────────────────────────────────────
        public WizardViewModel(int userId)
        {
            _userId = userId;

            SelectLevelCommand = new RelayCommand(p =>
            {
                SelectedLevelId = Convert.ToInt32(p);
                AdvanceStep();
            });

            SelectGoalCommand = new RelayCommand(p =>
            {
                SelectedGoalId = Convert.ToInt32(p);
                AdvanceStep();
            });

            SelectSessionsCommand = new RelayCommand(p =>
            {
                SelectedSessions = p?.ToString();
                AdvanceStep();
            });

            SelectDurationCommand = new RelayCommand(p =>
            {
                SelectedDuration = p?.ToString();
                AdvanceStep();
            });

            SelectFocusCommand = new RelayCommand(p =>
            {
                SelectedFocusId = Convert.ToInt32(p);
                // stay on this step so user can also pick specific muscles
            });

            ToggleMuscleCommand = new RelayCommand(p =>
            {
                int id = Convert.ToInt32(p);
                if (SelectedMuscleIds.Contains(id))
                    SelectedMuscleIds.Remove(id);
                else
                    SelectedMuscleIds.Add(id);
            });

            CommitMuscleSelectionCommand = new RelayCommand(_ =>
            {
                // after picking muscles, move to Finish
                AdvanceStep();
            });

            NextCommand = new RelayCommand(_ => AdvanceStep(),
                                          _ => CurrentIndex < Steps.Count - 1);
            PrevCommand = new RelayCommand(_ => RetreatStep(),
                                          _ => CurrentIndex > 0);

            FinishCommand = new RelayCommand(_ =>
            {
                // parse numeric values
                int sessions = int.Parse(SelectedSessions);
                int duration = int.Parse(SelectedDuration.Split(' ')[0]);

                // insert into user_preferences + join table
                int prefId = _repo.InsertUserPreferences(
                    userId: _userId,
                    goalId: SelectedGoalId,
                    levelId: SelectedLevelId,
                    availableDaysPerWeek: sessions,
                    sessionDurationMinutes: duration,
                    muscleGroupIds: SelectedMuscleIds
                );

                if (prefId > 0)
                {
                    // TODO: notify success / close window
                    System.Windows.MessageBox.Show(
                    "Your workout preferences have been saved!",
                    "Success",
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Information);
                    CloseAction?.Invoke();
                }
                else
                {
                    System.Windows.MessageBox.Show(
                    "Oops—something went wrong saving your preferences. Please try again.",
                    "Error",
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Error);
                }
            },
            _ => CurrentStep == Questions.Focus);
        }

        // ── step navigation ───────────────────────────────────────────────────
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

        // ── INotifyPropertyChanged ────────────────────────────────────────────
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }

    // simple ICommand impl
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute,
                            Predicate<object> canExecute = null)
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



