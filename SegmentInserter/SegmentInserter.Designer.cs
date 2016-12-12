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
            this.Direction = new System.Windows.Forms.ComboBox();
            this.DirectionLabel = new System.Windows.Forms.Label();
            this.SemanticLabel = new System.Windows.Forms.Label();
            this.SemanticLink = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button1.Location = new System.Drawing.Point(45, 450);
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
            this.StateLabel.Location = new System.Drawing.Point(59, 55);
            this.StateLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.StateLabel.Name = "StateLabel";
            this.StateLabel.Size = new System.Drawing.Size(95, 19);
            this.StateLabel.TabIndex = 1;
            this.StateLabel.Text = "StateLabel";
            this.StateLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // Direction
            // 
            this.Direction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Direction.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Direction.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Direction.FormattingEnabled = true;
            this.Direction.Items.AddRange(new object[] {
            "Outward",
            "Homeward"});
            this.Direction.Location = new System.Drawing.Point(59, 200);
            this.Direction.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Direction.Name = "Direction";
            this.Direction.Size = new System.Drawing.Size(276, 29);
            this.Direction.TabIndex = 2;
            // 
            // DirectionLabel
            // 
            this.DirectionLabel.AutoSize = true;
            this.DirectionLabel.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DirectionLabel.Location = new System.Drawing.Point(59, 138);
            this.DirectionLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.DirectionLabel.Name = "DirectionLabel";
            this.DirectionLabel.Size = new System.Drawing.Size(90, 21);
            this.DirectionLabel.TabIndex = 3;
            this.DirectionLabel.Text = "Direction";
            // 
            // SemanticLabel
            // 
            this.SemanticLabel.AutoSize = true;
            this.SemanticLabel.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SemanticLabel.Location = new System.Drawing.Point(59, 276);
            this.SemanticLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.SemanticLabel.Name = "SemanticLabel";
            this.SemanticLabel.Size = new System.Drawing.Size(128, 21);
            this.SemanticLabel.TabIndex = 4;
            this.SemanticLabel.Text = "SemanticLink";
            // 
            // SemanticLink
            // 
            this.SemanticLink.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SemanticLink.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.SemanticLink.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SemanticLink.FormattingEnabled = true;
            this.SemanticLink.Items.AddRange(new object[] {
            "220 復路マップマッチング用",
            "221 往路マップマッチング用",
            "224 復路マップマッチング用２",
            "225 往路マップマッチング用２"});
            this.SemanticLink.Location = new System.Drawing.Point(59, 357);
            this.SemanticLink.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.SemanticLink.Name = "SemanticLink";
            this.SemanticLink.Size = new System.Drawing.Size(276, 29);
            this.SemanticLink.TabIndex = 5;
            // 
            // SegmentInserter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 583);
            this.Controls.Add(this.SemanticLink);
            this.Controls.Add(this.SemanticLabel);
            this.Controls.Add(this.DirectionLabel);
            this.Controls.Add(this.Direction);
            this.Controls.Add(this.StateLabel);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "SegmentInserter";
            this.Text = "SegmentInserter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label StateLabel;
        private System.Windows.Forms.ComboBox Direction;
        private System.Windows.Forms.Label DirectionLabel;
        private System.Windows.Forms.Label SemanticLabel;
        private System.Windows.Forms.ComboBox SemanticLink;
    }
}

