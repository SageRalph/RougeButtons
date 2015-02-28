Public Class Creature
    Private xpos As Integer
    Private ypos As Integer
    Public showChar As Char
    Public displayColour As Color
    Public queryInfo As String
    Public status As String
    Public health As Integer
    Public nativestrength As Integer
    Public attackStrength As Integer = nativestrength
    Public dodgeChance As Integer
    Public flier As Boolean
    Public freeGrasps As Integer
    Public equipedItems As Item
    Public faction As String
    Public viewRange As Integer

    Public Function canSee(ByVal x As Integer, ByVal y As Integer)
        Return x < (xpos + viewRange) And x > (xpos - viewRange) And y < (ypos + viewRange) And y > (ypos - viewRange)
    End Function

    Public Sub kill(ByVal victim As Creature)
        GUI.gameLogWrite(victim.queryInfo & " was killed by " & queryInfo, Color.Red)
        victim.die()
    End Sub

    Public Sub die()
        status = "dead" 'mark as dead
        displayColour = Color.White

        For Each Creature In GUI.map.getAICreatureList 'all creatures stop hunting the dead creature
            If Not Creature.hunting Is Nothing Then
                If Creature.hunting.queryInfo = queryInfo Then
                    Creature.hunting = Nothing
                    'gameLogWrite(Creature.queryInfo & " has stopped hunting " & hunted.queryInfo, Color.Orange)
                End If
            End If
        Next
    End Sub

    Public Sub attack(ByVal defender As Creature)
        Dim decider As Integer = GUI.random(1, 100)
        If decider > defender.dodgeChance Then 'dodge check
            defender.health = defender.health - attackStrength
            GUI.gameLogWrite(queryInfo & " dealt " & attackStrength & " damage to " & defender.queryInfo, Color.Red)
            defender.status = ("wounded(" & defender.health & ")")
            If defender.health < 1 Then 'check if kill
                'killcreature(attacker, defender)
                kill(defender)
            End If
        Else
            GUI.gameLogWrite(queryInfo & " attacks " & defender.queryInfo & " but the attack is dodged", Color.Orange)
        End If
    End Sub

    Public Function adjacent(ByVal x As Integer, ByVal y As Integer)
        adjacent = False
        If xValue() = x + 1 Or xValue() = x - 1 Or xValue() = x Then
            If yValue() = y + 1 Or yValue() = y - 1 Or yValue() = y Then
                adjacent = True
            End If
        End If
    End Function

    Public Sub movenorth(ByVal no As Integer)
        setPos(xpos, ypos + no)
    End Sub
    Public Sub movesouth(ByVal no As Integer)
        setPos(xpos, ypos - no)
    End Sub
    Public Sub moveeast(ByVal no As Integer)
        setPos(xpos + no, ypos)
    End Sub
    Public Sub movewest(ByVal no As Integer)
        setPos(xpos - no, ypos)
    End Sub
    Public Sub movenortheast(ByVal xchange As Integer, ByVal ychange As Integer)
        setPos(xpos + xchange, ypos + ychange)
    End Sub
    Public Sub movesoutheast(ByVal xchange As Integer, ByVal ychange As Integer)
        setPos(xpos + xchange, ypos - ychange)
    End Sub
    Public Sub movenorthwest(ByVal xchange As Integer, ByVal ychange As Integer)
        setPos(xpos - xchange, ypos + ychange)
    End Sub
    Public Sub movesouthwest(ByVal xchange As Integer, ByVal ychange As Integer)
        setPos(xpos - xchange, ypos - ychange)
    End Sub
    Public Sub setPos(ByVal x As Integer, ByVal y As Integer)
        If GUI.map.inBounds(x, y) Then
            If xpos <> 0 And ypos <> 0 Then 'if already placed
                GUI.map.getTile(xpos, ypos).setcreature(Nothing)
            End If
            xpos = x
            ypos = y
            GUI.map.getTile(x, y).setCreature(Me)
        End If
    End Sub
    Public Sub setRandomPos(ByVal xmin As Integer, ByVal xmax As Integer, ByVal ymin As Integer, ByVal ymax As Integer)
        Dim x As Integer
        Dim y As Integer
        Do
            x = GUI.random(xmin, xmax)
            y = GUI.random(ymin, ymax)
        Loop Until GUI.map.validMoveTile(x, y, False)
        setPos(x, y)
    End Sub
    Public Function yValue()
        yValue = ypos
    End Function
    Public Function xValue()
        xValue = xpos
    End Function
    Public Sub pickUpItem(ByVal x As Integer, ByVal y As Integer)
        Dim item As Item = GUI.map.getTile(x, y).getitem
        equipedItems = item
        freegrasps = freegrasps - 1
        attackStrength = item.attackDamage
        GUI.gameLogWrite(queryInfo & " picks up " & item.queryInfo, Color.Blue)
        GUI.map.getTile(x, y).setItem(Nothing)
        GUI.drawmap()
    End Sub
End Class

