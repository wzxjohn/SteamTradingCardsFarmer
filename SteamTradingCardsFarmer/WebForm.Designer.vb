<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WebForm
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
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

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意:  以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WebForm))
        Me.WebBrowser = New System.Windows.Forms.WebBrowser()
        Me.SuspendLayout()
        '
        'WebBrowser
        '
        resources.ApplyResources(Me.WebBrowser, "WebBrowser")
        Me.WebBrowser.Name = "WebBrowser"
        Me.WebBrowser.ScrollBarsEnabled = False
        '
        'WebForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.WebBrowser)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "WebForm"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents WebBrowser As System.Windows.Forms.WebBrowser
End Class
