﻿using AssetManagementBase;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nursia.Utilities;
using System.Collections.Generic;
using System.Reflection;

namespace Nursia
{
	public static class Assets
	{
		private static AssetManager _assetManagerEffects = AssetManager.CreateResourceAssetManager(Assembly, "EffectsSource.FNA");
		private static Effect _colorEffect, _waterEffect, _skyboxEffect;
		private static Effect[] _defaultEffects = new Effect[16];
		private static Texture2D _white, _waterDUDV, _waterNormals;

		private static Assembly Assembly
		{
			get
			{
				return typeof(Assets).Assembly;
			}
		}

		internal static Texture2D White
		{
			get
			{
				if (_white == null)
				{
					_white = new Texture2D(Nrs.GraphicsDevice, 1, 1);
					_white.SetData(new[] { Color.White });
				}

				return _white;
			}
		}

		internal static Effect ColorEffect
		{
			get
			{
				if (_colorEffect != null)
				{
					return _colorEffect;
				}

				_colorEffect = _assetManagerEffects.LoadEffect(Nrs.GraphicsDevice, "ColorEffect.efb");
				return _colorEffect;
			}
		}

		internal static Effect WaterEffect
		{
			get
			{
				if (_waterEffect != null)
				{
					return _waterEffect;
				}

				_waterEffect = _assetManagerEffects.LoadEffect(Nrs.GraphicsDevice, "WaterEffect.efb");
				return _waterEffect;
			}
		}

		internal static Effect SkyboxEffect
		{
			get
			{
				if (_skyboxEffect != null)
				{
					return _skyboxEffect;
				}

				_skyboxEffect = _assetManagerEffects.LoadEffect(Nrs.GraphicsDevice, "SkyboxEffect.efb");
				return _skyboxEffect;
			}
		}

		internal static Texture2D WaterDUDV
		{
			get
			{
				if (_waterDUDV == null)
				{
					using (var stream = Assembly.OpenResourceStream("Resources.Images.waterDUDV.png"))
					{
						_waterDUDV = Texture2D.FromStream(Nrs.GraphicsDevice, stream);
					}
				}

				return _waterDUDV;
			}
		}

		internal static Texture2D WaterNormals
		{
			get
			{
				if (_waterNormals == null)
				{
					using (var stream = Assembly.OpenResourceStream("Resources.Images.waterNormals.png"))
					{
						_waterNormals = Texture2D.FromStream(Nrs.GraphicsDevice, stream);
					}
				}

				return _waterNormals;
			}
		}

		internal static Effect GetDefaultEffect(bool texture, bool lightning, bool skinning, bool clipPlane)
		{
			var key = 0;
			if (texture)
			{
				key |= 1;
			}

			if (lightning)
			{
				key |= 2;
			}

			if (skinning)
			{
				key |= 4;
			}

			if (clipPlane)
			{
				key |= 8;
			}

			if (_defaultEffects[key] != null)
			{
				return _defaultEffects[key];
			}

			var defines = new Dictionary<string, string>();
			if (texture)
			{
				defines["TEXTURE"] = "1";
			}

			if (lightning)
			{
				defines["LIGHTNING"] = "1";
			}

			if (skinning)
			{
				defines["SKINNING"] = "1";
			}

			if (clipPlane)
			{
				defines["CLIP_PLANE"] = "1";
			}

			var result = _assetManagerEffects.LoadEffect(Nrs.GraphicsDevice, "DefaultEffect.efb", defines);
			_defaultEffects[key] = result;

			return result;
		}
	}
}