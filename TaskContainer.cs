using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskTrackerCLI
{
    internal class TaskContainer
    {
        private List<Task_> tasks = new List<Task_>();
        public void SetTasks(List<Task_> tasks)
        {
            this.tasks = tasks ?? new List<Task_>();
        }
        public List<Task_> GetTasks()
        {
            return this.tasks;
        }
        public void Add(Task_ task)
        {
            tasks.Add(task);
            SerializeIntoJson();
        }
        public void Update(int ID, string newDescription)
        {
            var task = tasks.FirstOrDefault(t => t.ID == ID);
            if (task != null)
            {
                task.Description = newDescription;
                task.Updated = DateTime.Now;
                SerializeIntoJson();
            }
        }
        public void Delete(int ID)
        {
            var task = tasks.FirstOrDefault(t => t.ID == ID);
            if (task != null)
            {
                tasks.Remove(task);
                SerializeIntoJson();
            }
        }
        public void DeleteAll()
        {
            for(int i = 0; i < tasks.Count; i++)
            {
                tasks.Clear();
            }
            SerializeIntoJson();
        }
        public void MarkInProgress(int ID)
        {
            var task = tasks.FirstOrDefault(t => t.ID == ID);
            if (task != null)
            {
                task.Status = "In Progress";
            }
            SerializeIntoJson();
        }
        public void MarkDone(int ID)
        {
            var task = tasks.FirstOrDefault(t => t.ID == ID);
            if (task != null)
            {
                task.Status = "Done";
            }
            SerializeIntoJson();
        }
        public void TaskList()
        {
            for(int i = 0; i < tasks.Count; i++)
            {
                var task = tasks[i];
                Console.WriteLine(task.ToString());
            }
        }
        public void TaskListByStatus(string status)
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                var task = tasks[i];
                if(task.Status.ToLower().Equals(status.ToLower()))
                {
                    Console.WriteLine(task.ToString());
                    Console.WriteLine(status);
                }
            }
        }

        public int GetCount()
        {
            return tasks.Count;
        }
        private void SerializeIntoJson()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(tasks, options);
            File.WriteAllText("Tasks.json", jsonString);
        }

    }
}
