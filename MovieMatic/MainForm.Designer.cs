namespace Toenda.MovieMatic {
	partial class MainForm {
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer generierter Code

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.tslblAmount = new System.Windows.Forms.ToolStripStatusLabel();
			this.dgvMovies = new System.Windows.Forms.DataGridView();
			this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.SortValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colDiscs = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colOriginal = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.colCodec = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colGenre = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colQuality = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cCountry = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colConferred = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.colConferredTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cmsGridMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.editCmsItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.deleteCmsItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.e = new System.Windows.Forms.ToolStripSplitButton();
			this.subNewMovieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.subNewPersonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newFromWikipediaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.subPersonsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.subGenresToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.subCategoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.nonUsedIdsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moviesWithoutAddedPersonsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.subOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.datenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.subExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.subImportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.dbBackupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.dbRestoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.cleanupDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
			this.hilfeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.checkDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.checkNewerVersionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.subExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.cbFilterBar = new System.Windows.Forms.ToolStripComboBox();
			this.cbGenreBar = new System.Windows.Forms.ToolStripComboBox();
			this.cbCategoryBar = new System.Windows.Forms.ToolStripComboBox();
			this.txtSearchBar = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnHelp = new System.Windows.Forms.ToolStripButton();
			this.tsbtnInfo = new System.Windows.Forms.ToolStripButton();
			this.panel1 = new System.Windows.Forms.Panel();
			this.txtSearchBarTitle = new System.Windows.Forms.TextBox();
			this.cbFilterBarTitle = new System.Windows.Forms.ComboBox();
			this.contentPanel = new System.Windows.Forms.Panel();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.loadDataAsync = new System.ComponentModel.BackgroundWorker();
			this.toolStripContainer.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer.ContentPanel.SuspendLayout();
			this.toolStripContainer.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvMovies)).BeginInit();
			this.cmsGridMenu.SuspendLayout();
			this.toolStrip.SuspendLayout();
			this.panel1.SuspendLayout();
			this.contentPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripContainer
			// 
			// 
			// toolStripContainer.BottomToolStripPanel
			// 
			this.toolStripContainer.BottomToolStripPanel.Controls.Add(this.statusStrip1);
			// 
			// toolStripContainer.ContentPanel
			// 
			this.toolStripContainer.ContentPanel.BackColor = System.Drawing.Color.Transparent;
			this.toolStripContainer.ContentPanel.Controls.Add(this.dgvMovies);
			this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(1184, 717);
			this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer.LeftToolStripPanelVisible = false;
			this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer.Name = "toolStripContainer";
			this.toolStripContainer.RightToolStripPanelVisible = false;
			this.toolStripContainer.Size = new System.Drawing.Size(1184, 764);
			this.toolStripContainer.TabIndex = 0;
			this.toolStripContainer.Text = "toolStripContainer";
			// 
			// toolStripContainer.TopToolStripPanel
			// 
			this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslblAmount});
			this.statusStrip1.Location = new System.Drawing.Point(0, 0);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(1184, 22);
			this.statusStrip1.TabIndex = 0;
			// 
			// tslblAmount
			// 
			this.tslblAmount.Name = "tslblAmount";
			this.tslblAmount.Size = new System.Drawing.Size(78, 17);
			this.tslblAmount.Text = "Anzahl Filme:";
			// 
			// dgvMovies
			// 
			this.dgvMovies.AllowUserToAddRows = false;
			this.dgvMovies.AllowUserToResizeColumns = false;
			this.dgvMovies.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
			this.dgvMovies.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgvMovies.BackgroundColor = System.Drawing.Color.AntiqueWhite;
			this.dgvMovies.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dgvMovies.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dgvMovies.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvMovies.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dgvMovies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvMovies.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID,
            this.SortValue,
            this.colNumber,
            this.colName,
            this.colDiscs,
            this.colOriginal,
            this.colCodec,
            this.colGenre,
            this.colCategory,
            this.colQuality,
            this.cCountry,
            this.colConferred,
            this.colConferredTo});
			this.dgvMovies.ContextMenuStrip = this.cmsGridMenu;
			this.dgvMovies.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvMovies.Location = new System.Drawing.Point(0, 0);
			this.dgvMovies.MultiSelect = false;
			this.dgvMovies.Name = "dgvMovies";
			this.dgvMovies.ReadOnly = true;
			this.dgvMovies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvMovies.Size = new System.Drawing.Size(1184, 717);
			this.dgvMovies.TabIndex = 10;
			this.dgvMovies.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMovies_CellDoubleClick);
			this.dgvMovies.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMovies_CellEndEdit);
			this.dgvMovies.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMovies_ColumnHeaderMouseClick);
			this.dgvMovies.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvMovies_DataBindingComplete);
			this.dgvMovies.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvMovies_DataError);
			this.dgvMovies.RowContextMenuStripNeeded += new System.Windows.Forms.DataGridViewRowContextMenuStripNeededEventHandler(this.dgvMovies_RowContextMenuStripNeeded);
			this.dgvMovies.SelectionChanged += new System.EventHandler(this.dgvMovies_SelectionChanged);
			this.dgvMovies.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvMovies_UserDeletingRow);
			this.dgvMovies.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvMovies_KeyDown);
			// 
			// colID
			// 
			this.colID.DataPropertyName = "ID";
			this.colID.HeaderText = "ID";
			this.colID.Name = "colID";
			this.colID.ReadOnly = true;
			this.colID.Visible = false;
			// 
			// SortValue
			// 
			this.SortValue.DataPropertyName = "SortValue";
			this.SortValue.FillWeight = 60F;
			this.SortValue.HeaderText = "Sort.";
			this.SortValue.Name = "SortValue";
			this.SortValue.ReadOnly = true;
			this.SortValue.ToolTipText = "Sort.";
			this.SortValue.Width = 60;
			// 
			// colNumber
			// 
			this.colNumber.DataPropertyName = "Number";
			this.colNumber.FillWeight = 50F;
			this.colNumber.HeaderText = "Nr.";
			this.colNumber.MinimumWidth = 50;
			this.colNumber.Name = "colNumber";
			this.colNumber.ReadOnly = true;
			this.colNumber.ToolTipText = "Nr.";
			this.colNumber.Width = 50;
			// 
			// colName
			// 
			this.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.colName.DataPropertyName = "Name";
			this.colName.FillWeight = 266.2958F;
			this.colName.HeaderText = "Name";
			this.colName.MinimumWidth = 120;
			this.colName.Name = "colName";
			this.colName.ReadOnly = true;
			this.colName.ToolTipText = "Name";
			// 
			// colDiscs
			// 
			this.colDiscs.DataPropertyName = "DiscAmount";
			this.colDiscs.FillWeight = 40F;
			this.colDiscs.HeaderText = "Discs";
			this.colDiscs.Name = "colDiscs";
			this.colDiscs.ReadOnly = true;
			this.colDiscs.ToolTipText = "Discs";
			this.colDiscs.Width = 40;
			// 
			// colOriginal
			// 
			this.colOriginal.DataPropertyName = "IsOriginal";
			this.colOriginal.FillWeight = 50F;
			this.colOriginal.HeaderText = "Original";
			this.colOriginal.Name = "colOriginal";
			this.colOriginal.ReadOnly = true;
			this.colOriginal.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.colOriginal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.colOriginal.ToolTipText = "Original";
			this.colOriginal.Width = 50;
			// 
			// colCodec
			// 
			this.colCodec.DataPropertyName = "Codec";
			this.colCodec.FillWeight = 90F;
			this.colCodec.HeaderText = "Codec";
			this.colCodec.Name = "colCodec";
			this.colCodec.ReadOnly = true;
			this.colCodec.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.colCodec.ToolTipText = "Codec";
			this.colCodec.Width = 90;
			// 
			// colGenre
			// 
			this.colGenre.DataPropertyName = "GenerateGenresString";
			this.colGenre.FillWeight = 120F;
			this.colGenre.HeaderText = "Genre";
			this.colGenre.Name = "colGenre";
			this.colGenre.ReadOnly = true;
			this.colGenre.ToolTipText = "Genre";
			this.colGenre.Width = 120;
			// 
			// colCategory
			// 
			this.colCategory.DataPropertyName = "GenerateCategoriesString";
			this.colCategory.FillWeight = 120F;
			this.colCategory.HeaderText = "Kategorie";
			this.colCategory.Name = "colCategory";
			this.colCategory.ReadOnly = true;
			this.colCategory.ToolTipText = "Kategorie";
			this.colCategory.Width = 120;
			// 
			// colQuality
			// 
			this.colQuality.DataPropertyName = "GenerateQualityString";
			this.colQuality.FillWeight = 80F;
			this.colQuality.HeaderText = "Qualität";
			this.colQuality.Name = "colQuality";
			this.colQuality.ReadOnly = true;
			this.colQuality.ToolTipText = "Qualität";
			this.colQuality.Width = 80;
			// 
			// cCountry
			// 
			this.cCountry.DataPropertyName = "GenerateCountryString";
			this.cCountry.FillWeight = 170F;
			this.cCountry.HeaderText = "Land (Hauptproduzent)";
			this.cCountry.Name = "cCountry";
			this.cCountry.ReadOnly = true;
			this.cCountry.ToolTipText = "Land (Hauptproduzent)";
			this.cCountry.Width = 170;
			// 
			// colConferred
			// 
			this.colConferred.DataPropertyName = "IsConferred";
			this.colConferred.FillWeight = 40F;
			this.colConferred.HeaderText = "Verl.";
			this.colConferred.Name = "colConferred";
			this.colConferred.ReadOnly = true;
			this.colConferred.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.colConferred.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.colConferred.ToolTipText = "Verl.";
			this.colConferred.Width = 40;
			// 
			// colConferredTo
			// 
			this.colConferredTo.DataPropertyName = "ConferredTo";
			this.colConferredTo.FillWeight = 120F;
			this.colConferredTo.HeaderText = "Verliehen an";
			this.colConferredTo.Name = "colConferredTo";
			this.colConferredTo.ReadOnly = true;
			this.colConferredTo.ToolTipText = "Verliehen an";
			this.colConferredTo.Width = 120;
			// 
			// cmsGridMenu
			// 
			this.cmsGridMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editCmsItem,
            this.toolStripSeparator4,
            this.deleteCmsItem});
			this.cmsGridMenu.Name = "cmsGridMenu";
			this.cmsGridMenu.Size = new System.Drawing.Size(140, 54);
			// 
			// editCmsItem
			// 
			this.editCmsItem.Image = global::Toenda.MovieMatic.Properties.Resources.pencil;
			this.editCmsItem.Name = "editCmsItem";
			this.editCmsItem.Size = new System.Drawing.Size(139, 22);
			this.editCmsItem.Text = "Bearbeiten...";
			this.editCmsItem.Click += new System.EventHandler(this.editCmsItem_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(136, 6);
			// 
			// deleteCmsItem
			// 
			this.deleteCmsItem.Image = global::Toenda.MovieMatic.Properties.Resources.delete;
			this.deleteCmsItem.Name = "deleteCmsItem";
			this.deleteCmsItem.Size = new System.Drawing.Size(139, 22);
			this.deleteCmsItem.Text = "Löschen...";
			this.deleteCmsItem.Click += new System.EventHandler(this.deleteCmsItem_Click);
			// 
			// toolStrip
			// 
			this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.e,
            this.toolStripSeparator5,
            this.cbFilterBar,
            this.cbGenreBar,
            this.cbCategoryBar,
            this.txtSearchBar,
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.tsbtnHelp,
            this.tsbtnInfo});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Padding = new System.Windows.Forms.Padding(0);
			this.toolStrip.Size = new System.Drawing.Size(1184, 25);
			this.toolStrip.Stretch = true;
			this.toolStrip.TabIndex = 1;
			// 
			// e
			// 
			this.e.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subNewMovieToolStripMenuItem,
            this.subNewPersonToolStripMenuItem,
            this.newFromWikipediaToolStripMenuItem,
            this.toolStripSeparator6,
            this.subPersonsToolStripMenuItem,
            this.subGenresToolStripMenuItem1,
            this.subCategoriesToolStripMenuItem,
            this.nonUsedIdsToolStripMenuItem,
            this.moviesWithoutAddedPersonsToolStripMenuItem,
            this.toolStripSeparator7,
            this.subOptionsToolStripMenuItem,
            this.datenToolStripMenuItem,
            this.toolStripSeparator9,
            this.hilfeToolStripMenuItem,
            this.subExitToolStripMenuItem});
			this.e.Image = global::Toenda.MovieMatic.Properties.Resources.table_multiple;
			this.e.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.e.Name = "e";
			this.e.Size = new System.Drawing.Size(106, 22);
			this.e.Text = "Organisieren";
			this.e.ToolTipText = "Organisieren";
			this.e.ButtonClick += new System.EventHandler(this.tsbtnOrganize_ButtonClick);
			// 
			// subNewMovieToolStripMenuItem
			// 
			this.subNewMovieToolStripMenuItem.Image = global::Toenda.MovieMatic.Properties.Resources.film_add;
			this.subNewMovieToolStripMenuItem.Name = "subNewMovieToolStripMenuItem";
			this.subNewMovieToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.subNewMovieToolStripMenuItem.Size = new System.Drawing.Size(275, 22);
			this.subNewMovieToolStripMenuItem.Text = "Neuer Film";
			this.subNewMovieToolStripMenuItem.Click += new System.EventHandler(this.subNewMovieToolStripMenuItem_Click);
			// 
			// subNewPersonToolStripMenuItem
			// 
			this.subNewPersonToolStripMenuItem.Image = global::Toenda.MovieMatic.Properties.Resources.user_add;
			this.subNewPersonToolStripMenuItem.Name = "subNewPersonToolStripMenuItem";
			this.subNewPersonToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
			this.subNewPersonToolStripMenuItem.Size = new System.Drawing.Size(275, 22);
			this.subNewPersonToolStripMenuItem.Text = "Neue Person";
			this.subNewPersonToolStripMenuItem.Click += new System.EventHandler(this.subNewPersonToolStripMenuItem_Click);
			// 
			// newFromWikipediaToolStripMenuItem
			// 
			this.newFromWikipediaToolStripMenuItem.Image = global::Toenda.MovieMatic.Properties.Resources.wikipedia;
			this.newFromWikipediaToolStripMenuItem.Name = "newFromWikipediaToolStripMenuItem";
			this.newFromWikipediaToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
			this.newFromWikipediaToolStripMenuItem.Size = new System.Drawing.Size(275, 22);
			this.newFromWikipediaToolStripMenuItem.Text = "Neu aus Wikipedia";
			this.newFromWikipediaToolStripMenuItem.Click += new System.EventHandler(this.newFromWikipediaToolStripMenuItem_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(272, 6);
			// 
			// subPersonsToolStripMenuItem
			// 
			this.subPersonsToolStripMenuItem.Image = global::Toenda.MovieMatic.Properties.Resources.user;
			this.subPersonsToolStripMenuItem.Name = "subPersonsToolStripMenuItem";
			this.subPersonsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.P)));
			this.subPersonsToolStripMenuItem.Size = new System.Drawing.Size(275, 22);
			this.subPersonsToolStripMenuItem.Text = "Personen";
			this.subPersonsToolStripMenuItem.Click += new System.EventHandler(this.subPersonsToolStripMenuItem_Click);
			// 
			// subGenresToolStripMenuItem1
			// 
			this.subGenresToolStripMenuItem1.Image = global::Toenda.MovieMatic.Properties.Resources.layers;
			this.subGenresToolStripMenuItem1.Name = "subGenresToolStripMenuItem1";
			this.subGenresToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.G)));
			this.subGenresToolStripMenuItem1.Size = new System.Drawing.Size(275, 22);
			this.subGenresToolStripMenuItem1.Text = "Genres";
			this.subGenresToolStripMenuItem1.Click += new System.EventHandler(this.subGenresToolStripMenuItem1_Click);
			// 
			// subCategoriesToolStripMenuItem
			// 
			this.subCategoriesToolStripMenuItem.Image = global::Toenda.MovieMatic.Properties.Resources.tag_blue;
			this.subCategoriesToolStripMenuItem.Name = "subCategoriesToolStripMenuItem";
			this.subCategoriesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.K)));
			this.subCategoriesToolStripMenuItem.Size = new System.Drawing.Size(275, 22);
			this.subCategoriesToolStripMenuItem.Text = "Kategorien";
			this.subCategoriesToolStripMenuItem.Click += new System.EventHandler(this.subCategoriesToolStripMenuItem_Click);
			// 
			// nonUsedIdsToolStripMenuItem
			// 
			this.nonUsedIdsToolStripMenuItem.Name = "nonUsedIdsToolStripMenuItem";
			this.nonUsedIdsToolStripMenuItem.Size = new System.Drawing.Size(275, 22);
			this.nonUsedIdsToolStripMenuItem.Text = "Nicht verwendete Nummern anzeigen";
			this.nonUsedIdsToolStripMenuItem.Click += new System.EventHandler(this.nonUsedIdsToolStripMenuItem_Click);
			// 
			// moviesWithoutAddedPersonsToolStripMenuItem
			// 
			this.moviesWithoutAddedPersonsToolStripMenuItem.Name = "moviesWithoutAddedPersonsToolStripMenuItem";
			this.moviesWithoutAddedPersonsToolStripMenuItem.Size = new System.Drawing.Size(275, 22);
			this.moviesWithoutAddedPersonsToolStripMenuItem.Text = "Filme ohne zugeordnete Personen";
			this.moviesWithoutAddedPersonsToolStripMenuItem.Click += new System.EventHandler(this.moviesWithoutAddedPersonsToolStripMenuItem_Click);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(272, 6);
			// 
			// subOptionsToolStripMenuItem
			// 
			this.subOptionsToolStripMenuItem.Image = global::Toenda.MovieMatic.Properties.Resources.cog;
			this.subOptionsToolStripMenuItem.Name = "subOptionsToolStripMenuItem";
			this.subOptionsToolStripMenuItem.Size = new System.Drawing.Size(275, 22);
			this.subOptionsToolStripMenuItem.Text = "Optionen";
			this.subOptionsToolStripMenuItem.Click += new System.EventHandler(this.subOptionsToolStripMenuItem_Click);
			// 
			// datenToolStripMenuItem
			// 
			this.datenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subExportToolStripMenuItem,
            this.subImportToolStripMenuItem,
            this.toolStripSeparator2,
            this.dbBackupToolStripMenuItem,
            this.dbRestoreToolStripMenuItem,
            this.toolStripSeparator3,
            this.cleanupDBToolStripMenuItem});
			this.datenToolStripMenuItem.Name = "datenToolStripMenuItem";
			this.datenToolStripMenuItem.Size = new System.Drawing.Size(275, 22);
			this.datenToolStripMenuItem.Text = "Daten";
			// 
			// subExportToolStripMenuItem
			// 
			this.subExportToolStripMenuItem.Image = global::Toenda.MovieMatic.Properties.Resources.arrow_right;
			this.subExportToolStripMenuItem.Name = "subExportToolStripMenuItem";
			this.subExportToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
			this.subExportToolStripMenuItem.Text = "Export";
			this.subExportToolStripMenuItem.Click += new System.EventHandler(this.subExportToolStripMenuItem_Click);
			// 
			// subImportToolStripMenuItem
			// 
			this.subImportToolStripMenuItem.Image = global::Toenda.MovieMatic.Properties.Resources.arrow_left;
			this.subImportToolStripMenuItem.Name = "subImportToolStripMenuItem";
			this.subImportToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
			this.subImportToolStripMenuItem.Text = "Import";
			this.subImportToolStripMenuItem.Click += new System.EventHandler(this.subImportToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(217, 6);
			// 
			// dbBackupToolStripMenuItem
			// 
			this.dbBackupToolStripMenuItem.Image = global::Toenda.MovieMatic.Properties.Resources.database_save;
			this.dbBackupToolStripMenuItem.Name = "dbBackupToolStripMenuItem";
			this.dbBackupToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
			this.dbBackupToolStripMenuItem.Text = "Datenbank Backup";
			this.dbBackupToolStripMenuItem.Click += new System.EventHandler(this.dbBackupToolStripMenuItem_Click);
			// 
			// dbRestoreToolStripMenuItem
			// 
			this.dbRestoreToolStripMenuItem.Image = global::Toenda.MovieMatic.Properties.Resources.database_refresh;
			this.dbRestoreToolStripMenuItem.Name = "dbRestoreToolStripMenuItem";
			this.dbRestoreToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
			this.dbRestoreToolStripMenuItem.Text = "Datenbank wiederherstellen";
			this.dbRestoreToolStripMenuItem.Click += new System.EventHandler(this.dbRestoreToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(217, 6);
			// 
			// cleanupDBToolStripMenuItem
			// 
			this.cleanupDBToolStripMenuItem.Name = "cleanupDBToolStripMenuItem";
			this.cleanupDBToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
			this.cleanupDBToolStripMenuItem.Text = "Datenbank bereinigen";
			this.cleanupDBToolStripMenuItem.Click += new System.EventHandler(this.cleanupDBToolStripMenuItem_Click);
			// 
			// toolStripSeparator9
			// 
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			this.toolStripSeparator9.Size = new System.Drawing.Size(272, 6);
			// 
			// hilfeToolStripMenuItem
			// 
			this.hilfeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkDatabaseToolStripMenuItem,
            this.checkNewerVersionToolStripMenuItem,
            this.toolStripSeparator8,
            this.helpToolStripMenuItem,
            this.infoToolStripMenuItem});
			this.hilfeToolStripMenuItem.Name = "hilfeToolStripMenuItem";
			this.hilfeToolStripMenuItem.Size = new System.Drawing.Size(275, 22);
			this.hilfeToolStripMenuItem.Text = "Hilfe";
			// 
			// checkDatabaseToolStripMenuItem
			// 
			this.checkDatabaseToolStripMenuItem.Image = global::Toenda.MovieMatic.Properties.Resources.database_lightning;
			this.checkDatabaseToolStripMenuItem.Name = "checkDatabaseToolStripMenuItem";
			this.checkDatabaseToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
			this.checkDatabaseToolStripMenuItem.Text = "Datenbank überprüfen";
			this.checkDatabaseToolStripMenuItem.Click += new System.EventHandler(this.checkDatabaseToolStripMenuItem_Click);
			// 
			// checkNewerVersionToolStripMenuItem
			// 
			this.checkNewerVersionToolStripMenuItem.Image = global::Toenda.MovieMatic.Properties.Resources.lightning;
			this.checkNewerVersionToolStripMenuItem.Name = "checkNewerVersionToolStripMenuItem";
			this.checkNewerVersionToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
			this.checkNewerVersionToolStripMenuItem.Text = "Nach neuer Version suchen";
			this.checkNewerVersionToolStripMenuItem.Click += new System.EventHandler(this.checkNewerVersionToolStripMenuItem_Click);
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(215, 6);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.Image = global::Toenda.MovieMatic.Properties.Resources.help;
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
			this.helpToolStripMenuItem.Text = "Hilfe";
			this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
			// 
			// infoToolStripMenuItem
			// 
			this.infoToolStripMenuItem.Image = global::Toenda.MovieMatic.Properties.Resources.information;
			this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
			this.infoToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
			this.infoToolStripMenuItem.Text = "Info";
			this.infoToolStripMenuItem.Click += new System.EventHandler(this.infoToolStripMenuItem_Click);
			// 
			// subExitToolStripMenuItem
			// 
			this.subExitToolStripMenuItem.Image = global::Toenda.MovieMatic.Properties.Resources.stop;
			this.subExitToolStripMenuItem.Name = "subExitToolStripMenuItem";
			this.subExitToolStripMenuItem.Size = new System.Drawing.Size(275, 22);
			this.subExitToolStripMenuItem.Text = "Beenden";
			this.subExitToolStripMenuItem.Click += new System.EventHandler(this.subExitToolStripMenuItem_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
			// 
			// cbFilterBar
			// 
			this.cbFilterBar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbFilterBar.Name = "cbFilterBar";
			this.cbFilterBar.Size = new System.Drawing.Size(200, 25);
			this.cbFilterBar.SelectedIndexChanged += new System.EventHandler(this.cbFilterBar_SelectedIndexChanged);
			// 
			// cbGenreBar
			// 
			this.cbGenreBar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbGenreBar.Name = "cbGenreBar";
			this.cbGenreBar.Size = new System.Drawing.Size(200, 25);
			this.cbGenreBar.SelectedIndexChanged += new System.EventHandler(this.cbGenreBar_SelectedIndexChanged);
			// 
			// cbCategoryBar
			// 
			this.cbCategoryBar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbCategoryBar.Name = "cbCategoryBar";
			this.cbCategoryBar.Size = new System.Drawing.Size(200, 25);
			this.cbCategoryBar.SelectedIndexChanged += new System.EventHandler(this.cbCategoryBar_SelectedIndexChanged);
			// 
			// txtSearchBar
			// 
			this.txtSearchBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtSearchBar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSearchBar.Name = "txtSearchBar";
			this.txtSearchBar.Size = new System.Drawing.Size(300, 25);
			this.txtSearchBar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearchBar_KeyUp);
			this.txtSearchBar.TextChanged += new System.EventHandler(this.txtSearchBar_TextChanged);
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton1.Image = global::Toenda.MovieMatic.Properties.Resources.cross;
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton1.Text = "Sucheingabe zurücksetzen";
			this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbtnHelp
			// 
			this.tsbtnHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnHelp.Image = global::Toenda.MovieMatic.Properties.Resources.help;
			this.tsbtnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnHelp.Name = "tsbtnHelp";
			this.tsbtnHelp.Size = new System.Drawing.Size(23, 22);
			this.tsbtnHelp.Text = "Hilfe";
			this.tsbtnHelp.Click += new System.EventHandler(this.tsbtnHelp_Click);
			// 
			// tsbtnInfo
			// 
			this.tsbtnInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnInfo.Image = global::Toenda.MovieMatic.Properties.Resources.information;
			this.tsbtnInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnInfo.Name = "tsbtnInfo";
			this.tsbtnInfo.Size = new System.Drawing.Size(23, 22);
			this.tsbtnInfo.Text = "Informationen";
			this.tsbtnInfo.Click += new System.EventHandler(this.tsbtnInfo_Click);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BackColor = System.Drawing.Color.Transparent;
			this.panel1.Controls.Add(this.txtSearchBarTitle);
			this.panel1.Controls.Add(this.cbFilterBarTitle);
			this.panel1.Location = new System.Drawing.Point(498, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(684, 28);
			this.panel1.TabIndex = 1;
			// 
			// txtSearchBarTitle
			// 
			this.txtSearchBarTitle.BackColor = System.Drawing.SystemColors.Control;
			this.txtSearchBarTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtSearchBarTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSearchBarTitle.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtSearchBarTitle.Location = new System.Drawing.Point(382, 3);
			this.txtSearchBarTitle.Name = "txtSearchBarTitle";
			this.txtSearchBarTitle.Size = new System.Drawing.Size(300, 21);
			this.txtSearchBarTitle.TabIndex = 0;
			this.txtSearchBarTitle.TextChanged += new System.EventHandler(this.txtSearchBar_TextChanged);
			this.txtSearchBarTitle.Enter += new System.EventHandler(this.txtSearchBar_Enter);
			this.txtSearchBarTitle.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearchBar_KeyUp);
			this.txtSearchBarTitle.Leave += new System.EventHandler(this.txtSearchBar_Leave);
			// 
			// cbFilterBarTitle
			// 
			this.cbFilterBarTitle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbFilterBarTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cbFilterBarTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbFilterBarTitle.FormattingEnabled = true;
			this.cbFilterBarTitle.Location = new System.Drawing.Point(126, 3);
			this.cbFilterBarTitle.Name = "cbFilterBarTitle";
			this.cbFilterBarTitle.Size = new System.Drawing.Size(250, 21);
			this.cbFilterBarTitle.TabIndex = 1;
			this.cbFilterBarTitle.SelectedIndexChanged += new System.EventHandler(this.cbFilterBar_SelectedIndexChanged);
			// 
			// contentPanel
			// 
			this.contentPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.contentPanel.BackColor = System.Drawing.Color.Transparent;
			this.contentPanel.Controls.Add(this.panel1);
			this.contentPanel.Location = new System.Drawing.Point(0, -35);
			this.contentPanel.Name = "contentPanel";
			this.contentPanel.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.contentPanel.Size = new System.Drawing.Size(1182, 67);
			this.contentPanel.TabIndex = 1;
			this.contentPanel.Visible = false;
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(32, 19);
			// 
			// loadDataAsync
			// 
			this.loadDataAsync.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.loadDataAsync_RunWorkerCompleted);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.ClientSize = new System.Drawing.Size(1184, 764);
			this.Controls.Add(this.toolStripContainer);
			this.Controls.Add(this.contentPanel);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MovieMatic";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
			this.toolStripContainer.BottomToolStripPanel.ResumeLayout(false);
			this.toolStripContainer.BottomToolStripPanel.PerformLayout();
			this.toolStripContainer.ContentPanel.ResumeLayout(false);
			this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer.TopToolStripPanel.PerformLayout();
			this.toolStripContainer.ResumeLayout(false);
			this.toolStripContainer.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvMovies)).EndInit();
			this.cmsGridMenu.ResumeLayout(false);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.contentPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolStripContainer toolStripContainer;
		private System.Windows.Forms.DataGridView dgvMovies;
		private System.Windows.Forms.ContextMenuStrip cmsGridMenu;
		private System.Windows.Forms.ToolStripMenuItem editCmsItem;
		private System.Windows.Forms.ToolStripMenuItem deleteCmsItem;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripStatusLabel tslblAmount;
		private System.Windows.Forms.ToolStripSplitButton e;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem subNewMovieToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem subNewPersonToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem subPersonsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem subGenresToolStripMenuItem1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripMenuItem subOptionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem subImportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem subExportToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
		private System.Windows.Forms.ToolStripMenuItem subExitToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton tsbtnHelp;
		private System.Windows.Forms.ToolStripButton tsbtnInfo;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TextBox txtSearchBarTitle;
		private System.Windows.Forms.ComboBox cbFilterBarTitle;
		private System.Windows.Forms.Panel contentPanel;
		private System.Windows.Forms.ToolStripComboBox cbFilterBar;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripTextBox txtSearchBar;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.ToolStripMenuItem subCategoriesToolStripMenuItem;
		private System.Windows.Forms.ToolStripComboBox cbGenreBar;
		private System.Windows.Forms.ToolStripComboBox cbCategoryBar;
		private System.Windows.Forms.ToolStripMenuItem dbBackupToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem dbRestoreToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem datenToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem cleanupDBToolStripMenuItem;
		private System.ComponentModel.BackgroundWorker loadDataAsync;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem moviesWithoutAddedPersonsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newFromWikipediaToolStripMenuItem;
		private System.Windows.Forms.DataGridViewTextBoxColumn colID;
		private System.Windows.Forms.DataGridViewTextBoxColumn SortValue;
		private System.Windows.Forms.DataGridViewTextBoxColumn colNumber;
		private System.Windows.Forms.DataGridViewTextBoxColumn colName;
		private System.Windows.Forms.DataGridViewTextBoxColumn colDiscs;
		private System.Windows.Forms.DataGridViewCheckBoxColumn colOriginal;
		private System.Windows.Forms.DataGridViewTextBoxColumn colCodec;
		private System.Windows.Forms.DataGridViewTextBoxColumn colGenre;
		private System.Windows.Forms.DataGridViewTextBoxColumn colCategory;
		private System.Windows.Forms.DataGridViewTextBoxColumn colQuality;
		private System.Windows.Forms.DataGridViewTextBoxColumn cCountry;
		private System.Windows.Forms.DataGridViewCheckBoxColumn colConferred;
		private System.Windows.Forms.DataGridViewTextBoxColumn colConferredTo;
		private System.Windows.Forms.ToolStripMenuItem nonUsedIdsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem hilfeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem checkDatabaseToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem checkNewerVersionToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
	}
}

