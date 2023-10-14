using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Fuel : MonoBehaviour
{
    public float fuel = 100f;
    public float baseInterval = 1f;
    float decreaser = 1f;
    public TextMeshProUGUI fuelDisplay;

    public static Fuel f;
    // Start is called before the first frame update
    void Start()
    {
        f = this;
        decreaser = baseInterval;
    }

    // Update is called once per frame
    void Update()
    {
        fuelDisplay.text = "Fuel: " + fuel;
    }

    public void fuelDecreaser()
    {
        if (decreaser > 0)
        {
            decreaser -= Time.deltaTime;
        }

        else
        {
            decreaser = baseInterval;
            fuel -= 1f;
        }
    }
}
