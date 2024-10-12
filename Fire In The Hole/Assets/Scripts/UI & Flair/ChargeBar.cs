using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{
	public Slider slider;
	public Gradient gradient;
	public Image fill;

	public void SetMaxCharge(float charge)
	{
		slider.maxValue = 0.5f;
		slider.value = 0;

		fill.color = gradient.Evaluate(1f);
	}

	public void SetCharge(float charge)
	{
		slider.value = charge;

		fill.color = gradient.Evaluate(slider.normalizedValue);
	}

}
