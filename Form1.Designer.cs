namespace ProtoBoard
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.Toolbar = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.copyToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.pasteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.GridSpacing = new System.Windows.Forms.ToolStripComboBox();
            this.labelGridUnit = new System.Windows.Forms.ToolStripLabel();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRevert = new System.Windows.Forms.ToolStripMenuItem();
            this.menuClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.menuPreferences = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDeleteWithConnections = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.menuUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.menuCut = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.menuRotate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRotateReset = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRotateLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRotateRight = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRotate180 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuView = new System.Windows.Forms.ToolStripMenuItem();
            this.menuZoom = new System.Windows.Forms.ToolStripMenuItem();
            this.menuZoomFit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuZoomIn = new System.Windows.Forms.ToolStripMenuItem();
            this.menuZoomOut = new System.Windows.Forms.ToolStripMenuItem();
            this.menuZoomReset = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPartLabels = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPartLabelsOff = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPartLabelsHovered = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPartLabelsDefault = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPartLabelsOn = new System.Windows.Forms.ToolStripMenuItem();
            this.filterPartsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.CoordsText = new System.Windows.Forms.ToolStripStatusLabel();
            this.ConnectionTypeText = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.PartsTabs = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.PartsHierarchical = new System.Windows.Forms.TreeView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.PartsByCategory = new System.Windows.Forms.TreeView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.panelViaProperties = new System.Windows.Forms.Panel();
            this.labelViaRingRadiusUnit = new System.Windows.Forms.Label();
            this.textViaRingRadius = new System.Windows.Forms.TextBox();
            this.labelViaRingRadius = new System.Windows.Forms.Label();
            this.labelViaHoleRadiusUnit = new System.Windows.Forms.Label();
            this.textViaHoleRadius = new System.Windows.Forms.TextBox();
            this.labelViaHoleRadius = new System.Windows.Forms.Label();
            this.cbViaForm = new System.Windows.Forms.ComboBox();
            this.cbViaConnectionColor = new System.Windows.Forms.ComboBox();
            this.labelViaForm = new System.Windows.Forms.Label();
            this.labelViaConnectionColor = new System.Windows.Forms.Label();
            this.cbViaRingColor = new System.Windows.Forms.ComboBox();
            this.labelViaRingColor = new System.Windows.Forms.Label();
            this.labelViaName = new System.Windows.Forms.Label();
            this.panelWireProperties = new System.Windows.Forms.Panel();
            this.cbWireColor = new System.Windows.Forms.ComboBox();
            this.labelWireColor = new System.Windows.Forms.Label();
            this.labelWireName = new System.Windows.Forms.Label();
            this.panelTrackProperties = new System.Windows.Forms.Panel();
            this.labelTrackWidthUnit = new System.Windows.Forms.Label();
            this.textTrackWidth = new System.Windows.Forms.TextBox();
            this.labelTrackWidth = new System.Windows.Forms.Label();
            this.cbTrackColor = new System.Windows.Forms.ComboBox();
            this.labelTrackColor = new System.Windows.Forms.Label();
            this.labelTrackName = new System.Windows.Forms.Label();
            this.panelPartValue = new System.Windows.Forms.Panel();
            this.cbPartValueUnit = new System.Windows.Forms.ComboBox();
            this.cbPartValue = new System.Windows.Forms.ComboBox();
            this.labelPartValue = new System.Windows.Forms.Label();
            this.panelPartProperties = new System.Windows.Forms.Panel();
            this.labelPartLabel = new System.Windows.Forms.Label();
            this.labelPartName = new System.Windows.Forms.Label();
            this.cbPartLocked = new System.Windows.Forms.CheckBox();
            this.cbShowPartLabel = new System.Windows.Forms.CheckBox();
            this.textPartLabel = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.FilesTab = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.output = new System.Windows.Forms.TextBox();
            this.input = new System.Windows.Forms.TextBox();
            this.searchText = new System.Windows.Forms.TextBox();
            this.panelPinProperties = new System.Windows.Forms.Panel();
            this.labelPinProperties = new System.Windows.Forms.Label();
            this.lbPins = new System.Windows.Forms.ListBox();
            this.labelPinName = new System.Windows.Forms.Label();
            this.textPinName = new System.Windows.Forms.TextBox();
            this.labelPinRadiusUnit = new System.Windows.Forms.Label();
            this.textPinRadius = new System.Windows.Forms.TextBox();
            this.labelPinRadius = new System.Windows.Forms.Label();
            this.cbPinForm = new System.Windows.Forms.ComboBox();
            this.cbPinConnectionColor = new System.Windows.Forms.ComboBox();
            this.labelPinForm = new System.Windows.Forms.Label();
            this.labelPinConnectionColor = new System.Windows.Forms.Label();
            this.Toolbar.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.PartsTabs.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panelViaProperties.SuspendLayout();
            this.panelWireProperties.SuspendLayout();
            this.panelTrackProperties.SuspendLayout();
            this.panelPartValue.SuspendLayout();
            this.panelPartProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.FilesTab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.panelPinProperties.SuspendLayout();
            this.SuspendLayout();
            // 
            // Toolbar
            // 
            this.Toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator,
            this.cutToolStripButton,
            this.copyToolStripButton,
            this.pasteToolStripButton,
            this.toolStripSeparator3,
            this.toolStripLabel1,
            this.GridSpacing,
            this.labelGridUnit});
            this.Toolbar.Location = new System.Drawing.Point(0, 24);
            this.Toolbar.Name = "Toolbar";
            this.Toolbar.Size = new System.Drawing.Size(951, 25);
            this.Toolbar.TabIndex = 4;
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.newToolStripButton.Text = "&New";
            this.newToolStripButton.Click += new System.EventHandler(this.menuNew_Click);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "&Open";
            this.openToolStripButton.Click += new System.EventHandler(this.menuOpen_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "&Save";
            this.saveToolStripButton.Click += new System.EventHandler(this.menuSave_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // cutToolStripButton
            // 
            this.cutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cutToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripButton.Image")));
            this.cutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutToolStripButton.Name = "cutToolStripButton";
            this.cutToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.cutToolStripButton.Text = "C&ut";
            this.cutToolStripButton.Click += new System.EventHandler(this.menuCut_Click);
            // 
            // copyToolStripButton
            // 
            this.copyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copyToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripButton.Image")));
            this.copyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolStripButton.Name = "copyToolStripButton";
            this.copyToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.copyToolStripButton.Text = "&Copy";
            this.copyToolStripButton.Click += new System.EventHandler(this.menuCopy_Click);
            // 
            // pasteToolStripButton
            // 
            this.pasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pasteToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripButton.Image")));
            this.pasteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteToolStripButton.Name = "pasteToolStripButton";
            this.pasteToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.pasteToolStripButton.Text = "&Paste";
            this.pasteToolStripButton.Click += new System.EventHandler(this.menuPaste_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(32, 22);
            this.toolStripLabel1.Text = "Grid:";
            // 
            // GridSpacing
            // 
            this.GridSpacing.Items.AddRange(new object[] {
            "0",
            "0.1",
            "0.5",
            "1",
            "1.27",
            "2",
            "2.54",
            "5",
            "5.1",
            "10"});
            this.GridSpacing.Name = "GridSpacing";
            this.GridSpacing.Size = new System.Drawing.Size(75, 25);
            this.GridSpacing.SelectedIndexChanged += new System.EventHandler(this.GridSpacing_TextChanged);
            this.GridSpacing.TextUpdate += new System.EventHandler(this.GridSpacing_TextChanged);
            // 
            // labelGridUnit
            // 
            this.labelGridUnit.Name = "labelGridUnit";
            this.labelGridUnit.Size = new System.Drawing.Size(29, 22);
            this.labelGridUnit.Text = "mm";
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.menuEdit,
            this.menuView});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(951, 24);
            this.MainMenu.TabIndex = 3;
            this.MainMenu.Text = "MainMenu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNew,
            this.menuOpen,
            this.menuSave,
            this.menuSaveAs,
            this.menuSaveAll,
            this.menuRevert,
            this.menuClose,
            this.toolStripSeparator7,
            this.menuPreferences,
            this.toolStripSeparator1,
            this.menuExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // menuNew
            // 
            this.menuNew.Image = ((System.Drawing.Image)(resources.GetObject("menuNew.Image")));
            this.menuNew.Name = "menuNew";
            this.menuNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.menuNew.Size = new System.Drawing.Size(187, 22);
            this.menuNew.Text = "&New";
            this.menuNew.Click += new System.EventHandler(this.menuNew_Click);
            // 
            // menuOpen
            // 
            this.menuOpen.Image = ((System.Drawing.Image)(resources.GetObject("menuOpen.Image")));
            this.menuOpen.Name = "menuOpen";
            this.menuOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuOpen.Size = new System.Drawing.Size(187, 22);
            this.menuOpen.Text = "&Open";
            this.menuOpen.Click += new System.EventHandler(this.menuOpen_Click);
            // 
            // menuSave
            // 
            this.menuSave.Image = ((System.Drawing.Image)(resources.GetObject("menuSave.Image")));
            this.menuSave.Name = "menuSave";
            this.menuSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuSave.Size = new System.Drawing.Size(187, 22);
            this.menuSave.Text = "&Save";
            this.menuSave.Click += new System.EventHandler(this.menuSave_Click);
            // 
            // menuSaveAs
            // 
            this.menuSaveAs.Name = "menuSaveAs";
            this.menuSaveAs.Size = new System.Drawing.Size(187, 22);
            this.menuSaveAs.Text = "Save &As";
            this.menuSaveAs.Click += new System.EventHandler(this.menuSaveAs_Click);
            // 
            // menuSaveAll
            // 
            this.menuSaveAll.Name = "menuSaveAll";
            this.menuSaveAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.menuSaveAll.Size = new System.Drawing.Size(187, 22);
            this.menuSaveAll.Text = "Save A&ll";
            this.menuSaveAll.Click += new System.EventHandler(this.menuSaveAll_Click);
            // 
            // menuRevert
            // 
            this.menuRevert.Name = "menuRevert";
            this.menuRevert.Size = new System.Drawing.Size(187, 22);
            this.menuRevert.Text = "&Revert";
            this.menuRevert.Click += new System.EventHandler(this.menuRevert_Click);
            // 
            // menuClose
            // 
            this.menuClose.Name = "menuClose";
            this.menuClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.menuClose.Size = new System.Drawing.Size(187, 22);
            this.menuClose.Text = "&Close";
            this.menuClose.Click += new System.EventHandler(this.menuClose_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(184, 6);
            // 
            // menuPreferences
            // 
            this.menuPreferences.Name = "menuPreferences";
            this.menuPreferences.Size = new System.Drawing.Size(187, 22);
            this.menuPreferences.Text = "&Preferences";
            this.menuPreferences.Click += new System.EventHandler(this.menuPreferences_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(184, 6);
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.menuExit.Size = new System.Drawing.Size(187, 22);
            this.menuExit.Text = "E&xit";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // menuEdit
            // 
            this.menuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuDelete,
            this.menuDeleteWithConnections,
            this.toolStripSeparator2,
            this.menuSelectAll,
            this.toolStripSeparator11,
            this.menuUndo,
            this.menuRedo,
            this.toolStripSeparator8,
            this.menuCut,
            this.menuCopy,
            this.menuPaste,
            this.toolStripSeparator9,
            this.menuRotate});
            this.menuEdit.Name = "menuEdit";
            this.menuEdit.Size = new System.Drawing.Size(39, 20);
            this.menuEdit.Text = "&Edit";
            // 
            // menuDelete
            // 
            this.menuDelete.Name = "menuDelete";
            this.menuDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.menuDelete.Size = new System.Drawing.Size(278, 22);
            this.menuDelete.Text = "&Delete part only";
            this.menuDelete.Click += new System.EventHandler(this.menuDelete_Click);
            // 
            // menuDeleteWithConnections
            // 
            this.menuDeleteWithConnections.Name = "menuDeleteWithConnections";
            this.menuDeleteWithConnections.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)));
            this.menuDeleteWithConnections.Size = new System.Drawing.Size(278, 22);
            this.menuDeleteWithConnections.Text = "De&lete part and connections";
            this.menuDeleteWithConnections.Click += new System.EventHandler(this.menuDeleteWithConnections_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(275, 6);
            // 
            // menuSelectAll
            // 
            this.menuSelectAll.Name = "menuSelectAll";
            this.menuSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.menuSelectAll.Size = new System.Drawing.Size(278, 22);
            this.menuSelectAll.Text = "Select &All";
            this.menuSelectAll.Click += new System.EventHandler(this.menuSelectAll_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(275, 6);
            // 
            // menuUndo
            // 
            this.menuUndo.Name = "menuUndo";
            this.menuUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.menuUndo.Size = new System.Drawing.Size(278, 22);
            this.menuUndo.Text = "&Undo";
            this.menuUndo.Click += new System.EventHandler(this.menuUndo_Click);
            // 
            // menuRedo
            // 
            this.menuRedo.Name = "menuRedo";
            this.menuRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.menuRedo.Size = new System.Drawing.Size(278, 22);
            this.menuRedo.Text = "&Redo";
            this.menuRedo.Click += new System.EventHandler(this.menuRedo_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(275, 6);
            // 
            // menuCut
            // 
            this.menuCut.Image = ((System.Drawing.Image)(resources.GetObject("menuCut.Image")));
            this.menuCut.Name = "menuCut";
            this.menuCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.menuCut.Size = new System.Drawing.Size(278, 22);
            this.menuCut.Text = "Cu&t";
            this.menuCut.Click += new System.EventHandler(this.menuCut_Click);
            // 
            // menuCopy
            // 
            this.menuCopy.Image = ((System.Drawing.Image)(resources.GetObject("menuCopy.Image")));
            this.menuCopy.Name = "menuCopy";
            this.menuCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.menuCopy.Size = new System.Drawing.Size(278, 22);
            this.menuCopy.Text = "&Copy";
            this.menuCopy.Click += new System.EventHandler(this.menuCopy_Click);
            // 
            // menuPaste
            // 
            this.menuPaste.Image = ((System.Drawing.Image)(resources.GetObject("menuPaste.Image")));
            this.menuPaste.Name = "menuPaste";
            this.menuPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.menuPaste.Size = new System.Drawing.Size(278, 22);
            this.menuPaste.Text = "&Paste";
            this.menuPaste.Click += new System.EventHandler(this.menuPaste_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(275, 6);
            // 
            // menuRotate
            // 
            this.menuRotate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuRotateReset,
            this.menuRotateLeft,
            this.menuRotateRight,
            this.menuRotate180});
            this.menuRotate.Name = "menuRotate";
            this.menuRotate.Size = new System.Drawing.Size(278, 22);
            this.menuRotate.Text = "R&otate";
            // 
            // menuRotateReset
            // 
            this.menuRotateReset.Name = "menuRotateReset";
            this.menuRotateReset.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.menuRotateReset.Size = new System.Drawing.Size(201, 22);
            this.menuRotateReset.Text = "&Reset";
            this.menuRotateReset.Click += new System.EventHandler(this.menuRotateReset_Click);
            // 
            // menuRotateLeft
            // 
            this.menuRotateLeft.Name = "menuRotateLeft";
            this.menuRotateLeft.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)));
            this.menuRotateLeft.Size = new System.Drawing.Size(201, 22);
            this.menuRotateLeft.Text = "&Left";
            this.menuRotateLeft.Click += new System.EventHandler(this.menuRotateLeft_Click);
            // 
            // menuRotateRight
            // 
            this.menuRotateRight.Name = "menuRotateRight";
            this.menuRotateRight.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)));
            this.menuRotateRight.Size = new System.Drawing.Size(201, 22);
            this.menuRotateRight.Text = "&Right";
            this.menuRotateRight.Click += new System.EventHandler(this.menuRotateRight_Click);
            // 
            // menuRotate180
            // 
            this.menuRotate180.Name = "menuRotate180";
            this.menuRotate180.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.menuRotate180.Size = new System.Drawing.Size(201, 22);
            this.menuRotate180.Text = "&180 degrees";
            this.menuRotate180.Click += new System.EventHandler(this.menuRotate180_Click);
            // 
            // menuView
            // 
            this.menuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuZoom,
            this.menuPartLabels,
            this.filterPartsToolStripMenuItem});
            this.menuView.Name = "menuView";
            this.menuView.Size = new System.Drawing.Size(44, 20);
            this.menuView.Text = "&View";
            // 
            // menuZoom
            // 
            this.menuZoom.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuZoomFit,
            this.menuZoomIn,
            this.menuZoomOut,
            this.menuZoomReset});
            this.menuZoom.Name = "menuZoom";
            this.menuZoom.Size = new System.Drawing.Size(169, 22);
            this.menuZoom.Text = "&Zoom";
            // 
            // menuZoomFit
            // 
            this.menuZoomFit.Name = "menuZoomFit";
            this.menuZoomFit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Multiply)));
            this.menuZoomFit.Size = new System.Drawing.Size(172, 22);
            this.menuZoomFit.Text = "&Fit";
            this.menuZoomFit.Click += new System.EventHandler(this.menuZoomFit_Click);
            // 
            // menuZoomIn
            // 
            this.menuZoomIn.Name = "menuZoomIn";
            this.menuZoomIn.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Add)));
            this.menuZoomIn.Size = new System.Drawing.Size(172, 22);
            this.menuZoomIn.Text = "&In";
            this.menuZoomIn.Click += new System.EventHandler(this.menuZoomIn_Click);
            // 
            // menuZoomOut
            // 
            this.menuZoomOut.Name = "menuZoomOut";
            this.menuZoomOut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Subtract)));
            this.menuZoomOut.Size = new System.Drawing.Size(172, 22);
            this.menuZoomOut.Text = "&Out";
            this.menuZoomOut.Click += new System.EventHandler(this.menuZoomOut_Click);
            // 
            // menuZoomReset
            // 
            this.menuZoomReset.Name = "menuZoomReset";
            this.menuZoomReset.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Divide)));
            this.menuZoomReset.Size = new System.Drawing.Size(172, 22);
            this.menuZoomReset.Text = "&Reset";
            this.menuZoomReset.Click += new System.EventHandler(this.menuZoomReset_Click);
            // 
            // menuPartLabels
            // 
            this.menuPartLabels.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuPartLabelsOff,
            this.menuPartLabelsHovered,
            this.menuPartLabelsDefault,
            this.menuPartLabelsOn});
            this.menuPartLabels.Name = "menuPartLabels";
            this.menuPartLabels.Size = new System.Drawing.Size(169, 22);
            this.menuPartLabels.Text = "Part &Labels";
            // 
            // menuPartLabelsOff
            // 
            this.menuPartLabelsOff.Name = "menuPartLabelsOff";
            this.menuPartLabelsOff.Size = new System.Drawing.Size(145, 22);
            this.menuPartLabelsOff.Text = "Force O&ff";
            this.menuPartLabelsOff.Click += new System.EventHandler(this.menuPartLabelsOff_Click);
            // 
            // menuPartLabelsHovered
            // 
            this.menuPartLabelsHovered.Name = "menuPartLabelsHovered";
            this.menuPartLabelsHovered.Size = new System.Drawing.Size(145, 22);
            this.menuPartLabelsHovered.Text = "&Hovered only";
            this.menuPartLabelsHovered.Click += new System.EventHandler(this.menuPartLabelsHovered_Click);
            // 
            // menuPartLabelsDefault
            // 
            this.menuPartLabelsDefault.Checked = true;
            this.menuPartLabelsDefault.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuPartLabelsDefault.Name = "menuPartLabelsDefault";
            this.menuPartLabelsDefault.Size = new System.Drawing.Size(145, 22);
            this.menuPartLabelsDefault.Text = "&Default";
            this.menuPartLabelsDefault.Click += new System.EventHandler(this.menuPartLabelsDefault_Click);
            // 
            // menuPartLabelsOn
            // 
            this.menuPartLabelsOn.Name = "menuPartLabelsOn";
            this.menuPartLabelsOn.Size = new System.Drawing.Size(145, 22);
            this.menuPartLabelsOn.Text = "Force O&n";
            this.menuPartLabelsOn.Click += new System.EventHandler(this.menuPartLabelsOn_Click);
            // 
            // filterPartsToolStripMenuItem
            // 
            this.filterPartsToolStripMenuItem.Name = "filterPartsToolStripMenuItem";
            this.filterPartsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.filterPartsToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.filterPartsToolStripMenuItem.Text = "&Filter Parts";
            this.filterPartsToolStripMenuItem.Click += new System.EventHandler(this.filterPartsToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusText,
            this.CoordsText,
            this.ConnectionTypeText});
            this.statusStrip1.Location = new System.Drawing.Point(0, 621);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(951, 27);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusText
            // 
            this.StatusText.AutoSize = false;
            this.StatusText.Name = "StatusText";
            this.StatusText.Size = new System.Drawing.Size(770, 22);
            this.StatusText.Spring = true;
            this.StatusText.Text = "Status";
            this.StatusText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CoordsText
            // 
            this.CoordsText.Margin = new System.Windows.Forms.Padding(10, 3, 10, 2);
            this.CoordsText.Name = "CoordsText";
            this.CoordsText.Size = new System.Drawing.Size(45, 22);
            this.CoordsText.Text = "Coords";
            this.CoordsText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ConnectionTypeText
            // 
            this.ConnectionTypeText.Name = "ConnectionTypeText";
            this.ConnectionTypeText.Size = new System.Drawing.Size(101, 22);
            this.ConnectionTypeText.Text = "Connection: Auto";
            this.ConnectionTypeText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.PartsTabs);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.AutoScrollMargin = new System.Drawing.Size(4, 4);
            this.splitContainer2.Panel2.Controls.Add(this.panelPinProperties);
            this.splitContainer2.Panel2.Controls.Add(this.panelViaProperties);
            this.splitContainer2.Panel2.Controls.Add(this.panelWireProperties);
            this.splitContainer2.Panel2.Controls.Add(this.panelTrackProperties);
            this.splitContainer2.Panel2.Controls.Add(this.panelPartValue);
            this.splitContainer2.Panel2.Controls.Add(this.panelPartProperties);
            this.splitContainer2.Size = new System.Drawing.Size(208, 572);
            this.splitContainer2.SplitterDistance = 166;
            this.splitContainer2.TabIndex = 0;
            // 
            // PartsTabs
            // 
            this.PartsTabs.Controls.Add(this.tabPage2);
            this.PartsTabs.Controls.Add(this.tabPage3);
            this.PartsTabs.Controls.Add(this.tabPage4);
            this.PartsTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PartsTabs.Location = new System.Drawing.Point(0, 0);
            this.PartsTabs.Name = "PartsTabs";
            this.PartsTabs.SelectedIndex = 0;
            this.PartsTabs.Size = new System.Drawing.Size(204, 162);
            this.PartsTabs.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.PartsHierarchical);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(196, 136);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "Hierarchical";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // PartsHierarchical
            // 
            this.PartsHierarchical.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PartsHierarchical.Location = new System.Drawing.Point(3, 3);
            this.PartsHierarchical.Name = "PartsHierarchical";
            this.PartsHierarchical.Size = new System.Drawing.Size(190, 130);
            this.PartsHierarchical.TabIndex = 0;
            this.PartsHierarchical.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.PartsHierarchical_ItemDrag);
            this.PartsHierarchical.DoubleClick += new System.EventHandler(this.PartsHierarchical_DoubleClick);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.PartsByCategory);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(192, 134);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.Text = "By category";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // PartsByCategory
            // 
            this.PartsByCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PartsByCategory.Location = new System.Drawing.Point(3, 3);
            this.PartsByCategory.Name = "PartsByCategory";
            this.PartsByCategory.Size = new System.Drawing.Size(186, 128);
            this.PartsByCategory.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(192, 134);
            this.tabPage4.TabIndex = 2;
            this.tabPage4.Text = "Recent";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // panelViaProperties
            // 
            this.panelViaProperties.Controls.Add(this.labelViaRingRadiusUnit);
            this.panelViaProperties.Controls.Add(this.textViaRingRadius);
            this.panelViaProperties.Controls.Add(this.labelViaRingRadius);
            this.panelViaProperties.Controls.Add(this.labelViaHoleRadiusUnit);
            this.panelViaProperties.Controls.Add(this.textViaHoleRadius);
            this.panelViaProperties.Controls.Add(this.labelViaHoleRadius);
            this.panelViaProperties.Controls.Add(this.cbViaForm);
            this.panelViaProperties.Controls.Add(this.cbViaConnectionColor);
            this.panelViaProperties.Controls.Add(this.labelViaForm);
            this.panelViaProperties.Controls.Add(this.labelViaConnectionColor);
            this.panelViaProperties.Controls.Add(this.cbViaRingColor);
            this.panelViaProperties.Controls.Add(this.labelViaRingColor);
            this.panelViaProperties.Controls.Add(this.labelViaName);
            this.panelViaProperties.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelViaProperties.Location = new System.Drawing.Point(0, 230);
            this.panelViaProperties.Name = "panelViaProperties";
            this.panelViaProperties.Size = new System.Drawing.Size(204, 156);
            this.panelViaProperties.TabIndex = 8;
            // 
            // labelViaRingRadiusUnit
            // 
            this.labelViaRingRadiusUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelViaRingRadiusUnit.AutoSize = true;
            this.labelViaRingRadiusUnit.Location = new System.Drawing.Point(180, 133);
            this.labelViaRingRadiusUnit.Name = "labelViaRingRadiusUnit";
            this.labelViaRingRadiusUnit.Size = new System.Drawing.Size(23, 13);
            this.labelViaRingRadiusUnit.TabIndex = 23;
            this.labelViaRingRadiusUnit.Text = "mm";
            // 
            // textViaRingRadius
            // 
            this.textViaRingRadius.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textViaRingRadius.Location = new System.Drawing.Point(78, 130);
            this.textViaRingRadius.Name = "textViaRingRadius";
            this.textViaRingRadius.Size = new System.Drawing.Size(96, 20);
            this.textViaRingRadius.TabIndex = 22;
            this.textViaRingRadius.TextChanged += new System.EventHandler(this.textViaRingRadius_TextChanged);
            // 
            // labelViaRingRadius
            // 
            this.labelViaRingRadius.AutoSize = true;
            this.labelViaRingRadius.Location = new System.Drawing.Point(4, 133);
            this.labelViaRingRadius.Name = "labelViaRingRadius";
            this.labelViaRingRadius.Size = new System.Drawing.Size(68, 13);
            this.labelViaRingRadius.TabIndex = 21;
            this.labelViaRingRadius.Text = "Ring Radius:";
            // 
            // labelViaHoleRadiusUnit
            // 
            this.labelViaHoleRadiusUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelViaHoleRadiusUnit.AutoSize = true;
            this.labelViaHoleRadiusUnit.Location = new System.Drawing.Point(180, 108);
            this.labelViaHoleRadiusUnit.Name = "labelViaHoleRadiusUnit";
            this.labelViaHoleRadiusUnit.Size = new System.Drawing.Size(23, 13);
            this.labelViaHoleRadiusUnit.TabIndex = 20;
            this.labelViaHoleRadiusUnit.Text = "mm";
            // 
            // textViaHoleRadius
            // 
            this.textViaHoleRadius.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textViaHoleRadius.Location = new System.Drawing.Point(78, 105);
            this.textViaHoleRadius.Name = "textViaHoleRadius";
            this.textViaHoleRadius.Size = new System.Drawing.Size(96, 20);
            this.textViaHoleRadius.TabIndex = 19;
            this.textViaHoleRadius.TextChanged += new System.EventHandler(this.textViaHoleRadius_TextChanged);
            // 
            // labelViaHoleRadius
            // 
            this.labelViaHoleRadius.AutoSize = true;
            this.labelViaHoleRadius.Location = new System.Drawing.Point(4, 108);
            this.labelViaHoleRadius.Name = "labelViaHoleRadius";
            this.labelViaHoleRadius.Size = new System.Drawing.Size(68, 13);
            this.labelViaHoleRadius.TabIndex = 18;
            this.labelViaHoleRadius.Text = "Hole Radius:";
            // 
            // cbViaForm
            // 
            this.cbViaForm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbViaForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbViaForm.FormattingEnabled = true;
            this.cbViaForm.Location = new System.Drawing.Point(37, 78);
            this.cbViaForm.Name = "cbViaForm";
            this.cbViaForm.Size = new System.Drawing.Size(166, 21);
            this.cbViaForm.TabIndex = 17;
            this.cbViaForm.SelectedIndexChanged += new System.EventHandler(this.cbViaForm_SelectedIndexChanged);
            // 
            // cbViaConnectionColor
            // 
            this.cbViaConnectionColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbViaConnectionColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbViaConnectionColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbViaConnectionColor.FormattingEnabled = true;
            this.cbViaConnectionColor.Location = new System.Drawing.Point(101, 51);
            this.cbViaConnectionColor.Name = "cbViaConnectionColor";
            this.cbViaConnectionColor.Size = new System.Drawing.Size(102, 21);
            this.cbViaConnectionColor.TabIndex = 13;
            this.cbViaConnectionColor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.DrawColorItem);
            this.cbViaConnectionColor.SelectedIndexChanged += new System.EventHandler(this.cbViaConnectionColor_SelectedIndexChanged);
            // 
            // labelViaForm
            // 
            this.labelViaForm.AutoSize = true;
            this.labelViaForm.Location = new System.Drawing.Point(4, 81);
            this.labelViaForm.Name = "labelViaForm";
            this.labelViaForm.Size = new System.Drawing.Size(33, 13);
            this.labelViaForm.TabIndex = 16;
            this.labelViaForm.Text = "Form:";
            // 
            // labelViaConnectionColor
            // 
            this.labelViaConnectionColor.AutoSize = true;
            this.labelViaConnectionColor.Location = new System.Drawing.Point(4, 55);
            this.labelViaConnectionColor.Name = "labelViaConnectionColor";
            this.labelViaConnectionColor.Size = new System.Drawing.Size(91, 13);
            this.labelViaConnectionColor.TabIndex = 12;
            this.labelViaConnectionColor.Text = "Connection Color:";
            // 
            // cbViaRingColor
            // 
            this.cbViaRingColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbViaRingColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbViaRingColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbViaRingColor.FormattingEnabled = true;
            this.cbViaRingColor.Location = new System.Drawing.Point(69, 26);
            this.cbViaRingColor.Name = "cbViaRingColor";
            this.cbViaRingColor.Size = new System.Drawing.Size(134, 21);
            this.cbViaRingColor.TabIndex = 11;
            this.cbViaRingColor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.DrawColorItem);
            this.cbViaRingColor.SelectedIndexChanged += new System.EventHandler(this.cbViaRingColor_SelectedIndexChanged);
            // 
            // labelViaRingColor
            // 
            this.labelViaRingColor.AutoSize = true;
            this.labelViaRingColor.Location = new System.Drawing.Point(4, 30);
            this.labelViaRingColor.Name = "labelViaRingColor";
            this.labelViaRingColor.Size = new System.Drawing.Size(59, 13);
            this.labelViaRingColor.TabIndex = 10;
            this.labelViaRingColor.Text = "Ring Color:";
            // 
            // labelViaName
            // 
            this.labelViaName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelViaName.AutoEllipsis = true;
            this.labelViaName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelViaName.Location = new System.Drawing.Point(0, 0);
            this.labelViaName.Name = "labelViaName";
            this.labelViaName.Size = new System.Drawing.Size(204, 20);
            this.labelViaName.TabIndex = 9;
            this.labelViaName.Text = "Via properties";
            this.labelViaName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelWireProperties
            // 
            this.panelWireProperties.Controls.Add(this.cbWireColor);
            this.panelWireProperties.Controls.Add(this.labelWireColor);
            this.panelWireProperties.Controls.Add(this.labelWireName);
            this.panelWireProperties.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelWireProperties.Location = new System.Drawing.Point(0, 178);
            this.panelWireProperties.Name = "panelWireProperties";
            this.panelWireProperties.Size = new System.Drawing.Size(204, 52);
            this.panelWireProperties.TabIndex = 6;
            // 
            // cbWireColor
            // 
            this.cbWireColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbWireColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbWireColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWireColor.FormattingEnabled = true;
            this.cbWireColor.Location = new System.Drawing.Point(43, 26);
            this.cbWireColor.Name = "cbWireColor";
            this.cbWireColor.Size = new System.Drawing.Size(160, 21);
            this.cbWireColor.TabIndex = 11;
            this.cbWireColor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.DrawColorItem);
            this.cbWireColor.SelectedIndexChanged += new System.EventHandler(this.cbWireColor_SelectedIndexChanged);
            // 
            // labelWireColor
            // 
            this.labelWireColor.AutoSize = true;
            this.labelWireColor.Location = new System.Drawing.Point(4, 30);
            this.labelWireColor.Name = "labelWireColor";
            this.labelWireColor.Size = new System.Drawing.Size(34, 13);
            this.labelWireColor.TabIndex = 10;
            this.labelWireColor.Text = "Color:";
            // 
            // labelWireName
            // 
            this.labelWireName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelWireName.AutoEllipsis = true;
            this.labelWireName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelWireName.Location = new System.Drawing.Point(0, 0);
            this.labelWireName.Name = "labelWireName";
            this.labelWireName.Size = new System.Drawing.Size(204, 20);
            this.labelWireName.TabIndex = 9;
            this.labelWireName.Text = "Wire properties";
            this.labelWireName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelTrackProperties
            // 
            this.panelTrackProperties.Controls.Add(this.labelTrackWidthUnit);
            this.panelTrackProperties.Controls.Add(this.textTrackWidth);
            this.panelTrackProperties.Controls.Add(this.labelTrackWidth);
            this.panelTrackProperties.Controls.Add(this.cbTrackColor);
            this.panelTrackProperties.Controls.Add(this.labelTrackColor);
            this.panelTrackProperties.Controls.Add(this.labelTrackName);
            this.panelTrackProperties.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTrackProperties.Location = new System.Drawing.Point(0, 99);
            this.panelTrackProperties.Name = "panelTrackProperties";
            this.panelTrackProperties.Size = new System.Drawing.Size(204, 79);
            this.panelTrackProperties.TabIndex = 5;
            // 
            // labelTrackWidthUnit
            // 
            this.labelTrackWidthUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTrackWidthUnit.AutoSize = true;
            this.labelTrackWidthUnit.Location = new System.Drawing.Point(180, 56);
            this.labelTrackWidthUnit.Name = "labelTrackWidthUnit";
            this.labelTrackWidthUnit.Size = new System.Drawing.Size(23, 13);
            this.labelTrackWidthUnit.TabIndex = 14;
            this.labelTrackWidthUnit.Text = "mm";
            // 
            // textTrackWidth
            // 
            this.textTrackWidth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textTrackWidth.Location = new System.Drawing.Point(43, 53);
            this.textTrackWidth.Name = "textTrackWidth";
            this.textTrackWidth.Size = new System.Drawing.Size(131, 20);
            this.textTrackWidth.TabIndex = 13;
            this.textTrackWidth.TextChanged += new System.EventHandler(this.textTrackWidth_TextChanged);
            // 
            // labelTrackWidth
            // 
            this.labelTrackWidth.AutoSize = true;
            this.labelTrackWidth.Location = new System.Drawing.Point(4, 56);
            this.labelTrackWidth.Name = "labelTrackWidth";
            this.labelTrackWidth.Size = new System.Drawing.Size(38, 13);
            this.labelTrackWidth.TabIndex = 12;
            this.labelTrackWidth.Text = "Width:";
            // 
            // cbTrackColor
            // 
            this.cbTrackColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTrackColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbTrackColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTrackColor.FormattingEnabled = true;
            this.cbTrackColor.Location = new System.Drawing.Point(43, 26);
            this.cbTrackColor.Name = "cbTrackColor";
            this.cbTrackColor.Size = new System.Drawing.Size(160, 21);
            this.cbTrackColor.TabIndex = 11;
            this.cbTrackColor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.DrawColorItem);
            this.cbTrackColor.SelectedIndexChanged += new System.EventHandler(this.cbTrackColor_SelectedIndexChanged);
            // 
            // labelTrackColor
            // 
            this.labelTrackColor.AutoSize = true;
            this.labelTrackColor.Location = new System.Drawing.Point(4, 30);
            this.labelTrackColor.Name = "labelTrackColor";
            this.labelTrackColor.Size = new System.Drawing.Size(34, 13);
            this.labelTrackColor.TabIndex = 10;
            this.labelTrackColor.Text = "Color:";
            // 
            // labelTrackName
            // 
            this.labelTrackName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTrackName.AutoEllipsis = true;
            this.labelTrackName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelTrackName.Location = new System.Drawing.Point(0, 0);
            this.labelTrackName.Name = "labelTrackName";
            this.labelTrackName.Size = new System.Drawing.Size(204, 20);
            this.labelTrackName.TabIndex = 9;
            this.labelTrackName.Text = "Track properties";
            this.labelTrackName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelPartValue
            // 
            this.panelPartValue.Controls.Add(this.cbPartValueUnit);
            this.panelPartValue.Controls.Add(this.cbPartValue);
            this.panelPartValue.Controls.Add(this.labelPartValue);
            this.panelPartValue.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPartValue.Location = new System.Drawing.Point(0, 71);
            this.panelPartValue.Name = "panelPartValue";
            this.panelPartValue.Size = new System.Drawing.Size(204, 28);
            this.panelPartValue.TabIndex = 1;
            // 
            // cbPartValueUnit
            // 
            this.cbPartValueUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPartValueUnit.FormattingEnabled = true;
            this.cbPartValueUnit.Location = new System.Drawing.Point(159, 2);
            this.cbPartValueUnit.Name = "cbPartValueUnit";
            this.cbPartValueUnit.Size = new System.Drawing.Size(41, 21);
            this.cbPartValueUnit.TabIndex = 18;
            this.cbPartValueUnit.SelectedIndexChanged += new System.EventHandler(this.cbPartValueUnit_SelectedIndexChanged);
            this.cbPartValueUnit.TextUpdate += new System.EventHandler(this.cbPartValueUnit_TextUpdate);
            // 
            // cbPartValue
            // 
            this.cbPartValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPartValue.FormattingEnabled = true;
            this.cbPartValue.Location = new System.Drawing.Point(78, 2);
            this.cbPartValue.Name = "cbPartValue";
            this.cbPartValue.Size = new System.Drawing.Size(75, 21);
            this.cbPartValue.TabIndex = 17;
            this.cbPartValue.SelectedIndexChanged += new System.EventHandler(this.cbPartValue_SelectedIndexChanged);
            this.cbPartValue.TextUpdate += new System.EventHandler(this.cbPartValue_TextUpdate);
            // 
            // labelPartValue
            // 
            this.labelPartValue.AutoSize = true;
            this.labelPartValue.Location = new System.Drawing.Point(4, 5);
            this.labelPartValue.Name = "labelPartValue";
            this.labelPartValue.Size = new System.Drawing.Size(37, 13);
            this.labelPartValue.TabIndex = 16;
            this.labelPartValue.Text = "Value:";
            // 
            // panelPartProperties
            // 
            this.panelPartProperties.Controls.Add(this.labelPartLabel);
            this.panelPartProperties.Controls.Add(this.labelPartName);
            this.panelPartProperties.Controls.Add(this.cbPartLocked);
            this.panelPartProperties.Controls.Add(this.cbShowPartLabel);
            this.panelPartProperties.Controls.Add(this.textPartLabel);
            this.panelPartProperties.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPartProperties.Location = new System.Drawing.Point(0, 0);
            this.panelPartProperties.Name = "panelPartProperties";
            this.panelPartProperties.Size = new System.Drawing.Size(204, 71);
            this.panelPartProperties.TabIndex = 0;
            // 
            // labelPartLabel
            // 
            this.labelPartLabel.AutoSize = true;
            this.labelPartLabel.Location = new System.Drawing.Point(4, 30);
            this.labelPartLabel.Name = "labelPartLabel";
            this.labelPartLabel.Size = new System.Drawing.Size(36, 13);
            this.labelPartLabel.TabIndex = 8;
            this.labelPartLabel.Text = "Label:";
            // 
            // labelPartName
            // 
            this.labelPartName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPartName.AutoEllipsis = true;
            this.labelPartName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelPartName.Location = new System.Drawing.Point(0, 0);
            this.labelPartName.Name = "labelPartName";
            this.labelPartName.Size = new System.Drawing.Size(204, 20);
            this.labelPartName.TabIndex = 12;
            this.labelPartName.Text = "Part properties";
            this.labelPartName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbPartLocked
            // 
            this.cbPartLocked.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPartLocked.AutoSize = true;
            this.cbPartLocked.Location = new System.Drawing.Point(138, 52);
            this.cbPartLocked.Name = "cbPartLocked";
            this.cbPartLocked.Size = new System.Drawing.Size(62, 17);
            this.cbPartLocked.TabIndex = 11;
            this.cbPartLocked.Text = "Locked";
            this.cbPartLocked.UseVisualStyleBackColor = true;
            this.cbPartLocked.CheckedChanged += new System.EventHandler(this.cbLocked_CheckedChanged);
            // 
            // cbShowPartLabel
            // 
            this.cbShowPartLabel.AutoSize = true;
            this.cbShowPartLabel.Location = new System.Drawing.Point(4, 52);
            this.cbShowPartLabel.Name = "cbShowPartLabel";
            this.cbShowPartLabel.Size = new System.Drawing.Size(82, 17);
            this.cbShowPartLabel.TabIndex = 10;
            this.cbShowPartLabel.Text = "Show Label";
            this.cbShowPartLabel.UseVisualStyleBackColor = true;
            this.cbShowPartLabel.CheckedChanged += new System.EventHandler(this.cbShowLabel_CheckedChanged);
            // 
            // textPartLabel
            // 
            this.textPartLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textPartLabel.Location = new System.Drawing.Point(50, 27);
            this.textPartLabel.Name = "textPartLabel";
            this.textPartLabel.Size = new System.Drawing.Size(153, 20);
            this.textPartLabel.TabIndex = 9;
            this.textPartLabel.TextChanged += new System.EventHandler(this.textPartLabel_TextChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 49);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.FilesTab);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2.Layout += new System.Windows.Forms.LayoutEventHandler(this.splitContainer1_Panel2_Layout);
            this.splitContainer1.Size = new System.Drawing.Size(951, 572);
            this.splitContainer1.SplitterDistance = 739;
            this.splitContainer1.TabIndex = 6;
            // 
            // FilesTab
            // 
            this.FilesTab.Controls.Add(this.tabPage1);
            this.FilesTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilesTab.Location = new System.Drawing.Point(0, 0);
            this.FilesTab.Name = "FilesTab";
            this.FilesTab.SelectedIndex = 0;
            this.FilesTab.ShowToolTips = true;
            this.FilesTab.Size = new System.Drawing.Size(735, 568);
            this.FilesTab.TabIndex = 0;
            this.FilesTab.SelectedIndexChanged += new System.EventHandler(this.FilesTab_SelectedIndexChanged);
            this.FilesTab.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FilesTab_MouseClick);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.splitContainer3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(727, 542);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Console";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.output);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.input);
            this.splitContainer3.Size = new System.Drawing.Size(721, 536);
            this.splitContainer3.SplitterDistance = 499;
            this.splitContainer3.TabIndex = 0;
            // 
            // output
            // 
            this.output.Dock = System.Windows.Forms.DockStyle.Fill;
            this.output.Location = new System.Drawing.Point(0, 0);
            this.output.Multiline = true;
            this.output.Name = "output";
            this.output.ReadOnly = true;
            this.output.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.output.Size = new System.Drawing.Size(721, 499);
            this.output.TabIndex = 0;
            this.output.WordWrap = false;
            // 
            // input
            // 
            this.input.AcceptsReturn = true;
            this.input.Dock = System.Windows.Forms.DockStyle.Fill;
            this.input.Location = new System.Drawing.Point(0, 0);
            this.input.Multiline = true;
            this.input.Name = "input";
            this.input.Size = new System.Drawing.Size(721, 33);
            this.input.TabIndex = 0;
            this.input.WordWrap = false;
            this.input.TextChanged += new System.EventHandler(this.input_TextChanged);
            this.input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.input_KeyDown);
            this.input.KeyUp += new System.Windows.Forms.KeyEventHandler(this.input_KeyUp);
            // 
            // searchText
            // 
            this.searchText.Location = new System.Drawing.Point(601, 23);
            this.searchText.Name = "searchText";
            this.searchText.Size = new System.Drawing.Size(190, 20);
            this.searchText.TabIndex = 7;
            // 
            // panelPinProperties
            // 
            this.panelPinProperties.Controls.Add(this.labelPinRadiusUnit);
            this.panelPinProperties.Controls.Add(this.textPinRadius);
            this.panelPinProperties.Controls.Add(this.labelPinRadius);
            this.panelPinProperties.Controls.Add(this.cbPinForm);
            this.panelPinProperties.Controls.Add(this.cbPinConnectionColor);
            this.panelPinProperties.Controls.Add(this.labelPinForm);
            this.panelPinProperties.Controls.Add(this.labelPinConnectionColor);
            this.panelPinProperties.Controls.Add(this.labelPinName);
            this.panelPinProperties.Controls.Add(this.textPinName);
            this.panelPinProperties.Controls.Add(this.lbPins);
            this.panelPinProperties.Controls.Add(this.labelPinProperties);
            this.panelPinProperties.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPinProperties.Location = new System.Drawing.Point(0, 386);
            this.panelPinProperties.Name = "panelPinProperties";
            this.panelPinProperties.Size = new System.Drawing.Size(204, 228);
            this.panelPinProperties.TabIndex = 9;
            // 
            // labelPinProperties
            // 
            this.labelPinProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPinProperties.AutoEllipsis = true;
            this.labelPinProperties.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelPinProperties.Location = new System.Drawing.Point(0, 0);
            this.labelPinProperties.Name = "labelPinProperties";
            this.labelPinProperties.Size = new System.Drawing.Size(204, 20);
            this.labelPinProperties.TabIndex = 9;
            this.labelPinProperties.Text = "Pins";
            this.labelPinProperties.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbPins
            // 
            this.lbPins.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPins.FormattingEnabled = true;
            this.lbPins.Location = new System.Drawing.Point(0, 24);
            this.lbPins.Name = "lbPins";
            this.lbPins.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbPins.Size = new System.Drawing.Size(203, 95);
            this.lbPins.TabIndex = 10;
            // 
            // labelPinName
            // 
            this.labelPinName.AutoSize = true;
            this.labelPinName.Location = new System.Drawing.Point(2, 124);
            this.labelPinName.Name = "labelPinName";
            this.labelPinName.Size = new System.Drawing.Size(38, 13);
            this.labelPinName.TabIndex = 11;
            this.labelPinName.Text = "Name:";
            // 
            // textPinName
            // 
            this.textPinName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textPinName.Location = new System.Drawing.Point(48, 121);
            this.textPinName.Name = "textPinName";
            this.textPinName.Size = new System.Drawing.Size(153, 20);
            this.textPinName.TabIndex = 12;
            // 
            // labelPinRadiusUnit
            // 
            this.labelPinRadiusUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPinRadiusUnit.AutoSize = true;
            this.labelPinRadiusUnit.Location = new System.Drawing.Point(178, 202);
            this.labelPinRadiusUnit.Name = "labelPinRadiusUnit";
            this.labelPinRadiusUnit.Size = new System.Drawing.Size(23, 13);
            this.labelPinRadiusUnit.TabIndex = 27;
            this.labelPinRadiusUnit.Text = "mm";
            // 
            // textPinRadius
            // 
            this.textPinRadius.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textPinRadius.Location = new System.Drawing.Point(48, 199);
            this.textPinRadius.Name = "textPinRadius";
            this.textPinRadius.Size = new System.Drawing.Size(124, 20);
            this.textPinRadius.TabIndex = 26;
            // 
            // labelPinRadius
            // 
            this.labelPinRadius.AutoSize = true;
            this.labelPinRadius.Location = new System.Drawing.Point(2, 202);
            this.labelPinRadius.Name = "labelPinRadius";
            this.labelPinRadius.Size = new System.Drawing.Size(43, 13);
            this.labelPinRadius.TabIndex = 25;
            this.labelPinRadius.Text = "Radius:";
            // 
            // cbPinForm
            // 
            this.cbPinForm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPinForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPinForm.FormattingEnabled = true;
            this.cbPinForm.Location = new System.Drawing.Point(35, 172);
            this.cbPinForm.Name = "cbPinForm";
            this.cbPinForm.Size = new System.Drawing.Size(166, 21);
            this.cbPinForm.TabIndex = 24;
            // 
            // cbPinConnectionColor
            // 
            this.cbPinConnectionColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPinConnectionColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbPinConnectionColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPinConnectionColor.FormattingEnabled = true;
            this.cbPinConnectionColor.Location = new System.Drawing.Point(99, 145);
            this.cbPinConnectionColor.Name = "cbPinConnectionColor";
            this.cbPinConnectionColor.Size = new System.Drawing.Size(102, 21);
            this.cbPinConnectionColor.TabIndex = 22;
            // 
            // labelPinForm
            // 
            this.labelPinForm.AutoSize = true;
            this.labelPinForm.Location = new System.Drawing.Point(2, 175);
            this.labelPinForm.Name = "labelPinForm";
            this.labelPinForm.Size = new System.Drawing.Size(33, 13);
            this.labelPinForm.TabIndex = 23;
            this.labelPinForm.Text = "Form:";
            // 
            // labelPinConnectionColor
            // 
            this.labelPinConnectionColor.AutoSize = true;
            this.labelPinConnectionColor.Location = new System.Drawing.Point(2, 149);
            this.labelPinConnectionColor.Name = "labelPinConnectionColor";
            this.labelPinConnectionColor.Size = new System.Drawing.Size(91, 13);
            this.labelPinConnectionColor.TabIndex = 21;
            this.labelPinConnectionColor.Text = "Connection Color:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 648);
            this.Controls.Add(this.searchText);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.Toolbar);
            this.Controls.Add(this.MainMenu);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "ProtoBoard";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.Toolbar.ResumeLayout(false);
            this.Toolbar.PerformLayout();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.PartsTabs.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.panelViaProperties.ResumeLayout(false);
            this.panelViaProperties.PerformLayout();
            this.panelWireProperties.ResumeLayout(false);
            this.panelWireProperties.PerformLayout();
            this.panelTrackProperties.ResumeLayout(false);
            this.panelTrackProperties.PerformLayout();
            this.panelPartValue.ResumeLayout(false);
            this.panelPartValue.PerformLayout();
            this.panelPartProperties.ResumeLayout(false);
            this.panelPartProperties.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.FilesTab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.panelPinProperties.ResumeLayout(false);
            this.panelPinProperties.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip Toolbar;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton cutToolStripButton;
        private System.Windows.Forms.ToolStripButton copyToolStripButton;
        private System.Windows.Forms.ToolStripButton pasteToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem menuView;
        private System.Windows.Forms.ToolStripMenuItem menuZoom;
        private System.Windows.Forms.ToolStripMenuItem menuZoomFit;
        private System.Windows.Forms.ToolStripMenuItem menuZoomIn;
        private System.Windows.Forms.ToolStripMenuItem menuZoomOut;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuNew;
        private System.Windows.Forms.ToolStripMenuItem menuOpen;
        private System.Windows.Forms.ToolStripMenuItem menuSave;
        private System.Windows.Forms.ToolStripMenuItem menuSaveAs;
        private System.Windows.Forms.ToolStripMenuItem menuRevert;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.ToolStripMenuItem menuEdit;
        private System.Windows.Forms.ToolStripMenuItem menuUndo;
        private System.Windows.Forms.ToolStripMenuItem menuRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem menuCut;
        private System.Windows.Forms.ToolStripMenuItem menuCopy;
        private System.Windows.Forms.ToolStripMenuItem menuPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem menuDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem menuRotate;
        private System.Windows.Forms.ToolStripMenuItem menuRotateReset;
        private System.Windows.Forms.ToolStripMenuItem menuRotateLeft;
        private System.Windows.Forms.ToolStripMenuItem menuRotateRight;
        private System.Windows.Forms.ToolStripMenuItem menuRotate180;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuPreferences;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl FilesTab;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabControl PartsTabs;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TreeView PartsHierarchical;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TreeView PartsByCategory;
        private System.Windows.Forms.ToolStripMenuItem menuClose;
        private System.Windows.Forms.ToolStripMenuItem menuSaveAll;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TextBox input;
        private System.Windows.Forms.TextBox output;
        private System.Windows.Forms.TextBox searchText;
        private System.Windows.Forms.ToolStripMenuItem filterPartsToolStripMenuItem;
        public System.Windows.Forms.ToolStripStatusLabel StatusText;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel labelGridUnit;
        private System.Windows.Forms.ToolStripComboBox GridSpacing;
        private System.Windows.Forms.ToolStripStatusLabel ConnectionTypeText;
        private System.Windows.Forms.ToolStripMenuItem menuDeleteWithConnections;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menuSelectAll;
        private System.Windows.Forms.ToolStripStatusLabel CoordsText;
        private System.Windows.Forms.ToolStripMenuItem menuZoomReset;
        private System.Windows.Forms.ToolStripMenuItem menuPartLabels;
        private System.Windows.Forms.ToolStripMenuItem menuPartLabelsDefault;
        private System.Windows.Forms.ToolStripMenuItem menuPartLabelsOff;
        private System.Windows.Forms.ToolStripMenuItem menuPartLabelsOn;
        private System.Windows.Forms.ToolStripMenuItem menuPartLabelsHovered;
        private System.Windows.Forms.Panel panelPartProperties;
        private System.Windows.Forms.Label labelPartName;
        private System.Windows.Forms.CheckBox cbPartLocked;
        private System.Windows.Forms.CheckBox cbShowPartLabel;
        private System.Windows.Forms.TextBox textPartLabel;
        private System.Windows.Forms.Label labelPartLabel;
        private System.Windows.Forms.Panel panelPartValue;
        private System.Windows.Forms.ComboBox cbPartValueUnit;
        private System.Windows.Forms.ComboBox cbPartValue;
        private System.Windows.Forms.Label labelPartValue;
        private System.Windows.Forms.Panel panelWireProperties;
        private System.Windows.Forms.ComboBox cbWireColor;
        private System.Windows.Forms.Label labelWireColor;
        private System.Windows.Forms.Label labelWireName;
        private System.Windows.Forms.Panel panelTrackProperties;
        private System.Windows.Forms.Label labelTrackWidthUnit;
        private System.Windows.Forms.TextBox textTrackWidth;
        private System.Windows.Forms.Label labelTrackWidth;
        private System.Windows.Forms.ComboBox cbTrackColor;
        private System.Windows.Forms.Label labelTrackColor;
        private System.Windows.Forms.Label labelTrackName;
        private System.Windows.Forms.Panel panelViaProperties;
        private System.Windows.Forms.Label labelViaRingRadiusUnit;
        private System.Windows.Forms.TextBox textViaRingRadius;
        private System.Windows.Forms.Label labelViaRingRadius;
        private System.Windows.Forms.Label labelViaHoleRadiusUnit;
        private System.Windows.Forms.TextBox textViaHoleRadius;
        private System.Windows.Forms.Label labelViaHoleRadius;
        private System.Windows.Forms.ComboBox cbViaForm;
        private System.Windows.Forms.ComboBox cbViaConnectionColor;
        private System.Windows.Forms.Label labelViaForm;
        private System.Windows.Forms.Label labelViaConnectionColor;
        private System.Windows.Forms.ComboBox cbViaRingColor;
        private System.Windows.Forms.Label labelViaRingColor;
        private System.Windows.Forms.Label labelViaName;
        private System.Windows.Forms.Panel panelPinProperties;
        private System.Windows.Forms.Label labelPinProperties;
        private System.Windows.Forms.Label labelPinRadiusUnit;
        private System.Windows.Forms.TextBox textPinRadius;
        private System.Windows.Forms.Label labelPinRadius;
        private System.Windows.Forms.ComboBox cbPinForm;
        private System.Windows.Forms.ComboBox cbPinConnectionColor;
        private System.Windows.Forms.Label labelPinForm;
        private System.Windows.Forms.Label labelPinConnectionColor;
        private System.Windows.Forms.Label labelPinName;
        private System.Windows.Forms.TextBox textPinName;
        private System.Windows.Forms.ListBox lbPins;

    }
}

