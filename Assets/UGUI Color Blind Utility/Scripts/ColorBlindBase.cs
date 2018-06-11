using UnityEngine;
using UnityEngine.UI;

namespace ColorBlindUtility.UGUI 
{
	public abstract class ColorBlindBase : MonoBehaviour 
	{
		protected ColorBlindMode colorBlindMode = ColorBlindMode.None;

		// Used to allow Button color tweening if applicable to this instance.
		protected Button button;

		// Normal Colors for different color-blind options.
		public Color defaultNormalColor = Color.white;
		public Color protanopiaNormalColor = Color.white;
		public Color deuteranopiaNormalColor = Color.white;
		public Color tritanopiaNormalColor = Color.white;

		// Disabled Colors for different color-blind options (Button only).
		public Color defaultDisabledColor = Color.white;
		public Color protanopiaDisabledColor = Color.white;
		public Color deuteranopiaDisabledColor = Color.white;
		public Color tritanopiaDisabledColor = Color.white;

		// Highlighted Colors for different color-blind options (Button only).
		public Color defaultHighlightedColor = Color.white;
		public Color protanopiaHighlightedColor = Color.white;
		public Color deuteranopiaHighlightedColor = Color.white;
		public Color tritanopiaHighlightedColor = Color.white;

		// Pressed Colors for different color-blind options (Button only).
		public Color defaultPressedColor = Color.white;
		public Color protanopiaPressedColor = Color.white;
		public Color deuteranopiaPressedColor = Color.white;
		public Color tritanopiaPressedColor = Color.white;

		protected Color NormalColorToUse 
		{
			get 
			{
				switch (colorBlindMode) 
				{
				case ColorBlindMode.None:
					return defaultNormalColor;
				case ColorBlindMode.Protanopia:
					return protanopiaNormalColor;
				case ColorBlindMode.Deuteranopia:
					return deuteranopiaNormalColor;
				case ColorBlindMode.Tritanopia:
					return tritanopiaNormalColor;
				default:
					return defaultNormalColor;
				}
			}
		}

		protected Color DisabledColorToUse 
		{
			get 
			{
				switch (colorBlindMode) 
				{
				case ColorBlindMode.None:
					return defaultDisabledColor;
				case ColorBlindMode.Protanopia:
					return protanopiaDisabledColor;
				case ColorBlindMode.Deuteranopia:
					return deuteranopiaDisabledColor;
				case ColorBlindMode.Tritanopia:
					return tritanopiaDisabledColor;
				default:
					return defaultDisabledColor;
				}
			}
		}

		protected Color HighlightedColorToUse 
		{
			get 
			{
				switch (colorBlindMode) 
				{
				case ColorBlindMode.None:
					return defaultHighlightedColor;
				case ColorBlindMode.Protanopia:
					return protanopiaHighlightedColor;
				case ColorBlindMode.Deuteranopia:
					return deuteranopiaHighlightedColor;
				case ColorBlindMode.Tritanopia:
					return tritanopiaHighlightedColor;
				default:
					return defaultHighlightedColor;
				}
			}
		}

		protected Color PressedColorToUse 
		{
			get 
			{
				switch (colorBlindMode) 
				{
				case ColorBlindMode.None:
					return defaultPressedColor;
				case ColorBlindMode.Protanopia:
					return protanopiaPressedColor;
				case ColorBlindMode.Deuteranopia:
					return deuteranopiaPressedColor;
				case ColorBlindMode.Tritanopia:
					return tritanopiaPressedColor;
				default:
					return defaultNormalColor;
				}
			}
		}

		protected virtual void Start() 
		{
			button = transform.GetComponentInParent<Button>();
		}

		public virtual void Apply(ColorBlindMode mode) 
		{
			colorBlindMode = mode;
		}
	}
}