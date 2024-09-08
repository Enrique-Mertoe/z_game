using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int bulletDamage;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            print("hit " + collision.gameObject.name + "!");
            CreteBulletImpact(collision);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            print("hit Wall " + collision.gameObject.name + "!");
            CreteBulletImpact(collision);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Zombie1"))
        {
            collision.gameObject.GetComponent<ZombieScript>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }

    private void CreteBulletImpact(Collision collision)
    {
        
    }
}
