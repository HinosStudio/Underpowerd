using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private float value = 50.0f;
    [SerializeField] private float lowerBound = 0.0f;
    [SerializeField] private float upperBound = 100.0f;

    [SerializeField] private Image mask = null;

    public float Value {
        get => value;
        set {
            this.value = value;
            UpdateFill();
        }
    }

    public float LowerBound {
        get => lowerBound;
        set {
            lowerBound = value;
            UpdateFill();
        }
    }

    public float UpperBound {
        get => upperBound;
        set {
            upperBound = value;
            UpdateFill();
        }
    }

    private void OnValidate() {
        UpdateFill();
    }

    private void UpdateFill() {
        float currentOffset = value - lowerBound;
        float maxOffset = upperBound - lowerBound;
        mask.fillAmount = currentOffset / maxOffset;
    }
}
