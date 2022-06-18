using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectStranded
{
	class Atlas
	{
		public short CellLengthX;
		public short CellLengthY;
		public Texture2D MainTexture;
		public Dictionary<string, Vector2> Offsets;
		public Atlas(Texture2D texture, string settings)
		{
			MainTexture = texture;
			Offsets = new Dictionary<string, Vector2>();

			string[] parameters = settings.Split(' ');
			
			CellLengthX = Int16.Parse(parameters[0]);
			CellLengthY = Int16.Parse(parameters[1]);
			var rowlength = texture.Width / CellLengthX;
			var rowcount = texture.Height / CellLengthY;


			Util.foreachXY(rowlength, rowcount, (int x, int y) =>
			{
				Offsets.Add(parameters[y * rowlength + x + 2], new Vector2(x * CellLengthX, y * CellLengthY));
			});
		}
		public void Draw(SpriteBatch sb, string name, Vector2 offset, Color color)
		{
			sb.Draw(MainTexture, offset, new Rectangle(Offsets[name].ToPoint(), new Point(CellLengthX, CellLengthY)), color, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
		}
	}
}
