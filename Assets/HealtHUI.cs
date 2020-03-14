using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealtHUI : MonoBehaviour
{
    public CharacterEntity character;
    public Image image;

    // Update is called once per frame
    void Update()
    {
        if (character != null)
        {
            image.fillAmount = character.health / (float)character.maxHealth;
            image.color = Color.Lerp(Color.red, Color.green, character.health / (float)character.maxHealth);
        }
    }
}
