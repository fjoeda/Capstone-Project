using System;

namespace ImageViewerWinforms
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
            this.components = new System.ComponentModel.Container();
            this.pb_ImgCam = new System.Windows.Forms.PictureBox();
            this.pb_DepthImage = new System.Windows.Forms.PictureBox();
            this.btn_Record = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_Label = new System.Windows.Forms.TextBox();
            this.tb_HandData = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tm_Record = new System.Windows.Forms.Timer(this.components);
            this.cb_Test = new System.Windows.Forms.CheckBox();
            this.tb_Collection = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_CombineData = new System.Windows.Forms.Button();
            this.lbl_prediction = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pb_ImgCam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_DepthImage)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_ImgCam
            // 
            this.pb_ImgCam.Location = new System.Drawing.Point(11, 12);
            this.pb_ImgCam.Name = "pb_ImgCam";
            this.pb_ImgCam.Size = new System.Drawing.Size(469, 352);
            this.pb_ImgCam.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pb_ImgCam.TabIndex = 0;
            this.pb_ImgCam.TabStop = false;
            // 
            // pb_DepthImage
            // 
            this.pb_DepthImage.Location = new System.Drawing.Point(487, 12);
            this.pb_DepthImage.Name = "pb_DepthImage";
            this.pb_DepthImage.Size = new System.Drawing.Size(469, 352);
            this.pb_DepthImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pb_DepthImage.TabIndex = 0;
            this.pb_DepthImage.TabStop = false;
            // 
            // btn_Record
            // 
            this.btn_Record.Location = new System.Drawing.Point(211, 371);
            this.btn_Record.Name = "btn_Record";
            this.btn_Record.Size = new System.Drawing.Size(96, 68);
            this.btn_Record.TabIndex = 1;
            this.btn_Record.Text = "Start Record";
            this.btn_Record.UseVisualStyleBackColor = true;
            this.btn_Record.Click += new System.EventHandler(this.btn_Record_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 376);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Label :";
            // 
            // tb_Label
            // 
            this.tb_Label.Location = new System.Drawing.Point(70, 373);
            this.tb_Label.Name = "tb_Label";
            this.tb_Label.Size = new System.Drawing.Size(135, 27);
            this.tb_Label.TabIndex = 3;
            this.tb_Label.TextChanged += new System.EventHandler(this.tb_Label_TextChanged);
            // 
            // tb_HandData
            // 
            this.tb_HandData.Location = new System.Drawing.Point(962, 12);
            this.tb_HandData.Multiline = true;
            this.tb_HandData.Name = "tb_HandData";
            this.tb_HandData.ReadOnly = true;
            this.tb_HandData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_HandData.Size = new System.Drawing.Size(300, 352);
            this.tb_HandData.TabIndex = 4;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tm_Record
            // 
            this.tm_Record.Interval = 1000;
            this.tm_Record.Tick += new System.EventHandler(this.tm_Record_Tick);
            // 
            // cb_Test
            // 
            this.cb_Test.AutoSize = true;
            this.cb_Test.Location = new System.Drawing.Point(1164, 376);
            this.cb_Test.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cb_Test.Name = "cb_Test";
            this.cb_Test.Size = new System.Drawing.Size(98, 24);
            this.cb_Test.TabIndex = 5;
            this.cb_Test.Text = "Prediction";
            this.cb_Test.UseVisualStyleBackColor = true;
            this.cb_Test.CheckedChanged += new System.EventHandler(this.cb_Test_CheckedChanged);
            // 
            // tb_Collection
            // 
            this.tb_Collection.Location = new System.Drawing.Point(104, 406);
            this.tb_Collection.Name = "tb_Collection";
            this.tb_Collection.Size = new System.Drawing.Size(101, 27);
            this.tb_Collection.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 409);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Collection  :";
            // 
            // btn_CombineData
            // 
            this.btn_CombineData.Location = new System.Drawing.Point(1108, 407);
            this.btn_CombineData.Name = "btn_CombineData";
            this.btn_CombineData.Size = new System.Drawing.Size(154, 32);
            this.btn_CombineData.TabIndex = 1;
            this.btn_CombineData.Text = "Combine Data";
            this.btn_CombineData.UseVisualStyleBackColor = true;
            this.btn_CombineData.Click += new System.EventHandler(this.btn_CombineData_Click);
            // 
            // lbl_prediction
            // 
            this.lbl_prediction.AutoSize = true;
            this.lbl_prediction.Location = new System.Drawing.Point(962, 376);
            this.lbl_prediction.Name = "lbl_prediction";
            this.lbl_prediction.Size = new System.Drawing.Size(87, 20);
            this.lbl_prediction.TabIndex = 6;
            this.lbl_prediction.Text = "Prediction : ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1274, 451);
            this.Controls.Add(this.lbl_prediction);
            this.Controls.Add(this.btn_CombineData);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_Collection);
            this.Controls.Add(this.cb_Test);
            this.Controls.Add(this.tb_HandData);
            this.Controls.Add(this.tb_Label);
            this.Controls.Add(this.btn_Record);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pb_ImgCam);
            this.Controls.Add(this.pb_DepthImage);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pb_ImgCam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_DepthImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        

        #endregion

        private System.Windows.Forms.PictureBox pb_ImgCam;
        private System.Windows.Forms.PictureBox pb_DepthImage;
        private System.Windows.Forms.Button btn_Record;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_Label;
        private System.Windows.Forms.TextBox tb_HandData;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer tm_Record;
        private System.Windows.Forms.CheckBox cb_Test;
        private System.Windows.Forms.TextBox tb_Collection;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_CombineData;
        private System.Windows.Forms.Label lbl_prediction;
    }
}

