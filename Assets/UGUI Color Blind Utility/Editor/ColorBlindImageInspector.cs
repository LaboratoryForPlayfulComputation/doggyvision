using UnityEngine;
using UnityEditor;

namespace ColorBlindUtility.UGUI 
{
	[CustomEditor(typeof(ColorBlindImage), true)]
	public class ColorBlindImageInspector : Editor
	{
		private ColorBlindImage colorBlindImage;

		private bool colorNormalFoldout = false;
		private bool colorDisabledFoldout = false;
		private bool colorHighlightedFoldout = false;
		private bool colorPressedFoldout = false;
		private bool spritesFoldout = false;
		
		void Awake() 
		{
			colorBlindImage = (ColorBlindImage)target;
		}
		
		public override void OnInspectorGUI() 
		{
			colorBlindImage.supportType = (ColorBlindImage.SupportType)EditorGUILayout.EnumPopup("Support Type", colorBlindImage.supportType);
			if (colorBlindImage.supportType == ColorBlindImage.SupportType.ChangeColor || colorBlindImage.supportType == ColorBlindImage.SupportType.ChangeBoth) 
			{
				colorNormalFoldout = EditorGUILayout.Foldout(colorNormalFoldout, (colorBlindImage.IsButtonTarget ? "Normal Colors" : "Colors"));
				if (colorNormalFoldout) 
				{
					colorBlindImage.protanopiaNormalColor = EditorGUILayout.ColorField(new GUIContent("Protanopia", "The color to be used for Protanopia color-blindness."), colorBlindImage.protanopiaNormalColor);
					colorBlindImage.deuteranopiaNormalColor = EditorGUILayout.ColorField(new GUIContent("Deuteranopia", "The color to be used for Deuteranopia color-blindness."), colorBlindImage.deuteranopiaNormalColor);
					colorBlindImage.tritanopiaNormalColor = EditorGUILayout.ColorField(new GUIContent("Tritanopia", "The color to be used for Tritanopia color-blindness."), colorBlindImage.tritanopiaNormalColor);
				}
				if (colorBlindImage.IsButtonTarget) 
				{
					colorDisabledFoldout = EditorGUILayout.Foldout(colorDisabledFoldout, "Disabled Colors");
					if (colorDisabledFoldout) 
					{
						colorBlindImage.protanopiaDisabledColor = EditorGUILayout.ColorField(new GUIContent("Protanopia", "The color to be used for Protanopia color-blindness."), colorBlindImage.protanopiaDisabledColor);
						colorBlindImage.deuteranopiaDisabledColor = EditorGUILayout.ColorField(new GUIContent("Deuteranopia", "The color to be used for Deuteranopia color-blindness."), colorBlindImage.deuteranopiaDisabledColor);
						colorBlindImage.tritanopiaDisabledColor = EditorGUILayout.ColorField(new GUIContent("Tritanopia", "The color to be used for Tritanopia color-blindness."), colorBlindImage.tritanopiaDisabledColor);
					}
					colorHighlightedFoldout = EditorGUILayout.Foldout(colorHighlightedFoldout, "Highlight Colors");
					if (colorHighlightedFoldout) 
					{
						colorBlindImage.protanopiaHighlightedColor = EditorGUILayout.ColorField(new GUIContent("Protanopia", "The color to be used for Protanopia color-blindness."), colorBlindImage.protanopiaHighlightedColor);
						colorBlindImage.deuteranopiaHighlightedColor = EditorGUILayout.ColorField(new GUIContent("Deuteranopia", "The color to be used for Deuteranopia color-blindness."), colorBlindImage.deuteranopiaHighlightedColor);
						colorBlindImage.tritanopiaHighlightedColor = EditorGUILayout.ColorField(new GUIContent("Tritanopia", "The color to be used for Tritanopia color-blindness."), colorBlindImage.tritanopiaHighlightedColor);
					}
					colorPressedFoldout = EditorGUILayout.Foldout(colorPressedFoldout, "Pressed Colors");
					if (colorPressedFoldout) 
					{
						colorBlindImage.protanopiaPressedColor = EditorGUILayout.ColorField(new GUIContent("Protanopia", "The color to be used for Protanopia color-blindness."), colorBlindImage.protanopiaPressedColor);
						colorBlindImage.deuteranopiaPressedColor = EditorGUILayout.ColorField(new GUIContent("Deuteranopia", "The color to be used for Deuteranopia color-blindness."), colorBlindImage.deuteranopiaPressedColor);
						colorBlindImage.tritanopiaPressedColor = EditorGUILayout.ColorField(new GUIContent("Tritanopia", "The color to be used for Tritanopia color-blindness."), colorBlindImage.tritanopiaPressedColor);
					}
				}
			}
			if (colorBlindImage.supportType == ColorBlindImage.SupportType.ChangeSprite || colorBlindImage.supportType == ColorBlindImage.SupportType.ChangeBoth) 
			{
				spritesFoldout = EditorGUILayout.Foldout(spritesFoldout, "Images");
				if (spritesFoldout) 
				{
					colorBlindImage.protanopiaSprite = (Sprite)EditorGUILayout.ObjectField(new GUIContent("Protanopia", "The sprite to be used for Protanopia color-blindness."), colorBlindImage.protanopiaSprite, typeof(Sprite), false);
					colorBlindImage.deuteranopiaSprite = (Sprite)EditorGUILayout.ObjectField(new GUIContent("Deuteranopia", "The sprite to be used for Deuteranopia color-blindness."), colorBlindImage.deuteranopiaSprite, typeof(Sprite), false);
					colorBlindImage.tritanopiaSprite = (Sprite)EditorGUILayout.ObjectField(new GUIContent("Tritanopia", "The sprite to be used for Tritanopia color-blindness."), colorBlindImage.tritanopiaSprite, typeof(Sprite), false);
				}
			}
		}
	}
}