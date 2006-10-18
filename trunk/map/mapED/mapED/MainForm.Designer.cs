/*
 * (Program Name Here)
 * 
 * Created: 10/17/2006 at 6:03 PM
 * 
 */
namespace mapED
{
	partial class MainForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.pTiles = new System.Windows.Forms.Panel();
			this.gTiles = new System.Windows.Forms.GroupBox();
			this.tileScroll = new System.Windows.Forms.HScrollBar();
			this.openTiles = new System.Windows.Forms.OpenFileDialog();
			this.bLoadTiles = new System.Windows.Forms.Button();
			this.bLoadMap = new System.Windows.Forms.Button();
			this.gMap = new System.Windows.Forms.GroupBox();
			this.pMapScroll = new System.Windows.Forms.Panel();
			this.pMap = new System.Windows.Forms.Panel();
			this.gMapData = new System.Windows.Forms.GroupBox();
			this.tMapData = new System.Windows.Forms.TextBox();
			this.openMap = new System.Windows.Forms.OpenFileDialog();
			this.bGenMapData = new System.Windows.Forms.Button();
			this.gTiles.SuspendLayout();
			this.gMap.SuspendLayout();
			this.pMapScroll.SuspendLayout();
			this.gMapData.SuspendLayout();
			this.SuspendLayout();
			// 
			// pTiles
			// 
			this.pTiles.BackColor = System.Drawing.Color.Fuchsia;
			this.pTiles.Location = new System.Drawing.Point(6, 20);
			this.pTiles.Name = "pTiles";
			this.pTiles.Size = new System.Drawing.Size(304, 8);
			this.pTiles.TabIndex = 0;
			// 
			// gTiles
			// 
			this.gTiles.Controls.Add(this.tileScroll);
			this.gTiles.Controls.Add(this.pTiles);
			this.gTiles.Location = new System.Drawing.Point(12, 12);
			this.gTiles.Name = "gTiles";
			this.gTiles.Size = new System.Drawing.Size(318, 46);
			this.gTiles.TabIndex = 1;
			this.gTiles.TabStop = false;
			this.gTiles.Text = "Tiles";
			// 
			// tileScroll
			// 
			this.tileScroll.Location = new System.Drawing.Point(6, 32);
			this.tileScroll.Name = "tileScroll";
			this.tileScroll.Size = new System.Drawing.Size(304, 10);
			this.tileScroll.TabIndex = 1;
			this.tileScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.TileScrollScroll);
			// 
			// openTiles
			// 
			this.openTiles.Filter = "PNG Images|*.PNG|All Files|*.*";
			// 
			// bLoadTiles
			// 
			this.bLoadTiles.Location = new System.Drawing.Point(12, 64);
			this.bLoadTiles.Name = "bLoadTiles";
			this.bLoadTiles.Size = new System.Drawing.Size(75, 23);
			this.bLoadTiles.TabIndex = 2;
			this.bLoadTiles.Text = "Load Tiles";
			this.bLoadTiles.UseVisualStyleBackColor = true;
			this.bLoadTiles.Click += new System.EventHandler(this.BLoadTilesClick);
			// 
			// bLoadMap
			// 
			this.bLoadMap.Location = new System.Drawing.Point(93, 64);
			this.bLoadMap.Name = "bLoadMap";
			this.bLoadMap.Size = new System.Drawing.Size(96, 23);
			this.bLoadMap.TabIndex = 3;
			this.bLoadMap.Text = "Load Map Image";
			this.bLoadMap.UseVisualStyleBackColor = true;
			this.bLoadMap.Click += new System.EventHandler(this.BLoadMapClick);
			// 
			// gMap
			// 
			this.gMap.Controls.Add(this.pMapScroll);
			this.gMap.Location = new System.Drawing.Point(12, 93);
			this.gMap.Name = "gMap";
			this.gMap.Size = new System.Drawing.Size(177, 128);
			this.gMap.TabIndex = 4;
			this.gMap.TabStop = false;
			this.gMap.Text = "Map Image";
			// 
			// pMapScroll
			// 
			this.pMapScroll.AutoScroll = true;
			this.pMapScroll.AutoScrollMinSize = new System.Drawing.Size(155, 92);
			this.pMapScroll.Controls.Add(this.pMap);
			this.pMapScroll.Location = new System.Drawing.Point(6, 20);
			this.pMapScroll.Name = "pMapScroll";
			this.pMapScroll.Size = new System.Drawing.Size(165, 102);
			this.pMapScroll.TabIndex = 1;
			// 
			// pMap
			// 
			this.pMap.BackColor = System.Drawing.Color.Fuchsia;
			this.pMap.Location = new System.Drawing.Point(3, 3);
			this.pMap.Name = "pMap";
			this.pMap.Size = new System.Drawing.Size(159, 96);
			this.pMap.TabIndex = 0;
			// 
			// gMapData
			// 
			this.gMapData.Controls.Add(this.bGenMapData);
			this.gMapData.Controls.Add(this.tMapData);
			this.gMapData.Location = new System.Drawing.Point(195, 64);
			this.gMapData.Name = "gMapData";
			this.gMapData.Size = new System.Drawing.Size(135, 157);
			this.gMapData.TabIndex = 5;
			this.gMapData.TabStop = false;
			this.gMapData.Text = "Map Data";
			// 
			// tMapData
			// 
			this.tMapData.Location = new System.Drawing.Point(6, 20);
			this.tMapData.Multiline = true;
			this.tMapData.Name = "tMapData";
			this.tMapData.Size = new System.Drawing.Size(121, 102);
			this.tMapData.TabIndex = 0;
			// 
			// openMap
			// 
			this.openMap.Filter = "PNG Images|*.PNG|All Files|*.*";
			// 
			// bGenMapData
			// 
			this.bGenMapData.Location = new System.Drawing.Point(6, 128);
			this.bGenMapData.Name = "bGenMapData";
			this.bGenMapData.Size = new System.Drawing.Size(121, 23);
			this.bGenMapData.TabIndex = 1;
			this.bGenMapData.Text = "Generate";
			this.bGenMapData.UseVisualStyleBackColor = true;
			this.bGenMapData.Click += new System.EventHandler(this.BGenMapDataClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(342, 233);
			this.Controls.Add(this.gMapData);
			this.Controls.Add(this.gMap);
			this.Controls.Add(this.bLoadMap);
			this.Controls.Add(this.bLoadTiles);
			this.Controls.Add(this.gTiles);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "MainForm";
			this.Text = "mapED";
			this.gTiles.ResumeLayout(false);
			this.gMap.ResumeLayout(false);
			this.pMapScroll.ResumeLayout(false);
			this.gMapData.ResumeLayout(false);
			this.gMapData.PerformLayout();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button bGenMapData;
		private System.Windows.Forms.Panel pMapScroll;
		private System.Windows.Forms.OpenFileDialog openMap;
		private System.Windows.Forms.TextBox tMapData;
		private System.Windows.Forms.GroupBox gMapData;
		private System.Windows.Forms.Panel pMap;
		private System.Windows.Forms.GroupBox gMap;
		private System.Windows.Forms.Button bLoadMap;
		private System.Windows.Forms.HScrollBar tileScroll;
		private System.Windows.Forms.Button bLoadTiles;
		private System.Windows.Forms.OpenFileDialog openTiles;
		private System.Windows.Forms.GroupBox gTiles;
		private System.Windows.Forms.Panel pTiles;
	}
}
