Public Class Item
    Private xpos As Integer
    Private ypos As Integer
    Public showChar As Char
    Public displayColour As Color
    Public queryInfo As String
    Public attackDamage As Integer

    Sub setPos(ByVal x As Integer, ByVal y As Integer)
        If xpos <> 0 And ypos <> 0 Then 'if already placed
            GUI.map.getTile(xpos, ypos).items = Nothing
        End If
        xpos = x
        ypos = y
        GUI.map.getTile(x, y).setitem(Me)
    End Sub
    Sub setRandomPos(ByVal xmin As Integer, ByVal xmax As Integer, ByVal ymin As Integer, ByVal ymax As Integer)
        Dim x As Integer
        Dim y As Integer
        Do
            x = GUI.random(xmin, xmax)
            y = GUI.random(ymin, ymax)
        Loop Until GUI.map.validMoveTile(x, y, False)
        setPos(x, y)
    End Sub
    Function yValue()
        yValue = ypos
    End Function
    Function xValue()
        xValue = xpos
    End Function
End Class
