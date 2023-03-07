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
    PlayerShooter playerShooter;
    public GameObject explosionPrefab;
    ScoreManager scoreManager;

    public float speedRotation = 300;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hpSystem = GetComponent<HPSystem>();
        playerShooter = GetComponent<PlayerShooter>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
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
        if(Input.GetAxisRaw("Vertical") > 0)
            transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    void Rotate(Vector2 dir)
    {
        if (dir.x != 0 && dir.y > 0)
        {
            transform.Rotate(new Vector3(0f, 0f, - dir.x) * speedRotation * Time.deltaTime);
        }
    }
    
    void CheckHP()
    {
        if (hpSystem.hp <= 0) 
        {
            var explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
            explosion.transform.localScale = new Vector3(3f,3f);
            Invoke("Death", 2f);
            playerShooter.enabled = false;
        }
    }

    void Death()
    {
       GameManager.Instance.GoGameOver(scoreManager.score);
    }
}
