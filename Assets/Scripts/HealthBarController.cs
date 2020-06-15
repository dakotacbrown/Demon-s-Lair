using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour {

    public Slider slider;
    public Gradient gradient;
    public Image phil;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void SetHealth(int health) {
        slider.value = health;
        phil.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxHealth(int health) {
        slider.maxValue = health;
        slider.value = health;
        phil.color = gradient.Evaluate(1f);
    }
}
