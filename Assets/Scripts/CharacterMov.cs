﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterMov : MonoBehaviour
{
    Rigidbody2D rb;
    public Animator animator;
    public Text SaveInfo;

    public string currLevel;
    public static int currHealth = 3;
    public int maxHealth = 3;

    public float invLenght;
    private float invCounter;

    public float speed;

    private void Start()
    {
        Scene lygis = SceneManager.GetActiveScene();
        currLevel = lygis.name;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.white;

        if (invCounter > 0)
        {
            invCounter -= Time.deltaTime;
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }

        if(currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }

        if(currHealth <= 0)
        {
            die();
        }

        rb.velocity = (new Vector2(0, 0));
        animator.SetFloat("Speed", 0);
        animator.SetFloat("SpeedK", 0);

        if (Input.GetKey("x"))
        {
            Save();
        }

        if (Input.GetKey("c"))
        {
            Load();
        }
        

        if (Input.GetKey("d"))
        {
            rb.velocity = (new Vector2(speed, rb.velocity.y));
            animator.SetFloat("Speed", 1);
        }

        if (Input.GetKey("a"))
        {
            rb.velocity = (new Vector2(-speed, rb.velocity.y));
            animator.SetFloat("SpeedK", 1);
        }

        if (Input.GetKey("w"))
        {
            rb.velocity = (new Vector2(rb.velocity.y, speed));
            animator.SetFloat("Speed", 1);
        }

        if (Input.GetKey("s"))
        {
            rb.velocity = (new Vector2(rb.velocity.y, -speed));
            animator.SetFloat("SpeedK", 1);
        }

        if (Input.GetKey("d") && Input.GetKey("w"))
        {
            rb.velocity = (new Vector2(speed, speed));
            animator.SetFloat("Speed", 1);
        }

        if (Input.GetKey("a") && Input.GetKey("w"))
        {
            rb.velocity = (new Vector2(-speed, speed));
            animator.SetFloat("SpeedK", 1);
        }

        if (Input.GetKey("d") && Input.GetKey("s"))
        {
            rb.velocity = (new Vector2(speed, -speed));
            animator.SetFloat("Speed", 1);
        }

        if (Input.GetKey("a") && Input.GetKey("s"))
        {
            rb.velocity = (new Vector2(-speed, -speed));
            animator.SetFloat("SpeedK", 1);
        }

    }

    void die()
    {
        SceneManager.LoadScene("Dead");
        currHealth = maxHealth;
    }

    public int getHealth()
    {
        return currHealth;
    }

    public void gainHP(int ammount)
    {
        currHealth = currHealth + ammount;
    }
      
    public void damage(int dmg)
    {
        if (invCounter <= 0)
        {
            currHealth -= dmg;
            invCounter = invLenght;
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
            GameVariables.haveKey = false;
        }
    }

    public void Save()
    {
        SaveSystem.SavePlayer(this);
        SaveInfo.text = ("Game Saved!");
        StartCoroutine(wait());
    }

    public void Load()
    {
        PlayerData data = SaveSystem.LoadPLayer();

        if (data != null)
        {
            SceneManager.LoadScene(data.level);
            currHealth = data.health;
        }
        else
        {
            SaveInfo.text = ("No previous save was found");
            StartCoroutine(wait());
        }

    }

    IEnumerator wait()
    {
        yield return new WaitForSecondsRealtime(2);
        SaveInfo.text = ("");
    }
    public void bossDamage(int dmg)
    {
        if (invCounter <= 0)
        {
            currHealth -= dmg;
            invCounter = invLenght;
        }
    }

}
