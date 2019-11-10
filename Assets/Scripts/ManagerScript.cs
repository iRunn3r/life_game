using UnityEngine;
using Game;

public class ManagerScript : MonoBehaviour
{
    private Board board;
    private void Start()
    {
        board = new Board(10, 10);
        board.SetCell(3, 4, true);
        board.SetCell(4, 4, true);
        board.SetCell(5, 4, true);
        board.Print();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            board.Update();
            board.Print();
        }
    }
}
