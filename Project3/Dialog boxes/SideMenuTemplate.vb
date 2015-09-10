Option Explicit On
Option Infer Off
Option Strict On
Imports System.Windows.Forms
Imports Project3.main
'template version
Public Class SideMenuTemplate
    Private btn(8) As Button
    Private lblTitle As Label
    Private Sub ButtonClicks(sender As Object, e As EventArgs)
        Dim temp As Button = CType(sender, Button)
        Select Case temp.TabIndex
            Case 0

            Case 1

            Case 2

            Case 3

            Case 4

            Case 5

            Case 6

            Case 7

            Case 8

        End Select
    End Sub
    Public Sub ButtonKey(sender As Object, e As KeyPressEventArgs)
        Dim temp As Button = CType(sender, Button)
        If e.KeyChar = "w" Then
            SendKeys.Send("{UP}")
        ElseIf e.KeyChar = "s" Then
            SendKeys.Send("{DOWN}")
        End If
    End Sub

    Private Sub SideMenuTemplate_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        For i As Integer = 0 To 8 Step 1
            btn(i).Dispose()
        Next
    End Sub
    Private Sub ActionMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Size = New Size(100, 220)
        Me.Location = New Point(main.Location.X + 9, main.Location.Y + 577)
        Me.BackgroundImage = Image.FromFile("art\ActionMenu.png")
        For i As Integer = 0 To 8 Step 1
            btn(i) = New Button
            btn(i).Name = "btn" & i.ToString
            btn(i).Location = New Point(8, 18 + (i * 22))
            btn(i).Size = New Size(84, 20)
            btn(i).Font = New Font("Microsoft Sans Serif", 8.25)
            btn(i).BackColor = SystemColors.Control
            btn(i).TabIndex = i
            btn(i).TabStop = True
            btn(i).Text = ""
            'Select Case i
            '    Case 0
            '        btn(i).Text = "New Game"
            '    Case 1
            '        btn(i).Text = "Save Game"
            '    Case 2
            '        btn(i).Text = "Load Game"
            '    Case 3
            '        btn(i).Text = "Options"
            '    Case 4
            '        btn(i).Text = "Help"
            '    Case 5
            '        btn(i).Text = "Quit"
            '    Case 6
            '        btn(i).Text = "Exit Menu"
            'End Select
            btn(i).Visible = True
            Me.Controls.Add(btn(i))

            AddHandler btn(i).Click, AddressOf ButtonClicks
            AddHandler btn(i).KeyPress, AddressOf ButtonKey
        Next
        lblTitle = New Label
        lblTitle.Text = ""
        lblTitle.Name = "lblTitle"
        lblTitle.TabIndex = 9
        lblTitle.Location = New Point(8, 2)
        lblTitle.Font = New Font("Microsoft Sans Serif", 8.25)
    End Sub
End Class
