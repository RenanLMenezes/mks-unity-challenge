using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShooterController : MonoBehaviour
{
    Transform target;

    [SerializeField] Transform shootPoint;

    public GameObject explosionPrefab, bulletPrefab;

    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private float attackDistance = 5f;
    
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

    private bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

        if(currentDistance <= attackDistance)
        {
            if (currentDistance <= attackDistance - 2)
                agent.isStopped = true;
            if (canShoot)
            {
                StartCoroutine(SpawnShoot());
            }
            Rotate(target.position - transform.position);
        }
        else if (currentDistance <= maxDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);
            Rotate(target.position - transform.position);
        }
        else
        {
            Patrol();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }

    void Rotate(Vector2 dir)
    {
        if (dir != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, dir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, speedRotation * Time.deltaTime);

        }
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

        if(Physics2D.Raycast(destPoint, Vector2.down, groundLayer))
        {
            walkPointSet = true;
        }
    }

    void CheckHP()
    {
        if (hpSystem.hp <= 0)
        {
            agent.isStopped = true;
            var explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
            explosion.transform.localScale = new Vector3(3f, 3f);

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

    IEnumerator SpawnShoot()
    {
        canShoot = false;
        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        yield return new WaitForSeconds(1f);
        canShoot = true;
    }
}
