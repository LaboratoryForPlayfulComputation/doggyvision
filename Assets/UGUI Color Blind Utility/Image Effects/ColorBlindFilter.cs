/*Note to end user: If you experience stretching 
at the edge of the screen on a Mac you will need 
to go into Player Settings in the Unity Editor, 
uncheck 'Auto Graphics API for Mac' and drag OpenGL2
to the top of the exposed list to fix the issue.*/

using UnityEngine;
using System.Collections;

namespace ColorBlindUtility.UGUI 
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Color-Blind Utility/Color-Blind Filter")]
	public class ColorBlindFilter : MonoBehaviour 
	{
		public Shader filterShader = null;
		protected Material filterMaterial = null;
		protected Material FilterMaterial 
		{
			get 
			{
				if (!filterMaterial) 
				{
					filterMaterial = new Material(filterShader);
					filterMaterial.hideFlags = HideFlags.DontSave;
				}
				return filterMaterial;
			}
		}

		private Matrix4x4 filterMatrix = Matrix4x4.identity;

		public ColorBlindMode colorBlindMode = ColorBlindMode.None;

		private void OnEnable()
		{
			if (!SystemInfo.supportsImageEffects || filterShader && !filterShader.isSupported) 
			{
				enabled = false;
			}
		}

		private void OnDisable() 
		{
			if (filterMaterial) 
			{
				DestroyImmediate(filterMaterial);
			}
		}

		public void OnRenderImage(RenderTexture source, RenderTexture destination) 
		{
			switch (colorBlindMode) 
			{
			case ColorBlindMode.None:
				filterMatrix = Matrix4x4.identity;
				break;
			case ColorBlindMode.Protanopia:
				filterMatrix.SetColumn(0, new Vector4(0.567f, 0.433f, 0.0f, 0.0f));
				filterMatrix.SetColumn(1, new Vector4(0.558f, 0.442f, 0.0f, 0.0f));
				filterMatrix.SetColumn(2, new Vector4(0.0f, 0.242f, 0.758f, 0.0f));
				filterMatrix.SetColumn(3, new Vector4(0.0f, 0.0f, 0.0f, 1.0f));
				break;
			case ColorBlindMode.Deuteranopia:
				filterMatrix.SetColumn(0, new Vector4(0.625f, 0.375f, 0.0f, 0.0f));
				filterMatrix.SetColumn(1, new Vector4(0.7f, 0.3f, 0.0f, 0.0f));
				filterMatrix.SetColumn(2, new Vector4(0.0f, 0.3f, 0.7f, 0.0f));
				filterMatrix.SetColumn(3, new Vector4(0.0f, 0.0f, 0.0f, 1.0f));
				break;
			case ColorBlindMode.Tritanopia:
				filterMatrix.SetColumn(0, new Vector4(0.95f, 0.05f, 0.0f, 0.0f));
				filterMatrix.SetColumn(1, new Vector4(0.0f, 0.433f, 0.567f, 0.0f));
				filterMatrix.SetColumn(2, new Vector4(0.0f, 0.475f, 0.525f, 0.0f));
				filterMatrix.SetColumn(3, new Vector4(0.0f, 0.0f, 0.0f, 1.0f));
				break;
			default:
				break;
			}
			FilterMaterial.SetMatrix("_Filter", filterMatrix);
			Graphics.Blit(source, destination, FilterMaterial);
		}
	}

	public enum ColorBlindMode 
	{ 
		None, 
		Protanopia, 
		Deuteranopia, 
		Tritanopia 
	}
}