namespace Toenda.MovieMatic {
	partial class AdministerPersonsForm {
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
			this.btnCancel = new System.Windows.Forms.Button();
			this.dgvPersons = new System.Windows.Forms.DataGridView();
			this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colIsActor = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.colIsDirector = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.colIsProducer = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.colIsMusician = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.colIsCameraman = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.colIsCutter = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.colIsWriter = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.colMovies = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colMoviesAsActor = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colMoviesAsDirector = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colMoviesAsProducer = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colMoviesAsMusician = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colMoviesAsCameraman = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colMoviesAsCutter = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colMoviesAsWriter = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cmsGridMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.editCmsItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.moviesWithThisActorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moviesWithThisDirectorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moviesWithThisProducerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moviesWithThisMusicianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moviesWithThisCameramanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moviesWithThisCutterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moviesWithThisWriterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.deleteCmsItem = new System.Windows.Forms.ToolStripMenuItem();
			this.panel1 = new System.Windows.Forms.Panel();
			this.chkWriter = new System.Windows.Forms.CheckBox();
			this.chkCutter = new System.Windows.Forms.CheckBox();
			this.chkCameraman = new System.Windows.Forms.CheckBox();
			this.chkMusician = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.btnHelp = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.btnAddPerson = new System.Windows.Forms.Button();
			this.chkProducer = new System.Windows.Forms.CheckBox();
			this.chkDirector = new System.Windows.Forms.CheckBox();
			this.chkActor = new System.Windows.Forms.CheckBox();
			this.txtName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.lblFound = new System.Windows.Forms.Label();
			this.loadDataAsync = new System.ComponentModel.BackgroundWorker();
			( (System.ComponentModel.ISupportInitialize)( this.dgvPersons ) ).BeginInit();
			this.cmsGridMenu.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.btnCancel.Location = new System.Drawing.Point(1037, 589);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Schliessen";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// dgvPersons
			// 
			this.dgvPersons.AllowUserToAddRows = false;
			this.dgvPersons.AllowUserToResizeColumns = false;
			this.dgvPersons.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 238 ) ) ) ), ( (int)( ( (byte)( 240 ) ) ) ), ( (int)( ( (byte)( 255 ) ) ) ));
			this.dgvPersons.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgvPersons.BackgroundColor = System.Drawing.Color.AntiqueWhite;
			this.dgvPersons.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dgvPersons.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dgvPersons.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			this.dgvPersons.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvPersons.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID,
            this.colName,
            this.colIsActor,
            this.colIsDirector,
            this.colIsProducer,
            this.colIsMusician,
            this.colIsCameraman,
            this.colIsCutter,
            this.colIsWriter,
            this.colMovies,
            this.colMoviesAsActor,
            this.colMoviesAsDirector,
            this.colMoviesAsProducer,
            this.colMoviesAsMusician,
            this.colMoviesAsCameraman,
            this.colMoviesAsCutter,
            this.colMoviesAsWriter});
			this.dgvPersons.ContextMenuStrip = this.cmsGridMenu;
			this.dgvPersons.Location = new System.Drawing.Point(12, 81);
			this.dgvPersons.Name = "dgvPersons";
			this.dgvPersons.ReadOnly = true;
			this.dgvPersons.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.dgvPersons.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvPersons.Size = new System.Drawing.Size(1100, 502);
			this.dgvPersons.TabIndex = 4;
			this.dgvPersons.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPersons_CellClick);
			this.dgvPersons.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPersons_CellDoubleClick);
			this.dgvPersons.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPersons_ColumnHeaderMouseClick);
			this.dgvPersons.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvPersons_DataBindingComplete);
			this.dgvPersons.RowContextMenuStripNeeded += new System.Windows.Forms.DataGridViewRowContextMenuStripNeededEventHandler(this.dgvPersons_RowContextMenuStripNeeded);
			this.dgvPersons.SelectionChanged += new System.EventHandler(this.dgvPersons_SelectionChanged);
			this.dgvPersons.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvPersons_UserDeletingRow);
			this.dgvPersons.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvPersons_KeyUp);
			// 
			// colID
			// 
			this.colID.DataPropertyName = "ID";
			this.colID.HeaderText = "pkid";
			this.colID.Name = "colID";
			this.colID.ReadOnly = true;
			this.colID.Visible = false;
			// 
			// colName
			// 
			this.colName.DataPropertyName = "Fullname";
			this.colName.HeaderText = "Name";
			this.colName.Name = "colName";
			this.colName.ReadOnly = true;
			this.colName.ToolTipText = "Name";
			this.colName.Width = 212;
			// 
			// colIsActor
			// 
			this.colIsActor.DataPropertyName = "IsActor";
			this.colIsActor.HeaderText = "Schauspiel.";
			this.colIsActor.Name = "colIsActor";
			this.colIsActor.ReadOnly = true;
			this.colIsActor.ToolTipText = "Schauspiel.";
			this.colIsActor.Width = 63;
			// 
			// colIsDirector
			// 
			this.colIsDirector.DataPropertyName = "IsDirector";
			this.colIsDirector.HeaderText = "Regisseur";
			this.colIsDirector.Name = "colIsDirector";
			this.colIsDirector.ReadOnly = true;
			this.colIsDirector.ToolTipText = "Regisseur";
			this.colIsDirector.Width = 60;
			// 
			// colIsProducer
			// 
			this.colIsProducer.DataPropertyName = "IsProducer";
			this.colIsProducer.HeaderText = "Produzent";
			this.colIsProducer.Name = "colIsProducer";
			this.colIsProducer.ReadOnly = true;
			this.colIsProducer.ToolTipText = "Produzent";
			this.colIsProducer.Width = 63;
			// 
			// colIsMusician
			// 
			this.colIsMusician.DataPropertyName = "IsMusician";
			this.colIsMusician.HeaderText = "Musiker";
			this.colIsMusician.Name = "colIsMusician";
			this.colIsMusician.ReadOnly = true;
			this.colIsMusician.ToolTipText = "Musiker";
			this.colIsMusician.Width = 51;
			// 
			// colIsCameraman
			// 
			this.colIsCameraman.DataPropertyName = "IsCameraman";
			this.colIsCameraman.HeaderText = "Kameram.";
			this.colIsCameraman.Name = "colIsCameraman";
			this.colIsCameraman.ReadOnly = true;
			this.colIsCameraman.ToolTipText = "Kameram.";
			this.colIsCameraman.Width = 61;
			// 
			// colIsCutter
			// 
			this.colIsCutter.DataPropertyName = "IsCutter";
			this.colIsCutter.HeaderText = "Cutter";
			this.colIsCutter.Name = "colIsCutter";
			this.colIsCutter.ReadOnly = true;
			this.colIsCutter.ToolTipText = "Cutter";
			this.colIsCutter.Width = 43;
			// 
			// colIsWriter
			// 
			this.colIsWriter.DataPropertyName = "IsWriter";
			this.colIsWriter.HeaderText = "Autor";
			this.colIsWriter.Name = "colIsWriter";
			this.colIsWriter.ReadOnly = true;
			this.colIsWriter.ToolTipText = "Autor";
			this.colIsWriter.Width = 43;
			// 
			// colMovies
			// 
			this.colMovies.DataPropertyName = "MovieQuantity";
			this.colMovies.HeaderText = "Filme";
			this.colMovies.Name = "colMovies";
			this.colMovies.ReadOnly = true;
			this.colMovies.ToolTipText = "Filme";
			this.colMovies.Width = 45;
			// 
			// colMoviesAsActor
			// 
			this.colMoviesAsActor.DataPropertyName = "MovieQuantityAsActor";
			this.colMoviesAsActor.HeaderText = "Schauspiel";
			this.colMoviesAsActor.Name = "colMoviesAsActor";
			this.colMoviesAsActor.ReadOnly = true;
			this.colMoviesAsActor.ToolTipText = "Schauspiel";
			this.colMoviesAsActor.Width = 71;
			// 
			// colMoviesAsDirector
			// 
			this.colMoviesAsDirector.DataPropertyName = "MovieQuantityAsDirector";
			this.colMoviesAsDirector.HeaderText = "Regie";
			this.colMoviesAsDirector.Name = "colMoviesAsDirector";
			this.colMoviesAsDirector.ReadOnly = true;
			this.colMoviesAsDirector.ToolTipText = "Regie";
			this.colMoviesAsDirector.Width = 45;
			// 
			// colMoviesAsProducer
			// 
			this.colMoviesAsProducer.DataPropertyName = "MovieQuantityAsProducer";
			this.colMoviesAsProducer.HeaderText = "Produktion";
			this.colMoviesAsProducer.Name = "colMoviesAsProducer";
			this.colMoviesAsProducer.ReadOnly = true;
			this.colMoviesAsProducer.ToolTipText = "Produktion";
			this.colMoviesAsProducer.Width = 63;
			// 
			// colMoviesAsMusician
			// 
			this.colMoviesAsMusician.DataPropertyName = "MovieQuantityAsMusician";
			this.colMoviesAsMusician.HeaderText = "Musik";
			this.colMoviesAsMusician.Name = "colMoviesAsMusician";
			this.colMoviesAsMusician.ReadOnly = true;
			this.colMoviesAsMusician.ToolTipText = "Musik";
			this.colMoviesAsMusician.Width = 48;
			// 
			// colMoviesAsCameraman
			// 
			this.colMoviesAsCameraman.DataPropertyName = "MovieQuantityAsCameraman";
			this.colMoviesAsCameraman.HeaderText = "Kamera";
			this.colMoviesAsCameraman.Name = "colMoviesAsCameraman";
			this.colMoviesAsCameraman.ReadOnly = true;
			this.colMoviesAsCameraman.ToolTipText = "Kamera";
			this.colMoviesAsCameraman.Width = 58;
			// 
			// colMoviesAsCutter
			// 
			this.colMoviesAsCutter.DataPropertyName = "MovieQuantityAsCutter";
			this.colMoviesAsCutter.HeaderText = "Schnitt";
			this.colMoviesAsCutter.Name = "colMoviesAsCutter";
			this.colMoviesAsCutter.ReadOnly = true;
			this.colMoviesAsCutter.ToolTipText = "Schnitt";
			this.colMoviesAsCutter.Width = 50;
			// 
			// colMoviesAsWriter
			// 
			this.colMoviesAsWriter.DataPropertyName = "MovieQuantityAsWriter";
			this.colMoviesAsWriter.HeaderText = "Drehbuch";
			this.colMoviesAsWriter.Name = "colMoviesAsWriter";
			this.colMoviesAsWriter.ReadOnly = true;
			this.colMoviesAsWriter.ToolTipText = "Drehbuch";
			this.colMoviesAsWriter.Width = 63;
			// 
			// cmsGridMenu
			// 
			this.cmsGridMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editCmsItem,
            this.toolStripSeparator4,
            this.moviesWithThisActorToolStripMenuItem,
            this.moviesWithThisDirectorToolStripMenuItem,
            this.moviesWithThisProducerToolStripMenuItem,
            this.moviesWithThisMusicianToolStripMenuItem,
            this.moviesWithThisCameramanToolStripMenuItem,
            this.moviesWithThisCutterToolStripMenuItem,
            this.moviesWithThisWriterToolStripMenuItem,
            this.toolStripSeparator1,
            this.deleteCmsItem});
			this.cmsGridMenu.Name = "cmsGridMenu";
			this.cmsGridMenu.Size = new System.Drawing.Size(303, 214);
			// 
			// editCmsItem
			// 
			this.editCmsItem.Image = global::Toenda.MovieMatic.Properties.Resources.pencil;
			this.editCmsItem.Name = "editCmsItem";
			this.editCmsItem.Size = new System.Drawing.Size(302, 22);
			this.editCmsItem.Text = "Bearbeiten...";
			this.editCmsItem.Click += new System.EventHandler(this.editCmsItem_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(299, 6);
			// 
			// moviesWithThisActorToolStripMenuItem
			// 
			this.moviesWithThisActorToolStripMenuItem.Image = global::Toenda.MovieMatic.Properties.Resources.magnifier;
			this.moviesWithThisActorToolStripMenuItem.Name = "moviesWithThisActorToolStripMenuItem";
			this.moviesWithThisActorToolStripMenuItem.Size = new System.Drawing.Size(302, 22);
			this.moviesWithThisActorToolStripMenuItem.Text = "Filme mit dieser Person als Schauspieler";
			this.moviesWithThisActorToolStripMenuItem.Click += new System.EventHandler(this.moviesWithThisActorToolStripMenuItem_Click);
			// 
			// moviesWithThisDirectorToolStripMenuItem
			// 
			this.moviesWithThisDirectorToolStripMenuItem.Image = global::Toenda.MovieMatic.Properties.Resources.magnifier;
			this.moviesWithThisDirectorToolStripMenuItem.Name = "moviesWithThisDirectorToolStripMenuItem";
			this.moviesWithThisDirectorToolStripMenuItem.Size = new System.Drawing.Size(302, 22);
			this.moviesWithThisDirectorToolStripMenuItem.Text = "Filme mit dieser Person als Regisseur";
			this.moviesWithThisDirectorToolStripMenuItem.Click += new System.EventHandler(this.moviesWithThisDirectorToolStripMenuItem_Click);
			// 
			// moviesWithThisProducerToolStripMenuItem
			// 
			this.moviesWithThisProducerToolStripMenuItem.Image = global::Toenda.MovieMatic.Properties.Resources.magnifier;
			this.moviesWithThisProducerToolStripMenuItem.Name = "moviesWithThisProducerToolStripMenuItem";
			this.moviesWithThisProducerToolStripMenuItem.Size = new System.Drawing.Size(302, 22);
			this.moviesWithThisProducerToolStripMenuItem.Text = "Filme mit dieser Person als Produzent";
			this.moviesWithThisProducerToolStripMenuItem.Click += new System.EventHandler(this.moviesWithThisProducerToolStripMenuItem_Click);
			// 
			// moviesWithThisMusicianToolStripMenuItem
			// 
			this.moviesWithThisMusicianToolStripMenuItem.Image = global::Toenda.MovieMatic.Properties.Resources.magnifier;
			this.moviesWithThisMusicianToolStripMenuItem.Name = "moviesWithThisMusicianToolStripMenuItem";
			this.moviesWithThisMusicianToolStripMenuItem.Size = new System.Drawing.Size(302, 22);
			this.moviesWithThisMusicianToolStripMenuItem.Text = "Filme mit dieser Person als Musiker";
			this.moviesWithThisMusicianToolStripMenuItem.Click += new System.EventHandler(this.moviesWithThisMusicianToolStripMenuItem_Click);
			// 
			// moviesWithThisCameramanToolStripMenuItem
			// 
			this.moviesWithThisCameramanToolStripMenuItem.Image = global::Toenda.MovieMatic.Properties.Resources.magnifier;
			this.moviesWithThisCameramanToolStripMenuItem.Name = "moviesWithThisCameramanToolStripMenuItem";
			this.moviesWithThisCameramanToolStripMenuItem.Size = new System.Drawing.Size(302, 22);
			this.moviesWithThisCameramanToolStripMenuItem.Text = "Filme mit dieser Person als Kameramann";
			this.moviesWithThisCameramanToolStripMenuItem.Click += new System.EventHandler(this.moviesWithThisCameramanToolStripMenuItem_Click);
			// 
			// moviesWithThisCutterToolStripMenuItem
			// 
			this.moviesWithThisCutterToolStripMenuItem.Image = global::Toenda.MovieMatic.Properties.Resources.magnifier;
			this.moviesWithThisCutterToolStripMenuItem.Name = "moviesWithThisCutterToolStripMenuItem";
			this.moviesWithThisCutterToolStripMenuItem.Size = new System.Drawing.Size(302, 22);
			this.moviesWithThisCutterToolStripMenuItem.Text = "Filme mit dieser Person als Cutter";
			this.moviesWithThisCutterToolStripMenuItem.Click += new System.EventHandler(this.moviesWithThisCutterToolStripMenuItem_Click);
			// 
			// moviesWithThisWriterToolStripMenuItem
			// 
			this.moviesWithThisWriterToolStripMenuItem.Image = global::Toenda.MovieMatic.Properties.Resources.magnifier;
			this.moviesWithThisWriterToolStripMenuItem.Name = "moviesWithThisWriterToolStripMenuItem";
			this.moviesWithThisWriterToolStripMenuItem.Size = new System.Drawing.Size(302, 22);
			this.moviesWithThisWriterToolStripMenuItem.Text = "Filme mit dieser Person als Drehbuch Autor";
			this.moviesWithThisWriterToolStripMenuItem.Click += new System.EventHandler(this.moviesWithThisWriterToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(299, 6);
			// 
			// deleteCmsItem
			// 
			this.deleteCmsItem.Image = global::Toenda.MovieMatic.Properties.Resources.delete;
			this.deleteCmsItem.Name = "deleteCmsItem";
			this.deleteCmsItem.Size = new System.Drawing.Size(302, 22);
			this.deleteCmsItem.Text = "Löschen...";
			this.deleteCmsItem.Click += new System.EventHandler(this.deleteCmsItem_Click);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.LightGray;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.chkWriter);
			this.panel1.Controls.Add(this.chkCutter);
			this.panel1.Controls.Add(this.chkCameraman);
			this.panel1.Controls.Add(this.chkMusician);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.btnHelp);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.btnAddPerson);
			this.panel1.Controls.Add(this.chkProducer);
			this.panel1.Controls.Add(this.chkDirector);
			this.panel1.Controls.Add(this.chkActor);
			this.panel1.Controls.Add(this.txtName);
			this.panel1.Location = new System.Drawing.Point(12, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1100, 63);
			this.panel1.TabIndex = 9;
			// 
			// chkWriter
			// 
			this.chkWriter.AutoSize = true;
			this.chkWriter.Location = new System.Drawing.Point(608, 12);
			this.chkWriter.Name = "chkWriter";
			this.chkWriter.Size = new System.Drawing.Size(15, 14);
			this.chkWriter.TabIndex = 20;
			this.chkWriter.UseVisualStyleBackColor = true;
			this.chkWriter.CheckedChanged += new System.EventHandler(this.chkWriter_CheckedChanged);
			// 
			// chkCutter
			// 
			this.chkCutter.AutoSize = true;
			this.chkCutter.Location = new System.Drawing.Point(565, 12);
			this.chkCutter.Name = "chkCutter";
			this.chkCutter.Size = new System.Drawing.Size(15, 14);
			this.chkCutter.TabIndex = 19;
			this.chkCutter.UseVisualStyleBackColor = true;
			this.chkCutter.CheckedChanged += new System.EventHandler(this.chkCutter_CheckedChanged);
			// 
			// chkCameraman
			// 
			this.chkCameraman.AutoSize = true;
			this.chkCameraman.Location = new System.Drawing.Point(513, 12);
			this.chkCameraman.Name = "chkCameraman";
			this.chkCameraman.Size = new System.Drawing.Size(15, 14);
			this.chkCameraman.TabIndex = 18;
			this.chkCameraman.UseVisualStyleBackColor = true;
			this.chkCameraman.CheckedChanged += new System.EventHandler(this.chkCameraman_CheckedChanged);
			// 
			// chkMusician
			// 
			this.chkMusician.AutoSize = true;
			this.chkMusician.Location = new System.Drawing.Point(457, 12);
			this.chkMusician.Name = "chkMusician";
			this.chkMusician.Size = new System.Drawing.Size(15, 14);
			this.chkMusician.TabIndex = 17;
			this.chkMusician.UseVisualStyleBackColor = true;
			this.chkMusician.CheckedChanged += new System.EventHandler(this.chkMusician_CheckedChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(38, 46);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(445, 13);
			this.label3.TabIndex = 16;
			this.label3.Text = "Aktion: STRG+H oder STRG+N drücken, um Personeneditor mit aktuellem Namen zu öffn" +
				"en.";
			// 
			// btnHelp
			// 
			this.btnHelp.Location = new System.Drawing.Point(3, 10);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(32, 20);
			this.btnHelp.TabIndex = 15;
			this.btnHelp.Text = "?";
			this.btnHelp.UseVisualStyleBackColor = true;
			this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(38, 33);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(415, 13);
			this.label1.TabIndex = 14;
			this.label1.Text = "Filter: Namen eingeben um zu suchen, Checkboxen de/aktivieren um einzuschränken.";
			// 
			// btnAddPerson
			// 
			this.btnAddPerson.BackColor = System.Drawing.SystemColors.Control;
			this.btnAddPerson.Location = new System.Drawing.Point(993, 7);
			this.btnAddPerson.Name = "btnAddPerson";
			this.btnAddPerson.Size = new System.Drawing.Size(98, 23);
			this.btnAddPerson.TabIndex = 13;
			this.btnAddPerson.Text = "Hinzufügen...";
			this.btnAddPerson.UseVisualStyleBackColor = true;
			this.btnAddPerson.Click += new System.EventHandler(this.btnAddPerson_Click);
			// 
			// chkProducer
			// 
			this.chkProducer.AutoSize = true;
			this.chkProducer.Location = new System.Drawing.Point(400, 12);
			this.chkProducer.Name = "chkProducer";
			this.chkProducer.Size = new System.Drawing.Size(15, 14);
			this.chkProducer.TabIndex = 12;
			this.chkProducer.UseVisualStyleBackColor = true;
			this.chkProducer.CheckedChanged += new System.EventHandler(this.chkProducer_CheckedChanged);
			// 
			// chkDirector
			// 
			this.chkDirector.AutoSize = true;
			this.chkDirector.Location = new System.Drawing.Point(339, 12);
			this.chkDirector.Name = "chkDirector";
			this.chkDirector.Size = new System.Drawing.Size(15, 14);
			this.chkDirector.TabIndex = 11;
			this.chkDirector.UseVisualStyleBackColor = true;
			this.chkDirector.CheckedChanged += new System.EventHandler(this.chkDirector_CheckedChanged);
			// 
			// chkActor
			// 
			this.chkActor.AutoSize = true;
			this.chkActor.Location = new System.Drawing.Point(280, 12);
			this.chkActor.Name = "chkActor";
			this.chkActor.Size = new System.Drawing.Size(15, 14);
			this.chkActor.TabIndex = 10;
			this.chkActor.UseVisualStyleBackColor = true;
			this.chkActor.CheckedChanged += new System.EventHandler(this.chkActor_CheckedChanged);
			// 
			// txtName
			// 
			this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtName.Location = new System.Drawing.Point(41, 10);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(215, 20);
			this.txtName.TabIndex = 9;
			this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
			this.txtName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyUp);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 594);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(57, 13);
			this.label2.TabIndex = 10;
			this.label2.Text = "Gefunden:";
			// 
			// lblFound
			// 
			this.lblFound.AutoSize = true;
			this.lblFound.Location = new System.Drawing.Point(72, 594);
			this.lblFound.Name = "lblFound";
			this.lblFound.Size = new System.Drawing.Size(13, 13);
			this.lblFound.TabIndex = 11;
			this.lblFound.Text = "0";
			// 
			// loadDataAsync
			// 
			this.loadDataAsync.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.loadDataAsync_RunWorkerCompleted);
			// 
			// AdministerPersonsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1124, 624);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.dgvPersons);
			this.Controls.Add(this.lblFound);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AdministerPersonsForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Personen verwalten";
			this.Shown += new System.EventHandler(this.AdministerPersonsForm_Shown);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AdministerPersonsForm_KeyDown);
			( (System.ComponentModel.ISupportInitialize)( this.dgvPersons ) ).EndInit();
			this.cmsGridMenu.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.DataGridView dgvPersons;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.CheckBox chkProducer;
		private System.Windows.Forms.CheckBox chkDirector;
		private System.Windows.Forms.CheckBox chkActor;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Button btnAddPerson;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblFound;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Label label3;
		private System.ComponentModel.BackgroundWorker loadDataAsync;
		private System.Windows.Forms.ContextMenuStrip cmsGridMenu;
		private System.Windows.Forms.ToolStripMenuItem editCmsItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem deleteCmsItem;
		private System.Windows.Forms.ToolStripMenuItem moviesWithThisActorToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem moviesWithThisDirectorToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem moviesWithThisProducerToolStripMenuItem;
		private System.Windows.Forms.CheckBox chkMusician;
		private System.Windows.Forms.CheckBox chkCameraman;
		private System.Windows.Forms.CheckBox chkCutter;
		private System.Windows.Forms.CheckBox chkWriter;
		private System.Windows.Forms.ToolStripMenuItem moviesWithThisMusicianToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem moviesWithThisCameramanToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem moviesWithThisCutterToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem moviesWithThisWriterToolStripMenuItem;
		private System.Windows.Forms.DataGridViewTextBoxColumn colID;
		private System.Windows.Forms.DataGridViewTextBoxColumn colName;
		private System.Windows.Forms.DataGridViewCheckBoxColumn colIsActor;
		private System.Windows.Forms.DataGridViewCheckBoxColumn colIsDirector;
		private System.Windows.Forms.DataGridViewCheckBoxColumn colIsProducer;
		private System.Windows.Forms.DataGridViewCheckBoxColumn colIsMusician;
		private System.Windows.Forms.DataGridViewCheckBoxColumn colIsCameraman;
		private System.Windows.Forms.DataGridViewCheckBoxColumn colIsCutter;
		private System.Windows.Forms.DataGridViewCheckBoxColumn colIsWriter;
		private System.Windows.Forms.DataGridViewTextBoxColumn colMovies;
		private System.Windows.Forms.DataGridViewTextBoxColumn colMoviesAsActor;
		private System.Windows.Forms.DataGridViewTextBoxColumn colMoviesAsDirector;
		private System.Windows.Forms.DataGridViewTextBoxColumn colMoviesAsProducer;
		private System.Windows.Forms.DataGridViewTextBoxColumn colMoviesAsMusician;
		private System.Windows.Forms.DataGridViewTextBoxColumn colMoviesAsCameraman;
		private System.Windows.Forms.DataGridViewTextBoxColumn colMoviesAsCutter;
		private System.Windows.Forms.DataGridViewTextBoxColumn colMoviesAsWriter;

	}
}