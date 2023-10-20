using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public float score;
    private PlayerController playercontrollerscript;
    public Transform startingPoint;
    public float lerpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        playercontrollerscript = GetComponent<PlayerController>();
        score = 0;
        playercontrollerscript.GameOver = true;
        StartCoroutine(PlayIntro());
    }

  

    // Update is called once per frame
    void Update()
    {
        if (playercontrollerscript.GameOver)
        {
            score += 2;
        }
        else
        {
            score++;
        }
        Debug.Log("score: " + score);
        }
    IEnumerator PlayIntro()
    {
        Vector3 startPos = playercontrollerscript.transform.position;
        Vector3 endPos = startingPoint.position;
        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;
        float distanceCovered = (Time.time - startTime) * lerpSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;
        playercontrollerscript.GetComponent<Animator>().SetFloat("Speed_Multiplier",
        0.5f);
        while (fractionOfJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * lerpSpeed;
            fractionOfJourney = distanceCovered / journeyLength;
            playercontrollerscript.transform.position = Vector3.Lerp(startPos, endPos,
            fractionOfJourney);
            yield return null;
        }

        playercontrollerscript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 1.0f);
        playercontrollerscript.GameOver = false;
    }
  
}
