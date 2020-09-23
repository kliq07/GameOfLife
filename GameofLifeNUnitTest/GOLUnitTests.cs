using GameOfLife;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameofLifeNUnitTest
{
    [TestFixture]
    public class GOLUnitTests
    {
        IBoard _board;
        int[,] playBoard;
        int row = 5, column = 5, iteration = 1;
        public GOLUnitTests()
        {
         playBoard = new int[,]{
                { 0,1,1,0,1 },
                { 1,0,0,1,1 },
                { 0,0,0,1,0 },
                { 0,1,0,1,0 },
                { 0,0,1,0,1 }

            };  
        }

        [Test]
        public void LiveWithFewerThan2NeighboursDies()
        {
            _board = new Board();
            int[,] testBoard = playBoard;
            int actual = 0;
            int expected = 0;

            testBoard = _board.getNextGenerations(row, column, iteration, testBoard);
            actual = testBoard[3,1];

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void LiveWith2or3NeighboursLives()
        {
            _board = new Board();
            int[,] testBoard = playBoard;
            int actual = 0;
            int expected = 1;

            testBoard = _board.getNextGenerations(row, column, iteration, testBoard);
            actual = testBoard[3, 3];

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void LiveWithMoreThan3NeighboursDies()
        {
            _board = new Board();
            int[,] testBoard = playBoard;
            int actual = 0;
            int expected = 0;

            testBoard = _board.getNextGenerations(row, column, iteration, testBoard);
            actual = testBoard[1,3];

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeadWithExactly3NeighboursLives()
        {
            _board = new Board();
            int[,] testBoard = playBoard;
            int actual = 0;
            int expected = 1;

            testBoard = _board.getNextGenerations(row, column, iteration, testBoard);
            actual = testBoard[1, 1];

            Assert.AreEqual(expected, actual);
        }
    }
}
