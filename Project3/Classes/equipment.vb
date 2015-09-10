Public Class equipment
    Private _index As Integer
    Private _name As String
    Private _atk As Double
    Private _defense As Double
    Private _speed As Double
    Private _magic As Double
    Private _resist As Double
    Private _cost As Double
    Private _extraatkrange As Double
    Private _slot As Integer
    Private _handed2 As Boolean
    Private _consumable As Boolean
    Private _stack As Integer
    Const MAXSTACK As Integer = 99
    Private _isTemplate As Boolean
    Public Sub New()

    End Sub
    Public Sub New(ByVal argName As String, ByVal argIndex As Integer, ByVal argAtk As Double, ByVal argdefense As Double, ByVal argspeed As Double, ByVal argmagic As Double, ByVal argresist As Double, argcost As Double, ByVal argextraatkrange As Double, ByVal argslot As Integer, ByVal arg2h As Boolean, ByVal argConsumable As Boolean)
        _index = argIndex
        _atk = argAtk
        _defense = argdefense
        _speed = argspeed
        _magic = argmagic
        _resist = argresist
        _cost = argcost
        _extraatkrange = argextraatkrange
        _slot = argslot 'slot -1 is null slot 0 is mainhand slot 1 is offhand slot 2 is armor
        _handed2 = arg2h
        _name = argName
        _consumable = argConsumable 'yay for consumables
        _isTemplate = True
        _stack = 1
    End Sub
    Public Overrides Function ToString() As String
        If _consumable Then
            Return name & " " & stack
        Else
            Return name
        End If
    End Function
    Public Property stack As Integer
        Get
            Return _stack
        End Get
        Set(value As Integer)
            If _consumable Then
                If value >= 0 And value <= MAXSTACK Then
                    _stack = value
                Else
                    If value <= 0 Then
                        _stack = 0
                    Else
                        _stack = MAXSTACK
                    End If
                End If
            Else
                If value >= 0 And value <= 1 Then
                    _stack = value
                Else
                    If value <= 0 Then
                        _stack = 0
                    Else
                        _stack = 1
                    End If
                End If
            End If
        End Set
    End Property
    Public ReadOnly Property consumable As Boolean
        Get
            Return _consumable
        End Get
    End Property
    Public ReadOnly Property name As String
        Get
            Return _name
        End Get
    End Property
    Public ReadOnly Property slot As Integer
        Get
            Return _slot
        End Get
    End Property
    Public ReadOnly Property slotName As String
        Get
            Select Case _slot
                Case -1
                    Return "Null"
                Case 0
                    Return "Mainhand"
                Case 1
                    Return "Offhand"
                Case 2
                    Return "Armor"
                Case Else
                    Return "Null"
            End Select
        End Get
    End Property
    Public ReadOnly Property handed2 As Boolean
        Get
            Return _handed2
        End Get
    End Property
    Public ReadOnly Property extraatkrange As Double
        Get
            Return _extraatkrange
        End Get
    End Property
    Public ReadOnly Property index As Integer
        Get
            Return _index
        End Get
    End Property
    Public ReadOnly Property atk As Double
        Get
            Return _atk
        End Get
    End Property
    Public ReadOnly Property defense As Double
        Get
            Return _defense
        End Get
    End Property
    Public ReadOnly Property speed As Double
        Get
            Return _speed
        End Get
    End Property
    Public ReadOnly Property magic As Double
        Get
            Return _magic
        End Get
    End Property
    Public ReadOnly Property resist As Double
        Get
            Return _resist
        End Get
    End Property
    Public ReadOnly Property cost As Double
        Get
            Return _cost
        End Get
    End Property
    Public Sub ConvertFromTemplate()
        _isTemplate = False
    End Sub
    Public Sub CopyTo(ByRef argEquip As equipment)
        argEquip = New equipment(_name, _index, _atk, _defense, _speed, _magic, _resist, _cost, _extraatkrange, _slot, _handed2, _consumable)
        argEquip.ConvertFromTemplate()
    End Sub
End Class
