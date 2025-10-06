using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(ColorMixing_POTIONCRAFT))]
public class Costumer_POTIONCRAFT : MonoBehaviour
{
    public int difficultySteps = 2;
    [SerializeField] bool newColor = false;
    public Color goalColor;

    ColorMixing_POTIONCRAFT mixing;
    private void Start()
    {
        mixing = GetComponent<ColorMixing_POTIONCRAFT>();
        generateColor(difficultySteps);
        
    }

    public void generateColor(int steps)
    {
        // clear old color
        mixing.resetColors();

        // generate color
        for (int i = 0; i < difficultySteps; i++)
        {
            int color = Random.Range(0, 4);

            switch (color)
            {
                case 0:
                    mixing.addColor(Color.red);
                    break;

                case 1:
                    mixing.addColor(Color.blue);
                    break;

                case 2:
                    mixing.addColor(Color.green);
                    break;
                case 3:
                    // skip
                    break;
            }
        }

        goalColor = GetComponent<Renderer>().material.color;

    }

    private void OnValidate()
    {
        if (newColor)
        {
            if (mixing == null) mixing = GetComponent<ColorMixing_POTIONCRAFT>();
            newColor = false;
            generateColor(difficultySteps);
        }
    }
}
