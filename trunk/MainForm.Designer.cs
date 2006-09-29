/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 6/4/2006
 * Time: 5:06 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace MMBNO
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.pnlMap = new System.Windows.Forms.Panel();
			this.pnlNavi = new System.Windows.Forms.Panel();
			this.frameTimer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// pnlMap
			// 
			this.pnlMap.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlMap.BackgroundImage")));
			this.pnlMap.Location = new System.Drawing.Point(12, 12);
			this.pnlMap.Name = "pnlMap";
			this.pnlMap.Size = new System.Drawing.Size(28, 31);
			this.pnlMap.TabIndex = 0;
			this.pnlMap.Visible = false;
			// 
			// pnlNavi
			// 
			this.pnlNavi.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlNavi.BackgroundImage")));
			this.pnlNavi.Location = new System.Drawing.Point(56, 12);
			this.pnlNavi.Name = "pnlNavi";
			this.pnlNavi.Size = new System.Drawing.Size(47, 43);
			this.pnlNavi.TabIndex = 1;
			this.pnlNavi.Visible = false;
			// 
			// frameTimer
			// 
			this.frameTimer.Enabled = true;
			this.frameTimer.Interval = 41;
			this.frameTimer.Tick += new System.EventHandler(this.FrameTimerTick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(240, 160);
			this.Controls.Add(this.pnlNavi);
			this.Controls.Add(this.pnlMap);
			this.Name = "MainForm";
			this.Text = "MMBNO";
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Timer frameTimer;
		private System.Windows.Forms.Panel pnlNavi;
		private System.Windows.Forms.Panel pnlMap;
}
	}
	
