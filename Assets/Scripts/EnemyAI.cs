using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private GameObject playerTarget;
    private Rigidbody rb;
    private CharacterController controller;

    public float moveSpeed = 0.6f;
    public float damage = 5f;

    void Start()
    {
        playerTarget = GameObject.Find("Target");
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 direction = playerTarget.transform.position - transform.position;
        Vector3 nextPosition = transform.position + direction * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(nextPosition);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Target")
        {
            PlayerStats stats = playerTarget.GetComponent<PlayerStats>();
            stats.TakeDamage(damage);
            Debug.Log("Damaged Player!");
        }
    }
}
