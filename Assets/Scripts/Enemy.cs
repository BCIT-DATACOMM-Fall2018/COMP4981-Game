using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //[HideInInspector]
    public float speed = 20f;
    public float startHealth = 100;
    protected float health;

    public Transform target;

    [Header("Unity Stuff")]
    public Image healthBar;

    private void Start()
    {
        target = WavePoint.waypoint;
        health = startHealth;
    }

    public void TakeDamage(float amount) {
        health -= amount;
        healthBar.fillAmount = health / startHealth;

        if (health <= 0) {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
    }
}
