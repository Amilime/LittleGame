using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Ingame : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Slider slider;

    [SerializeField] private Image dashImage;
    [SerializeField] private float dashCooldown;
    // Start is called before the first frame update
    void Start()
    {
        if (playerStats != null)
            playerStats.onHealthChanged += UpdateHealthUI;
        UpdateHealthUI();

        dashCooldown = SkillManager.instance.dash.cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthUI();
        if (Input.GetKeyDown(KeyCode.LeftShift))
            SetCooldownOf(dashImage);

        CheckCooldownOf(dashImage, dashCooldown);
    }
    private void UpdateHealthUI()
    {
        slider.maxValue = playerStats.GetMaxHealthValue();
        slider.value = playerStats.currentHealth;
    }
    private void SetCooldownOf(Image _image)
    {
        if (_image.fillAmount <= 0)
            _image.fillAmount = 1;
    }
    private void CheckCooldownOf(Image _image,float _cooldown)
    {
        if (_image.fillAmount > 0)
            _image.fillAmount -= 1 / _cooldown * Time.deltaTime;
    }
}
