using System;

public class ResourcePool {
    private readonly float maxValue;
    private float value;

    public float MaxValue { get => maxValue; }
    public float Value { get => value; }

    public ResourcePool(float maxValue, float initialValue) {
        this.maxValue = maxValue;
        this.value = initialValue;
    }

    public float ApplyValue(float value) {
        var delta = this.value + value;

        if (delta >= maxValue) {
            this.value = maxValue;
            return delta - maxValue;
        }

        if (delta <= 0) {
            this.value = 0;
            return delta;
        }

        this.value = delta;
        return 0;
    }
}
