using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7._4
{
    public class TaskScheduler<TTask, TPriority>
    {
        private SortedDictionary<TPriority, Queue<TTask>> taskQueue;

        public TaskScheduler()
        {
            taskQueue = new SortedDictionary<TPriority, Queue<TTask>>();
        }

        public void AddTask(TTask task, TPriority priority)
        {
            if (!taskQueue.ContainsKey(priority))
            {
                taskQueue[priority] = new Queue<TTask>();
            }

            taskQueue[priority].Enqueue(task);
        }

        public void ExecuteTasks(TaskExecution<TTask> taskExecutor)
        {
            foreach (var priority in taskQueue.Keys)
            {
                Queue<TTask> tasks = taskQueue[priority];
                while (tasks.Count > 0)
                {
                    TTask task = tasks.Dequeue();
                    taskExecutor(task);
                }
            }
        }

        public void ExecuteNext(TaskExecution<TTask> taskExecutor)
        {
            if (taskQueue.Count > 0)
            {
                TPriority highestPriority = taskQueue.Keys.Max();
                TTask task = taskQueue[highestPriority].Dequeue();
                if (taskQueue[highestPriority].Count == 0)
                {
                    taskQueue.Remove(highestPriority);
                }

                taskExecutor(task);
            }
        }

        public void ResetAllTasks()
        {
            taskQueue.Clear();
        }
    }

    public delegate void TaskExecution<TTask>(TTask task);
}
