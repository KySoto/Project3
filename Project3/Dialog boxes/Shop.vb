Public Class Shop
    Public Property Shopkeeper As Integer
    Private Sub btnBuy_Click(sender As Object, e As EventArgs) Handles btnBuy.Click
        If Not main.NPCTemplate(Shopkeeper).Buy(main.TotalMoney, lstinventory.SelectedIndex, CInt(cmbQuantity.SelectedItem)) Then
            MsgBox("Not Enough Money")
        End If
        lblCash.Text = main.TotalMoney.ToString("C2")
    End Sub

    Private Sub lstinventory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstinventory.SelectedIndexChanged
        PopulateQuantity()
    End Sub

    Private Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        Me.Close()
    End Sub
    Private Sub PopulateQuantity()
        If main.EquipsTemplate(main.NPCTemplate(Shopkeeper).Inventory(lstinventory.SelectedIndex)).consumable Then
            cmbQuantity.Enabled = True
        Else
            cmbQuantity.SelectedIndex = 0
            cmbQuantity.Enabled = False
        End If
    End Sub
    Private Sub populateInventory()
        lstinventory.Items.Clear()
        For i As Integer = 0 To main.NPCTemplate(Shopkeeper).Inventory.GetUpperBound(0) Step 1
            If main.EquipsTemplate(main.NPCTemplate(Shopkeeper).Inventory(i)).consumable Then
                lstinventory.Items.Add(main.EquipsTemplate(main.NPCTemplate(Shopkeeper).Inventory(i)).name & " Type: Consumable Price: " & main.EquipsTemplate(main.NPCTemplate(Shopkeeper).Inventory(i)).cost.ToString("C2"))
            Else
                If main.EquipsTemplate(main.NPCTemplate(Shopkeeper).Inventory(i)).slot = 1 Then
                    lstinventory.Items.Add(main.EquipsTemplate(main.NPCTemplate(Shopkeeper).Inventory(i)).name & " Type: Mainhand Price: " & main.EquipsTemplate(main.NPCTemplate(Shopkeeper).Inventory(i)).cost.ToString("C2"))
                ElseIf main.EquipsTemplate(main.NPCTemplate(Shopkeeper).Inventory(i)).slot = 2 Then
                    lstinventory.Items.Add(main.EquipsTemplate(main.NPCTemplate(Shopkeeper).Inventory(i)).name & " Type: Offhand Price: " & main.EquipsTemplate(main.NPCTemplate(Shopkeeper).Inventory(i)).cost.ToString("C2"))
                ElseIf main.EquipsTemplate(main.NPCTemplate(Shopkeeper).Inventory(i)).slot = 3 Then
                    lstinventory.Items.Add(main.EquipsTemplate(main.NPCTemplate(Shopkeeper).Inventory(i)).name & " Type: Armor Price: " & main.EquipsTemplate(main.NPCTemplate(Shopkeeper).Inventory(i)).cost.ToString("C2"))
                Else
                    lstinventory.Items.Add(main.EquipsTemplate(main.NPCTemplate(Shopkeeper).Inventory(i)).name & " Type: unknown Price: " & main.EquipsTemplate(main.NPCTemplate(Shopkeeper).Inventory(i)).cost.ToString("C2"))
                End If
            End If

        Next
        lstinventory.SelectedIndex = 0
        cmbQuantity.SelectedIndex = 0
    End Sub
    Private Sub Shop_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For i As Integer = 1 To 99 Step 1
            cmbQuantity.Items.Add(i)
        Next
    End Sub

    Private Sub Shop_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        populateInventory()
        lblCash.Text = main.TotalMoney.ToString("C2")
    End Sub
End Class