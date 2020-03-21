using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerFactory : MonoBehaviour
{
    public static AttackerFactory instance;
    static List<AttackerAI> stock = new List<AttackerAI>();
    public AttackerAI attackerPrefab;
    public int  maxSize = 50;
    static int instanced = 0;

    private void Awake()
    {
        instance = this;
    }

    public static void Create(Vector3 position, Quaternion rotation)
    {
        AttackerAI attacker = null;
        if (stock.Count > 0 && stock[0] == null)
        {
            stock.Clear();
            instanced = 0;
        }
        if (stock.Count > 0)
        {
            attacker = stock[0];
            stock.Remove(attacker);
        }
        else
        {
            if (instanced < instance.maxSize)
            {
                attacker = Instantiate(instance.attackerPrefab, position, rotation);
                instanced++;
            }
        }
        if (attacker != null)
        {
            attacker.Init(position, rotation);
        }
    }

    public static void Restock(AttackerAI attacker)
    {
        attacker.gameObject.SetActive(false);
        stock.Add(attacker);
    }
}
