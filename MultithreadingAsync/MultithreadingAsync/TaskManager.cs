namespace MultiThreadAsync
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class TaskManager
    {
        public Queue<IChildTask> QueueOfChildTasks;
        public bool IsExecuting;

        public TaskManager()
        {
            this.QueueOfChildTasks = new Queue<IChildTask>();
        }

        public void AddTask(IChildTask childTask)
        {
            Console.WriteLine($"TaskManager Status: Adding a child task of name {childTask.Name}");
            lock (this.QueueOfChildTasks)
            {
                this.QueueOfChildTasks.Enqueue(childTask);
            }
            if (!this.IsExecuting)
            {
                this.ExecuteNextTask();
            }
        }

        private void ChildTask_OnStateChange(object sender, TaskState e)
        {
            if (e == TaskState.InProgress)
            {
                this.IsExecuting = true;
            }
            else if (e == TaskState.Completed)
            {
                this.IsExecuting = false;
                var childTask = (IChildTask)sender;
                Console.WriteLine($"TaskManager Status: Finished task --> {childTask.Name}");
                childTask.OnStateChange -= this.ChildTask_OnStateChange;
                this.ExecuteNextTask();
            }
        }
       
        public void ExecuteNextTask()
        {
            if (this.QueueOfChildTasks.Count > 0)
            {
                lock (this.QueueOfChildTasks)
                {
                    var childTask = this.QueueOfChildTasks.Dequeue();

                    childTask.OnStateChange += this.ChildTask_OnStateChange;

                    // Async call to start
                    var task = new Task(() => { childTask.Execute(); });
                    task.Start();
                }
            }
        }

    }
}
