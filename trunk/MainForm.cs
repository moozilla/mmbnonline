/*
 * Created with SharpDevelop by Spikeman
 * 
 * Todo:
 * - Animate map/background
 * - Collision
 * - Multiple layers on maps
 * - Objects on map
 * - Mulitple levels on maps
 * - Online functionality
 * - Battle stuff
 *
 # [10/12/06]
 * - Changed map.. now is BN6 and better for testing
 * - Added running - hold down "D"
 *   - Should later add functionality for user to set keys
 # [10/11/06]
 * - Made perfect skin for BN6 Megaman. Works perfectly.
 * - Rewrote skin parser for future work
 * - Coming next: map stuff!
 # [October 9, 2006]
 * - Replaced widthToPass with hFlip in navi class
 * - Moved drawNavi to navi.draw (now it will be able to draw multiple navis)
 * - Started working on parsing skins (probably will have a mmbno.ini file which stores directory for skin file)
 *   - right now skin.txt must be in same directory as the app
 # [October 6, 2006]:
 * - Added navi class, made code work with it
 # [September 28, 2006]:
 * - Fixed framesBeforeUpdate error in merge
 * - Changed interval on timer to be a bit more accurate (it was too slow before..)
 * - Added drawing buffer.. fixed flicker! It looks SO much better now!
 * 
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
using System.IO;

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
		
		private MMBNO.navi userNavi; //the users navi
		
		private int framesBeforeUpdate; //how many frames waits until passing to the next animation image
		
		private int mapOffsetX; // have to declare global variables here so they can
		private int mapOffsetY; // be used anywhere in the code later
		
		private bool isRunning;
		private bool isStanding;
		private int hMove; //tells direction it mores horizontally 0=no movement 1=left 2=right
		private int vMove; //tells direction it mores horizontally 0=no movement 1=up 2=down

		private BufferedGraphics gBuffer; //creates a buffer for graphics to be drawn to
										  //it is then drawn to the screen all at once, this stops flickering
		private UserActivityHook keyHook; // this creates a structure that it's only job is to catch the key that are pressed
		
		private string appPath = Application.StartupPath;
		private string skinFile = "skin2.txt";
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//parseSkin(skinFile);
			
			mapOffsetX = -25; //initial values of the global vars
			mapOffsetY = 375;
			
			userNavi = new navi(appPath, skinFile); //initialize navi
			
			/* == this info is now in the navi class ==
			naviWidth = 27; //the skin file would say this
			naviHeight = 40;
			naviNumFrames = 6; //doesn't count standing still
			*/
			
			//this two next lines center the navi on the screen
			//changed how it was centered so it matched with the gba version exactly
			userNavi.x = 133 - userNavi.width;
			userNavi.y = 87 - userNavi.height;
			userNavi.dir = 0; //direction the navi is facing
			userNavi.frame = 0; //frame of the navi sheet that will be displayed
			
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
			// draws whenever the window is refreshed
			gBuffer.Graphics.Clear(Color.Black); //black as blackground for now
			drawMap(gBuffer.Graphics); //draw the map
			userNavi.draw(gBuffer.Graphics); //draw the user's navi
			//draw other navis
			gBuffer.Render(pe.Graphics); //draw buffer to window
		}
		
		
		public void MyKeyDown(object sender, KeyEventArgs e)
		{
			switch(e.KeyCode) {
				//debug keys
				case Keys.Back:		//backspace
				System.Diagnostics.Trace.WriteLine("x,y:"+mapOffsetX+","+mapOffsetY);
				break;
				//end debug
				case Keys.D:
					isRunning = true;
					break;
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
					if(hMove==1)
					{
						hMove=0;
						if(vMove==0){isStanding=true;}
					}
					break;
				case Keys.Right:
					if(hMove==2)
					{
						hMove=0;
						if(vMove==0){isStanding=true;}
					}
					break;
				case Keys.Up:
					if(vMove==1)
					{
						vMove=0;
						if(hMove==0){isStanding=true;}
					}
					break;
				case Keys.Down:
					if(vMove==2)
					{
						vMove=0;
						if(hMove==0){isStanding=true;}
					}
					break;
				case Keys.D:
					isRunning = false;
					break;
			}
			if(isStanding==true) {framesBeforeUpdate = 0;}
		}
		private void drawMap(Graphics g)
		{
			Image mapImg = pnlMap.BackgroundImage;
			Rectangle rect = new Rectangle(0,0,240,160);
			g.DrawImage(mapImg,rect,mapOffsetX,mapOffsetY,240,160,GraphicsUnit.Pixel);
		}

		private void FrameTimerTick(object sender, System.EventArgs e)
		{
			if(framesBeforeUpdate>0) {
				if (hMove==1)
				{
					userNavi.dir=2;
					mapOffsetX-=isRunning ? 4 : 2; //moves the map, not the navi, this creates the illusion of movement
					userNavi.hFlip = true; //makes the navi looks to the left
				}
				else if(hMove==2)
				{
					userNavi.dir=2;
					mapOffsetX+=isRunning ? 4 : 2;
					userNavi.hFlip = false;
				}
				if(vMove==1)
				{
					userNavi.dir=4;
					mapOffsetY-=isRunning ? 4 : 2;
				}
				else if(vMove==2)
				{	
					userNavi.dir=0;
					mapOffsetY+=isRunning ? 4 : 2;
				}
				if(hMove!=0&&vMove==2) {
					userNavi.dir=1;
					mapOffsetY-=isRunning ? 2 : 1;
				}
				if(hMove!=0&&vMove==1) {
					userNavi.dir=3;
					mapOffsetY+=isRunning ? 2 : 1;
				}
				framesBeforeUpdate--;
			}
			else
			{
				if(!isStanding)	//don't animate if standing still
				{
					userNavi.frame++;
					if (userNavi.frame>userNavi.numFrames)
						userNavi.frame = 1;
						framesBeforeUpdate=6;
				} else {
					userNavi.frame=0;
				}
			}
			Graphics g = this.CreateGraphics();
			this.OnPaint(new PaintEventArgs(g,this.ClientRectangle));
			g.Dispose();
		}
		
		//the next process was moved to the navi class
		/*private void parseSkin(string filename)
		{
			StreamReader sr = new StreamReader(Application.StartupPath + "\\" + filename);
			string line;
			while ((line = sr.ReadLine()) != null) {
				if(!line.StartsWith("//"))
				   System.Diagnostics.Trace.WriteLine(line);
			}
		}*/
		
	}
}
