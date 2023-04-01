using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    public GameObject fireballPrefab;
    public float fireballSpeed = 10f;
    public float fireballCooldown = 0.5f;
    public float fireballDuration = 2f;
 

    private bool canShoot = true;

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
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        GameObject fireball = Instantiate(fireballPrefab, transform.position, rotation);
        fireball.GetComponent<Rigidbody2D>().velocity = direction * fireballSpeed;
        Destroy(fireball, fireballDuration);
    }
    IEnumerator StartFireballCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireballCooldown);
        canShoot = true;
    }
}
