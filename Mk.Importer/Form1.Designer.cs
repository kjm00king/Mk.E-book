namespace Mk.Importer
{
    partial class Form1
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
            this.ddlSeries = new System.Windows.Forms.ComboBox();
            this.btnReadXls = new System.Windows.Forms.Button();
            this.txtXls = new System.Windows.Forms.TextBox();
            this.txtJson = new System.Windows.Forms.TextBox();
            this.txtHtml = new System.Windows.Forms.TextBox();
            this.isHtml = new System.Windows.Forms.CheckBox();
            this.txtWeb = new System.Windows.Forms.TextBox();
            this.btnLoadTest = new System.Windows.Forms.Button();
            this.btnCheckWxApi = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ddlSeries
            // 
            this.ddlSeries.FormattingEnabled = true;
            this.ddlSeries.Items.AddRange(new object[] {
            "Manmo"});
            this.ddlSeries.Location = new System.Drawing.Point(12, 12);
            this.ddlSeries.Name = "ddlSeries";
            this.ddlSeries.Size = new System.Drawing.Size(83, 21);
            this.ddlSeries.TabIndex = 0;
            this.ddlSeries.Text = "Manmo";
            // 
            // btnReadXls
            // 
            this.btnReadXls.Location = new System.Drawing.Point(367, 38);
            this.btnReadXls.Name = "btnReadXls";
            this.btnReadXls.Size = new System.Drawing.Size(75, 73);
            this.btnReadXls.TabIndex = 1;
            this.btnReadXls.Text = "Convert";
            this.btnReadXls.UseVisualStyleBackColor = true;
            this.btnReadXls.Click += new System.EventHandler(this.btnReadXls_Click);
            // 
            // txtXls
            // 
            this.txtXls.Location = new System.Drawing.Point(12, 39);
            this.txtXls.Name = "txtXls";
            this.txtXls.Size = new System.Drawing.Size(348, 20);
            this.txtXls.TabIndex = 2;
            this.txtXls.Text = "D:\\Work\\[2016.12]Marykey\\Mk.E-book\\Mk.E-book\\DATA\\db.xlsx";
            // 
            // txtJson
            // 
            this.txtJson.Location = new System.Drawing.Point(12, 65);
            this.txtJson.Name = "txtJson";
            this.txtJson.Size = new System.Drawing.Size(348, 20);
            this.txtJson.TabIndex = 3;
            this.txtJson.Text = "D:\\Work\\[2016.12]Marykey\\Mk.E-book\\Mk.E-book\\DATA\\Json";
            // 
            // txtHtml
            // 
            this.txtHtml.Location = new System.Drawing.Point(12, 91);
            this.txtHtml.Name = "txtHtml";
            this.txtHtml.Size = new System.Drawing.Size(348, 20);
            this.txtHtml.TabIndex = 4;
            this.txtHtml.Text = "D:\\Work\\[2016.12]Marykey\\Mk.E-book\\Mk.E-book\\Content";
            // 
            // isHtml
            // 
            this.isHtml.AutoSize = true;
            this.isHtml.Checked = true;
            this.isHtml.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isHtml.Location = new System.Drawing.Point(101, 14);
            this.isHtml.Name = "isHtml";
            this.isHtml.Size = new System.Drawing.Size(47, 17);
            this.isHtml.TabIndex = 5;
            this.isHtml.Text = "Html";
            this.isHtml.UseVisualStyleBackColor = true;
            // 
            // txtWeb
            // 
            this.txtWeb.Location = new System.Drawing.Point(12, 117);
            this.txtWeb.Name = "txtWeb";
            this.txtWeb.Size = new System.Drawing.Size(348, 20);
            this.txtWeb.TabIndex = 6;
            this.txtWeb.Text = "D:\\Work\\[2016.12]Marykey\\Mk.E-book\\Mk.E-book";
            // 
            // btnLoadTest
            // 
            this.btnLoadTest.Location = new System.Drawing.Point(12, 159);
            this.btnLoadTest.Name = "btnLoadTest";
            this.btnLoadTest.Size = new System.Drawing.Size(99, 29);
            this.btnLoadTest.TabIndex = 7;
            this.btnLoadTest.Text = "Load Json";
            this.btnLoadTest.UseVisualStyleBackColor = true;
            this.btnLoadTest.Click += new System.EventHandler(this.btnLoadTest_Click);
            // 
            // btnCheckWxApi
            // 
            this.btnCheckWxApi.Location = new System.Drawing.Point(12, 194);
            this.btnCheckWxApi.Name = "btnCheckWxApi";
            this.btnCheckWxApi.Size = new System.Drawing.Size(99, 29);
            this.btnCheckWxApi.TabIndex = 8;
            this.btnCheckWxApi.Text = "Check WxApi";
            this.btnCheckWxApi.UseVisualStyleBackColor = true;
            this.btnCheckWxApi.Click += new System.EventHandler(this.btnCheckWxApi_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 248);
            this.Controls.Add(this.btnCheckWxApi);
            this.Controls.Add(this.btnLoadTest);
            this.Controls.Add(this.txtWeb);
            this.Controls.Add(this.isHtml);
            this.Controls.Add(this.txtHtml);
            this.Controls.Add(this.txtJson);
            this.Controls.Add(this.txtXls);
            this.Controls.Add(this.btnReadXls);
            this.Controls.Add(this.ddlSeries);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ddlSeries;
        private System.Windows.Forms.Button btnReadXls;
        private System.Windows.Forms.TextBox txtXls;
        private System.Windows.Forms.TextBox txtJson;
        private System.Windows.Forms.TextBox txtHtml;
        private System.Windows.Forms.CheckBox isHtml;
        private System.Windows.Forms.TextBox txtWeb;
        private System.Windows.Forms.Button btnLoadTest;
        private System.Windows.Forms.Button btnCheckWxApi;
    }
}

