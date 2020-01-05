using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ALD.LibFiscalCode.Persistence.Events;
using ALD.LibFiscalCode.Persistence.Sqlite;
using ALD.LibFiscalCode.Progress;

namespace ALD.LibFiscalCode.ViewModels
{
    public class ResetViewModel : AbstractNotifyPropertyChanged
    {
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

        private bool _resetDataSource;
        private bool _resetHistory;

        public Progress<ResetProgress> CurrentProgress
        {
            get => currentProgress;
            set
            {
                currentProgress = value;
                OnPropertyChanged(nameof(CurrentProgress));
            }
        }

        private Progress<ResetProgress> currentProgress;

        public async void DropHistory(IProgress<ResetProgress> progress)
        {
            var reportProgress = new ResetProgress();

            using var context = new PlacesContext();
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
}