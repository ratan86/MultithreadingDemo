namespace MultiThreadAsync
{
    using MultiThreadAsync.ChildTasks;
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            int option = 0;

            var taskManager = new TaskManager();

            while (option != 3)
            {
                Console.WriteLine("1.Add File Task\n2.Add PhoneCall Task\n3.Exit\nYour option:");
                option = int.Parse(Console.ReadLine());
                var childLifeSpan = new int();
                IChildTask childTask;
                switch (option)
                {
                    case 1:
                        Console.WriteLine("Enter milliseconds to run:");
                        childLifeSpan = int.Parse(Console.ReadLine());
                        childTask = new FileTask(childLifeSpan);
                        taskManager.AddTask(childTask);
                        break;
                    case 2:
                        Console.WriteLine("Enter milliseconds to run:");
                        childLifeSpan = int.Parse(Console.ReadLine());
                        childTask = new PhoneCallTask(childLifeSpan);
                        taskManager.AddTask(childTask);
                        break;
                    case 3:
                        Console.WriteLine("Thank you:");
                        break;
                }
            }
        }
    }
}
