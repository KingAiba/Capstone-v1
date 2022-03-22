using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    public Image foreground;
    public Image background;

    public EnemyController owner;

    public void UpdatePosition()
    {
        transform.position = Camera.main.WorldToScreenPoint(target.position + offset);
    }

    public void UpdateHealthBarFill(float percent)
    {
        float parentWidth = GetComponent<RectTransform>().rect.width;
        float newWidth = parentWidth * percent;
        foreground.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
    }


    private void Start()
    {
        owner = GetComponentInParent<EnemyController>();
        owner.OnDamage += UpdateHealthBarFill;
    }
    private void LateUpdate()
    {
        UpdatePosition();
    }

    private void OnDestroy()
    {
        owner.OnDamage -= UpdateHealthBarFill;
    }
}
