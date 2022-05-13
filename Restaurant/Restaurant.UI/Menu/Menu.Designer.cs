namespace Restaurant.UI
{
    partial class Menu
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
            this.components = new System.ComponentModel.Container();
            this.comboBoxMainDishes1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxAdditions = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.listViewOrderedProducts = new System.Windows.Forms.ListView();
            this.button1 = new System.Windows.Forms.Button();
            this.labelCostOfOrder = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button3 = new System.Windows.Forms.Button();
            this.PriceProductLabel = new System.Windows.Forms.Label();
            this.PriceProduct = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxMainDishes1
            // 
            this.comboBoxMainDishes1.DropDownHeight = 80;
            this.comboBoxMainDishes1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMainDishes1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBoxMainDishes1.FormattingEnabled = true;
            this.comboBoxMainDishes1.IntegralHeight = false;
            this.comboBoxMainDishes1.Location = new System.Drawing.Point(96, 56);
            this.comboBoxMainDishes1.MaxDropDownItems = 2;
            this.comboBoxMainDishes1.Name = "comboBoxMainDishes1";
            this.comboBoxMainDishes1.Size = new System.Drawing.Size(262, 24);
            this.comboBoxMainDishes1.TabIndex = 0;
            this.comboBoxMainDishes1.SelectedIndexChanged += new System.EventHandler(this.ChangedItem);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(93, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(255, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Wybierz produkt, który chcesz zamówić";
            // 
            // comboBoxAdditions
            // 
            this.comboBoxAdditions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAdditions.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBoxAdditions.FormattingEnabled = true;
            this.comboBoxAdditions.Location = new System.Drawing.Point(379, 56);
            this.comboBoxAdditions.Name = "comboBoxAdditions";
            this.comboBoxAdditions.Size = new System.Drawing.Size(121, 24);
            this.comboBoxAdditions.TabIndex = 3;
            this.comboBoxAdditions.Visible = false;
            this.comboBoxAdditions.SelectedIndexChanged += new System.EventHandler(this.ChangedAddition);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(406, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Dodatki:";
            this.label3.Visible = false;
            // 
            // listViewOrderedProducts
            // 
            this.listViewOrderedProducts.HideSelection = false;
            this.listViewOrderedProducts.Location = new System.Drawing.Point(30, 159);
            this.listViewOrderedProducts.Name = "listViewOrderedProducts";
            this.listViewOrderedProducts.Size = new System.Drawing.Size(589, 191);
            this.listViewOrderedProducts.TabIndex = 6;
            this.listViewOrderedProducts.UseCompatibleStateImageBehavior = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(92, 130);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(161, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Dodaj do zamówienia";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.AddToOrder);
            // 
            // labelCostOfOrder
            // 
            this.labelCostOfOrder.AutoSize = true;
            this.labelCostOfOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelCostOfOrder.Location = new System.Drawing.Point(525, 353);
            this.labelCostOfOrder.Name = "labelCostOfOrder";
            this.labelCostOfOrder.Size = new System.Drawing.Size(51, 17);
            this.labelCostOfOrder.TabIndex = 11;
            this.labelCostOfOrder.Text = "Koszt: ";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(285, 130);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(161, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "Usuń z zamówienia";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.DeleteFromOrder);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.RefreshCost);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(142, 353);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(260, 32);
            this.button3.TabIndex = 13;
            this.button3.Text = "Zrealizuj zamówienie";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.OrderRealization);
            // 
            // PriceProductLabel
            // 
            this.PriceProductLabel.AutoSize = true;
            this.PriceProductLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PriceProductLabel.Location = new System.Drawing.Point(193, 94);
            this.PriceProductLabel.Name = "PriceProductLabel";
            this.PriceProductLabel.Size = new System.Drawing.Size(45, 17);
            this.PriceProductLabel.TabIndex = 14;
            this.PriceProductLabel.Text = "Cena:";
            this.PriceProductLabel.Visible = false;
            // 
            // PriceProduct
            // 
            this.PriceProduct.AutoSize = true;
            this.PriceProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PriceProduct.Location = new System.Drawing.Point(263, 94);
            this.PriceProduct.Name = "PriceProduct";
            this.PriceProduct.Size = new System.Drawing.Size(0, 17);
            this.PriceProduct.TabIndex = 15;
            this.PriceProduct.Visible = false;
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PriceProduct);
            this.Controls.Add(this.PriceProductLabel);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.labelCostOfOrder);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listViewOrderedProducts);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxAdditions);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxMainDishes1);
            this.Name = "Menu";
            this.Size = new System.Drawing.Size(642, 385);
            this.Load += new System.EventHandler(this.OnLoad);
            this.VisibleChanged += new System.EventHandler(this.LoadLeftMenu);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxMainDishes1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxAdditions;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView listViewOrderedProducts;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelCostOfOrder;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label PriceProductLabel;
        private System.Windows.Forms.Label PriceProduct;
    }
}
