﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreScript : MonoBehaviour
{
    // Start is called before the first frame update

    public static int ScoreValue1 = 0;
    Text score1;
    void Start()
    {
        score1 = GetComponent<Text>();   
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score " + ScoreValue1;
    }
}
