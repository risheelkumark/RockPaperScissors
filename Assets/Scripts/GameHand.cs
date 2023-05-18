using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameHand
{
    public string hand;
    public List<string> upStream;
    public List<string> downStream;

    public GameHand(string hand, List<string> upStream, List<string> downStream)
    {
        this.hand = hand;
        this.upStream = upStream;
        this.downStream = downStream;
    }

    public bool CheckStream(string hand)
    {
        if (upStream.Contains(hand))
        {
            return true;
        }

        return false;
    }
}