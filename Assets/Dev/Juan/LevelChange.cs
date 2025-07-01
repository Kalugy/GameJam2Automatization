using UnityEngine;
using UnityEditor.SceneManagement;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
