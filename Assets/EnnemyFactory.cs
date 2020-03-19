using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyFactory : MonoBehaviour
{
    public static EnnemyFactory instance;
    static List<CharacterEntity> stock = new List<CharacterEntity>();
    public CharacterEntity ennemyPrefab;
    public int  maxSize = 50;
    static int instanced = 0;

    private void Awake()
    {
        instance = this;
    }

    public static void Create(Vector3 position, Quaternion rotation)
    {
        CharacterEntity ennemy = null;
        if (stock.Count > 0 && stock[0] == null)
        {
            stock.Clear();
        }
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
            if (instanced < instance.maxSize)
            {
                ennemy = Instantiate(instance.ennemyPrefab, position, rotation);
                instanced++;
            }
        }
        if (ennemy != null)
        {
            ennemy.health = ennemy.maxHealth;
            CitizenAI[] citizenCharacters = FindObjectsOfType<CitizenAI>();
            ennemy.navMeshAgent.Warp(position);
            if (citizenCharacters.Length > 0)
            {
                ennemy.MoveToPosition(citizenCharacters[Random.Range(0, citizenCharacters.Length)].transform.position);
            }
            else
            {
                AllyAI[] playerCharacters = FindObjectsOfType<AllyAI>();
                ennemy.MoveToPosition(playerCharacters[Random.Range(0, playerCharacters.Length)].transform.position);
            }
        }
    }

    public static void Restock(CharacterEntity character)
    {
        character.gameObject.SetActive(false);
        stock.Add(character);
    }
}
