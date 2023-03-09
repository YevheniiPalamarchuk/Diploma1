
namespace test1123
{
    partial class TeamStat
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
            this.dgv1 = new System.Windows.Forms.DataGridView();
            this.txtTeamname = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv1
            // 
            this.dgv1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv1.Location = new System.Drawing.Point(45, 165);
            this.dgv1.Name = "dgv1";
            this.dgv1.RowHeadersWidth = 51;
            this.dgv1.RowTemplate.Height = 29;
            this.dgv1.Size = new System.Drawing.Size(824, 374);
            this.dgv1.TabIndex = 0;
            // 
            // txtTeamname
            // 
            this.txtTeamname.Location = new System.Drawing.Point(45, 84);
            this.txtTeamname.Name = "txtTeamname";
            this.txtTeamname.Size = new System.Drawing.Size(125, 27);
            this.txtTeamname.TabIndex = 1;
            this.txtTeamname.TextChanged += new System.EventHandler(this.txtTeamname_TextChanged);
            // 
            // TeamStat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 571);
            this.Controls.Add(this.txtTeamname);
            this.Controls.Add(this.dgv1);
            this.Name = "TeamStat";
            this.Text = "Teamstat";
            this.Load += new System.EventHandler(this.TeamStat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv1;
        private System.Windows.Forms.TextBox txtTeamname;
    }
}