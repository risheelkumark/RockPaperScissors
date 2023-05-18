using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using System.Reflection;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    public List<GameHand> hands;

    public TextMeshProUGUI botHand;

    public Button handBtn;// rock, scissors, spock, lizard, paper;

    public int totalTurns;

    public int playerIndex;
    public int botIndex;

    public RectTransform homePanel;
    public RectTransform gamePlayPanel;

    public Image timerFill;
    public TextMeshProUGUI timer;

    public RectTransform handsPanel;

    public UiManager uiManager;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        hands = new List<GameHand> ();
        hands.Add(
            CreateHand(HandConstants.Rock,
            new List<string> { HandConstants.Scissors, HandConstants.Lizard },
            new List<string> { HandConstants.Paper, HandConstants.Spock }));

        hands.Add(
          CreateHand(HandConstants.Paper,
          new List<string> { HandConstants.Rock, HandConstants.Spock },
          new List<string> { HandConstants.Lizard, HandConstants.Scissors }));

        hands.Add(
           CreateHand(HandConstants.Scissors,
           new List<string> { HandConstants.Paper, HandConstants.Lizard },
           new List<string> { HandConstants.Rock, HandConstants.Spock }));

        hands.Add(
           CreateHand(HandConstants.Lizard,
           new List<string> { HandConstants.Paper, HandConstants.Spock },
           new List<string> { HandConstants.Rock, HandConstants.Scissors }));

        hands.Add(
           CreateHand(HandConstants.Spock,
           new List<string> { HandConstants. Rock, HandConstants.Scissors},
           new List<string> { HandConstants.Paper, HandConstants.Lizard }));

        for (int i = 0; i < hands.Count; i++)
        {
            HandItem item = Instantiate(handBtn.gameObject, handsPanel.transform).GetComponent<HandItem>();
            int index = i;
            item.Init(hands[i].hand, index, (int index) => { OnPlayerTurn(index); });
        }

        uiManager.Init(StartGame);
    }

    private void StartGame()
    {
        totalTurns = 0;
        playerIndex = -1;
        botIndex = UnityEngine.Random.Range(0, hands.Count);
        OnBotTurn(playerIndex == -1);
    }

    private GameHand CreateHand(string hand, List<string> upStream, List<string> downStream)
    {
        return new GameHand(hand, upStream, downStream);
    }

    public void OnBotTurn(bool newHand)
    {
        if (newHand)
        {
            botHand.text = hands[botIndex].hand;
        }
        else
        {
            botIndex = UnityEngine.Random.Range(0, hands[playerIndex].downStream.Count);
            botHand.text = hands[playerIndex].downStream[botIndex];
            botIndex = hands.IndexOf(hands.Find(x => x.hand.Equals(hands[playerIndex].downStream[botIndex])));
        }
        totalTurns++;
        uiManager.UpdateScore("Turns :" + totalTurns.ToString());

        StartCoroutine(RunPlayerTurn());
    }

    private IEnumerator RunPlayerTurn()
    {
        float delta = 1;
        int turn = totalTurns;
        bool Input = totalTurns == turn;
        while (delta > 0 && Input)
        {
            Input = totalTurns == turn;
            yield return new WaitForEndOfFrame();
            delta -= Time.deltaTime;
            timerFill.fillAmount = delta;
        }

        Input = totalTurns == turn;
        if (delta <= 0 && Input)
        {
            GameEnded();
        }
    }

    public void OnPlayerTurn(int index)
    {
        playerIndex = index;
        totalTurns++;
        OnValidateTurn(index);
        uiManager.UpdateScore("Turns :" + totalTurns.ToString());
    }

    public void OnValidateTurn(int index)
    {
        bool valid = hands[index].upStream.Find(x => x.Equals(hands[botIndex].hand)) != null;
        foreach (var item in hands[index].upStream)
        {
            Debug.Log(item);
        }
        if (valid)
        {
            OnBotTurn(false);
        }
        else
        {
            GameEnded();
        }
    }

    public void GameStart()
    {
        homePanel.gameObject.SetActive(false);
        gamePlayPanel.gameObject.SetActive(true);
    }

    private void GameEnded()
    {
        homePanel.gameObject.SetActive(true);
        gamePlayPanel.gameObject.SetActive(false);
    }
}
