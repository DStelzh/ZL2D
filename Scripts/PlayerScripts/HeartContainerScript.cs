using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HeartContainerScript : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite trqHeart;
    public Sprite halfHeart;
    public Sprite qrtHeart;
    public Sprite emptHeart;
    public FloatValue HeartContainers;
    public FloatValue playerCurrentHealth;

   

    // Start is called before the first frame update
    void Start()
    {
        InitHearts();
    }

    public void InitHearts()
    {
        for (int i = 0; i < HeartContainers.initialValue; i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }
    }

    public void UpdateHearts()
    {
        float tempHealth = playerCurrentHealth.RuntimeValue / 4; // bc 4 sprites
        for (int i = 0; i < HeartContainers.initialValue; i++)
        {
            float currHeart = Mathf.Ceil(tempHealth - 1); // We want to get the current heart that is "next" up to be decremented. For example if our current health is 7... 7/4 = 1.75 
                                                          //so this means that there is 1 full heart and the second heart needs to be decremented to be 3 / 4 heart.
                                                          // so by doing the ceiling of 1.75 it rounds up to 2 so we know that heart 2
                                                          //(Or in other words the 1st index since arrays are 0 index) is the one that needs to be manipulated.
            if (i <= tempHealth - 1)
            {
                //FullHeart
                hearts[i].sprite = fullHeart;
            }
            else if (i >= tempHealth)
            {
                //emptyHeart
                hearts[i].sprite = emptHeart;
            }
            else if (i == currHeart && (tempHealth % 1) == .50) //"i == currHeart": We want to make sure we are only editing the correct heart
                                                 // "(tempHealth % 1) == X" by moding the temp health we get the remainder so in the example above 1.75 moded by 1 has a remainder of .75 we then compare the remainder to which
                                                //  quarter of heart it needs to be and update the sprite accordingly.
            {
                //Half full heart
                hearts[i].sprite = halfHeart;
            }
            else if (i == currHeart && (tempHealth % 1) == .25)
            {
                //1/4 heart
                hearts[i].sprite = qrtHeart;
            }
            else /*(i == currHeart && (tempHealth % 1) == .75)*/
            {
                //3/4 heart
                hearts[i].sprite = trqHeart;
            }
        }
    }
}
