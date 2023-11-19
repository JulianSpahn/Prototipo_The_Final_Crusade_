using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FighterStats : MonoBehaviour, IComparable
{


    [SerializeField]
    private GameObject healthFill;

    [SerializeField]
    private GameObject powerFill;

    [Header("Stats")]
    public float health;
    public float power;
    public float melee;
    public float powerRange;
    public float defense;
    public float speed;
    public float experience;

    private float startHealth;
    private float startPower;

    [HideInInspector]
    public int nextActTurn;

    private bool dead = false;

    // Resize health and magic bar
    private Transform healthTransform;
    private Transform powerTransform;

    private Vector2 healthScale;
    private Vector2 powerScale;

    private float xNewHealthScale;
    private float xNewPowerScale;

    private GameObject GameControllerObj;

    void Awake()
    {
        healthTransform = healthFill.GetComponent<RectTransform>();
        healthScale = healthFill.transform.localScale;

        powerTransform = powerFill.GetComponent<RectTransform>();
        powerScale = powerFill.transform.localScale;

        startHealth = health;
        startPower = power;

        GameControllerObj = GameObject.Find("GameControllerObject");
    }

    public void ReceiveDamage(float damage) //funcion que mnajea el daño recibido
    {
        health = health - damage;

        if(health <= 0)
        {
            dead = true;
            gameObject.tag = "Dead";
            Destroy(healthFill);
            Destroy(gameObject);
        } else if (damage > 0)
        {
            xNewHealthScale = healthScale.x * (health / startHealth);
            healthFill.transform.localScale = new Vector2(xNewHealthScale, healthScale.y);
        }
        if(damage > 0)
        {
            GameControllerObj.GetComponent<GameController>().battleText.gameObject.SetActive(true);
            GameControllerObj.GetComponent<GameController>().battleText.text = damage.ToString();
        }
        Invoke("ContinueGame", 2);
    }

    public void updateMagicFill(float cost) //funcion que se encarga de las barras de la habilidad especial
    {
        if(cost > 0)
        {
            power = power - cost;
            xNewPowerScale = powerScale.x * (power / startPower);
            powerFill.transform.localScale = new Vector2(xNewPowerScale, powerScale.y);
        }
    }

    public bool GetDead() //obtenemos si murio o no el enemigo o nuestro personaje
    {
        return dead;
    }

    void ContinueGame() //controlamos que siga el juego
    {
        GameObject.Find("GameControllerObject").GetComponent<GameController>().NextTurn();
    }
    public void CalculateNextTurn(int currentTurn)//funcion para calcular el tiempo entre turno y turno
    {
        nextActTurn = currentTurn + Mathf.CeilToInt(100f / speed);
    }

    public int CompareTo(object otherStats)
    {
        int nex = nextActTurn.CompareTo(((FighterStats)otherStats).nextActTurn);
        return nex;
    }

}


