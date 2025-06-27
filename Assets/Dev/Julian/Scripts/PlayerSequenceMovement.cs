using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerSequenceMovement : MonoBehaviour
{
    public float moveDistance = 1f;
    public float moveSpeed = 2f;

    private Vector3 targetPosition;
    private bool isMoving = false;

    public enum Direction { Front, Back, Left, Right }

    public List<Direction> correctSequence = new List<Direction>() {
        Direction.Front,
        Direction.Left,
        Direction.Front,
        Direction.Right,
        Direction.Front
    };

    private int currentIndex = 0;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;
            }
        }
    }

    public void OnButtonPressed(string dir)
    {
        if (isMoving) return;

        Direction pressedDirection = (Direction)System.Enum.Parse(typeof(Direction), dir);

        if (pressedDirection == correctSequence[currentIndex])
        {
            MoveInDirection(pressedDirection);
            currentIndex++;

            if (currentIndex >= correctSequence.Count)
            {
                Debug.Log("🎉 You Win!");
            }
        }
        else
        {
            Debug.Log("❌ Wrong move. Sequence reset.");
            currentIndex = 0;
        }
    }

    void MoveInDirection(Direction dir)
    {
        Vector3 move = Vector3.zero;

        switch (dir)
        {
            case Direction.Front: move = Vector3.forward; break;
            case Direction.Back: move = Vector3.back; break;
            case Direction.Left: move = Vector3.left; break;
            case Direction.Right: move = Vector3.right; break;
        }

        targetPosition += move * moveDistance;
        isMoving = true;
    }
}
