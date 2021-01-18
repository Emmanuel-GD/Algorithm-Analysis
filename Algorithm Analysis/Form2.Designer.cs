
namespace AnalysisOfAlgorithms
{
    partial class Form2
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.RandomArray = new System.Windows.Forms.Button();
            this.SortedArray = new System.Windows.Forms.Button();
            this.SortedDESC = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 276);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(456, 222);
            this.dataGridView1.TabIndex = 0;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(474, 276);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(456, 222);
            this.dataGridView2.TabIndex = 0;
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(936, 276);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.Size = new System.Drawing.Size(456, 222);
            this.dataGridView3.TabIndex = 0;
            // 
            // RandomArray
            // 
            this.RandomArray.Location = new System.Drawing.Point(12, 236);
            this.RandomArray.Name = "RandomArray";
            this.RandomArray.Size = new System.Drawing.Size(75, 23);
            this.RandomArray.TabIndex = 1;
            this.RandomArray.Text = "Random";
            this.RandomArray.UseVisualStyleBackColor = true;
            this.RandomArray.Click += new System.EventHandler(this.RandomArray_Click);
            // 
            // SortedArray
            // 
            this.SortedArray.Location = new System.Drawing.Point(474, 236);
            this.SortedArray.Name = "SortedArray";
            this.SortedArray.Size = new System.Drawing.Size(75, 23);
            this.SortedArray.TabIndex = 1;
            this.SortedArray.Text = "Sorted ASC";
            this.SortedArray.UseVisualStyleBackColor = true;
            this.SortedArray.Click += new System.EventHandler(this.SortedArray_Click);
            // 
            // SortedDESC
            // 
            this.SortedDESC.Location = new System.Drawing.Point(936, 236);
            this.SortedDESC.Name = "SortedDESC";
            this.SortedDESC.Size = new System.Drawing.Size(75, 23);
            this.SortedDESC.TabIndex = 1;
            this.SortedDESC.Text = "Sorted DESC";
            this.SortedDESC.UseVisualStyleBackColor = true;
            this.SortedDESC.Click += new System.EventHandler(this.SortedDESC_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1404, 831);
            this.Controls.Add(this.SortedDESC);
            this.Controls.Add(this.SortedArray);
            this.Controls.Add(this.RandomArray);
            this.Controls.Add(this.dataGridView3);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form2";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Button RandomArray;
        private System.Windows.Forms.Button SortedArray;
        private System.Windows.Forms.Button SortedDESC;
    }
}