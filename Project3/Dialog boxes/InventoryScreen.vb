Public Class InventoryScreen
    Private _LastSelectedCharacter As Integer = 0
    Private _battleplayer As Integer = 0
    Private _Sell As Boolean = False
    Private _battle As Boolean = False
    Private _battleUse As Boolean = False
    Private _lastSelectedIndex As Integer = 0
    Private _moving As Boolean = False
    Private _tomove As Integer = 0

    Private Sub updateCharStats()
        picPC.Image = main.Party(_LastSelectedCharacter).Portrate
        lblOutput.Text = main.Party(_LastSelectedCharacter).charname & ControlChars.NewLine & "HP: " & CStr(CInt(main.Party(_LastSelectedCharacter).hp)) & "/" & CStr(CInt(main.Party(_LastSelectedCharacter).maxhp)) & ControlChars.NewLine & "MP: " & CStr(CInt(main.Party(_LastSelectedCharacter).mp)) & "/" & CStr(CInt(main.Party(_LastSelectedCharacter).maxmp)) & ControlChars.NewLine & "Armor: " & main.EquipsTemplate(main.Party(_LastSelectedCharacter).armorindex).name & " Defense " & main.EquipsTemplate(main.Party(_LastSelectedCharacter).armorindex).defense & " Resist " & main.EquipsTemplate(main.Party(_LastSelectedCharacter).armorindex).resist & ControlChars.NewLine & " Speed " & main.EquipsTemplate(main.Party(_LastSelectedCharacter).armorindex).speed & ControlChars.NewLine & "MainHand: " & main.EquipsTemplate(main.Party(_LastSelectedCharacter).weaponindex).name & " Attack " & main.EquipsTemplate(main.Party(_LastSelectedCharacter).weaponindex).atk & "-" & (main.EquipsTemplate(main.Party(_LastSelectedCharacter).weaponindex).atk + main.EquipsTemplate(main.Party(_LastSelectedCharacter).weaponindex).extraatkrange) & " Magic " & main.EquipsTemplate(main.Party(_LastSelectedCharacter).weaponindex).magic & ControlChars.NewLine & " Defense " & main.EquipsTemplate(main.Party(_LastSelectedCharacter).weaponindex).defense & " Resist " & main.EquipsTemplate(main.Party(_LastSelectedCharacter).weaponindex).resist & " Speed " & main.EquipsTemplate(main.Party(_LastSelectedCharacter).weaponindex).speed & ControlChars.NewLine & "OffHand: " & main.EquipsTemplate(main.Party(_LastSelectedCharacter).offhandindex).name & " Attack " & main.EquipsTemplate(main.Party(_LastSelectedCharacter).offhandindex).atk & "-" & (main.EquipsTemplate(main.Party(_LastSelectedCharacter).offhandindex).atk + main.EquipsTemplate(main.Party(_LastSelectedCharacter).offhandindex).extraatkrange) & " Magic " & main.EquipsTemplate(main.Party(_LastSelectedCharacter).offhandindex).magic & ControlChars.NewLine & " Defense " & main.EquipsTemplate(main.Party(_LastSelectedCharacter).offhandindex).defense & " Resist " & main.EquipsTemplate(main.Party(_LastSelectedCharacter).offhandindex).resist & " Speed " & main.EquipsTemplate(main.Party(_LastSelectedCharacter).offhandindex).speed
    End Sub

    Public Property battleuse As Boolean
        Get
            Return _battleUse
        End Get
        Set(value As Boolean)
            _battleUse = value
        End Set
    End Property
    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If _LastSelectedCharacter >= 3 Then
            _LastSelectedCharacter = 0
        Else
            _LastSelectedCharacter += 1
        End If
        If _battle Then
            If _LastSelectedCharacter <> _battleplayer Then
                btnEquip.Enabled = False
            Else
                If main.Inventory(lstInventory.SelectedIndex).consumable Then
                    btnEquip.Enabled = True
                End If
            End If
        End If
        updateCharStats()
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If _LastSelectedCharacter <= 0 Then
            _LastSelectedCharacter = 3
        Else
            _LastSelectedCharacter -= 1
        End If
        If _battle Then
            If _LastSelectedCharacter <> _battleplayer Then
                btnEquip.Enabled = False
            Else
                If main.Inventory(lstInventory.SelectedIndex).consumable Then
                    btnEquip.Enabled = True
                End If
            End If
        End If
        updateCharStats()
    End Sub

    Private Sub InventoryScreen_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If Not _battle Then
            main.KeyPreview = True
            main.PauseAll = False
        End If
    End Sub

    Private Sub InventoryScreen_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        updateCharStats()
        lblGold.Text = main.TotalMoney.ToString("C2")
        updateInventory()
        lstInventory.SelectedIndex = 0
        updateButtons()
        If Not _battle Then
            main.KeyPreview = False
            main.PauseAll = True
        End If

    End Sub
    Private Sub updateInventory()
        _lastSelectedIndex = lstInventory.SelectedIndex
        'lstInventory.Items.Clear()
        For i As Integer = 0 To main.Inventory.GetUpperBound(0) Step 1
            ' If main.Inventory(i).stack = 1 Then
            'lstInventory.Items.Add(main.Inventory(i).name)
            lstInventory.Items.Insert(i, main.Inventory(i))
            'Else
            'lstInventory.Items.Add(main.Inventory(i).name & " " & main.Inventory(i).stack.ToString)
            ' End If
        Next
        lstInventory.SelectedIndex = _lastSelectedIndex
    End Sub
    Private Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        If _battle Then
            DialogResult = Windows.Forms.DialogResult.Cancel
        End If
        Me.Close()
    End Sub

    Private Sub updateButtons()

        If main.Inventory(lstInventory.SelectedIndex).consumable Then

            btnEquip.Enabled = False
            btnUse.Enabled = True
        ElseIf Not main.Inventory(lstInventory.SelectedIndex).index = 0 Then
            btnEquip.Enabled = True
            btnUse.Enabled = False
        Else
            btnEquip.Enabled = False
            btnUse.Enabled = False
        End If
        If _battle Then
            If _LastSelectedCharacter <> _battleplayer Then
                btnEquip.Enabled = False
            End If
        End If
    End Sub
    Public Sub Battle(ByVal argPlayer As Integer)
        _battle = True
        _battleplayer = argPlayer
        _LastSelectedCharacter = argPlayer
    End Sub
    Public Sub EndBattle()
        _battle = False
        _battleUse = False
        btnNext.Enabled = True
        btnPrevious.Enabled = True
    End Sub
    Public Property Sell As Boolean
        Get
            Return _Sell
        End Get
        Set(value As Boolean)
            If value Then
                btnEquip.Text = "Sell"
                btnUse.Visible = False
                picPC.Visible = False
                btnNext.Visible = False
                btnPrevious.Visible = False
                lblOutput.Visible = False
                cmbQuantity.Visible = True
                Me.Size = New Size(640, 376)
                lstInventory.Location = New Point(12, 13)
                btnEquip.Location = New Point(493, 13)
                btnQuit.Location = New Point(493, 284)
                cmbQuantity.Location = New Point(493, 257)
                lblGold.Location = New Point(13, 314)
                Me.Text = "Sell"
            Else
                btnEquip.Text = "Equip"
                btnUse.Visible = True
                picPC.Visible = True
                btnNext.Visible = True
                btnPrevious.Visible = True
                lblOutput.Visible = True
                cmbQuantity.Visible = False
                Me.Size = New Size(640, 501)
                lstInventory.Location = New Point(12, 142)
                btnEquip.Location = New Point(493, 142)
                btnQuit.Location = New Point(493, 409)
                cmbQuantity.Location = New Point(493, 382)
                lblGold.Location = New Point(13, 439)
                Me.Text = "InventoryScreen"
            End If
            _Sell = value
        End Set
    End Property
    Private Sub quantity(ByVal argQuantity As Integer)
        cmbQuantity.Items.Clear()
        If argQuantity <= 1 Then
            cmbQuantity.Items.Add(1)
        Else
            For i As Integer = 1 To argQuantity Step 1
                cmbQuantity.Items.Add(i)
            Next
        End If
        cmbQuantity.SelectedIndex = 0
    End Sub
    Private Sub btnEquip_Click(sender As Object, e As EventArgs) Handles btnEquip.Click
        If Sell Then
            If main.InsertItemInToInventory(main.Inventory(lstInventory.SelectedIndex).index, CInt(cmbQuantity.SelectedItem) * -1) = 0 Then
                main.TotalMoney += (CInt(cmbQuantity.SelectedItem) * main.Inventory(lstInventory.SelectedIndex).cost * 0.25)
            End If
        Else
            Dim temp As Integer = main.Inventory(lstInventory.SelectedIndex).index
            If main.Party(_LastSelectedCharacter).EquipItem(temp) Then
                main.InsertItemInToInventory(temp, -1)
            End If

        End If
        updateCharStats()
        updateInventory()
        lblGold.Text = main.TotalMoney.ToString("C2")
        If _battle Then
            _battleUse = True
            Me.Close()
        End If
    End Sub

    Private Sub btnUse_Click(sender As Object, e As EventArgs) Handles btnUse.Click
        Dim temp As Integer = main.Inventory(lstInventory.SelectedIndex).index
        If main.ConsumeItem(_LastSelectedCharacter, temp) Then
            main.InsertItemInToInventory(temp, -1)
        End If

        updateCharStats()
        updateInventory()
        If _battle Then
            _battleUse = True
            DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub lstInventory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstInventory.SelectedIndexChanged
        If _moving Then

        Else
            If Sell Then
                quantity(main.Inventory(lstInventory.SelectedIndex).stack)
            Else
                updateButtons()
            End If
        End If
    End Sub

    Private Sub InventoryScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnMove_Click(sender As Object, e As EventArgs) Handles btnMove.Click
        If _moving Then
            btnMove.Text = "Cancel"
            btnEquip.Enabled = False
            btnUse.Enabled = False
            _tomove = lstInventory.SelectedIndex
        Else
            btnMove.Text = "Move"
            updateButtons()
        End If




    End Sub

    Private Sub InventoryScreen_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        _moving = False
    End Sub

    Private Sub InventoryScreen_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyValue = Keys.Escape Then
            If _battle Then
                DialogResult = Windows.Forms.DialogResult.Cancel
            End If
            Me.Close()
        End If
    End Sub
End Class