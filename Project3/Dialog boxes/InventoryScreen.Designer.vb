<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InventoryScreen
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(InventoryScreen))
        Me.lstInventory = New System.Windows.Forms.ListBox()
        Me.picPC = New System.Windows.Forms.PictureBox()
        Me.lblOutput = New System.Windows.Forms.Label()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnPrevious = New System.Windows.Forms.Button()
        Me.btnQuit = New System.Windows.Forms.Button()
        Me.btnEquip = New System.Windows.Forms.Button()
        Me.btnUse = New System.Windows.Forms.Button()
        Me.cmbQuantity = New System.Windows.Forms.ComboBox()
        Me.lblGold = New System.Windows.Forms.Label()
        Me.btnMove = New System.Windows.Forms.Button()
        CType(Me.picPC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lstInventory
        '
        Me.lstInventory.FormattingEnabled = True
        Me.lstInventory.Location = New System.Drawing.Point(12, 142)
        Me.lstInventory.Name = "lstInventory"
        Me.lstInventory.Size = New System.Drawing.Size(475, 290)
        Me.lstInventory.TabIndex = 0
        '
        'picPC
        '
        Me.picPC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.picPC.Image = CType(resources.GetObject("picPC.Image"), System.Drawing.Image)
        Me.picPC.Location = New System.Drawing.Point(13, 13)
        Me.picPC.Name = "picPC"
        Me.picPC.Size = New System.Drawing.Size(120, 120)
        Me.picPC.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picPC.TabIndex = 1
        Me.picPC.TabStop = False
        '
        'lblOutput
        '
        Me.lblOutput.AutoSize = True
        Me.lblOutput.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblOutput.Location = New System.Drawing.Point(139, 13)
        Me.lblOutput.Name = "lblOutput"
        Me.lblOutput.Size = New System.Drawing.Size(39, 13)
        Me.lblOutput.TabIndex = 2
        Me.lblOutput.Text = "Label1"
        '
        'btnNext
        '
        Me.btnNext.Location = New System.Drawing.Point(493, 232)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(107, 23)
        Me.btnNext.TabIndex = 3
        Me.btnNext.Text = "Next Character"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnPrevious
        '
        Me.btnPrevious.Location = New System.Drawing.Point(493, 261)
        Me.btnPrevious.Name = "btnPrevious"
        Me.btnPrevious.Size = New System.Drawing.Size(107, 23)
        Me.btnPrevious.TabIndex = 4
        Me.btnPrevious.Text = "Previous Character"
        Me.btnPrevious.UseVisualStyleBackColor = True
        '
        'btnQuit
        '
        Me.btnQuit.Location = New System.Drawing.Point(493, 409)
        Me.btnQuit.Name = "btnQuit"
        Me.btnQuit.Size = New System.Drawing.Size(107, 23)
        Me.btnQuit.TabIndex = 5
        Me.btnQuit.Text = "Exit"
        Me.btnQuit.UseVisualStyleBackColor = True
        '
        'btnEquip
        '
        Me.btnEquip.Location = New System.Drawing.Point(493, 142)
        Me.btnEquip.Name = "btnEquip"
        Me.btnEquip.Size = New System.Drawing.Size(107, 23)
        Me.btnEquip.TabIndex = 6
        Me.btnEquip.Text = "Equip"
        Me.btnEquip.UseVisualStyleBackColor = True
        '
        'btnUse
        '
        Me.btnUse.Location = New System.Drawing.Point(493, 171)
        Me.btnUse.Name = "btnUse"
        Me.btnUse.Size = New System.Drawing.Size(107, 23)
        Me.btnUse.TabIndex = 7
        Me.btnUse.Text = "Use"
        Me.btnUse.UseVisualStyleBackColor = True
        '
        'cmbQuantity
        '
        Me.cmbQuantity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbQuantity.FormattingEnabled = True
        Me.cmbQuantity.Location = New System.Drawing.Point(493, 382)
        Me.cmbQuantity.Name = "cmbQuantity"
        Me.cmbQuantity.Size = New System.Drawing.Size(107, 21)
        Me.cmbQuantity.TabIndex = 8
        Me.cmbQuantity.Visible = False
        '
        'lblGold
        '
        Me.lblGold.AutoSize = True
        Me.lblGold.Location = New System.Drawing.Point(13, 439)
        Me.lblGold.Name = "lblGold"
        Me.lblGold.Size = New System.Drawing.Size(39, 13)
        Me.lblGold.TabIndex = 9
        Me.lblGold.Text = "Label1"
        '
        'btnMove
        '
        Me.btnMove.Location = New System.Drawing.Point(493, 203)
        Me.btnMove.Name = "btnMove"
        Me.btnMove.Size = New System.Drawing.Size(107, 23)
        Me.btnMove.TabIndex = 10
        Me.btnMove.Text = "Move"
        Me.btnMove.UseVisualStyleBackColor = True
        '
        'InventoryScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(624, 462)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnMove)
        Me.Controls.Add(Me.lblGold)
        Me.Controls.Add(Me.cmbQuantity)
        Me.Controls.Add(Me.btnUse)
        Me.Controls.Add(Me.btnEquip)
        Me.Controls.Add(Me.btnQuit)
        Me.Controls.Add(Me.btnPrevious)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.lblOutput)
        Me.Controls.Add(Me.picPC)
        Me.Controls.Add(Me.lstInventory)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "InventoryScreen"
        Me.ShowIcon = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "InventoryScreen"
        CType(Me.picPC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lstInventory As System.Windows.Forms.ListBox
    Friend WithEvents picPC As System.Windows.Forms.PictureBox
    Friend WithEvents lblOutput As System.Windows.Forms.Label
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnPrevious As System.Windows.Forms.Button
    Friend WithEvents btnQuit As System.Windows.Forms.Button
    Friend WithEvents btnEquip As System.Windows.Forms.Button
    Friend WithEvents btnUse As System.Windows.Forms.Button
    Friend WithEvents cmbQuantity As System.Windows.Forms.ComboBox
    Friend WithEvents lblGold As System.Windows.Forms.Label
    Friend WithEvents btnMove As System.Windows.Forms.Button
End Class
