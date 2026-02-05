namespace EddyDemo
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btConnect = new Button();
            label1 = new Label();
            label2 = new Label();
            tbIp = new TextBox();
            tbPort = new TextBox();
            btDisconnect = new Button();
            btZero = new Button();
            btGetData = new Button();
            lbGetData = new Label();
            SuspendLayout();
            // 
            // btConnect
            // 
            btConnect.Location = new Point(12, 24);
            btConnect.Name = "btConnect";
            btConnect.Size = new Size(213, 44);
            btConnect.TabIndex = 0;
            btConnect.Text = "Connect";
            btConnect.UseVisualStyleBackColor = true;
            btConnect.Click += btConnect_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label1.Location = new Point(271, 29);
            label1.Name = "label1";
            label1.Size = new Size(39, 28);
            label1.TabIndex = 1;
            label1.Text = "IP :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label2.Location = new Point(271, 73);
            label2.Name = "label2";
            label2.Size = new Size(60, 28);
            label2.TabIndex = 2;
            label2.Text = "Port :";
            // 
            // tbIp
            // 
            tbIp.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            tbIp.Location = new Point(338, 30);
            tbIp.Name = "tbIp";
            tbIp.Size = new Size(246, 34);
            tbIp.TabIndex = 3;
            tbIp.Text = "192.168.1.99";
            // 
            // tbPort
            // 
            tbPort.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            tbPort.Location = new Point(338, 77);
            tbPort.Name = "tbPort";
            tbPort.Size = new Size(246, 34);
            tbPort.TabIndex = 4;
            tbPort.Text = "502";
            // 
            // btDisconnect
            // 
            btDisconnect.Location = new Point(12, 73);
            btDisconnect.Name = "btDisconnect";
            btDisconnect.Size = new Size(213, 44);
            btDisconnect.TabIndex = 5;
            btDisconnect.Text = "Disconnect";
            btDisconnect.UseVisualStyleBackColor = true;
            btDisconnect.Click += btDisconnect_Click;
            // 
            // btZero
            // 
            btZero.Location = new Point(12, 145);
            btZero.Name = "btZero";
            btZero.Size = new Size(213, 44);
            btZero.TabIndex = 6;
            btZero.Text = "Zero";
            btZero.UseVisualStyleBackColor = true;
            btZero.Click += btZero_Click;
            // 
            // btGetData
            // 
            btGetData.Location = new Point(12, 195);
            btGetData.Name = "btGetData";
            btGetData.Size = new Size(213, 44);
            btGetData.TabIndex = 7;
            btGetData.Text = "Get Data";
            btGetData.UseVisualStyleBackColor = true;
            btGetData.Click += btGetData_Click;
            // 
            // lbGetData
            // 
            lbGetData.AutoSize = true;
            lbGetData.Font = new Font("맑은 고딕", 24F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbGetData.Location = new Point(350, 307);
            lbGetData.Name = "lbGetData";
            lbGetData.Size = new Size(186, 54);
            lbGetData.TabIndex = 8;
            lbGetData.Text = "000.0000";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1036, 551);
            Controls.Add(lbGetData);
            Controls.Add(btGetData);
            Controls.Add(btZero);
            Controls.Add(btDisconnect);
            Controls.Add(tbPort);
            Controls.Add(tbIp);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btConnect);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btConnect;
        private Label label1;
        private Label label2;
        private TextBox tbIp;
        private TextBox tbPort;
        private Button btDisconnect;
        private Button btZero;
        private Button btGetData;
        private Label lbGetData;
    }
}
