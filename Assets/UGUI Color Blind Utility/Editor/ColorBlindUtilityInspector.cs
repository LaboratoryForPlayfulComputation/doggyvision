using UnityEngine;
using UnityEditor;

namespace ColorBlindUtility.UGUI 
{
	[CanEditMultipleObjects]
	[CustomEditor(typeof(ColorBlindUtility), true)]
	public class UIColorBlindUtilityInspector : Editor 
	{
		private ColorBlindUtility colorBlindUtility;
		
		void Awake() 
		{
			colorBlindUtility = (ColorBlindUtility)target;
		}
		
		public override void OnInspectorGUI()
		{
			colorBlindUtility.colorBlindMode = (ColorBlindMode)EditorGUILayout.EnumPopup("Color-Blind Mode", colorBlindUtility.colorBlindMode);
			colorBlindUtility.SetColorBlindMode(colorBlindUtility.colorBlindMode);
		}
	}
}