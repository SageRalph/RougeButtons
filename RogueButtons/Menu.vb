Public Class StartupMenu

    Private Sub StartupMenu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Enabled = True
    End Sub

    Private Sub BTNPlay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNPlay.Click
        Me.Enabled = False
        Me.Visible = False
        GUI.Show()
        BTNPlay.Enabled = False
    End Sub

    Private Sub BTNnewWorld_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNnewWorld.Click
        Me.Enabled = False
        GUI.map.newWorld()
        Me.Enabled = True
        BTNPlay.Enabled = True
    End Sub

End Class