using UnityEngine;
using UnityEngine.UI;

public class HappyBarController : BarController
{
    [SerializeField] float decreaseMoodPerMin = 1f;
    float timer;
    protected override void InitBar(float maxCount)
    {
        bar = GameObject.FindWithTag("happyBar");
        barText = GameObject.FindWithTag("happyBarText").GetComponent<Text>();
        base.InitBar(_maxCount);
    } 
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 60)
        {
            UpdateBar(-decreaseMoodPerMin);
            timer = 0;
        }

    }

    void Win()
    {

    }

    void Lose()
    {

    }
}
