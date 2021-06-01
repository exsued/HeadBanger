using System;
using UnityEngine;

[Serializable]
public enum FoodType
{
    None = -1,
    Burger, //Толстеет
    Candy,  //Прыщи
    Protein,//Мускулы
    Vegetables,//Худеет
    WaterBottle,//Худеет
    BrokenGlass,//Синяки
    Dynamite,//Взрыв, синяки
    Poison  //Синяки, пластыри
}
public class Food : MonoBehaviour
{
    public GameObject deadInstance;
    public FoodType type;
    public bool DestroyByTouch = true;
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EatZone")
        {
            Player.instance.OnEat(type);
        }
        else if(other.tag == "EnemyEatZone")
        {
            Bot.instance.OnEat(type);
        }
        if (DestroyByTouch)
            Destroy(gameObject);
        if (deadInstance != null)
            Destroy(Instantiate(deadInstance, transform.position, Quaternion.identity), 1f);
    }
}
