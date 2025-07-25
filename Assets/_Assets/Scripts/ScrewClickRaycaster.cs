using UnityEngine;

public class ScrewClickRaycaster : MonoBehaviour
{
    public LayerMask screwLayer;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, screwLayer))
            {
                ScrewClickHandler screw = hit.collider.GetComponent<ScrewClickHandler>();
                if (screw != null)
                {
                    screw.OnClickManually();
                }
            }
        }
    }
}
