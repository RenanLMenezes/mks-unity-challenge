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
    HUDManager hud;

    private NavMeshAgent agent;

    [SerializeField]
    private float speedRotation;

    // Start is called before the first frame update
    void Start()
    {
        hpSystem = GetComponent<HPSystem>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDManager>();
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
                hud.score += Random.Range(5, 10);
                Invoke("Death", 1f);

            }
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
