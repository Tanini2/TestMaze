using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace TestMaze
{
    class CreerLabyrinthe
    {
        Grid grid;
        public CreerLabyrinthe()
        {
            ForegroundColor = ConsoleColor.Green;
            Menu1();
            Menu2();
            AfficherLabyrinthe();
        }

        private void Menu1()
        {
            string choix = "";
            Clear();
            WriteLine("****BIENVENUE AU LABYRINTHE!****");
            WriteLine("Choisissez un labyrinthe :");
            WriteLine("1 - Petit 7x7");
            WriteLine("2 - Moyen 10x10");
            WriteLine("3 - Grand 13x13");
            Write("Votre choix : ");
            choix = ReadLine();
            InitialiserLabyrinthe(choix);
            
        }

        private void Menu2()
        {
            string choix = "";
            Clear();
            WriteLine("Choisissez le type de labyrinthe désiré :");
            WriteLine("1 - Binary Tree");
            WriteLine("2 - SideWinder");
            Write("Votre choix : ");
            choix = ReadLine();
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
                    BinaryTree.Maze(grid);
                    break;
                case "2":
                    SideWinder.Maze(grid);
                    break;
                default:
                    Write("Choix incorrect!");
                    Menu2();
                    break;
            }
        }

        private void AfficherLabyrinthe()
        {
            string menu = "";
            Clear();
            WriteLine(grid.ToString());
            Jouer();
            do
            {
                Write("Pour retourner au menu, appuyez sur 'M'");
                menu = ReadLine().ToLower();
            } while (menu != "m");
            CreerLabyrinthe lab = new CreerLabyrinthe();
        }

        private void Jouer()
        {
            WriteLine("Utilisez les flèches pour vous déplacer.");
            WriteLine("Vous devez vous rendre dans le coin inférieur droit du labyrinthe pour gagner.");
            WriteLine("Bonne chance!");
            const char toWrite = '*'; // Character to write on-screen.

            int x = 2, y = 1; // Contains current cursor position.

            ToWrite(toWrite, x, y); // Write the character on the default location (0,0).

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var command = Console.ReadKey().Key;

                    switch (command)
                    {
                        case ConsoleKey.DownArrow:
                            y += 2;
                            break;
                        case ConsoleKey.UpArrow:
                            if (y > 0)
                            {
                                y -= 2;
                            }
                            break;
                        case ConsoleKey.LeftArrow:
                            if (x > 0)
                            {
                                x -= 4;
                            }
                            break;
                        case ConsoleKey.RightArrow:
                            x += 4;
                            break;
                    }

                    ToWrite(toWrite, x, y);
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }

        public static void ToWrite(char toWrite, int x = 0, int y = 0)
        {
            try
            {
                if (x >= 0 && y >= 0) // 0-based
                {
                    SetCursorPosition(x, y);
                    Write(toWrite);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
