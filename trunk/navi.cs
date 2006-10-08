/*
 * MMBNO navi class
 * 
 * Created: 10/6/2006 at 9:04 PM by Spikeman
 * 
 */

using System;

namespace MMBNO
{
	/// <summary>
	/// The class for navis.
	/// </summary>

	public class navi
	{
		private int naviX;
		private int naviY;
		private int naviDir;
		private int naviFrame;
		
		private int naviWidth;
		private int naviHeight;
		private int naviNumFrames;
		
		private System.Drawing.Image naviImage;
		
		public navi(int naviWidth, int naviHeight, int naviNumFrames, System.Drawing.Image naviImage) //constructor
		{
			this.naviWidth = naviWidth;
			this.naviHeight = naviHeight;
			this.naviNumFrames = naviNumFrames;
			this.naviImage = naviImage;
		}
		
		public int x
		{
			get { return naviX; }
			set { naviX = value; }
		}
		
		public int y
		{
			get { return naviY; }
			set { naviY = value; }
		}
		
		public int dir
		{
			get { return naviDir; }
			set { naviDir = value; }
		}
		
		public int frame
		{
			get { return naviFrame; }
			set { naviFrame = value; }
		}
		
		public int width
		{
			get { return naviWidth; }
			set { naviWidth = value; } //should this be read only?
		}
		
		public int height
		{
			get { return naviHeight; }
			set { naviHeight = value; }
		}
		
		public int numFrames
		{
			get { return naviNumFrames; }
			set { naviNumFrames = value; }
		}
		
		public System.Drawing.Image image
		{
			get { return naviImage; }
			set { naviImage = value; }
		}
	}
}
