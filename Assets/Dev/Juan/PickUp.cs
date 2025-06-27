using UnityEngine;

public class PickUp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        IPickUpInteractions pickUp = other.GetComponent<IPickUpInteractions>();
        if (pickUp != null)
        {
            Debug.Log("Cummmmmmer");
        }
    }
}
