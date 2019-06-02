using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TestMaze
{
    //Timer pouvant être utilisé
    //Pas implémenté, mais il a été testé et est fonctionnel
    class Timer
    {
        static int seconds = 0;
        static System.Timers.Timer aTimer;
        public static void StartTimer()
        {
            aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 1000;
            aTimer.Enabled = true;
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            seconds++;
            Console.SetCursorPosition(70, 1);
            Console.CursorVisible = false;
            Console.Write("Temps écoulé (en secondes) : " + seconds.ToString());
        }

        public static void StopTimer()
        {
            seconds = 0;
            aTimer.Close();
        }
    }
}
