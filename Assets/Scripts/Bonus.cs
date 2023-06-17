using System.Collections;
using System.Collections.Generic;
//using Codice.Utils;
using UnityEngine;
using TMPro;

public class Bonus : Entity
{
   public string bonusName;
   private bool isActivated;
   public TextMeshProUGUI moneyCount;
   private static Bonus instance;
   public static Bonus Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        moneyCount.text = PlayerPrefs.GetInt("Money").ToString();
        if (instance == null)
        {
            instance = this;
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player" && !isActivated)
        {
            isActivated = true;
            switch(bonusName)
            {
                
                case "Money":
                int Money = PlayerPrefs.GetInt("Money");
                PlayerPrefs.SetInt("Money", Money+1);
                if (Application.isPlaying)
                {
                    moneyCount.text = (Money + 1).ToString();
                    Destroy(gameObject);
                }

                break;
                case "Potion":
                Player_Behaviour.Instance.HealthPotion();
                if (Application.isPlaying)
                Destroy(gameObject);
                break;

            }
        }
    }
}
