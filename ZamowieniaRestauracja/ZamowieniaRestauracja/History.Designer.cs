namespace ZamowieniaRestauracja
{
    partial class History
    {
        /// <summary> 
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod wygenerowany przez Projektanta składników

        /// <summary> 
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować 
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.showOrders = new System.Windows.Forms.DataGridView();
            this.zamowienie_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zmowienie_nr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zmowienie_data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zmowienie_koszt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zmowienie_email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.showDetailsofOrder = new System.Windows.Forms.DataGridView();
            this.produkt_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.produkt_nazwa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.produkt_koszt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.produkt_id_zamowienie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.showOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.showDetailsofOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // showOrders
            // 
            this.showOrders.AllowUserToAddRows = false;
            this.showOrders.AllowUserToDeleteRows = false;
            this.showOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.showOrders.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.zamowienie_id,
            this.zmowienie_nr,
            this.zmowienie_data,
            this.zmowienie_koszt,
            this.zmowienie_email});
            this.showOrders.Location = new System.Drawing.Point(22, 12);
            this.showOrders.Name = "showOrders";
            this.showOrders.ReadOnly = true;
            this.showOrders.Size = new System.Drawing.Size(562, 176);
            this.showOrders.TabIndex = 0;
            // 
            // zamowienie_id
            // 
            this.zamowienie_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.zamowienie_id.DataPropertyName = "zm_id";
            this.zamowienie_id.HeaderText = "id";
            this.zamowienie_id.Name = "zamowienie_id";
            this.zamowienie_id.ReadOnly = true;
            // 
            // zmowienie_nr
            // 
            this.zmowienie_nr.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.zmowienie_nr.DataPropertyName = "zm_nr_zamowienia";
            this.zmowienie_nr.HeaderText = "Nr zamówienia";
            this.zmowienie_nr.Name = "zmowienie_nr";
            this.zmowienie_nr.ReadOnly = true;
            this.zmowienie_nr.Width = 93;
            // 
            // zmowienie_data
            // 
            this.zmowienie_data.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.zmowienie_data.DataPropertyName = "zm_data_zamowienia";
            this.zmowienie_data.HeaderText = "Data zamówienia";
            this.zmowienie_data.Name = "zmowienie_data";
            this.zmowienie_data.ReadOnly = true;
            this.zmowienie_data.Width = 104;
            // 
            // zmowienie_koszt
            // 
            this.zmowienie_koszt.DataPropertyName = "zm_koszt";
            this.zmowienie_koszt.HeaderText = "Cena [zł]";
            this.zmowienie_koszt.Name = "zmowienie_koszt";
            this.zmowienie_koszt.ReadOnly = true;
            // 
            // zmowienie_email
            // 
            this.zmowienie_email.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.zmowienie_email.DataPropertyName = "zm_email";
            this.zmowienie_email.HeaderText = "Email";
            this.zmowienie_email.Name = "zmowienie_email";
            this.zmowienie_email.ReadOnly = true;
            this.zmowienie_email.Width = 57;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(112, 194);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Szczegóły zamówienia";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Details);
            // 
            // showDetailsofOrder
            // 
            this.showDetailsofOrder.AllowUserToAddRows = false;
            this.showDetailsofOrder.AllowUserToDeleteRows = false;
            this.showDetailsofOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.showDetailsofOrder.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.produkt_id,
            this.produkt_nazwa,
            this.produkt_koszt,
            this.produkt_id_zamowienie});
            this.showDetailsofOrder.Location = new System.Drawing.Point(22, 235);
            this.showDetailsofOrder.Name = "showDetailsofOrder";
            this.showDetailsofOrder.ReadOnly = true;
            this.showDetailsofOrder.Size = new System.Drawing.Size(562, 128);
            this.showDetailsofOrder.TabIndex = 2;
            this.showDetailsofOrder.Visible = false;
            // 
            // produkt_id
            // 
            this.produkt_id.DataPropertyName = "pr_id";
            this.produkt_id.HeaderText = "id";
            this.produkt_id.Name = "produkt_id";
            this.produkt_id.ReadOnly = true;
            this.produkt_id.Width = 40;
            // 
            // produkt_nazwa
            // 
            this.produkt_nazwa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.produkt_nazwa.DataPropertyName = "pr_nazwa";
            this.produkt_nazwa.HeaderText = "Nazwa produktu";
            this.produkt_nazwa.Name = "produkt_nazwa";
            this.produkt_nazwa.ReadOnly = true;
            this.produkt_nazwa.Width = 101;
            // 
            // produkt_koszt
            // 
            this.produkt_koszt.DataPropertyName = "pr_cena";
            this.produkt_koszt.HeaderText = "Cena produktu [zł]";
            this.produkt_koszt.Name = "produkt_koszt";
            this.produkt_koszt.ReadOnly = true;
            // 
            // produkt_id_zamowienie
            // 
            this.produkt_id_zamowienie.DataPropertyName = "zm_id";
            this.produkt_id_zamowienie.HeaderText = "id zamówienia";
            this.produkt_id_zamowienie.Name = "produkt_id_zamowienie";
            this.produkt_id_zamowienie.ReadOnly = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(274, 194);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(144, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Usuń zamówienie";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.DeleteOrderFromDB);
            // 
            // History
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.showDetailsofOrder);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.showOrders);
            this.Name = "History";
            this.Size = new System.Drawing.Size(642, 385);
            this.VisibleChanged += new System.EventHandler(this.LoadLeftHistory);
            ((System.ComponentModel.ISupportInitialize)(this.showOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.showDetailsofOrder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView showOrders;
        
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView showDetailsofOrder;

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn zamowienie_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn zmowienie_nr;
        private System.Windows.Forms.DataGridViewTextBoxColumn zmowienie_data;
        private System.Windows.Forms.DataGridViewTextBoxColumn zmowienie_koszt;
        private System.Windows.Forms.DataGridViewTextBoxColumn zmowienie_email;
        private System.Windows.Forms.DataGridViewTextBoxColumn produkt_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn produkt_nazwa;
        private System.Windows.Forms.DataGridViewTextBoxColumn produkt_koszt;
        private System.Windows.Forms.DataGridViewTextBoxColumn produkt_id_zamowienie;
    }
}
