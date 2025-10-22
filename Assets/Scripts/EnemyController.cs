using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Rigidbody enemyRb;

    [SerializeField] private float speed = 0.4f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 direction = target.position - transform.position;
        Vector3 nextPosition = transform.position + direction * speed * Time.fixedDeltaTime;
        enemyRb.MovePosition(nextPosition);
    }
}
