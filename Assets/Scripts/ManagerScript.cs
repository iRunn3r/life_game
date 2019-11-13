using UnityEngine;
using Game;
using UnityEditor.SceneManagement;

public class ManagerScript : MonoBehaviour
{
    private Board board;
    
    private void Start()
    {
        Application.targetFrameRate = 60;
        board = gameObject.AddComponent(typeof(Board)) as Board;
        if (board == null)
            return;
        Camera.main.transform.position = new Vector3(10, 10 - 0.5f, Camera.main.transform.position.z);
        board.Initialize(20, 20);
        board.SetCell(3, 4, true);
        board.SetCell(4, 4, true);
        board.SetCell(5, 4, true);
        
        board.SetCell(10, 10, true);
        board.SetCell(11, 9, true);
        board.SetCell(11, 8, true);
        board.SetCell(12, 9, true);
        board.SetCell(12, 10, true);
        InvokeRepeating("UpdateBoard", 0.5f, 0.5f);
    }

    private void UpdateBoard()
    {
        board.UpdateCells();
    }
}
