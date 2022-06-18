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

		public CellDrawParams(Color? shade, Random r)
		{
			ShadeAround = shade is null ? Color.White : (Color)shade;
			random = r;
		}

		public Color GetShadeAround()
		{
			if (ShadeAround == Color.White)
				return ShadeAround;

			var shade = ShadeAround.ToVector3() + new Vector3(random.Next(-10, 10), random.Next(-10, 10), random.Next(-10, 10));

			if (shade.X > 255)
				shade.X -= random.Next(10);

			if (shade.Y > 255)
				shade.Y -= random.Next(10);

			if (shade.Z > 255)
				shade.Z -= random.Next(10);

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
