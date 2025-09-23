using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Renderer))]
public class colorMixing : MonoBehaviour
{
    // Unsure if this script will be used, or if some elements will be copy-pasted into a different script (like a cauldron.cs script or something)
    // Basic idea is to average the color of gameobject this is attached to with input color - maybe except when white (inital color) in which case 
    // itll just take the new color

    // when generating customer demands, maybe run some random combos through this instead of just selecting a random color to better control the difficulty and ensure its possible
    // and can seperate different difficulties as "steps to complete" where each step is either {red, blue, yellow, none}
    // ex) 2 step: blue -> red (makes purple)
    // ex) 5 step: yellow -> red -> none -> blue -> red
    // probabaly can be done in any order?
    // make sure first step is never "none"

    Material material;
    List<Color> colors = new List<Color>();

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    public void addColor(Color newColor)
    {
        Color curColor = material.color;
        colors.Add(newColor);

        // if has had no color yet, just become new color
        if (curColor.Equals(Color.white))
        {
            resetColors();
            colors.Add (newColor);
            material.color = newColor;
        }
        else if (newColor.Equals(Color.black))
        {
            Debug.Log("Hit Black");
            // if new color is black then skip
            return;
        }
        else
        {
            // average colors
            //material.color = averageColors(colors);
            material.color = averageRGB(colors);

        }

    }

    private void resetColors()
    {
        material.color = Color.white;
        colors.Clear();
    }

    // going to try rgb averaging again but with manually setting saturation to max each time
    private Color averageRGB(List<Color> lst)
    {
        float r = 0, g = 0, b = 0;

        for (int i = 0; i < lst.Count; i++)
        {
            r += lst[i].r;
            g += lst[i].g;
            b += lst[i].b;
        }

        r /= lst.Count;
        g /= lst.Count;
        b /= lst.Count;

        Color newCol = new Color(r, g, b);
        Color.RGBToHSV(newCol, out float h, out float s, out float v);
        newCol = Color.HSVToRGB(h, s, 1f);
        return newCol;
    }

    private Color averageColors(List<Color> lst)
    {
        float sSum = 0;
        float vSum = 0;
        List<float> hLst = new List<float>();

        for (int i = 0; i < lst.Count; i++)
        {
            Color.RGBToHSV(colors[i], out float h, out float s, out float v);
            hLst.Add(h);
            sSum += s;
            vSum += v;
        }

        float newH = circularAverage(hLst);
        float newS = sSum / lst.Count;
        float newV = vSum / lst.Count;



        return Color.HSVToRGB(newH, newS, newV);
    }

    // Old way - didnt like, delete before turn in project
    private Color averageColorsOLD(Color color1, Color color2)
    {
        // Translate to HSV, this is because averaging in RGB tends to lower saturation
        Color.RGBToHSV(color1, out float h1, out float s1, out float v1);
        Color.RGBToHSV(color2, out float h2, out float s2, out float v2);

        float newH;
        float newS;
        float newV;

        if (Mathf.Approximately(Mathf.Abs(h1 - h2), 0.5f)) // colors are opposites, average linearly and set saturation to 0
        {
            newH = (h1 + h2) / 2f;
            newS = 0;
        }
        else
        {
            newH = circularAverage(h1, h2);
            newS = (s1 + s2) / 2;
        }
        newV = (v1 + v2) / 2;

        Color returnColor = Color.HSVToRGB(newH, newS, newV);
        //Color returnColor = Color.HSVToRGB((h1 + h2) / 2, (s1 + s2) / 2, (v1 + v2) / 2);


        return returnColor;
    }

    private float circularAverage(List<float> lst)
    {
        float xSum = 0;
        float ySum = 0;

        for (int i = 0; i < lst.Count; i++)
        {
            float angle = lst[i] * 2f * Mathf.PI;

            xSum += Mathf.Cos(angle);
            ySum += Mathf.Sin(angle);
        }

        float xAvg = xSum / lst.Count;
        float yAvg = ySum / lst.Count;

        float avg = (Mathf.Atan2(yAvg, xAvg)) / (2f * Mathf.PI);
        if (avg < 0f) avg += 1f; // wrap around
        return avg;
    }

    // circular average for just 2 - not needed anymore delete before turn in
    private float circularAverage(float n1, float n2)
    {
        float angle1 = n1 * 2f * Mathf.PI;
        float angle2 = n2 * 2f * Mathf.PI;

        float x1 = Mathf.Cos(angle1);
        float y1 = Mathf.Sin(angle1);

        float x2 = Mathf.Cos(angle2);
        float y2 = Mathf.Sin(angle2);

        float avgX = (x1 + x2) / 2;
        float avgY = (y1 + y2) / 2;

        float avg;

        
        avg = (Mathf.Atan2(avgY, avgX)) / (2f * Mathf.PI);
        if (avg < 0f) avg += 1f; // wrap around

        return avg;
    }

    //TEMP METHOD FOR TESTING - REMOVE BEFORE TURNING IN
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "reset") resetColors();
        else addColor(other.gameObject.GetComponent<Renderer>().material.color);
    }

}
