Public Class GUI
    'Const playerViewRange As Integer = 10
    'Const tilesInLine As Integer = playerViewRange * 2 + 1
    Const tileSize As Integer = 50
    ''variables
    Public player As New Player 'required to draw map
    Public map As New World
    Dim previousClickedTile(1) As Integer
    Dim tilesExist As Boolean = False
    ''test
    Dim horizontalTiles As Integer
    Dim verticalTiles As Integer

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        StartupMenu.Enabled = True
        StartupMenu.Visible = True
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        createTiles()
    End Sub

    Private Sub createTiles()
        horizontalTiles = Gamewindow.Width / tileSize

        ''Ensure odd number of tiles so player can be centered
        If horizontalTiles Mod 2 = 0 Then
            horizontalTiles -= 1
        End If
        verticalTiles = Gamewindow.Height / tileSize
        If verticalTiles Mod 2 = 0 Then
            verticalTiles -= 1
        End If

        ''generate tiles
        For x = 1 To horizontalTiles    'left to right
            For y = 1 To verticalTiles  'bottom to top
                Dim newTile As New Button
                newTile.Top = (y - 1) * tileSize                        'Position
                newTile.Left = (x - 1) * tileSize
                newTile.Width = tileSize                                'Size
                newTile.Height = tileSize
                newTile.FlatStyle = FlatStyle.Flat                      'Change draw style to look less button-like
                AddHandler newTile.Click, AddressOf Me.tileClick           'Add click event handler
                newTile.TextAlign = ContentAlignment.MiddleCenter       'Center text
                newTile.Font = New Font("New Roman", 13, FontStyle.Bold)
                newTile.Name = "T" & x & "," & y                        'Set unique ID, eg. T1,1 for bottom left tile
                Gamewindow.Controls.Add(newTile)                        'Add to window
            Next
        Next
        tilesExist = True
        drawmap()
        restoremovementcontrol()
    End Sub

    Private Sub deleteTiles()
        Dim reflabel As Button
        Dim ytile = 1
        Dim xtile = 1
        For y = 1 To verticalTiles      'bottom to top
            For x = 1 To horizontalTiles  'left to right
                Try
                    reflabel = Controls.Find("T" & CType(xtile & "," & ytile, String), True)(0)
                    reflabel.Dispose()
                Catch ex As Exception

                End Try
                xtile = xtile + 1
            Next
            ytile = ytile + 1
            xtile = 1
        Next
        tilesExist = False
    End Sub

    Private Sub gameWindowResize() Handles Gamewindow.SizeChanged
        If tilesExist Then
            deleteTiles()
            createTiles()
        End If
    End Sub

    Public Function random(ByVal min As Integer, ByVal max As Integer)
        Randomize()
        random = (Int((max - min + 1) * Rnd() + min))
    End Function

    Public Sub drawmap()
        For ytile = 1 To verticalTiles  'bottom to top
            For xtile = 1 To horizontalTiles  'left to right
                Dim x As Integer = horizontalTileMapping(xtile)
                Dim y As Integer = verticalTileMapping(ytile)
                renderTile(xtile, ytile, x, y)
            Next
        Next
    End Sub

    Private Function horizontalTileMapping(ByVal xtile As Integer)
        Return player.xValue - (horizontalTiles + 1) / 2 + xtile
    End Function

    Private Function verticalTileMapping(ByVal ytile As Integer)
        Return player.yValue + (verticalTiles + 1) / 2 - ytile  'drawn bottom up
    End Function

    Private Sub renderTile(ByVal xtile As Integer, ByVal ytile As Integer, ByVal x As Integer, ByVal y As Integer)
        Dim tile As Button = CType(Controls.Find("T" & CType(xtile & "," & ytile, String), True)(0), Button)
        If Not player.canSee(x, y) Then 'can't see
            tile.Text = " "
            tile.ForeColor = Color.Gray
            tile.BackColor = Color.Gray
        ElseIf Not map.inBounds(x, y) Then   'edge of map      
            tile.Text = " "
            tile.ForeColor = Color.Black
            tile.BackColor = Color.Black
        Else
            Dim mapTile As MapTile = map.getTile(x, y)
            tile.BackColor = mapTile.terrain.ground.colour
            Dim creature As Creature = mapTile.getCreature
            Dim item As Item = mapTile.getItem
            If Not creature Is Nothing Then 'else try to draw creature
                tile.Text = creature.showChar
                tile.ForeColor = creature.displayColour
            ElseIf Not item Is Nothing Then 'else try to draw item
                tile.Text = item.showChar
                tile.ForeColor = item.displayColour
            Else                                       'else draw terrain
                tile.Text = mapTile.terrain.construct.showChar
                tile.ForeColor = mapTile.terrain.construct.colour
            End If
        End If
    End Sub

    Private Sub playerDirectionAction(ByVal xchange As Integer, ByVal ychange As Integer, ByRef turntaken As Boolean)
        Dim tilex As Integer = player.xValue + xchange
        Dim tiley As Integer = player.yValue + ychange
        Dim tile As MapTile = map.getTile(tilex, tiley)
        If Not tile Is Nothing Then
            Dim creature = tile.getCreature
            If map.validMoveTile(tilex, tiley, player.flier) = True Then
                player.setPos(tilex, tiley)
                turntaken = True
            ElseIf Not creature Is Nothing Then
                If creature.status <> "dead" Then
                    player.attack(creature)
                    turntaken = True
                End If
            End If
        End If
    End Sub

    Private Sub userInput(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles MyBase.KeyPress
        Dim choice As Char
        Dim turntaken As Boolean = False
        choice = e.KeyChar.ToString
        ''player move
        If choice = "5" Or player.status = "dead" Then
            turntaken = True
        ElseIf choice = "1" Then 'SouthWest
            playerDirectionAction(-1, -1, turntaken)
        ElseIf choice = "2" Then 'South
            playerDirectionAction(0, -1, turntaken)
        ElseIf choice = "3" Then 'SouthEast
            playerDirectionAction(1, -1, turntaken)
        ElseIf choice = "4" Then 'West
            playerDirectionAction(-1, 0, turntaken)
        ElseIf choice = "6" Then 'East
            playerDirectionAction(1, 0, turntaken)
        ElseIf choice = "7" Then 'NorthWest
            playerDirectionAction(-1, 1, turntaken)
        ElseIf choice = "8" Then 'North
            playerDirectionAction(0, 1, turntaken)
        ElseIf choice = "9" Then 'NorthEast
            playerDirectionAction(1, 1, turntaken)
        End If
        If turntaken = True Then
            previousClickedTile(0) = map.width + 1
            previousClickedTile(1) = map.height + 1
            ''others move
            For Each creature In map.getAICreatureList
                creature.act()
            Next
            ''update display
            drawmap()
        End If
        restoremovementcontrol()
    End Sub

    Private Sub tileClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim tileundercursorname As String = Mid(sender.name, 2)
        Dim x, y As Integer
        Dim cords(1) As String
        cords = Split(tileundercursorname, ",")
        x = horizontalTileMapping(cords(0))
        y = verticalTileMapping(cords(1))
        If map.inBounds(x, y) And player.canSee(x, y) Then
            If player.adjacent(x, y) Then
                If Not map.getTile(x, y).getitem Is Nothing Then
                    If previousClickedTile(0) = x And previousClickedTile(1) = y Then
                        player.pickUpItem(x, y)
                    Else
                        map.Tquery(x, y)
                        gameLogWrite("Pick it up? (click again)", Color.Black)
                    End If
                Else
                    map.Tquery(x, y)
                End If
            Else
                map.Tquery(x, y)
            End If
        Else
            gameLogWrite("You can't see anything", Color.Black)
        End If

        previousClickedTile(0) = x
        previousClickedTile(1) = y
        restoremovementcontrol()
    End Sub

    Public Sub gameLogWrite(ByVal text As String, ByVal colour As Color)
        TBGamelog.SelectionColor = colour
        TBGamelog.AppendText(Environment.NewLine & text)
        TBGamelog.SelectionStart = TBGamelog.Text.Length
    End Sub

    Private Sub restoremovementcontrol() Handles TBGamelog.GotFocus 'prevent selection of gamelog
        focusdivert.Select()
    End Sub

    'Sub savemap()
    '    FileOpen(1, "Save\World.txt", OpenMode.Output)
    '    For y = 1 To mapy
    '        For x = 1 To mapx
    '            Print(1, map(x, y).showChar)
    '        Next
    '        PrintLine(1)
    '    Next
    'End Sub

    'Private Sub WindowResize() Handles Gamewindow.Resize
    '    For x = 0 To xtiles - 1
    '        For y = 0 To ytiles - 1
    '            resizelabel(x, y)
    '        Next
    '    Next
    'End Sub

    'Sub resizelabel(ByVal x As Integer, ByVal y As Integer)
    '    Try
    '        Dim reflabel As Control() = Controls.Find("T" & CType(x & "," & y, String), True)
    '        CType(reflabel(0), Label).Top = (Gamewindow.Height / 20 * y)
    '        CType(reflabel(0), Label).Left = Gamewindow.Width / 20 * x
    '        CType(reflabel(0), Label).Width = Gamewindow.Width / 20
    '        CType(reflabel(0), Label).Height = Gamewindow.Height / 20
    '    Catch ex As Exception

    '    End Try

    'End Sub
End Class
