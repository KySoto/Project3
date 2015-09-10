Public Class WorldMapSprites
    'stuff i need to handle here
    'picturebox 
    'movement grid location
    'hell the movement for the mobs but not the PC
    'the index
    Private _Sprite As PictureBox
    Private _location As Point
    Private _mobIndex As Integer
    Private _direction As Integer
    Private _npcIndex As Integer

    Public Sub New()
        _Sprite = New PictureBox
        _Sprite.Size = New Size(24, 24)
        _Sprite.Visible = False
        _Sprite.BackColor = Color.Transparent
        _direction = 3
        _mobIndex = 0
        _npcIndex = 0
        Location = New Point(0, 0)
        main.Controls.Add(_Sprite)
    End Sub
    Public Sub Dispose()
        _Sprite.Dispose()
    End Sub

    Public Property Active As Boolean
        Get
            Return _Sprite.Visible
        End Get
        Set(value As Boolean)
            _Sprite.Visible = value
        End Set
    End Property
    Public Property Sprite As Image
        Get
            Return _Sprite.Image
        End Get
        Set(value As Image)
            _Sprite.Image = value
        End Set
    End Property
    Public Property Location As Point
        Get
            Return _location
        End Get
        Set(value As Point)
            If value.X >= 0 And value.Y >= 0 And value.X <= 23 And value.Y <= 23 Then
                _location = value
            End If
        End Set
    End Property
    Public Property MobIndex As Integer
        Get
            Return _mobIndex
        End Get
        Set(value As Integer)
            If value <= main.MobTemplate.GetUpperBound(0) And value >= 0 Then
                _mobIndex = value
            End If

        End Set
    End Property
    Public Property npcIndex As Integer
        Get
            Return _npcIndex
        End Get
        Set(value As Integer)
            If value <= main.NPCTemplate.GetUpperBound(0) And value >= 0 Then
                _npcIndex = value
            End If

        End Set
    End Property
    Public Property Direction As Integer
        Get
            Return _direction
        End Get
        Set(value As Integer)
            If value >= 0 And value <= 3 Then
                _direction = value
            End If
        End Set
    End Property
    Public Sub MoveImage() ' to the location variable.
        _Sprite.Location = main.MovementGrid(Location.X, Location.Y)
        _Sprite.BringToFront()
    End Sub
End Class
