Imports System.Windows.Forms
Imports Project3.main
Public Class escMenu
    Public btn(6) As Button
    Private Sub buttondoodle(sender As Object, e As EventArgs)
        Dim temp As Button = CType(sender, Button)
        Select Case temp.TabIndex
            Case 0
                main.NewGame(False)
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                Me.Close()
            Case 1
                MsgBox(SaveSystem.Save(0))
            Case 2
                Dim i As Integer = SaveSystem.Load(0)
                MsgBox(i)
                If i = 1 Then
                    main.NewGame(True)
                End If
            Case 3
                MsgBox("To be added")
            Case 4
                MsgBox("Esc brings up the menu that you are currently using to read this document. " & ControlChars.NewLine & "In this beta test f1-f4 allows you to level up your characters by giving them experiance. The maximum level is 100." & ControlChars.NewLine & "e brings up the inventory menu." & ControlChars.NewLine & "q opens the character statistics menu, you can chose who is shown on the world map from here." & ControlChars.NewLine & "While in battle, you CAN cast offensive spells and attack your allies and even yourself." & ControlChars.NewLine & "Movement can be done with A S W D or the arrow keys.", MsgBoxStyle.OkOnly, "Halp for noobs")
            Case 5
                Me.DialogResult = Windows.Forms.DialogResult.Abort
                main.Close()
            Case 6
                Me.Close()
        End Select
    End Sub
    Public Sub buttonmovement(sender As Object, e As KeyPressEventArgs)
        Dim temp As Button = CType(sender, Button)
        If e.KeyChar = "w" Then
            SendKeys.Send("{UP}")
        ElseIf e.KeyChar = "s" Then
            SendKeys.Send("{DOWN}")
        End If
    End Sub

    Private Sub escMenu_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        main.KeyPreview = True
        main.PauseAll = False
        For i As Integer = 0 To 6 Step 1
            btn(i).Dispose()
        Next
    End Sub

    Private Sub escMenu_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape And main.GameStarted Then
            Me.Close()
        End If
    End Sub

    Private Sub escMenu_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Size = New Size(100, 244)

        For i As Integer = 0 To 6 Step 1
            btn(i) = New Button
            btn(i).Name = i.ToString
            btn(i).Location = New Point(12, 12 + (i * 29))
            btn(i).Size = New Size(75, 23)
            btn(i).Font = New Font("Microsoft Sans Serif", 8.25)
            btn(i).BackColor = SystemColors.Control
            btn(i).TabIndex = i
            btn(i).TabStop = True
            Select Case i
                Case 0
                    btn(i).Text = "New Game"
                    btn(i).Name = "btnNewGame"
                Case 1
                    btn(i).Text = "Save Game"
                    btn(i).Name = "btnSaveGame"
                    btn(i).Enabled = main.SaveEnable
                Case 2
                    btn(i).Text = "Load Game"
                    btn(i).Name = "btnLoadGame"
                    btn(i).Enabled = main.LoadEnable
                Case 3
                    btn(i).Text = "Options"
                    btn(i).Name = "btnOptions"
                    btn(i).Enabled = False
                Case 4
                    btn(i).Text = "Help"
                    btn(i).Name = "btnHelp"
                Case 5
                    btn(i).Text = "Quit"
                    btn(i).Name = "btnQuit"
                Case 6
                    btn(i).Text = "Exit Menu"
                    btn(i).Name = "btnExitMenu"
                    btn(i).Enabled = main.CloseEnable
                    btn(i).Location = New Point(12, btn(i - 1).Location.Y + 52)
            End Select
            btn(i).Visible = True
            Me.Controls.Add(btn(i))
            AddHandler btn(i).Click, AddressOf buttondoodle
            AddHandler btn(i).KeyPress, AddressOf buttonmovement
        Next

    End Sub

    Private Sub escMenu_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        main.KeyPreview = False
        Me.KeyPreview = True
        main.PauseAll = True
    End Sub
End Class
