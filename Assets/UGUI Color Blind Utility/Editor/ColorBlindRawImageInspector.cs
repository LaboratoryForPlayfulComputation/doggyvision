using UnityEngine;
using UnityEditor;

namespace ColorBlindUtility.UGUI 
{
	[CustomEditor(typeof(ColorBlindRawImage), true)]
	public class ColorBlindRawImageInspector : Editor
	{
		private ColorBlindRawImage colorBlindRawImage;

		private bool colorNormalFoldout = false;
		private bool colorDisabledFoldout = false;
		private bool colorHighlightedFoldout = false;
		private bool colorPressedFoldout = false;
		private bool textureFoldout = false;

		void Awake() 
		{
			colorBlindRawImage = (ColorBlindRawImage)target;
		}

		public override void OnInspectorGUI() 
		{
			colorBlindRawImage.supportType = (ColorBlindRawImage.SupportType)EditorGUILayout.EnumPopup("Support Type", colorBlindRawImage.supportType);
			if (colorBlindRawImage.supportType == ColorBlindRawImage.SupportType.ChangeColor || colorBlindRawImage.supportType == ColorBlindRawImage.SupportType.ChangeBoth) 
			{
				colorNormalFoldout = EditorGUILayout.Foldout(colorNormalFoldout, (colorBlindRawImage.IsButtonTarget ? "Normal Colors" : "Colors"));
				if (colorNormalFoldout) 
				{
					colorBlindRawImage.protanopiaNormalColor = EditorGUILayout.ColorField(new GUIContent("Protanopia", "The color to be used for Protanopia color-blindness."), colorBlindRawImage.protanopiaNormalColor);
					colorBlindRawImage.deuteranopiaNormalColor = EditorGUILayout.ColorField(new GUIContent("Deuteranopia", "The color to be used for Deuteranopia color-blindness."), colorBlindRawImage.deuteranopiaNormalColor);
					colorBlindRawImage.tritanopiaNormalColor = EditorGUILayout.ColorField(new GUIContent("Tritanopia", "The color to be used for Tritanopia color-blindness."), colorBlindRawImage.tritanopiaNormalColor);
				}
				if (colorBlindRawImage.IsButtonTarget) 
				{
					colorDisabledFoldout = EditorGUILayout.Foldout(colorDisabledFoldout, "Disabled Colors");
					if (colorDisabledFoldout) 
					{
						colorBlindRawImage.protanopiaDisabledColor = EditorGUILayout.ColorField(new GUIContent("Protanopia", "The color to be used for Protanopia color-blindness."), colorBlindRawImage.protanopiaDisabledColor);
						colorBlindRawImage.deuteranopiaDisabledColor = EditorGUILayout.ColorField(new GUIContent("Deuteranopia", "The color to be used for Deuteranopia color-blindness."), colorBlindRawImage.deuteranopiaDisabledColor);
						colorBlindRawImage.tritanopiaDisabledColor = EditorGUILayout.ColorField(new GUIContent("Tritanopia", "The color to be used for Tritanopia color-blindness."), colorBlindRawImage.tritanopiaDisabledColor);
					}
					colorHighlightedFoldout = EditorGUILayout.Foldout(colorHighlightedFoldout, "Highlight Colors");
					if (colorHighlightedFoldout) 
					{
						colorBlindRawImage.protanopiaHighlightedColor = EditorGUILayout.ColorField(new GUIContent("Protanopia", "The color to be used for Protanopia color-blindness."), colorBlindRawImage.protanopiaHighlightedColor);
						colorBlindRawImage.deuteranopiaHighlightedColor = EditorGUILayout.ColorField(new GUIContent("Deuteranopia", "The color to be used for Deuteranopia color-blindness."), colorBlindRawImage.deuteranopiaHighlightedColor);
						colorBlindRawImage.tritanopiaHighlightedColor = EditorGUILayout.ColorField(new GUIContent("Tritanopia", "The color to be used for Tritanopia color-blindness."), colorBlindRawImage.tritanopiaHighlightedColor);
					}
					colorPressedFoldout = EditorGUILayout.Foldout(colorPressedFoldout, "Pressed Colors");
					if (colorPressedFoldout) 
					{
						colorBlindRawImage.protanopiaPressedColor = EditorGUILayout.ColorField(new GUIContent("Protanopia", "The color to be used for Protanopia color-blindness."), colorBlindRawImage.protanopiaPressedColor);
						colorBlindRawImage.deuteranopiaPressedColor = EditorGUILayout.ColorField(new GUIContent("Deuteranopia", "The color to be used for Deuteranopia color-blindness."), colorBlindRawImage.deuteranopiaPressedColor);
						colorBlindRawImage.tritanopiaPressedColor = EditorGUILayout.ColorField(new GUIContent("Tritanopia", "The color to be used for Tritanopia color-blindness."), colorBlindRawImage.tritanopiaPressedColor);
					}
				}
			}
			if (colorBlindRawImage.supportType == ColorBlindRawImage.SupportType.ChangeTexture || colorBlindRawImage.supportType == ColorBlindRawImage.SupportType.ChangeBoth) 
			{
				textureFoldout = EditorGUILayout.Foldout(textureFoldout, "Textures");
				if (textureFoldout) 
				{
					colorBlindRawImage.protanopiaTexture = (Texture)EditorGUILayout.ObjectField(new GUIContent("Protanopia", "The texture to be used for Protanopia color-blindness."), colorBlindRawImage.protanopiaTexture, typeof(Texture), false);
					colorBlindRawImage.deuteranopiaTexture = (Texture)EditorGUILayout.ObjectField(new GUIContent("Deuteranopia", "The texture to be used for Deuteranopia color-blindness."), colorBlindRawImage.deuteranopiaTexture, typeof(Texture), false);
					colorBlindRawImage.tritanopiaTexture = (Texture)EditorGUILayout.ObjectField(new GUIContent("Tritanopia", "The texture to be used for Tritanopia color-blindness."), colorBlindRawImage.tritanopiaTexture, typeof(Texture), false);
				}
			}
		}
	}
}