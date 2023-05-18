using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePlayUI : MonoBehaviour
{
    public TextMeshProUGUI score;
    public void UpdateScore(string score)
    {
        this.score.text = score;
    }
}
