using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 200;
    //Vector3 paddingPlayerShoot = new Vector3 (0, -1f, 0);

    [SerializeField] GameObject laserPrefabs;
    //[SerializeField][Range(0, 10f)] float projectTitleSpeed;
    [SerializeField] float projectTitleSpeed = 10f;
    [SerializeField] float firingTimeOfBullets = 0.15f;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = .3f;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip shootSFX;
    [SerializeField][Range(0, 1)] float deathSoundVolume = .75f;
    [SerializeField][Range(0, 1)] float shootSoundVolume = .25f;

    float xMin;
    float yMin;
    float xMax;
    float yMax;

    Coroutine firingCoroutine;

    void Start()
    {
        SetUpBoundaries();
    }

    private void SetUpBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1")) {
            firingCoroutine = StartCoroutine(FireCoutinously());
        }
        if (Input.GetButtonUp("Fire1")) {
            //StopCoroutine(FireCoutinously()); ==> cannot stop fire when don't hold down the button
            //StopAllCoroutines(); ==> don't use to much because when we use StopAllCoroutine will be stop all Coroutine running
            StopCoroutine(firingCoroutine);
        }
        //AudioSource.PlayClipAtPoint(shootSFX, transform.position);
    }

    IEnumerator FireCoutinously() {
        while (true)
        {
            GameObject laserObject = Instantiate(
                laserPrefabs,
                //transform.position + paddingPlayerShoot,
                transform.position,
                Quaternion.identity) as GameObject;
            laserObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectTitleSpeed);
            AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSoundVolume);
            yield return new WaitForSeconds(firingTimeOfBullets);
        }
    }

    private void Move()
    {
        //Move Player using "Input Setting"
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var newXPos = Math.Clamp(transform.position.x + deltaX, xMin, xMax);
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newPosY = Math.Clamp(transform.position.y + deltaY, yMin, yMax);
        //Update Position of PLayer
        transform.position = new Vector2(newXPos, newPosY);
        //transform.position = new Vector2(newXPos, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

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
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSoundVolume);
        FindObjectOfType<Level>().LoadGameOver();
        //SceneManager.LoadScene("Game Over", LoadSceneMode.Additive);
    }

    public int GetHealth()
    {
        return health;
    }
}
