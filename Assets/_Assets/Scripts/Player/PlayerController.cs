using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    Rigidbody2D rb;
    
    HPSystem hpSystem;
    public GameObject explosionPrefab;

    public float speedRotation = 300;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hpSystem = GetComponent<HPSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInvoking("Death"))
            return;

        var dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        Move(dir);
        Rotate(dir);
        CheckHP();
    }

    void Move(Vector2 dir)
    {
        dir.Normalize();

        rb.velocity = dir * speed;
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
            var explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
            explosion.transform.localScale = new Vector3(5f,5f);
            Invoke("Death", 1f);
        }
    }

    void Death()
    {
        SceneManager.LoadScene(2);
    }
}
