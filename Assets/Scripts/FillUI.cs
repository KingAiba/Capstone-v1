using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FillUI : MonoBehaviour
{
    public Image foreground;
    public Image background;


    public virtual void UpdateFill(float percent)
    {
        foreground.fillAmount = percent;
        if(percent >= 0.98f)
        {
            DisableFillOnComplete();
        }
    }

    public virtual void UpdateBarFill(float percent)
    {
        float parentWidth = GetComponent<RectTransform>().rect.width;
        float newWidth = parentWidth * percent;
        foreground.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
    }

    private void DisableFillOnComplete()
    {
        gameObject.SetActive(false);
    }

}
