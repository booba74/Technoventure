using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour
{
    protected GameObject bar;
    protected Text barText;
    protected float count = 0;
    [SerializeField] protected float startCount;
    [SerializeField] protected float _maxCount;
    void Start()
    {
     //   InitBar(_maxCount);
     //   UpdateBar(startCount);
    }

    protected virtual void InitBar(float maxCount)
    {
        _maxCount = maxCount;
    }

    public void UpdateBar(float newCount)
    {
        float prevCount = count;    
        count += newCount;
        count = Mathf.Clamp(count, 0, _maxCount);
        float percent = count / _maxCount;
     //   bar.GetComponent<Image>().fillAmount = percent;
  //      barText.text = count.ToString() + "%";
    }
}
