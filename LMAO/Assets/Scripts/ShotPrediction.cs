using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPrediction : MonoBehaviour
{
    public GameObject trajectoryDotPrefab;
    public int AmountOfDots = 5;
    List<GameObject> AllDots = new List<GameObject>();

    public bool showingDots = false;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= AmountOfDots; i++)
        {
            GameObject trajectoryDot = Instantiate(trajectoryDotPrefab);
            AllDots.Add(trajectoryDot);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (showingDots)
        {

            for (int i = 0; i < AllDots.Count; i++)
            {
                AllDots[i].transform.position = CalculatedPosition(i * 0.2f);
                AllDots[i].transform.position = new Vector3(AllDots[i].transform.position.x, AllDots[i].transform.position.y, -3);
            }
        }

        if (GetComponent<BallControl>().isShooting && showingDots == false && !GetComponent<BallControl>().cancelRange)
        {
            ShowDots();
        }
        else if (!GetComponent<BallControl>().isShooting || GetComponent<BallControl>().cancelRange)
        {
            HideDots();
        }

    }

    private Vector2 CalculatedPosition(float elapsedTime)
    {
        return GetComponent<BallControl>().vectorToShoot * elapsedTime + new Vector2(transform.position.x,transform.position.y);

    }

    public void ShowDots()
    {
        showingDots = true;
        foreach (GameObject dot in AllDots)
        {
            dot.SetActive(true);
        }
    }

    public void HideDots()
    {
        showingDots = false;
        foreach (GameObject dot in AllDots)
        {
            dot.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        HideDots();
    }
}
