using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class PlayerSequenceBuilder : MonoBehaviour, IPickUpInteractions
{
    public enum Direction { Front, Back, Left, Right }
    private bool hasCollided = false;
    [SerializeField]
    GameObject playerVehicle;
    [SerializeField]
    ParticleSystem destroyedFX;
    bool isTeleporting = false;

    [Header("Movement Settings")]
    public float moveDistance = 1f;
    public float moveSpeed = 2f;
    public float delayBetweenSteps = 0.5f;
    public int maxSteps = 10;

    [Header("Sequence")]
    public List<Direction> correctSequence = new List<Direction>() {
        Direction.Left,
        Direction.Right,
        Direction.Front,
        Direction.Front
    };

    private List<Direction> customSequence = new List<Direction>();

    [Header("UI Elements")]
    public TMP_Text sequenceDisplay;
    public TMP_Text messageDisplay;
    public Button startButton;
    public Button undoButton;
    public Button clearButton;

    private Vector3 initialPosition;
    private Coroutine sequenceRoutine;

    void Start()
    {
        initialPosition = transform.position;
        UpdateSequenceDisplay();

        startButton.onClick.AddListener(PlayCustomSequence);
        undoButton.onClick.AddListener(UndoLastStep);
        clearButton.onClick.AddListener(ClearSequence);
        messageDisplay.text = "";
    }

    public void AddStep(string dir)
    {
        if (customSequence.Count >= maxSteps)
        {
            messageDisplay.text = "Max steps reached!";
            return;
        }

        Direction d = (Direction)System.Enum.Parse(typeof(Direction), dir);
        customSequence.Add(d);
        UpdateSequenceDisplay();
        messageDisplay.text = "";
    }

    void UpdateSequenceDisplay()
    {
        sequenceDisplay.text = "Your Sequence: \n";
        for (int i = 0; i < customSequence.Count; i++)
        {
            sequenceDisplay.text += $"{i + 1}. {customSequence[i]}\n";
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            hasCollided = true;
            playerVehicle.SetActive(false);
            destroyedFX.Play();
        }
    }

    public void UndoLastStep()
    {
        if (customSequence.Count > 0)
        {
            customSequence.RemoveAt(customSequence.Count - 1);
            UpdateSequenceDisplay();
            messageDisplay.text = "";
        }
    }

    public void ClearSequence()
    {
        customSequence.Clear();
        UpdateSequenceDisplay();
        messageDisplay.text = "";
    }

    void PlayCustomSequence()
    {
        

        if (customSequence.Count == 0)
        {
            messageDisplay.text = "Add steps first!";
            return;
        }

        if (sequenceRoutine != null)
            StopCoroutine(sequenceRoutine);

        transform.position = initialPosition;
        sequenceRoutine = StartCoroutine(PlayAndCheck());
    }

    IEnumerator PlayAndCheck()
    {
        messageDisplay.text = "Playing sequence...";
        hasCollided = false;

        yield return new WaitForSeconds(0.5f);

        foreach (Direction step in customSequence)
        {
            if (isTeleporting)
            {
                yield return new WaitForSeconds(0.1f);
                isTeleporting = false;
            }
                Debug.Log(step);
            yield return MoveInDirection(step);
            yield return new WaitForSeconds(delayBetweenSteps);

            if (hasCollided)
            {
                messageDisplay.text = "Hit an obstacle! Try Again!";
                yield return new WaitForSeconds(1.5f);
                playerVehicle.SetActive(true);
                destroyedFX.Stop();
                transform.position = initialPosition;
                ClearSequence();
                yield break;
            }
        }

        yield return new WaitForSeconds(0.5f);

        if (IsSequenceCorrect())
        {
            messageDisplay.text = "You Win!";
        }
        else
        {
            messageDisplay.text = "Try Again!";
            yield return new WaitForSeconds(1f);
            transform.position = initialPosition;
            ClearSequence();
        }
    }

    bool IsSequenceCorrect()
    {
        //if (customSequence.Count != correctSequence.Count)
        //    return false;

        //for (int i = 0; i < correctSequence.Count; i++)
        //{
        //    if (customSequence[i] != correctSequence[i])
        //        return false;
        //}

        return true;
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
            if (isTeleporting)
            {
                yield break;
            }
            yield return null;
        }

        transform.position = target;
    }

    void IPickUpInteractions.OnObjectPicked()
    {
        isTeleporting = true;
    }
}
