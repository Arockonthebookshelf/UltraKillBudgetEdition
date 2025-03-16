using System;
using UnityEngine;
using UnityEngine.UI;

public class HitIndicator : MonoBehaviour
{
    Image hitIndicator;
    [SerializeField] float indicatorTime = 0.1f;

    void Awake()
    {
        hitIndicator = GetComponent<Image>();
    }

    void Start()
    {
        hitIndicator.enabled = false;
    }

    public void Hit()
    {
        if(!hitIndicator.enabled)
        {
            hitIndicator.enabled = true;
            Invoke("DisableIndicator", indicatorTime);
        }
        else
        {
            CancelInvoke();
            Invoke("DisableIndicator", indicatorTime);
        }
    }
    void DisableIndicator()
    {
        hitIndicator.enabled = false;
    }
}
