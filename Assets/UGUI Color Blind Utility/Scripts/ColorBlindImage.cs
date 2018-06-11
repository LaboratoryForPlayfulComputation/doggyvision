using UnityEngine;
using UnityEngine.UI;

namespace ColorBlindUtility.UGUI 
{
	[RequireComponent(typeof(Image))]
	[AddComponentMenu("UI/Color-Blind Support/Color-Blind Image")]
	public class ColorBlindImage : ColorBlindBase 
	{
		public SupportType supportType = SupportType.ChangeColor;
		public enum SupportType 
		{ 
			ChangeColor, 
			ChangeSprite, 
			ChangeBoth 
		}

		// The Image component of this instance.
		private Image image;

		// Sprites for different color-blind options (Image only). 
		public Sprite defaultSprite;
		public Sprite protanopiaSprite;
		public Sprite deuteranopiaSprite;
		public Sprite tritanopiaSprite;

		protected override void Start() 
		{
			base.Start();
			image = GetComponent<Image>();
			if (image) 
			{
				defaultNormalColor = image.color;
				defaultSprite = image.sprite;
			}
		}

		/// <summary>
		/// Applies changes to this instance. 
		/// </summary>
		public override void Apply(ColorBlindMode mode) 
		{
			base.Apply(mode);
			if (supportType == SupportType.ChangeColor || supportType == SupportType.ChangeBoth) 
			{
				if (button && button.targetGraphic == image) 
				{
					ColorBlock colorBlock = button.colors;
					colorBlock.normalColor = NormalColorToUse;
					colorBlock.disabledColor = DisabledColorToUse;
					colorBlock.highlightedColor = HighlightedColorToUse;
					colorBlock.pressedColor = PressedColorToUse;
					button.colors = colorBlock;
				}
				if (image) 
				{
					image.color = NormalColorToUse;
				}
			}
			if (supportType == SupportType.ChangeSprite || supportType == SupportType.ChangeBoth) 
			{
				if (image) 
				{
					image.sprite = SpriteToUse;
				}
			}
		}

		private Sprite SpriteToUse 
		{
			get 
			{
				switch (colorBlindMode) 
				{
				case ColorBlindMode.None:
					return defaultSprite;
				case ColorBlindMode.Protanopia:
					return protanopiaSprite ? protanopiaSprite : defaultSprite;
				case ColorBlindMode.Deuteranopia:
					return deuteranopiaSprite ? deuteranopiaSprite : defaultSprite;
				case ColorBlindMode.Tritanopia:
					return tritanopiaSprite ? tritanopiaSprite : defaultSprite;
				default:
					return defaultSprite;
				}
			}
		}

		public bool IsButtonTarget 
		{
			get 
			{
				if (button && button.targetGraphic == image || GetComponent<Button>() && GetComponent<Button>().targetGraphic == GetComponent<Image>()) 
				{
					return true;
				}
				return false;
			}
		}
	}
}