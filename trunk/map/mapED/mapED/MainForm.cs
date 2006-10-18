/*
 * (Program Name Here)
 * 
 * Created: 10/17/2006 at 6:03 PM
 * 
 */

using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace mapED
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm
	{
		Collection<Image> tiles = new Collection<Image>();
		int tileOffset = 0;
		
		bool tilesLoaded = false;
		bool mapLoaded = false;
		
		Image mapImage;
		
		[STAThread]
		public static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
		}
		
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if(tilesLoaded) updateTiles();
		}
		
		void BLoadTilesClick(object sender, System.EventArgs e)
		{
			if(openTiles.ShowDialog()==DialogResult.OK) {
				Image temp = Image.FromFile(openTiles.FileName);
				for(int i=0;i<temp.Width;i+=8) {
					Image tmp = new Bitmap(8,8);
					Graphics.FromImage(tmp).DrawImage(temp,new Rectangle(0,0,8,8),new Rectangle(i,0,8,8), GraphicsUnit.Pixel);
					tiles.Add(tmp);
				}
				if(tiles.Count>29) {
					tileScroll.Maximum = tiles.Count-29;
				} else {
					tileScroll.Enabled = false;
				}
				tilesLoaded = true;
				updateTiles();
			}
		}
		
		void updateTiles()
		{
			Graphics g = pTiles.CreateGraphics();
			g.Clear(pTiles.BackColor);
			for(int i=0;i<38;i++) {
				g.DrawImage(tiles[i+tileOffset],8*i,0,8,8);
			}
			g.Dispose();
		}
		
		void TileScrollScroll(object sender, System.Windows.Forms.ScrollEventArgs e)
		{
			tileOffset = tileScroll.Value;
			updateTiles();
		}
		
		void BLoadMapClick(object sender, System.EventArgs e)
		{
			if(openMap.ShowDialog()==DialogResult.OK)
			{
				mapImage = Image.FromFile(openMap.FileName);
				pMap.BackgroundImage = mapImage;
				pMap.Size = mapImage.Size;
				mapLoaded = true;
			}
		}
		
		void BGenMapDataClick(object sender, System.EventArgs e)
		{
			if(tilesLoaded&&mapLoaded)
			{
				for(int i=0;i<mapImage.Width;i+=8) {
					Image tmp = new Bitmap(8,8);
					Graphics.FromImage(tmp).DrawImage(mapImage,new Rectangle(0,0,8,8),new Rectangle(i,0,8,8), GraphicsUnit.Pixel); //piece of map image
					int idx = tiles.IndexOf(tmp);
					if(idx==-1) {
						Image temp = (Image)tmp.Clone();
						System.Diagnostics.Trace.WriteLine(tmp.PropertyItems==temp.PropertyItems);
						//tmp.Save(Application.StartupPath + "\\test"+i+".bmp");
						//MessageBox.Show("Piece not found in tiles.","Bad tiles", MessageBoxButtons.OK, MessageBoxIcon.Error);
						//return;
					}
					tMapData.Text+=idx+",";
				}
			} else {
				MessageBox.Show("Please load both the tile and the map image.","Load images", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
	}
}
