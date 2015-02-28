<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StartupMenu
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.BTNnewWorld = New System.Windows.Forms.Button
        Me.BTNPlay = New System.Windows.Forms.Button
        Me.BTNLoad = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'BTNnewWorld
        '
        Me.BTNnewWorld.Location = New System.Drawing.Point(12, 12)
        Me.BTNnewWorld.Name = "BTNnewWorld"
        Me.BTNnewWorld.Size = New System.Drawing.Size(136, 38)
        Me.BTNnewWorld.TabIndex = 0
        Me.BTNnewWorld.Text = "New World"
        Me.BTNnewWorld.UseVisualStyleBackColor = True
        '
        'BTNPlay
        '
        Me.BTNPlay.Enabled = False
        Me.BTNPlay.Location = New System.Drawing.Point(12, 100)
        Me.BTNPlay.Name = "BTNPlay"
        Me.BTNPlay.Size = New System.Drawing.Size(136, 38)
        Me.BTNPlay.TabIndex = 1
        Me.BTNPlay.Text = "Play"
        Me.BTNPlay.UseVisualStyleBackColor = True
        '
        'BTNLoad
        '
        Me.BTNLoad.Enabled = False
        Me.BTNLoad.Location = New System.Drawing.Point(12, 56)
        Me.BTNLoad.Name = "BTNLoad"
        Me.BTNLoad.Size = New System.Drawing.Size(136, 38)
        Me.BTNLoad.TabIndex = 2
        Me.BTNLoad.Text = "Load World"
        Me.BTNLoad.UseVisualStyleBackColor = True
        '
        'StartupMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(156, 148)
        Me.Controls.Add(Me.BTNLoad)
        Me.Controls.Add(Me.BTNPlay)
        Me.Controls.Add(Me.BTNnewWorld)
        Me.Enabled = False
        Me.Name = "StartupMenu"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BTNnewWorld As System.Windows.Forms.Button
    Friend WithEvents BTNPlay As System.Windows.Forms.Button
    Friend WithEvents BTNLoad As System.Windows.Forms.Button
End Class
