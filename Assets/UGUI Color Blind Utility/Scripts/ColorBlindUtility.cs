using UnityEngine;
using System;
using System.Collections.Generic;

namespace ColorBlindUtility.UGUI 
{
	[AddComponentMenu("UI/Color-Blind Support/Color-Blind Utility")]
	public class ColorBlindUtility : MonoBehaviour 
	{
		public ColorBlindMode colorBlindMode = ColorBlindMode.None;

		public Action<ColorBlindMode> OnColorBlindModeChangeEvent;

		private void OnEnable() 
		{
			foreach (ColorBlindBase element in GetComponentsInChildren<ColorBlindBase>()) 
			{
				OnColorBlindModeChangeEvent += element.Apply;
			}
		}

		private void OnDisable() 
		{
			foreach (ColorBlindBase element in GetComponentsInChildren<ColorBlindBase>()) 
			{
				OnColorBlindModeChangeEvent -= element.Apply;
			}
		}

		public void SetColorBlindMode(int index) 
		{
			SetColorBlindMode((ColorBlindMode)index);
		}

		public void SetColorBlindMode(ColorBlindMode mode) 
		{
			colorBlindMode = mode;
			if (OnColorBlindModeChangeEvent != null) 
			{
				OnColorBlindModeChangeEvent(colorBlindMode);
			}
		}
	}
}