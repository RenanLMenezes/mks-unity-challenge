using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    public GameObject explosionPrefab;

    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    void OnDestroy()
    {
        Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            if(collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<HPSystem>().hp -= damage;
            }
            Destroy(gameObject);
        }
    }
}
