using UnityEngine;
using UnityEngine.UI;

namespace ColorBlindUtility.UGUI 
{
	[RequireComponent(typeof(RawImage))]
	[AddComponentMenu("UI/Color-Blind Support/Color-Blind Raw Image")]
	public class ColorBlindRawImage : ColorBlindBase 
	{
		public SupportType supportType = SupportType.ChangeColor;
		public enum SupportType 
		{ 
			ChangeColor, 
			ChangeTexture, 
			ChangeBoth 
		}

		// The RawImage component of this instance.
		private RawImage rawImage;

		// Textures for different color-blind options (RawImage only). 
		public Texture defaultTexture;
		public Texture protanopiaTexture;
		public Texture deuteranopiaTexture;
		public Texture tritanopiaTexture;

		protected override void Start() 
		{
			base.Start();
			rawImage = GetComponent<RawImage>();
			if (rawImage) 
			{
				defaultNormalColor = rawImage.color;
				defaultTexture = rawImage.texture;
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
				if (button && button.targetGraphic == rawImage) 
				{
					ColorBlock colorBlock = button.colors;
					colorBlock.normalColor = NormalColorToUse;
					colorBlock.disabledColor = DisabledColorToUse;
					colorBlock.highlightedColor = HighlightedColorToUse;
					colorBlock.pressedColor = PressedColorToUse;
					button.colors = colorBlock;
				}
				if (rawImage) 
				{
					rawImage.color = NormalColorToUse;
				}
			}
			if (supportType == SupportType.ChangeTexture || supportType == SupportType.ChangeBoth) 
			{
				if (rawImage) 
				{
					rawImage.texture = TextureToUse;
				}
			}
		}

		private Texture TextureToUse 
		{
			get 
			{
				switch (colorBlindMode) 
				{
				case ColorBlindMode.None:
					return defaultTexture;
				case ColorBlindMode.Protanopia:
					return protanopiaTexture ? protanopiaTexture : defaultTexture;
				case ColorBlindMode.Deuteranopia:
					return deuteranopiaTexture ? deuteranopiaTexture : defaultTexture;
				case ColorBlindMode.Tritanopia:
					return tritanopiaTexture ? tritanopiaTexture : defaultTexture;
				default:
					return defaultTexture;
				}
			}
		}

		public bool IsButtonTarget 
		{
			get 
			{
				if (button && button.targetGraphic == rawImage || GetComponent<Button>() && GetComponent<Button>().targetGraphic == GetComponent<RawImage>()) 
				{
					return true;
				}
				return false;
			}
		}
	}
}