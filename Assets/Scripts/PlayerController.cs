using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed = 5.0f;
    private GameObject focalPoint;
    public bool hasPowerUp = false;
    private float powerupStrength = 15.0f;
    public GameObject powerUpI;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocusPoint");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        powerUpI.transform.position = transform.position - new Vector3(0f, 1f, 0f);
    }
    private void OnTriggerEnter(Collider gem)
    {
        if (gem.CompareTag("powerUp"))
        {
            hasPowerUp = true;
            Destroy(gem.gameObject);
            StartCoroutine(PowerUpTime());
            powerUpI.gameObject.SetActive(true);
        }
    }
    IEnumerator PowerUpTime()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        powerUpI.gameObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRigidBody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            Debug.Log("Object collide with" + collision.gameObject.name + "haspowerup" + hasPowerUp);
            enemyRigidBody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }
}
