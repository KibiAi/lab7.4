using lab7._4;
using System.Diagnostics;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        TaskScheduler<string, int> scheduler = new TaskScheduler<string, int>();

        Func<string> initializer = () =>
        {
            Console.WriteLine("Ініціалізую завдання...");
            return "New Object";
        };

        Action<string> reset = (obj) =>
        {
            Console.WriteLine("Сброс завдання...");
        };

        ObjectPool<string> objectPool = new ObjectPool<string>(initializer, reset);

        TaskExecution<string> taskExecutor = (task) =>
        {
            Console.WriteLine($"Виконую завдання {task}");
        };

        Console.WriteLine("Введи завдання (напиши 'done' щоб побачити результат/напиши 'reset' щоб скинути усі завдання):");
        string input;
        while ((input = Console.ReadLine()) != "done")
        {
            if (input.ToLower() == "reset")
            {
                Console.WriteLine("Скидання усіх завдань...");
                scheduler.ResetAllTasks();
            }
            else
            {
                Console.WriteLine("Введіть пріоритет для завдання (ціле число):");
                if (int.TryParse(Console.ReadLine(), out int priority))
                {
                    string task = objectPool.GetObject();
                    task = input;
                    scheduler.AddTask(task, priority);
                   
                }
                else
                {
                    Console.WriteLine("Недійсний пріоритет. Будь ласка, введіть ціле число.");
                }
            }
        }

        // Виконання завдань
        scheduler.ExecuteTasks(taskExecutor);
    }
}
