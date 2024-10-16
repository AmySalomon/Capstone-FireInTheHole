using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{
	public Slider slider;
	public Gradient gradient;
	public Image fill;

	public void SetCharge(float charge)
	{
		slider.value = charge;

		fill.color = gradient.Evaluate(slider.normalizedValue);
	}

}
