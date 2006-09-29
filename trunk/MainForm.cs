/*
 * Created with SharpDevelop by Spikeman
 * 
 * Todo:
 * - Better animation (releastic frames)
 * - Diagonally movement
 * - Fix flicker
 * - Fix skin, make sure animations are same as in game
 * 
 * Newset Updates (September 28, 2006):
 * - Fixed framesBeforeUpdate error in merge
 * - Changed interval on timer to be a bit more accurate (it was too slow before..)
 * - Added drawing buffer.. fixed flicker! It looks SO much better now!
 * 
 * Things to be fixed/done:
 * - You move too fast going diagonally and too slow going up or down.. fix this
 * - Fix it so you can switch opposite directions without stopping (left to right, up to down)
 * - Add background
 * - Make map scroll with navi
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
 * 
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
		
		private int framesBeforeUpdate; //how many frames waits until passing to the next animation image
		
		private bool isStanding;
		private int hMove; //tells direction it mores horizontally 0=no movement 1=left 2=right
		private int vMove; //tells direction it mores horizontally 0=no movement 1=up 2=down

		private BufferedGraphics gBuffer; //creates a buffer for graphics to be drawn to
										  //it is then drawn to the screen all at once, this stops flickering
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
			
			framesBeforeUpdate=6; //how many frames each walking step takes
			
			isStanding=true;
			
			keyHook= new UserActivityHook();
			keyHook.KeyDown+=new KeyEventHandler(MyKeyDown);
			keyHook.KeyUp+=new KeyEventHandler(MyKeyUp);
			
			BufferedGraphicsContext gContext = BufferedGraphicsManager.Current; //set up buffer
			gContext.MaximumBuffer = new Size(240,160); //size of screen
			gBuffer = gContext.Allocate(this.CreateGraphics(), new Rectangle(0,0,this.Width,this.Height)); //allocate buffer
		}
		
		protected override void OnPaint(PaintEventArgs pe)
		{
			// draws map and navi whenever a window is refreshed
			Graphics g = pe.Graphics;
			//g.Clear(Color.Black);
			drawMap(gBuffer.Graphics);
			drawNavi(gBuffer.Graphics);
			gBuffer.Render(pe.Graphics); //draw buffer to window
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
				//end debug
				case Keys.Left:
					hMove=1;
					isStanding=false;
					break;
				case Keys.Right:
					hMove=2;
					isStanding=false;
					break;
				case Keys.Up:
					vMove=1;
					isStanding=false;
					break;
				case Keys.Down:
					vMove=2;
					isStanding=false;
					break;
				default:
					break;
			}
		}
		public void MyKeyUp(object sender, KeyEventArgs e)
		{
			switch(e.KeyCode) {
				case Keys.Left:
					hMove=0;
					if(vMove==0){isStanding=true;}
					break;
				case Keys.Right:
					hMove=0;
					if(vMove==0){isStanding=true;}
					break;
				case Keys.Up:
					vMove=0;
					if(hMove==0){isStanding=true;}
					break;
				case Keys.Down:
					vMove=0;
					if(hMove==0){isStanding=true;}
					break;
			}
			if(isStanding==true){framesBeforeUpdate = 2;}
		}
		private void drawMap(Graphics g)
		{
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
		
		void FrameTimerTick(object sender, System.EventArgs e)
		{
			if(framesBeforeUpdate>0) {
				if (hMove==1)
				{
					naviDir=2;
				  	naviX-=2;
					widthToPass = -naviWidth; //makes the navi looks to the left
				}
				else if(hMove==2)
				{
					naviDir=2;
					naviX+=2;
					widthToPass = naviWidth;
				}
				if(vMove==1)
				{
					naviDir=4;
					naviY--;
				}
				else if(vMove==2)
				{	
					naviDir=0;
					naviY++;
				}
				if(hMove>0&&vMove==2) {naviDir=1;}
				if(hMove>0&&vMove==1) {naviDir=3;}
				framesBeforeUpdate--;
			}
			else
			{
				if(!isStanding)	//don't animate if standing still
				{
					naviFrame++;
					if (naviFrame>naviNumFrames)
						naviFrame = 1;
						framesBeforeUpdate=6;
				} else {
					naviFrame=0;
				}
			}
			Graphics g = this.CreateGraphics();
			this.OnPaint(new PaintEventArgs(g,this.ClientRectangle));
			g.Dispose();
		}
	}
}
