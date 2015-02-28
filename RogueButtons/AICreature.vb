Public Class AICreature
    Inherits Creature
    Public hunting As Creature
    Public speed As Integer

    Overloads Sub kill(ByVal victim As Creature)
        hunting = Nothing
        victim.die()
    End Sub

    'Sub lookForTarget() ''make searching more sensible
    '    For x = xValue() - viewRange To xValue() + viewRange
    '        For y = yValue() - viewRange To yValue() + viewRange
    '            If x > 0 And x < (GUI.mapx + 1) And y > 0 And y < (GUI.mapy + 1) Then 'catch edge of map
    '                If Not GUI.map.getTile(x, y).getCreature Is Nothing Then 'if there is a creature
    '                    If GUI.map.getTile(x, y).getCreature.faction <> faction And GUI.map.getTile(x, y).getCreature.status <> "dead" Then 'and not the same faction and not dead
    '                        hunting = GUI.map.getTile(x, y).getCreature 'start hunting
    '                        GUI.gameLogWrite(queryInfo & " is now hunting " & hunting.queryInfo, Color.Salmon)
    '                    End If
    '                End If
    '            End If
    '        Next
    '    Next
    'End Sub

    'Private Sub lookForTarget() 'all equal cost (broken)
    '    For distance = 1 To viewRange
    '        For x = xValue() - distance To xValue() + distance
    '            If checkTileForPrey(xValue(), yValue() - distance) Then
    '                Return
    '            End If
    '            If checkTileForPrey(xValue(), yValue() + distance) Then
    '                Return
    '            End If
    '        Next
    '        For y = yValue() - distance + 1 To yValue() + distance - 1
    '            If checkTileForPrey(xValue() - distance, yValue()) Then
    '                Return
    '            End If
    '            If checkTileForPrey(xValue() + distance, yValue()) Then
    '                Return
    '            End If
    '        Next
    '    Next
    'End Sub

    Private Sub lookForTarget() 'expencive diagonals
        For distance = 1 To viewRange
            For i = 0 To distance
                If checkTileForPrey(xValue() - distance + i, yValue() - i) Then
                    Return
                End If
                If checkTileForPrey(xValue() + distance - i, yValue() + i) Then
                    Return
                End If
            Next
            For i = 1 To distance - 1
                If checkTileForPrey(xValue() - i, yValue() + distance - i) Then
                    Return
                End If
                If checkTileForPrey(xValue() + distance - i, yValue() - i) Then
                    Return
                End If
            Next
        Next
    End Sub

    Private Function checkTileForPrey(ByVal x As Integer, ByVal y As Integer)
        If GUI.map.inBounds(x, y) Then 'catch edge of map
            Dim tile As MapTile = GUI.map.getTile(x, y)
            Dim creature As Creature = tile.getCreature
            If Not creature Is Nothing Then 'if there is a creature
                If creature.faction <> faction And creature.status <> "dead" Then 'and not the same faction and not dead
                    hunting = creature 'start hunting
                    GUI.gameLogWrite(queryInfo & " is now hunting " & hunting.queryInfo, Color.Salmon)
                    Return True
                End If
            End If
        End If
        Return False
    End Function

    Private Sub pathToTarget() ''need better pathfinding
        If xValue() > hunting.xValue And yValue() > hunting.yValue And GUI.map.validMoveTile(xValue() - 1, yValue() - 1, flier) Then
            movesouthwest(1, 1)
        ElseIf xValue() < hunting.xValue And yValue() > hunting.yValue And GUI.map.validMoveTile(xValue() + 1, yValue() - 1, flier) Then
            movesoutheast(1, 1)
        ElseIf xValue() < hunting.xValue And yValue() < hunting.yValue And GUI.map.validMoveTile(xValue() + 1, yValue() + 1, flier) Then
            movenortheast(1, 1)
        ElseIf xValue() > hunting.xValue And yValue() < hunting.yValue And GUI.map.validMoveTile(xValue() - 1, yValue() + 1, flier) Then
            movenorthwest(1, 1)
        ElseIf xValue() > hunting.xValue And GUI.map.validMoveTile(xValue() - 1, yValue, flier) Then
            movewest(1)
        ElseIf xValue() < hunting.xValue And GUI.map.validMoveTile(xValue() + 1, yValue, flier) Then
            moveeast(1)
        ElseIf yValue() > hunting.yValue And GUI.map.validMoveTile(xValue, yValue() - 1, flier) Then
            movesouth(1)
        ElseIf yValue() < hunting.yValue And GUI.map.validMoveTile(xValue, yValue() + 1, flier) Then
            movenorth(1)
        ElseIf (xValue() > hunting.xValue Or yValue() > hunting.yValue) And GUI.map.validMoveTile(xValue() - 1, yValue() - 1, flier) Then 'if obstacle
            movesouthwest(1, 1)
        ElseIf (xValue() < hunting.xValue Or yValue() > hunting.yValue) And GUI.map.validMoveTile(xValue() + 1, yValue() - 1, flier) Then
            movesoutheast(1, 1)
        ElseIf (xValue() < hunting.xValue Or yValue() < hunting.yValue) And GUI.map.validMoveTile(xValue() + 1, yValue() + 1, flier) Then
            movenortheast(1, 1)
        ElseIf (xValue() > hunting.xValue Or yValue() < hunting.yValue) And GUI.map.validMoveTile(xValue() - 1, yValue() + 1, flier) Then
            movenorthwest(1, 1)
        End If
    End Sub

    Public Sub act()
        If status <> "dead" Then 'if not dead
            If Not hunting Is Nothing Then 'if hunting
                For i = 1 To speed
                    If adjacent(hunting.xValue, hunting.yValue) Then
                        attack(hunting)
                        Exit For
                    Else
                        pathToTarget()
                    End If
                Next
            Else
                lookForTarget()
                If hunting Is Nothing Then
                    wanderAimlessly()
                End If
            End If
        End If
    End Sub

    Private Sub wanderAimlessly()
        Dim choice As Integer = GUI.random(1, 10)
        Select Case choice
            Case 1
                If GUI.map.validMoveTile(xValue() - 1, yValue() - 1, flier) Then
                    movesouthwest(1, 1)
                End If
            Case 2
                If GUI.map.validMoveTile(xValue() + 1, yValue() - 1, flier) Then
                    movesoutheast(1, 1)
                End If
            Case 3
                If GUI.map.validMoveTile(xValue() + 1, yValue() + 1, flier) Then
                    movenortheast(1, 1)
                End If
            Case 4
                If GUI.map.validMoveTile(xValue() - 1, yValue() + 1, flier) Then
                    movenorthwest(1, 1)
                End If
            Case 5
                If GUI.map.validMoveTile(xValue() - 1, yValue(), flier) Then
                    movewest(1)
                End If
            Case 6
                If GUI.map.validMoveTile(xValue() + 1, yValue(), flier) Then
                    moveeast(1)
                End If
            Case 7
                If GUI.map.validMoveTile(xValue, yValue() - 1, flier) Then
                    movesouth(1)
                End If
            Case 8
                If GUI.map.validMoveTile(xValue, yValue() + 1, flier) Then
                    movenorth(1)
                End If
            Case Else
                'do nothing
        End Select
    End Sub
End Class
