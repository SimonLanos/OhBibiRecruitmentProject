using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyFactory : MonoBehaviour
{
    public static EnnemyFactory instance;
    static List<CharacterEntity> stock = new List<CharacterEntity>();
    public CharacterEntity ennemyPrefab;

    private void Start()
    {
        instance = this;
    }

    public static void Create(LayerMask layerMask, Vector3 position, Quaternion rotation)
    {
        CharacterEntity ennemy = null;
        if (stock.Count > 0)
        {
            ennemy = stock[0];
            stock.Remove(ennemy);
            ennemy.gameObject.SetActive(true);
            ennemy.transform.position = position;
            ennemy.transform.rotation = rotation;
        }
        else
        {
            ennemy = Instantiate(instance.ennemyPrefab, position, rotation, instance.transform);
        }

    }

    public static void Restock(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
        //stock.Add(projectile);
    }
}
