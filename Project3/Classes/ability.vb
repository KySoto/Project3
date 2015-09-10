Public Class ability 'class for spells and skills
    Private _HealSpell As Boolean
    Private _cost As Double
    Private _damageRange As Double
    Private _statMod As Double
    Private _name As String
    'Private _PCability As Boolean
    Public Sub New(ByVal argName As String, ByVal argCost As Double, ByVal argDamageRange As Double, ByVal argStatMod As Double, ByVal argHealSpell As Boolean) 'no non constructor because meh
        _name = argName
        _cost = argCost
        _damageRange = argDamageRange
        _statMod = argStatMod
        ' _PCability = argPCability
        _HealSpell = argHealSpell
    End Sub
    Public ReadOnly Property cost As Double
        Get
            Return _cost
        End Get
    End Property
    Public ReadOnly Property damageRange As Double
        Get
            Return _damageRange
        End Get
    End Property
    Public ReadOnly Property statMod As Double
        Get
            Return _statMod
        End Get
    End Property
    Public ReadOnly Property name As String
        Get
            Return _name
        End Get
    End Property
    Public ReadOnly Property HealSpell As Boolean
        Get
            Return _HealSpell
        End Get
    End Property
    'Public ReadOnly Property PCability As Boolean
    '    Get
    '        Return _PCability
    '    End Get
    'End Property
End Class
