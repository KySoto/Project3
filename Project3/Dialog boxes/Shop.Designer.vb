<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Shop
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lstinventory = New System.Windows.Forms.ListBox()
        Me.btnBuy = New System.Windows.Forms.Button()
        Me.btnQuit = New System.Windows.Forms.Button()
        Me.cmbQuantity = New System.Windows.Forms.ComboBox()
        Me.lblCash = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lstinventory
        '
        Me.lstinventory.FormattingEnabled = True
        Me.lstinventory.Location = New System.Drawing.Point(13, 13)
        Me.lstinventory.Name = "lstinventory"
        Me.lstinventory.Size = New System.Drawing.Size(258, 342)
        Me.lstinventory.TabIndex = 0
        '
        'btnBuy
        '
        Me.btnBuy.Location = New System.Drawing.Point(277, 39)
        Me.btnBuy.Name = "btnBuy"
        Me.btnBuy.Size = New System.Drawing.Size(75, 23)
        Me.btnBuy.TabIndex = 1
        Me.btnBuy.Text = "Buy"
        Me.btnBuy.UseVisualStyleBackColor = True
        '
        'btnQuit
        '
        Me.btnQuit.Location = New System.Drawing.Point(278, 321)
        Me.btnQuit.Name = "btnQuit"
        Me.btnQuit.Size = New System.Drawing.Size(75, 23)
        Me.btnQuit.TabIndex = 2
        Me.btnQuit.Text = "Quit"
        Me.btnQuit.UseVisualStyleBackColor = True
        '
        'cmbQuantity
        '
        Me.cmbQuantity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbQuantity.FormattingEnabled = True
        Me.cmbQuantity.Location = New System.Drawing.Point(277, 68)
        Me.cmbQuantity.Name = "cmbQuantity"
        Me.cmbQuantity.Size = New System.Drawing.Size(75, 21)
        Me.cmbQuantity.TabIndex = 3
        '
        'lblCash
        '
        Me.lblCash.AutoSize = True
        Me.lblCash.Location = New System.Drawing.Point(277, 13)
        Me.lblCash.Name = "lblCash"
        Me.lblCash.Size = New System.Drawing.Size(39, 13)
        Me.lblCash.TabIndex = 4
        Me.lblCash.Text = "Label1"
        '
        'Shop
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(490, 356)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblCash)
        Me.Controls.Add(Me.cmbQuantity)
        Me.Controls.Add(Me.btnQuit)
        Me.Controls.Add(Me.btnBuy)
        Me.Controls.Add(Me.lstinventory)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "Shop"
        Me.Text = "Shop"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lstinventory As System.Windows.Forms.ListBox
    Friend WithEvents btnBuy As System.Windows.Forms.Button
    Friend WithEvents btnQuit As System.Windows.Forms.Button
    Friend WithEvents cmbQuantity As System.Windows.Forms.ComboBox
    Friend WithEvents lblCash As System.Windows.Forms.Label
End Class
