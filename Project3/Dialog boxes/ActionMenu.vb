Option Explicit On
Option Infer Off
Option Strict On
Imports System.Windows.Forms
Imports Project3.main

Public Class ActionMenu
    Private Magic As Boolean = False
    Public player As Integer = 0
    Public Target As Integer = 0
    Public ability As Integer = 0
    Private targeting As Boolean = False
    Private attack As Boolean = False
    Private _invenResult As DialogResult = Windows.Forms.DialogResult.None
    Private btn(8) As Button
    Private lblTitle As Label = New Label
    Private Sub ButtonClicks(sender As Object, e As EventArgs)
        Dim temp As Button = CType(sender, Button)
        Select Case temp.TabIndex
            Case 0
                If targeting Then
                    Target = 0
                    If Magic Then
                        DialogResult = Windows.Forms.DialogResult.OK
                    Else
                        DialogResult = Windows.Forms.DialogResult.Yes
                    End If
                    endit()
                ElseIf Magic Then
                    ability = 0
                    dotarget(main.Spelltemplate(main.Party(player).spellList(0)).HealSpell)
                    targeting = True
                Else
                    dotarget(False)
                    targeting = True

                End If
            Case 1
                If targeting Then
                    Target = 1
                    If Magic Then
                        DialogResult = Windows.Forms.DialogResult.OK
                    Else
                        DialogResult = Windows.Forms.DialogResult.Yes
                    End If
                    endit()
                ElseIf Magic Then
                    ability = 1
                    dotarget(main.Spelltemplate(main.Party(player).spellList(0)).HealSpell)
                    targeting = True
                Else
                    Magic = True
                    magicstuff()
                End If
            Case 2
                If targeting Then
                    Target = 2
                    If Magic Then
                        DialogResult = Windows.Forms.DialogResult.OK
                    Else
                        DialogResult = Windows.Forms.DialogResult.Yes
                    End If
                    endit()
                ElseIf Magic Then
                    ability = 2
                    dotarget(main.Spelltemplate(main.Party(player).spellList(0)).HealSpell)
                    targeting = True
                Else
                    main.InvenScreen = New InventoryScreen
                    main.InvenScreen.Battle(player)
                    _invenResult = main.InvenScreen.ShowDialog()
                    If main.InvenScreen.battleuse Then
                        main.InvenScreen.battleuse = False
                        If _invenResult = Windows.Forms.DialogResult.OK Then
                            DialogResult = Windows.Forms.DialogResult.Abort
                            endit()
                        Else
                            RefreshMenu()
                        End If
                    End If
                    'MsgBox("currently not enabled")
                End If
            Case 3
                If targeting Then
                    Target = 3
                    If Magic Then
                        DialogResult = Windows.Forms.DialogResult.OK
                    Else
                        DialogResult = Windows.Forms.DialogResult.Yes
                    End If
                    endit()
                ElseIf Magic Then
                    ability = 3
                    dotarget(main.Spelltemplate(main.Party(player).spellList(0)).HealSpell)
                    targeting = True
                Else
                    DialogResult = Windows.Forms.DialogResult.Retry
                    endit()
                End If
            Case 4
                If targeting Then
                    Target = 4
                    If Magic Then
                        DialogResult = Windows.Forms.DialogResult.OK
                    Else
                        DialogResult = Windows.Forms.DialogResult.Yes
                    End If
                    endit()
                ElseIf Magic Then
                    ability = 4
                    dotarget(main.Spelltemplate(main.Party(player).spellList(0)).HealSpell)
                    targeting = True
                Else
                    main.EnemyInfo = New mobStats
                    main.EnemyInfo.ShowDialog()
                    DialogResult = Windows.Forms.DialogResult.Abort
                    endit()
                End If
            Case 5
                If targeting Then
                    Target = 5
                    If Magic Then
                        DialogResult = Windows.Forms.DialogResult.OK
                    Else
                        DialogResult = Windows.Forms.DialogResult.Yes
                    End If
                    endit()
                End If
            Case 6
                If targeting Then
                    Target = 6
                    If Magic Then
                        DialogResult = Windows.Forms.DialogResult.OK
                    Else
                        DialogResult = Windows.Forms.DialogResult.Yes
                    End If
                    endit()
                End If
            Case 7
                If targeting Then
                    Target = 7
                    If Magic Then
                        DialogResult = Windows.Forms.DialogResult.OK
                    Else
                        DialogResult = Windows.Forms.DialogResult.Yes
                    End If
                    endit()
                End If
            Case 8
                If targeting Then
                    If Magic Then
                        magicstuff()
                        targeting = False
                    Else
                        RefreshMenu()
                        targeting = False
                    End If
                ElseIf Magic Then
                    RefreshMenu()
                    Magic = False
                Else
                    DialogResult = Windows.Forms.DialogResult.Abort
                    endit()
                End If
        End Select
    End Sub

 
    Private Sub endit()
        Magic = False
        attack = False
        targeting = False
        Me.Close()
    End Sub
    Public Sub Setname(ByVal argname As String)
        lblTitle.Text = argname
    End Sub
    Public Sub RefreshMenu()
        btn(0).Visible = True
        btn(0).Text = "Attack"
        btn(1).Visible = True
        btn(1).Text = "Magic"
        btn(2).Visible = True
        btn(2).Text = "Item"
        btn(3).Visible = True
        btn(3).Text = "Run"
        btn(4).Visible = True
        btn(4).Text = "Scan"
        btn(5).Visible = False
        btn(5).Text = ""
        btn(6).Visible = False
        btn(6).Text = ""
        btn(7).Visible = False
        btn(7).Text = ""
        btn(8).Text = "Do Nothing"
    End Sub
    Private Sub dotarget(type As Boolean)
        If type Then
            If Not main.Party(0).dead Then
                btn(0).Visible = True
                btn(0).Text = main.Party(0).charname
            Else
                btn(0).Visible = False
            End If
            If Not main.Party(1).dead Then
                btn(1).Visible = True
                btn(1).Text = main.Party(1).charname
            Else
                btn(1).Visible = False
            End If
            If Not main.Party(2).dead Then
                btn(2).Visible = True
                btn(2).Text = main.Party(2).charname
            Else
                btn(2).Visible = False
            End If
            If Not main.Party(3).dead Then
                btn(3).Visible = True
                btn(3).Text = main.Party(3).charname
            Else
                btn(3).Visible = False
            End If
        Else

            If Not main.monsterParty(0).dead Then
                btn(0).Visible = True
                btn(0).Text = main.monsterParty(0).charname
            Else
                btn(0).Visible = False
            End If
            If Not main.monsterParty(1).dead Then
                btn(1).Visible = True
                btn(1).Text = main.monsterParty(1).charname
            Else
                btn(1).Visible = False
            End If
            If Not main.monsterParty(2).dead Then
                btn(2).Visible = True
                btn(2).Text = main.monsterParty(2).charname
            Else
                btn(2).Visible = False
            End If
            If Not main.monsterParty(3).dead Then
                btn(3).Visible = True
                btn(3).Text = main.monsterParty(3).charname
            Else
                btn(3).Visible = False
            End If
        End If
        btn(8).Text = "Cancel"

    End Sub
    Private Sub magicstuff()
        For i As Integer = 0 To 7 Step 1
            btn(i).Visible = False
        Next

        For i As Integer = 0 To 4 Step 1
            If (main.Party(player).spellList.GetUpperBound(0) + 1) >= (i + 1) Then
                btn(i).Visible = True
                btn(i).Text = main.Spelltemplate(main.Party(player).spellList(i)).name
            End If
        Next

        'If (main.Party(player).spellList.GetUpperBound(0) + 1) >= 1 Then
        '    If Not main.Party(player).spellList(0) = 0 Then
        '        btn(0).Visible = True
        '        btn(0).Text = main.Spelltemplate(main.Party(player).spellList(0)).name
        '    End If
        'End If
        'If (main.Party(player).spellList.GetUpperBound(0) + 1) >= 2 Then
        '    btn(1).Visible = True
        '    btn(1).Text = main.Spelltemplate(main.Party(player).spellList(1)).name
        'End If
        'If (main.Party(player).spellList.GetUpperBound(0) + 1) >= 3 Then
        '    btn(2).Visible = True
        '    btn(2).Text = main.Spelltemplate(main.Party(player).spellList(2)).name
        'End If
        'If (main.Party(player).spellList.GetUpperBound(0) + 1) >= 4 Then
        '    btn(3).Visible = True
        '    btn(3).Text = main.Spelltemplate(main.Party(player).spellList(3)).name
        'End If
        'If (main.Party(player).spellList.GetUpperBound(0) + 1) >= 5 Then
        '    btn(4).Visible = True
        '    btn(4).Text = main.Spelltemplate(main.Party(player).spellList(4)).name
        'End If
        btn(8).Text = "Cancel"
    End Sub

    Private Sub ActionMenu_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        For i As Integer = 0 To 8 Step 1
            btn(i).Dispose()
        Next
    End Sub

    Private Sub ActionMenu_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then
            main.menuEsc = New escMenu
            Dim temp As DialogResult = main.menuEsc.DialogResult
            If temp = Windows.Forms.DialogResult.Abort OrElse main.menuEsc.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                Me.Close()
            End If

        End If
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
            btn(i).Visible = True
            Me.Controls.Add(btn(i))

            AddHandler btn(i).Click, AddressOf ButtonClicks
            AddHandler btn(i).KeyPress, AddressOf ButtonKey
        Next
        lblTitle.Name = "lblTitle"
        lblTitle.TabIndex = 9
        lblTitle.Location = New Point(8, 2)
        lblTitle.Font = New Font("Microsoft Sans Serif", 8.25)
        lblTitle.Visible = True
        Me.Controls.Add(lblTitle)
        Me.KeyPreview = True
        RefreshMenu()
    End Sub
    Public Sub ButtonKey(sender As Object, e As KeyPressEventArgs)
        Dim temp As Button = CType(sender, Button)
        If e.KeyChar = "w" Then
            SendKeys.Send("{UP}")
        ElseIf e.KeyChar = "s" Then
            SendKeys.Send("{DOWN}")
        End If
    End Sub
End Class
