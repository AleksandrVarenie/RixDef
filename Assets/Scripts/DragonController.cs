using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    public GameObject fireballPrefab;
    public float fireballSpeed = 10f;
    public float fireballCooldown = 0.5f;

    public float speedLight = 2f;

    public GameObject LightPrefab;


    private bool canShoot = true;

    private OrbitController controller;

    void Update()
    {
        if (canShoot && Input.GetMouseButton(0))
        {
            ShootFireball();
            StartCoroutine(StartFireballCooldown());
        }
    }

    void ShootFireball()
    {
        Vector2 direction = transform.right;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        GameObject fireball = Instantiate(fireballPrefab, transform.position, rotation);
        fireball.GetComponent<Rigidbody2D>().velocity = direction * fireballSpeed;
        fireball.GetComponent<FireBall>().owner = gameObject;
        GameObject lightHolder = Instantiate(LightPrefab, transform.position, rotation);
        lightHolder.GetComponent<FireBallLight>().projectail = fireball.transform;
        fireball.GetComponent<FireBall>().Light = lightHolder;
        // Destroy(fireball, fireballDuration);
    }
    IEnumerator StartFireballCooldown()
    {
        canShoot = false;
       // transform.GetComponent<OrbitController>().enabled = false;
        yield return new WaitForSeconds(fireballCooldown);
        canShoot = true;
    }
}
