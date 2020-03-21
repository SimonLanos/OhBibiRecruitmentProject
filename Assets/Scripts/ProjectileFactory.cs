using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFactory : MonoBehaviour
{
    public static ProjectileFactory instance;
    static List<Projectile> stock = new List<Projectile>();
    public Projectile projectilePrefab;

    private void Start()
    {
        instance = this;
    }

    public static void Create(LayerMask layerMask, Vector3 position, Quaternion rotation)
    {
        Projectile projectile = null;
        if(stock.Count>0 && stock[0] == null)
        {
            stock.Clear();
        }
        if (stock.Count > 0)
        {
            projectile = stock[0];
            stock.Remove(projectile);
        }
        else
        {
            projectile  = Instantiate(instance.projectilePrefab, instance.transform);
        }
        projectile.Init(position, rotation,layerMask);
    }

    public static void Restock(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
        stock.Add(projectile);
    }
}
