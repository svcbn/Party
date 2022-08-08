using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text p1StarUI;
    public Text p1CoinUI;
    public Text p1RankUI;

    int p1Star = 0;
    int p1Coin = 0;
    int p1Rank = 0;

    public int P1Star
    {
        get
        {
            return p1Star;
        }
        set
        {
            p1Star = value;
            p1StarUI.text = "Stars : " + p1Star + 1;
            PlayerPrefs.SetInt("p1Star", p1Star);
        }
    }

    public int P1Coin
    {
        get
        {
            return p1Coin;
        }
        set
        {
            p1Coin = value;
            p1CoinUI.text = "Coins : " + p1Coin;
            PlayerPrefs.SetInt("p1Coin", p1Coin);
        }
    }

    public int P1Rank
    {
        get
        {
            return p1Rank;
        }
        set
        {
            p1Rank = value;
            p1RankUI.text = "1st";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
