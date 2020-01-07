using System.Collections.Generic;

namespace ALD.LibFiscalCode.Progress
{
    public class ResetProgress
    {
        public int CompletedTasks { get; set; } = 1;
        public List<string> TaskDescriptions { get; } = new List<string>();
        public string CurrentTask { get; set; }

        public int Percentage => CompletedTasks * 100 / TaskDescriptions.Count;
    }
}