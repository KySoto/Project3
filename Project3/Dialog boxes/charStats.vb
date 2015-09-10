Public Class charStats
    Public ShownChar As Integer = 0

    Private Sub charStats_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        main.PauseAll = False
        main.KeyPreview = True
    End Sub
    Private Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        Me.Close()
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If ShownChar = 3 Then
            ShownChar = 0
        Else
            ShownChar += 1
        End If
        UpdateStats()
        If main.Party(ShownChar).dead Then
            btnSetWMChar.Enabled = False
        End If
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If ShownChar = 0 Then
            ShownChar = 3
        Else
            ShownChar -= 1
        End If
        UpdateStats()
        If main.Party(ShownChar).dead Then
            btnSetWMChar.Enabled = False
        End If
    End Sub
    Public Sub UpdateStats()
        picPortrate.Image = main.Party(ShownChar).Portrate

        lblArmor.Text = "Armor: " & main.EquipsTemplate(main.Party(ShownChar).armorindex).name & " Defense " & main.EquipsTemplate(main.Party(ShownChar).armorindex).defense & " Resist " & main.EquipsTemplate(main.Party(ShownChar).armorindex).resist & ControlChars.NewLine & " Speed " & main.EquipsTemplate(main.Party(ShownChar).armorindex).speed

        lblAttack.Text = "Attack: " & main.Party(ShownChar).atk & "-" & (main.Party(ShownChar).atkrange + main.Party(ShownChar).atk) & "(" & (main.EquipsTemplate(main.Party(ShownChar).weaponindex).atk + main.EquipsTemplate(main.Party(ShownChar).offhandindex).atk) & "-" & (main.EquipsTemplate(main.Party(ShownChar).weaponindex).atk + main.EquipsTemplate(main.Party(ShownChar).offhandindex).atk + main.EquipsTemplate(main.Party(ShownChar).weaponindex).extraatkrange + main.EquipsTemplate(main.Party(ShownChar).offhandindex).extraatkrange) & ")"
        lblClass.Text = main.Party(ShownChar).ClassName & " " & main.Party(ShownChar).currentlevel
        lblDefense.Text = "Defense: " & main.Party(ShownChar).defense & "(" & (main.EquipsTemplate(main.Party(ShownChar).armorindex).defense + main.EquipsTemplate(main.Party(ShownChar).weaponindex).defense + main.EquipsTemplate(main.Party(ShownChar).offhandindex).defense) & ")"
        lblExpC.Text = "Current Exp: " & main.Party(ShownChar).xp.ToString("F0")
        If main.Party(ShownChar).currentlevel = 100 Then
            lblExpN.Text = "Next Level: N/A"
        Else
            lblExpN.Text = "Next Level: " & main.Party(ShownChar).NextLevel
        End If

        lblHP.Text = "HP " & main.Party(ShownChar).maxhp & "\" & main.Party(ShownChar).hp
        lblMagic.Text = "Magic: " & main.Party(ShownChar).magic & "(" & (main.EquipsTemplate(main.Party(ShownChar).weaponindex).magic + main.EquipsTemplate(main.Party(ShownChar).offhandindex).magic) & ")"

        lblMhand.Text = "MainHand: " & main.EquipsTemplate(main.Party(ShownChar).weaponindex).name & " Attack " & main.EquipsTemplate(main.Party(ShownChar).weaponindex).atk & "-" & (main.EquipsTemplate(main.Party(ShownChar).weaponindex).atk + main.EquipsTemplate(main.Party(ShownChar).weaponindex).extraatkrange) & " Magic " & main.EquipsTemplate(main.Party(ShownChar).weaponindex).magic & ControlChars.NewLine & " Defense " & main.EquipsTemplate(main.Party(ShownChar).weaponindex).defense & " Resist " & main.EquipsTemplate(main.Party(ShownChar).weaponindex).resist & " Speed " & main.EquipsTemplate(main.Party(ShownChar).weaponindex).speed

        lblOhand.Text = "OffHand: " & main.EquipsTemplate(main.Party(ShownChar).offhandindex).name & " Attack " & main.EquipsTemplate(main.Party(ShownChar).offhandindex).atk & "-" & (main.EquipsTemplate(main.Party(ShownChar).offhandindex).atk + main.EquipsTemplate(main.Party(ShownChar).offhandindex).extraatkrange) & " Magic " & main.EquipsTemplate(main.Party(ShownChar).offhandindex).magic & ControlChars.NewLine & " Defense " & main.EquipsTemplate(main.Party(ShownChar).offhandindex).defense & " Resist " & main.EquipsTemplate(main.Party(ShownChar).offhandindex).resist & " Speed " & main.EquipsTemplate(main.Party(ShownChar).offhandindex).speed


        lblMP.Text = "MP " & main.Party(ShownChar).maxmp & "\" & main.Party(ShownChar).mp
        lblName.Text = main.Party(ShownChar).charname

        lblResist.Text = "Resist: " & main.Party(ShownChar).resist & "(" & (main.EquipsTemplate(main.Party(ShownChar).armorindex).resist + main.EquipsTemplate(main.Party(ShownChar).weaponindex).resist + main.EquipsTemplate(main.Party(ShownChar).offhandindex).resist) & ")"
        lblSpeed.Text = "Speed: " & main.Party(ShownChar).speed & "(" & (main.EquipsTemplate(main.Party(ShownChar).armorindex).speed + main.EquipsTemplate(main.Party(ShownChar).weaponindex).speed + main.EquipsTemplate(main.Party(ShownChar).offhandindex).speed) & ")"
    End Sub

    Private Sub charStats_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub charStats_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        UpdateStats()
        main.PauseAll = True
        main.KeyPreview = False
        Me.KeyPreview = True
        If main.Party(ShownChar).dead Then
            btnSetWMChar.Enabled = False
        End If
    End Sub

    Private Sub btnSetWMChar_Click(sender As Object, e As EventArgs) Handles btnSetWMChar.Click
        main.CurrentShownPartyMember = ShownChar
    End Sub
End Class