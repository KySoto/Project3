Public Class mobStats
    Public ShownChar As Integer = 0

    Private Sub charStats_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'main.PauseAll = False
        'main.KeyPreview = True
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
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If ShownChar = 0 Then
            ShownChar = 3
        Else
            ShownChar -= 1
        End If
        UpdateStats()
    End Sub
    Public Sub UpdateStats()
        picPortrate.Image = main.monsterParty(ShownChar).Portrate

        lblArmor.Text = "Armor: " & main.EquipsTemplate(main.monsterParty(ShownChar).armorindex).name & " Defense " & main.EquipsTemplate(main.monsterParty(ShownChar).armorindex).defense & " Resist " & main.EquipsTemplate(main.monsterParty(ShownChar).armorindex).resist & ControlChars.NewLine & " Speed " & main.EquipsTemplate(main.monsterParty(ShownChar).armorindex).speed

        lblAttack.Text = "Attack: " & main.monsterParty(ShownChar).atk & "-" & (main.monsterParty(ShownChar).atkrange + main.monsterParty(ShownChar).atk) & "(" & (main.EquipsTemplate(main.monsterParty(ShownChar).weaponindex).atk + main.EquipsTemplate(main.monsterParty(ShownChar).offhandindex).atk) & "-" & (main.EquipsTemplate(main.monsterParty(ShownChar).weaponindex).atk + main.EquipsTemplate(main.monsterParty(ShownChar).offhandindex).atk + main.EquipsTemplate(main.monsterParty(ShownChar).weaponindex).extraatkrange + main.EquipsTemplate(main.monsterParty(ShownChar).offhandindex).extraatkrange) & ")"
        lblClass.Text = main.monsterParty(ShownChar).ClassName & " " & main.monsterParty(ShownChar).currentlevel
        lblDefense.Text = "Defense: " & main.monsterParty(ShownChar).defense & "(" & (main.EquipsTemplate(main.monsterParty(ShownChar).armorindex).defense + main.EquipsTemplate(main.monsterParty(ShownChar).weaponindex).defense + main.EquipsTemplate(main.monsterParty(ShownChar).offhandindex).defense) & ")"
        lblExpC.Text = "Exp Value: " & main.monsterParty(ShownChar).xp.ToString("f0")

        lblExpN.Text = "Cash Carried: " & main.monsterParty(ShownChar).Money.ToString("f0")

        lblHP.Text = "HP " & main.monsterParty(ShownChar).maxhp & "\" & main.monsterParty(ShownChar).hp
        lblMagic.Text = "Magic: " & main.monsterParty(ShownChar).magic & "(" & (main.EquipsTemplate(main.monsterParty(ShownChar).weaponindex).magic + main.EquipsTemplate(main.monsterParty(ShownChar).offhandindex).magic) & ")"

        lblMhand.Text = "MainHand: " & main.EquipsTemplate(main.monsterParty(ShownChar).weaponindex).name & " Attack " & main.EquipsTemplate(main.monsterParty(ShownChar).weaponindex).atk & "-" & (main.EquipsTemplate(main.monsterParty(ShownChar).weaponindex).atk + main.EquipsTemplate(main.monsterParty(ShownChar).weaponindex).extraatkrange) & " Magic " & main.EquipsTemplate(main.monsterParty(ShownChar).weaponindex).magic & ControlChars.NewLine & " Defense " & main.EquipsTemplate(main.monsterParty(ShownChar).weaponindex).defense & " Resist " & main.EquipsTemplate(main.monsterParty(ShownChar).weaponindex).resist & " Speed " & main.EquipsTemplate(main.monsterParty(ShownChar).weaponindex).speed

        lblOhand.Text = "OffHand: " & main.EquipsTemplate(main.monsterParty(ShownChar).offhandindex).name & " Attack " & main.EquipsTemplate(main.monsterParty(ShownChar).offhandindex).atk & "-" & (main.EquipsTemplate(main.monsterParty(ShownChar).offhandindex).atk + main.EquipsTemplate(main.monsterParty(ShownChar).offhandindex).extraatkrange) & " Magic " & main.EquipsTemplate(main.monsterParty(ShownChar).offhandindex).magic & ControlChars.NewLine & " Defense " & main.EquipsTemplate(main.monsterParty(ShownChar).offhandindex).defense & " Resist " & main.EquipsTemplate(main.monsterParty(ShownChar).offhandindex).resist & " Speed " & main.EquipsTemplate(main.monsterParty(ShownChar).offhandindex).speed


        lblMP.Text = "MP " & main.monsterParty(ShownChar).maxmp & "\" & main.monsterParty(ShownChar).mp
        lblName.Text = main.monsterParty(ShownChar).charname

        lblResist.Text = "Resist: " & main.monsterParty(ShownChar).resist & "(" & (main.EquipsTemplate(main.monsterParty(ShownChar).armorindex).resist + main.EquipsTemplate(main.monsterParty(ShownChar).weaponindex).resist + main.EquipsTemplate(main.monsterParty(ShownChar).offhandindex).resist) & ")"
        lblSpeed.Text = "Speed: " & main.monsterParty(ShownChar).speed & "(" & (main.EquipsTemplate(main.monsterParty(ShownChar).armorindex).speed + main.EquipsTemplate(main.monsterParty(ShownChar).weaponindex).speed + main.EquipsTemplate(main.monsterParty(ShownChar).offhandindex).speed) & ")"
    End Sub

    Private Sub charStats_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        UpdateStats()
        'main.PauseAll = True
        'main.KeyPreview = False
    End Sub
End Class