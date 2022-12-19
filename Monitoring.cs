using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace ProcessMonitor
{
    class Monitoring
    {

        static void Main(string[] args)
        {


            var vm = new Monitoring();

            string processName = "";
            int maxLifeTime;
            int frequency;



            Console.WriteLine("Enter process name");
            processName = Console.ReadLine();

            Console.WriteLine("Enter max process life time");               //Inputs
            maxLifeTime = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter frequency");
            frequency = Convert.ToInt32(Console.ReadLine());


            vm.Monitor(processName, frequency, maxLifeTime);


        }



        public bool IsRunning(string processName)                           //Checks if selected process is running
        {
            Process[] pname = Process.GetProcessesByName(processName);

            if (pname.Length == 0)
                return false;
            else
                return true;
        }



        public void Monitor(string processName, int frequency, int maxLifeTime)
        {

            var vm = new Monitoring();

            do
            {
                while (!Console.KeyAvailable)
                {

                    Process[] proclist = Process.GetProcessesByName(processName);

                    if (vm.IsRunning(processName))
                    {
                        foreach (Process process in proclist)
                        {
                            TimeSpan runningTime = DateTime.Now - process.StartTime;
                            if (runningTime.TotalMinutes >= maxLifeTime)                //Kill process if it runs longer than desired amount of time 
                            {

                                process.Kill();


                                using (StreamWriter sw = new StreamWriter(@"records.log", true))
                                {
                                    sw.WriteLine("{0} {1}", Convert.ToString(process.Id), process.ProcessName);  //Add record to the log

                                }


                            }
                        }

                        TimeSpan freq = TimeSpan.FromMinutes(frequency);

                        Thread.Sleep(freq);
                    }


                }
            } while (Console.ReadKey(true).Key != ConsoleKey.F);       //If f key is pressed, program stops


        }


    }
}
