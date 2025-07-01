using UnityEngine;

public class PickUp_Teleporter : PickUp
{
    [SerializeField]
    Transform TeleportGoal;

    public override void ActivatePickUp(GameObject objectToTeleport)
    {
        objectToTeleport.transform.position = TeleportGoal.transform.position;
    }
}
