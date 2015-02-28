Public Class MapTile

    Public terrain As terrainTile
    Private creatures As Creature
    Private items As Item

    Structure terrainTile
        Dim construct As constructInfo
        Dim ground As groundInfo
    End Structure

    Structure constructInfo
        Dim showChar As Char
        Dim colour As Color
        Dim queryInfo As String
    End Structure

    Structure groundInfo
        Dim colour As Color
        Dim queryInfo As String
    End Structure

    Public Sub setCreature(ByRef creature As Creature)
        Me.creatures = creature
    End Sub

    Public Function getCreature()
        Return Me.creatures
    End Function

    Public Sub setItem(ByRef item As Item)
        Me.items = item
    End Sub

    Public Function getItem()
        Return Me.items
    End Function

    Public Function describe()
        Dim queryResponse As String
        If Not (creatures Is Nothing) Then 'try to describe creature and terrain
            If creatures.status = "dead" Then 'is it dead?
                queryResponse = "This is the corpse of " & creatures.queryInfo & " laying dead on " & terrain.ground.queryInfo
            ElseIf creatures.flier = True Then 'or is it flying?
                queryResponse = "This is " & creatures.queryInfo & " flying over " & terrain.ground.queryInfo & ", it seems to be " & creatures.status
            Else
                queryResponse = "This is " & creatures.queryInfo & " standing on " & terrain.ground.queryInfo & ", it seems to be " & creatures.status
            End If
            If Not creatures.equipedItems Is Nothing Then
                queryResponse = queryResponse & ", it is holding " & creatures.equipedItems.queryInfo
            End If
        Else
            If terrain.construct.showChar <> "." And terrain.construct.showChar <> "~" Then 'else try to describe construct and terrain
                queryResponse = "This is " & terrain.construct.queryInfo & " standing on " & terrain.ground.queryInfo
            Else 'else describe terrain
                queryResponse = "This is " & terrain.ground.queryInfo
            End If
        End If
        If Not items Is Nothing Then 'also describe any items
            queryResponse = queryResponse & ", there is " & items.queryInfo & " on the ground"
        End If
        Return queryResponse
    End Function

End Class
