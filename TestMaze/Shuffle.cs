using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaze
{
    public static class Shuffle
    {
        private static Random random = new Random();

        public static void ShuffleList<E>(IList<E> list)
        {
            if (list.Count > 1)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    E tmp = list[i];
                    int randomIndex = random.Next(i + 1);

                    //Swap elements
                    list[i] = list[randomIndex];
                    list[randomIndex] = tmp;
                }
            }
        }
    }
}
