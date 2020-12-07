using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUp : MonoBehaviour
{
    public GameObject pickupEffect;
    public float multiplier = 1.4f;
    public float duration = 4f;
     void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickUp(other);
        }
    }

    void  PickUp(Collider player)
    {
        //Debug.Log("Picked up item!");

        Instantiate(pickupEffect, transform.position, transform.rotation);

        //player.transform.localScale *= multiplier;

        /*PlayerStats stats = player.GetComponent<PlayerStats>();
        stats.health *= multiplier;
        */
        //yield return new WaitForSeconds(duration);
        //stats.health /= multiplier

        Destroy(gameObject);
    }

}
