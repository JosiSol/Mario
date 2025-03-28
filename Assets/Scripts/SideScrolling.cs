using UnityEngine;

public class SideScrolling: MonoBehaviour
{
    private Transform player;
    public float height = 6.6f;
    public float undergroundHeight = -11f;
    public void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    public void LateUpdate()
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = Mathf.Max(player.position.x, cameraPosition.x);
        transform.position = cameraPosition;
    }
    public void setUnderground(bool underground)
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.y = underground ? undergroundHeight : height;
        transform.position = cameraPosition;
    }
}