using System;
using System.Drawing;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Forms;
using SourceGrid2;
using Cells = SourceGrid2.Cells.Real;

using ObjectExplorer;

namespace CombatLog
{
	/// <summary>
	/// Summary description for OEResults.
	/// </summary>
	public class OEResults : System.Windows.Forms.Form
	{
		public string SearchText;
		public CombatLog.Config.UserConfig MyConfig;

		private System.Windows.Forms.ListView listViewSearchResults;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private SourceGrid2.Grid grid;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.RichTextBox rtbSummary;
		private System.ComponentModel.Container components = null;

		public OEResults()
		{
			InitializeComponent();
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(OEResults));
			this.listViewSearchResults = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.grid = new SourceGrid2.Grid();
			this.panel1 = new System.Windows.Forms.Panel();
			this.rtbSummary = new System.Windows.Forms.RichTextBox();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// listViewSearchResults
			// 
			this.listViewSearchResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																									this.columnHeader1});
			this.listViewSearchResults.Dock = System.Windows.Forms.DockStyle.Top;
			this.listViewSearchResults.FullRowSelect = true;
			this.listViewSearchResults.Location = new System.Drawing.Point(0, 0);
			this.listViewSearchResults.Name = "listViewSearchResults";
			this.listViewSearchResults.Size = new System.Drawing.Size(320, 96);
			this.listViewSearchResults.TabIndex = 0;
			this.listViewSearchResults.View = System.Windows.Forms.View.Details;
			this.listViewSearchResults.ItemActivate += new System.EventHandler(this.listViewSearchResults_ItemActivate);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Item";
			this.columnHeader1.Width = 139;
			// 
			// grid
			// 
			this.grid.AutoSizeMinHeight = 10;
			this.grid.AutoSizeMinWidth = 10;
			this.grid.AutoStretchColumnsToFitWidth = false;
			this.grid.AutoStretchRowsToFitHeight = false;
			this.grid.ContextMenuStyle = SourceGrid2.ContextMenuStyle.None;
			this.grid.CustomSort = false;
			this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grid.GridToolTipActive = true;
			this.grid.Location = new System.Drawing.Point(0, 192);
			this.grid.Name = "grid";
			this.grid.Size = new System.Drawing.Size(320, 262);
			this.grid.SpecialKeys = SourceGrid2.GridSpecialKeys.Default;
			this.grid.TabIndex = 1;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.rtbSummary);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 96);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(320, 96);
			this.panel1.TabIndex = 2;
			// 
			// rtbSummary
			// 
			this.rtbSummary.BackColor = System.Drawing.SystemColors.Info;
			this.rtbSummary.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtbSummary.Location = new System.Drawing.Point(0, 0);
			this.rtbSummary.Name = "rtbSummary";
			this.rtbSummary.ReadOnly = true;
			this.rtbSummary.Size = new System.Drawing.Size(320, 96);
			this.rtbSummary.TabIndex = 0;
			this.rtbSummary.Text = "richTextBox1";
			// 
			// OEResults
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(320, 454);
			this.Controls.Add(this.grid);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.listViewSearchResults);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "OEResults";
			this.ShowInTaskbar = false;
			this.Text = "EVE-I Object Explorer Results";
			this.Load += new System.EventHandler(this.OEResults_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void OEResults_Load(object sender, System.EventArgs e)
		{
			if ( SearchText.Length > 0 )
				DoSearch();
		}

		private void DoSearch()
		{
			ObjectExplorer.OEParser Parser = new ObjectExplorer.OEParser();
			ObjectExplorer.SearchResult[] Results = new ObjectExplorer.SearchResult[1];

			try
			{
				Results = Parser.GetSearchResults(SearchText);
			}
			catch
			{
				// No results
				MessageBox.Show("Nothing found on the EVE-I Object Explorer for '" + SearchText + "'", "Nothing found", MessageBoxButtons.OK);
				return;
			}

			DrawSearchResultSummary(SearchText, Results);
		}

		private int GetExactNameMatch(string SearchItemName, ObjectExplorer.SearchResult[] Results)
		{
			if ( Results.Length == 1 )
				return Results[0].ItemID;

			foreach ( ObjectExplorer.SearchResult sr in Results )
			{
				if ( SearchItemName.Trim().ToLower() == sr.ItemName.Trim().ToLower() )
					return sr.ItemID;
			}

			return -1;
		}

		private void DrawSearchResultSummary(string SearchItemName, ObjectExplorer.SearchResult[] Results)
		{
			listViewSearchResults.Items.Clear();

			if ( Results.Length == 1 || GetExactNameMatch(SearchItemName, Results) != -1 )
			{
				listViewSearchResults.Visible = false;
				DrawItemInfo(GetExactNameMatch(SearchItemName, Results));
				return;
			}

			listViewSearchResults.Visible = true;

			foreach ( ObjectExplorer.SearchResult r in Results )
			{
				ListViewItem l = new ListViewItem(new string[] { r.ItemName });
				l.Tag = r.ItemID;
				listViewSearchResults.Items.Add(l);
			}
		}

		private int GetRowCountForAttributes(ObjectExplorer.ItemAttributeCollection Attributes)
		{
			string GroupName = "";
			int GroupCount = 0;

			foreach ( ObjectExplorer.ItemAttribute i in Attributes )
			{
				if ( GroupName != i.GroupName )
				{
					GroupCount++;
					GroupName = i.GroupName;
				}
			}

			return Attributes.Count + GroupCount + 1;
		}

		private void DrawItemInfo(int ItemID)
		{
			ObjectExplorer.OEParser Parser = new ObjectExplorer.OEParser();

			Parser.GetItemDetail(ItemID);

			int rowCount = GetRowCountForAttributes(Parser.Item.Attributes);

			grid.Redim(rowCount + 2, 3); // +1 For the item summary row

			#region Visual Styles for grid columns
			SourceGrid2.VisualModels.Common l_TitleModel = new SourceGrid2.VisualModels.Common(false);
			l_TitleModel.BackColor = Color.Black;
			l_TitleModel.ForeColor = Color.White;
			l_TitleModel.Border.SetColor(Color.White);
			l_TitleModel.Border.SetWidth(12);
			l_TitleModel.Font = new Font(this.Font, FontStyle.Bold);
			l_TitleModel.TextAlignment = SourceLibrary.Drawing.ContentAlignment.MiddleCenter;

			SourceGrid2.VisualModels.Common l_CaptionModel = new SourceGrid2.VisualModels.Common(false);
			grid.BackColor = this.BackColor;
			l_CaptionModel.BackColor = Color.FromArgb(0xd8, 0xd9, 0xd8);;
			l_CaptionModel.Border.SetColor(Color.White);
			l_CaptionModel.Border.SetWidth(12);
			#endregion

			#region Draw Item Summary
			int NormalTextPosition;
			rtbSummary.Text = "";
			rtbSummary.Text += Parser.Item.Name;
			rtbSummary.Select(0,rtbSummary.Text.Length);

			NormalTextPosition = rtbSummary.Text.Length + 1;
			rtbSummary.SelectionFont = new Font(rtbSummary.SelectionFont.FontFamily, 12, FontStyle.Bold);

			rtbSummary.Text += Environment.NewLine + Environment.NewLine;
			rtbSummary.Text += Parser.Item.Path.Replace("&raquo;", ">>") + Environment.NewLine + Environment.NewLine;
			rtbSummary.Text += Parser.Item.Description;

			rtbSummary.Select(NormalTextPosition,rtbSummary.Text.Length);
			rtbSummary.SelectionFont = new Font(rtbSummary.SelectionFont.FontFamily, 8.25f, FontStyle.Regular);
			#endregion

			#region Draw Attributes
			int l_CurrentRow = 0;
			string LastCategory = "";

			Utils.ImageFromWeb.MyConfig = MyConfig;
			Utils.ImageFromWeb WebImage = new Utils.ImageFromWeb();
			string ImageBaseURL = @"http://www.eve-i.com/home/crowley/";
            
			foreach ( ObjectExplorer.ItemAttribute ia in Parser.Item.Attributes )
			{
				if ( LastCategory != ia.GroupName )
				{
					grid[l_CurrentRow, 0] = new Cells.Cell(ia.GroupName, null, l_TitleModel);
					grid[l_CurrentRow, 0].ColumnSpan = 3;
					l_CurrentRow++;
					LastCategory = ia.GroupName;
				}

				//Create a VisualModel with an image
				SourceGrid2.VisualModels.Common m_VisualModel1 = new SourceGrid2.VisualModels.Common();
				m_VisualModel1.BackColor = l_CaptionModel.BackColor;
				// m_VisualModel1.Image = Image.FromFile(@"c:\projects\eve\combatlog2\images\icon26_01.png", false);
				try
				{
					m_VisualModel1.Image = WebImage.GetImage(ImageBaseURL + ia.IconURL.Replace("../",""));
					WebImage.CacheImage(ia.IconURL, m_VisualModel1.Image);
				}
				catch
                {
				}

				m_VisualModel1.ImageAlignment = SourceLibrary.Drawing.ContentAlignment.MiddleCenter;

				grid[l_CurrentRow,0] = new Cells.Cell();
				grid[l_CurrentRow,0].VisualModel = m_VisualModel1;

				//string
				grid[l_CurrentRow,1] = new Cells.Cell(ia.Name, null, l_CaptionModel);
				grid[l_CurrentRow,2] = new Cells.Cell(ia.Value, null, l_CaptionModel);

				l_CurrentRow++;
			}
			#endregion

			#region Grid Auto Sizing
			grid.Columns[0].AutoSizeMode = SourceGrid2.AutoSizeMode.MinimumSize;
			grid.Columns[1].AutoSizeMode = SourceGrid2.AutoSizeMode.MinimumSize | SourceGrid2.AutoSizeMode.Default;
			grid.Columns[2].AutoSizeMode = SourceGrid2.AutoSizeMode.MinimumSize | SourceGrid2.AutoSizeMode.Default;
			grid.AutoSize();
			grid.AutoStretchColumnsToFitWidth = true;
			grid.StretchColumnsToFitWidth();
			#endregion

		}

		private void listViewSearchResults_ItemActivate(object sender, System.EventArgs e)
		{
			DrawItemInfo((int)listViewSearchResults.SelectedItems[0].Tag);
		}
	}
}
