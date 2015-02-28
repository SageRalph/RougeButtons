Public Class Player
    Inherits Creature

    Public Sub loadDefaults()
        showChar = "θ"
        displayColour = Color.Yellow
        queryInfo = "the player"
        faction = "player"
        status = "healthy(100)"
        dodgeChance = 50
        health = 100
        attackStrength = 10
        freeGrasps = 2
        equipedItems = Nothing
        setRandomPos(1, GUI.map.width, 1, GUI.map.height)
        viewRange = 8
    End Sub

End Class
