using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAutoSequence : MonoBehaviour
{
    public enum Direction { Front, Back, Left, Right }

    [Header("Sequence Settings")]
    public List<Direction> sequence = new List<Direction>() {
        Direction.Left,
        Direction.Right,
        Direction.Front,
        Direction.Front
    };

    [Header("Movement Settings")]
    public float moveDistance = 1f;
    public float moveSpeed = 2f;
    public float delayBetweenSteps = 0.5f;

    private Vector3 initialPosition;
    private Coroutine sequenceRoutine;

    void Start()
    {
        initialPosition = transform.position;
        StartSequence();
    }

    public void StartSequence()
    {
        if (sequenceRoutine != null)
            StopCoroutine(sequenceRoutine);

        transform.position = initialPosition;
        sequenceRoutine = StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        foreach (Direction step in sequence)
        {
            yield return MoveInDirection(step);
            yield return new WaitForSeconds(delayBetweenSteps);
        }

        Debug.Log("✅ Finished automatic movement.");
    }

    IEnumerator MoveInDirection(Direction dir)
    {
        Vector3 move = Vector3.zero;

        switch (dir)
        {
            case Direction.Front: move = Vector3.forward; break;
            case Direction.Back: move = Vector3.back; break;
            case Direction.Left: move = Vector3.left; break;
            case Direction.Right: move = Vector3.right; break;
        }

        Vector3 start = transform.position;
        Vector3 target = start + move * moveDistance;

        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = target;
    }
}
