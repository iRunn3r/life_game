using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Game;

namespace Tests
{
    public class BoardTests
    {
        [Test]
        public void Board_OscillatorWorks()
        {
            var board = new Board(10, 10);
            board.SetCell(3, 4, true);
            board.SetCell(4, 4, true);
            board.SetCell(5, 4, true);

            board.UpdateCells();

            Assert.That(board.GetCell(4, 3));
            Assert.That(board.GetCell(4, 4));
            Assert.That(board.GetCell(4, 5));

            board.UpdateCells();
            
            Assert.That(board.GetCell(3, 4));
            Assert.That(board.GetCell(4, 4));
            Assert.That(board.GetCell(5, 4));
        }
    }
}
