Option Explicit On
Option Infer Off
Option Strict On
'Imports ADOX
'Imports System.Data
'Imports System.Data.OleDb
Imports System.Data.SQLite
Imports Project3.main
Public Class SaveSystem
    'Private Shared _saveExists As Boolean
    'Private Shared _savename As String
    Private Shared _party(3) As Character 'the temp storage to copy to the primary
    Private Shared Conn As SQLiteConnection
    Public Shared splash As SplashScreen
    Public Shared Function Save(ByVal argSlot As Integer) As Integer 'assuming argslots 0,1,2 but leaving the capability to do more then that.
        splash = New SplashScreen
        splash.Show()
        Dim result As Integer = 0
        splash.setProgress()
        splash.setProgress()
        splash.setProgress()
        Dim cmd As SQLiteCommand
        If Not IO.File.Exists("save\" & argSlot & ".save") Then
            Try
                SQLiteConnection.CreateFile("save\" & argSlot & ".save") 'create database
                Conn = New SQLiteConnection("DataSource=save\" & argSlot & ".save;")

                Using Conn 'part where i generate the database
                    Conn.Open()
                    cmd = Conn.CreateCommand()
                    cmd.CommandText = "CREATE TABLE party(id INTEGER PRIMARY KEY,name varchar(255),class integer,exp double,level integer,maxhp double,currenthp double,maxmp double,currentmp double,speed integer,defense integer,resistance integer,attack integer,attackrange integer,magic integer,mainhandindex integer, offhandindex integer, armorindex integer); CREATE TABLE world(id integer primary key,location string,money integer,shownchr integer); CREATE TABLE bprogress(bossid integer primary key,kills integer); CREATE TABLE qprogress(questid integer primary key,complete boolean); CREATE TABLE inventory(id integer primary key,item integer,quantity integer);"
                    cmd.ExecuteNonQuery()
                    result = 1
                    Conn.Close()
                End Using
            Catch
                result = 3
            End Try
        End If
        splash.setProgress()
        splash.setProgress()
        splash.setProgress()
        splash.setProgress()
        If result = 3 Then
            splash.Dispose()
            Return result
        Else
            Try
                Conn = New SQLiteConnection("DataSource=save\" & argSlot & ".save;")
                Using Conn
                    splash.setProgress()
                    splash.setProgress()
                    Conn.Open()
                    cmd = Conn.CreateCommand()
                    cmd.CommandText = "DELETE FROM party;DELETE FROM world; DELETE FROM inventory;"
                    cmd.ExecuteNonQuery()
                    splash.setProgress()
                    splash.setProgress()
                    For i As Integer = 0 To 3 Step 1
                        cmd.CommandText += "INSERT INTO party VALUES(" & i & "," & ControlChars.Quote & main.Party(i).charname & ControlChars.Quote & "," & main.Party(i).playerslot & "," & main.Party(i).xp & "," & main.Party(i).currentlevel & "," & main.Party(i).maxhp & "," & main.Party(i).hp & "," & main.Party(i).maxmp & "," & main.Party(i).mp & "," & main.Party(i).speed & "," & main.Party(i).defense & "," & main.Party(i).resist & "," & main.Party(i).atk & "," & "," & main.Party(i).atkrange & "," & main.Party(i).magic & "," & main.Party(i).weaponindex & "," & main.Party(i).offhandindex & "," & main.Party(i).armorindex & ");"
                    Next
                    splash.setProgress()

                    cmd.CommandText += "INSERT INTO world VALUES(0," & main.destination.zone.ToString("00") & main.destination.areax.ToString("00") & main.destination.areay.ToString("00") & main.MapSprites(0).Location.X.ToString("00") & main.MapSprites(0).Location.Y.ToString("00") & "," & main.TotalMoney & "," & main.CurrentShownPartyMember & ");"
                    For i As Integer = 0 To 254 Step 1
                        cmd.CommandText += "INSERT INTO inventory VALUES(" & i & "," & main.Inventory(i).index & "," & main.Inventory(i).stack & ");"
                    Next
                    splash.setProgress()
                    splash.setProgress()
                    splash.setProgress()
                    splash.setProgress()
                    splash.setProgress()
                    splash.setProgress()

                    cmd.ExecuteNonQuery()
                    Conn.Close()
                End Using
            Catch
                Conn.Close()
                IO.File.Delete("save\" & argSlot & ".save")
                Try


                    SQLiteConnection.CreateFile("save\" & argSlot & ".save") 'create database
                    Conn = New SQLiteConnection("DataSource=save\" & argSlot & ".save;")
                    Using Conn
                        cmd = Conn.CreateCommand()
                        cmd.CommandText = "CREATE TABLE party(id INTEGER PRIMARY KEY,name varchar(255),class integer,exp double,level integer,maxhp double,currenthp double,maxmp double,currentmp double,speed integer,defense integer,resistance integer,attack integer,attackrange integer,magic integer,mainhandindex integer, offhandindex integer, armorindex integer); CREATE TABLE world(id integer primary key,location string,money integer,shownchr integer); CREATE TABLE bprogress(bossid integer primary key,kills integer); CREATE TABLE qprogress(questid integer primary key,complete boolean); CREATE TABLE inventory(id integer primary key,item integer,quantity integer);"
                        cmd.ExecuteNonQuery()
                        For i As Integer = 0 To 3 Step 1
                            cmd.CommandText += "INSERT INTO party VALUES(" & i & "," & ControlChars.Quote & main.Party(i).charname & ControlChars.Quote & "," & main.Party(i).playerslot & "," & main.Party(i).xp & "," & main.Party(i).currentlevel & "," & main.Party(i).maxhp & "," & main.Party(i).hp & "," & main.Party(i).maxmp & "," & main.Party(i).mp & "," & main.Party(i).speed & "," & main.Party(i).defense & "," & main.Party(i).resist & "," & main.Party(i).atk & "," & "," & main.Party(i).atkrange & "," & main.Party(i).magic & "," & main.Party(i).weaponindex & "," & main.Party(i).offhandindex & "," & main.Party(i).armorindex & ");"
                        Next
                        splash.setProgress()

                        cmd.CommandText += "INSERT INTO world VALUES(0," & main.destination.zone.ToString("00") & main.destination.areax.ToString("00") & main.destination.areay.ToString("00") & main.MapSprites(0).Location.X.ToString("00") & main.MapSprites(0).Location.Y.ToString("00") & "," & main.TotalMoney & "," & main.CurrentShownPartyMember & ");"
                        For i As Integer = 0 To 254 Step 1
                            cmd.CommandText += "INSERT INTO inventory VALUES(" & i & "," & main.Inventory(i).index & "," & main.Inventory(i).stack & ");"
                        Next
                    End Using
                Catch
                    result = 3
                End Try
            End Try

            splash.setProgress()
            splash.setProgress()


        End If
        splash.Dispose()
        main.LoadEnable = IO.File.Exists("save\" & argSlot & ".save")
        Return result
    End Function
    Public Shared Function Load(ByVal argLoadSlot As Integer) As Integer
        splash = New SplashScreen
        splash.Show()
        Dim result As Integer = 0
        If IO.File.Exists("save\" & argLoadSlot & ".save") Then
            Try
                Conn = New SQLiteConnection("DataSource=save\" & argLoadSlot & ".save;")
                Using Conn 'pulling the character data from the database
                    Conn.Open()
                    For i As Integer = 0 To 3 Step 1
                        Using main
                            'Public Sub New(ByVal argatk As Double, ByVal argatkrange As Double, ByVal argdefense As Double, ByVal argName As String, ByVal arglevel As Integer, ByVal arghpmax As Double, ByVal argmpmax As Double, ByVal argresist As Double, ByVal argmagic As Double, ByVal argxp As Double, ByVal argspeed As Double, ByVal argmhindex As Integer, ByVal argohindex As Integer, ByVal argarmorindex As Integer, ByRef argTemplate As Character)
                            main.Party(i) = New Character(GetTableDBLEntry("party", (i + 0).ToString, "attack", Conn), GetTableDBLEntry("party", (i + 0).ToString, "attackrange", Conn), GetTableDBLEntry("party", (i + 0).ToString, "defense", Conn), GetTableSTREntry("party", (i + 0).ToString, "name", Conn), GetTableINTEntry("party", (i + 0).ToString, "level", Conn), GetTableDBLEntry("party", (i + 0).ToString, "maxhp", Conn), GetTableDBLEntry("party", (i + 0).ToString, "maxmp", Conn), GetTableDBLEntry("party", (i + 0).ToString, "resistance", Conn), GetTableDBLEntry("party", (i + 0).ToString, "magic", Conn), GetTableDBLEntry("party", (i + 0).ToString, "exp", Conn), GetTableDBLEntry("party", (i + 0).ToString, "speed", Conn), GetTableINTEntry("party", (i + 0).ToString, "mainhandindex", Conn), GetTableINTEntry("party", (i + 0).ToString, "offhandindex", Conn), GetTableINTEntry("party", (i + 0).ToString, "armorindex", Conn), main.PCTemplate(i))


                            main.Party(i).hp = GetTableDBLEntry("party", (i + 0).ToString, "currenthp", Conn)
                            main.Party(i).mp = GetTableDBLEntry("party", (i + 0).ToString, "currentmp", Conn)
                        End Using

                    Next
                    main.CurrentShownPartyMember = GetTableINTEntry("world", "0", "shownchr", Conn)
                    main.TotalMoney = GetTableDBLEntry("world", "0", "money", Conn)
                    Dim loc As String = GetTableSTREntry("world", "0", "location", Conn)
                    main.destination = New globalCordinate(CInt(loc.Substring(0, 2)), CInt(loc.Substring(2, 2)), CInt(loc.Substring(4, 2)), CInt(loc.Substring(6, 2)), CInt(loc.Substring(8, 2)))

                    result = 1
                    Conn.Close()

                End Using

            Catch
                result = 3
            End Try
        End If
        splash.Dispose()
        Return result
    End Function





End Class

