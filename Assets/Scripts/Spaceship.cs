using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 1f;
    private Rigidbody2D m_Body;
    
    private void Start()
    {
        m_Body = transform.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var left = Input.GetKey(KeyCode.A);
        var right = Input.GetKey(KeyCode.D);
        var horizontal = 0;
        if (left && !right)
            horizontal = -1;
        if (right && !left)
            horizontal = 1;
        
        var up = Input.GetKey(KeyCode.W);
        var down = Input.GetKey(KeyCode.S);
        var vertical = 0;
        if (up && !down)
            vertical = 1;
        if (down && !up)
            vertical = -1;
        
        m_Body.velocity = new Vector2(horizontal, vertical).normalized * m_Speed;
    }
}
