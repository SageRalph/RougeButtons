Public Class World
    ''world gen
    Private Const structureNumber As Integer = 50
    Private Const maxBuildTrys As Integer = 100
    Private Const lakeNumber As Integer = 10
    Private Const lakeMaxSize As Integer = 100
    Private Const itemInBuildingTileProb As Integer = 10
    Private Const xSize As Integer = 100
    Private Const ySize As Integer = 100
    ''creatures
    Private Const lionNumber As Integer = 15
    Private Const wolfNumber As Integer = 30
    Private Const vultureNumber As Integer = 15
    Private AICreatureList(lionNumber + wolfNumber + vultureNumber - 1) As AICreature

    Private world(xSize, ySize) As MapTile

    Public Sub Tquery(ByVal x As Integer, ByVal y As Integer)
        Dim response As String = "This is nothing"  'default 
        If inBounds(x, y) Then
            response = world(x, y).describe()
        End If
        GUI.gameLogWrite(response, Color.Black)
    End Sub

    Public Function inBounds(ByVal x As Integer, ByVal y As Integer)
        Return x > 0 And x <= xSize And y > 0 And y <= ySize
    End Function

    Public Function width()
        width = xSize
    End Function

    Public Function height()
        height = ySize
    End Function

    Public Function getTile(ByVal x As Integer, ByVal y As Integer)
        If inBounds(x, y) Then
            Return world(x, y)
        End If
        Return Nothing
    End Function

    Public Sub newWorld()
        ''set tile defaults
        For x = 1 To xSize
            For y = 1 To ySize
                world(x, y) = New MapTile
                world(x, y).terrain.construct.showChar = "."
                world(x, y).terrain.construct.colour = Color.Black
                world(x, y).terrain.ground.colour = Color.Green
                world(x, y).setCreature(Nothing)
                world(x, y).setItem(Nothing)
                world(x, y).terrain.ground.queryInfo = "some grass"
            Next
        Next
        ''Gen lakes
        For i = 1 To lakeNumber
            genLake()
        Next
        ''place structures
        For i = 1 To structureNumber
            genStructure()
        Next
        GUI.player.loadDefaults()
        defineCreatures()

    End Sub

    Private Sub genLake()
        Dim laketiles As Integer = GUI.random(10, lakeMaxSize)
        Dim startx As Integer = GUI.random(1, xSize)
        Dim starty As Integer = GUI.random(1, ySize)
        Dim workingx As Integer
        Dim workingy As Integer
        Dim decider As Integer
        world(startx, starty).terrain.construct.showChar = "~"
        world(startx, starty).terrain.ground.colour = Color.Blue
        world(startx, starty).terrain.ground.queryInfo = "water"
        For i = 1 To lakeMaxSize - 1
            workingx = startx
            workingy = starty
            Do
                decider = GUI.random(1, 4)
                Select Case decider
                    Case 1
                        workingx = workingx + 1
                    Case 2
                        workingx = workingx - 1
                    Case 3
                        workingy = workingy + 1
                    Case 4
                        workingy = workingy - 1
                End Select
                If Not inBounds(workingx, workingy) Then 'catch edge of world
                    workingx = startx
                    workingy = starty
                End If
            Loop Until world(workingx, workingy).terrain.construct.showChar <> "~"
            world(workingx, workingy).terrain.construct.showChar = "~"
            world(workingx, workingy).terrain.ground.colour = Color.Blue
            world(workingx, workingy).terrain.ground.queryInfo = "water"
        Next
    End Sub

    Private Sub genStructure()
        Dim counter As System.Collections.ObjectModel.ReadOnlyCollection(Of String)
        counter = My.Computer.FileSystem.GetFiles("Resources\Structures\")
        Dim fileCount As Integer = CStr(counter.Count)
        Dim structureFile As String = "Resources\Structures\" & GUI.random(1, fileCount) & ".txt" 'pick GUI.Random structure plan
        ''read dimensions
        Dim width, height As Integer
        FileOpen(1, structureFile, OpenMode.Input)
        Dim line As String
        Do Until EOF(1)
            line = LineInput(1)
            height = height + 1 'count height
            If Len(line) > width Then 'find widest point
                width = Len(line)
            End If
        Loop
        FileClose(1)
        ''read shape
        FileOpen(1, structureFile, OpenMode.Input)
        Dim tiles(width, height) As Char
        For y = height To 1 Step -1
            line = LineInput(1)
            For x = 1 To width
                tiles(x, y) = Mid(line, x, 1)
            Next
        Next
        FileClose(1)
        ''position structure
        Dim failCount As Integer
        Dim fail As Boolean
        Dim anchorx, anchory As Integer
        Do
            fail = False
            anchorx = GUI.random(2, xSize - width - 1)
            anchory = GUI.random(2, ySize - height - 1)
            For y = anchory - 1 To anchory + height + 1
                For x = anchorx - 1 To anchorx + width + 1
                    If world(x, y).terrain.construct.showChar <> "." Then
                        fail = True
                        failCount = failCount + 1
                        If failCount = maxBuildTrys Then
                            Exit Sub
                        End If
                        Exit For
                    End If
                Next
                If fail = True Then
                    Exit For
                End If
            Next
        Loop Until fail = False
        ''build structure
        For y = 1 To height
            For x = 1 To width
                If tiles(x, y) <> Nothing And tiles(x, y) <> " " Then 'don't paint outside
                    world(anchorx + x - 1, anchory + y - 1).terrain.construct.showChar = tiles(x, y)
                    world(anchorx + x - 1, anchory + y - 1).terrain.construct.queryInfo = constructionQueryInfo(tiles(x, y))
                    world(anchorx + x - 1, anchory + y - 1).terrain.construct.colour = Color.Black
                    world(anchorx + x - 1, anchory + y - 1).terrain.ground.colour = Color.LightGray
                    world(anchorx + x - 1, anchory + y - 1).terrain.ground.queryInfo = "some gravel"
                    If tiles(x, y) <> "0" And tiles(x, y) <> "-" And tiles(x, y) <> "|" And GUI.random(1, 100) < itemInBuildingTileProb Then
                        placeRandomItem(anchorx + x - 1, anchory + y - 1)
                    End If
                End If
            Next
        Next
    End Sub

    Public Function constructionQueryInfo(ByVal tile As Char)
        If tile = "0" Or tile = "-" Or tile = "|" Then
            constructionQueryInfo = "a wall"
        ElseIf tile = "h" Then
            constructionQueryInfo = "a chair"
        Else
            constructionQueryInfo = "an unknown object"
        End If
    End Function

    Public Function validMoveTile(ByVal x As Integer, ByVal y As Integer, ByVal flier As Boolean)
        validMoveTile = False
        If inBounds(x, y) Then
            If world(x, y).terrain.construct.showChar = "." Or flier = True Then 'terrain check
                If world(x, y).getCreature Is Nothing Then 'creature check
                    validMoveTile = True
                End If
            End If
        End If
    End Function

    Private Sub placeItem(ByVal x As Integer, ByVal y As Integer, ByVal name As String, ByVal showChar As Char, ByVal displayColour As Color, ByVal attackDamage As Integer)
        Dim newItem As New Item
        newItem.displayColour = displayColour
        newItem.queryInfo = name
        newItem.showChar = showChar
        newItem.attackDamage = attackDamage
        newItem.setPos(x, y)
    End Sub

    Private Sub placeRandomItem(ByVal x As Integer, ByVal y As Integer)
        Dim choice As Integer
        choice = GUI.random(1, 2)
        If choice = 1 Then
            placeitem(x, y, "a sword", "✝", Color.Silver, 40)
        ElseIf choice = 2 Then
            placeitem(x, y, "a large rock", ".", Color.Gray, 20)
        End If
    End Sub

    Private Sub defineCreatures()
        Dim AICreatureListposition As Integer = 0
        For i = 1 To lionNumber
            defineNewCreature(AICreatureListposition, i, "a lion", "lionFaction", "L", Color.Yellow, 2, 7, False, 40, 30, 30, 0)
        Next
        For i = 1 To wolfNumber
            defineNewCreature(AICreatureListposition, i, "a wolf", "wolfFaction", "W", Color.Gray, 1, 7, False, 60, 20, 20, 0)
        Next
        For i = 1 To vultureNumber
            defineNewCreature(AICreatureListposition, i, "a vulture", "vultureFaction", "V", Color.Brown, 3, 7, True, 30, 5, 60, 0)
        Next
    End Sub

    Private Sub defineNewCreature(ByRef AICreatureListposition As Integer, ByVal instance As Integer, ByVal name As String, ByVal faction As String, ByVal showChar As Char, ByVal displayColour As Color, ByVal speed As Integer, ByVal viewRange As Integer, ByVal flier As Boolean, ByVal maxHealth As Integer, ByVal attackStrength As Integer, ByVal dodgeChance As Integer, ByVal freeGrasps As Integer)
        Dim newCreature As New AICreature
        newCreature.displayColour = displayColour
        newCreature.queryInfo = (name) ' & instance)
        newCreature.faction = faction
        newCreature.showChar = showChar
        newCreature.speed = speed
        newCreature.viewRange = viewRange
        newCreature.flier = flier
        newCreature.hunting = Nothing
        newCreature.status = ("healthy(" & maxHealth & ")")
        newCreature.health = maxHealth
        newCreature.dodgeChance = dodgeChance
        newCreature.attackStrength = attackStrength
        newCreature.freeGrasps = freeGrasps
        newCreature.equipedItems = Nothing
        newCreature.setRandomPos(1, xSize, 1, ySize)
        AICreatureList(AICreatureListposition) = newCreature
        AICreatureListposition = AICreatureListposition + 1
    End Sub

    Public Function getAICreatureList()
        getAICreatureList = AIcreaturelist
    End Function

End Class
