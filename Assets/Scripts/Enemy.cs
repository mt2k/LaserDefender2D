using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Starts")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 100;

    [Header("Shooting")]
    float shootCounter;
    [SerializeField] float minTimeBetweenShoots = 0.2f;
    [SerializeField] float maxTimeBetweenShoots = 3f;
    [SerializeField] GameObject laserPrefabs;
    [SerializeField] float projectTitleSpeeds = 5f;
    [SerializeField] float speedOfSpinLaser = 720f;

    [Header("VFX")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = .3f;

    [Header("SFX")]
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = .75f; // 3/4 level sound
    [SerializeField] [Range(0, 1)] float shootSoundVolume = .25f;
    [SerializeField] [Range(0, 2)] float firstLaserFired = .2f;
    private void Start()
    {
        shootCounter = (int)Random.Range(minTimeBetweenShoots, maxTimeBetweenShoots);
    }
    private void Update()
    {
        CountDownAndShoots();
        //SpinnerLaserBig(laserPrefabs);
    }
    private void CountDownAndShoots()
    {
        shootCounter -= Time.deltaTime;
        if (shootCounter <= 0)
        {
            Fire();
            //SpinnerLaserBig(laserPrefabs);
            shootCounter = (int)Random.Range(minTimeBetweenShoots, maxTimeBetweenShoots);
        }

    }
    private void Fire()
    {
        // as GameObject as last laser, gameObject.transform.position == transform.position
        GameObject laser = Instantiate(
                laserPrefabs,
                gameObject.transform.position,
                Quaternion.identity
            //Quaternion(0, 0, speedOfSpinLaser * Time.deltaTime)
            //Quaternion.identity
            ); ;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, - projectTitleSpeeds);
        //SpinnerLaserBig(laser);
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
    }

    [Obsolete]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //DamageDealer damageDealer = GetComponent<DamageDealer>();
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    [Obsolete]
    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    [Obsolete]
    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, Quaternion.identity);
        Destroy(explosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }

    private void SpinnerLaserBig(GameObject laser)
    {
        if (laser.name == "Laser Green (Big)")
        {
            Debug.Log("Correct G");
            laser.transform.Rotate(0, 0, speedOfSpinLaser * Time.deltaTime);
        }
        else if (laser.name == "Laser Orange (Fix)")
        {
            Debug.Log("Correct R");
            laser.transform.Rotate(0, 0, speedOfSpinLaser * Time.deltaTime);
        }
    }
}
