using UnityEngine;
using System;
using System.Collections.Generic;

public class Simulator : MonoBehaviour
{
    [Header("Tournament")]
    public List<Blade> contestants = new List<Blade>();

    public Bracket currentBracket;

    public float upsetThreshold;

    [Header("Winner Probability")]
    [Tooltip("Higher values make stats have a stronger influence on the winner.")]
    [Range(1f, 4f)]
    public float statPower = 2f;

    [Tooltip("Prevents either contestant from having a 0% or 100% chance.")]
    [Range(0f, 50f)]
    public float minimumChance = 5f;

    [Header("Type Matchups")]
    [Tooltip("How much of a type advantage is given to the favorable matchup.")]
    [Range(0f, 50f)]
    public float typeAdvantage = 10f;

    void Start()
    {
        RunTournament();
    }

    void RunTournament()
    {
        currentBracket = GenerateBracket();
        SimulateTournament();
    }

    void SimulateTournament()
    {
        while (currentBracket.matches.Count > 0)
        {
            currentBracket = SimulateRound(currentBracket);
        }
    }

    Bracket GenerateBracket()
    {
        Bracket bracket = new Bracket();

        List<Blade> entries = new List<Blade>(contestants);

        while (entries.Count >= 2)
        {
            Blade contestantOne = GetRandomContestant(entries);
            Blade contestantTwo = GetRandomContestant(entries);

            Match match = new Match(contestantOne, contestantTwo);
            bracket.matches.Add(match);
        }

        if (entries.Count == 1)
        {
            Debug.LogWarning(
                $"{entries[0].Name} was not placed into the bracket because there was no opponent.");
        }

        return bracket;
    }

    Blade GetRandomContestant(List<Blade> entries)
    {
        int randomIndex = UnityEngine.Random.Range(0, entries.Count);

        Blade contestant = entries[randomIndex];
        entries.RemoveAt(randomIndex);

        return contestant;
    }

    Bracket SimulateRound(Bracket bracket)
    {
        Debug.Log($"======= Beginning Round {bracket.roundNum} =======");

        List<Blade> winners = DetermineRoundWinners(bracket.matches);

        if (winners.Count == 1)
        {
            DisplayWinner(winners[0]);

            bracket.matches.Clear();
            return bracket;
        }

        bracket.matches = CreateNextRoundMatches(winners);
        bracket.roundNum++;

        return bracket;
    }

    List<Blade> DetermineRoundWinners(List<Match> matches)
    {
        List<Blade> winners = new List<Blade>();

        foreach (Match match in matches)
        {
            Blade winner = DetermineMatchWinner(match);
            winners.Add(winner);
        }

        return winners;
    }

    Blade DetermineMatchWinner(Match match)
    {
        Blade one = match.contestantOne;
        Blade two = match.contestantTwo;

        double oneChance = CalculateWinChance(one, two);

        float roll = UnityEngine.Random.Range(0f, 100f);

        if (roll < oneChance)
        {
            LogMatchResult(one, two, oneChance, roll);
            return one;
        }

        LogMatchResult(two, one, 100 - oneChance, roll);
        return two;
    }

    double CalculateWinChance(Blade one, Blade two)
    {
        double oneStat = one.GetStatPercentage();
        double twoStat = two.GetStatPercentage();

        double oneWeight = Math.Pow(oneStat, statPower);
        double twoWeight = Math.Pow(twoStat, statPower);

        double chance = oneWeight / (oneWeight + twoWeight) * 100;

        chance += GetTypeAdvantage(one, two);

        chance = Math.Max(minimumChance, chance);
        chance = Math.Min(100 - minimumChance, chance);

        return chance;
    }

    double GetTypeAdvantage(Blade one, Blade two)
    {
        if (one.Type == BladeType.Balance ||
            two.Type == BladeType.Balance)
        {
            return 0;
        }

        if (one.Type == BladeType.Attack &&
            two.Type == BladeType.Stamina)
        {
            return typeAdvantage;
        }

        if (one.Type == BladeType.Stamina &&
            two.Type == BladeType.Defense)
        {
            return typeAdvantage;
        }

        if (one.Type == BladeType.Defense &&
            two.Type == BladeType.Attack)
        {
            return typeAdvantage;
        }

        if (two.Type == BladeType.Attack &&
            one.Type == BladeType.Stamina)
        {
            return -typeAdvantage;
        }

        if (two.Type == BladeType.Stamina &&
            one.Type == BladeType.Defense)
        {
            return -typeAdvantage;
        }

        if (two.Type == BladeType.Defense &&
            one.Type == BladeType.Attack)
        {
            return -typeAdvantage;
        }

        return 0;
    }

    List<Match> CreateNextRoundMatches(List<Blade> winners)
    {
        List<Match> newMatches = new List<Match>();

        for (int i = 0; i + 1 < winners.Count; i += 2)
        {
            Match match = new Match(winners[i], winners[i + 1]);
            newMatches.Add(match);
        }

        if (winners.Count % 2 != 0)
        {
            Debug.LogWarning(
                "There was an odd number of winners. One contestant was not paired.");
        }

        return newMatches;
    }

    void LogMatchResult(
        Blade winner,
        Blade loser,
        double winnerChance,
        float roll)
    {
        if (winnerChance <= upsetThreshold)
        {
            Debug.LogWarning("Upset !!!");
            Debug.LogWarning("Upset !!!");
            Debug.LogWarning("Upset !!!");
        }

        Debug.Log(
            $"{winner.Name} ({winner.Type}) defeated " +
            $"{loser.Name} ({loser.Type}) " +
            $"({winnerChance:F1}% chance | Roll: {roll:F1})"
        );
    }

    void DisplayWinner(Blade winner)
    {
        Debug.Log($"🏆 WINNER: {winner.Name} ({winner.Type})");
    }
}