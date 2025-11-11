using UnityEngine;
using UnityEngine.SceneManagement;
public class WinScript : MonoBehaviour
{
    private BoxCollider2D Collision;
    private SpriteRenderer Sprite;

    public void enable(bool value)
    {
        Sprite.enabled = value;
        Collision.enabled = value;
    }
    void Awake()
    {
        Collision = GetComponent<BoxCollider2D>();
        Sprite = GetComponent<SpriteRenderer>();
        enable(false);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void OnTriggerEnter2D(Collider2D collision)
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }
}
