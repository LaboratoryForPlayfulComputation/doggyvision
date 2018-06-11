using UnityEngine;
using UnityEditor;

namespace ColorBlindUtility.UGUI 
{
	[CustomEditor(typeof(ColorBlindText), true)]
	public class ColorBlindTextInspector : Editor
	{
		private ColorBlindText colorBlindText;

		private bool colorNormalFoldout = false;
		private bool colorDisabledFoldout = false;
		private bool colorHighlightedFoldout = false;
		private bool colorPressedFoldout = false;
		private bool fontsFoldout = false;

		void Awake() 
		{
			colorBlindText = (ColorBlindText)target;
		}

		public override void OnInspectorGUI() 
		{
			colorBlindText.supportType = (ColorBlindText.SupportType)EditorGUILayout.EnumPopup("Support Type", colorBlindText.supportType);
			if (colorBlindText.supportType == ColorBlindText.SupportType.ChangeColor || colorBlindText.supportType == ColorBlindText.SupportType.ChangeBoth) 
			{
				colorNormalFoldout = EditorGUILayout.Foldout(colorNormalFoldout, (colorBlindText.IsButtonTarget ? "Normal Colors" : "Colors"));
				if (colorNormalFoldout) 
				{
					colorBlindText.protanopiaNormalColor = EditorGUILayout.ColorField(new GUIContent("Protanopia", "The color to be used for Protanopia color-blindness."), colorBlindText.protanopiaNormalColor);
					colorBlindText.deuteranopiaNormalColor = EditorGUILayout.ColorField(new GUIContent("Deuteranopia", "The color to be used for Deuteranopia color-blindness."), colorBlindText.deuteranopiaNormalColor);
					colorBlindText.tritanopiaNormalColor = EditorGUILayout.ColorField(new GUIContent("Tritanopia", "The color to be used for Tritanopia color-blindness."), colorBlindText.tritanopiaNormalColor);
				}
				if (colorBlindText.IsButtonTarget) 
				{
					colorDisabledFoldout = EditorGUILayout.Foldout(colorDisabledFoldout, "Disabled Colors");
					if (colorDisabledFoldout) 
					{
						colorBlindText.protanopiaDisabledColor = EditorGUILayout.ColorField(new GUIContent("Protanopia", "The color to be used for Protanopia color-blindness."), colorBlindText.protanopiaDisabledColor);
						colorBlindText.deuteranopiaDisabledColor = EditorGUILayout.ColorField(new GUIContent("Deuteranopia", "The color to be used for Deuteranopia color-blindness."), colorBlindText.deuteranopiaDisabledColor);
						colorBlindText.tritanopiaDisabledColor = EditorGUILayout.ColorField(new GUIContent("Tritanopia", "The color to be used for Tritanopia color-blindness."), colorBlindText.tritanopiaDisabledColor);
					}
					colorHighlightedFoldout = EditorGUILayout.Foldout(colorHighlightedFoldout, "Highlight Colors");
					if (colorHighlightedFoldout) 
					{
						colorBlindText.protanopiaHighlightedColor = EditorGUILayout.ColorField(new GUIContent("Protanopia", "The color to be used for Protanopia color-blindness."), colorBlindText.protanopiaHighlightedColor);
						colorBlindText.deuteranopiaHighlightedColor = EditorGUILayout.ColorField(new GUIContent("Deuteranopia", "The color to be used for Deuteranopia color-blindness."), colorBlindText.deuteranopiaHighlightedColor);
						colorBlindText.tritanopiaHighlightedColor = EditorGUILayout.ColorField(new GUIContent("Tritanopia", "The color to be used for Tritanopia color-blindness."), colorBlindText.tritanopiaHighlightedColor);
					}
					colorPressedFoldout = EditorGUILayout.Foldout(colorPressedFoldout, "Pressed Colors");
					if (colorPressedFoldout) 
					{
						colorBlindText.protanopiaPressedColor = EditorGUILayout.ColorField(new GUIContent("Protanopia", "The color to be used for Protanopia color-blindness."), colorBlindText.protanopiaPressedColor);
						colorBlindText.deuteranopiaPressedColor = EditorGUILayout.ColorField(new GUIContent("Deuteranopia", "The color to be used for Deuteranopia color-blindness."), colorBlindText.deuteranopiaPressedColor);
						colorBlindText.tritanopiaPressedColor = EditorGUILayout.ColorField(new GUIContent("Tritanopia", "The color to be used for Tritanopia color-blindness."), colorBlindText.tritanopiaPressedColor);
					}
				}
			}
			if (colorBlindText.supportType == ColorBlindText.SupportType.ChangeFont || colorBlindText.supportType == ColorBlindText.SupportType.ChangeBoth) 
			{
				fontsFoldout = EditorGUILayout.Foldout(fontsFoldout, "Fonts");
				if (fontsFoldout) 
				{
					colorBlindText.protanopiaFont = (Font)EditorGUILayout.ObjectField(new GUIContent("Protanopia", "The font to be used for Protanopia color-blindness."), colorBlindText.protanopiaFont, typeof(Font), false);
					colorBlindText.deuteranopiaFont = (Font)EditorGUILayout.ObjectField(new GUIContent("Deuteranopia", "The font to be used for Deuteranopia color-blindness."), colorBlindText.deuteranopiaFont, typeof(Font), false);
					colorBlindText.tritanopiaFont = (Font)EditorGUILayout.ObjectField(new GUIContent("Tritanopia", "The font to be used for Tritanopia color-blindness."), colorBlindText.tritanopiaFont, typeof(Font), false);
				}
			}
		}
	}
}