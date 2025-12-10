using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitRoom : MonoBehaviour
{
    BoxCollider2D Collider;
    void Awake()
	{
        Collider = GetComponent<BoxCollider2D>();
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		string CurrentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(CurrentScene);
	}
}
