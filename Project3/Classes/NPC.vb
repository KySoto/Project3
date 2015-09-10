Public Class NPC
    Private _name As String
    Private _dialogue(0) As String
    Private _IsShopkeeper As Boolean
    Private _IsInnkeeper As Boolean
    Private _lastDialogueIndex As Integer = -1
    Private _inventory(0) As Integer
    Private _RestPrice As Integer = 0
    Private _artDir As String
    Private _anim(7) As Image


    Public Sub New(ByVal argName As String, ByVal argDialogue As String, ByVal argIsShopkeeper As Boolean, ByVal argIsInnkeeper As Boolean, ByVal argInventory As String, ByVal argArtFolder As String)
        _name = argName
        _artDir = argArtFolder
        For i As Integer = 0 To 7 Step 1
            If IO.File.Exists(_artDir & "\" & i & ".png") Then
                _anim(i) = Image.FromFile(_artDir & "\" & i & ".png") 'arganim is the string from the DB pointing to the correct folder
            Else
                _anim(i) = Image.FromFile("art\CharacterPlaceholder.png")
            End If
        Next
        _IsShopkeeper = argIsShopkeeper
        _IsInnkeeper = argIsInnkeeper
        Dim counter As Integer = 0
        For i As Integer = 0 To argDialogue.Length - 1 Step 1 'bomb ass method of insterting the dialogue line 14 to line 38
            If argDialogue(i) = ";" Then
                counter += 1
            End If
        Next
        If counter = 0 Then
            ReDim _dialogue(0)
        Else
            ReDim _dialogue(counter)
        End If
        Dim startIndex As Integer = 0
        Dim Length As Integer = 1
        counter = 0
        For i As Integer = 0 To argDialogue.Length - 1 Step 1
            If argDialogue(i) = ";" Then
                Length = i - startIndex
                _dialogue(counter) = argDialogue.Substring(startIndex, Length)
                counter += 1
                startIndex = i + 1
            End If
            If i = argDialogue.Length - 1 Then
                _dialogue(counter) = argDialogue.Substring(startIndex)
            End If
        Next
        If argIsShopkeeper Then 'parses the inventory
            ReDim _inventory(CInt((argInventory.Length / 3) - 1))
            For i As Integer = 0 To CInt((argInventory.Length / 3) - 1) Step 1
                ' If i < CInt((argInventory.Length / 3) - 1) Then
                _inventory(i) = CInt(argInventory.Substring((i * 3), 3))
                'End If
            Next
        Else
            _inventory(0) = -1
        End If
        If argIsInnkeeper Then
            _RestPrice = 10
        End If
    End Sub
    Public Function getimage(ByVal argDirection As Integer, ByVal argPrevious As Integer) As Image '(0 N) (1 W ) (2 E) (3 S) rofl poached from the character class
        If argDirection >= 0 Or argDirection <= 3 Then
            If argPrevious = 0 Then
                Return _anim(2 * argDirection)
            ElseIf argPrevious = 1 Then
                Return _anim((2 * argDirection) + 1)
            Else
                MsgBox("uh oh things broke in getimage argDirection")
                Return _anim(0)
            End If
        Else
            MsgBox("uh oh things broke in getimage argPrevious")
            If Not argPrevious = 0 And Not argPrevious = 1 Then
                MsgBox("uh oh things broke in getimage argDirection")
            End If
            Return _anim(0)
        End If
    End Function
    Public ReadOnly Property IsMerchent As Boolean
        Get
            Return _IsShopkeeper
        End Get
    End Property
    Public ReadOnly Property IsInnKeeper As Boolean
        Get
            Return _IsInnkeeper
        End Get
    End Property
    Public ReadOnly Property Inventory As Integer()
        Get
            Return _inventory
        End Get
    End Property
    Public Function Buy(ByRef argMoney As Double, ByVal argShopKeeperInventoryIndex As Integer, ByVal argStackSize As Integer) As Boolean
        If _IsShopkeeper Then
            If argShopKeeperInventoryIndex <= _inventory.GetUpperBound(0) AndAlso argShopKeeperInventoryIndex >= 0 Then
                If argMoney >= main.EquipsTemplate(_inventory(argShopKeeperInventoryIndex)).cost Then
                    Dim Result As Integer = main.InsertItemInToInventory(_inventory(argShopKeeperInventoryIndex), argStackSize)
                    If Result = 0 Then
                        argMoney -= main.EquipsTemplate(_inventory(argShopKeeperInventoryIndex)).cost
                        main.TalkMenu.updatemoney()
                        Return True
                    ElseIf Result = 1 Then
                        MsgBox("No room in your inventory.")
                    ElseIf Result = 2 Then
                        MsgBox("database error, shopkeeper inventory slot " & argShopKeeperInventoryIndex.ToString & " item template index " & _inventory(argShopKeeperInventoryIndex).ToString & " Does not exist", MsgBoxStyle.Critical, "Item Not Found")
                    End If
                End If
            Else
                MsgBox("invalid Shopkeeper inventory index")
            End If
        Else
            MsgBox("I cant Sell you anything, im not a merchent.")
        End If
        main.TalkMenu.updatemoney()
        Return False
    End Function
    Public Sub Rest(ByRef argMoney As Double)
        If _IsInnkeeper Then
            If argMoney >= RestPrice Then
                argMoney -= RestPrice

                For i As Integer = 0 To 3 Step 1
                    If main.Party(i).dead Then
                        main.Party(i).Revive()
                    End If
                    main.Party(i).hp = main.Party(i).maxhp
                    main.Party(i).mp = main.Party(i).maxmp
                Next
            Else
                MsgBox("Dang, you dont have enough money to stay at my Inn")
            End If
        Else
            MsgBox("Im not an Inn keeper, i cant rent you a bed for the night.")
        End If
        main.TalkMenu.updatemoney()
    End Sub
    Public Property RestPrice As Integer
        Get
            Return _RestPrice
        End Get
        Set(value As Integer)
            If value >= 10 Then
                _RestPrice = value
            Else
                _RestPrice = 10
            End If
        End Set
    End Property
    Public Function Talk() As String
        If _lastDialogueIndex < _dialogue.GetUpperBound(0) Then
            _lastDialogueIndex += 1
        Else
            _lastDialogueIndex = 0
        End If
        Return _dialogue(_lastDialogueIndex)
    End Function
    Public Property name As String
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
        End Set
    End Property
End Class
