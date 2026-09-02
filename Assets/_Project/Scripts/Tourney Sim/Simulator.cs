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
        SimulateTournament();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SimulateTournament()
    {
        do
        {
            currentBracket = SimulateRound(currentBracket);
        }
        while (currentBracket.matches.Count >= 1);
    }

    Bracket GenerateBracket()
    {
        Bracket generated = new Bracket();
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

            generated.matches.Add(pairing);
        }

        return generated;
    }

    Bracket SimulateRound(Bracket currentBracket)
    {
        Debug.Log("======= Beginning Round {currentBracket.roundNum} =======");

        List<Blade> winners = new List<Blade>();
        for (int i = 0; i < currentBracket.matches.Count; i++)
        {
            Match current = currentBracket.matches[i];
            Blade one = current.contestantOne;
            Blade two = current.contestantTwo;

            double Onechance = one.GetStatPercentage() * 100 / (one.GetStatPercentage() + two.GetStatPercentage());

            Debug.Log($"{one.Name} has a {Onechance}% chance of defeating {two.Name}");

            if (UnityEngine.Random.Range(0, 100) <= Onechance)
            {
                winners.Add(one);
                Debug.Log($"Blade one ({one.Name}) defeated {two.Name}");
                continue;
            }

            winners.Add(two);

            Debug.Log($"Blade two ({two.Name}) defeated {one.Name}");
        }

        if (winners.Count == 1)
        {
            DisplayWinner(winners[0]);
            currentBracket.matches = new List<Match>();
            return currentBracket;
        }

        List<Match> newMatches = new();

        for (int i = 0; i < winners.Count; i += 2)
        {
            newMatches.Add(new Match(winners[i], winners[i + 1]));
        }

        currentBracket.matches = newMatches;

        currentBracket.roundNum++;

        return currentBracket;
    }

    void DisplayWinner(Blade winner)
    {
        Debug.Log($"Winner: {winner.Name}");
    }
}
