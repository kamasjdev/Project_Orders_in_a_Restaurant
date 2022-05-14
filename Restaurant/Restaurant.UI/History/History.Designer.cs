namespace Restaurant.UI
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
            this.button1 = new System.Windows.Forms.Button();
            this.showDetailsofOrder = new System.Windows.Forms.DataGridView();
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
            this.showOrders.Location = new System.Drawing.Point(22, 12);
            this.showOrders.Name = "showOrders";
            this.showOrders.ReadOnly = true;
            this.showOrders.Size = new System.Drawing.Size(562, 176);
            this.showOrders.TabIndex = 0;
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
            this.showDetailsofOrder.Location = new System.Drawing.Point(22, 235);
            this.showDetailsofOrder.Name = "showDetailsofOrder";
            this.showDetailsofOrder.ReadOnly = true;
            this.showDetailsofOrder.Size = new System.Drawing.Size(562, 128);
            this.showDetailsofOrder.TabIndex = 2;
            this.showDetailsofOrder.Visible = false;
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
    }
}
