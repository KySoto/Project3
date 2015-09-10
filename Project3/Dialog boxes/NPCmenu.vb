Option Explicit On
Option Infer Off
Option Strict On
Imports System.Windows.Forms
Imports Project3.main

Public Class NPCmenu
    Public NPCIndex As Integer = 0
    Private loaded As Boolean = False
    Private btn(4) As Button
    Public lblTitle As Label = New Label
    Public lblMoney As Label = New Label
    Public gold As Double = main.TotalMoney
    Private _IsMerchent As Boolean
    Private _IsInnKeeper As Boolean
    Private _MerchName As String
    Private Sub ButtonClicks(sender As Object, e As EventArgs)
        Dim temp As Button = CType(sender, Button)
        Select Case temp.TabIndex
            Case 0
                MsgBox(main.NPCTemplate(NPCIndex).Talk(), MsgBoxStyle.OkOnly, main.NPCTemplate(NPCIndex).name)
            Case 1
                If main.NPCTemplate(NPCIndex).RestPrice > 0 Then
                    If MsgBox("It costs " & main.NPCTemplate(NPCIndex).RestPrice.ToString("C2") & " to rest here", MsgBoxStyle.YesNo, "Rest up") = MsgBoxResult.Yes Then
                        main.NPCTemplate(NPCIndex).Rest(main.TotalMoney)
                        updatemoney()
                    End If
                Else
                    main.NPCTemplate(NPCIndex).Rest(main.TotalMoney)
                End If
            Case 2
                If main.NPCTemplate(NPCIndex).IsMerchent Then 'this check is now pointless!
                    main.NPCshop = New Shop
                    main.NPCshop.Shopkeeper = NPCIndex
                    main.NPCshop.Show()
                Else
                    MsgBox("I cant Sell you anything, im not a merchent.")
                End If
            Case 3
                If main.NPCTemplate(NPCIndex).IsMerchent Then
                    main.InvenScreen = New InventoryScreen
                    main.InvenScreen.Sell = True
                    main.InvenScreen.Show()
                Else
                    MsgBox("I cant buy your old stuff, im not a merchent.")
                End If
            Case 4

                Me.Close()
        End Select
    End Sub
    Public Sub ButtonKey(sender As Object, e As KeyPressEventArgs)
        Dim temp As Button = CType(sender, Button)
        If e.KeyChar = "w" Then
            SendKeys.Send("{UP}")
        ElseIf e.KeyChar = "s" Then
            SendKeys.Send("{DOWN}")
        ElseIf e.KeyChar = Chr(Keys.Escape) Then
            Me.Close()
        End If
    End Sub

    'Private Sub btn1_Click(sender As Object, e As EventArgs) Handles btn1.Click
    '    MsgBox(main.NPCTemplate(NPCIndex).Talk(), MsgBoxStyle.OkOnly, main.NPCTemplate(NPCIndex).name)

    'End Sub

    'Private Sub btn2_Click(sender As Object, e As EventArgs) Handles btn2.Click
    '    If main.NPCTemplate(NPCIndex).RestPrice > 0 Then
    '        If MsgBox("It costs " & main.NPCTemplate(NPCIndex).RestPrice.ToString("C2") & " to rest here", MsgBoxStyle.YesNo, "Rest up") = MsgBoxResult.Yes Then
    '            main.NPCTemplate(NPCIndex).Rest(main.TotalMoney)
    '            updatemoney()
    '        End If
    '    Else
    '        main.NPCTemplate(NPCIndex).Rest(main.TotalMoney)
    '    End If
    'End Sub


    'Private Sub btn3_Click(sender As Object, e As EventArgs) Handles btn3.Click
    '    If main.NPCTemplate(NPCIndex).IsMerchent Then 'this check is now pointless!
    '        main.NPCshop = New Shop
    '        main.NPCshop.Shopkeeper = NPCIndex
    '        main.NPCshop.Show()
    '    Else
    '        MsgBox("I cant Sell you anything, im not a merchent.")
    '    End If
    '    'MsgBox("this button doesnt work yet.")
    'End Sub

    'Private Sub btn4_Click(sender As Object, e As EventArgs) Handles btn4.Click
    '    If main.NPCTemplate(NPCIndex).IsMerchent Then
    '        main.InvenScreen = New InventoryScreen
    '        main.InvenScreen.Sell = True
    '        main.InvenScreen.Show()
    '    Else
    '        MsgBox("I cant buy your old stuff, im not a merchent.")
    '    End If
    'End Sub

    'Private Sub btn5_Click(sender As Object, e As EventArgs) Handles btn5.Click
    '    Me.Close()

    'End Sub
    Public Property MerchName As String
        Get
            Return _MerchName
        End Get
        Set(value As String)
            _MerchName = value
        End Set
    End Property
    Public Property IsMerchent As Boolean
        Get
            Return _IsMerchent
        End Get
        Set(value As Boolean)
            If loaded Then
                If value Then
                    lblMoney.Visible = True
                    updatemoney()
                    btn(2).Enabled = True
                    btn(3).Enabled = True
                Else
                    lblMoney.Visible = False
                    btn(2).Enabled = False
                    btn(3).Enabled = False
                End If
            End If
            _IsMerchent = value
        End Set
    End Property
    Public Property IsInnKeeper As Boolean
        Get
            Return _IsInnKeeper
        End Get
        Set(value As Boolean)
            If loaded Then
                If value = True Then
                    lblMoney.Visible = True
                    updatemoney()
                    btn(1).Enabled = True
                Else
                    btn(1).Enabled = False
                    lblMoney.Visible = False
                End If
            End If
            _IsInnKeeper = value
        End Set

    End Property
    Public Sub updatemoney()
        lblMoney.Text = main.TotalMoney.ToString("C2")
        lblMoney.Text = "fuck you asshole"
    End Sub

    Private Sub NPCmenu_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        For i As Integer = 0 To 4 Step 1
            btn(i).Dispose()
        Next
    End Sub
    Private Sub ActionMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Size = New Size(100, 151)
        Me.Location = New Point(main.Location.X + 9, main.Location.Y + 577)
        Me.BackgroundImage = Image.FromFile("art\ActionMenu.png")
        For i As Integer = 0 To 4 Step 1
            btn(i) = New Button
            btn(i).Name = "btn" & i.ToString
            btn(i).Location = New Point(8, 18 + (i * 22))
            btn(i).Size = New Size(84, 20)
            btn(i).Font = New Font("Microsoft Sans Serif", 8.25)
            btn(i).BackColor = SystemColors.Control
            btn(i).TabIndex = i
            btn(i).TabStop = True
            btn(i).Text = ""
            Select Case i
                Case 0
                    btn(i).Text = "Talk"
                Case 1
                    btn(i).Text = "Rest"
                Case 2
                    btn(i).Text = "Buy"
                Case 3
                    btn(i).Text = "Sell"
                Case 4
                    btn(i).Text = "Cancel"
            End Select
            btn(i).Visible = True
            Me.Controls.Add(btn(i))

            AddHandler btn(i).Click, AddressOf ButtonClicks
            AddHandler btn(i).KeyPress, AddressOf ButtonKey
        Next
        If IsInnKeeper Then
            btn(1).Enabled = True
        Else
            btn(1).Enabled = False
        End If
        If IsMerchent Then
            btn(2).Enabled = True
            btn(3).Enabled = True
        Else
            btn(2).Enabled = False
            btn(3).Enabled = False
        End If
        ' lblTitle = New Label
        lblTitle.Text = MerchName
        lblTitle.Name = "lblTitle"
        lblTitle.TabIndex = 5
        lblTitle.Location = New Point(8, 2)
        lblTitle.Font = New Font("Microsoft Sans Serif", 8.25)
        lblTitle.Visible = True
        '  lblMoney = New Label
        'lblMoney.Text = ""
        lblMoney.Name = "lblMoney"
        lblMoney.TabIndex = 6
        lblMoney.Location = New Point(12, 129)
        lblMoney.Font = New Font("Microsoft Sans Serif", 8.25)
        lblMoney.Visible = True
        If IsInnKeeper OrElse IsMerchent Then


            updatemoney()
        End If
    End Sub

End Class
