using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace TestMaze
{
    class CreerLabyrinthe
    {
        Grid grid;
        public CreerLabyrinthe()
        {
            Menu1();
            Menu2();
        }

        private void Menu1()
        {
            string choix;
            Console.Clear();
            WriteLine("****BIENVENUE AU LABYRINTHE!****");
            WriteLine("Choisissez un labyrinthe :");
            WriteLine("1 - Petit 7x7");
            WriteLine("2 - Moyen 10x10");
            WriteLine("3 - Grand 13x13");
            Write("Votre choix : ");
            choix = Console.ReadLine();
            InitialiserLabyrinthe(choix);
            
        }

        private void Menu2()
        {
            string choix;
            WriteLine("Choisissez le type de labyrinthe désiré :");
            WriteLine("1 - Binary Tree");
            WriteLine("2 - SideWinder");
            Write("Votre choix : ");
            choix = Console.ReadLine();
            TypeLabyrinthe(choix);
        }

        private void InitialiserLabyrinthe(string choix)
        {
            switch (choix)
            {
                case "1":
                    grid = new Grid(7, 7);
                    break;
                case "2":
                    grid = new Grid(10, 10);
                    break;
                case "3":
                    grid = new Grid(13, 13);
                    break;
                default:
                    Write("Choix incorrect!");
                    Menu1();
                    break;
            }
        }

        private void TypeLabyrinthe(string choix)
        {
            switch (choix)
            {
                case "1":
                    
                    break;
                case "2":

                    break;
                default:
                    Write("Choix incorrect!");
                    Menu2();
                    break;
            }
        }
    }
}
