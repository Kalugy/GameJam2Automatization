using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform[] points; // Array of positions to move through
    public float speed = 5f;
    public float inputSpeed = 10f;

    private int currentTargetIndex = 0;
    private bool autoMoving = true;

    void Start()
    {
        if (points.Length > 0)
        {
            transform.position = points[0].position;
            currentTargetIndex = 1;
        }
    }

    void Update()
    {
        if (autoMoving && currentTargetIndex < points.Length)
        {
            MoveTowardsPoint(points[currentTargetIndex]);

            if (Vector3.Distance(transform.position, points[currentTargetIndex].position) < 0.1f)
            {
                currentTargetIndex++;

                if (currentTargetIndex >= points.Length)
                {
                    autoMoving = false;
                }
            }
        }
        else
        {
            ManualMovement();
        }
    }

    void MoveTowardsPoint(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    void ManualMovement()
    {
        float input = Input.GetAxis("Horizontal");
        transform.position += new Vector3(input, 0, 0) * inputSpeed * Time.deltaTime;
    }
}
