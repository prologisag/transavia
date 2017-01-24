namespace Buchung4U
{
    partial class Buchung3
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitTopMain = new System.Windows.Forms.SplitContainer();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblLoggedIn = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblAgentName = new System.Windows.Forms.Label();
            this.btnLogon = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtAgentName = new System.Windows.Forms.TextBox();
            this.splitNavContent = new System.Windows.Forms.SplitContainer();
            this.btnGetSeatAvailability = new System.Windows.Forms.Button();
            this.btnNavGetBookingFromState = new System.Windows.Forms.Button();
            this.btnNavAXSTest = new System.Windows.Forms.Button();
            this.btnNavAddPayment = new System.Windows.Forms.Button();
            this.btnNavFindBooking = new System.Windows.Forms.Button();
            this.btnNavClear = new System.Windows.Forms.Button();
            this.btnNavCommit = new System.Windows.Forms.Button();
            this.btnNavSell = new System.Windows.Forms.Button();
            this.btnNavPriceItinerary = new System.Windows.Forms.Button();
            this.btnNavGetAvailability = new System.Windows.Forms.Button();
            this.tabAPIMethods = new System.Windows.Forms.TabControl();
            this.tabGetAvailability = new System.Windows.Forms.TabPage();
            this.lblPromotionCode = new System.Windows.Forms.Label();
            this.txtPromotionCode = new System.Windows.Forms.TextBox();
            this.lblOrganizationCode = new System.Windows.Forms.Label();
            this.txtOrganizationCode = new System.Windows.Forms.TextBox();
            this.lblProductClass = new System.Windows.Forms.Label();
            this.txtProductClass = new System.Windows.Forms.TextBox();
            this.txtArrivalStation = new System.Windows.Forms.TextBox();
            this.lblArrivalStation = new System.Windows.Forms.Label();
            this.lblDepartureStation = new System.Windows.Forms.Label();
            this.txtDepartureStation = new System.Windows.Forms.TextBox();
            this.btnGetAvailability = new System.Windows.Forms.Button();
            this.lblInfants = new System.Windows.Forms.Label();
            this.numericInfants = new System.Windows.Forms.NumericUpDown();
            this.lblChilds = new System.Windows.Forms.Label();
            this.numericChilds = new System.Windows.Forms.NumericUpDown();
            this.lblAdults = new System.Windows.Forms.Label();
            this.numericAdults = new System.Windows.Forms.NumericUpDown();
            this.lblTravellers = new System.Windows.Forms.Label();
            this.dateInbound = new System.Windows.Forms.DateTimePicker();
            this.lblInbound = new System.Windows.Forms.Label();
            this.lblOutbound = new System.Windows.Forms.Label();
            this.dateOutbound = new System.Windows.Forms.DateTimePicker();
            this.radioOneway = new System.Windows.Forms.RadioButton();
            this.lblRoute = new System.Windows.Forms.Label();
            this.radioReturn = new System.Windows.Forms.RadioButton();
            this.tabPriceItinerary = new System.Windows.Forms.TabPage();
            this.lblDetailSelectedFareRuleInbound = new System.Windows.Forms.Label();
            this.lblDetailSelectedFareRuleOutbound = new System.Windows.Forms.Label();
            this.lblFareRuleInbound = new System.Windows.Forms.Label();
            this.lblFareRuleOutbound = new System.Windows.Forms.Label();
            this.richTxtFareRuleInbound = new System.Windows.Forms.RichTextBox();
            this.richTxtFareRuleOutbound = new System.Windows.Forms.RichTextBox();
            this.lblSelectInboundFlight = new System.Windows.Forms.Label();
            this.lblSelectOutboundFlight = new System.Windows.Forms.Label();
            this.dataGridViewAvailabilityInbound = new System.Windows.Forms.DataGridView();
            this.dataGridViewAvailabilityOutbound = new System.Windows.Forms.DataGridView();
            this.lblDetailSelectedInboundFlight = new System.Windows.Forms.Label();
            this.lblDetailSelectedOutboundFlight = new System.Windows.Forms.Label();
            this.lblSelectedInboundFlight = new System.Windows.Forms.Label();
            this.lblSelectedOutboundFlight = new System.Windows.Forms.Label();
            this.btnAPIPriceItinerary = new System.Windows.Forms.Button();
            this.tabSell = new System.Windows.Forms.TabPage();
            this.btnAPISellSSR = new System.Windows.Forms.Button();
            this.btnAPISellFlight = new System.Windows.Forms.Button();
            this.tabAddPayment = new System.Windows.Forms.TabPage();
            this.btnAPIAddPayment = new System.Windows.Forms.Button();
            this.tabCommit = new System.Windows.Forms.TabPage();
            this.btnAPICommit = new System.Windows.Forms.Button();
            this.tabGetBookingPayment = new System.Windows.Forms.TabPage();
            this.tabAusgabefenster = new System.Windows.Forms.TabPage();
            this.txtOutputWindow = new System.Windows.Forms.TextBox();
            this.tabFindBooking = new System.Windows.Forms.TabPage();
            this.txtFindByDetail = new System.Windows.Forms.TextBox();
            this.comboBoxFindBookingBy = new System.Windows.Forms.ComboBox();
            this.lblFindBookingBy = new System.Windows.Forms.Label();
            this.dataGridViewFindBooking = new System.Windows.Forms.DataGridView();
            this.btnAPIFindBooking = new System.Windows.Forms.Button();
            this.tabAXSTest = new System.Windows.Forms.TabPage();
            this.btnAPIGetBooking = new System.Windows.Forms.Button();
            this.btnAddBaggage = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRetrievePNRforAXS = new System.Windows.Forms.Button();
            this.txtRetrievePNRforAXS = new System.Windows.Forms.TextBox();
            this.tabAssignSeat = new System.Windows.Forms.TabPage();
            this.txtUnitDesignator = new System.Windows.Forms.TextBox();
            this.txtPNRAssignSeat = new System.Windows.Forms.TextBox();
            this.btnAssignSeat = new System.Windows.Forms.Button();
            this.splitTopMain.Panel1.SuspendLayout();
            this.splitTopMain.Panel2.SuspendLayout();
            this.splitTopMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.splitNavContent.Panel1.SuspendLayout();
            this.splitNavContent.Panel2.SuspendLayout();
            this.splitNavContent.SuspendLayout();
            this.tabAPIMethods.SuspendLayout();
            this.tabGetAvailability.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericInfants)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericChilds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAdults)).BeginInit();
            this.tabPriceItinerary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAvailabilityInbound)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAvailabilityOutbound)).BeginInit();
            this.tabSell.SuspendLayout();
            this.tabAddPayment.SuspendLayout();
            this.tabCommit.SuspendLayout();
            this.tabAusgabefenster.SuspendLayout();
            this.tabFindBooking.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFindBooking)).BeginInit();
            this.tabAXSTest.SuspendLayout();
            this.tabAssignSeat.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 506);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(767, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // splitTopMain
            // 
            this.splitTopMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitTopMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitTopMain.IsSplitterFixed = true;
            this.splitTopMain.Location = new System.Drawing.Point(0, 0);
            this.splitTopMain.Margin = new System.Windows.Forms.Padding(0);
            this.splitTopMain.Name = "splitTopMain";
            this.splitTopMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitTopMain.Panel1
            // 
            this.splitTopMain.Panel1.BackColor = System.Drawing.Color.White;
            this.splitTopMain.Panel1.Controls.Add(this.pictureBox1);
            this.splitTopMain.Panel1.Controls.Add(this.lblLoggedIn);
            this.splitTopMain.Panel1.Controls.Add(this.lblPassword);
            this.splitTopMain.Panel1.Controls.Add(this.lblAgentName);
            this.splitTopMain.Panel1.Controls.Add(this.btnLogon);
            this.splitTopMain.Panel1.Controls.Add(this.txtPassword);
            this.splitTopMain.Panel1.Controls.Add(this.txtAgentName);
            // 
            // splitTopMain.Panel2
            // 
            this.splitTopMain.Panel2.Controls.Add(this.splitNavContent);
            this.splitTopMain.Size = new System.Drawing.Size(767, 506);
            this.splitTopMain.SplitterDistance = 60;
            this.splitTopMain.SplitterWidth = 1;
            this.splitTopMain.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Location = new System.Drawing.Point(611, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(156, 60);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // lblLoggedIn
            // 
            this.lblLoggedIn.AutoSize = true;
            this.lblLoggedIn.Location = new System.Drawing.Point(12, 29);
            this.lblLoggedIn.Name = "lblLoggedIn";
            this.lblLoggedIn.Size = new System.Drawing.Size(118, 13);
            this.lblLoggedIn.TabIndex = 5;
            this.lblLoggedIn.Text = "Confirmation after logon";
            this.lblLoggedIn.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblLoggedIn.Visible = false;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(130, 9);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "Password:";
            // 
            // lblAgentName
            // 
            this.lblAgentName.AutoSize = true;
            this.lblAgentName.Location = new System.Drawing.Point(12, 9);
            this.lblAgentName.Name = "lblAgentName";
            this.lblAgentName.Size = new System.Drawing.Size(66, 13);
            this.lblAgentName.TabIndex = 3;
            this.lblAgentName.Text = "AgentName:";
            // 
            // btnLogon
            // 
            this.btnLogon.Location = new System.Drawing.Point(251, 24);
            this.btnLogon.Name = "btnLogon";
            this.btnLogon.Size = new System.Drawing.Size(75, 23);
            this.btnLogon.TabIndex = 2;
            this.btnLogon.Text = "Logon";
            this.btnLogon.UseVisualStyleBackColor = true;
            this.btnLogon.Click += new System.EventHandler(this.btnLogon_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(133, 26);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(100, 20);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // txtAgentName
            // 
            this.txtAgentName.Location = new System.Drawing.Point(15, 26);
            this.txtAgentName.Name = "txtAgentName";
            this.txtAgentName.Size = new System.Drawing.Size(100, 20);
            this.txtAgentName.TabIndex = 0;
            // 
            // splitNavContent
            // 
            this.splitNavContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitNavContent.IsSplitterFixed = true;
            this.splitNavContent.Location = new System.Drawing.Point(0, 0);
            this.splitNavContent.Margin = new System.Windows.Forms.Padding(0);
            this.splitNavContent.Name = "splitNavContent";
            // 
            // splitNavContent.Panel1
            // 
            this.splitNavContent.Panel1.Controls.Add(this.btnGetSeatAvailability);
            this.splitNavContent.Panel1.Controls.Add(this.btnNavGetBookingFromState);
            this.splitNavContent.Panel1.Controls.Add(this.btnNavAXSTest);
            this.splitNavContent.Panel1.Controls.Add(this.btnNavAddPayment);
            this.splitNavContent.Panel1.Controls.Add(this.btnNavFindBooking);
            this.splitNavContent.Panel1.Controls.Add(this.btnNavClear);
            this.splitNavContent.Panel1.Controls.Add(this.btnNavCommit);
            this.splitNavContent.Panel1.Controls.Add(this.btnNavSell);
            this.splitNavContent.Panel1.Controls.Add(this.btnNavPriceItinerary);
            this.splitNavContent.Panel1.Controls.Add(this.btnNavGetAvailability);
            // 
            // splitNavContent.Panel2
            // 
            this.splitNavContent.Panel2.Controls.Add(this.tabAPIMethods);
            this.splitNavContent.Size = new System.Drawing.Size(767, 445);
            this.splitNavContent.SplitterDistance = 150;
            this.splitNavContent.SplitterWidth = 1;
            this.splitNavContent.TabIndex = 0;
            // 
            // btnGetSeatAvailability
            // 
            this.btnGetSeatAvailability.Location = new System.Drawing.Point(15, 247);
            this.btnGetSeatAvailability.Name = "btnGetSeatAvailability";
            this.btnGetSeatAvailability.Size = new System.Drawing.Size(100, 23);
            this.btnGetSeatAvailability.TabIndex = 9;
            this.btnGetSeatAvailability.Text = "Seat Availability";
            this.btnGetSeatAvailability.UseVisualStyleBackColor = true;
            this.btnGetSeatAvailability.Click += new System.EventHandler(this.btnGetSeatAvailability_Click);
            // 
            // btnNavGetBookingFromState
            // 
            this.btnNavGetBookingFromState.Location = new System.Drawing.Point(15, 217);
            this.btnNavGetBookingFromState.Name = "btnNavGetBookingFromState";
            this.btnNavGetBookingFromState.Size = new System.Drawing.Size(100, 23);
            this.btnNavGetBookingFromState.TabIndex = 8;
            this.btnNavGetBookingFromState.Text = "GetBooking State";
            this.btnNavGetBookingFromState.UseVisualStyleBackColor = true;
            this.btnNavGetBookingFromState.Click += new System.EventHandler(this.btnNavGetBookingFromState_Click);
            // 
            // btnNavAXSTest
            // 
            this.btnNavAXSTest.Enabled = false;
            this.btnNavAXSTest.Location = new System.Drawing.Point(15, 316);
            this.btnNavAXSTest.Name = "btnNavAXSTest";
            this.btnNavAXSTest.Size = new System.Drawing.Size(100, 23);
            this.btnNavAXSTest.TabIndex = 7;
            this.btnNavAXSTest.Text = "AXS Test";
            this.btnNavAXSTest.UseVisualStyleBackColor = true;
            this.btnNavAXSTest.Click += new System.EventHandler(this.btnNavAXSTest_Click);
            // 
            // btnNavAddPayment
            // 
            this.btnNavAddPayment.Enabled = false;
            this.btnNavAddPayment.Location = new System.Drawing.Point(15, 109);
            this.btnNavAddPayment.Name = "btnNavAddPayment";
            this.btnNavAddPayment.Size = new System.Drawing.Size(100, 23);
            this.btnNavAddPayment.TabIndex = 6;
            this.btnNavAddPayment.Text = "Add Payment";
            this.btnNavAddPayment.UseVisualStyleBackColor = true;
            this.btnNavAddPayment.Click += new System.EventHandler(this.btnAddPayment_Click);
            // 
            // btnNavFindBooking
            // 
            this.btnNavFindBooking.Enabled = false;
            this.btnNavFindBooking.Location = new System.Drawing.Point(15, 287);
            this.btnNavFindBooking.Name = "btnNavFindBooking";
            this.btnNavFindBooking.Size = new System.Drawing.Size(100, 23);
            this.btnNavFindBooking.TabIndex = 5;
            this.btnNavFindBooking.Text = "Find Booking";
            this.btnNavFindBooking.UseVisualStyleBackColor = true;
            this.btnNavFindBooking.Click += new System.EventHandler(this.btnNavFindBooking_Click);
            // 
            // btnNavClear
            // 
            this.btnNavClear.Enabled = false;
            this.btnNavClear.Location = new System.Drawing.Point(15, 405);
            this.btnNavClear.Name = "btnNavClear";
            this.btnNavClear.Size = new System.Drawing.Size(100, 23);
            this.btnNavClear.TabIndex = 4;
            this.btnNavClear.Text = "Clear";
            this.btnNavClear.UseVisualStyleBackColor = true;
            this.btnNavClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnNavCommit
            // 
            this.btnNavCommit.Enabled = false;
            this.btnNavCommit.Location = new System.Drawing.Point(15, 155);
            this.btnNavCommit.Name = "btnNavCommit";
            this.btnNavCommit.Size = new System.Drawing.Size(100, 23);
            this.btnNavCommit.TabIndex = 3;
            this.btnNavCommit.Text = "Commit";
            this.btnNavCommit.UseVisualStyleBackColor = true;
            this.btnNavCommit.Click += new System.EventHandler(this.btnCommit_Click);
            // 
            // btnNavSell
            // 
            this.btnNavSell.Enabled = false;
            this.btnNavSell.Location = new System.Drawing.Point(15, 80);
            this.btnNavSell.Name = "btnNavSell";
            this.btnNavSell.Size = new System.Drawing.Size(100, 23);
            this.btnNavSell.TabIndex = 2;
            this.btnNavSell.Text = "Sell / Sell SSR";
            this.btnNavSell.UseVisualStyleBackColor = true;
            this.btnNavSell.Click += new System.EventHandler(this.btnSell_Click);
            // 
            // btnNavPriceItinerary
            // 
            this.btnNavPriceItinerary.Enabled = false;
            this.btnNavPriceItinerary.Location = new System.Drawing.Point(15, 51);
            this.btnNavPriceItinerary.Name = "btnNavPriceItinerary";
            this.btnNavPriceItinerary.Size = new System.Drawing.Size(100, 23);
            this.btnNavPriceItinerary.TabIndex = 1;
            this.btnNavPriceItinerary.Text = "Price Itinerary";
            this.btnNavPriceItinerary.UseVisualStyleBackColor = true;
            this.btnNavPriceItinerary.Click += new System.EventHandler(this.btnPriceItinerary_Click);
            // 
            // btnNavGetAvailability
            // 
            this.btnNavGetAvailability.Enabled = false;
            this.btnNavGetAvailability.Location = new System.Drawing.Point(15, 22);
            this.btnNavGetAvailability.Name = "btnNavGetAvailability";
            this.btnNavGetAvailability.Size = new System.Drawing.Size(100, 23);
            this.btnNavGetAvailability.TabIndex = 0;
            this.btnNavGetAvailability.Text = "Get Availability";
            this.btnNavGetAvailability.UseVisualStyleBackColor = true;
            this.btnNavGetAvailability.Click += new System.EventHandler(this.btnGetAvailability_Click);
            // 
            // tabAPIMethods
            // 
            this.tabAPIMethods.Controls.Add(this.tabGetAvailability);
            this.tabAPIMethods.Controls.Add(this.tabPriceItinerary);
            this.tabAPIMethods.Controls.Add(this.tabSell);
            this.tabAPIMethods.Controls.Add(this.tabAddPayment);
            this.tabAPIMethods.Controls.Add(this.tabCommit);
            this.tabAPIMethods.Controls.Add(this.tabGetBookingPayment);
            this.tabAPIMethods.Controls.Add(this.tabAusgabefenster);
            this.tabAPIMethods.Controls.Add(this.tabFindBooking);
            this.tabAPIMethods.Controls.Add(this.tabAXSTest);
            this.tabAPIMethods.Controls.Add(this.tabAssignSeat);
            this.tabAPIMethods.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabAPIMethods.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tabAPIMethods.Location = new System.Drawing.Point(0, 0);
            this.tabAPIMethods.Name = "tabAPIMethods";
            this.tabAPIMethods.SelectedIndex = 0;
            this.tabAPIMethods.Size = new System.Drawing.Size(616, 445);
            this.tabAPIMethods.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabAPIMethods.TabIndex = 0;
            // 
            // tabGetAvailability
            // 
            this.tabGetAvailability.Controls.Add(this.lblPromotionCode);
            this.tabGetAvailability.Controls.Add(this.txtPromotionCode);
            this.tabGetAvailability.Controls.Add(this.lblOrganizationCode);
            this.tabGetAvailability.Controls.Add(this.txtOrganizationCode);
            this.tabGetAvailability.Controls.Add(this.lblProductClass);
            this.tabGetAvailability.Controls.Add(this.txtProductClass);
            this.tabGetAvailability.Controls.Add(this.txtArrivalStation);
            this.tabGetAvailability.Controls.Add(this.lblArrivalStation);
            this.tabGetAvailability.Controls.Add(this.lblDepartureStation);
            this.tabGetAvailability.Controls.Add(this.txtDepartureStation);
            this.tabGetAvailability.Controls.Add(this.btnGetAvailability);
            this.tabGetAvailability.Controls.Add(this.lblInfants);
            this.tabGetAvailability.Controls.Add(this.numericInfants);
            this.tabGetAvailability.Controls.Add(this.lblChilds);
            this.tabGetAvailability.Controls.Add(this.numericChilds);
            this.tabGetAvailability.Controls.Add(this.lblAdults);
            this.tabGetAvailability.Controls.Add(this.numericAdults);
            this.tabGetAvailability.Controls.Add(this.lblTravellers);
            this.tabGetAvailability.Controls.Add(this.dateInbound);
            this.tabGetAvailability.Controls.Add(this.lblInbound);
            this.tabGetAvailability.Controls.Add(this.lblOutbound);
            this.tabGetAvailability.Controls.Add(this.dateOutbound);
            this.tabGetAvailability.Controls.Add(this.radioOneway);
            this.tabGetAvailability.Controls.Add(this.lblRoute);
            this.tabGetAvailability.Controls.Add(this.radioReturn);
            this.tabGetAvailability.Location = new System.Drawing.Point(4, 22);
            this.tabGetAvailability.Name = "tabGetAvailability";
            this.tabGetAvailability.Padding = new System.Windows.Forms.Padding(3);
            this.tabGetAvailability.Size = new System.Drawing.Size(608, 419);
            this.tabGetAvailability.TabIndex = 0;
            this.tabGetAvailability.Text = "Get Availability";
            this.tabGetAvailability.UseVisualStyleBackColor = true;
            // 
            // lblPromotionCode
            // 
            this.lblPromotionCode.AutoSize = true;
            this.lblPromotionCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPromotionCode.Location = new System.Drawing.Point(146, 179);
            this.lblPromotionCode.Name = "lblPromotionCode";
            this.lblPromotionCode.Size = new System.Drawing.Size(96, 13);
            this.lblPromotionCode.TabIndex = 24;
            this.lblPromotionCode.Text = "Promotion Code";
            // 
            // txtPromotionCode
            // 
            this.txtPromotionCode.Location = new System.Drawing.Point(149, 195);
            this.txtPromotionCode.Name = "txtPromotionCode";
            this.txtPromotionCode.Size = new System.Drawing.Size(116, 20);
            this.txtPromotionCode.TabIndex = 23;
            // 
            // lblOrganizationCode
            // 
            this.lblOrganizationCode.AutoSize = true;
            this.lblOrganizationCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrganizationCode.Location = new System.Drawing.Point(298, 119);
            this.lblOrganizationCode.Name = "lblOrganizationCode";
            this.lblOrganizationCode.Size = new System.Drawing.Size(111, 13);
            this.lblOrganizationCode.TabIndex = 22;
            this.lblOrganizationCode.Text = "Organization Code";
            // 
            // txtOrganizationCode
            // 
            this.txtOrganizationCode.Location = new System.Drawing.Point(301, 135);
            this.txtOrganizationCode.Name = "txtOrganizationCode";
            this.txtOrganizationCode.Size = new System.Drawing.Size(116, 20);
            this.txtOrganizationCode.TabIndex = 21;
            // 
            // lblProductClass
            // 
            this.lblProductClass.AutoSize = true;
            this.lblProductClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductClass.Location = new System.Drawing.Point(146, 119);
            this.lblProductClass.Name = "lblProductClass";
            this.lblProductClass.Size = new System.Drawing.Size(85, 13);
            this.lblProductClass.TabIndex = 20;
            this.lblProductClass.Text = "Product Class";
            // 
            // txtProductClass
            // 
            this.txtProductClass.Location = new System.Drawing.Point(149, 135);
            this.txtProductClass.Name = "txtProductClass";
            this.txtProductClass.Size = new System.Drawing.Size(116, 20);
            this.txtProductClass.TabIndex = 19;
            // 
            // txtArrivalStation
            // 
            this.txtArrivalStation.Location = new System.Drawing.Point(149, 81);
            this.txtArrivalStation.Name = "txtArrivalStation";
            this.txtArrivalStation.Size = new System.Drawing.Size(116, 20);
            this.txtArrivalStation.TabIndex = 3;
            // 
            // lblArrivalStation
            // 
            this.lblArrivalStation.AutoSize = true;
            this.lblArrivalStation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblArrivalStation.Location = new System.Drawing.Point(146, 62);
            this.lblArrivalStation.Name = "lblArrivalStation";
            this.lblArrivalStation.Size = new System.Drawing.Size(81, 13);
            this.lblArrivalStation.TabIndex = 18;
            this.lblArrivalStation.Text = "Zielflughafen";
            // 
            // lblDepartureStation
            // 
            this.lblDepartureStation.AutoSize = true;
            this.lblDepartureStation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartureStation.Location = new System.Drawing.Point(146, 9);
            this.lblDepartureStation.Name = "lblDepartureStation";
            this.lblDepartureStation.Size = new System.Drawing.Size(75, 13);
            this.lblDepartureStation.TabIndex = 17;
            this.lblDepartureStation.Text = "Abflughafen";
            // 
            // txtDepartureStation
            // 
            this.txtDepartureStation.Location = new System.Drawing.Point(149, 29);
            this.txtDepartureStation.Name = "txtDepartureStation";
            this.txtDepartureStation.Size = new System.Drawing.Size(116, 20);
            this.txtDepartureStation.TabIndex = 2;
            // 
            // btnGetAvailability
            // 
            this.btnGetAvailability.Location = new System.Drawing.Point(10, 78);
            this.btnGetAvailability.Name = "btnGetAvailability";
            this.btnGetAvailability.Size = new System.Drawing.Size(111, 23);
            this.btnGetAvailability.TabIndex = 7;
            this.btnGetAvailability.Text = "Flüge suchen";
            this.btnGetAvailability.UseVisualStyleBackColor = true;
            this.btnGetAvailability.Click += new System.EventHandler(this.btnGetAvailabilityAPI_Click);
            // 
            // lblInfants
            // 
            this.lblInfants.AutoSize = true;
            this.lblInfants.Location = new System.Drawing.Point(498, 83);
            this.lblInfants.Name = "lblInfants";
            this.lblInfants.Size = new System.Drawing.Size(59, 13);
            this.lblInfants.TabIndex = 14;
            this.lblInfants.Text = "Kleinkinder";
            // 
            // numericInfants
            // 
            this.numericInfants.Location = new System.Drawing.Point(456, 81);
            this.numericInfants.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.numericInfants.Name = "numericInfants";
            this.numericInfants.Size = new System.Drawing.Size(36, 20);
            this.numericInfants.TabIndex = 13;
            this.numericInfants.TabStop = false;
            // 
            // lblChilds
            // 
            this.lblChilds.AutoSize = true;
            this.lblChilds.Location = new System.Drawing.Point(498, 57);
            this.lblChilds.Name = "lblChilds";
            this.lblChilds.Size = new System.Drawing.Size(37, 13);
            this.lblChilds.TabIndex = 12;
            this.lblChilds.Text = "Kinder";
            // 
            // numericChilds
            // 
            this.numericChilds.Location = new System.Drawing.Point(456, 55);
            this.numericChilds.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.numericChilds.Name = "numericChilds";
            this.numericChilds.Size = new System.Drawing.Size(36, 20);
            this.numericChilds.TabIndex = 11;
            this.numericChilds.TabStop = false;
            // 
            // lblAdults
            // 
            this.lblAdults.AutoSize = true;
            this.lblAdults.Location = new System.Drawing.Point(498, 31);
            this.lblAdults.Name = "lblAdults";
            this.lblAdults.Size = new System.Drawing.Size(66, 13);
            this.lblAdults.TabIndex = 10;
            this.lblAdults.Text = "Erwachsene";
            // 
            // numericAdults
            // 
            this.numericAdults.Location = new System.Drawing.Point(456, 29);
            this.numericAdults.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.numericAdults.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericAdults.Name = "numericAdults";
            this.numericAdults.Size = new System.Drawing.Size(36, 20);
            this.numericAdults.TabIndex = 6;
            this.numericAdults.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblTravellers
            // 
            this.lblTravellers.AutoSize = true;
            this.lblTravellers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTravellers.Location = new System.Drawing.Point(453, 9);
            this.lblTravellers.Name = "lblTravellers";
            this.lblTravellers.Size = new System.Drawing.Size(60, 13);
            this.lblTravellers.TabIndex = 8;
            this.lblTravellers.Text = "Reisende";
            // 
            // dateInbound
            // 
            this.dateInbound.Enabled = false;
            this.dateInbound.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateInbound.Location = new System.Drawing.Point(301, 81);
            this.dateInbound.Name = "dateInbound";
            this.dateInbound.Size = new System.Drawing.Size(116, 20);
            this.dateInbound.TabIndex = 5;
            // 
            // lblInbound
            // 
            this.lblInbound.AutoSize = true;
            this.lblInbound.Enabled = false;
            this.lblInbound.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInbound.Location = new System.Drawing.Point(298, 62);
            this.lblInbound.Name = "lblInbound";
            this.lblInbound.Size = new System.Drawing.Size(58, 13);
            this.lblInbound.TabIndex = 6;
            this.lblInbound.Text = "Rückflug";
            // 
            // lblOutbound
            // 
            this.lblOutbound.AutoSize = true;
            this.lblOutbound.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutbound.Location = new System.Drawing.Point(298, 9);
            this.lblOutbound.Name = "lblOutbound";
            this.lblOutbound.Size = new System.Drawing.Size(47, 13);
            this.lblOutbound.TabIndex = 5;
            this.lblOutbound.Text = "Hinflug";
            // 
            // dateOutbound
            // 
            this.dateOutbound.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateOutbound.Location = new System.Drawing.Point(301, 29);
            this.dateOutbound.Name = "dateOutbound";
            this.dateOutbound.Size = new System.Drawing.Size(116, 20);
            this.dateOutbound.TabIndex = 4;
            // 
            // radioOneway
            // 
            this.radioOneway.AutoSize = true;
            this.radioOneway.Checked = true;
            this.radioOneway.Location = new System.Drawing.Point(6, 50);
            this.radioOneway.Name = "radioOneway";
            this.radioOneway.Size = new System.Drawing.Size(78, 17);
            this.radioOneway.TabIndex = 1;
            this.radioOneway.TabStop = true;
            this.radioOneway.Text = "Einfachflug";
            this.radioOneway.UseVisualStyleBackColor = true;
            this.radioOneway.CheckedChanged += new System.EventHandler(this.radioSwitchFlightType_CheckedChanged);
            // 
            // lblRoute
            // 
            this.lblRoute.AutoSize = true;
            this.lblRoute.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoute.Location = new System.Drawing.Point(7, 9);
            this.lblRoute.Name = "lblRoute";
            this.lblRoute.Size = new System.Drawing.Size(51, 13);
            this.lblRoute.TabIndex = 1;
            this.lblRoute.Text = "Strecke";
            // 
            // radioReturn
            // 
            this.radioReturn.AutoSize = true;
            this.radioReturn.Location = new System.Drawing.Point(6, 30);
            this.radioReturn.Name = "radioReturn";
            this.radioReturn.Size = new System.Drawing.Size(111, 17);
            this.radioReturn.TabIndex = 0;
            this.radioReturn.Text = "Hin- und Rückflug";
            this.radioReturn.UseVisualStyleBackColor = true;
            this.radioReturn.CheckedChanged += new System.EventHandler(this.radioSwitchFlightType_CheckedChanged);
            // 
            // tabPriceItinerary
            // 
            this.tabPriceItinerary.Controls.Add(this.lblDetailSelectedFareRuleInbound);
            this.tabPriceItinerary.Controls.Add(this.lblDetailSelectedFareRuleOutbound);
            this.tabPriceItinerary.Controls.Add(this.lblFareRuleInbound);
            this.tabPriceItinerary.Controls.Add(this.lblFareRuleOutbound);
            this.tabPriceItinerary.Controls.Add(this.richTxtFareRuleInbound);
            this.tabPriceItinerary.Controls.Add(this.richTxtFareRuleOutbound);
            this.tabPriceItinerary.Controls.Add(this.lblSelectInboundFlight);
            this.tabPriceItinerary.Controls.Add(this.lblSelectOutboundFlight);
            this.tabPriceItinerary.Controls.Add(this.dataGridViewAvailabilityInbound);
            this.tabPriceItinerary.Controls.Add(this.dataGridViewAvailabilityOutbound);
            this.tabPriceItinerary.Controls.Add(this.lblDetailSelectedInboundFlight);
            this.tabPriceItinerary.Controls.Add(this.lblDetailSelectedOutboundFlight);
            this.tabPriceItinerary.Controls.Add(this.lblSelectedInboundFlight);
            this.tabPriceItinerary.Controls.Add(this.lblSelectedOutboundFlight);
            this.tabPriceItinerary.Controls.Add(this.btnAPIPriceItinerary);
            this.tabPriceItinerary.Location = new System.Drawing.Point(4, 22);
            this.tabPriceItinerary.Name = "tabPriceItinerary";
            this.tabPriceItinerary.Padding = new System.Windows.Forms.Padding(3);
            this.tabPriceItinerary.Size = new System.Drawing.Size(608, 419);
            this.tabPriceItinerary.TabIndex = 1;
            this.tabPriceItinerary.Text = "Price Itinerary";
            this.tabPriceItinerary.UseVisualStyleBackColor = true;
            // 
            // lblDetailSelectedFareRuleInbound
            // 
            this.lblDetailSelectedFareRuleInbound.AutoSize = true;
            this.lblDetailSelectedFareRuleInbound.Enabled = false;
            this.lblDetailSelectedFareRuleInbound.Location = new System.Drawing.Point(374, 260);
            this.lblDetailSelectedFareRuleInbound.Name = "lblDetailSelectedFareRuleInbound";
            this.lblDetailSelectedFareRuleInbound.Size = new System.Drawing.Size(35, 13);
            this.lblDetailSelectedFareRuleInbound.TabIndex = 32;
            this.lblDetailSelectedFareRuleInbound.Text = "label1";
            // 
            // lblDetailSelectedFareRuleOutbound
            // 
            this.lblDetailSelectedFareRuleOutbound.AutoSize = true;
            this.lblDetailSelectedFareRuleOutbound.Location = new System.Drawing.Point(64, 260);
            this.lblDetailSelectedFareRuleOutbound.Name = "lblDetailSelectedFareRuleOutbound";
            this.lblDetailSelectedFareRuleOutbound.Size = new System.Drawing.Size(35, 13);
            this.lblDetailSelectedFareRuleOutbound.TabIndex = 31;
            this.lblDetailSelectedFareRuleOutbound.Text = "label1";
            // 
            // lblFareRuleInbound
            // 
            this.lblFareRuleInbound.AutoSize = true;
            this.lblFareRuleInbound.Enabled = false;
            this.lblFareRuleInbound.Location = new System.Drawing.Point(312, 260);
            this.lblFareRuleInbound.Name = "lblFareRuleInbound";
            this.lblFareRuleInbound.Size = new System.Drawing.Size(56, 13);
            this.lblFareRuleInbound.TabIndex = 30;
            this.lblFareRuleInbound.Text = "Fare Rule:";
            // 
            // lblFareRuleOutbound
            // 
            this.lblFareRuleOutbound.AutoSize = true;
            this.lblFareRuleOutbound.Location = new System.Drawing.Point(4, 260);
            this.lblFareRuleOutbound.Name = "lblFareRuleOutbound";
            this.lblFareRuleOutbound.Size = new System.Drawing.Size(56, 13);
            this.lblFareRuleOutbound.TabIndex = 29;
            this.lblFareRuleOutbound.Text = "Fare Rule:";
            // 
            // richTxtFareRuleInbound
            // 
            this.richTxtFareRuleInbound.Enabled = false;
            this.richTxtFareRuleInbound.Location = new System.Drawing.Point(315, 276);
            this.richTxtFareRuleInbound.Name = "richTxtFareRuleInbound";
            this.richTxtFareRuleInbound.ReadOnly = true;
            this.richTxtFareRuleInbound.Size = new System.Drawing.Size(286, 106);
            this.richTxtFareRuleInbound.TabIndex = 28;
            this.richTxtFareRuleInbound.Text = "";
            // 
            // richTxtFareRuleOutbound
            // 
            this.richTxtFareRuleOutbound.Location = new System.Drawing.Point(7, 276);
            this.richTxtFareRuleOutbound.Name = "richTxtFareRuleOutbound";
            this.richTxtFareRuleOutbound.ReadOnly = true;
            this.richTxtFareRuleOutbound.Size = new System.Drawing.Size(286, 106);
            this.richTxtFareRuleOutbound.TabIndex = 27;
            this.richTxtFareRuleOutbound.Text = "";
            // 
            // lblSelectInboundFlight
            // 
            this.lblSelectInboundFlight.AutoSize = true;
            this.lblSelectInboundFlight.Enabled = false;
            this.lblSelectInboundFlight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectInboundFlight.Location = new System.Drawing.Point(312, 55);
            this.lblSelectInboundFlight.Name = "lblSelectInboundFlight";
            this.lblSelectInboundFlight.Size = new System.Drawing.Size(102, 13);
            this.lblSelectInboundFlight.TabIndex = 26;
            this.lblSelectInboundFlight.Text = "Rückflug wählen";
            // 
            // lblSelectOutboundFlight
            // 
            this.lblSelectOutboundFlight.AutoSize = true;
            this.lblSelectOutboundFlight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectOutboundFlight.Location = new System.Drawing.Point(8, 55);
            this.lblSelectOutboundFlight.Name = "lblSelectOutboundFlight";
            this.lblSelectOutboundFlight.Size = new System.Drawing.Size(91, 13);
            this.lblSelectOutboundFlight.TabIndex = 25;
            this.lblSelectOutboundFlight.Text = "Hinflug wählen";
            // 
            // dataGridViewAvailabilityInbound
            // 
            this.dataGridViewAvailabilityInbound.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewAvailabilityInbound.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAvailabilityInbound.Enabled = false;
            this.dataGridViewAvailabilityInbound.Location = new System.Drawing.Point(315, 71);
            this.dataGridViewAvailabilityInbound.Name = "dataGridViewAvailabilityInbound";
            this.dataGridViewAvailabilityInbound.Size = new System.Drawing.Size(286, 182);
            this.dataGridViewAvailabilityInbound.TabIndex = 24;
            this.dataGridViewAvailabilityInbound.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewAvailabilityInbound_CellClick);
            // 
            // dataGridViewAvailabilityOutbound
            // 
            this.dataGridViewAvailabilityOutbound.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewAvailabilityOutbound.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAvailabilityOutbound.Location = new System.Drawing.Point(7, 71);
            this.dataGridViewAvailabilityOutbound.Name = "dataGridViewAvailabilityOutbound";
            this.dataGridViewAvailabilityOutbound.Size = new System.Drawing.Size(286, 182);
            this.dataGridViewAvailabilityOutbound.TabIndex = 23;
            this.dataGridViewAvailabilityOutbound.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewAvailabilityOutbound_CellClick);
            // 
            // lblDetailSelectedInboundFlight
            // 
            this.lblDetailSelectedInboundFlight.AutoSize = true;
            this.lblDetailSelectedInboundFlight.Location = new System.Drawing.Point(154, 29);
            this.lblDetailSelectedInboundFlight.Name = "lblDetailSelectedInboundFlight";
            this.lblDetailSelectedInboundFlight.Size = new System.Drawing.Size(35, 13);
            this.lblDetailSelectedInboundFlight.TabIndex = 4;
            this.lblDetailSelectedInboundFlight.Text = "label2";
            // 
            // lblDetailSelectedOutboundFlight
            // 
            this.lblDetailSelectedOutboundFlight.AutoSize = true;
            this.lblDetailSelectedOutboundFlight.Location = new System.Drawing.Point(154, 10);
            this.lblDetailSelectedOutboundFlight.Name = "lblDetailSelectedOutboundFlight";
            this.lblDetailSelectedOutboundFlight.Size = new System.Drawing.Size(35, 13);
            this.lblDetailSelectedOutboundFlight.TabIndex = 3;
            this.lblDetailSelectedOutboundFlight.Text = "label1";
            // 
            // lblSelectedInboundFlight
            // 
            this.lblSelectedInboundFlight.AutoSize = true;
            this.lblSelectedInboundFlight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedInboundFlight.Location = new System.Drawing.Point(6, 29);
            this.lblSelectedInboundFlight.Name = "lblSelectedInboundFlight";
            this.lblSelectedInboundFlight.Size = new System.Drawing.Size(142, 13);
            this.lblSelectedInboundFlight.TabIndex = 2;
            this.lblSelectedInboundFlight.Text = "Ausgewählter Rückflug:";
            // 
            // lblSelectedOutboundFlight
            // 
            this.lblSelectedOutboundFlight.AutoSize = true;
            this.lblSelectedOutboundFlight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedOutboundFlight.Location = new System.Drawing.Point(6, 10);
            this.lblSelectedOutboundFlight.Name = "lblSelectedOutboundFlight";
            this.lblSelectedOutboundFlight.Size = new System.Drawing.Size(131, 13);
            this.lblSelectedOutboundFlight.TabIndex = 1;
            this.lblSelectedOutboundFlight.Text = "Ausgewählter Hinflug:";
            // 
            // btnAPIPriceItinerary
            // 
            this.btnAPIPriceItinerary.Location = new System.Drawing.Point(494, 390);
            this.btnAPIPriceItinerary.Name = "btnAPIPriceItinerary";
            this.btnAPIPriceItinerary.Size = new System.Drawing.Size(106, 23);
            this.btnAPIPriceItinerary.TabIndex = 0;
            this.btnAPIPriceItinerary.Text = "Price Itinerary";
            this.btnAPIPriceItinerary.UseVisualStyleBackColor = true;
            this.btnAPIPriceItinerary.Click += new System.EventHandler(this.btnAPIPriceItinerary_Click);
            // 
            // tabSell
            // 
            this.tabSell.Controls.Add(this.btnAPISellSSR);
            this.tabSell.Controls.Add(this.btnAPISellFlight);
            this.tabSell.Location = new System.Drawing.Point(4, 22);
            this.tabSell.Name = "tabSell";
            this.tabSell.Size = new System.Drawing.Size(608, 419);
            this.tabSell.TabIndex = 2;
            this.tabSell.Text = "Sell";
            this.tabSell.UseVisualStyleBackColor = true;
            // 
            // btnAPISellSSR
            // 
            this.btnAPISellSSR.Location = new System.Drawing.Point(84, 103);
            this.btnAPISellSSR.Name = "btnAPISellSSR";
            this.btnAPISellSSR.Size = new System.Drawing.Size(75, 23);
            this.btnAPISellSSR.TabIndex = 1;
            this.btnAPISellSSR.Text = "Sell SSR";
            this.btnAPISellSSR.UseVisualStyleBackColor = true;
            this.btnAPISellSSR.Click += new System.EventHandler(this.btnAPISellSSR_Click);
            // 
            // btnAPISellFlight
            // 
            this.btnAPISellFlight.Location = new System.Drawing.Point(3, 103);
            this.btnAPISellFlight.Name = "btnAPISellFlight";
            this.btnAPISellFlight.Size = new System.Drawing.Size(75, 23);
            this.btnAPISellFlight.TabIndex = 0;
            this.btnAPISellFlight.Text = "Sell Flight";
            this.btnAPISellFlight.UseVisualStyleBackColor = true;
            this.btnAPISellFlight.Click += new System.EventHandler(this.btnAPISellFlight_Click);
            // 
            // tabAddPayment
            // 
            this.tabAddPayment.Controls.Add(this.btnAPIAddPayment);
            this.tabAddPayment.Location = new System.Drawing.Point(4, 22);
            this.tabAddPayment.Name = "tabAddPayment";
            this.tabAddPayment.Size = new System.Drawing.Size(608, 419);
            this.tabAddPayment.TabIndex = 7;
            this.tabAddPayment.Text = "Add Payment";
            this.tabAddPayment.UseVisualStyleBackColor = true;
            // 
            // btnAPIAddPayment
            // 
            this.btnAPIAddPayment.Location = new System.Drawing.Point(24, 87);
            this.btnAPIAddPayment.Name = "btnAPIAddPayment";
            this.btnAPIAddPayment.Size = new System.Drawing.Size(92, 23);
            this.btnAPIAddPayment.TabIndex = 0;
            this.btnAPIAddPayment.Text = "Add Payment";
            this.btnAPIAddPayment.UseVisualStyleBackColor = true;
            this.btnAPIAddPayment.Click += new System.EventHandler(this.btnAPIAddPayment_Click);
            // 
            // tabCommit
            // 
            this.tabCommit.Controls.Add(this.btnAPICommit);
            this.tabCommit.Location = new System.Drawing.Point(4, 22);
            this.tabCommit.Name = "tabCommit";
            this.tabCommit.Size = new System.Drawing.Size(608, 419);
            this.tabCommit.TabIndex = 3;
            this.tabCommit.Text = "Commit";
            this.tabCommit.UseVisualStyleBackColor = true;
            // 
            // btnAPICommit
            // 
            this.btnAPICommit.Location = new System.Drawing.Point(20, 47);
            this.btnAPICommit.Name = "btnAPICommit";
            this.btnAPICommit.Size = new System.Drawing.Size(75, 23);
            this.btnAPICommit.TabIndex = 0;
            this.btnAPICommit.Text = "Commit";
            this.btnAPICommit.UseVisualStyleBackColor = true;
            this.btnAPICommit.Click += new System.EventHandler(this.btnAPICommit_Click);
            // 
            // tabGetBookingPayment
            // 
            this.tabGetBookingPayment.Location = new System.Drawing.Point(4, 22);
            this.tabGetBookingPayment.Name = "tabGetBookingPayment";
            this.tabGetBookingPayment.Size = new System.Drawing.Size(608, 419);
            this.tabGetBookingPayment.TabIndex = 4;
            this.tabGetBookingPayment.Text = "Get Booking Payment";
            this.tabGetBookingPayment.UseVisualStyleBackColor = true;
            // 
            // tabAusgabefenster
            // 
            this.tabAusgabefenster.Controls.Add(this.txtOutputWindow);
            this.tabAusgabefenster.Location = new System.Drawing.Point(4, 22);
            this.tabAusgabefenster.Name = "tabAusgabefenster";
            this.tabAusgabefenster.Padding = new System.Windows.Forms.Padding(3);
            this.tabAusgabefenster.Size = new System.Drawing.Size(608, 419);
            this.tabAusgabefenster.TabIndex = 5;
            this.tabAusgabefenster.Text = "Ausgabefenster";
            this.tabAusgabefenster.UseVisualStyleBackColor = true;
            // 
            // txtOutputWindow
            // 
            this.txtOutputWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutputWindow.Location = new System.Drawing.Point(3, 3);
            this.txtOutputWindow.Multiline = true;
            this.txtOutputWindow.Name = "txtOutputWindow";
            this.txtOutputWindow.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutputWindow.Size = new System.Drawing.Size(602, 413);
            this.txtOutputWindow.TabIndex = 0;
            // 
            // tabFindBooking
            // 
            this.tabFindBooking.Controls.Add(this.txtFindByDetail);
            this.tabFindBooking.Controls.Add(this.comboBoxFindBookingBy);
            this.tabFindBooking.Controls.Add(this.lblFindBookingBy);
            this.tabFindBooking.Controls.Add(this.dataGridViewFindBooking);
            this.tabFindBooking.Controls.Add(this.btnAPIFindBooking);
            this.tabFindBooking.Location = new System.Drawing.Point(4, 22);
            this.tabFindBooking.Name = "tabFindBooking";
            this.tabFindBooking.Padding = new System.Windows.Forms.Padding(3);
            this.tabFindBooking.Size = new System.Drawing.Size(608, 419);
            this.tabFindBooking.TabIndex = 6;
            this.tabFindBooking.Text = "Find Booking";
            this.tabFindBooking.UseVisualStyleBackColor = true;
            // 
            // txtFindByDetail
            // 
            this.txtFindByDetail.Location = new System.Drawing.Point(286, 7);
            this.txtFindByDetail.Name = "txtFindByDetail";
            this.txtFindByDetail.Size = new System.Drawing.Size(200, 20);
            this.txtFindByDetail.TabIndex = 4;
            this.txtFindByDetail.Text = "hansen@prologis.aero";
            // 
            // comboBoxFindBookingBy
            // 
            this.comboBoxFindBookingBy.FormattingEnabled = true;
            this.comboBoxFindBookingBy.Items.AddRange(new object[] {
            "FindByEmailAddress",
            "FindByRecordLocator"});
            this.comboBoxFindBookingBy.Location = new System.Drawing.Point(114, 7);
            this.comboBoxFindBookingBy.Name = "comboBoxFindBookingBy";
            this.comboBoxFindBookingBy.Size = new System.Drawing.Size(149, 21);
            this.comboBoxFindBookingBy.TabIndex = 3;
            this.comboBoxFindBookingBy.Text = "FindByEmailAddress";
            // 
            // lblFindBookingBy
            // 
            this.lblFindBookingBy.AutoSize = true;
            this.lblFindBookingBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFindBookingBy.Location = new System.Drawing.Point(6, 10);
            this.lblFindBookingBy.Name = "lblFindBookingBy";
            this.lblFindBookingBy.Size = new System.Drawing.Size(102, 13);
            this.lblFindBookingBy.TabIndex = 2;
            this.lblFindBookingBy.Text = "Find Booking by:";
            // 
            // dataGridViewFindBooking
            // 
            this.dataGridViewFindBooking.AllowUserToAddRows = false;
            this.dataGridViewFindBooking.AllowUserToDeleteRows = false;
            this.dataGridViewFindBooking.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewFindBooking.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFindBooking.Location = new System.Drawing.Point(6, 34);
            this.dataGridViewFindBooking.Name = "dataGridViewFindBooking";
            this.dataGridViewFindBooking.Size = new System.Drawing.Size(594, 379);
            this.dataGridViewFindBooking.TabIndex = 1;
            this.dataGridViewFindBooking.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewFindBooking_CellDoubleClick);
            // 
            // btnAPIFindBooking
            // 
            this.btnAPIFindBooking.Location = new System.Drawing.Point(492, 5);
            this.btnAPIFindBooking.Name = "btnAPIFindBooking";
            this.btnAPIFindBooking.Size = new System.Drawing.Size(96, 23);
            this.btnAPIFindBooking.TabIndex = 0;
            this.btnAPIFindBooking.Text = "Find Booking";
            this.btnAPIFindBooking.UseVisualStyleBackColor = true;
            this.btnAPIFindBooking.Click += new System.EventHandler(this.btnAPIFindBooking_Click);
            // 
            // tabAXSTest
            // 
            this.tabAXSTest.Controls.Add(this.btnAPIGetBooking);
            this.tabAXSTest.Controls.Add(this.btnAddBaggage);
            this.tabAXSTest.Controls.Add(this.label1);
            this.tabAXSTest.Controls.Add(this.btnRetrievePNRforAXS);
            this.tabAXSTest.Controls.Add(this.txtRetrievePNRforAXS);
            this.tabAXSTest.Location = new System.Drawing.Point(4, 22);
            this.tabAXSTest.Name = "tabAXSTest";
            this.tabAXSTest.Padding = new System.Windows.Forms.Padding(3);
            this.tabAXSTest.Size = new System.Drawing.Size(608, 419);
            this.tabAXSTest.TabIndex = 8;
            this.tabAXSTest.Text = "AXS Test";
            this.tabAXSTest.UseVisualStyleBackColor = true;
            // 
            // btnAPIGetBooking
            // 
            this.btnAPIGetBooking.Location = new System.Drawing.Point(112, 58);
            this.btnAPIGetBooking.Name = "btnAPIGetBooking";
            this.btnAPIGetBooking.Size = new System.Drawing.Size(102, 23);
            this.btnAPIGetBooking.TabIndex = 4;
            this.btnAPIGetBooking.Text = "Get Booking";
            this.btnAPIGetBooking.UseVisualStyleBackColor = true;
            this.btnAPIGetBooking.Click += new System.EventHandler(this.btnGetBooking_Click);
            // 
            // btnAddBaggage
            // 
            this.btnAddBaggage.Location = new System.Drawing.Point(112, 87);
            this.btnAddBaggage.Name = "btnAddBaggage";
            this.btnAddBaggage.Size = new System.Drawing.Size(102, 23);
            this.btnAddBaggage.TabIndex = 3;
            this.btnAddBaggage.Text = "Add Baggage";
            this.btnAddBaggage.UseVisualStyleBackColor = true;
            this.btnAddBaggage.Click += new System.EventHandler(this.btnAddBaggage_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Retrieve PNR to add AXS Payment to it";
            // 
            // btnRetrievePNRforAXS
            // 
            this.btnRetrievePNRforAXS.Location = new System.Drawing.Point(112, 30);
            this.btnRetrievePNRforAXS.Name = "btnRetrievePNRforAXS";
            this.btnRetrievePNRforAXS.Size = new System.Drawing.Size(102, 23);
            this.btnRetrievePNRforAXS.TabIndex = 1;
            this.btnRetrievePNRforAXS.Text = "Retrieve Booking";
            this.btnRetrievePNRforAXS.UseVisualStyleBackColor = true;
            this.btnRetrievePNRforAXS.Click += new System.EventHandler(this.btnRetrievePNRforAXS_Click);
            // 
            // txtRetrievePNRforAXS
            // 
            this.txtRetrievePNRforAXS.Location = new System.Drawing.Point(6, 32);
            this.txtRetrievePNRforAXS.Name = "txtRetrievePNRforAXS";
            this.txtRetrievePNRforAXS.Size = new System.Drawing.Size(100, 20);
            this.txtRetrievePNRforAXS.TabIndex = 0;
            // 
            // tabAssignSeat
            // 
            this.tabAssignSeat.Controls.Add(this.txtUnitDesignator);
            this.tabAssignSeat.Controls.Add(this.txtPNRAssignSeat);
            this.tabAssignSeat.Controls.Add(this.btnAssignSeat);
            this.tabAssignSeat.Location = new System.Drawing.Point(4, 22);
            this.tabAssignSeat.Name = "tabAssignSeat";
            this.tabAssignSeat.Padding = new System.Windows.Forms.Padding(3);
            this.tabAssignSeat.Size = new System.Drawing.Size(608, 419);
            this.tabAssignSeat.TabIndex = 9;
            this.tabAssignSeat.Text = "AssignSeats";
            this.tabAssignSeat.UseVisualStyleBackColor = true;
            // 
            // txtUnitDesignator
            // 
            this.txtUnitDesignator.Location = new System.Drawing.Point(141, 29);
            this.txtUnitDesignator.Name = "txtUnitDesignator";
            this.txtUnitDesignator.Size = new System.Drawing.Size(100, 20);
            this.txtUnitDesignator.TabIndex = 2;
            this.txtUnitDesignator.Text = "26B";
            // 
            // txtPNRAssignSeat
            // 
            this.txtPNRAssignSeat.Location = new System.Drawing.Point(35, 29);
            this.txtPNRAssignSeat.Name = "txtPNRAssignSeat";
            this.txtPNRAssignSeat.Size = new System.Drawing.Size(100, 20);
            this.txtPNRAssignSeat.TabIndex = 1;
            this.txtPNRAssignSeat.Text = "B7U7JW";
            // 
            // btnAssignSeat
            // 
            this.btnAssignSeat.Location = new System.Drawing.Point(35, 55);
            this.btnAssignSeat.Name = "btnAssignSeat";
            this.btnAssignSeat.Size = new System.Drawing.Size(75, 23);
            this.btnAssignSeat.TabIndex = 0;
            this.btnAssignSeat.Text = "Assign Seat";
            this.btnAssignSeat.UseVisualStyleBackColor = true;
            this.btnAssignSeat.Click += new System.EventHandler(this.btnAssignSeat_Click);
            // 
            // Buchung3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 528);
            this.Controls.Add(this.splitTopMain);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Buchung3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Skies 4.1.0";
            this.splitTopMain.Panel1.ResumeLayout(false);
            this.splitTopMain.Panel1.PerformLayout();
            this.splitTopMain.Panel2.ResumeLayout(false);
            this.splitTopMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.splitNavContent.Panel1.ResumeLayout(false);
            this.splitNavContent.Panel2.ResumeLayout(false);
            this.splitNavContent.ResumeLayout(false);
            this.tabAPIMethods.ResumeLayout(false);
            this.tabGetAvailability.ResumeLayout(false);
            this.tabGetAvailability.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericInfants)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericChilds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAdults)).EndInit();
            this.tabPriceItinerary.ResumeLayout(false);
            this.tabPriceItinerary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAvailabilityInbound)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAvailabilityOutbound)).EndInit();
            this.tabSell.ResumeLayout(false);
            this.tabAddPayment.ResumeLayout(false);
            this.tabCommit.ResumeLayout(false);
            this.tabAusgabefenster.ResumeLayout(false);
            this.tabAusgabefenster.PerformLayout();
            this.tabFindBooking.ResumeLayout(false);
            this.tabFindBooking.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFindBooking)).EndInit();
            this.tabAXSTest.ResumeLayout(false);
            this.tabAXSTest.PerformLayout();
            this.tabAssignSeat.ResumeLayout(false);
            this.tabAssignSeat.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitTopMain;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblAgentName;
        private System.Windows.Forms.Button btnLogon;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtAgentName;
        private System.Windows.Forms.Label lblLoggedIn;
        private System.Windows.Forms.SplitContainer splitNavContent;
        private System.Windows.Forms.TabControl tabAPIMethods;
        private System.Windows.Forms.TabPage tabGetAvailability;
        private System.Windows.Forms.TabPage tabPriceItinerary;
        private System.Windows.Forms.Button btnNavGetAvailability;
        private System.Windows.Forms.TabPage tabSell;
        private System.Windows.Forms.TabPage tabCommit;
        private System.Windows.Forms.TabPage tabGetBookingPayment;
        private System.Windows.Forms.Button btnNavCommit;
        private System.Windows.Forms.Button btnNavSell;
        private System.Windows.Forms.Button btnNavPriceItinerary;
        private System.Windows.Forms.Button btnNavClear;
        private System.Windows.Forms.TabPage tabAusgabefenster;
        private System.Windows.Forms.TextBox txtOutputWindow;
        private System.Windows.Forms.RadioButton radioOneway;
        private System.Windows.Forms.Label lblRoute;
        private System.Windows.Forms.RadioButton radioReturn;
        private System.Windows.Forms.DateTimePicker dateOutbound;
        private System.Windows.Forms.DateTimePicker dateInbound;
        private System.Windows.Forms.Label lblInbound;
        private System.Windows.Forms.Label lblOutbound;
        private System.Windows.Forms.Label lblTravellers;
        private System.Windows.Forms.Label lblAdults;
        private System.Windows.Forms.NumericUpDown numericAdults;
        private System.Windows.Forms.NumericUpDown numericChilds;
        private System.Windows.Forms.Label lblChilds;
        private System.Windows.Forms.NumericUpDown numericInfants;
        private System.Windows.Forms.Label lblInfants;
        private System.Windows.Forms.Button btnGetAvailability;
        private System.Windows.Forms.TextBox txtDepartureStation;
        private System.Windows.Forms.Label lblArrivalStation;
        private System.Windows.Forms.Label lblDepartureStation;
        private System.Windows.Forms.TextBox txtArrivalStation;
        private System.Windows.Forms.Button btnNavFindBooking;
        private System.Windows.Forms.TabPage tabFindBooking;
        private System.Windows.Forms.Button btnAPIFindBooking;
        private System.Windows.Forms.DataGridView dataGridViewFindBooking;
        private System.Windows.Forms.ComboBox comboBoxFindBookingBy;
        private System.Windows.Forms.Label lblFindBookingBy;
        private System.Windows.Forms.TextBox txtFindByDetail;
        private System.Windows.Forms.Button btnAPIPriceItinerary;
        private System.Windows.Forms.Label lblDetailSelectedInboundFlight;
        private System.Windows.Forms.Label lblDetailSelectedOutboundFlight;
        private System.Windows.Forms.Label lblSelectedInboundFlight;
        private System.Windows.Forms.Label lblSelectedOutboundFlight;
        private System.Windows.Forms.Button btnAPISellFlight;
        private System.Windows.Forms.Button btnAPICommit;
        private System.Windows.Forms.Label lblSelectInboundFlight;
        private System.Windows.Forms.Label lblSelectOutboundFlight;
        private System.Windows.Forms.DataGridView dataGridViewAvailabilityInbound;
        private System.Windows.Forms.DataGridView dataGridViewAvailabilityOutbound;
        private System.Windows.Forms.RichTextBox richTxtFareRuleInbound;
        private System.Windows.Forms.RichTextBox richTxtFareRuleOutbound;
        private System.Windows.Forms.Label lblFareRuleInbound;
        private System.Windows.Forms.Label lblFareRuleOutbound;
        private System.Windows.Forms.Label lblDetailSelectedFareRuleInbound;
        private System.Windows.Forms.Label lblDetailSelectedFareRuleOutbound;
        private System.Windows.Forms.Button btnAPISellSSR;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnNavAddPayment;
        private System.Windows.Forms.TabPage tabAddPayment;
        private System.Windows.Forms.Button btnAPIAddPayment;
        private System.Windows.Forms.TabPage tabAXSTest;
        private System.Windows.Forms.Button btnNavAXSTest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRetrievePNRforAXS;
        private System.Windows.Forms.TextBox txtRetrievePNRforAXS;
        private System.Windows.Forms.Button btnAddBaggage;
        private System.Windows.Forms.Button btnAPIGetBooking;
        private System.Windows.Forms.TabPage tabAssignSeat;
        private System.Windows.Forms.TextBox txtUnitDesignator;
        private System.Windows.Forms.TextBox txtPNRAssignSeat;
        private System.Windows.Forms.Button btnAssignSeat;
        private System.Windows.Forms.Button btnNavGetBookingFromState;
        private System.Windows.Forms.Button btnGetSeatAvailability;
        private System.Windows.Forms.Label lblOrganizationCode;
        private System.Windows.Forms.TextBox txtOrganizationCode;
        private System.Windows.Forms.Label lblProductClass;
        private System.Windows.Forms.TextBox txtProductClass;
        private System.Windows.Forms.Label lblPromotionCode;
        private System.Windows.Forms.TextBox txtPromotionCode;
    }
}

