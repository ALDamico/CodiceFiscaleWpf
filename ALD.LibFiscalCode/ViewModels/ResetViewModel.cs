using System;
using ALD.LibFiscalCode.Persistence.Events;

//using ALD.LibFiscalCode.Progress;
/*
namespace ALD.LibFiscalCode.ViewModels
{
    public class ResetViewModel : AbstractNotifyPropertyChanged
    {
        private bool _resetDataSource;
        private bool _resetHistory;

        private Progress<ResetProgress> currentProgress;

        public ResetViewModel()
        {
            CurrentProgress = new Progress<ResetProgress>();
        }

        public bool ResetDataSource
        {
            get => _resetDataSource;
            set
            {
                _resetDataSource = value;
                OnPropertyChanged(nameof(ResetDataSource));
                OnPropertyChanged(nameof(CanStartRestore));
            }
        }

        public bool ResetHistory
        {
            get => _resetHistory;
            set
            {
                _resetHistory = value;
                OnPropertyChanged(nameof(ResetHistory));
                OnPropertyChanged(nameof(CanStartRestore));
            }
        }

        public bool CanStartRestore => ResetDataSource || ResetHistory;

        public Progress<ResetProgress> CurrentProgress
        {
            get => currentProgress;
            set
            {
                currentProgress = value;
                OnPropertyChanged(nameof(CurrentProgress));
            }
        }

        public async void DropHistory(IProgress<ResetProgress> progress)
        {
            var reportProgress = new ResetProgress();

            using var context = new AppDataContext();
            context.FiscalCodes.RemoveRange(context.FiscalCodes);
            reportProgress.TaskDescriptions.Add("Rimozione dei codici fiscali");
            reportProgress.TaskDescriptions.Add("Rimozione delle informazioni personali");
            reportProgress.CompletedTasks = 1;
            reportProgress.CurrentTask = reportProgress.TaskDescriptions[0];
            progress?.Report(reportProgress);
            context.People.RemoveRange(context.People);
            reportProgress.CompletedTasks = 2;
            reportProgress.CurrentTask = reportProgress.TaskDescriptions[1];
            progress?.Report(reportProgress);
            context.SaveChangesAsync();
        }
    }
}*/