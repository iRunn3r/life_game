using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 1f;
    [SerializeField]
    private Vector2 m_BulletSpawnLocation = Vector2.zero;
    [SerializeField]
    private float m_BulletSpeed = 1f;
    [SerializeField]
    private int m_ShotsPerSecond = 10;
    private Rigidbody2D m_Body;
    private GameObject m_BulletPrefab;
    private GameObject[] m_Bullets;
    
    private void Start()
    {
        m_ShotsPerSecond = Application.targetFrameRate / m_ShotsPerSecond;
        m_Body = transform.GetComponent<Rigidbody2D>();
        m_BulletPrefab = Resources.Load("Bullet") as GameObject;
        m_Bullets = new GameObject[50];
        for (var i = 0; i < 50; i++)
        {
            m_Bullets[i] = Instantiate(m_BulletPrefab, m_BulletSpawnLocation, Quaternion.identity);
            m_Bullets[i].SetActive(false);
        }
    }

    private void Update()
    {
        HandleMovement();

        if (Input.GetKey(KeyCode.Mouse0) && Time.frameCount % m_ShotsPerSecond == 0)
        {
            ShootSimple();
        }
    }

    private void ShootSimple()
    {
        for (var i = 0; i < m_Bullets.Length; i++)
        {
            if (m_Bullets[i].activeSelf)
                continue;

            var spawnPosition = transform.position + (transform.rotation * m_BulletSpawnLocation);
            m_Bullets[i].transform.position = spawnPosition;
            m_Bullets[i].transform.rotation = transform.rotation;
            m_Bullets[i].SetActive(true);
            m_Bullets[i].GetComponent<Rigidbody2D>().velocity = (spawnPosition - transform.position).normalized * m_BulletSpeed;
            return;
        }
    }

    private void HandleMovement()
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
        Vector3 lookDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg -  90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
