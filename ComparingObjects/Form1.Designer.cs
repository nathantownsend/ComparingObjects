﻿namespace ComparingObjects
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
            this.button2 = new System.Windows.Forms.Button();
            this.CompareLocation = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(24, 31);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(221, 83);
            this.button2.TabIndex = 1;
            this.button2.Text = "Run Comparison";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // CompareLocation
            // 
            this.CompareLocation.Location = new System.Drawing.Point(294, 31);
            this.CompareLocation.Name = "CompareLocation";
            this.CompareLocation.Size = new System.Drawing.Size(229, 83);
            this.CompareLocation.TabIndex = 2;
            this.CompareLocation.Text = "Compare Location";
            this.CompareLocation.UseVisualStyleBackColor = true;
            this.CompareLocation.Click += new System.EventHandler(this.CompareLocation_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 344);
            this.Controls.Add(this.CompareLocation);
            this.Controls.Add(this.button2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button CompareLocation;

    }
}

