<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GUI
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
        Me.Gamewindow = New System.Windows.Forms.GroupBox
        Me.focusdivert = New System.Windows.Forms.Label
        Me.TBGamelog = New System.Windows.Forms.RichTextBox
        Me.TBMenu = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'Gamewindow
        '
        Me.Gamewindow.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Gamewindow.Location = New System.Drawing.Point(12, 3)
        Me.Gamewindow.Name = "Gamewindow"
        Me.Gamewindow.Size = New System.Drawing.Size(845, 665)
        Me.Gamewindow.TabIndex = 0
        Me.Gamewindow.TabStop = False
        '
        'focusdivert
        '
        Me.focusdivert.AutoSize = True
        Me.focusdivert.Enabled = False
        Me.focusdivert.Location = New System.Drawing.Point(1049, 910)
        Me.focusdivert.Name = "focusdivert"
        Me.focusdivert.Size = New System.Drawing.Size(0, 17)
        Me.focusdivert.TabIndex = 0
        Me.focusdivert.Visible = False
        '
        'TBGamelog
        '
        Me.TBGamelog.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TBGamelog.BackColor = System.Drawing.SystemColors.Control
        Me.TBGamelog.HideSelection = False
        Me.TBGamelog.Location = New System.Drawing.Point(12, 681)
        Me.TBGamelog.Name = "TBGamelog"
        Me.TBGamelog.ReadOnly = True
        Me.TBGamelog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical
        Me.TBGamelog.Size = New System.Drawing.Size(711, 125)
        Me.TBGamelog.TabIndex = 3
        Me.TBGamelog.Text = ""
        '
        'TBMenu
        '
        Me.TBMenu.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TBMenu.Enabled = False
        Me.TBMenu.Location = New System.Drawing.Point(729, 681)
        Me.TBMenu.Multiline = True
        Me.TBMenu.Name = "TBMenu"
        Me.TBMenu.Size = New System.Drawing.Size(128, 125)
        Me.TBMenu.TabIndex = 4
        Me.TBMenu.Text = "Query : Click" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Move  : Numpad" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Wait    : ." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(869, 818)
        Me.Controls.Add(Me.TBGamelog)
        Me.Controls.Add(Me.TBMenu)
        Me.Controls.Add(Me.focusdivert)
        Me.Controls.Add(Me.Gamewindow)
        Me.Name = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Gamewindow As System.Windows.Forms.GroupBox
    Friend WithEvents focusdivert As System.Windows.Forms.Label
    Friend WithEvents TBGamelog As System.Windows.Forms.RichTextBox
    Friend WithEvents TBMenu As System.Windows.Forms.TextBox

End Class
