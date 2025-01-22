using System.ComponentModel;
using System.Text.Json;
using TaskTrackerCLI;

internal class Program
{
    public static void Main(string[] args)
    {
        string fileName = "Tasks.json";
        //File.WriteAllText(fileName, String.Empty);
        CLI_Input(fileName);
    }
    public static void CLI_Input(string fileName)
    {
        TaskContainer taskContainer = new TaskContainer();

        try
        {
            if(File.Exists(fileName))
            {
                string jsonString = File.ReadAllText(fileName);
                if(!string.IsNullOrEmpty(jsonString))
                {
                    var tasks = JsonSerializer.Deserialize<List<Task_>>(jsonString);
                    if(tasks != null)
                    {
                        taskContainer.SetTasks(tasks);
                    }
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error reading or deserializing file: {ex.Message}");
            return;
        }


        bool isRunning = true;
        while (isRunning)
        {
            Console.WriteLine("Enter command: ");
            string input = Console.ReadLine()?.ToLower() ?? string.Empty;
            if (input.Equals("exit"))
            {
                break;
            }

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("No input detected");
                continue;
            }

            switch (input)
            {
                case "add":
                        int count = taskContainer.GetCount();
                        Console.WriteLine("Add a description");
                        string description = Console.ReadLine() ?? String.Empty;
                        Task_ task = new Task_(count++, description);
                        taskContainer.Add(task);
                        Console.WriteLine($"#Added Task {task.ID}");
                        break;

                case "update":
                    Console.WriteLine("Which task to update?(put ID)");
                    if(int.TryParse(Console.ReadLine(), out int updateID))
                    {
                        Console.WriteLine("What new description?");
                        string newDescription = Console.ReadLine() ?? String.Empty;
                        taskContainer.Update(updateID, newDescription);
                        Console.WriteLine($"#Updated Task {updateID}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid ID format");
                    }
                    break;

                case "delete":
                    Console.WriteLine("Which task to delete?(put ID)");
                    if (int.TryParse(Console.ReadLine(), out int deleteID))
                    {
                        taskContainer.Delete(deleteID);
                        Console.WriteLine($"#Deleted Task {deleteID}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid ID format");
                    }
                    break;
                case "delete all":
                    Console.WriteLine("Are you sure you want to delete all tasks?");
                    string yesNo = Console.ReadLine()?.ToLower() ?? String.Empty;
                    if(yesNo.Equals("yes"))
                    {
                        taskContainer.DeleteAll();
                        Console.WriteLine($"#Deleted all");
                    }
                    break;
                case "mark in progress":
                    Console.WriteLine("Which task do you want to mark-in-progress?(put ID)");
                    if(int.TryParse(Console.ReadLine(), out int MIPID))
                    {
                        taskContainer.MarkInProgress(MIPID);
                        Console.WriteLine($"#Marked in progress task {MIPID}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid ID format");
                    }
                    break;

                case "mark done":

                    Console.WriteLine("Which task do you want to mark as done? (put ID)");
                    if (int.TryParse(Console.ReadLine(), out int MDPID))
                    {
                        taskContainer.MarkDone(MDPID);
                        Console.WriteLine($"#Marked done task {MDPID}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid ID format");
                    }
                    break;

                case "list":
                    Console.WriteLine("Which list do you want?(ALL, TODO, IN PROGRESS, DONE)");
                    string choiceList = Console.ReadLine()?.ToLower() ?? string.Empty;
                    switch(choiceList)
                    {
                        case "all":
                            taskContainer.TaskList();
                            break;

                        case "todo":
                            taskContainer.TaskListByStatus(choiceList);
                            break;

                        case "in progress":
                            taskContainer.TaskListByStatus(choiceList);
                            break;

                        case "done":
                            taskContainer.TaskListByStatus(choiceList);
                            break;

                        default:
                            Console.WriteLine("Invalid status");
                            break;
                    }
                    break;

                case "help":
                    Help();
                    break;

                default:
                    Console.WriteLine("Invalid status");
                    break;
            }
        }
        SaveTasks(fileName, taskContainer);
    }
    public static void Help()
    {
        Console.WriteLine();
        Console.WriteLine("All commands:");
        Console.WriteLine("1. add");
        Console.WriteLine("2. update");
        Console.WriteLine("3. delete");
        Console.WriteLine("4. mark in progress");
        Console.WriteLine("5. mark done");
        Console.WriteLine("6. list");
        Console.WriteLine("7. help");
        Console.WriteLine();
    }
    public static void SaveTasks(string fileName, TaskContainer taskContainer)
    {
        try
        {
            string jsonString = JsonSerializer.Serialize(taskContainer.GetTasks(), new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(fileName, jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving tasks to file: {ex.Message}");
        }
    }
}
