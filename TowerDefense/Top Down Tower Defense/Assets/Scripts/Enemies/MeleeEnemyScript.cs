﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeEnemyScript : MonoBehaviour {

    private Vector2 origin;

    public float maxHealth;
    public float health;
    public float speed;
    public float damage;

    public AudioClip minecraft;
    public AudioClip death;

    Image healthBar;

    // Use this for initialization
    void Start () {
        origin = new Vector2(0, 0);

        GameObject healthBarFill = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        healthBar = healthBarFill.GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector2.MoveTowards(transform.position, origin, speed * Time.deltaTime);

        healthBar.fillAmount = health / maxHealth;

        if (health <= 0) {
            Destroy(gameObject);
            GameObject.Find("Player 1").GetComponent<PlayerScript>().gold += 5;
            GameObject.Find("GameSoundController").GetComponent<AudioSource>().PlayOneShot(death, .6f);

            if (GameObject.Find("Player 1").tag == "RangedPlayer") {
                GameObject.Find("Player 1").GetComponent<PlayerCombat>().rapidShootCharge += 5;
            } else if (GameObject.Find("Player 1").tag == "MeleePlayer") {
                GameObject.Find("Sword").GetComponent<SwordScript>().ultimateCharge += 5;
            }
        }
	}


     void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Base") {
            Destroy(gameObject);
            GameObject.Find("Base").GetComponent<BaseScript>().health -= damage;
        } else if (collision.gameObject.name  == "Player 1") {
            Destroy(gameObject);
            GameObject.Find("Player 1").GetComponent<PlayerScript>().health -= damage;
            GameObject.Find("GameSoundController").GetComponent<AudioSource>().PlayOneShot(minecraft, .5f);
        }
     }
}
