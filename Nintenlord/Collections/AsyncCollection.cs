using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nintenlord.Collections
{
    public sealed class AsyncCollection
    {
        private readonly List<Task> tasks = new List<Task>();

        public void AddTask(Task task)
        {
            tasks.Add(task);
        }

        public async Task WaitAll()
        {
            while (tasks.Count > 0)
            {
                var finishedTask = await Task.WhenAny(tasks);
                tasks.Remove(finishedTask);
            }
        }

        public void ClearFinished()
        {
            for (int i = tasks.Count - 1; i >= 0; i--)
            {
                switch (tasks[i].Status)
                {
                    case TaskStatus.Faulted:
                    case TaskStatus.Canceled:
                    case TaskStatus.RanToCompletion:
                        tasks.RemoveAt(i);
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
