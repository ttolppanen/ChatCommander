using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MoneySystem : MonoBehaviour
{
    public ScriptableUnitBox[] unitBoxes;
    public static MoneySystem ins;
    public GameObject gunBox;
    public int gunBoxPrice;
    public GameObject resourceBox;
    public int resourceBoxPrice;
    public GameObject engineer;
    public int engineerCost;
    public Text moneyText;
    int money = 0;

    private void Start()
    {
        if (ins == null)
        {
            ins = this;
            UpdateMoneyText();
        }
        else
        {
            Destroy(this);
        }
    }
    public void GiveMoney(int amount)
    {
        money += amount;
        UpdateMoneyText();
    }
    public bool SpendMoney(int amount)
    {
        if (amount <= money)
        {
            money -= amount;
            UpdateMoneyText();
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool BuyGunBox()
    {
        return InstantiateBox(gunBoxPrice, gunBox);
    }
    public bool BuyResourceBox()
    {
        return InstantiateBox(resourceBoxPrice, resourceBox);
    }
    public bool BuyUnitBox(UnitTypes type)
    {
        ScriptableUnitBox box = unitBoxes.Single(unitBox => unitBox.type == type);
        return InstantiateBox(box.price, box.box);
    }
    bool InstantiateBox(int price, GameObject box)
    {
        if (SpendMoney(price))
        {
            Instantiate(box, GM.ins.RandomPositionOnMap(), Quaternion.Euler(0, 0, Random.Range(0f, 360f)));
            return true;
        }
        else
        {
            return false;
        }
    }
    void UpdateMoneyText()
    {
        moneyText.text = money.ToString();
    }
}
