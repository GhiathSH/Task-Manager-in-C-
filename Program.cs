
using System;


namespace TaskManager
{
    class Task
    {
         public string Name;                                                                                    //fields
        public int Priority; // 1-High, 2-Medium, 3-Low
        public DateTime Date;

        public Task(string name, int priority, DateTime date)                                                   //constructer 
        {
            Name = name;
            Priority = priority;
            Date = date;
        }

        public void Print()
        {
            Console.WriteLine($"Name: {Name}, Priority: {Priority}, Date: {Date.ToShortDateString()}");     //Console.WriteLine("Name: " + Name + ", Priority: " + Priority + ", Date: " + Date.ToShortDateString());

        }
    }

    class CompletedTaskNode
    {
        public Task Data;
        public CompletedTaskNode Next;          //pointer بتأشر على ال next node

        public CompletedTaskNode(Task task)         //constructer بيشتغل لما ينضاف عندي  completed data
        {
            Data = task;
            Next = null;
        }
    }

    class Program
    {
        static Task[] tasks = new Task[100];
        static int taskCount = 0;

        static CompletedTaskNode completedHead = null;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine(" Task Manager by Ghiath ");
                Console.WriteLine("1. Add Task");
                Console.WriteLine("2. Display Tasks");
                Console.WriteLine("3. Delete Task");
                Console.WriteLine("4. Sort by Priority");
                Console.WriteLine("5. Sort by Date");
                Console.WriteLine("6. Complete Task");
                Console.WriteLine("7. Display Completed Tasks");
                Console.WriteLine("0. Exit");
                Console.Write("Choose: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AddTask(); break;
                    case "2": DisplayTasks(); break;
                    case "3": DeleteTask(); break;
                    case "4": SortByPriority(); break;
                    case "5": SortByDate(); break;
                    case "6": CompleteTask(); break;
                    case "7": DisplayCompleted(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice."); break;
                }
            }
        }

 static void AddTask()                              //(1)
{
     Console.Write("Enter task name: ");
     string name = Console.ReadLine();

    Console.Write("Enter priority (1-High, 2-Med, 3-Low): ");
    int priority = int.Parse(Console.ReadLine());
    
    if (priority < 1 || priority > 3)
    {Console.WriteLine("Priority must be 1, 2, or 3.");
        return;}

     Console.Write("Enter date (yyyy-mm-dd): ");
     DateTime date = DateTime.Parse(Console.ReadLine());

            tasks[taskCount++] = new Task(name, priority, date);
            Console.WriteLine("Task added.");
}

 static void DisplayTasks()                        //(2)
 {
    if (taskCount == 0)
     {
        Console.WriteLine("No tasks.");
             return;
     }


         for (int i = 0; i < taskCount; i++)
            {
                Console.Write($"{i + 1}. ");
                tasks[i].Print();
            }
}

 static void DeleteTask()                             //(3)
{
Console.Write("Enter task number to delete: ");
 int num = int.Parse(Console.ReadLine());

    if (num < 1 || num > taskCount)
        {
            Console.WriteLine("Invalid task number.");
            return;
        }

            for (int i = num - 1; i < taskCount - 1; i++)
            {
                tasks[i] = tasks[i + 1];
            }

    taskCount--;
    Console.WriteLine("Task deleted.");
}

        static void SortByPriority()                    //(4)
        {                                                   //buble sort
            for (int i = 0; i < taskCount - 1; i++)      /* for (int i = 0; i < n - 1; i++)*/  

                                        
                for (int j = 0; j < taskCount - i - 1; j++)    /* for (int j = 0; j < n - i - 1; j++)*/
                    if (tasks[j].Priority > tasks[j + 1].Priority)  /*if (NumArray[j] > NumArray[j + 1])*/
                     {
                       var temp = tasks[j];                         /* var tempVar = NumArray[j]*/
                       tasks[j] = tasks[j + 1];                     /*NumArray[j] = NumArray[j + 1]*/
                       tasks[j + 1] = temp;                         /*NumArray[j + 1] = tempVar*/
                     }
         

            Console.WriteLine("Tasks sorted by priority.");
            DisplayTasks();
        
         }

        static void QuickSort(Task[] arr, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(arr, left, right);
                if (pivot > 1) {
                    QuickSort(arr, left, pivot - 1);}
                if (pivot + 1 < right) {
                    QuickSort(arr, pivot + 1, right);}
            }
        }
                static void SortByDate()
        {
            QuickSort(tasks, 0, taskCount - 1);
            Console.WriteLine("Tasks sorted by date.");
            DisplayTasks();
        }


        static int Partition(Task[] arr, int left, int right) /*https://www.w3resource.com/csharp-exercises/searching-and-sorting-algorithm/searching-and-sorting-algorithm-exercise-9.php*/
        {
        

    // Select the pivot element (using Task.Date)
    DateTime pivot = arr[left].Date;

    while (true)
    {
        // Move left pointer until a value >= pivot is found
        while (arr[left].Date < pivot)
        {
            left++;
        }

        // Move right pointer until a value <= pivot is found
        while (arr[right].Date > pivot)
        {
            right--;
        }

        // If pointers have crossed, return the partition index
        if (left >= right)
        {
            return right;
        }

        // Swap tasks at left and right pointers
        Task temp = arr[left];
        arr[left] = arr[right];
        arr[right] = temp;

        // Move pointers after swapping
        left++;
        right--;
    }
        }
        

        static void CompleteTask()
        {
            Console.Write("Enter task number to complete: ");
            int num = int.Parse(Console.ReadLine());

            if (num < 1 || num > taskCount)
            {
                Console.WriteLine("Invalid task number.");
                return;
            }

            Task finished = tasks[num - 1];

            // remove from array
            for (int i = num - 1; i < taskCount - 1; i++)
            {
                tasks[i] = tasks[i + 1];
            }
            taskCount--;

            // add to linked list
            CompletedTaskNode node = new CompletedTaskNode(finished);
            node.Next = completedHead;
            completedHead = node;

            Console.WriteLine("Task marked as completed.");
        }

        static void DisplayCompleted()
        {
            if (completedHead == null)
            {
                Console.WriteLine("No completed tasks.");
                return;
            }

            CompletedTaskNode current = completedHead;
            int i = 1;
            while (current != null)
            {
                Console.Write($"{i++}. ");
                current.Data.Print();
                current = current.Next;
            }
        }
    }
}





