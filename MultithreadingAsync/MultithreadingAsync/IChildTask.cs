namespace MultiThreadAsync
{
    using System;
    
    public enum TaskState
    {
        New,
        InProgress,
        Completed
    }

    public interface IChildTask
    {
         void Execute();

         TaskState CurrentTaskState { get;  }
        
         string Name { get; }

         event EventHandler<TaskState> OnStateChange;
    }
}
