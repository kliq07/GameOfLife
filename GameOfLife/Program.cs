using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife
{
    class Program
    {
        public IBoard _board = new Board();
        static void Main(string[] args)
        {
            new Program();
        }

        public Program()
        {
            Console.OutputEncoding = Encoding.Unicode;

            Console.Write("How many rows in your board: ");
            int r = Convert.ToInt32(Console.ReadLine());
            Console.Write("How many columns in your board: ");
            int c = Convert.ToInt32(Console.ReadLine());


            Console.Write("How many iterations: ");
            int tot_iterations = Convert.ToInt32(Console.ReadLine());

            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            Console.Clear();
            Console.WriteLine();

            int[,] board = _board.initialiseBoard(r, c);
            _board.displayBoard(r, c, board);

            Console.WriteLine("Press Enter to get Next Generations...");
            Console.ReadKey();

            board=_board.getNextGenerations(r, c, tot_iterations, board);

            Console.WriteLine("Done! Press any key to exit");
            Console.ReadLine();
        }                     
    }

    public interface IBoard
    {
        int[,] initialiseBoard(int row, int column);

        int[,] getNextGenerations(int row, int column, int totalIterations, int[,] board);

        void displayBoard(int row, int column, int[,] board);
    }

    public class Board : IBoard
    {
        public Board()
        {

        }
        public int[,] getNextGenerations(int r, int c, int tot_iterations, int[,] board)
        {
            for (int itrns = 0; itrns < tot_iterations; itrns++)
            {
                string gen = "Generation " + Convert.ToInt32(itrns + 1);
                Console.WriteLine();

                board = getNextBoardState(board, r, c, gen);

                for (int i = 0; i < r; i++)
                {
                    for (int j = 0; j < c; j++)
                    {
                        if (board[i, j] == 0)
                        {
                            Console.Write("* ");
                        }
                        else
                        {
                            Console.Write("☻ ");
                        }

                    }
                    Console.WriteLine();
                }

                if (itrns < tot_iterations - 1)
                {
                    Console.WriteLine("Press Enter for next Generation...");
                    Console.ReadKey();
                }
            }
            return board;
        }

        public int[,] initialiseBoard(int r, int c)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Initial Life");

            int[,] board = new int[r, c];
            Random rndm = new Random();

            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    board[i, j] = rndm.Next(0, 2);

                }
            }

            return board;
        }
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        private int[,] getNextBoardState(int[,] brd, int r, int c, string g)
        {
            //Console.Clear();

            int[,] nextBoardState = new int[r, c];

            for (int cr = 1; cr < r - 1; cr++)
            {
                for (int cc = 1; cc < c - 1; cc++)
                {
                    //look for neighbours that are alive
                    int _alive = 0;
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            _alive += brd[cr + i, cc + j];

                        }
                    }
                    //subtract current cell from _alive totals
                    _alive -= brd[cr, cc];

                    //apply rules

                    if ((brd[cr, cc] == 1) && (_alive < 2)) //cell dies due to under-population
                    {
                        nextBoardState[cr, cc] = 0;
                    }
                    else if ((brd[cr, cc]) == 1 && (_alive > 3)) //cell dies due to over-population
                    {
                        nextBoardState[cr, cc] = 0;
                    }
                    else if ((brd[cr, cc] == 0) && (_alive == 3)) //dead cell becomes alive
                    {
                        nextBoardState[cr, cc] = 1;
                    }
                    else if ((brd[cr, cc] == 1) && ((_alive == 2) || (_alive == 3)))
                    {
                        nextBoardState[cr, cc] = 1;
                    }
                    else
                        nextBoardState[cr, cc] = brd[cr, cc]; //all other states remain unchanged
                }

            }
            if (GetConsoleWindow() != IntPtr.Zero)
            {
                Console.SetCursorPosition(0, 0);
                Console.Clear();
                Console.WriteLine(g);
            }        

            return nextBoardState;

        }

        public void displayBoard(int r, int c, int[,] board)
        {
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    if (board[i, j] == 0)
                    {
                        Console.Write("* ");
                    }
                    else
                    {
                        Console.Write("☻ ");
                    }

                }
                Console.WriteLine();

            }
        }
    }
}
