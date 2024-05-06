using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [Header("stats")]
    public float health;
    public float maxHealth = 100f;
    public float hunger;
    public float maxHunger = 100f;
    public float thirst;
    public float maxThirst = 100f;

    [Header("Stats Depletion")]

    public float hungerDepletion = 0.5f;
    public float thirstDepletion = 0.75f;

    [Header("Stats Damages")]
    public float hungerDamage = 1.5f;
    public float thirstDamge = 2.25f;

    [Header("UI")]
    public StatsBar healthBar;
    public StatsBar hungerBar;
    public StatsBar thirstBar;
   
    private void Start()
    {
        health = maxHealth;
        hunger = maxHunger;
        thirst = maxThirst;
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateStats();
        UpdateUI();
    }

    private void UpdateUI()
    {
        healthBar.numberText.text = health.ToString("f0");
        healthBar.bar.fillAmount = health / 100;

        hungerBar.numberText.text = hunger.ToString("f0");
        hungerBar.bar.fillAmount = hunger / 100;

        thirstBar.numberText.text = thirst.ToString("f0");
        thirstBar.bar.fillAmount = thirst / 100;
    }

    private void UpdateStats()
    {
        if (health <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(sceneToLoad);
            health = 0;
        }
        if (health >= maxHealth)
            health = maxHealth;

        if (hunger <= 0)
            hunger = 0;
        if (hunger >= maxHunger)
            hunger = maxHunger;

        if (thirst <= 0)
            thirst = 0;
        if (thirst >= maxThirst) 
            thirst = maxThirst;

        if(hunger <= 0)
            health -= hungerDamage * Time.deltaTime;

        if(thirst <= 0)
            health -= thirstDamge * Time.deltaTime;

        if (hunger > 0)
            hunger -= hungerDepletion * Time.deltaTime;

        if (thirst > 0)
            thirst -= thirstDepletion * Time.deltaTime;

    }
}
