﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeButton : MonoBehaviour
{
    [SerializeField]
    private bool physical;

    private GameObject hero;
    void Start()
    {
        string temp = gameObject.name;
        gameObject.GetComponent<Button>().onClick.AddListener(() => AttachCallback(temp));
        hero = GameObject.FindGameObjectWithTag("Hero");
    }

    private void AttachCallback(string btn)
    {
        if (btn.CompareTo("Attack") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("melee");
        } else if (btn.CompareTo("Super") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("range");
        } else if (btn.CompareTo("Item")==0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("item");
        }
    }
}
