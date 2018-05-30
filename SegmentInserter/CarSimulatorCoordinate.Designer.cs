using System;

namespace SegmentInserter
{
    partial class SegmentInserter
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
			this.button1 = new System.Windows.Forms.Button();
			this.StateLabel = new System.Windows.Forms.Label();
			this.DirectionLabel = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.button1.Location = new System.Drawing.Point(87, 370);
			this.button1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(309, 70);
			this.button1.TabIndex = 0;
			this.button1.Text = "StartInsert";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// StateLabel
			// 
			this.StateLabel.AutoSize = true;
			this.StateLabel.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.StateLabel.Location = new System.Drawing.Point(15, 24);
			this.StateLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
			this.StateLabel.Name = "StateLabel";
			this.StateLabel.Size = new System.Drawing.Size(95, 19);
			this.StateLabel.TabIndex = 1;
			this.StateLabel.Text = "StateLabel";
			this.StateLabel.Click += new System.EventHandler(this.label1_Click);
			// 
			// DirectionLabel
			// 
			this.DirectionLabel.AutoSize = true;
			this.DirectionLabel.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.DirectionLabel.Location = new System.Drawing.Point(108, 119);
			this.DirectionLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
			this.DirectionLabel.Name = "DirectionLabel";
			this.DirectionLabel.Size = new System.Drawing.Size(121, 21);
			this.DirectionLabel.TabIndex = 3;
			this.DirectionLabel.Text = "START_NUM";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1.Location = new System.Drawing.Point(132, 169);
			this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(97, 21);
			this.label1.TabIndex = 5;
			this.label1.Text = "END_NUM";
			this.label1.Click += new System.EventHandler(this.label1_Click);
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(262, 119);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(134, 28);
			this.textBox2.TabIndex = 7;
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(262, 169);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(134, 28);
			this.textBox3.TabIndex = 8;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label2.Location = new System.Drawing.Point(132, 228);
			this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(81, 21);
			this.label2.TabIndex = 9;
			this.label2.Text = "Velocity";
			this.label2.Click += new System.EventHandler(this.label1_Click);
			// 
			// textBox4
			// 
			this.textBox4.Location = new System.Drawing.Point(262, 225);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(115, 28);
			this.textBox4.TabIndex = 10;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label3.Location = new System.Drawing.Point(132, 278);
			this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(0, 21);
			this.label3.TabIndex = 12;
			// 
			// SegmentInserter
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(457, 454);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBox4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.DirectionLabel);
			this.Controls.Add(this.StateLabel);
			this.Controls.Add(this.button1);
			this.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
			this.Name = "SegmentInserter";
			this.Text = "SegmentInserter";
			this.Load += new System.EventHandler(this.SegmentInserter_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        private void SemanticLabel_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label StateLabel;
        private System.Windows.Forms.Label DirectionLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.Label label3;
	}
}

