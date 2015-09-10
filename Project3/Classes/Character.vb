Option Explicit On
Option Infer Off
Option Strict On
Imports Project3.main
Public Class Character
    Private _anim(10) As Image ' (0 1 N) (2 3 W ) (4 5 E) (6 7 S) (8 9 Battle)(10 = portrate)
    Private _animstr As String
    Private _atk As Double 'attack stat
    Private _MPBar As ProgressBar 'these wont show up unless its a pc and not a template
    Private _HPBar As ProgressBar 'these wont show up unless its a pc and not a template
    Private _Name As Label 'these wont show up unless its a pc and not a template
    Private _charname As String
    Private _classandlevel As Label 'these wont show up unless its a pc and not a template
    Private _defense As Double 'defense stat
    Private _hp As Double 'current health
    Private _mp As Double 'current mana
    Private _IsEnemy As Boolean 'to differentiate between mobs and PC's
    Private _magic As Double 'magic damage stat
    Private _resist As Double 'magic defense stat
    Private _hpminrange As Double 'hp range mobs
    Private _hpmaxrange As Double 'hp range
    Private _mpminrange As Double 'mana range mobs
    Private _mpmaxrange As Double 'mana range
    Private _IsMagicUser As Boolean 'since we should make abilities cost points no matter what, this will dictate the color of the mana bar
    Private _xp As Double ' for mobs how much xp they are worth for pc's how much they have
    Private _money As Double 'for mobs its how much they are worth and for pc's its how much they add to the group total of cash
    Private _IsTemplate As Boolean 'template check
    Private _maxhp As Double 'max hp
    Private _maxmp As Double 'max mp
    Private _dead As Boolean 'dead check
    Private _initialized As Boolean 'a check to make sure it doesnt do things that could break the program
    Private _speed As Double 'used in determining initiative order
    Private _atkrange As Double 'the RNG range for attacking
    Private _currentLevel As Integer 'current level of the PC as an index, for mobs its not used
    Private _xplevel(MAXLEVEL) As Double 'exp table
    Const MAXLEVEL As Integer = 99 'in index form
    Private _class As Integer '0=fighter 1=rouge 2=cleric 3=mage 4=monster even though we have a bool for that.
    Private _hpbonus As Double 'is the RNG range to add to the stat and is used by the level up function
    Private _mpbonus As Double 'is the RNG range to add to the stat and is used by the level up function for casters, otherwise it adds it directly to the stat
    Private _atkbonus As Double 'is the RNG range to add to the stat and is used by the level up function
    Private _defensebonus As Double 'is the RNG range to add to the stat and is used by the level up function
    Private _magicbonus As Double 'is the RNG range to add to the stat and is used by the level up function
    Private _resistbonus As Double 'is the RNG range to add to the stat and is used by the level up function
    Private _atkrangebonus As Double 'is the RNG range to add to the stat and is used by the level up function for non mages, otherwise it adds it directly to the stat
    Private _speedbonus As Double 'is the RNG range to add to the stat and is used by the level up function
    Private _className As String 'added this in since monster type.
    Private _weaponIndex As Integer 'identifier of your weapon
    Private _weapon As Boolean 'is the slot occupied
    Private _offhandindex As Integer 'identifier of your offhand
    Private _offhand As Boolean 'is hte slot occupied
    Private _armorindex As Integer 'identifier of your armor
    Private _armor As Boolean 'is the slot occupied
    Private _playerSlot As Integer
    Private _load As Boolean
    Private rand As New Random
    'may add more armor info
    Public spellList(4) As Integer 'each person has 1 basic attack and 5 spells/abilities and the run command
    Public Sub New()

    End Sub

    Public Sub New(ByVal arganim As String, ByVal argatk As Double, ByVal argdefense As Double, ByVal argName As String, ByVal arglevel As Integer, ByVal argIsEnemy As Boolean, ByVal argIsMagicUser As Boolean, ByVal arghpmax As Double, ByVal argmpmax As Double, ByVal arghpmin As Double, ByVal argmpmin As Double, ByVal argresist As Double, ByVal argmagic As Double, ByVal argxp As Double, ByVal argmoney As Double, ByVal argspeed As Double, ByVal argatkrange As Double, ByVal argclass As Integer, ByVal arghpbonus As Double, ByVal argmpbonus As Double, ByVal argatkbonus As Double, ByVal argdefensebonus As Double, ByVal argmagicbonus As Double, ByVal argresistbonus As Double, ByVal argatkragebonus As Double, ByVal argspeedbonus As Double, ByVal argClassname As String, ByVal argSpellList() As Integer)
        _initialized = False

        For i As Integer = 0 To 10 Step 1
            If IO.File.Exists(arganim & "\" & i & ".png") Then
                _anim(i) = Image.FromFile(arganim & "\" & i & ".png") 'arganim is the string from the DB pointing to the correct folder
            Else
                _anim(i) = Image.FromFile("art\CharacterPlaceholder.png")
            End If

        Next
        _animstr = arganim
        charname = argName
        _class = argclass
        _className = argClassname
        atk = argatk
        atkrange = argatkrange
        _atkbonus = argatkbonus
        _atkrangebonus = argatkragebonus
        defense = argdefense
        _defensebonus = argdefensebonus
        magic = argmagic
        _magicbonus = argmagicbonus
        resist = argresist
        _resistbonus = argresistbonus
        speed = argspeed
        _speedbonus = argspeedbonus
        _IsEnemy = argIsEnemy
        _IsMagicUser = argIsMagicUser
        _IsTemplate = True 'i realized i would NEVER create an object that ISNT a template.
        _hpmaxrange = arghpmax
        _hpminrange = arghpmin
        _mpmaxrange = argmpmax
        _mpminrange = argmpmin
        _hpbonus = arghpbonus
        _mpbonus = argmpbonus
        _xp = argxp
        If _IsEnemy Then
            _maxhp = 1
            _maxmp = 1
            _hp = 1
            _mp = 1
        Else
            _maxhp = arghpmax
            _maxmp = argmpmax
            _hp = arghpmax
            _mp = argmpmax
        End If
        _money = argmoney
        _weaponIndex = 0
        _weapon = False
        _offhandindex = 0
        _weapon = False
        _armorindex = 0
        _armor = False
        spellList = argSpellList
        _playerSlot = -1
        setleveltable()
        _currentLevel = arglevel
    End Sub

    Public Sub New(ByVal argatk As Double, ByVal argatkrange As Double, ByVal argdefense As Double, ByVal argName As String, ByVal arglevel As Integer, ByVal arghpmax As Double, ByVal argmpmax As Double, ByVal argresist As Double, ByVal argmagic As Double, ByVal argxp As Double, ByVal argspeed As Double, ByVal argmhindex As Integer, ByVal argohindex As Integer, ByVal argarmorindex As Integer, ByRef argTemplate As Character) 'the load system constructor
        _initialized = False

        For i As Integer = 0 To 10 Step 1
            If IO.File.Exists(argTemplate.animstr & "\" & i & ".png") Then
                _anim(i) = Image.FromFile(argTemplate.animstr & "\" & i & ".png") 'arganim is the string from the DB pointing to the correct folder
            Else
                _anim(i) = Image.FromFile("art\CharacterPlaceholder.png")
            End If

        Next
        _animstr = argTemplate.animstr
        charname = argName
        _class = argTemplate.playerslot
        _className = argTemplate.ClassName
        atk = argatk
        atkrange = argTemplate.atkrange
        _atkbonus = argTemplate.atkbonus
        _atkrangebonus = argTemplate.atkrangebonus
        defense = argdefense
        _defensebonus = argTemplate.defensebonus
        magic = argmagic
        _magicbonus = argTemplate.magicbonus
        resist = argresist
        _resistbonus = argTemplate.resistbonus
        speed = argspeed
        _speedbonus = argTemplate.speedbonus
        _IsEnemy = False
        _IsMagicUser = argTemplate.iscaster
        _IsTemplate = True 'i realized i would NEVER create an object that ISNT a template even on load.
        _hpmaxrange = arghpmax
        _hpminrange = 0
        _mpmaxrange = argmpmax
        _mpminrange = 0
        _hpbonus = argTemplate.hpbonus
        _mpbonus = argTemplate.mpbonus
        _xp = argxp
        _maxhp = arghpmax
        _maxmp = argmpmax
        _hp = arghpmax
        _mp = argmpmax
        _money = argTemplate.Money
        _weaponIndex = argmhindex
        If argmhindex = 0 Then
            _weapon = False
        Else
            _weapon = True
        End If
        If argohindex = 0 Then
            _offhand = False
        Else
            _offhand = True
        End If
        If argarmorindex = 0 Then
            _armor = False
        Else
            _armor = True
        End If
        _offhandindex = argohindex
        _armorindex = argarmorindex
        spellList = argTemplate.spellList
        _playerSlot = -1
        setleveltable()
        _currentLevel = arglevel
    End Sub
    Public Sub Revive()
        _dead = False
        _hp = 1
    End Sub
    Public Sub CopyTo(ByRef ArgCharacter As Character, ByVal argload As Boolean)
        If _IsTemplate Then
            ArgCharacter = New Character(_animstr, atk, defense, charname, _currentLevel, _IsEnemy, _IsMagicUser, _hpmaxrange, _mpmaxrange, _hpminrange, _mpminrange, resist, magic, _xp, _money, speed, atkrange, _class, _hpbonus, _mpbonus, _atkbonus, _defensebonus, _magicbonus, _resistbonus, _atkrangebonus, _speedbonus, _className, spellList)
        End If

    End Sub

    Public Sub convertFromTemplate(ByVal argslot As Integer)
        _IsTemplate = False
        If _IsEnemy Then
            calculateMobHPandMP()
        Else
            playerslot = argslot
            PlaceInterface(playerslot)
        End If
    End Sub

    Public Function getimage(ByVal argDirection As Integer, ByVal argPrevious As Integer) As Image '(0 N) (1 W ) (2 E) (3 S) (4 Battle) previous refers to if it was image 0 or  of the animation so wierd things dont happen
        If argDirection >= 0 Or argDirection <= 4 Then
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
    Private Sub setleveltable()
        _xplevel(0) = 20
        For i As Integer = 1 To MAXLEVEL Step 1
            _xplevel(i) = _xplevel(i - 1) + (i + 1) * 20 'level * 20 + previous experiance amount so level 1 is 0xp level 2 is 40xp level 3 is 120 
        Next
    End Sub
    Private Sub PlaceInterface(ByVal i As Integer)
        If Not _IsTemplate And Not _IsEnemy And (i >= 0 And i <= 3) Then
            _Name = New Label()
            _classandlevel = New Label()
            _HPBar = New ProgressBar()
            _MPBar = New ProgressBar()
            _Name.Location = New Point(33, 33 + (128 * i))
            _classandlevel.Location = New Point(33, 65 + (128 * i))
            _HPBar.Location = New Point(33, 97 + (128 * i))
            _MPBar.Location = New Point(33, 112 + (128 * i))
            _Name.Text = charname
            _Name.AutoSize = False
            _Name.Size = New Size(192, 30)
            _classandlevel.AutoSize = False
            _classandlevel.Size = New Size(192, 30)
            _Name.Font = New Font("Courier New", 14.25)
            _classandlevel.Font = New Font("Courier New", 9)
            _HPBar.Size = New Size(192, 15)
            _HPBar.ForeColor = Color.Maroon
            _HPBar.BackColor = Color.Black
            _MPBar.Size = New Size(192, 15)
            If _IsMagicUser Then
                _MPBar.ForeColor = Color.Navy
            Else
                _MPBar.ForeColor = Color.Gold
            End If
            _MPBar.BackColor = Color.Black
            _HPBar.Style = ProgressBarStyle.Continuous
            _MPBar.Style = ProgressBarStyle.Continuous
            _maxhp = _hpmaxrange
            _maxmp = _mpmaxrange
            hp = _maxhp
            mp = _maxmp
            _HPBar.Maximum = CInt(_maxhp)
            _MPBar.Maximum = CInt(_maxmp)
            _HPBar.Value = CInt(hp)
            _MPBar.Value = CInt(mp)
            _classandlevel.Text = _className & " Level " & currentlevel & ControlChars.NewLine & "HP " & hp & "/" & maxhp & " MP " & mp & "/" & maxmp
            With main.Controls
                .Add(_Name)
                .Add(_classandlevel)
                .Add(_HPBar)
                .Add(_MPBar)
            End With

            _initialized = True
        End If
    End Sub
    Public Sub removeControls()
        _Name.Dispose()
        _classandlevel.Dispose()
        _HPBar.Dispose()
        _MPBar.Dispose()
    End Sub
    Public Sub calculateMobHPandMP() 'only for non template mobs
        If Not _IsTemplate And _IsEnemy Then

            _maxhp = rand.Next(CInt(_hpminrange), CInt(_hpmaxrange))
            _maxmp = rand.Next(CInt(_mpminrange), CInt(_mpmaxrange))
            hp = _maxhp
            mp = _maxmp
        End If
    End Sub
    Public Function Initiative() As Integer
        Return (rand.Next(1, 21) + CInt(speed))
    End Function
    Public Function Attack() As Double
        Return (rand.Next(1, (1 + CInt(_atkrange))) + atk)
    End Function
    Public Function Defend(ByVal argDamge As Double) As Double
        If (argDamge - defense) >= 1 Then
            Return argDamge - defense
        Else
            Return 1
        End If
    End Function
    Public Function Cast(ByVal argability As ability, ByRef argDamage As Double) As Boolean 'yay works for mages and non mages alike!
        If (mp - argability.cost) >= 0 And Not argability.name.Equals("Null") Then
            mp -= argability.cost
            'MsgBox(charname & " casts " & argability.name)
            If argability.HealSpell Then
                argDamage = ((magic * argability.statMod) + rand.Next(1, CInt(argability.damageRange))) * -1
            Else

                If _IsMagicUser Then
                    argDamage = (magic * argability.statMod) + rand.Next(1, CInt(argability.damageRange))
                Else
                    argDamage = (atk * argability.statMod) + rand.Next(1, CInt(argability.damageRange))
                End If
            End If
            Return True
        Else
            argDamage = 0
            Return False
        End If
    End Function
    Public Function Resistance(ByVal argDamge As Double, ByVal IsHealing As Boolean) As Double
        If IsHealing Then
            Return argDamge
        Else
            If (argDamge - resist) >= 1 Then
                Return argDamge - resist
            Else
                Return 1
            End If
        End If

    End Function
    Public Sub AddXP(ByVal argXPBonusas As Double) 'needs work
        If argXPBonusas > 0 Then
            _xp += argXPBonusas
            Dim tempint As Integer = 0
            For i As Integer = _currentLevel To MAXLEVEL Step 1
                If xp >= _xplevel(i) Then
                    tempint += 1
                End If
            Next
            If tempint > 0 Then
                LevelUp(tempint)
            End If
        End If
    End Sub
    Public Sub SetXP(ByVal argXP As Double) 'does NOT trigger a level up, used for the load system ONLY
        If argXP > 0 Then
            _xp = argXP
        End If

    End Sub
    Public Sub LevelUp(ByVal argLevelsAdded As Integer)
        If argLevelsAdded > 0 Then
            For i As Integer = _currentLevel To (_currentLevel + argLevelsAdded - 1) Step 1
                _currentLevel += 1
                If _currentLevel <= MAXLEVEL Or _IsEnemy Then
                    defense += rand.Next(1, CInt(_defensebonus))
                    speed += rand.Next(0, CInt(_speedbonus))
                    If _class = 4 Then 'if its the mage specifically then it doesnt do a range it just adds 1 unless we decide to change it later for whatever reason
                        atkrange += _atkbonus
                        atk += _atkbonus
                    Else
                        atkrange += rand.Next(1, CInt(_atkbonus))
                        atk += rand.Next(1, CInt(_atkbonus))
                    End If
                    If _IsMagicUser Then
                        magic += rand.Next(1, CInt(_magicbonus))
                        _maxmp += rand.Next(1, CInt(_mpbonus))
                        resist += rand.Next(1, CInt(_resistbonus))
                    Else
                        magic += _magicbonus 'should be 0
                        resist += _resistbonus
                        _maxmp += _mpbonus 'should be 1
                    End If

                    _maxhp += rand.Next(1, CInt(_hpbonus))
                    _hp = _maxhp
                    _mp = _maxmp
                    If Not _IsTemplate And Not _IsEnemy Then
                        _HPBar.Maximum = CInt(_maxhp)
                        _MPBar.Maximum = CInt(_maxmp)
                        _HPBar.Value = CInt(hp)
                        _MPBar.Value = CInt(mp)
                        _classandlevel.Text = _className & " Level " & currentlevel & ControlChars.NewLine & "HP " & CInt(hp) & "/" & maxhp & " MP " & CInt(mp) & "/" & maxmp
                    End If
                    If _IsEnemy Then
                        _xp = _xp * 1.12
                        _money = _money * 1.12
                    End If
                Else
                    _currentLevel = MAXLEVEL
                End If
            Next
        End If
    End Sub
    Public Sub SetEquipment(ByVal argMH As Integer, ByVal argOH As Integer, ByVal argArm As Integer)
        If argMH = 0 Then
            _weapon = False
        Else
            _weapon = True
            _weaponIndex = argMH
        End If
        If argOH = 0 Then
            _offhand = False
        Else
            _weapon = True
            _offhandindex = argOH
        End If
        If argArm = 0 Then
            _armor = False
        Else
            _armor = True
            _armorindex = argArm
        End If
    End Sub
    Public Function EquipItem(ByVal argEquipIndex As Integer) As Boolean
        Select Case main.EquipsTemplate(argEquipIndex).slot
            Case 1
                If weapon Then
                    If Not DequipItem(1) Then
                        Return False
                    End If
                End If
                If main.EquipsTemplate(argEquipIndex).handed2 AndAlso offhand Then
                    If Not DequipItem(2) Then
                        Return False
                    End If
                End If
                weapon = True
                weaponindex = argEquipIndex
            Case 2
                If weapon AndAlso main.EquipsTemplate(argEquipIndex).handed2 Then
                    If Not DequipItem(1) Then
                        Return False
                    End If
                End If
                If offhand Then
                    If Not DequipItem(2) Then
                        Return False
                    End If
                End If
                offhand = True
                offhandindex = argEquipIndex
            Case 3
                If armor Then
                    If Not DequipItem(3) Then
                        Return False
                    End If
                End If
                armor = True
                armorindex = argEquipIndex
            Case Else
                Return False

        End Select
        atk += main.EquipsTemplate(argEquipIndex).atk
        defense += main.EquipsTemplate(argEquipIndex).defense
        speed += main.EquipsTemplate(argEquipIndex).speed
        magic += main.EquipsTemplate(argEquipIndex).magic
        resist += main.EquipsTemplate(argEquipIndex).resist
        atkrange += main.EquipsTemplate(argEquipIndex).extraatkrange
        Return True
    End Function
    Public Function DequipItem(ByVal argSlot As Integer) As Boolean
        Dim i As Integer = 0
        Select Case argSlot
            Case 1
                weapon = False
                i = weaponindex
                weaponindex = 0
            Case 2
                offhand = False
                i = offhandindex
                offhandindex = 0
            Case 3
                armor = False
                i = armorindex
                armorindex = 0
            Case Else
                Return False
        End Select
        atk -= main.EquipsTemplate(i).atk
        defense -= main.EquipsTemplate(i).defense
        speed -= main.EquipsTemplate(i).speed
        magic -= main.EquipsTemplate(i).magic
        resist -= main.EquipsTemplate(i).resist
        atkrange -= main.EquipsTemplate(i).extraatkrange
        main.InsertItemInToInventory(i, 1)
        Return True
    End Function
    
    Public Function equipmentString() As String
        Return "Weapon: " & main.EquipsTemplate(weaponindex).name & ControlChars.NewLine & "Offhand: " & main.EquipsTemplate(weaponindex).name & ControlChars.NewLine & "Armor: " & main.EquipsTemplate(weaponindex).name
    End Function
    Public Property speed As Double
        Get
            Return _speed
        End Get
        Set(value As Double)
            _speed = value
        End Set
    End Property
    Public Property xp As Double

        Get
            Return _xp
        End Get
        Set(value As Double)
            _xp = value
        End Set
    End Property
    Public ReadOnly Property currentlevel As Integer
        Get
            Return _currentLevel + 1
        End Get
    End Property
    Public Property atk As Double
        Get
            Return _atk
        End Get
        Set(value As Double)
            If value >= 0 Then
                _atk = value
            Else
                _atk = 0
            End If
        End Set
    End Property
    Public Property atkrange As Double
        Get
            Return _atkrange
        End Get
        Set(value As Double)
            If value > 0 Then
                _atkrange = value
            Else
                _atkrange = 1
            End If
        End Set
    End Property
    Public Property defense As Double
        Get
            Return _defense
        End Get
        Set(value As Double)
            If value >= 0 Then
                _defense = value
            Else
                _defense = 0
            End If
        End Set
    End Property
    Public Property magic As Double
        Get
            Return _magic
        End Get
        Set(value As Double)
            If value >= 0 Then
                _magic = value
            Else
                _magic = 0
            End If
        End Set
    End Property
    Public Property resist As Double
        Get
            Return _resist
        End Get
        Set(value As Double)
            If value >= 0 Then
                _resist = value
            Else
                _resist = 0
            End If
        End Set
    End Property
    Public Property hp As Double
        Get
            Return _hp
        End Get
        Set(value As Double)
            If value <= 0 Then
                _hp = 0
                _dead = True

            ElseIf value > _maxhp Then
                _hp = _maxhp
            Else
                _hp = value
            End If
            If Not _IsTemplate And Not _IsEnemy And _initialized Then
                _HPBar.Value = CInt(_hp)
                _classandlevel.Text = _className & " Level " & currentlevel & ControlChars.NewLine & "HP " & CInt(hp) & "/" & maxhp & " MP " & CInt(mp) & "/" & maxmp
            End If

        End Set
    End Property
    Public ReadOnly Property maxhp As Double
        Get
            Return _maxhp
        End Get
    End Property
    Public ReadOnly Property maxmp As Double
        Get
            Return _maxmp
        End Get
    End Property
    Public Property mp As Double
        Get
            Return _mp
        End Get
        Set(value As Double)
            If value < 0 Then
                _mp = 0
            ElseIf value > _maxmp Then
                _mp = _maxmp
            Else
                _mp = value
            End If
            If Not _IsTemplate And Not _IsEnemy And _initialized Then
                _MPBar.Value = CInt(_mp)
                _classandlevel.Text = _className & " Level " & currentlevel & ControlChars.NewLine & "HP " & CInt(hp) & "/" & maxhp & " MP " & CInt(mp) & "/" & maxmp
            End If
        End Set
    End Property
    Public Property dead As Boolean
        Get
            Return _dead
        End Get
        Set(value As Boolean)
            _dead = value
        End Set
    End Property
    Public Property charname As String
        Get
            Return _charname
        End Get
        Set(value As String)
            _charname = value
        End Set
    End Property
    Public Property playerslot As Integer 'number between 0 and 3 -1 means it was set to an invalid number or its a monster.
        Get
            Return _playerSlot
        End Get
        Set(value As Integer)
            If value >= 0 And value <= 3 Then
                _playerSlot = value
            Else
                _playerSlot = -1
            End If
        End Set
    End Property
    Public Property weaponindex As Integer
        Get
            Return _weaponIndex
        End Get
        Set(value As Integer)
            _weaponIndex = value
        End Set
    End Property
    Public Property weapon As Boolean
        Get
            Return _weapon
        End Get
        Set(value As Boolean)
            _weapon = value
        End Set
    End Property
    Public Property offhandindex As Integer
        Get
            Return _offhandindex
        End Get
        Set(value As Integer)
            _offhandindex = value
        End Set
    End Property
    Public Property offhand As Boolean
        Get
            Return _offhand
        End Get
        Set(value As Boolean)
            _offhand = value
        End Set
    End Property
    Public Property armorindex As Integer
        Get
            Return _armorindex
        End Get
        Set(value As Integer)
            _armorindex = value
        End Set
    End Property
    Public Property armor As Boolean
        Get
            Return _armor
        End Get
        Set(value As Boolean)
            _armor = value
        End Set
    End Property
    Public Property txtname As String
        Get
            Return _Name.Text
        End Get
        Set(value As String)
            _Name.Text = value
        End Set
    End Property
    Public ReadOnly Property Portrate As Image
        Get
            If Not _IsEnemy Then
                Return _anim(10)
            End If
            Return _anim(8)
        End Get
    End Property
    Public ReadOnly Property Money As Double
        Get
            Return _money
        End Get
    End Property
    Public ReadOnly Property ClassName As String
        Get
            Return _className
        End Get
    End Property
    Public ReadOnly Property NextLevel As Double
        Get
            Return _xplevel(_currentLevel)
        End Get
    End Property
    ReadOnly Property iscaster As Boolean
        Get
            Return _IsMagicUser
        End Get
    End Property
    ReadOnly Property animstr As String
        Get
            Return _animstr
        End Get
    End Property
    ReadOnly Property speedbonus As Double
        Get
            Return _speedbonus
        End Get
    End Property
    ReadOnly Property atkrangebonus As Double
        Get
            Return _atkrangebonus
        End Get
    End Property
    ReadOnly Property resistbonus As Double
        Get
            Return _resistbonus
        End Get
    End Property
    ReadOnly Property magicbonus As Double
        Get
            Return _magicbonus
        End Get
    End Property
    ReadOnly Property defensebonus As Double
        Get
            Return _defensebonus
        End Get
    End Property
    ReadOnly Property atkbonus As Double
        Get
            Return _atkbonus
        End Get
    End Property
    ReadOnly Property mpbonus As Double
        Get
            Return _mpbonus
        End Get
    End Property
    ReadOnly Property hpbonus As Double
        Get
            Return _hpbonus
        End Get
    End Property
End Class
