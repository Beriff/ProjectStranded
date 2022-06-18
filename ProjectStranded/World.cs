using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectStranded
{
	class Chunk
	{
		public static int ChunkSizeX = 32;
		public static int ChunkSizeY = 32;

		public GridCell[,] Grid;

		public Chunk()
		{
			Grid = new GridCell[ChunkSizeX, ChunkSizeY];
		}
		public Chunk debug_fill(GridCell cell)
		{
			Util.foreachXY(ChunkSizeX, ChunkSizeY, (int x, int y) =>
			{
				Grid[x, y] = cell.Clone();
			});
			return this;
		}
		public void foreachCell(Action<Chunk, int,int> filler)
		{
			for(int y = 0; y < ChunkSizeY; y++)
			{
				for(int x = 0; x < ChunkSizeX; x++)
				{
					filler(this, x, y);
				}
			}
		}
		public void Draw(SpriteBatch sb, Atlas atlas, Vector2 offset)
		{
			Util.foreachXY(ChunkSizeX, ChunkSizeY, (int x, int y) =>
			{
				Grid[x, y].Draw(sb, atlas, offset + new Vector2(x * atlas.CellLengthX, y * atlas.CellLengthY));
			});
		}
	}
	struct Camera
	{
		public int x;
		public int y;
		public int w;
		public int h;
		public Camera(int x = 0, int y = 0, int w = 0, int h = 0)
		{
			this.x = x;
			this.y = y;
			this.w = w;
			this.h = h;
		}
		public Camera(Viewport vp, int x = 0, int y = 0) : this(x, y, vp.Width, vp.Height) { }
		public void SetCenter(int cx, int cy)
		{
			x = cx - w / 2;
			y = cy - h / 2;
		}

		public Vector2 GetOffset()
		{
			return new Vector2(x, y);
		}
	}
	class World
	{
		public Dictionary<Vector2, Chunk> ChunkGrid;
		public Camera MainCamera;

		public void Draw(SpriteBatch sb, Atlas atlas)
		{
			int pix_width = Chunk.ChunkSizeX * atlas.CellLengthX;
			int pix_height = Chunk.ChunkSizeY * atlas.CellLengthY;
			foreach (KeyValuePair<Vector2, Chunk> kp in ChunkGrid)
			{
				kp.Value.Draw(sb, atlas, MainCamera.GetOffset() + kp.Key * new Vector2(pix_width, pix_height));
			}
		}

		public World()
		{
			MainCamera = new Camera();
			ChunkGrid = new Dictionary<Vector2, Chunk>();
			ChunkGrid[Vector2.Zero] = new Chunk().debug_fill(new GridCell("grass", false, "floor", new CellDrawParams(Color.LightGreen, new Random())));
			ChunkGrid[Vector2.UnitX] = new Chunk().debug_fill(new GridCell("farmland", false, "floor", new CellDrawParams(Color.White, new Random())));
		}
	}
}
