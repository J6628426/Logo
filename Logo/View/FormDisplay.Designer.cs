namespace LOGO.View
{
    partial class FormDisplay
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
            this.textInput = new System.Windows.Forms.TextBox();
            this.pictureTurtle = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureTurtle)).BeginInit();
            this.SuspendLayout();
            // 
            // textInput
            // 
            this.textInput.AcceptsReturn = true;
            this.textInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textInput.Location = new System.Drawing.Point(2, 420);
            this.textInput.Name = "textInput";
            this.textInput.Size = new System.Drawing.Size(620, 20);
            this.textInput.TabIndex = 0;
            // 
            // pictureTurtle
            // 
            this.pictureTurtle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureTurtle.Location = new System.Drawing.Point(3, 3);
            this.pictureTurtle.Name = "pictureTurtle";
            this.pictureTurtle.Size = new System.Drawing.Size(618, 413);
            this.pictureTurtle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureTurtle.TabIndex = 1;
            this.pictureTurtle.TabStop = false;
            // 
            // FormDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.pictureTurtle);
            this.Controls.Add(this.textInput);
            this.Name = "FormDisplay";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureTurtle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textInput;
        private System.Windows.Forms.PictureBox pictureTurtle;
    }
}

