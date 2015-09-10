Option Explicit On
Option Infer Off
Option Strict On
Imports Project3.main
Public Class WorldMap
    Private _map As Image
    Private _name As String
    Private _movementGrid(23, 23) As Point
    Private _obsticleGrid(23, 23) As Integer
    Private _DestList(23, 23) As globalCordinate
    Private _DestListSerial() As globalCordinate
    Private _spritePosition(12) As Point
    Private _counter As Integer = 0
    Private _npcCounter As Integer = 0
    ' Private _labelarray(23, 23) As Label
    Private _NPClistSerial As String
    Private _NPCList() As Integer
    Private _NPCPosition(12) As Point
    Private _HasNpcs As Boolean
    Public Sub New(ByVal argDestList() As globalCordinate, ByVal argMovementGrid(,) As Point, ByVal argname As String, ByVal argNPCList As String)
        'argdestlist will be a list of global cords that will corospond with 2's in the obsticle grid. if somehow we get errors then they are in the database.
        _name = argname
        _movementGrid = argMovementGrid
        _map = Image.FromFile("art\maps\" & _name & ".png")
        _obsticleGrid = GetDefaultObsticleGrid()
        _DestListSerial = argDestList
        _NPClistSerial = argNPCList
        If Not argNPCList = "aa" Then
            ReDim _NPCList(CInt((argNPCList.Length / 2) - 1))
            For i As Integer = 0 To _NPCList.GetUpperBound(0) Step 1
                _NPCList(i) = CInt(argNPCList.Substring(i * 2, 2))
            Next
            _HasNpcs = True
        Else
            _HasNpcs = False
        End If
        Dim count As Integer = 0

        For i As Integer = 0 To 23 Step 1
            For j As Integer = 0 To 23 Step 1
                If _obsticleGrid(i, j) = 2 Then
                    _DestList(i, j) = argDestList(count)
                    count += 1
                ElseIf _obsticleGrid(i, j) = 6 Then
                    If i = 0 Then
                        _DestList(i, j) = New globalCordinate(CInt(argname.Substring(0, 2)), CInt(argname.Substring(2, 2)) - 1, CInt(argname.Substring(4, 2)), 22, j)
                    ElseIf i = 23 Then
                        _DestList(i, j) = New globalCordinate(CInt(argname.Substring(0, 2)), CInt(argname.Substring(2, 2)) + 1, CInt(argname.Substring(4, 2)), 1, j)
                    ElseIf j = 0 Then
                        _DestList(i, j) = New globalCordinate(CInt(argname.Substring(0, 2)), CInt(argname.Substring(2, 2)), CInt(argname.Substring(4, 2)) - 1, i, 22)
                    ElseIf j = 23 Then
                        _DestList(i, j) = New globalCordinate(CInt(argname.Substring(0, 2)), CInt(argname.Substring(2, 2)), CInt(argname.Substring(4, 2)) + 1, i, 1)
                    End If
                ElseIf _obsticleGrid(i, j) = 3 Then
                    _NPCPosition(_npcCounter) = New Point(i, j)
                    _npcCounter += 1
                End If

            Next
        Next
        If _HasNpcs Then
            If _npcCounter < 12 Then
                For i As Integer = _npcCounter To 12 Step 1
                    _NPCPosition(i) = New Point(-1, -1)
                Next
            ElseIf _npcCounter = 12 Then
                _NPCPosition(12) = New Point(-1, -1)
            End If
        End If
        For i As Integer = 0 To 12 Step 1
            _spritePosition(i) = New Point(-1, -1)
        Next
    End Sub
    Public Sub New(ByVal argMovementGrid(,) As Point, ByVal argname As String, ByVal argNPCList As String)
        'argdestlist will be a list of global cords that will corospond with 2's in the obsticle grid. if somehow we get errors then they are in the database.
        _name = argname
        _movementGrid = argMovementGrid
        _map = Image.FromFile("art\maps\" & _name & ".png")
        _obsticleGrid = GetDefaultObsticleGrid()
        _NPClistSerial = argNPCList
        If Not argNPCList = "aa" Then
            ReDim _NPCList(CInt((argNPCList.Length / 2) - 1))
            For i As Integer = 0 To _NPCList.GetUpperBound(0) Step 1
                _NPCList(i) = CInt(argNPCList.Substring(i * 2, 2))
            Next
            _HasNpcs = True
        Else
            _HasNpcs = False
        End If
        Dim count As Integer = 0

        For i As Integer = 0 To 23 Step 1
            For j As Integer = 0 To 23 Step 1
                If _obsticleGrid(i, j) = 6 Then
                    If i = 0 Then
                        _DestList(i, j) = New globalCordinate(CInt(argname.Substring(0, 2)), CInt(argname.Substring(2, 2)) - 1, CInt(argname.Substring(4, 2)), 22, j)
                    ElseIf i = 23 Then
                        _DestList(i, j) = New globalCordinate(CInt(argname.Substring(0, 2)), CInt(argname.Substring(2, 2)) + 1, CInt(argname.Substring(4, 2)), 1, j)
                    ElseIf j = 0 Then
                        _DestList(i, j) = New globalCordinate(CInt(argname.Substring(0, 2)), CInt(argname.Substring(2, 2)), CInt(argname.Substring(4, 2)) - 1, i, 22)
                    ElseIf j = 23 Then
                        _DestList(i, j) = New globalCordinate(CInt(argname.Substring(0, 2)), CInt(argname.Substring(2, 2)), CInt(argname.Substring(4, 2)) + 1, i, 1)
                    End If
                ElseIf _obsticleGrid(i, j) = 3 Then
                    _NPCPosition(_npcCounter) = New Point(i, j)
                    _npcCounter += 1
                End If

            Next
        Next
        If _HasNpcs Then
            If _npcCounter < 12 Then
                For i As Integer = _npcCounter To 12 Step 1
                    _NPCPosition(i) = New Point(-1, -1)
                Next
            ElseIf _npcCounter = 12 Then
                _NPCPosition(12) = New Point(-1, -1)
            End If
        End If
        For i As Integer = 0 To 12 Step 1
            _spritePosition(i) = New Point(-1, -1)
        Next
    End Sub
    Private Function GetDefaultObsticleGrid() As Integer(,)
        Dim CollisionMap As New Bitmap("art\maps\" & _name & "_COLLISION.png")
        Dim obsticlegrid(23, 23) As Integer
        For i As Integer = 0 To 23 Step 1
            For j As Integer = 0 To 23 Step 1
                If CollisionMap.GetPixel(_movementGrid(i, j).X, _movementGrid(i, j).Y) = Color.FromArgb(237, 28, 36) Then 'red
                    obsticlegrid(i, j) = 1
                ElseIf CollisionMap.GetPixel(_movementGrid(i, j).X, _movementGrid(i, j).Y) = Color.FromArgb(255, 242, 0) Then 'yellow
                    obsticlegrid(i, j) = 6
                ElseIf CollisionMap.GetPixel(_movementGrid(i, j).X, _movementGrid(i, j).Y) = Color.FromArgb(0, 162, 232) Then 'blueish
                    obsticlegrid(i, j) = 3
                ElseIf CollisionMap.GetPixel(_movementGrid(i, j).X, _movementGrid(i, j).Y) = Color.FromArgb(163, 73, 164) Then 'purple
                    obsticlegrid(i, j) = 2
                    ' ElseIf CollisionMap.GetPixel(_movementGrid(i, j).X, _movementGrid(i, j).Y) = Color.FromArgb(34, 177, 76) Then 'green
                    'obsticlegrid(i, j) = 7
                Else
                    obsticlegrid(i, j) = 0
                End If
            Next
        Next
        Return obsticlegrid
        '                movement grid codes
        '        0 open free space - no color
        '1:      pathing block - red - Color.FromArgb(237, 28, 36)
        '2:      teleport square - purple - Color.FromArgb(163, 73, 164)
        '3:      NPC square - blueish - Color.FromArgb(0, 162, 232)
        '4:      monster -N / A
        '5:      player -N / A
        '        6:      auto teleport square- yellow - Color.FromArgb(255, 242, 0)
        '7:      destination -green - Color.FromArgb(34, 177, 76)
    End Function

    Public Function AddPlayer(ByVal argPoint As Point) As Boolean
        If _obsticleGrid(argPoint.X, argPoint.Y) = 0 Then
            _obsticleGrid(argPoint.X, argPoint.Y) = 5
            _spritePosition(0) = argPoint
            Return True
        Else
            Return False
        End If
    End Function
    Public Function AddMob() As Boolean
        Dim rand As New Random
        Dim argpoint As Point = New Point(rand.Next(1, 23), rand.Next(1, 23))
        Dim check As Boolean = True
        Do While check
            If _obsticleGrid(argpoint.X, argpoint.Y) = 0 AndAlso ((Math.Abs(getPlayerCords().X - argpoint.X) > 2) AndAlso (Math.Abs(getPlayerCords().Y - argpoint.Y) > 2)) Then
                _counter += 1
                If _counter <= _spritePosition.GetUpperBound(0) Then
                    _spritePosition(_counter) = argpoint
                    _obsticleGrid(argpoint.X, argpoint.Y) = 4
                    Return True
                Else
                    check = False
                End If
            Else
                argpoint = New Point(rand.Next(1, 23), rand.Next(1, 23))
            End If
        Loop
        Return False
    End Function
    Public Sub KillMob(ByVal argIndex As Integer)
        If argIndex >= 1 And argIndex <= _counter And argIndex <= 12 And Not _spritePosition(argIndex) = New Point(-1, -1) Then
            _obsticleGrid(_spritePosition(argIndex).X, _spritePosition(argIndex).Y) = 0
            _spritePosition(argIndex) = New Point(-1, -1)

        End If
    End Sub
    Public Sub CopyTo(ByRef argWorldMap As WorldMap)
        argWorldMap = New WorldMap(_DestListSerial, main.MovementGrid, _name, _NPClistSerial)
    End Sub
    Public Function getPlayerCords() As Point
        Return _spritePosition(0)
    End Function
    Public Function GetMobCords(ByVal argSprite As Integer) As Point
        If argSprite >= 1 And argSprite <= _counter And argSprite <= 12 And Not _spritePosition(argSprite) = New Point(-1, -1) Then
            Return _spritePosition(argSprite)
        Else
            Return New Point(-1, -1)
        End If
    End Function
    Public Function getNPCIndex(ByVal argNPC As Integer) As Integer

        Return _NPCList(argNPC)
    End Function

    Public Function GetNPCCords(ByRef argNPC As Integer) As Point
        If argNPC >= 0 And argNPC <= 12 And Not _NPCPosition(argNPC) = New Point(-1, -1) Then
            Return _NPCPosition(argNPC)
        Else
            Return New Point(-1, -1)
        End If
    End Function
    'Public Sub ShowLables()
    '    For i As Integer = 0 To 23 Step 1
    '        For j As Integer = 0 To 23 Step 1
    '            If Not _labelarray(i, j) Is Nothing Then
    '                _labelarray(i, j).Visible = True
    '            End If
    '        Next
    '    Next
    'End Sub
    'Public Sub HideLables()
    '    For i As Integer = 0 To 23 Step 1
    '        For j As Integer = 0 To 23 Step 1
    '            If Not _labelarray(i, j) Is Nothing Then
    '                _labelarray(i, j).Visible = False
    '            End If
    '        Next
    '    Next
    'End Sub
    Public Function getDest(ByVal argPoint As Point, ByRef argDest As globalCordinate) As Boolean
        If Not _DestList(argPoint.X, argPoint.Y) Is Nothing Then
            argDest = _DestList(argPoint.X, argPoint.Y)
            Return True
        Else
            Return False
        End If
    End Function
    Public Function Move(ByVal argDestination As Point, ByVal argSprite As Integer) As Integer ' 0= worked -1=failed 1=battle 2=npc talk 3 = move to new location
        If argSprite >= 0 And argSprite <= _spritePosition.GetUpperBound(0) And argSprite <= _counter Then 'proper sprite integer
            If _spritePosition(argSprite).X > -1 And _spritePosition(argSprite).Y > -1 Then 'checks if the sprite is in play or not.
                If argDestination.X >= 0 And argDestination.Y >= 0 And argDestination.X <= 23 And argDestination.Y <= 23 Then ' checks total bounds
                    If _obsticleGrid(argDestination.X, argDestination.Y) = 0 Then 'clear spot
                        If argSprite = 0 Then
                            _obsticleGrid(argDestination.X, argDestination.Y) = 5 'sets new position to player
                        Else
                            _obsticleGrid(argDestination.X, argDestination.Y) = 4 'sets new position to mob
                        End If
                        _obsticleGrid(_spritePosition(argSprite).X, _spritePosition(argSprite).Y) = 0 'sets old position as clear
                        _spritePosition(argSprite) = argDestination
                        main.MapSprites(argSprite).Location = argDestination
                        main.MapSprites(argSprite).MoveImage()
                        Return 0
                    ElseIf _obsticleGrid(argDestination.X, argDestination.Y) = 1 Then 'obsticle
                        Return -1
                    ElseIf _obsticleGrid(argDestination.X, argDestination.Y) = 2 OrElse _obsticleGrid(argDestination.X, argDestination.Y) = 6 Then 'leave the zone
                        If argSprite = 0 Then 'PC check
                            Return 3
                        End If
                    ElseIf _obsticleGrid(argDestination.X, argDestination.Y) = 4 Then 'monster
                        If argSprite = 0 Then

                            Return 1
                        End If
                    ElseIf _obsticleGrid(argDestination.X, argDestination.Y) = 3 Then 'npc
                        If argSprite = 0 Then
                            Return 2
                        End If
                    ElseIf _obsticleGrid(argDestination.X, argDestination.Y) = 5 Then 'player
                        Return 1
                    End If
                End If
            End If
        End If
        Return -1

    End Function
    Public Property HasNPCS As Boolean
        Get
            Return _HasNpcs
        End Get
        Set(value As Boolean)
            _HasNpcs = value
        End Set
    End Property
    Private Sub AnimatePC()

    End Sub
    Public ReadOnly Property npccounter As Integer
        Get
            Return _npcCounter
        End Get
    End Property
    Public ReadOnly Property counter As Integer
        Get
            Return _counter
        End Get
    End Property
    Public ReadOnly Property map As Image
        Get
            Return _map
        End Get
    End Property
End Class
