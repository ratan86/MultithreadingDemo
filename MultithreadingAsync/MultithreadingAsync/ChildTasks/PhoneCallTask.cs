namespace MultiThreadAsync.ChildTasks
{
    using System;
    using System.Threading;

    class PhoneCallTask : IChildTask
    {
        private TaskState currentTaskState;
        private readonly int lifeSpan;
        private readonly string name;

        public event EventHandler<TaskState> OnStateChange;

        public PhoneCallTask(int milliSecondsToRun)
        {
            this.currentTaskState = TaskState.New;
            this.lifeSpan = milliSecondsToRun;
            this.name = Guid.NewGuid().ToString();
        }

        public string Name
        {
            get => this.name;
        }

        public TaskState CurrentTaskState
        {
            get => this.currentTaskState;
            set
            {
                this.currentTaskState = value;
                this.OnStateChange?.Invoke(this, this.currentTaskState);
            }
        }

        public void Execute()
        {
            this.currentTaskState = TaskState.InProgress;
            var timePeriod = this.lifeSpan / 5;
            var endTime = DateTime.Now.AddMilliseconds(this.lifeSpan);
            
            while (DateTime.Now < endTime)
            {
                var remainingTime = endTime - DateTime.Now;
                Console.WriteLine($"Executing PhoneCall Task {this.Name} -> RemainingTime in sec: {remainingTime.TotalSeconds}");
                Thread.Sleep(timePeriod);
            }

            this.currentTaskState = TaskState.Completed;
        }
    }
}

