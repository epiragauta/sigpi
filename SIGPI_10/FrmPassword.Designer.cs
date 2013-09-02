namespace SIGPI_10
{
  partial class FrmPassword
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
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnAceptar = new System.Windows.Forms.Button();
      this.txtClave = new System.Windows.Forms.TextBox();
      this.txUsuario = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(158, 71);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 11;
      this.btnCancel.Text = "&Cancelar";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // btnAceptar
      // 
      this.btnAceptar.Location = new System.Drawing.Point(69, 71);
      this.btnAceptar.Name = "btnAceptar";
      this.btnAceptar.Size = new System.Drawing.Size(75, 23);
      this.btnAceptar.TabIndex = 10;
      this.btnAceptar.Text = "&Aceptar";
      this.btnAceptar.UseVisualStyleBackColor = true;
      this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
      // 
      // txtClave
      // 
      this.txtClave.Location = new System.Drawing.Point(85, 38);
      this.txtClave.Name = "txtClave";
      this.txtClave.PasswordChar = '*';
      this.txtClave.Size = new System.Drawing.Size(148, 20);
      this.txtClave.TabIndex = 9;
      // 
      // txUsuario
      // 
      this.txUsuario.Location = new System.Drawing.Point(85, 12);
      this.txUsuario.Name = "txUsuario";
      this.txUsuario.Size = new System.Drawing.Size(148, 20);
      this.txUsuario.TabIndex = 8;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(15, 45);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(64, 13);
      this.label2.TabIndex = 7;
      this.label2.Text = "Contraseña:";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(15, 15);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(46, 13);
      this.label1.TabIndex = 6;
      this.label1.Text = "Usuario:";
      // 
      // FrmPassword
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(269, 108);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnAceptar);
      this.Controls.Add(this.txtClave);
      this.Controls.Add(this.txUsuario);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Name = "FrmPassword";
      this.Text = "FrmPassword";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnAceptar;
    private System.Windows.Forms.TextBox txtClave;
    private System.Windows.Forms.TextBox txUsuario;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
  }
}