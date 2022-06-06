using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static Text moneyText;
    public static Text livesText;
    public static int lives;
    public int startLives = 5;
    public static int Money;
    public int startMoney = 400;

    // Start is called before the first frame update
    void Start()
    {
        Money = startMoney;
        lives = startLives;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
