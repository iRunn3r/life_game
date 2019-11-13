using UnityEngine;

public class SelfDisable : MonoBehaviour
{
    [SerializeField]
    private float m_Delay = 1.0f;
    
    private void OnEnable()
    {
        Invoke("Disable", m_Delay);
    }

    private void Disable()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.SetActive(false);
    }
}
