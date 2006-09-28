/*
 * Created with SharpDevelop by Spikeman
 * Todo:
 * - Put timer so animation is at right speed, and one so he goes back to standing right
 * - Fix skin, make sure animations are same as in game
 * - Better animation (releastic frames)
 * - Diagonally movement
 * - Fix flicker
 */

/*
 * Edits with SharpDevelop by Nare
 * date: 24/September/2006
 * Changes/fixes:
 * - Key wrapping is now made by an activity hook instead of the Form
 * - Made the Navi flip when walking to the left
 * - Stands still when stop moving
 * 
 * Bugs to fix:
 * - The key hook is working on global status, that means it takes keys even outside the game window (soon to be fixed/changed)
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using gma.System.Windows;

namespace MMBNO
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm
	{
		[STAThread]
		public static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
		
		private int mapOffsetX; // have to declare global variables here so they can be
		private int mapOffsetY; // be used anywhere in the code later
		
		private int naviX;
		private int naviY;
		private int naviDir;
		private int naviFrame;
		private int widthToPass; //This will be passed to the DrawImage function instead of the naviWith to allow the flip
		
		private int naviWidth;
		private int naviHeight;
		private int naviNumFrames;

		UserActivityHook keyHook; // this creates a structure that it's only job is to catch the key that are pressed
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			mapOffsetX = 0; //initial values of the global vars
			mapOffsetY = 0;
			
			naviX = 10;
			naviY = 100;
			naviDir = 0;
			naviFrame = 0;
			naviWidth = 27; //the skin file would say this
			widthToPass = naviWidth;
			naviHeight = 40;
			naviNumFrames = 6; //doesn't count standing still
			keyHook= new UserActivityHook();
			keyHook.KeyDown+=new KeyEventHandler(MyKeyDown);
			keyHook.KeyUp+=new KeyEventHandler(MyKeyUp);
		}
		
		protected override void OnPaint(PaintEventArgs pe)
		{
			// draws map and navi whenever a window is refreshed
			Graphics g = pe.Graphics;
			g.Clear(Color.Black);
			drawMap(g);
			drawNavi(g);
		}
		
		
		public void MyKeyDown(object sender, KeyEventArgs e)
		{
			switch(e.KeyCode) {
				//debug keys
				case Keys.NumPad4:
					mapOffsetX-=10;
					break;
				case Keys.NumPad6:
					mapOffsetX+=10;
					break;
				case Keys.NumPad8:
					mapOffsetY-=10;
					break;
				case Keys.NumPad2:
					mapOffsetY+=10;
					break;
				case Keys.OemMinus:
					naviDir--;
					break;
				case Keys.Oemplus:
					naviDir++;
					break;
				case Keys.OemOpenBrackets: //[
					naviFrame--;
					break;
				case Keys.Oem6: //]
					naviFrame++;
					break;
				//end debug
				case Keys.Left:
					naviX--;
					naviDir=2;
					widthToPass = -naviWidth; //makes the navi looks to the left
					naviFrame++;
					break;
				case Keys.Right:
					naviX++;
					naviDir=2;
					naviFrame++;
					widthToPass = naviWidth;
					break;
				case Keys.Up:
					naviY--;
					naviDir=4;
					naviFrame++;
					break;
				case Keys.Down:
					naviY++;
					naviDir=0;
					naviFrame++;
					break;
				default:
					break;
			}
			if (naviFrame>naviNumFrames)
				naviFrame = 1;
			Graphics g = this.CreateGraphics();
			drawMap(g);
			drawNavi(g);
		}
		public void MyKeyUp(object sender, KeyEventArgs e)
		{
				naviFrame = 0;
			Graphics g = this.CreateGraphics();
			drawMap(g);
			drawNavi(g);
		}
		private void drawMap(Graphics g)
		{
			//figured out these by looking at the sdk
			Image mapImg = pnlMap.BackgroundImage;
			Rectangle rect = new Rectangle(0,0,240,160);
			g.DrawImage(mapImg,rect,mapOffsetX,mapOffsetY,240,160,GraphicsUnit.Pixel);
		}
		private void drawNavi(Graphics g)
		{
			Rectangle rect;
			Image naviImg = pnlNavi.BackgroundImage;
			//Rectangle rect = new Rectangle(0,0,naviWidth-1,naviHeight-1);
			//g.DrawImage(naviImg,naviX,naviY,rect,GraphicsUnit.Pixel);
			if (widthToPass<0)
			{rect = new Rectangle(naviX + naviWidth,naviY,widthToPass,naviHeight);} //When the width is negative, the image is flipped horizontally
																								//the naviWidth is added to the horizontal position so it flips in it's place (otherwise it would flip on the border of the old rectangle)
			else
			{rect = new Rectangle(naviX,naviY,widthToPass,naviHeight);}
			g.DrawImage(naviImg,rect,naviFrame*naviWidth,naviDir*naviHeight,naviWidth,naviHeight,GraphicsUnit.Pixel);
		}
	}
}
