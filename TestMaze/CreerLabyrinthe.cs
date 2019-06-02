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
            SetWindowSize(100, 48);
            Menu1();
            Menu2();
            AfficherLabyrinthe();
            
        }
        //Choisir la grosseur du labyrinthe
        private void Menu1()
        {
            string choix = "";
            Clear();
            WriteLine("****BIENVENUE AU LABYRINTHE!****");
            WriteLine("Choisissez un labyrinthe :");
            WriteLine("1 - Petit 10x10");
            WriteLine("2 - Moyen 15x15");
            WriteLine("3 - Grand 20x20");
            Write("Votre choix : ");
            choix = ReadLine();
            InitialiserLabyrinthe(choix);
            
        }
        //Choisir le type de labyrinthe
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
        //Initialise la grille 
        private void InitialiserLabyrinthe(string choix)
        {
            switch (choix)
            {
                case "1":
                    grid = new Grid(10, 10);
                    break;
                case "2":
                    grid = new Grid(15, 15);
                    break;
                case "3":
                    grid = new Grid(20, 20);
                    break;
                default:
                    Write("Choix incorrect!");
                    Menu1();
                    break;
            }
        }
        //Choisi le bon algorithme 
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
        //Affiche le labyrinthe
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
        //Permet de se déplacer tant que le joueur n'a pas atteint la fin du labyrinthe
        private void Jouer()
        {
            WriteLine("Utilisez les flèches pour vous déplacer.");
            WriteLine("Vous devez vous rendre dans le coin inférieur droit du labyrinthe pour gagner.");
            WriteLine("Bonne chance!");
            int endX = CursorLeft;
            int endY = CursorTop;
            const char toWrite = '*'; // Character to write on-screen.

            int x = 2, y = 1; // Contains current cursor position.

            ToWrite(toWrite, x, y); // Write the character on the default location (2,1).

            while (x < grid.MaxX || y < grid.MaxY)
            {
                if (Console.KeyAvailable)
                {
                    char vide = ' ';
                    ToWrite(vide, x, y);
                    var command = Console.ReadKey().Key;

                    switch (command)
                    {
                        case ConsoleKey.DownArrow:
                            if(y + 2 <= grid.MaxY)
                            {
                                if(grid.IsLinked(x, y, "s"))
                                {
                                    y += 2;
                                }
                            }
                            break;
                        case ConsoleKey.UpArrow:
                            if (y > grid.MinY)
                            {
                                if (grid.IsLinked(x, y, "n"))
                                {
                                    y -= 2;
                                }
                            }
                            break;
                        case ConsoleKey.LeftArrow:
                            if (x > grid.MinX)
                            {
                                if (grid.IsLinked(x, y, "w"))
                                {
                                    x -= 4;
                                }
                            }
                            break;
                        case ConsoleKey.RightArrow:
                            if(x + 4 <= grid.MaxX)
                            {
                                if (grid.IsLinked(x, y, "e"))
                                {
                                    x += 4;
                                }
                            }
                            break;
                    }

                    ToWrite(toWrite, x, y);
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
            AffichageFin(endX, endY);
        }
        //Permet d'afficher le caractère du joueur
        public static void ToWrite(char toWrite, int x = 0, int y = 0)
        {
            try
            {
                if (x >= 0 && y >= 0) // 0-based
                {
                    SetCursorPosition(x, y);
                    ForegroundColor = ConsoleColor.Cyan;
                    Write(toWrite);
                }
            }
            catch (Exception)
            {
            }
        }
        //Affiche la fin du jeu lorsque le joueur a gagné
        private void AffichageFin(int endX, int endY)
        {
            ForegroundColor = ConsoleColor.Green;
            string choix = "";
            SetCursorPosition(endX, endY + 1);
            WriteLine("Vous avez gagné!");
            Write("Voulez-vous rejouer (O ou N)?");
            choix = Console.ReadLine().ToUpper();
            if(choix == "O")
            {
                CreerLabyrinthe lab = new CreerLabyrinthe();
            }else if(choix == "N"){
                WriteLine("Merci d'avoir joué!");
                Thread.Sleep(1500);
                Environment.Exit(0);
            }
            else
            {
                WriteLine("Choix incorrect! Bye-bye!");
                Thread.Sleep(1500);
                Environment.Exit(0);
            }
        }
    }
}
