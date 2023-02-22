using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaserController : MonoBehaviour
{
    Transform target;

    public GameObject explosionPrefab;

    [SerializeField]
    private float maxDistance = 10f;
    private float currentDistance;
    HPSystem hpSystem;
    ScoreManager scoreManager;

    private NavMeshAgent agent;

    [SerializeField] LayerMask groundLayer;

    //Patrol
    Vector3 destPoint;
    bool walkPointSet;
    [SerializeField] float walkRange;

    [SerializeField]
    private float speedRotation;

    // Start is called before the first frame update
    void Start()
    {
        hpSystem = GetComponent<HPSystem>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckHP();

        if (IsInvoking("Death"))
            return;

        currentDistance = Vector3.Distance(transform.position, target.transform.position);

        if (currentDistance <= maxDistance)
        {
            agent.SetDestination(target.position);
            Rotate(target.position - transform.position);
        }
        else
        {
            Patrol();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && hpSystem.hp > 0)
        {
            HPSystem playerHp = collision.gameObject.GetComponent<HPSystem>();
            playerHp.hp -= 10;
            Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }

    void Patrol()
    {
        if (!walkPointSet)
        {
            SearchForDest();
        }
        if (walkPointSet)
        {
            agent.SetDestination(destPoint);
            Rotate(destPoint - transform.position);
        }
        if (Vector3.Distance(transform.position, destPoint) < 10f)
        {
            walkPointSet = false;
        }
    }

    void SearchForDest()
    {
        float y = Random.Range(-walkRange, walkRange);
        float x = Random.Range(-walkRange, walkRange);

        destPoint = new Vector3(transform.position.x + x, transform.position.y + y);

        if (Physics2D.Raycast(destPoint, Vector2.down, groundLayer))
        {
            walkPointSet = true;
        }
    }

    void Rotate(Vector2 dir)
    {
        if (dir != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, dir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, speedRotation * Time.deltaTime);

        }
    }
    void CheckHP()
    {
        if (hpSystem.hp <= 0)
        {
            agent.isStopped = true;
            Instantiate(explosionPrefab, transform.position, transform.rotation);

            if (!IsInvoking("Death"))
            {
                scoreManager.score += Random.Range(5, 10);
                Invoke("Death", 1f);

            }
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
