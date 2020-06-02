using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DiceController : MonoBehaviour
{

    public List<Dice> dices;
    private int diceValue = 0;
    public Text diceValueText;

    private void ResetAll()
    {
        float zPosition = UnityEngine.Random.Range(-4, 4);
        diceValue = 0;
        dices.ForEach(dice => {
            zPosition += +1f;
            dice.Reset(zPosition);
        });
    }

    private void RollAllAgain()
    {
        float zPosition = UnityEngine.Random.Range(-4, 4);
        diceValue = 0;
        dices.ForEach(dice => {
            zPosition += +1f;
            dice.Reset(zPosition);
            dice.RollDice();
        });
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (checkForAllDices(dice => !dice.thrown && !dice.hasLanded))
            {
                dices.ForEach(dice => dice.RollDice());
            } else
            {
                ResetAll();
            }
        }

        if (checkForAllDices(dice => dice.rb.IsSleeping() && !dice.hasLanded && dice.thrown))
        {
            dices.ForEach(dice => {
                dice.hasLanded = true;
                dice.rb.useGravity = false;
                int diceVal = dice.SideValueCheck();
                if(diceVal == 0)
                {
                    RollAllAgain();
                    return;
                }
                diceValue += diceVal;
            });
            diceValueText.text = diceValue.ToString();
        } else if (checkForAllDices(dice => dice.rb.IsSleeping() && dice.hasLanded) && diceValue == 0)
        {
            RollAllAgain();
        }
    }

    private bool checkForAllDices(Func<Dice, bool> predicate)
    {
        return dices.All(predicate);
    }
}
