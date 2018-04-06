using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public float fireRate = 0;
    public int Damage = 10;
    public LayerMask whatToHit;
    public Transform BulletTrailPrefab;
    float timeToSpawnEffect = 0;
    public float effectSpawnRate = 10;
    float timeToFire = 0;
    public Transform FirePoint;



    //Use this for initialization
    void Awake()
    {
        FirePoint = transform.Find("FirePoint");
        if (FirePoint == null)
        {
            Debug.LogError("No FirePoint? WHAT?!");
        }
    }

    //Update is called once per frame
    void Update()
    {
        if (fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
    }
    void Shoot()
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 FirePointPosition = new Vector2(FirePoint.position.x, FirePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(FirePointPosition, mousePosition - FirePointPosition, 100, whatToHit);
        if (Time.time >= timeToSpawnEffect) {
            Effect();
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
        }
        Debug.DrawLine(FirePointPosition, (mousePosition - FirePointPosition) * 100, Color.black);
        if (hit.collider != null) { 
                Debug.DrawLine(FirePointPosition, hit.point, Color.red);
                
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null) {
                    enemy.DamageEnemy(Damage);
                    //Debug.Log("We hit " + hit.collider.name + " and did " + Damage + " damage. ");
            }
            }
        }

        void Effect() {
        Instantiate(BulletTrailPrefab, FirePoint.position, FirePoint.rotation);

    }
}