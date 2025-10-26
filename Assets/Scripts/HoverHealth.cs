using UnityEngine;

public class HoverHealth : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    private Transform player;
    private Transform playerCam;

    void Awake()
    {
        playerCam = Camera.main.transform;
        player = GameObject.Find("Target").transform;

        transform.SetParent(player, worldPositionStays: true);
    }

    void LateUpdate()
    {
        transform.position += offset;

        transform.rotation = Quaternion.LookRotation(transform.position - playerCam.transform.position);
    }
}
