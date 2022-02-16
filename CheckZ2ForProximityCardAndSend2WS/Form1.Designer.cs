namespace CheckZ2ForProximityCardAndSend2WS
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnClose = new System.Windows.Forms.Button();
            this.lblVirtualCom = new System.Windows.Forms.Label();
            this.txbVirtCOMPort = new System.Windows.Forms.TextBox();
            this.txbToolsInitialized = new System.Windows.Forms.TextBox();
            this.lblToolsInitialized = new System.Windows.Forms.Label();
            this.txbReaderPlugged = new System.Windows.Forms.TextBox();
            this.lblReaderPlugged = new System.Windows.Forms.Label();
            this.txbCardNumber = new System.Windows.Forms.TextBox();
            this.lblCardNumber = new System.Windows.Forms.Label();
            this.txbKioskNumber = new System.Windows.Forms.TextBox();
            this.lblKioskNumber = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(129, 233);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(289, 34);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Завершить программу";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblVirtualCom
            // 
            this.lblVirtualCom.AutoSize = true;
            this.lblVirtualCom.Location = new System.Drawing.Point(126, 67);
            this.lblVirtualCom.Name = "lblVirtualCom";
            this.lblVirtualCom.Size = new System.Drawing.Size(170, 17);
            this.lblVirtualCom.TabIndex = 2;
            this.lblVirtualCom.Text = "Виртуальный COM порт:";
            // 
            // txbVirtCOMPort
            // 
            this.txbVirtCOMPort.HideSelection = false;
            this.txbVirtCOMPort.Location = new System.Drawing.Point(302, 64);
            this.txbVirtCOMPort.Name = "txbVirtCOMPort";
            this.txbVirtCOMPort.ReadOnly = true;
            this.txbVirtCOMPort.Size = new System.Drawing.Size(116, 22);
            this.txbVirtCOMPort.TabIndex = 3;
            // 
            // txbToolsInitialized
            // 
            this.txbToolsInitialized.HideSelection = false;
            this.txbToolsInitialized.Location = new System.Drawing.Point(336, 106);
            this.txbToolsInitialized.Name = "txbToolsInitialized";
            this.txbToolsInitialized.ReadOnly = true;
            this.txbToolsInitialized.Size = new System.Drawing.Size(82, 22);
            this.txbToolsInitialized.TabIndex = 5;
            // 
            // lblToolsInitialized
            // 
            this.lblToolsInitialized.AutoSize = true;
            this.lblToolsInitialized.Location = new System.Drawing.Point(126, 106);
            this.lblToolsInitialized.Name = "lblToolsInitialized";
            this.lblToolsInitialized.Size = new System.Drawing.Size(206, 17);
            this.lblToolsInitialized.TabIndex = 4;
            this.lblToolsInitialized.Text = "Средства инициализированы:";
            // 
            // txbReaderPlugged
            // 
            this.txbReaderPlugged.HideSelection = false;
            this.txbReaderPlugged.Location = new System.Drawing.Point(322, 149);
            this.txbReaderPlugged.Name = "txbReaderPlugged";
            this.txbReaderPlugged.ReadOnly = true;
            this.txbReaderPlugged.Size = new System.Drawing.Size(96, 22);
            this.txbReaderPlugged.TabIndex = 7;
            // 
            // lblReaderPlugged
            // 
            this.lblReaderPlugged.AutoSize = true;
            this.lblReaderPlugged.Location = new System.Drawing.Point(126, 149);
            this.lblReaderPlugged.Name = "lblReaderPlugged";
            this.lblReaderPlugged.Size = new System.Drawing.Size(190, 17);
            this.lblReaderPlugged.TabIndex = 6;
            this.lblReaderPlugged.Text = "Считыватель присоединен:";
            // 
            // txbCardNumber
            // 
            this.txbCardNumber.HideSelection = false;
            this.txbCardNumber.Location = new System.Drawing.Point(322, 191);
            this.txbCardNumber.Name = "txbCardNumber";
            this.txbCardNumber.ReadOnly = true;
            this.txbCardNumber.Size = new System.Drawing.Size(96, 22);
            this.txbCardNumber.TabIndex = 9;
            // 
            // lblCardNumber
            // 
            this.lblCardNumber.AutoSize = true;
            this.lblCardNumber.Location = new System.Drawing.Point(126, 191);
            this.lblCardNumber.Name = "lblCardNumber";
            this.lblCardNumber.Size = new System.Drawing.Size(190, 17);
            this.lblCardNumber.TabIndex = 8;
            this.lblCardNumber.Text = "Номер поднесенной карты:";
            // 
            // txbKioskNumber
            // 
            this.txbKioskNumber.HideSelection = false;
            this.txbKioskNumber.Location = new System.Drawing.Point(236, 22);
            this.txbKioskNumber.Name = "txbKioskNumber";
            this.txbKioskNumber.ReadOnly = true;
            this.txbKioskNumber.Size = new System.Drawing.Size(182, 22);
            this.txbKioskNumber.TabIndex = 11;
            // 
            // lblKioskNumber
            // 
            this.lblKioskNumber.AutoSize = true;
            this.lblKioskNumber.Location = new System.Drawing.Point(126, 25);
            this.lblKioskNumber.Name = "lblKioskNumber";
            this.lblKioskNumber.Size = new System.Drawing.Size(104, 17);
            this.lblKioskNumber.TabIndex = 10;
            this.lblKioskNumber.Text = "Номер киоска:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 290);
            this.Controls.Add(this.txbKioskNumber);
            this.Controls.Add(this.lblKioskNumber);
            this.Controls.Add(this.txbCardNumber);
            this.Controls.Add(this.lblCardNumber);
            this.Controls.Add(this.txbReaderPlugged);
            this.Controls.Add(this.lblReaderPlugged);
            this.Controls.Add(this.txbToolsInitialized);
            this.Controls.Add(this.lblToolsInitialized);
            this.Controls.Add(this.txbVirtCOMPort);
            this.Controls.Add(this.lblVirtualCom);
            this.Controls.Add(this.btnClose);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Мониторинг считывателя пропусков Z-2 ";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblVirtualCom;
        private System.Windows.Forms.TextBox txbVirtCOMPort;
        private System.Windows.Forms.TextBox txbToolsInitialized;
        private System.Windows.Forms.Label lblToolsInitialized;
        private System.Windows.Forms.TextBox txbReaderPlugged;
        private System.Windows.Forms.Label lblReaderPlugged;
        private System.Windows.Forms.TextBox txbCardNumber;
        private System.Windows.Forms.Label lblCardNumber;
        private System.Windows.Forms.TextBox txbKioskNumber;
        private System.Windows.Forms.Label lblKioskNumber;
    }
}

