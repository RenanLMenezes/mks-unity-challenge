using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    // l = left, r = right, f = front
    // teclas para atirar;
    [SerializeField] private KeyCode lShootkey, rShootkey, fShootkey;

    // posições onde serão instanciados os tiros
    public Transform[] lShootPoint, rShootPoint;
    public Transform fShootPoint;

    public GameObject bulletPrefab;

    private bool canShoot = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(lShootkey))
        {
            if (canShoot)
            {
                Debug.Log("left");
                StartCoroutine(LSpawnShoot());
            }
        }
        
        if (Input.GetKey(rShootkey))
        {
            if (canShoot)
            {
                Debug.Log("right");
                StartCoroutine(RSpawnShoot());
            }
        }

        if (Input.GetKey(fShootkey))
        {
            if (canShoot)
            {
                Debug.Log("front");
                StartCoroutine(FSpawnShoot());
            }
        }
    }

    // Spawn dos tiros
    IEnumerator LSpawnShoot()
    {
        canShoot = false;
        foreach (var shootPoint in lShootPoint)
        {
            Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        }
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    }
    IEnumerator RSpawnShoot()
    {
        canShoot = false;
        foreach (var shootPoint in rShootPoint)
        {
           Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        }
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    }
    IEnumerator FSpawnShoot()
    {
        canShoot = false;
        Instantiate(bulletPrefab, fShootPoint.position, fShootPoint.rotation);
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    }
}
