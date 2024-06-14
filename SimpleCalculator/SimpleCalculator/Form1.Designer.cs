namespace SimpleCalculator
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox textBox_Result;
        private System.Windows.Forms.Button[] numberButtons;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonSubtract;
        private System.Windows.Forms.Button buttonMultiply;
        private System.Windows.Forms.Button buttonDivide;
        private System.Windows.Forms.Button buttonEquals;
        private System.Windows.Forms.Button buttonClear;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.textBox_Result = new System.Windows.Forms.TextBox();
            this.numberButtons = new System.Windows.Forms.Button[10];
            for (int i = 0; i < 10; i++)
            {
                this.numberButtons[i] = new System.Windows.Forms.Button();
                this.numberButtons[i].Text = i.ToString();
                this.numberButtons[i].Size = new System.Drawing.Size(50, 50);
                this.numberButtons[i].Click += new System.EventHandler(this.button_Click);
            }
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonSubtract = new System.Windows.Forms.Button();
            this.buttonMultiply = new System.Windows.Forms.Button();
            this.buttonDivide = new System.Windows.Forms.Button();
            this.buttonEquals = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // textBox_Result
            this.textBox_Result.Location = new System.Drawing.Point(10, 10);
            this.textBox_Result.Name = "textBox_Result";
            this.textBox_Result.Size = new System.Drawing.Size(260, 20);
            this.textBox_Result.TabIndex = 0;
            this.textBox_Result.Text = "0";

            // numberButtons
            for (int i = 0; i < 10; i++)
            {
                this.numberButtons[i].Location = new System.Drawing.Point(10 + (i % 3) * 60, 40 + (i / 3) * 60);
                this.numberButtons[i].Name = "button" + i;
                this.numberButtons[i].TabIndex = i + 1;
            }

            // buttonAdd
            this.buttonAdd.Location = new System.Drawing.Point(190, 40);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(50, 50);
            this.buttonAdd.TabIndex = 10;
            this.buttonAdd.Text = "+";
            this.buttonAdd.Click += new System.EventHandler(this.operator_Click);

            // buttonSubtract
            this.buttonSubtract.Location = new System.Drawing.Point(190, 100);
            this.buttonSubtract.Name = "buttonSubtract";
            this.buttonSubtract.Size = new System.Drawing.Size(50, 50);
            this.buttonSubtract.TabIndex = 11;
            this.buttonSubtract.Text = "-";
            this.buttonSubtract.Click += new System.EventHandler(this.operator_Click);

            // buttonMultiply
            this.buttonMultiply.Location = new System.Drawing.Point(190, 160);
            this.buttonMultiply.Name = "buttonMultiply";
            this.buttonMultiply.Size = new System.Drawing.Size(50, 50);
            this.buttonMultiply.TabIndex = 12;
            this.buttonMultiply.Text = "*";
            this.buttonMultiply.Click += new System.EventHandler(this.operator_Click);

            // buttonDivide
            this.buttonDivide.Location = new System.Drawing.Point(190, 220);
            this.buttonDivide.Name = "buttonDivide";
            this.buttonDivide.Size = new System.Drawing.Size(50, 50);
            this.buttonDivide.TabIndex = 13;
            this.buttonDivide.Text = "/";
            this.buttonDivide.Click += new System.EventHandler(this.operator_Click);

            // buttonEquals
            this.buttonEquals.Location = new System.Drawing.Point(250, 160);
            this.buttonEquals.Name = "buttonEquals";
            this.buttonEquals.Size = new System.Drawing.Size(50, 110);
            this.buttonEquals.TabIndex = 14;
            this.buttonEquals.Text = "=";
            this.buttonEquals.Click += new System.EventHandler(this.buttonEquals_Click);

            // buttonClear
            this.buttonClear.Location = new System.Drawing.Point(250, 40);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(50, 110);
            this.buttonClear.TabIndex = 15;
            this.buttonClear.Text = "C";
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 281);
            this.Controls.Add(this.textBox_Result);
            foreach (var button in numberButtons)
            {
                this.Controls.Add(button);
            }
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonSubtract);
            this.Controls.Add(this.buttonMultiply);
            this.Controls.Add(this.buttonDivide);
            this.Controls.Add(this.buttonEquals);
            this.Controls.Add(this.buttonClear);
            this.Name = "Form1";
            this.Text = "Simple Calculator";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
