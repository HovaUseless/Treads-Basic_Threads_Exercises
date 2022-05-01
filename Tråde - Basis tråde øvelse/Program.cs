using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("1. Task 1 and 2 - Simple Treading exercise");
        Console.WriteLine("2. Task 3 - Tempeture reader");
        Console.WriteLine("3. Task 4 - Input printer");

        switch (Console.ReadLine())
        {
            case "1":
                SimpleThreadingExercise task1 = new SimpleThreadingExercise();
                task1.RunExercise();
                break;
            case "2":
                TempChecker task3 = new TempChecker();
                task3.Exercise3();
                break;
            case "3":
                InputOutputPrinter inputPrinter = new InputOutputPrinter();
                inputPrinter.Exercise4();
                break;
        }
        Console.Read();
    }
}

#region Task 1 and 2
class SimpleThreadingExercise
{

    public void RunExercise()
    {
            
        Thread thread1 = new Thread(new ThreadStart(EasyThreading));
        thread1.Name = "FirstThread";

        Thread thread2 = new Thread(new ThreadStart(MultipleThreads));
        thread2.Name = "SecondTread";
        thread1.Start();
        thread2.Start();
        Console.Read();
    }

    // Method to display 5 lines in the console
    public void EasyThreading()
    {
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine("C# Threading is darn easy");
            Thread.Sleep(1000);
        }
    }

    // Method to display 5 lines in the console
    public void MultipleThreads()
    {
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine("Even with multiple threads...");
            Thread.Sleep(1000);
        }
    }
}
#endregion


#region Task 3 - Tempature Generator

public class TempChecker
{
    private int currentTemp;
    public int CurrentTemp
    {
        get { return currentTemp; }
        set { currentTemp = value; }
    }

    private bool updated;
    public bool Updated
    {
        get { return updated; }
        set { updated = value; }
    }

    private bool endThread;
    public bool EndThread
    {
        get { return endThread; }
        set { endThread = value; }
    }


    public void Exercise3()
    {
        
        Thread readerThread = new Thread(DisplayTempController);
        readerThread.Name = "Temp reader thread";
        
        Thread checkerThread = new Thread(TempCheckContoller);
        checkerThread.Name = "Temp checker tread";

        readerThread.Start();
        checkerThread.Start();  

        while (checkerThread.IsAlive)
        {
            Thread.Sleep(5000); // Waits 5 seconds between checks
        }
        Console.WriteLine("Alarm thread terminated");
    }
    public void TempCheckContoller()
    {   
        TempAlertChecker tempChecker = new TempAlertChecker(0, 100);
        while (TempAlertChecker.alerts < 4)
        {
            if (updated)
            {
                tempChecker.TempCheck(CurrentTemp);
                updated = false;
            }

        }
    }

    public void DisplayTempController()
    {
        TempReader tempReader = new TempReader();
        while (TempAlertChecker.alerts < 4)
        {
            CurrentTemp = tempReader.DisplayTempValue();
            updated = true;
            Console.WriteLine("{0}: Current tempeture: {1} C", Thread.CurrentThread.Name, CurrentTemp);
            Thread.Sleep(2000);
        }
    }

    // Returns a random tempeture between -20 and 120 C
    private class TempReader
    {
        
        private int temp;

        public int Temp
        {
            get { return temp; }
            set { temp = value; }
        }

        public int DisplayTempValue()
        {
            Random random = new Random();
            return random.Next(-20, 120);
        }
    }

    private class TempAlertChecker
    {
        private int minTemp;
        public int MinTemp
        {
            get { return minTemp; }
            set { minTemp = value; }
        }

        private int maxTemp;
        public int MaxTemp
        {
            get { return maxTemp; }
            set { maxTemp = value; }
        }

        public static int alerts;



        public TempAlertChecker(int min, int max)
        {
            MinTemp = min;
            MaxTemp = max;
            alerts = 0;
        }
        public void TempCheck(int temp)
        {
            if (temp < MinTemp || temp > MaxTemp)
            {
                TempAlarm();
            }
        }

        private void TempAlarm()
        {
            Console.WriteLine("{0}: Tempeture alert!", Thread.CurrentThread.Name);
            alerts++;
        }

    }
}

#endregion

#region Task 4 - Input/Output threads

public class InputOutputPrinter
{
    private char input = '*';
    public char Input
    {
        get { return input; }
        set { input = value; }
    }

    public void Exercise4()
    {
        Thread printerThread = new Thread(Print);
        printerThread.Name = "Printer Thread";

        Thread listenerThread = new Thread(InputListener);
        listenerThread.Name = "Listener Thread";

        printerThread.Start();
        listenerThread.Start();

        while(true)
        {

        }
        Console.Read();
    }

    public void InputListener()
    {
            char inputBeforeEnter = '\0'; // Using \0 as empty char essentially null.
        while(true)
        {
            ConsoleKeyInfo keyinfo = Console.ReadKey(true);
            if (keyinfo.Key != ConsoleKey.Enter)
            {
                inputBeforeEnter = keyinfo.KeyChar;
            }
            else if (inputBeforeEnter != '\0' && keyinfo.Key == ConsoleKey.Enter)
            {
                Input = inputBeforeEnter;
            }

        }
    }

    public void Print()
    {
        while(true)
        {
            Console.Write(Input);
            Thread.Sleep(200);
        }
    }

}

#endregion