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
using System.Collections.Generic;
using System.Windows.Forms;

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
		private int naviStandingFrames;
		
		private Image naviImage;
		
		public navi(string path, string filename) //constructor
		{
			if(!loadNavi(path + "\\" + filename))
				throw new System.Exception("Invalid skin");
		}
		
		public navi(string filename) //full filename
		{
			if(!loadNavi(filename))
				throw new System.Exception("Invalid skin");
		}
		
		private bool loadNavi(string filename)
		{
			//parses file into a dictionary so anything later added to skins can be added very easily
			
			StreamReader sr;
			try {
				sr = new StreamReader(filename);
			} catch {
				//MessageBox.Show("Skin not found.","Skin not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			string line;
			Dictionary<string, string> vals = new Dictionary<string, string>(); //store data from file in dictionary
				
			while ((line = sr.ReadLine()) != null) {
				if(!line.StartsWith("//")) //only gets lines that aren't comments
				{
					int t = line.IndexOf("=");
					string key = line.Substring(0,t).ToLower(); //key name is on left of equal sign
					string val = line.Substring(t+1); //value name on right of equal sign
					vals.Add(key,val);
				}
			}
			sr.Close();
			
			string path = filename.Substring(0,filename.LastIndexOf("\\"));
			
			try {
				naviWidth = Convert.ToInt32(vals["width"]);
				naviHeight = Convert.ToInt32(vals["height"]);
				naviNumFrames = Convert.ToInt32(vals["frames"]);
				naviImage = Image.FromFile(path + "\\" + vals["filename"]);
			} catch {
				MessageBox.Show("Required value not found in skin file.","Missing value", MessageBoxButtons.OK,MessageBoxIcon.Error);
				return false;
			}
			naviStandingFrames = vals.ContainsKey("standingframes") ? Convert.ToInt32(vals["standingframes"]) : 1; //default 1
			return true; //success
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
		
		public int standingFrames
		{
			get { return naviStandingFrames; }
			set { naviStandingFrames = value; }
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
