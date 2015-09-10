Option Explicit On
Option Infer Off
Option Strict On
Imports System.Data
'Imports System.Data.OleDb
Imports System.Data.SQLite

Public Class main 'the default form.
    'templates
    Public MobTemplate() As Character 'the mob templates
    Public EquipsTemplate() As equipment
    Public NPCTemplate() As NPC
    Public PCTemplate() As Character 'the templates to pop in.
    Public Spelltemplate() As ability
    Public worldmaptemplate(,,) As WorldMap 'x,y,zone zone 0=null zone 1=building maps zone 2=forest area

    Public Party(3) As Character 'PC party
    Public monsterParty(3) As Character
    Public BattleSprites(7) As PictureBox
    Public MapSprites(12) As WorldMapSprites
    Public NPCMapSprites(12) As WorldMapSprites
    Public Inventory(254) As equipment
    Public TotalMoney As Double = 0
    Public SQLdb As String = "Data Source=Data\Characters.db"

    Public CurrentWorldMap As WorldMap
    Public battleBackground As Image
    Public GameStarted As Boolean = False
    Public escmenubool As Boolean = False
    Public MovementGrid(23, 23) As Point 'the grid everything moves on. since everything will be 24x24 the points will be 4 down and 4 to the right of each upper left corner.
    Public destination As globalCordinate
    Public MapSpriteLastAnim As Integer = 0
    Public InBattle As Boolean = False
    Public hasStarted As Boolean = False
    Public GameOver As Boolean = False
    Public Smooth As Boolean = True
    Public splash As SplashScreen = DirectCast(My.Application.SplashScreen, SplashScreen)
    Public PauseAll As Boolean = False
    Public CurrentShownPartyMember As Integer = 0

    Public NPCTalk As Label

    Public menuEsc As escMenu
    Public InvenScreen As InventoryScreen
    Public stats As charStats
    Public TalkMenu As NPCmenu
    Public playerAction As ActionMenu
    Public EnemyInfo As mobStats
    Public NPCshop As Shop

    Public SaveEnable As Boolean = False
    Public LoadEnable As Boolean = False
    Public loading As Boolean = False
    Public CloseEnable As Boolean = False

    Private Sub main_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If Not playerAction Is Nothing Then
            playerAction.Dispose()
        End If
        If Not menuEsc Is Nothing Then
            menuEsc.Dispose()
        End If
        If Not InvenScreen Is Nothing Then
            InvenScreen.Dispose()
        End If
        If Not stats Is Nothing Then
            stats.Dispose()
        End If
        If Not TalkMenu Is Nothing Then
            TalkMenu.Dispose()
        End If
        If Not EnemyInfo Is Nothing Then
            EnemyInfo.Dispose()
        End If
        If Not NPCshop Is Nothing Then
            NPCshop.Dispose()
        End If
    End Sub
    'variables to set the screens back





    Private Sub map_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If IO.File.Exists("data\characters.db") Then
            Me.Size = New Size(1040, 806)
            splash.Invoke(New MethodInvoker(AddressOf splash.setProgress))
            'testimage = Image.FromFile("Art\CharacterPlaceholder.png")
            Dim a As Integer = 260
            Dim b As Integer = 4
            Dim k As Integer = 0
            For i As Integer = 0 To 23 Step 1
                For j As Integer = 0 To 23 Step 1
                    MovementGrid(i, j) = New Point(a, b)
                    'Dim test As New Label
                    'test.Text = i & " " & j
                    'test.Font = New Font("Microsoft Sans Serif", 5)
                    'test.Size = New Size(24, 24)
                    'test.Location = MovementGrid(i, j)
                    'Me.Controls.Add(test)
                    k += 1
                    b += 32
                Next
                b = 4
                a += 32
            Next
            splash.Invoke(New MethodInvoker(AddressOf splash.setProgress))
            initializeMobTemplate()
            splash.Invoke(New MethodInvoker(AddressOf splash.setProgress))
            splash.Invoke(New MethodInvoker(AddressOf splash.setProgress))
            Dim BSP As Point = New Point(304, 350)
            For i As Integer = 0 To 7 Step 1
                BattleSprites(i) = New PictureBox
                If i <= 3 Then
                    BattleSprites(i).Location = New Point(BSP.X, BSP.Y + (i * 54))
                Else
                    BattleSprites(i).Location = New Point(BSP.X + 585, BSP.Y + ((i - 4) * 54))
                End If
                BattleSprites(i).Size = New Size(48, 48)
                BattleSprites(i).Visible = False
                BattleSprites(i).BackColor = Color.Transparent
                Me.Controls.Add(BattleSprites(i))
            Next
            splash.Invoke(New MethodInvoker(AddressOf splash.setProgress))
            splash.Invoke(New MethodInvoker(AddressOf splash.setProgress))
            initializeMapTemplates()
            splash.Invoke(New MethodInvoker(AddressOf splash.setProgress))
            splash.Invoke(New MethodInvoker(AddressOf splash.setProgress))
            If Not IO.Directory.Exists("save") Then
                Try
                    IO.Directory.CreateDirectory("save")

                Catch
                    MessageBox.Show("You don't have permission to save.")
                End Try
            End If
            splash.Invoke(New MethodInvoker(AddressOf splash.setProgress))
            splash.Invoke(New MethodInvoker(AddressOf splash.setProgress))
            initializePCtemplate()
            splash.Invoke(New MethodInvoker(AddressOf splash.setProgress))
            splash.Invoke(New MethodInvoker(AddressOf splash.setProgress))
            splash.Invoke(New MethodInvoker(AddressOf splash.setProgress))
            splash.Invoke(New MethodInvoker(AddressOf splash.setProgress))
            initializeNPCtemplate()
            splash.Invoke(New MethodInvoker(AddressOf splash.setProgress))
            splash.Invoke(New MethodInvoker(AddressOf splash.setProgress))
            initializeEquiptemplate()
            splash.Invoke(New MethodInvoker(AddressOf splash.setProgress))
            splash.Invoke(New MethodInvoker(AddressOf splash.setProgress))
            initializeAbilitytemplate()
            splash.Invoke(New MethodInvoker(AddressOf splash.setProgress))
            splash.Invoke(New MethodInvoker(AddressOf splash.setProgress))
            ' createMenus()
            For i As Integer = 0 To Inventory.GetUpperBound(0) Step 1
                EquipsTemplate(0).CopyTo(Inventory(i))
            Next
            For i As Integer = 0 To 12 Step 1
                MapSprites(i) = New WorldMapSprites
                NPCMapSprites(i) = New WorldMapSprites
            Next
            playerAction = New ActionMenu
            splash.Invoke(New MethodInvoker(AddressOf splash.setProgress))
            TotalMoney = 0
            LoadEnable = IO.File.Exists("save\0.save")
        Else
            MsgBox("uh oh you are missing the database")
            Me.Close()
        End If

        'MsgBox("i exist")
    End Sub

    Private Sub main_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        escMenushow()
    End Sub
    Public Sub escMenushow()
        Me.KeyPreview = False
        menuEsc = New escMenu
        menuEsc.Show()
    End Sub
    Private Sub createMenus()
        'If menuEsc Is Nothing Then
        '    menuEsc = New escMenu()
        'End If
        'If TalkMenu Is Nothing Then
        '    TalkMenu = New NPCmenu()
        'End If
        'If InvenScreen Is Nothing Then
        '    InvenScreen = New InventoryScreen()
        'End If
        'If stats Is Nothing Then
        '    stats = New charStats()
        'End If
        'If EnemyInfo Is Nothing Then
        '    EnemyInfo = New mobStats()
        'End If
        'If NPCshop Is Nothing Then
        '    NPCshop = New Shop()
        'End If




    End Sub
    Private Sub initializeMobTemplate()
        ReDim MobTemplate(GetTableSize("Evil") - 1)
        Dim tempspelllist As String
        Dim argSpellList(4) As Integer
        Using cn As New SQLiteConnection(SQLdb)
            cn.Open()
            For i As Integer = 0 To MobTemplate.GetUpperBound(0) Step 1
                tempspelllist = GetTableSTREntry("Evil", (i + 1).ToString, "spellList", cn)
                If tempspelllist = "00" Then
                    ReDim argSpellList(0)
                    argSpellList(0) = 0
                Else
                    ReDim argSpellList(CInt(tempspelllist.Length / 2) - 1)
                    Dim k As Integer = 0
                    For j As Integer = 0 To (tempspelllist.Length - 1) Step 2
                        Dim s As String = tempspelllist.Substring(j, 2)
                        Integer.TryParse(s, argSpellList(k))
                        k += 1
                    Next
                End If
                MobTemplate(i) = New Character(GetTableSTREntry("Evil", (i + 1).ToString, "artDir", cn), GetTableDBLEntry("Evil", (i + 1).ToString, "atk", cn), GetTableDBLEntry("Evil", (i + 1).ToString, "def", cn), GetTableSTREntry("Evil", (i + 1).ToString, "type", cn), 1, True, GetTableBOOLEntry("Evil", (i + 1).ToString, "IsCaster", cn), GetTableDBLEntry("Evil", (i + 1).ToString, "hpmax", cn), GetTableDBLEntry("Evil", (i + 1).ToString, "mpmax", cn), GetTableDBLEntry("Evil", (i + 1).ToString, "hpmin", cn), GetTableDBLEntry("Evil", (i + 1).ToString, "mpmin", cn), GetTableDBLEntry("Evil", (i + 1).ToString, "resist", cn), GetTableDBLEntry("Evil", (i + 1).ToString, "mgk", cn), GetTableDBLEntry("Evil", (i + 1).ToString, "exp", cn), GetTableDBLEntry("Evil", (i + 1).ToString, "money", cn), GetTableDBLEntry("Evil", (i + 1).ToString, "speed", cn), GetTableDBLEntry("Evil", (i + 1).ToString, "damageRange", cn), -1, GetTableDBLEntry("Evil", (i + 1).ToString, "hpbonus", cn), GetTableDBLEntry("Evil", (i + 1).ToString, "mpbonus", cn), GetTableDBLEntry("Evil", (i + 1).ToString, "atkbonus", cn), GetTableDBLEntry("Evil", (i + 1).ToString, "defbonus", cn), GetTableDBLEntry("Evil", (i + 1).ToString, "mgkbonus", cn), GetTableDBLEntry("Evil", (i + 1).ToString, "resistbonus", cn), GetTableDBLEntry("Evil", (i + 1).ToString, "damageRangeBonus", cn), GetTableDBLEntry("Evil", (i + 1).ToString, "speedbonus", cn), GetTableSTREntry("Evil", (i + 1).ToString, "type", cn), argSpellList)
            Next
            cn.Close()
        End Using
    End Sub
    Private Sub initializeMapTemplates() 'piece of fucking shit took over 4 hours

        Using cn As New SQLiteConnection(SQLdb)
            cn.Open()
            Dim s4 As String = ""
            Dim i1 As Integer = 0
            Dim i2 As Integer = 0
            Dim i3 As Integer = 0
            Dim i4 As Integer = 0
            Dim i5 As Integer = 0
            ReDim worldmaptemplate(99, 99, 99)
            Dim destlist() As globalCordinate
            For i As Integer = 0 To GetTableSize("Map") - 1 Step 1
                s4 = GetTableSTREntry("Map", (i + 1).ToString, "gcs", cn)
                ReDim destlist(CInt((s4.Length) / 12) - 1)
                Dim k As Integer = 0
                If s4.Length <> 2 Then
                    For j As Integer = 0 To s4.Length - 1 Step 12
                        If j < s4.Length Then
                            i1 = CInt(s4.Substring(j, 2))
                            i2 = CInt(s4.Substring(j + 2, 2))
                            i3 = CInt(s4.Substring(j + 4, 2))
                            i4 = CInt(s4.Substring(j + 6, 2))
                            i5 = CInt(s4.Substring(j + 8, 2))
                            destlist(k) = New globalCordinate(i1, i2, i3, i4, i5) 'i dont understand why i4, i5 had to be switched and i2,i3 had to be switched too.
                            k += 1
                        End If
                    Next
                    worldmaptemplate(GetTableINTEntry("Map", (i + 1).ToString, "thezone", cn), GetTableINTEntry("Map", (i + 1).ToString, "areax", cn), GetTableINTEntry("Map", (i + 1).ToString, "areay", cn)) = New WorldMap(destlist, MovementGrid, GetTableSTREntry("Map", (i + 1).ToString, "thezone", cn) & GetTableSTREntry("Map", (i + 1).ToString, "areax", cn) & GetTableSTREntry("Map", (i + 1).ToString, "areay", cn), GetTableSTREntry("Map", (i + 1).ToString, "npclist", cn))
                Else
                    worldmaptemplate(GetTableINTEntry("Map", (i + 1).ToString, "thezone", cn), GetTableINTEntry("Map", (i + 1).ToString, "areax", cn), GetTableINTEntry("Map", (i + 1).ToString, "areay", cn)) = New WorldMap(MovementGrid, GetTableSTREntry("Map", (i + 1).ToString, "thezone", cn) & GetTableSTREntry("Map", (i + 1).ToString, "areax", cn) & GetTableSTREntry("Map", (i + 1).ToString, "areay", cn), GetTableSTREntry("Map", (i + 1).ToString, "npclist", cn))
                End If



            Next

            cn.Close()
        End Using

    End Sub
    Private Sub initializePCtemplate()
        ReDim PCTemplate(GetTableSize("Good") - 1)
        Using cn As New SQLiteConnection(SQLdb)
            cn.Open()
            For i As Integer = 0 To PCTemplate.GetUpperBound(0) Step 1
                Dim tempspelllist As String = GetTableSTREntry("Good", (i + 1).ToString, "spellList", cn)
                Dim argSpellList() As Integer
                If tempspelllist = "00" Then
                    ReDim argSpellList(0)
                    argSpellList(0) = 0
                Else
                    ReDim argSpellList(CInt(tempspelllist.Length / 2) - 1)
                    Dim k As Integer = 0
                    For j As Integer = 0 To (tempspelllist.Length - 1) Step 2
                        Dim s As String = tempspelllist.Substring(j, 2)
                        Integer.TryParse(s, argSpellList(k))
                        k += 1
                    Next
                End If

                PCTemplate(i) = New Character(GetTableSTREntry("Good", (i + 1).ToString, "artDir", cn), GetTableDBLEntry("Good", (i + 1).ToString, "atk", cn), GetTableDBLEntry("Good", (i + 1).ToString, "def", cn), GetTableSTREntry("Good", (i + 1).ToString, "type", cn), 0, False, GetTableBOOLEntry("Good", (i + 1).ToString, "IsCaster", cn), GetTableDBLEntry("Good", (i + 1).ToString, "hp", cn), GetTableDBLEntry("Good", (i + 1).ToString, "mp", cn), 0, 0, GetTableDBLEntry("Good", (i + 1).ToString, "resist", cn), GetTableDBLEntry("Good", (i + 1).ToString, "mgk", cn), 0, GetTableDBLEntry("Good", (i + 1).ToString, "money", cn), GetTableDBLEntry("Good", (i + 1).ToString, "speed", cn), GetTableDBLEntry("Good", (i + 1).ToString, "damageRange", cn), i, GetTableDBLEntry("Good", (i + 1).ToString, "hpbonus", cn), GetTableDBLEntry("Good", (i + 1).ToString, "mpbonus", cn), GetTableDBLEntry("Good", (i + 1).ToString, "atkbonus", cn), GetTableDBLEntry("Good", (i + 1).ToString, "defbonus", cn), GetTableDBLEntry("Good", (i + 1).ToString, "mgkbonus", cn), GetTableDBLEntry("Good", (i + 1).ToString, "resistbonus", cn), GetTableDBLEntry("Good", (i + 1).ToString, "damageRangeBonus", cn), GetTableDBLEntry("Good", (i + 1).ToString, "speedbonus", cn), GetTableSTREntry("Good", (i + 1).ToString, "type", cn), argSpellList)
            Next
            cn.Close()
        End Using
    End Sub

    Private Sub initializeNPCtemplate()
        ReDim NPCTemplate(GetTableSize("Neutral") - 1)
        Using cn As New SQLiteConnection(SQLdb)
            cn.Open()
            For i As Integer = 0 To NPCTemplate.GetUpperBound(0) Step 1
                NPCTemplate(i) = New NPC(GetTableSTREntry("Neutral", (i + 1).ToString, "type", cn), GetTableSTREntry("Neutral", (i + 1).ToString, "dialogue", cn), GetTableBOOLEntry("Neutral", (i + 1).ToString, "shopkeeper", cn), GetTableBOOLEntry("Neutral", (i + 1).ToString, "innkeeper", cn), GetTableSTREntry("Neutral", (i + 1).ToString, "Inventory", cn), GetTableSTREntry("Neutral", (i + 1).ToString, "artfolder", cn))
            Next
            cn.Close()
        End Using
    End Sub

    Private Sub initializeEquiptemplate()
        ReDim EquipsTemplate(GetTableSize("Equipment"))
        Using cn As New SQLiteConnection(SQLdb)
            cn.Open()
            EquipsTemplate(0) = New equipment("Nothing", 0, 0, 0, 0, 0, 0, 0, 0, -1, False, False)

            For i As Integer = 1 To EquipsTemplate.GetUpperBound(0) Step 1
                EquipsTemplate(i) = New equipment(GetTableSTREntry("Equipment", (i).ToString, "type", cn), i, GetTableDBLEntry("Equipment", (i).ToString, "atk", cn), GetTableDBLEntry("Equipment", (i).ToString, "def", cn), GetTableDBLEntry("Equipment", (i).ToString, "speed", cn), GetTableDBLEntry("Equipment", (i).ToString, "mgk", cn), GetTableDBLEntry("Equipment", (i).ToString, "resist", cn), GetTableDBLEntry("Equipment", (i).ToString, "cost", cn), GetTableDBLEntry("Equipment", (i).ToString, "damageRange", cn), GetTableINTEntry("Equipment", (i).ToString, "slot", cn), GetTableBOOLEntry("Equipment", (i).ToString, "handed2", cn), GetTableBOOLEntry("Equipment", (i).ToString, "consumable", cn))
            Next
            cn.Close()
        End Using
    End Sub

    Private Sub initializeAbilitytemplate()
        ReDim Spelltemplate(GetTableSize("Abilities"))
        Using cn As New SQLiteConnection(SQLdb)
            cn.Open()
            Spelltemplate(0) = New ability("Null", 0, 0, 0, False) ' if the ability corosponds with slot 0 it goes OH CRAP! lol
            For i As Integer = 1 To Spelltemplate.GetUpperBound(0) Step 1 'todo adding spells.
                Spelltemplate(i) = New ability(GetTableSTREntry("Abilities", (i).ToString, "spellname", cn), GetTableDBLEntry("Abilities", (i).ToString, "cost", cn), GetTableDBLEntry("Abilities", (i).ToString, "damageRange", cn), GetTableDBLEntry("Abilities", (i).ToString, "statmod", cn), GetTableBOOLEntry("Abilities", (i).ToString, "healspell", cn))
            Next
            cn.Close()
        End Using
    End Sub

    Public Sub NewGame(ByVal argLoading As Boolean)
        GameStarted = True
        GameOver = False
        InBattle = False
        Me.KeyPreview = True
        If menuEsc.Visible = True Then
            menuEsc.Visible = False
        End If
 
        ' CurrentWorldMap.ShowLables()
        'If Not hasStarted Then
        '    Me.Controls.Add(MapSprites(0))
        'End If
        If argLoading Then
            For i As Integer = 0 To 3 Step 1
                If hasStarted Then
                    Party(i).removeControls()
                End If
                Party(i).convertFromTemplate(i)
                TotalMoney += Party(i).Money
            Next
        Else
            destination = New globalCordinate()
            CurrentWorldMap = worldmaptemplate(0, 0, 0)
            NextArea(New Point(1, 1), 3, 0)
            MapSprites(0).Active = True
            'MapSprites(0).BringToFront()
            MapSprites(0).Location = destination.xyPair
            MapSprites(0).MoveImage()
            For i As Integer = 0 To 3 Step 1
                If hasStarted Then
                    Party(i).removeControls()
                End If
                PCTemplate(i).CopyTo(Party(i), False)
                Dim temps As String = ""
                Do While temps.Equals("")
                    temps = InputBox("Please Name your " & Party(i).ClassName, "Name your character", Party(i).ClassName)
                    If temps.Length > 16 Then
                        temps = ""
                        MsgBox("Your name can only be 16 characters long", MsgBoxStyle.OkOnly, "Naming Error")
                    End If
                Loop
                Party(i).charname = temps
                Party(i).convertFromTemplate(i)
                TotalMoney += Party(i).Money
            Next
            For i As Integer = 0 To Inventory.GetUpperBound(0) Step 1
                Inventory(i) = EquipsTemplate(0)
            Next
            CurrentShownPartyMember = 0
        End If

        MapSprites(0).Sprite = Party(0).getimage(3, 1)
        For i As Integer = 0 To 7 Step 1
            BattleSprites(i).Visible = False
        Next

        hasStarted = True
        timAnim.Start()
        timOnster.Start()
        SaveEnable = True
        loading = False
        CloseEnable = True
    End Sub
    Public Function Battle(ByVal argMobIndex As Integer) As Integer 'return 0 = win 1 = run 2 = loss 3 forcequit

        InBattle = True
        Me.BackgroundImage = battleBackground
        Dim monstermemory(12) As Boolean
        Dim tempdamage As Double = 0
        GenerateEnemyParty(argMobIndex)
        For i As Integer = 0 To 12 Step 1
            monstermemory(i) = MapSprites(i).Active
            MapSprites(i).Active = False
        Next
        Dim BattleResult As Integer = 0
        ' Dim battleOver As Boolean = True
        Dim run As Boolean = False
        Dim initiative(7) As Integer 'slots 0-3 are PC 4-7 are mob
        Dim initiativeOrder(7) As Integer
        Dim targ As Integer = 0
        Dim ability As Integer = 0
        Dim CurrentPlayer As Integer = 0

        For i As Integer = 0 To 8 Step 1
            For j As Integer = 1 To 7 Step 1
                If i = 0 Then
                    If j <= 3 Then
                        initiative(j) = Party(j).Initiative
                    Else
                        initiative(j) = monsterParty(j - 4).Initiative
                    End If
                    initiativeOrder(j) = j
                Else
                    If initiative(initiativeOrder(j)) > initiative(initiativeOrder(j - 1)) Then 'thank you c++ for the idea to sort the index instead of the actual information
                        Dim temp As Integer = initiativeOrder(j)
                        initiativeOrder(j) = initiativeOrder(j - 1)
                        initiativeOrder(j - 1) = temp
                    End If
                End If

            Next
        Next
        For i As Integer = 0 To 7 Step 1
            If i <= 3 Then
                BattleSprites(i).Image = Party(i).getimage(4, MapSpriteLastAnim)
                If Not Party(i).dead Then
                    BattleSprites(i).Visible = True
                End If
            Else
                BattleSprites(i).Image = monsterParty(i - 4).getimage(4, MapSpriteLastAnim)
                If Not monsterParty(i - 4).dead Then
                    BattleSprites(i).Visible = True
                End If
            End If


        Next
        Do While InBattle
            If (monsterParty(0).dead AndAlso monsterParty(1).dead AndAlso monsterParty(2).dead AndAlso monsterParty(3).dead) Then

                Dim exp As Double = 0
                Dim money As Double = 0
                Dim leftalive As Integer = 0
                Dim temps As String = ""
                For i As Integer = 0 To 3 Step 1
                    exp += monsterParty(i).xp
                    money += monsterParty(i).Money
                    If Not Party(i).dead Then
                        leftalive += 1
                    End If
                Next
                For i As Integer = 0 To 3 Step 1
                    If Not Party(i).dead Then
                        Party(i).AddXP((exp / leftalive))
                        temps += Party(i).charname & " got " & (exp / leftalive).ToString & " exp" & ControlChars.NewLine
                    End If
                Next


                MsgBox(temps & "and you recieved " & money.ToString("c2"), MsgBoxStyle.OkOnly, "Rewards")
                TotalMoney += money
                PostBattle(monstermemory)
                NextAvailiblePartyMember()
                Return 0
            ElseIf (Party(0).dead AndAlso Party(1).dead AndAlso Party(2).dead AndAlso Party(3).dead) Then
                MsgBox("you lose")
                Return 1
            ElseIf run Then
                MsgBox("you ran")
                PostBattle(monstermemory)
                NextAvailiblePartyMember()
                Return 2
            End If
            If InBattle Then
                For i As Integer = 0 To 7 Step 1
                    If Not BypassTurn(run) Then

                        If initiativeOrder(i) <= 3 Then
                            If Not Party(initiativeOrder(i)).dead Then
                                Dim actionComplete As Boolean = False
                                CurrentPlayer = initiativeOrder(i)
                                Do While (Not actionComplete) And InBattle
                                    playerAction = New ActionMenu
                                    playerAction.player = CurrentPlayer
                                    playerAction.Setname(Party(CurrentPlayer).charname)
                                    ' playerAction.RefreshMenu()
                                    Dim actionresult As DialogResult = playerAction.ShowDialog()
                                    If actionresult = Windows.Forms.DialogResult.Cancel Then
                                        Return 3
                                    End If
                                    targ = playerAction.Target 'target 0-7 0-3= player 4-7 = monster
                                    ability = playerAction.ability 'index of the spell list on the caster
                                    If actionresult = Windows.Forms.DialogResult.Yes Then 'player chose to attack
                                        If Spelltemplate(Party(CurrentPlayer).spellList(playerAction.ability)).HealSpell Then
                                            If Not Party(targ).dead Then
                                                tempdamage = Party(CurrentPlayer).Attack()
                                                Party(targ).hp -= Party(targ).Defend(tempdamage)
                                                MsgBox(Party(CurrentPlayer).charname & " attacks " & Party(targ).charname & " for " & Party(targ).Defend(tempdamage) & " Damage")
                                                If Party(targ).dead Then
                                                    BattleSprites(targ).Visible = False
                                                    MsgBox(Party(targ).charname & " Died.", MsgBoxStyle.OkOnly, "Dood died")

                                                End If
                                                actionComplete = True
                                            End If
                                        Else
                                            If Not monsterParty(targ).dead Then
                                                tempdamage = Party(CurrentPlayer).Attack()
                                                monsterParty(targ).hp -= monsterParty(targ).Defend(tempdamage)
                                                MsgBox(Party(CurrentPlayer).charname & " attacks " & monsterParty(targ).charname & " for " & monsterParty(targ).Defend(tempdamage) & " Damage")
                                                If monsterParty(targ).dead Then
                                                    BattleSprites(targ).Visible = False
                                                    MsgBox(monsterParty(targ).charname & " Died.", MsgBoxStyle.OkOnly, "Dood died")
                                                End If
                                                actionComplete = True
                                            End If
                                        End If
                                    ElseIf actionresult = Windows.Forms.DialogResult.OK Then 'player chose to cast a spell
                                        If targ <= 3 Then
                                            If Not Party(targ).dead Then
                                                If Party(CurrentPlayer).Cast(Spelltemplate(Party(CurrentPlayer).spellList(ability)), tempdamage) Then
                                                    Party(targ).hp -= Party(targ).Resistance(tempdamage, Spelltemplate(Party(CurrentPlayer).spellList(ability)).HealSpell)
                                                    MsgBox(Party(CurrentPlayer).charname & " Casts " & Spelltemplate(Party(CurrentPlayer).spellList(ability)).name & " at " & Party(targ).charname & " for " & Party(targ).Resistance(tempdamage, Spelltemplate(Party(CurrentPlayer).spellList(ability)).HealSpell) & " Damage")
                                                    If Party(targ).dead Then
                                                        BattleSprites(targ).Visible = False
                                                        MsgBox(Party(targ).charname & " Died.", MsgBoxStyle.OkOnly, "Dood died")
                                                    End If
                                                    actionComplete = True
                                                End If

                                            End If
                                        Else
                                            If Not monsterParty(targ).dead Then
                                                If Party(CurrentPlayer).Cast(Spelltemplate(Party(CurrentPlayer).spellList(ability)), tempdamage) Then
                                                    monsterParty(targ).hp -= monsterParty(targ).Resistance(tempdamage, Spelltemplate(Party(CurrentPlayer).spellList(ability)).HealSpell)
                                                    MsgBox(Party(CurrentPlayer).charname & " Casts " & Spelltemplate(Party(CurrentPlayer).spellList(ability)).name & " at " & monsterParty(targ).charname & " for " & monsterParty(targ).Resistance(tempdamage, Spelltemplate(Party(CurrentPlayer).spellList(ability)).HealSpell) & " Damage")
                                                    If monsterParty(targ).dead Then
                                                        BattleSprites(targ).Visible = False
                                                        MsgBox(monsterParty(targ).charname & " Died.", MsgBoxStyle.OkOnly, "Dood died")
                                                    End If
                                                    actionComplete = True
                                                End If
                                            End If
                                        End If
                                    ElseIf actionresult = Windows.Forms.DialogResult.Abort Then 'player chose to do nothing
                                        actionComplete = True
                                    ElseIf actionresult = Windows.Forms.DialogResult.Retry Then 'player chose to run
                                        Dim rand As New Random
                                        If rand.Next(0, 100) <= 24 Then
                                            run = True
                                        Else
                                            MsgBox("You failed to get away")
                                        End If
                                        actionComplete = True
                                    End If

                                Loop
                            End If
                        Else
                            If Not monsterParty(initiativeOrder(i) - 4).dead Then
                                Ai(initiativeOrder(i) - 4)
                            End If
                        End If
                    End If
                Next
            End If
        Loop

        MsgBox("Battle Over")
        Return 0
    End Function
    Private Function BypassTurn(ByVal argrun As Boolean) As Boolean
        If monsterParty(0).dead And monsterParty(1).dead And monsterParty(2).dead And monsterParty(3).dead Then
            Return True
        End If
        If Party(0).dead And Party(1).dead And Party(2).dead And Party(3).dead Then
            Return True
        End If
        If argrun Then
            Return True
        End If
        Return False
    End Function
    Private Sub Ai(ByVal argMob As Integer)
        Dim rand As New Random
        Dim target As Integer = 0
        Dim j As Integer
        Dim i As Integer = 0
        Dim TempDamage As Double = 0
        Dim ActionComplete As Boolean = False
        Do While Not ActionComplete
            i = rand.Next(0, 100)
            If i >= 90 Then
                target = 3
            ElseIf i >= 80 Then
                target = 1
            ElseIf i >= 50 Then
                target = 2
            Else
                target = 0
            End If
            If Not Party(target).dead Then
                If monsterParty(argMob).mp = 0 Or monsterParty(argMob).spellList(0) = 0 Then
                    TempDamage = Party(target).Defend(monsterParty(argMob).Attack())
                    Party(target).hp -= TempDamage
                    MsgBox(monsterParty(argMob).charname & " attacks " & Party(target).charname & " for " & TempDamage & " Damage")
                    If Party(target).dead Then
                        MsgBox(Party(target).charname & " Died.", MsgBoxStyle.OkOnly, "Dood died")
                        BattleSprites(target).Visible = False
                    End If
                    ActionComplete = True
                Else
                    If rand.Next(0, 100) <= 25 Then
                        TempDamage = Party(target).Defend(monsterParty(argMob).Attack())
                        Party(target).hp -= TempDamage
                        MsgBox(monsterParty(argMob).charname & " attacks " & Party(target).charname & " for " & TempDamage & " Damage")
                        If Party(target).dead Then
                            MsgBox(Party(target).charname & " Died.", MsgBoxStyle.OkOnly, "Dood died")
                            BattleSprites(target).Visible = False
                        End If
                        ActionComplete = True
                    Else
                        If monsterParty(argMob).spellList.GetUpperBound(0) = 1 Then
                            j = rand.Next(0, monsterParty(argMob).spellList.GetUpperBound(0) + 1)
                        Else
                            j = monsterParty(argMob).spellList(0)
                        End If
                        If monsterParty(argMob).Cast(Spelltemplate(j), TempDamage) Then
                            TempDamage = Party(target).Resistance(TempDamage, False)
                            Party(target).hp -= TempDamage
                            MsgBox(monsterParty(argMob).charname & " Casts " & Spelltemplate(j).name & " at " & Party(target).charname & " for " & TempDamage & " Damage")
                        Else
                            TempDamage = Party(target).Defend(monsterParty(argMob).Attack())
                            Party(target).hp -= TempDamage
                            MsgBox(monsterParty(argMob).charname & " attacks " & Party(target).charname & " for " & TempDamage & " Damage")
                        End If
                        If Party(target).dead Then
                            MsgBox(Party(target).charname & " Died.", MsgBoxStyle.OkOnly, "Dood died")
                            BattleSprites(target).Visible = False
                        End If
                        ActionComplete = True
                    End If
                End If
            Else
                If Party(0).dead And Party(1).dead And Party(2).dead And Party(3).dead Then
                    ActionComplete = True
                End If
            End If
        Loop

    End Sub
    Public Sub NextAvailiblePartyMember()
        Dim count As Integer = 0
        Do While Party(CurrentShownPartyMember).dead And count <= 3
            If CurrentShownPartyMember >= 3 Then
                CurrentShownPartyMember = 0
            Else
                CurrentShownPartyMember += 1
            End If

            count += 1 'a check to make sure it doesnt loop forever if all party members die
        Loop


    End Sub
    Private Sub GenerateEnemyParty(ByVal argMobIndex As Integer)
        Dim level As Integer = CInt((Party(0).currentlevel + Party(1).currentlevel + Party(2).currentlevel + Party(3).currentlevel) / 4)

        For i As Integer = 0 To 3 Step 1

            MobTemplate(argMobIndex).CopyTo(monsterParty(i), False)
            monsterParty(i).convertFromTemplate(i)
            monsterParty(i).charname += (i + 1).ToString
            If level > 1 Then
                monsterParty(i).LevelUp(level - 1)
            End If
            'argMonsterParty(i).LevelUp(level)
        Next i

    End Sub
    Public Sub PostBattle(ByRef argMemory() As Boolean)
        For i As Integer = 0 To 12 Step 1
            MapSprites(i).Active = argMemory(i)
        Next
        For i As Integer = 0 To 7 Step 1
            BattleSprites(i).Visible = False
        Next
        InBattle = False
        Me.BackgroundImage = CurrentWorldMap.map
        'InvenScreen.EndBattle()
    End Sub
    Private Function GetTableSize(ByVal argTablename As String) As Integer
        'Using cn As New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Data\Characters.accdb")
        '    cn.Open()
        '    Dim cmd As New OleDb.OleDbCommand("Select COUNT(*) FROM " & argTablename, cn)
        '    ' MsgBox(CStr(cmd.ExecuteScalar()))
        '    Return CInt(cmd.ExecuteScalar())
        'End Using

        Using cn As New SQLiteConnection(SQLdb)
            cn.Open()
            Dim cmd As New SQLiteCommand
            cmd = cn.CreateCommand()
            cmd.CommandText = "Select COUNT(*) FROM " & argTablename
            ' MsgBox(CStr(cmd.ExecuteScalar()))
            Return CInt(cmd.ExecuteScalar())
        End Using
    End Function
    'Private Function GetTableDBLEntry(ByVal argTablename As String, ByVal argRow As String, ByVal argColumn As String, ByRef argConnection As OleDbConnection) As Double
    '    Dim cmd As New OleDb.OleDbCommand("Select " & argColumn & " FROM " & argTablename & " WHERE ID=" & argRow, argConnection)
    '    Return CDbl(cmd.ExecuteScalar())
    'End Function
    Public Shared Function GetTableDBLEntry(ByVal argTablename As String, ByVal argRow As String, ByVal argColumn As String, ByRef argConnection As SQLiteConnection) As Double
        Dim cmd As New SQLiteCommand
        Try
            cmd = argConnection.CreateCommand()
            cmd.CommandText = "Select " & argColumn & " FROM " & argTablename & " WHERE ID=" & argRow
        Catch
            Return 0
        End Try
        Return CDbl(cmd.ExecuteScalar())
    End Function
    Public Shared Function GetTableINTEntry(ByVal argTablename As String, ByVal argRow As String, ByVal argColumn As String, ByRef argConnection As SQLiteConnection) As Integer
        Dim cmd As New SQLiteCommand
        Try
            cmd = argConnection.CreateCommand()
            cmd.CommandText = "Select " & argColumn & " FROM " & argTablename & " WHERE ID=" & argRow
        Catch
            Return 0
        End Try
        Return CInt(cmd.ExecuteScalar())
    End Function
    Public Shared Function GetTableBOOLEntry(ByVal argTablename As String, ByVal argRow As String, ByVal argColumn As String, ByRef argConnection As SQLiteConnection) As Boolean
        Dim cmd As New SQLiteCommand
        Try
            cmd = argConnection.CreateCommand()
            cmd.CommandText = "Select " & argColumn & " FROM " & argTablename & " WHERE ID=" & argRow
        Catch
            Return False
        End Try
        Dim r As String = CStr(cmd.ExecuteScalar())
        If r.ToUpper = "Y" Then
            Return True
        End If
        Return False
    End Function
    Public Shared Function GetTableSTREntry(ByVal argTablename As String, ByVal argRow As String, ByVal argColumn As String, ByRef argConnection As SQLiteConnection) As String
        Dim cmd As New SQLiteCommand
        Try
            cmd = argConnection.CreateCommand()
            cmd.CommandText = "Select " & argColumn & " FROM " & argTablename & " WHERE ID=" & argRow
        Catch
            Return ""
        End Try

        Return CStr(cmd.ExecuteScalar())
    End Function

    Public Function InsertItemInToInventory(ByVal argItemIndex As Integer, ByVal argStack As Integer) As Integer '0 = worked fine 1 = full inventory 2 = item not found 99 you formulated the function improperly
        Dim temp As Integer = -1 'considering changing hte return type to int so i know what went wrong.
        Dim temp2 As Integer = 0
        If argItemIndex > 0 AndAlso argItemIndex <= EquipsTemplate.GetUpperBound(0) And argStack >= -99 And argStack <= 99 Then 'checks to make sure that we didnt get screwed up info
            If EquipsTemplate(argItemIndex).consumable Then 'checks if its equipment or consumables
                temp = GetNextInventorySlotOfIndex(argItemIndex) 'checks to see if it can find an item that it can stack with
                If argStack > 0 Then 'checks if the stack size is larger then 0 (buying or finding stuff)
                    If temp = -1 Then
                        temp = GetNextEmptyInventorySlot()
                        If temp = -1 Then ' if there are no stacks of the item and it has no inventory space left then it returns false
                            Return 1
                        Else
                            EquipsTemplate(argItemIndex).CopyTo(Inventory(temp))'yay its making a new stack
                            Inventory(temp).stack = argStack
                            Return 0
                        End If
                    Else
                        If (Inventory(temp).stack + argStack) > 99 Then 'if it DOES find a slot that has a stack inside but if you add argstack with the current ammount it goes over 99 
                            temp2 = argStack - Inventory(temp).stack 'then it just adds what it can to the stack and looks for a new slot.
                            Inventory(temp).stack = 99 'if it cant find a new slot it just discards all extras
                            temp = GetNextEmptyInventorySlot()
                            If temp = -1 Then
                                Return 1
                            Else
                                EquipsTemplate(argItemIndex).CopyTo(Inventory(temp))
                                Inventory(temp).stack = temp2
                                Return 0
                            End If
                        Else 'this items stack plus argstack dont go over 99 so it just adds it and ends the function
                            Inventory(temp).stack += argStack

                            Return 0
                        End If
                    End If
                ElseIf argStack < 0 Then 'this side is for using or selling items.
                    If temp > -1 Then
                        If Inventory(temp).stack + argStack <= 0 Then
                            EquipsTemplate(0).CopyTo(Inventory(temp))
                            Return 0
                        Else
                            Inventory(temp).stack += argStack
                            Return 0
                        End If
                    Else
                        Return 2
                    End If
                Else
                    Return 99
                End If
            Else 'this side isnt a consumable, used for equipment items .
                If argStack = 1 Then 'dequiping or buying
                    temp = GetNextEmptyInventorySlot()
                    If temp = -1 Then 'no empty slot
                        Return 1
                    Else 'yay empty slot
                        EquipsTemplate(argItemIndex).CopyTo(Inventory(temp))
                        Return 0
                    End If
                ElseIf argStack = -1 Then 'equiping or selling
                    temp = GetNextInventorySlotOfIndex(argItemIndex)
                    If temp = -1 Then 'item doesnt exist to get rid of
                        Return 2
                    Else 'item removed
                        EquipsTemplate(0).CopyTo(Inventory(temp))
                        Return 0
                    End If
                Else
                    Return 99
                End If

            End If

        Else
            Return 99
        End If
    End Function
    Public Function GetNextInventorySlotOfIndex(ByVal argTemplateIndex As Integer) As Integer
        For i As Integer = 0 To Inventory.GetUpperBound(0) Step 1
            If Inventory(i).index = argTemplateIndex Then
                Return i
            End If
        Next
        Return -1
    End Function
    Public Function GetNextEmptyInventorySlot() As Integer
        For i As Integer = 0 To Inventory.GetUpperBound(0) Step 1
            If Inventory(i).index = 0 Then
                Return i
            End If
        Next
        Return -1
    End Function
    Public Function SwapItemLocationsInInventory(ByVal argPosition1 As Integer, ByVal argPosition2 As Integer) As Boolean
        If argPosition1 >= 0 AndAlso argPosition1 <= Inventory.GetUpperBound(0) AndAlso argPosition2 >= 0 AndAlso argPosition2 <= Inventory.GetUpperBound(0) Then
            Dim temp As equipment = New equipment()
            Inventory(argPosition1).CopyTo(temp)
            Inventory(argPosition2).CopyTo(Inventory(argPosition1))
            temp.CopyTo(Inventory(argPosition2))
            Return True
        Else
            Return False
        End If

    End Function
    Public Function ConsumeItem(ByVal argPartyslot As Integer, ByVal argInventoryIndex As Integer) As Boolean 'using atk for hp and magic for mp to save on space.
        If argPartyslot >= 0 And argPartyslot <= 3 Then 'party slot check
            If Inventory(argInventoryIndex).index > 0 Then 'validating inventory index slot
                If Inventory(argInventoryIndex).consumable Then 'validating if its a consumable item
                    If Inventory(argInventoryIndex).handed2 Then 'dead check for phoenix down item
                        If Party(argPartyslot).dead Then 'using handed2 as the bool to determin if its a revive item to save space
                            If InsertItemInToInventory(Inventory(argInventoryIndex).index, -1) = 0 Then
                                Party(argPartyslot).dead = False
                                Party(argPartyslot).hp += Inventory(argInventoryIndex).atk
                                Party(argPartyslot).mp += Inventory(argInventoryIndex).magic
                                MsgBox(Party(argPartyslot).charname & " was revived from death")
                                Return True
                            End If
                        End If
                    Else
                        If Not Party(argPartyslot).dead Then
                            ' If Inventory(argInventoryIndex).atk > 0 Then
                            '  If Party(argPartyslot).hp < Party(argPartyslot).maxhp Or Party(argPartyslot).mp < Party(argPartyslot).maxmp Then
                            If InsertItemInToInventory(Inventory(argInventoryIndex).index, -1) = 0 Then
                                Party(argPartyslot).hp += Inventory(argInventoryIndex).atk
                                Party(argPartyslot).mp += Inventory(argInventoryIndex).magic
                                MsgBox(Party(argPartyslot).charname & " was healed for " & Inventory(argInventoryIndex).atk & "Hp and " & Inventory(argInventoryIndex).magic & "Mp")
                                Return True
                            End If
                            'End If

                            'End If
                    End If
                End If
            End If
            End If
        End If
        Return False
    End Function
    Private Sub main_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Dim temp As Integer = 0
        Dim tempp As Point = New Point(0, 0)
        If e.KeyCode = Keys.Escape Then
            Me.KeyPreview = False
            escMenushow()

            'If GameStarted Then

            'Else
            '    NewGame()
            'End If

        ElseIf e.KeyCode = Keys.A Or e.KeyCode = Keys.Left Then
            If GameStarted AndAlso Smooth AndAlso Not GameOver AndAlso Not InBattle Then
                timDontBreakIt.Start()
                Smooth = False
                tempp = New Point(CurrentWorldMap.getPlayerCords().X - 1, CurrentWorldMap.getPlayerCords().Y - 0)
                NextArea(tempp, CurrentWorldMap.Move(tempp, 0), 0)
                MapSprites(0).Direction = 1
                MapSprites(0).Sprite = Party(CurrentShownPartyMember).getimage(MapSprites(0).Direction, MapSpriteLastAnim)
            End If
        ElseIf e.KeyCode = Keys.W Or e.KeyCode = Keys.Up Then
            If GameStarted AndAlso Smooth AndAlso Not GameOver AndAlso Not InBattle Then
                timDontBreakIt.Start()
                Smooth = False
                tempp = New Point(CurrentWorldMap.getPlayerCords().X - 0, CurrentWorldMap.getPlayerCords().Y - 1)
                NextArea(tempp, CurrentWorldMap.Move(tempp, 0), 0)
                MapSprites(0).Direction = 0
                MapSprites(0).Sprite = Party(CurrentShownPartyMember).getimage(MapSprites(0).Direction, MapSpriteLastAnim)
            End If
        ElseIf e.KeyCode = Keys.S Or e.KeyCode = Keys.Down Then
            If GameStarted AndAlso Smooth AndAlso Not GameOver AndAlso Not InBattle Then
                timDontBreakIt.Start()
                Smooth = False
                tempp = New Point(CurrentWorldMap.getPlayerCords().X + 0, CurrentWorldMap.getPlayerCords().Y + 1)
                NextArea(tempp, CurrentWorldMap.Move(tempp, 0), 0)
                MapSprites(0).Direction = 3
                MapSprites(0).Sprite = Party(CurrentShownPartyMember).getimage(MapSprites(0).Direction, MapSpriteLastAnim)
            End If
        ElseIf e.KeyCode = Keys.D Or e.KeyCode = Keys.Right Then
            If GameStarted AndAlso Smooth AndAlso Not GameOver AndAlso Not InBattle Then
                timDontBreakIt.Start()
                Smooth = False
                tempp = New Point(CurrentWorldMap.getPlayerCords().X + 1, CurrentWorldMap.getPlayerCords().Y + 0)
                NextArea(tempp, CurrentWorldMap.Move(tempp, 0), 0)
                MapSprites(0).Direction = 2
                MapSprites(0).Sprite = Party(CurrentShownPartyMember).getimage(MapSprites(0).Direction, MapSpriteLastAnim)
            End If
        ElseIf e.KeyCode = Keys.F1 Then
            If GameStarted Then
                If Party(0).dead Then
                    Party(0).Revive()
                End If
                Party(0).AddXP(Val(InputBox("add exp to slot 1")))
                MsgBox("xp: " & Party(0).xp & "level: " & Party(0).currentlevel)
            End If
        ElseIf e.KeyCode = Keys.F2 Then
            If GameStarted Then
                If Party(1).dead Then
                    Party(1).Revive()
                End If
                Party(1).AddXP(Val(InputBox("add exp to slot 2")))
                MsgBox("xp: " & Party(1).xp & "level: " & Party(1).currentlevel)
            End If
        ElseIf e.KeyCode = Keys.F3 Then
            If GameStarted Then
                If Party(2).dead Then
                    Party(2).Revive()
                End If
                Party(2).AddXP(Val(InputBox("add exp to slot 3")))
                MsgBox("xp: " & Party(2).xp & "level: " & Party(2).currentlevel)
            End If
        ElseIf e.KeyCode = Keys.F4 Then
            If GameStarted Then
                If Party(3).dead Then
                    Party(3).Revive()
                End If
                Party(3).AddXP(Val(InputBox("add exp to slot 4")))
                MsgBox("xp: " & Party(3).xp & "level: " & Party(3).currentlevel)
            End If
        ElseIf e.KeyCode = Keys.Q Then
            stats = New charStats
            stats.Show()
        ElseIf e.KeyCode = Keys.E Then
            InvenScreen = New InventoryScreen
            InvenScreen.Sell = False
            InvenScreen.Show()
            'ElseIf e.KeyCode = Keys.R Then
            '    InvenScreen.Sell = True
            '    InvenScreen.ShowDialog()
            'ElseIf e.KeyCode = Keys.F5 Then
            '    If GameStarted Then
            '        Party(0).EquipItem(2)
            '        Party(0).EquipItem(5)
            '        Party(0).EquipItem(11)
            '    End If

            'ElseIf e.KeyCode = Keys.F6 Then
            '    If GameStarted Then
            '        Party(1).EquipItem(6)
            '        Party(1).EquipItem(7)
            '        Party(1).EquipItem(9)
            '    End If

            'ElseIf e.KeyCode = Keys.F7 Then
            '    If GameStarted Then
            '        Party(2).EquipItem(13)
            '        Party(2).EquipItem(14)
            '        Party(2).EquipItem(11)
            '    End If

            'ElseIf e.KeyCode = Keys.F8 Then
            '    If GameStarted Then
            '        Party(3).EquipItem(12)
            '        'Party(3).EquipItem(5)
            '        Party(3).EquipItem(8)
            '    End If

            'ElseIf e.KeyCode = Keys.F9 Then
            '    InsertItemInToInventory(1, 1)
            '    InsertItemInToInventory(2, 1)
            '    InsertItemInToInventory(3, 1)
            '    InsertItemInToInventory(4, 1)
            '    InsertItemInToInventory(5, 1)
            '    InsertItemInToInventory(6, 1)
            '    InsertItemInToInventory(7, 1)
            '    InsertItemInToInventory(8, 1)
            '    InsertItemInToInventory(9, 1)
            '    InsertItemInToInventory(10, 1)
            '    InsertItemInToInventory(11, 1)
            '    InsertItemInToInventory(12, 1)
            '    InsertItemInToInventory(13, 1)
            '    InsertItemInToInventory(14, 1)
            '    InsertItemInToInventory(15, 75)
            '    InsertItemInToInventory(16, 89)
            '    InsertItemInToInventory(17, 98)
        End If
    End Sub
    Private Function NextArea(ByVal argPoint As Point, ByVal argResult As Integer, ByVal argSprite As Integer) As Integer

        If argResult = 3 Then
            NPCMapSprites(0).Active = False
            For i As Integer = 1 To 12 Step 1
                MapSprites(i).Active = False
                NPCMapSprites(i).Active = False
            Next
            If CurrentWorldMap.getDest(argPoint, destination) Then

                'CurrentWorldMap.HideLables()
                worldmaptemplate(destination.zone, destination.areax, destination.areay).CopyTo(CurrentWorldMap)
                'CurrentWorldMap.ShowLables()
                CurrentWorldMap.AddPlayer(destination.xyPair)
                MapSprites(0).Location = destination.xyPair
                MapSprites(0).MoveImage()
                If destination.zone < 10 Then
                    battleBackground = Image.FromFile("art\maps\0" & destination.zone & "Battle.png")
                Else
                    battleBackground = Image.FromFile("art\maps\" & destination.zone & "Battle.png")
                End If
                Dim rand As New Random
                If destination.zone >= 2 Then
                    Dim tempj As Integer = rand.Next(4, 12)
                    For i As Integer = 1 To tempj Step 1
                        CurrentWorldMap.AddMob()
                        Dim temppoint As Point = CurrentWorldMap.GetMobCords(i)
                        MapSprites(i).Location = temppoint
                        MapSprites(i).Active = True
                        MapSprites(i).MoveImage()
                        MapSprites(i).MobIndex = rand.Next(0, MobTemplate.GetUpperBound(0) + 1)
                    Next
                End If
                If CurrentWorldMap.HasNPCS Then
                    For i As Integer = 0 To CurrentWorldMap.npccounter - 1 Step 1
                        NPCMapSprites(i).Active = True
                        NPCMapSprites(i).Location = CurrentWorldMap.GetNPCCords(i)
                        NPCMapSprites(i).npcIndex = CurrentWorldMap.getNPCIndex(i)
                        NPCMapSprites(i).Sprite = NPCTemplate(NPCMapSprites(i).npcIndex).getimage(3, MapSpriteLastAnim)
                        NPCMapSprites(i).MoveImage()
                    Next
                End If
            Else
                'CurrentWorldMap.HideLables()
                CurrentWorldMap = worldmaptemplate(0, 0, 0)
                'CurrentWorldMap.ShowLables()
                CurrentWorldMap.AddPlayer(New Point(5, 5))
            End If
            Me.BackgroundImage = CurrentWorldMap.map
        ElseIf argResult = 1 Then
            If argSprite = 0 Then
                Dim tempk As Integer = -1
                For i As Integer = 1 To 12
                    If argPoint = MapSprites(i).Location And MapSprites(i).Active Then
                        tempk = i
                    End If
                Next
                If tempk >= 1 Then
                    Dim tempint As Integer = Battle(MapSprites(tempk).MobIndex)
                    If tempint = 0 Then
                        CurrentWorldMap.KillMob(tempk)
                        MapSprites(tempk).Active = False
                    ElseIf tempint = 1 Then
                        gameoverlulz()
                    ElseIf tempint = 3 Then
                        Return 0
                    End If
                Else
                    MsgBox("Uh oh something went wrong in nextares")
                End If
            Else
                Dim tempint As Integer = Battle(MapSprites(argSprite).MobIndex)
                If tempint = 0 Then
                    CurrentWorldMap.KillMob(argSprite)
                    MapSprites(argSprite).Active = False
                ElseIf tempint = 1 Then
                    gameoverlulz()
                End If
            End If
        ElseIf argResult = 2 Then
            If argSprite = 0 Then
                For i As Integer = 0 To 12
                    If argPoint = NPCMapSprites(i).Location And NPCMapSprites(i).Active Then
                        TalkMenu = New NPCmenu
                        TalkMenu.NPCIndex = CurrentWorldMap.getNPCIndex(i)
                        TalkMenu.IsMerchent = NPCTemplate(TalkMenu.NPCIndex).IsMerchent
                        TalkMenu.IsInnKeeper = NPCTemplate(TalkMenu.NPCIndex).IsInnKeeper
                        TalkMenu.MerchName = NPCTemplate(TalkMenu.NPCIndex).name
                        TalkMenu.Show()

                    End If
                Next

            End If
        ElseIf argResult = 4 Then
            NPCMapSprites(0).Active = False
            For i As Integer = 1 To 12 Step 1
                MapSprites(i).Active = False
                NPCMapSprites(i).Active = False
            Next
            'CurrentWorldMap.HideLables()
            worldmaptemplate(destination.zone, destination.areax, destination.areay).CopyTo(CurrentWorldMap)
            'CurrentWorldMap.ShowLables()
            CurrentWorldMap.AddPlayer(destination.xyPair)
            MapSprites(0).Location = destination.xyPair
            MapSprites(0).MoveImage()
            If destination.zone < 10 Then
                battleBackground = Image.FromFile("art\maps\0" & destination.zone & "Battle.png")
            Else
                battleBackground = Image.FromFile("art\maps\" & destination.zone & "Battle.png")
            End If
            Dim rand As New Random
            If destination.zone >= 2 Then
                Dim tempj As Integer = rand.Next(4, 12)
                For i As Integer = 1 To tempj Step 1
                    CurrentWorldMap.AddMob()
                    Dim temppoint As Point = CurrentWorldMap.GetMobCords(i)
                    MapSprites(i).Location = temppoint
                    MapSprites(i).Active = True
                    MapSprites(i).MoveImage()
                    MapSprites(i).MobIndex = rand.Next(0, MobTemplate.GetUpperBound(0) + 1)
                Next
            End If
            If CurrentWorldMap.HasNPCS Then
                For i As Integer = 0 To CurrentWorldMap.npccounter - 1 Step 1
                    NPCMapSprites(i).Active = True
                    NPCMapSprites(i).Location = CurrentWorldMap.GetNPCCords(i)
                    NPCMapSprites(i).npcIndex = CurrentWorldMap.getNPCIndex(i)
                    NPCMapSprites(i).Sprite = NPCTemplate(NPCMapSprites(i).npcIndex).getimage(3, MapSpriteLastAnim)
                    NPCMapSprites(i).MoveImage()
                Next
            End If
        End If
        Me.Text = "Loaded Dice RPG " & destination.zone.ToString("00") & destination.areax.ToString("00") & destination.areay.ToString("00") & MapSprites(0).Location.X.ToString("00") & MapSprites(0).Location.Y.ToString("00")
        Return 0
    End Function
    Private Sub timAnim_Tick(sender As Object, e As EventArgs) Handles timAnim.Tick
        If GameStarted Then
            '(0 N) (1 W ) (2 E) (3 S) (4 Battle) previous refers to if it was image 0 or  of the animation so wierd things dont happen
            If MapSpriteLastAnim = 0 Then
                MapSpriteLastAnim = 1
            Else
                MapSpriteLastAnim = 0
            End If

            If InBattle Then
                For i As Integer = 0 To 7 Step 1
                    If i <= 3 Then
                        BattleSprites(i).Image = Party(i).getimage(4, MapSpriteLastAnim)
                    Else
                        BattleSprites(i).Image = monsterParty(i - 4).getimage(4, MapSpriteLastAnim)
                    End If
                Next
            Else
                MapSprites(0).Sprite = Party(CurrentShownPartyMember).getimage(MapSprites(0).Direction, MapSpriteLastAnim)
                For i As Integer = 1 To 12 Step 1
                    If MapSprites(i).Active Then
                        MapSprites(i).Sprite = MobTemplate(MapSprites(i).MobIndex).getimage(MapSprites(i).Direction, MapSpriteLastAnim)
                    End If
                Next
            End If

        End If
    End Sub
    Public Sub gameoverlulz()
        Me.BackgroundImage = Image.FromFile("art\GAME_OVER.png")
        SaveEnable = False
        For i As Integer = 0 To 12 Step 1
            If i <= 3 Then
                Party(i).removeControls()
            End If
            If i <= 7 Then
                BattleSprites(i).Visible = False
            End If
            MapSprites(i).Active = False
        Next
        escMenushow()
    End Sub
    Private Sub timOnster_Tick(sender As Object, e As EventArgs) Handles timOnster.Tick 'shits really broken
        timOnster.Stop()
        If Not InBattle And GameStarted And Not PauseAll Then
            Dim rand As New Random
            Dim j As Integer = 0
            Dim k As Integer = 0
            Dim l As Integer = 0
            Dim tempcount As Integer = 0
            Dim tempp As Point
            For i As Integer = 1 To 12 Step 1
                If MapSprites(i).Active = True Then
                    Dim check As Boolean = True
                    Do While check
                        j = rand.Next(0, 3)
                        Select Case j
                            Case 0
                                tempp = New Point(CurrentWorldMap.GetMobCords(i).X + 0, CurrentWorldMap.GetMobCords(i).Y - 1)
                            Case 1
                                tempp = New Point(CurrentWorldMap.GetMobCords(i).X - 1, CurrentWorldMap.GetMobCords(i).Y + 0)
                            Case 2
                                tempp = New Point(CurrentWorldMap.GetMobCords(i).X + 1, CurrentWorldMap.GetMobCords(i).Y + 0)
                            Case 3
                                tempp = New Point(CurrentWorldMap.GetMobCords(i).X + 0, CurrentWorldMap.GetMobCords(i).Y + 1)
                        End Select
                        k = CurrentWorldMap.Move(tempp, i)
                        If k = 1 Then
                            NextArea(tempp, 1, i)
                        ElseIf k = 0 Then
                            check = False
                        ElseIf k = -1 Then 'this is here in case somehow a sprite gets stuck.
                            tempcount += 1
                            If tempcount >= 32 Then
                                check = False
                            End If
                        End If
                    Loop
                End If
                tempcount = 0
            Next
        End If
        timOnster.Start()

    End Sub

    Private Sub timDontBreakIt_Tick(sender As Object, e As EventArgs) Handles timDontBreakIt.Tick
        timDontBreakIt.Stop()
        Smooth = True
    End Sub


End Class
'cheat sheet

'image direction codes
'0 North
'1 West
'2 East
'3 South

'cordinate system
'zone,areax,areay,x,y
'example (area and on screen. zone is "linear")
'0,0:1,0:2,0
'0,1:1,1:2,1
'0,2:1,2:2,2

'movement grid codes
'0 open free space - no color
'1 pathing block - red - Color.FromArgb(237, 28, 36)
'2 teleport square - yellow - Color.FromArgb(255, 242, 0)
'3 NPC square - blueish - Color.FromArgb(0, 162, 232)
'4 monster - N/A
'5 player - N/A

'Move Result Codes
'-1 failure to move
'0 successful movement
'1 battle
'2 talk
'3 teleport

'save-load codes
'0 success
'1 success new file created
'2 failure file not found
'3 failure file permissions

'save database structure
'-table "party"
'-collum "slot" integer PRIMARY KEY
'-collum "name" string
'-collum "class" integer
'-collum "exp" double
'-collum "level" integer
'-collum "maxhp" double
'-collum "currenthp" double
'-collum "maxmp" double
'-collum "currentmp" double
'-collum "speed" integer
'-collum "defense" integer
'-collum "resistance" integer
'-collum "attack" integer
'-collum "magic" integer
'-collum "mainhandindex" integer
'-collum "offhandindex" integer
'-collum "armorindex" integer

'-table "world"
'-collum "id" integer PRIMARY KEY
'-collum "location" string(using format GCS which is 10 characters in length)
'-collum "money" integer

'-table inventory
'-collum "id" integer PRIMARY KEY -slot in inventory 0-255
'-collum "item" integer -item template index
'-collum "quantity" integer

'\* currently unused
'-table "bprogress"
'-collum "bossid" integer
'-collum "kills" integer

'-table "qprogress" 
'-collum "questid" integer
'-collum "complete" bool *\
