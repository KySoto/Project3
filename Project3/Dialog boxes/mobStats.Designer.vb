<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class mobStats
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
        Me.picPortrate = New System.Windows.Forms.PictureBox()
        Me.lblName = New System.Windows.Forms.Label()
        Me.lblClass = New System.Windows.Forms.Label()
        Me.lblHP = New System.Windows.Forms.Label()
        Me.lblMP = New System.Windows.Forms.Label()
        Me.lblExpC = New System.Windows.Forms.Label()
        Me.lblExpN = New System.Windows.Forms.Label()
        Me.lblAttack = New System.Windows.Forms.Label()
        Me.lblDefense = New System.Windows.Forms.Label()
        Me.lblMagic = New System.Windows.Forms.Label()
        Me.lblResist = New System.Windows.Forms.Label()
        Me.lblSpeed = New System.Windows.Forms.Label()
        Me.lblMhand = New System.Windows.Forms.Label()
        Me.lblOhand = New System.Windows.Forms.Label()
        Me.lblArmor = New System.Windows.Forms.Label()
        Me.btnPrevious = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnQuit = New System.Windows.Forms.Button()
        CType(Me.picPortrate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picPortrate
        '
        Me.picPortrate.BackColor = System.Drawing.Color.Transparent
        Me.picPortrate.Location = New System.Drawing.Point(12, 12)
        Me.picPortrate.Name = "picPortrate"
        Me.picPortrate.Size = New System.Drawing.Size(120, 120)
        Me.picPortrate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picPortrate.TabIndex = 0
        Me.picPortrate.TabStop = False
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblName.Location = New System.Drawing.Point(149, 12)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(51, 20)
        Me.lblName.TabIndex = 1
        Me.lblName.Text = "Name"
        '
        'lblClass
        '
        Me.lblClass.AutoSize = True
        Me.lblClass.Location = New System.Drawing.Point(150, 32)
        Me.lblClass.Name = "lblClass"
        Me.lblClass.Size = New System.Drawing.Size(41, 13)
        Me.lblClass.TabIndex = 2
        Me.lblClass.Text = "Class 1"
        '
        'lblHP
        '
        Me.lblHP.AutoSize = True
        Me.lblHP.Location = New System.Drawing.Point(150, 71)
        Me.lblHP.Name = "lblHP"
        Me.lblHP.Size = New System.Drawing.Size(42, 13)
        Me.lblHP.TabIndex = 3
        Me.lblHP.Text = "HP 0/0"
        '
        'lblMP
        '
        Me.lblMP.AutoSize = True
        Me.lblMP.Location = New System.Drawing.Point(150, 84)
        Me.lblMP.Name = "lblMP"
        Me.lblMP.Size = New System.Drawing.Size(43, 13)
        Me.lblMP.TabIndex = 4
        Me.lblMP.Text = "MP 0/0"
        '
        'lblExpC
        '
        Me.lblExpC.AutoSize = True
        Me.lblExpC.Location = New System.Drawing.Point(150, 45)
        Me.lblExpC.Name = "lblExpC"
        Me.lblExpC.Size = New System.Drawing.Size(71, 13)
        Me.lblExpC.TabIndex = 5
        Me.lblExpC.Text = "Current Exp 0"
        '
        'lblExpN
        '
        Me.lblExpN.AutoSize = True
        Me.lblExpN.Location = New System.Drawing.Point(150, 58)
        Me.lblExpN.Name = "lblExpN"
        Me.lblExpN.Size = New System.Drawing.Size(73, 13)
        Me.lblExpN.TabIndex = 6
        Me.lblExpN.Text = "Next Level 20"
        '
        'lblAttack
        '
        Me.lblAttack.AutoSize = True
        Me.lblAttack.Location = New System.Drawing.Point(9, 154)
        Me.lblAttack.Name = "lblAttack"
        Me.lblAttack.Size = New System.Drawing.Size(65, 13)
        Me.lblAttack.TabIndex = 7
        Me.lblAttack.Text = "Attack: 4 (2)"
        '
        'lblDefense
        '
        Me.lblDefense.AutoSize = True
        Me.lblDefense.Location = New System.Drawing.Point(9, 169)
        Me.lblDefense.Name = "lblDefense"
        Me.lblDefense.Size = New System.Drawing.Size(74, 13)
        Me.lblDefense.TabIndex = 8
        Me.lblDefense.Text = "Defense: 2 (1)"
        '
        'lblMagic
        '
        Me.lblMagic.AutoSize = True
        Me.lblMagic.Location = New System.Drawing.Point(9, 189)
        Me.lblMagic.Name = "lblMagic"
        Me.lblMagic.Size = New System.Drawing.Size(63, 13)
        Me.lblMagic.TabIndex = 9
        Me.lblMagic.Text = "Magic: 0 (0)"
        '
        'lblResist
        '
        Me.lblResist.AutoSize = True
        Me.lblResist.Location = New System.Drawing.Point(9, 204)
        Me.lblResist.Name = "lblResist"
        Me.lblResist.Size = New System.Drawing.Size(63, 13)
        Me.lblResist.TabIndex = 10
        Me.lblResist.Text = "Resist: 3 (0)"
        '
        'lblSpeed
        '
        Me.lblSpeed.AutoSize = True
        Me.lblSpeed.Location = New System.Drawing.Point(9, 224)
        Me.lblSpeed.Name = "lblSpeed"
        Me.lblSpeed.Size = New System.Drawing.Size(71, 13)
        Me.lblSpeed.TabIndex = 11
        Me.lblSpeed.Text = "Speed: -1 (-5)"
        '
        'lblMhand
        '
        Me.lblMhand.AutoSize = True
        Me.lblMhand.Location = New System.Drawing.Point(150, 154)
        Me.lblMhand.Name = "lblMhand"
        Me.lblMhand.Size = New System.Drawing.Size(151, 13)
        Me.lblMhand.TabIndex = 12
        Me.lblMhand.Text = "MainHand: Dagger (+2 attack)"
        '
        'lblOhand
        '
        Me.lblOhand.AutoSize = True
        Me.lblOhand.Location = New System.Drawing.Point(150, 189)
        Me.lblOhand.Name = "lblOhand"
        Me.lblOhand.Size = New System.Drawing.Size(126, 13)
        Me.lblOhand.TabIndex = 13
        Me.lblOhand.Text = "Offhand: Shiv (+2 attack)"
        '
        'lblArmor
        '
        Me.lblArmor.AutoSize = True
        Me.lblArmor.Location = New System.Drawing.Point(150, 224)
        Me.lblArmor.Name = "lblArmor"
        Me.lblArmor.Size = New System.Drawing.Size(192, 13)
        Me.lblArmor.TabIndex = 14
        Me.lblArmor.Text = "Armor: FullPlate (+10 defense -7 speed)"
        '
        'btnPrevious
        '
        Me.btnPrevious.Location = New System.Drawing.Point(12, 283)
        Me.btnPrevious.Name = "btnPrevious"
        Me.btnPrevious.Size = New System.Drawing.Size(75, 23)
        Me.btnPrevious.TabIndex = 1
        Me.btnPrevious.Text = "Previous"
        Me.btnPrevious.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Location = New System.Drawing.Point(12, 254)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(75, 23)
        Me.btnNext.TabIndex = 0
        Me.btnNext.Text = "Next"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnQuit
        '
        Me.btnQuit.Location = New System.Drawing.Point(13, 313)
        Me.btnQuit.Name = "btnQuit"
        Me.btnQuit.Size = New System.Drawing.Size(75, 23)
        Me.btnQuit.TabIndex = 2
        Me.btnQuit.Text = "Exit"
        Me.btnQuit.UseVisualStyleBackColor = True
        '
        'charStats
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(386, 343)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnQuit)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.btnPrevious)
        Me.Controls.Add(Me.lblArmor)
        Me.Controls.Add(Me.lblOhand)
        Me.Controls.Add(Me.lblMhand)
        Me.Controls.Add(Me.lblSpeed)
        Me.Controls.Add(Me.lblResist)
        Me.Controls.Add(Me.lblMagic)
        Me.Controls.Add(Me.lblDefense)
        Me.Controls.Add(Me.lblAttack)
        Me.Controls.Add(Me.lblExpN)
        Me.Controls.Add(Me.lblExpC)
        Me.Controls.Add(Me.lblMP)
        Me.Controls.Add(Me.lblHP)
        Me.Controls.Add(Me.lblClass)
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.picPortrate)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "charStats"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = " "
        CType(Me.picPortrate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents picPortrate As System.Windows.Forms.PictureBox
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents lblClass As System.Windows.Forms.Label
    Friend WithEvents lblHP As System.Windows.Forms.Label
    Friend WithEvents lblMP As System.Windows.Forms.Label
    Friend WithEvents lblExpC As System.Windows.Forms.Label
    Friend WithEvents lblExpN As System.Windows.Forms.Label
    Friend WithEvents lblAttack As System.Windows.Forms.Label
    Friend WithEvents lblDefense As System.Windows.Forms.Label
    Friend WithEvents lblMagic As System.Windows.Forms.Label
    Friend WithEvents lblResist As System.Windows.Forms.Label
    Friend WithEvents lblSpeed As System.Windows.Forms.Label
    Friend WithEvents lblMhand As System.Windows.Forms.Label
    Friend WithEvents lblOhand As System.Windows.Forms.Label
    Friend WithEvents lblArmor As System.Windows.Forms.Label
    Friend WithEvents btnPrevious As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnQuit As System.Windows.Forms.Button
End Class
