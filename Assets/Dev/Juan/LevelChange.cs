using UnityEngine;
using UnityEditor.SceneManagement;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    [SerializeField]
    bool isLastScene;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(LevelEndSequence());
        }
    }

    IEnumerator LevelEndSequence()
    {
        Vector3 initialScale = transform.localScale;
        Debug.Log(initialScale);
        while (transform.localScale.x < 200)
        {
            transform.localScale += transform.localScale * 5 * Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }
        if (isLastScene)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
