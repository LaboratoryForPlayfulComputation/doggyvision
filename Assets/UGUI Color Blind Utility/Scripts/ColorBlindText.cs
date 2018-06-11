using UnityEngine;
using UnityEngine.UI;

namespace ColorBlindUtility.UGUI 
{
	[RequireComponent(typeof(Text))]
	[AddComponentMenu("UI/Color-Blind Support/Color-Blind Text")]
	public class ColorBlindText : ColorBlindBase 
	{
		public SupportType supportType = SupportType.ChangeColor;
		public enum SupportType 
		{ 
			ChangeColor, 
			ChangeFont, 
			ChangeBoth 
		}

		// The Text component of this instance.
		private Text text;
		
		// Fonts for different color-blind options. 
		public Font defaultFont;
		public Font protanopiaFont;
		public Font deuteranopiaFont;
		public Font tritanopiaFont;
		
		protected override void Start() 
		{
			base.Start();
			text = GetComponent<Text>();
			defaultNormalColor = text.color;
			defaultFont = text.font;
		}

		/// <summary>
		/// Applies changes to this instance. 
		/// </summary>
		public override void Apply(ColorBlindMode mode) 
		{
			base.Apply(mode);
			if (supportType == SupportType.ChangeColor || supportType == SupportType.ChangeBoth) 
			{
				if (button && button.targetGraphic == text) 
				{
					ColorBlock colorBlock = button.colors;
					colorBlock.normalColor = NormalColorToUse;
					colorBlock.disabledColor = DisabledColorToUse;
					colorBlock.highlightedColor = HighlightedColorToUse;
					colorBlock.pressedColor = PressedColorToUse;
					button.colors = colorBlock;
				}
				text.color = NormalColorToUse;
			}
			if (supportType == SupportType.ChangeFont || supportType == SupportType.ChangeBoth) 
			{
				text.font = FontToUse;
			}
		}

		private Font FontToUse 
		{
			get 
			{
				switch (colorBlindMode) 
				{
				case ColorBlindMode.None:
					return defaultFont;
				case ColorBlindMode.Protanopia:
					return protanopiaFont ? protanopiaFont : defaultFont;
				case ColorBlindMode.Deuteranopia:
					return deuteranopiaFont ? deuteranopiaFont : defaultFont;
				case ColorBlindMode.Tritanopia:
					return tritanopiaFont ? tritanopiaFont : defaultFont;
				default:
					return defaultFont;
				}
			}
		}

		public bool IsButtonTarget 
		{
			get 
			{
				if (button && button.targetGraphic == text || GetComponent<Button>() && GetComponent<Button>().targetGraphic == GetComponent<Text>()) 
				{
					return true;
				}
				return false;
			}
		}
	}
}