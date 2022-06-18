using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectStranded
{
	public struct CellDrawParams
	{
		//Change rendering color to a shade around this var (do not shade if ShadeAround == Color.White)
		public Color ShadeAround;
		public Random random;
		public static int ShadeRange = 1;

		public CellDrawParams(Color? shade, Random r)
		{
			ShadeAround = shade is null ? Color.White : (Color)shade;
			random = r;
		}

		public Color GetShadeAround()
		{
			if (ShadeAround == Color.White)
				return ShadeAround;

			Vector3 shade;//= ShadeAround.ToVector3() + new Vector3(random.Next(-ShadeRange, ShadeRange), random.Next(-ShadeRange, ShadeRange), random.Next(-ShadeRange, ShadeRange));
			shade = ShadeAround.ToVector3() / ((float)random.NextDouble() * 0.5f + 1);


			return new Color(shade);
		}
	}
	class GridCell
	{
		public string Name;
		public bool Collision;
		public string CellType;
		public CellDrawParams DrawParams;
		public Color CellColor;

		public GridCell(string name, bool collision, string celltype, CellDrawParams drawparams)
		{
			DrawParams = drawparams;
			CellColor = DrawParams.GetShadeAround();
			Name = name;
			Collision = collision;
			CellType = celltype;
		}

		public GridCell Clone()
		{
			return new GridCell(Name, Collision, CellType, DrawParams);
		}

		public void Draw(SpriteBatch sb, Atlas atlas, Vector2 offset)
		{
			atlas.Draw(sb, Name, offset, CellColor);
		}
	}
}
