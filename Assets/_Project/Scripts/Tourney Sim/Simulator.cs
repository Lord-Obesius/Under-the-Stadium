using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class NewMonoBehaviourScript : MonoBehaviour
{

    public List<Blade> contestants = new List<Blade>();
    public Bracket currentBracket;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentBracket = GenerateBracket();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Bracket GenerateBracket()
    {
        Bracket generated = new Bracket();
        generated.roundNum = 1;
        List<Blade> entries = new List<Blade>(contestants);

        while (entries.Count > 0)
        {
            int randIndex = UnityEngine.Random.Range(0, entries.Count);
            Blade one = entries[randIndex];
            entries.RemoveAt(randIndex);

            if (entries.Count == 0)
            {
                break;
            }

            randIndex = UnityEngine.Random.Range(0, entries.Count);
            Blade two = entries[randIndex];
            entries.RemoveAt(randIndex);

            Match pairing = new Match(one, two);
        }

        return generated;
    }

    void SimulateRound(Bracket currentBracket)
    {
        List<Blade> winners = new List<Blade>();
        for (int i = 0; i < currentBracket.matches.Count; i++)
        {
            Match current = currentBracket.matches[i];
            Blade one = current.contestantOne;
            Blade two = current.contestantTwo;

            double Onechance = (one.GetStatPercentage() / (one.GetStatPercentage() + two.GetStatPercentage())) * 100;
            if (UnityEngine.Random.Range(0, 100) <= Onechance)
            {
                winners.Add(one);
                continue;
            }

            winners.Add(two);
        }
    }
}
