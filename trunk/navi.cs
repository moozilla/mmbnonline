/*
 * MMBNO navi class
 * 
 * Created: 10/6/2006 at 9:04 PM by Spikeman
 * 
 */
/*
 * Edited: 10/10/2006 at 2:15 AM by by Nare
 * 
 * Changed the contructor to parse the skin file automatically por the navi data
 * 
 */
 
using System;
using System.Drawing;
using System.IO;

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
		private bool naviHFlip = false; //default to false
		
		private int naviWidth;
		private int naviHeight;
		private int naviNumFrames;
		
		private Image naviImage;
		
		public navi(string filename, string appPath) //constructor
		{
			StreamReader sr = new StreamReader(appPath + "\\" + filename);
			string line;
			line = sr.ReadLine();
			line = sr.ReadLine();
			line = line.Substring(9);
			naviImage = Image.FromFile(appPath + "\\" + line);
			line = sr.ReadLine();
			naviHeight=int.Parse(line.Substring(7));
			line = sr.ReadLine();
			naviWidth=int.Parse(line.Substring(6));
			line = sr.ReadLine();
			naviNumFrames=int.Parse(line.Substring(7));
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
		
		public bool hFlip
		{
			get { return naviHFlip; }
			set { naviHFlip = value; }
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
		
		public Image image
		{
			get { return naviImage; }
			set { naviImage = value; }
		}
		
		public void draw(Graphics g)
		{
			Rectangle rect;
			if (hFlip)
			{rect = new Rectangle(x + width,y,-width,height);}
			//When the width is negative, the image is flipped horizontally the naviWidth is added to the
			//horizontal position so it flips in it's place (otherwise it would flip on the border of the old rectangle)
			else
			{rect = new Rectangle(x,y,width,height);}
			g.DrawImage(image,rect,frame*width,dir*height,width,height,GraphicsUnit.Pixel);
		}

	}
}
