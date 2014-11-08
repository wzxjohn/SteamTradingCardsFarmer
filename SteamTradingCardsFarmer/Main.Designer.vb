<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
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

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ButtonSelect = New System.Windows.Forms.Button()
        Me.ButtonUnselect = New System.Windows.Forms.Button()
        Me.ButtonInverse = New System.Windows.Forms.Button()
        Me.ButtonProfile = New System.Windows.Forms.Button()
        Me.ButtonClipboard = New System.Windows.Forms.Button()
        Me.TabControlMain = New System.Windows.Forms.TabControl()
        Me.TabPageHome = New System.Windows.Forms.TabPage()
        Me.LabelLoading = New System.Windows.Forms.Label()
        Me.GroupBoxSource = New System.Windows.Forms.GroupBox()
        Me.ButtonClear = New System.Windows.Forms.Button()
        Me.GroupBoxSelection = New System.Windows.Forms.GroupBox()
        Me.ButtonStartStop = New System.Windows.Forms.Button()
        Me.GroupBoxInfo = New System.Windows.Forms.GroupBox()
        Me.LinkLabelSCE = New System.Windows.Forms.LinkLabel()
        Me.LabelGamesTitle = New System.Windows.Forms.Label()
        Me.LabelCardsTitle = New System.Windows.Forms.Label()
        Me.LabelGames = New System.Windows.Forms.Label()
        Me.LabelCards = New System.Windows.Forms.Label()
        Me.dgvList = New System.Windows.Forms.DataGridView()
        Me.ColFarming = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColCheckbox = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ColAppId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColTitle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColDrop = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabPageAbout = New System.Windows.Forms.TabPage()
        Me.LinkLabelDownload = New System.Windows.Forms.LinkLabel()
        Me.LabelGithub = New System.Windows.Forms.Label()
        Me.LinkLabelGtihub = New System.Windows.Forms.LinkLabel()
        Me.LabelSAM = New System.Windows.Forms.Label()
        Me.LinkLabelSAM = New System.Windows.Forms.LinkLabel()
        Me.LabelBlog = New System.Windows.Forms.Label()
        Me.LinkLabelBlog = New System.Windows.Forms.LinkLabel()
        Me.LabelDonation = New System.Windows.Forms.Label()
        Me.LinkLabelDonation = New System.Windows.Forms.LinkLabel()
        Me.LabelWarning = New System.Windows.Forms.Label()
        Me.LabelNotice = New System.Windows.Forms.Label()
        Me.TabPageLog = New System.Windows.Forms.TabPage()
        Me.dgvLog = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TimerForClip = New System.Windows.Forms.Timer(Me.components)
        Me.TimerForProfile = New System.Windows.Forms.Timer(Me.components)
        Me.NotifyIconMain = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.CheckBoxWebListener = New System.Windows.Forms.CheckBox()
        Me.ToolTipProfile = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolTipClear = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolTipClipboard = New System.Windows.Forms.ToolTip(Me.components)
        Me.ProgressBarLoading = New System.Windows.Forms.ProgressBar()
        Me.PanelLoading = New System.Windows.Forms.Panel()
        Me.Form1BindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TabControlMain.SuspendLayout()
        Me.TabPageHome.SuspendLayout()
        Me.GroupBoxSource.SuspendLayout()
        Me.GroupBoxSelection.SuspendLayout()
        Me.GroupBoxInfo.SuspendLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageAbout.SuspendLayout()
        Me.TabPageLog.SuspendLayout()
        CType(Me.dgvLog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelLoading.SuspendLayout()
        CType(Me.Form1BindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ButtonSelect
        '
        resources.ApplyResources(Me.ButtonSelect, "ButtonSelect")
        Me.ButtonSelect.Name = "ButtonSelect"
        Me.ButtonSelect.UseVisualStyleBackColor = True
        '
        'ButtonUnselect
        '
        resources.ApplyResources(Me.ButtonUnselect, "ButtonUnselect")
        Me.ButtonUnselect.Name = "ButtonUnselect"
        Me.ButtonUnselect.UseVisualStyleBackColor = True
        '
        'ButtonInverse
        '
        resources.ApplyResources(Me.ButtonInverse, "ButtonInverse")
        Me.ButtonInverse.Name = "ButtonInverse"
        Me.ButtonInverse.UseVisualStyleBackColor = True
        '
        'ButtonProfile
        '
        resources.ApplyResources(Me.ButtonProfile, "ButtonProfile")
        Me.ButtonProfile.Name = "ButtonProfile"
        Me.ButtonProfile.UseVisualStyleBackColor = True
        '
        'ButtonClipboard
        '
        resources.ApplyResources(Me.ButtonClipboard, "ButtonClipboard")
        Me.ButtonClipboard.Name = "ButtonClipboard"
        Me.ButtonClipboard.UseVisualStyleBackColor = True
        '
        'TabControlMain
        '
        Me.TabControlMain.Controls.Add(Me.TabPageHome)
        Me.TabControlMain.Controls.Add(Me.TabPageAbout)
        Me.TabControlMain.Controls.Add(Me.TabPageLog)
        resources.ApplyResources(Me.TabControlMain, "TabControlMain")
        Me.TabControlMain.Name = "TabControlMain"
        Me.TabControlMain.SelectedIndex = 0
        Me.TabControlMain.Tag = ""
        '
        'TabPageHome
        '
        Me.TabPageHome.Controls.Add(Me.PanelLoading)
        Me.TabPageHome.Controls.Add(Me.GroupBoxSource)
        Me.TabPageHome.Controls.Add(Me.GroupBoxSelection)
        Me.TabPageHome.Controls.Add(Me.ButtonStartStop)
        Me.TabPageHome.Controls.Add(Me.GroupBoxInfo)
        Me.TabPageHome.Controls.Add(Me.dgvList)
        resources.ApplyResources(Me.TabPageHome, "TabPageHome")
        Me.TabPageHome.Name = "TabPageHome"
        Me.TabPageHome.UseVisualStyleBackColor = True
        '
        'LabelLoading
        '
        Me.LabelLoading.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.LabelLoading, "LabelLoading")
        Me.LabelLoading.Name = "LabelLoading"
        '
        'GroupBoxSource
        '
        Me.GroupBoxSource.Controls.Add(Me.ButtonClear)
        Me.GroupBoxSource.Controls.Add(Me.ButtonProfile)
        Me.GroupBoxSource.Controls.Add(Me.ButtonClipboard)
        resources.ApplyResources(Me.GroupBoxSource, "GroupBoxSource")
        Me.GroupBoxSource.Name = "GroupBoxSource"
        Me.GroupBoxSource.TabStop = False
        '
        'ButtonClear
        '
        resources.ApplyResources(Me.ButtonClear, "ButtonClear")
        Me.ButtonClear.Name = "ButtonClear"
        Me.ButtonClear.UseVisualStyleBackColor = True
        '
        'GroupBoxSelection
        '
        Me.GroupBoxSelection.Controls.Add(Me.ButtonSelect)
        Me.GroupBoxSelection.Controls.Add(Me.ButtonInverse)
        Me.GroupBoxSelection.Controls.Add(Me.ButtonUnselect)
        resources.ApplyResources(Me.GroupBoxSelection, "GroupBoxSelection")
        Me.GroupBoxSelection.Name = "GroupBoxSelection"
        Me.GroupBoxSelection.TabStop = False
        '
        'ButtonStartStop
        '
        Me.ButtonStartStop.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.ButtonStartStop, "ButtonStartStop")
        Me.ButtonStartStop.Name = "ButtonStartStop"
        Me.ButtonStartStop.UseVisualStyleBackColor = False
        '
        'GroupBoxInfo
        '
        Me.GroupBoxInfo.Controls.Add(Me.LinkLabelSCE)
        Me.GroupBoxInfo.Controls.Add(Me.LabelGamesTitle)
        Me.GroupBoxInfo.Controls.Add(Me.LabelCardsTitle)
        Me.GroupBoxInfo.Controls.Add(Me.LabelGames)
        Me.GroupBoxInfo.Controls.Add(Me.LabelCards)
        resources.ApplyResources(Me.GroupBoxInfo, "GroupBoxInfo")
        Me.GroupBoxInfo.Name = "GroupBoxInfo"
        Me.GroupBoxInfo.TabStop = False
        '
        'LinkLabelSCE
        '
        resources.ApplyResources(Me.LinkLabelSCE, "LinkLabelSCE")
        Me.LinkLabelSCE.Name = "LinkLabelSCE"
        Me.LinkLabelSCE.TabStop = True
        '
        'LabelGamesTitle
        '
        resources.ApplyResources(Me.LabelGamesTitle, "LabelGamesTitle")
        Me.LabelGamesTitle.Name = "LabelGamesTitle"
        '
        'LabelCardsTitle
        '
        resources.ApplyResources(Me.LabelCardsTitle, "LabelCardsTitle")
        Me.LabelCardsTitle.Name = "LabelCardsTitle"
        '
        'LabelGames
        '
        resources.ApplyResources(Me.LabelGames, "LabelGames")
        Me.LabelGames.Name = "LabelGames"
        '
        'LabelCards
        '
        resources.ApplyResources(Me.LabelCards, "LabelCards")
        Me.LabelCards.Name = "LabelCards"
        '
        'dgvList
        '
        Me.dgvList.AllowUserToAddRows = False
        Me.dgvList.AllowUserToDeleteRows = False
        Me.dgvList.AllowUserToResizeColumns = False
        Me.dgvList.AllowUserToResizeRows = False
        Me.dgvList.BackgroundColor = System.Drawing.SystemColors.Window
        Me.dgvList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("微软雅黑", 9.0!)
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        resources.ApplyResources(Me.dgvList, "dgvList")
        Me.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColFarming, Me.ColCheckbox, Me.ColAppId, Me.ColTitle, Me.ColDrop, Me.ColTotal})
        Me.dgvList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.dgvList.EnableHeadersVisualStyles = False
        Me.dgvList.MultiSelect = False
        Me.dgvList.Name = "dgvList"
        Me.dgvList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.Font = New System.Drawing.Font("微软雅黑", 9.0!)
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvList.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.dgvList.RowHeadersVisible = False
        Me.dgvList.RowTemplate.Height = 23
        Me.dgvList.ShowCellErrors = False
        Me.dgvList.ShowCellToolTips = False
        Me.dgvList.ShowEditingIcon = False
        Me.dgvList.ShowRowErrors = False
        '
        'ColFarming
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.ColFarming.DefaultCellStyle = DataGridViewCellStyle2
        Me.ColFarming.FillWeight = 20.0!
        resources.ApplyResources(Me.ColFarming, "ColFarming")
        Me.ColFarming.Name = "ColFarming"
        Me.ColFarming.ReadOnly = True
        Me.ColFarming.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ColFarming.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ColCheckbox
        '
        Me.ColCheckbox.FalseValue = "false"
        Me.ColCheckbox.FillWeight = 20.0!
        resources.ApplyResources(Me.ColCheckbox, "ColCheckbox")
        Me.ColCheckbox.IndeterminateValue = "false"
        Me.ColCheckbox.Name = "ColCheckbox"
        Me.ColCheckbox.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ColCheckbox.TrueValue = "true"
        '
        'ColAppId
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.ColAppId.DefaultCellStyle = DataGridViewCellStyle3
        Me.ColAppId.FillWeight = 55.0!
        resources.ApplyResources(Me.ColAppId, "ColAppId")
        Me.ColAppId.Name = "ColAppId"
        Me.ColAppId.ReadOnly = True
        Me.ColAppId.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ColAppId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ColTitle
        '
        Me.ColTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.ColTitle.FillWeight = 250.0!
        resources.ApplyResources(Me.ColTitle, "ColTitle")
        Me.ColTitle.Name = "ColTitle"
        Me.ColTitle.ReadOnly = True
        Me.ColTitle.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ColTitle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ColDrop
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.ColDrop.DefaultCellStyle = DataGridViewCellStyle4
        Me.ColDrop.FillWeight = 50.0!
        resources.ApplyResources(Me.ColDrop, "ColDrop")
        Me.ColDrop.Name = "ColDrop"
        Me.ColDrop.ReadOnly = True
        Me.ColDrop.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ColDrop.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ColTotal
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.ColTotal.DefaultCellStyle = DataGridViewCellStyle5
        Me.ColTotal.FillWeight = 50.0!
        resources.ApplyResources(Me.ColTotal, "ColTotal")
        Me.ColTotal.Name = "ColTotal"
        Me.ColTotal.ReadOnly = True
        Me.ColTotal.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ColTotal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'TabPageAbout
        '
        Me.TabPageAbout.Controls.Add(Me.LinkLabelDownload)
        Me.TabPageAbout.Controls.Add(Me.LabelGithub)
        Me.TabPageAbout.Controls.Add(Me.LinkLabelGtihub)
        Me.TabPageAbout.Controls.Add(Me.LabelSAM)
        Me.TabPageAbout.Controls.Add(Me.LinkLabelSAM)
        Me.TabPageAbout.Controls.Add(Me.LabelBlog)
        Me.TabPageAbout.Controls.Add(Me.LinkLabelBlog)
        Me.TabPageAbout.Controls.Add(Me.LabelDonation)
        Me.TabPageAbout.Controls.Add(Me.LinkLabelDonation)
        Me.TabPageAbout.Controls.Add(Me.LabelWarning)
        Me.TabPageAbout.Controls.Add(Me.LabelNotice)
        resources.ApplyResources(Me.TabPageAbout, "TabPageAbout")
        Me.TabPageAbout.Name = "TabPageAbout"
        Me.TabPageAbout.UseVisualStyleBackColor = True
        '
        'LinkLabelDownload
        '
        resources.ApplyResources(Me.LinkLabelDownload, "LinkLabelDownload")
        Me.LinkLabelDownload.Name = "LinkLabelDownload"
        Me.LinkLabelDownload.TabStop = True
        '
        'LabelGithub
        '
        resources.ApplyResources(Me.LabelGithub, "LabelGithub")
        Me.LabelGithub.Name = "LabelGithub"
        '
        'LinkLabelGtihub
        '
        resources.ApplyResources(Me.LinkLabelGtihub, "LinkLabelGtihub")
        Me.LinkLabelGtihub.Name = "LinkLabelGtihub"
        Me.LinkLabelGtihub.TabStop = True
        '
        'LabelSAM
        '
        resources.ApplyResources(Me.LabelSAM, "LabelSAM")
        Me.LabelSAM.Name = "LabelSAM"
        '
        'LinkLabelSAM
        '
        resources.ApplyResources(Me.LinkLabelSAM, "LinkLabelSAM")
        Me.LinkLabelSAM.Name = "LinkLabelSAM"
        Me.LinkLabelSAM.TabStop = True
        '
        'LabelBlog
        '
        resources.ApplyResources(Me.LabelBlog, "LabelBlog")
        Me.LabelBlog.Name = "LabelBlog"
        '
        'LinkLabelBlog
        '
        resources.ApplyResources(Me.LinkLabelBlog, "LinkLabelBlog")
        Me.LinkLabelBlog.Name = "LinkLabelBlog"
        Me.LinkLabelBlog.TabStop = True
        '
        'LabelDonation
        '
        resources.ApplyResources(Me.LabelDonation, "LabelDonation")
        Me.LabelDonation.Name = "LabelDonation"
        '
        'LinkLabelDonation
        '
        resources.ApplyResources(Me.LinkLabelDonation, "LinkLabelDonation")
        Me.LinkLabelDonation.Name = "LinkLabelDonation"
        Me.LinkLabelDonation.TabStop = True
        '
        'LabelWarning
        '
        resources.ApplyResources(Me.LabelWarning, "LabelWarning")
        Me.LabelWarning.ForeColor = System.Drawing.Color.Red
        Me.LabelWarning.Name = "LabelWarning"
        '
        'LabelNotice
        '
        resources.ApplyResources(Me.LabelNotice, "LabelNotice")
        Me.LabelNotice.Name = "LabelNotice"
        '
        'TabPageLog
        '
        Me.TabPageLog.Controls.Add(Me.dgvLog)
        resources.ApplyResources(Me.TabPageLog, "TabPageLog")
        Me.TabPageLog.Name = "TabPageLog"
        Me.TabPageLog.UseVisualStyleBackColor = True
        '
        'dgvLog
        '
        Me.dgvLog.AllowUserToAddRows = False
        Me.dgvLog.AllowUserToDeleteRows = False
        Me.dgvLog.AllowUserToResizeColumns = False
        Me.dgvLog.AllowUserToResizeRows = False
        Me.dgvLog.BackgroundColor = System.Drawing.SystemColors.Window
        Me.dgvLog.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle7.Font = New System.Drawing.Font("微软雅黑", 9.0!)
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvLog.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        resources.ApplyResources(Me.dgvLog, "dgvLog")
        Me.dgvLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvLog.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4})
        Me.dgvLog.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.dgvLog.EnableHeadersVisualStyles = False
        Me.dgvLog.MultiSelect = False
        Me.dgvLog.Name = "dgvLog"
        Me.dgvLog.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.dgvLog.RowHeadersVisible = False
        Me.dgvLog.RowTemplate.Height = 23
        Me.dgvLog.ShowCellErrors = False
        Me.dgvLog.ShowCellToolTips = False
        Me.dgvLog.ShowEditingIcon = False
        Me.dgvLog.ShowRowErrors = False
        '
        'DataGridViewTextBoxColumn3
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle8
        Me.DataGridViewTextBoxColumn3.FillWeight = 150.0!
        resources.ApplyResources(Me.DataGridViewTextBoxColumn3, "DataGridViewTextBoxColumn3")
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'DataGridViewTextBoxColumn4
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.DataGridViewTextBoxColumn4.DefaultCellStyle = DataGridViewCellStyle9
        Me.DataGridViewTextBoxColumn4.FillWeight = 555.0!
        resources.ApplyResources(Me.DataGridViewTextBoxColumn4, "DataGridViewTextBoxColumn4")
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'TimerForClip
        '
        '
        'TimerForProfile
        '
        '
        'NotifyIconMain
        '
        resources.ApplyResources(Me.NotifyIconMain, "NotifyIconMain")
        '
        'CheckBoxWebListener
        '
        resources.ApplyResources(Me.CheckBoxWebListener, "CheckBoxWebListener")
        Me.CheckBoxWebListener.Name = "CheckBoxWebListener"
        Me.CheckBoxWebListener.UseVisualStyleBackColor = True
        '
        'ProgressBarLoading
        '
        Me.ProgressBarLoading.ForeColor = System.Drawing.Color.White
        resources.ApplyResources(Me.ProgressBarLoading, "ProgressBarLoading")
        Me.ProgressBarLoading.Name = "ProgressBarLoading"
        '
        'PanelLoading
        '
        Me.PanelLoading.BackColor = System.Drawing.Color.White
        Me.PanelLoading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelLoading.Controls.Add(Me.LabelLoading)
        Me.PanelLoading.Controls.Add(Me.ProgressBarLoading)
        resources.ApplyResources(Me.PanelLoading, "PanelLoading")
        Me.PanelLoading.Name = "PanelLoading"
        '
        'Form1BindingSource
        '
        Me.Form1BindingSource.DataSource = GetType(SteamTradingCardsFarmer.Main)
        '
        'Main
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.CheckBoxWebListener)
        Me.Controls.Add(Me.TabControlMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "Main"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.TabControlMain.ResumeLayout(False)
        Me.TabPageHome.ResumeLayout(False)
        Me.GroupBoxSource.ResumeLayout(False)
        Me.GroupBoxSelection.ResumeLayout(False)
        Me.GroupBoxInfo.ResumeLayout(False)
        Me.GroupBoxInfo.PerformLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageAbout.ResumeLayout(False)
        Me.TabPageAbout.PerformLayout()
        Me.TabPageLog.ResumeLayout(False)
        CType(Me.dgvLog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelLoading.ResumeLayout(False)
        CType(Me.Form1BindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonSelect As System.Windows.Forms.Button
    Friend WithEvents ButtonUnselect As System.Windows.Forms.Button
    Friend WithEvents ButtonInverse As System.Windows.Forms.Button
    Friend WithEvents ButtonProfile As System.Windows.Forms.Button
    Friend WithEvents ButtonClipboard As System.Windows.Forms.Button
    Friend WithEvents TabControlMain As System.Windows.Forms.TabControl
    Friend WithEvents TabPageHome As System.Windows.Forms.TabPage
    Friend WithEvents TabPageAbout As System.Windows.Forms.TabPage
    Friend WithEvents ButtonStartStop As System.Windows.Forms.Button
    Friend WithEvents Form1BindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents dgvList As System.Windows.Forms.DataGridView
    Friend WithEvents LabelGamesTitle As System.Windows.Forms.Label
    Friend WithEvents LabelCardsTitle As System.Windows.Forms.Label
    Friend WithEvents LabelGames As System.Windows.Forms.Label
    Friend WithEvents LabelCards As System.Windows.Forms.Label
    Friend WithEvents dgvLog As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBoxInfo As System.Windows.Forms.GroupBox
    Friend WithEvents TimerForClip As System.Windows.Forms.Timer
    Friend WithEvents GroupBoxSelection As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBoxSource As System.Windows.Forms.GroupBox
    Friend WithEvents LinkLabelSCE As System.Windows.Forms.LinkLabel
    Friend WithEvents TimerForProfile As System.Windows.Forms.Timer
    Friend WithEvents NotifyIconMain As System.Windows.Forms.NotifyIcon
    Friend WithEvents TabPageLog As System.Windows.Forms.TabPage
    Friend WithEvents LabelWarning As System.Windows.Forms.Label
    Friend WithEvents LabelNotice As System.Windows.Forms.Label
    Friend WithEvents LabelBlog As System.Windows.Forms.Label
    Friend WithEvents LinkLabelBlog As System.Windows.Forms.LinkLabel
    Friend WithEvents LabelDonation As System.Windows.Forms.Label
    Friend WithEvents LinkLabelDonation As System.Windows.Forms.LinkLabel
    Friend WithEvents LabelSAM As System.Windows.Forms.Label
    Friend WithEvents LinkLabelSAM As System.Windows.Forms.LinkLabel
    Friend WithEvents CheckBoxWebListener As System.Windows.Forms.CheckBox
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ButtonClear As System.Windows.Forms.Button
    Friend WithEvents LabelGithub As System.Windows.Forms.Label
    Friend WithEvents LinkLabelGtihub As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabelDownload As System.Windows.Forms.LinkLabel
    Friend WithEvents ColFarming As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColCheckbox As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents ColAppId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColTitle As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColDrop As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColTotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LabelLoading As System.Windows.Forms.Label
    Friend WithEvents ToolTipProfile As System.Windows.Forms.ToolTip
    Friend WithEvents ToolTipClear As System.Windows.Forms.ToolTip
    Friend WithEvents ToolTipClipboard As System.Windows.Forms.ToolTip
    Friend WithEvents ProgressBarLoading As System.Windows.Forms.ProgressBar
    Friend WithEvents PanelLoading As System.Windows.Forms.Panel

End Class
