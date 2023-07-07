using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FighterLabel : MonoBehaviour
{
    private Fighter fighter;
    public TextMesh labelText;

    // Start is called before the first frame update
    void Start()
    {
        fighter = gameObject.GetComponent<Fighter>();
    }

    // Update is called once per frame
    void Update()
    {
        string NewLine = System.Environment.NewLine;
        labelText.text = fighter.health.ToString() + "/" + fighter.maxHealth.ToString() + "HP" + NewLine
            + fighter.username + NewLine
            + "Hack = " + fighter.hack;

        float healthBarScale = Mathf.Clamp01(fighter.health / fighter.maxHealth);
        // TODO: Create a health bar. Scale the fill object by health.
    }
}
