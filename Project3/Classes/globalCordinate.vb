Public Class globalCordinate
    Private _x As Integer
    Private _y As Integer
    Private _areaX As Integer
    Private _areaY As Integer
    Private _zone As Integer '0 is null 1 is interior 2 is forest
    Private _xy As Point
    Private _axy As Point
    Public Sub New()
        x = 0
        y = 0
        areax = 0
        areay = 0
        zone = 0
    End Sub
    Public Sub New(ByVal argZone As Integer, ByVal argAreaX As Integer, ByVal argAreaY As Integer, ByVal argX As Integer, ByVal argY As Integer)
        x = argX
        y = argY
        areax = argAreaX
        areay = argAreaY
        zone = argZone
        _xy = New Point(x, y)
        _axy = New Point(areax, areay)
    End Sub
    Public Property xyPair As Point
        Get
            Return _xy
        End Get
        Set(value As Point)
            If value.X >= 0 And value.X <= 23 And value.Y >= 0 And value.Y <= 23 Then
                _xy = value
            End If
        End Set
    End Property
    Public Property xyAreaPair As Point
        Get
            Return _axy
        End Get
        Set(value As Point)
            If value.X >= 0 And value.X <= 99 And value.Y >= 0 And value.Y <= 99 Then
                _xy = value
            End If
        End Set
    End Property
    Public Property x As Integer
        Get
            Return _x
        End Get
        Set(value As Integer)
            If value >= 0 And value <= 23 Then
                _x = value
            End If
        End Set
    End Property
    Public Property y As Integer
        Get
            Return _y
        End Get
        Set(value As Integer)
            If value >= 0 And value <= 23 Then
                _y = value
            End If
        End Set
    End Property
    Public Property areax As Integer
        Get
            Return _areaX
        End Get
        Set(value As Integer)
            If value >= 0 And value <= 99 Then
                _areaX = value
            End If
        End Set
    End Property
    Public Property areay As Integer
        Get
            Return _areaY
        End Get
        Set(value As Integer)
            If value >= 0 And value <= 99 Then
                _areaY = value
            End If
        End Set
    End Property
    Public Property zone As Integer
        Get
            Return _zone
        End Get
        Set(value As Integer)
            If value >= 0 And value <= 99 Then
                _zone = value
            End If
        End Set
    End Property
End Class
