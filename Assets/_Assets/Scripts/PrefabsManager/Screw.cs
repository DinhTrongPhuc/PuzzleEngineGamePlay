using UnityEngine;

public class Screw : MonoBehaviour
{
    public Color color;

    public void MoveToBox(Vector3 target)
    {
        // animator.SetTrigger("MoveToBox");
        transform.position = target;
    }

    public void MoveToTempSlot(int slotIndex)
    {
        Vector3 tempSlotPosition = GameUIManager.Instance.GetTempSlotPosition(slotIndex);
        transform.position = tempSlotPosition;
    }
}
