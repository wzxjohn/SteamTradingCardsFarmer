Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Net
Imports System.Resources
Imports System.Reflection
Imports System.Threading
Imports System.Runtime.InteropServices

Public Class Main
    Private VERSION As String = "0.2" '定义版本号
    Private SAM_EXE_NAME As String = "SAM.STCF" '定义SAM文件名
    Private SAM_DLL_NAME As String = "SAM.API" '定义SAM API文件名
    Private SAMEXEPath As String = Path.GetTempPath() & SAM_EXE_NAME & ".exe" '定义SAM路径（临时文件夹）
    Private SAMDLLPath As String = Path.GetTempPath() & SAM_DLL_NAME & ".dll" '定义SAM API路径（临时文件夹）
    Private avaliableCardsCount As Integer = 0 '定义可掉落卡片数
    Private avaliableGamesCount As Integer = 0 '定义可掉落游戏数
    Private selectedAppId As String = "753" '定义当前选择游戏的APP ID
    Private farmAppIds() As String '定义当前需要挂机的游戏的APP ID数组
    Private farmingAppNumInArray As Integer = 0 '定义当前挂机的游戏在farmAppId中的标引
    Private farmingState As Boolean = False '定义当前是否在执行挂机
    Private fromProfile As Boolean '定义当前挂机数据来源是否为个人资料页面
    Private reloadingProfile As Boolean = False '定义是否是重载个人资料页
    Private instantRunning As Boolean = False '定义程序实例是否已在运行

    Friend langStrings As New Language '语言资源
    Friend htmlSource As String 'html源码
    Friend cookieContainer As CookieContainer 'CookieContainer
    Friend cookieText As String '从网页中获取到的Cookie文本
    Friend badgesURL As String '个人资料页面地址
    Friend logout As Boolean = False '是否退出

    Private threadGetCardsData As New System.Threading.Thread(AddressOf getCardsData) '分析卡片数据线程
    Private threadGetHtmlSource As New System.Threading.Thread(AddressOf getHtmlSource) '拉取徽章页面线程
    Private threadRunAppInBackground As New System.Threading.Thread(AddressOf runAppInBackground) '拉取徽章页面线程

    '程序的初始化
    Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '判断程序是否已在运行，如果已经在运行就退出程序
        Dim process As Process '定义进程
        If (IsInstanceRunning()) Then
            instantRunning = True
            MsgBox(langStrings.msg_running, , langStrings.title)
            process.GetCurrentProcess().Kill()
            process = Nothing
        End If

        '初始化界面语言
        langStrings.init()
        Me.Text = langStrings.title & " " & VERSION
        TabPageHome.Text = langStrings.t_home
        TabPageAbout.Text = langStrings.t_about
        TabPageLog.Text = langStrings.t_log
        ButtonClear.Text = langStrings.b_clear
        ButtonClipboard.Text = langStrings.b_clipboard
        ButtonInverse.Text = langStrings.b_inverse
        ButtonSelect.Text = langStrings.b_select
        ButtonUnselect.Text = langStrings.b_unselect
        ButtonInverse.Text = langStrings.b_inverse
        ButtonStartStop.Text = langStrings.b_start
        ButtonProfile.Text = langStrings.b_profile
        GroupBoxInfo.Text = langStrings.gb_info
        GroupBoxSelection.Text = langStrings.gb_selection
        GroupBoxSource.Text = langStrings.gb_source
        dgvList.Columns(2).HeaderText = langStrings.dgv_id
        dgvList.Columns(3).HeaderText = langStrings.dgv_title
        dgvList.Columns(4).HeaderText = langStrings.dgv_drop
        dgvList.Columns(5).HeaderText = langStrings.dgv_total
        dgvLog.Columns(0).HeaderText = langStrings.dgv_time
        dgvLog.Columns(1).HeaderText = langStrings.dgv_msg
        LabelCardsTitle.Text = langStrings.l_cards
        LabelGamesTitle.Text = langStrings.l_games
        LabelWarning.Text = langStrings.l_warning
        LabelNotice.Text = langStrings.l_notice
        LabelBlog.Text = langStrings.l_blog
        LabelDonation.Text = langStrings.l_donation
        LinkLabelSCE.Text = langStrings.ll_sce
        LabelGithub.Text = langStrings.l_github
        LinkLabelDownload.Text = langStrings.ll_download
        LabelLoading.Text = langStrings.l_loading

        log(langStrings.msg_launch, False)

        TimerForClip.Interval = 25 * 60 * 1000 '设置剪贴板方式切换游戏时间
        TimerForProfile.Interval = 0.5 * 60 * 1000 '设置个人资料方式切换游戏时间

        dgvList.SelectionMode = DataGridViewSelectionMode.FullRowSelect '设置多行选择
        dgvLog.SelectionMode = DataGridViewSelectionMode.FullRowSelect '设置多行选择

        NotifyIconMain.Icon = My.Resources.Resource.icon_normal '设置托盘图标
        NotifyIconMain.Text = langStrings.title '托盘图标设置标题

        '设置三个重要按钮的Tooltip
        ToolTipProfile.SetToolTip(ButtonProfile, langStrings.tt_profile)
        ToolTipClear.SetToolTip(ButtonClear, langStrings.tt_clear)
        ToolTipClipboard.SetToolTip(ButtonClipboard, langStrings.tt_clipboard)

        Control.CheckForIllegalCrossThreadCalls = False
    End Sub

    '设置托盘图标和事件
    Private Sub Main_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            Me.Visible = False
            Me.NotifyIconMain.Visible = True
        End If
    End Sub
    '退出程序时删除临时文件
    Private Sub Main_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        If instantRunning = False Then '判断是否有实例运行，否则在这里就会把正在运行的SAM也关掉
            '关闭sam
            killSAM()

            '删除临时文件
            Dim FileExists As Boolean
            FileExists = My.Computer.FileSystem.FileExists(SAMEXEPath)
            If FileExists Then
                My.Computer.FileSystem.DeleteFile(SAMEXEPath, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
            End If
            FileExists = My.Computer.FileSystem.FileExists(SAMDLLPath)
            If FileExists Then
                My.Computer.FileSystem.DeleteFile(SAMDLLPath, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
            End If

        End If

        '关掉自身，强制垃圾回收
        Me.Dispose()
        System.GC.Collect()
    End Sub
    '处理通知图标的点击事件
    Private Sub NotifyIcon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NotifyIconMain.Click
        Me.Visible = True
        Me.WindowState = FormWindowState.Normal
    End Sub

    '添加游戏的托管事件
    Public Delegate Sub dgvListAddRowDelgate(dgv As DataGridView, appState As String, appToRun As Boolean, appID As Double, appTitle As String, appCardDrop As Double, appCardTotal As Double)
    Public Sub dgvLogAddRowInvoke(dgv As DataGridView, appState As String, appToRun As Boolean, appID As Double, appTitle As String, appCardDrop As Double, appCardTotal As Double)
        dgv.Rows.Add(appState, appToRun, appID, appTitle, appCardDrop, appCardTotal)
    End Sub

    '获取卡片数据
    Private Sub getCardsData()
        '先关闭所有按键
        enableButton(False)

        LinkLabelSCE.Enabled = False

        '清空info信息
        avaliableCardsCount = 0
        avaliableGamesCount = 0

        '如果html源码数据不存在，则进行一些处理
        If htmlSource Is Nothing Or htmlSource = "" Or htmlSource Is DBNull.Value Then
            TimerForClip.Enabled = False
            TimerForProfile.Enabled = False

            ButtonClipboard.Enabled = True
            ButtonProfile.Enabled = True
            ButtonClear.Enabled = True

            If reloadingProfile Then
                NotifyIconMain.Icon = My.Resources.Resource.icon_error
            End If
            log(langStrings.msg_nocards, True)

            Exit Sub '跳过后续步骤
        End If

        Dim m As Match = Regex.Match(Replace(htmlSource, Chr(10), "", , , vbBinaryCompare), _
                            "progress_info_bold"">(\d+)?.*?card_drop_info_gamebadge_(\d+)_.*?card_drop_info_header.*?(\d+).*?""badge_title"">(.*?)\&n", _
                            RegexOptions.IgnoreCase Or RegexOptions.IgnorePatternWhitespace)

        Dim stillFarm As Boolean = False

        Do While m.Success '如果成功匹配，则进行添加到列表的任务
            If Val(m.Groups(1).Value) <= Val(m.Groups(3).Value) And Val(m.Groups(1).Value) > 0 Then '掉落卡片能掉落卡片时才添加视图中

                '将每个游戏的卡片资料加入到列表中
                '使用invoke进行操作
                dgvList.Invoke(New dgvListAddRowDelgate(AddressOf dgvLogAddRowInvoke), New Object() {dgvList, Nothing, IIf(reloadingProfile, False, True), Val(m.Groups(2).Value), m.Groups(4).Value.ToString().Trim(), Val(m.Groups(1).Value), Val(m.Groups(3).Value)})

                avaliableGamesCount = avaliableGamesCount + 1
                avaliableCardsCount = avaliableCardsCount + Val(m.Groups(1).Value)

                '如果是重读取资料的情况
                If reloadingProfile Then
                    '在之前的打上勾的游戏数组中查询，如果存在则勾上
                    For Each item In farmAppIds
                        If Val(item) = Val(m.Groups(2).Value) Then
                            dgvList.Rows.Item(avaliableGamesCount - 1).Cells(1).Value = True
                        End If
                    Next

                    '如果之前挂机的游戏没挂完，这里设置一个状态
                    If Val(m.Groups(2).Value) = Val(farmAppIds(farmingAppNumInArray)) Then
                        stillFarm = True
                    End If
                End If
            End If
            m = m.NextMatch() '进行下一个匹配
        Loop

        '显示到info窗口
        LabelCards.Text = avaliableCardsCount
        LabelGames.Text = avaliableGamesCount

        '重置变量为空
        htmlSource = Nothing

        '重新将需要挂机的游戏加入到数组中去
        findFarmApp()

        '如果当前能够挂卡的游戏数量为0，则提示并退出；否则将一些按钮设置为可用状态
        If (avaliableGamesCount = 0) Or (farmAppIds.Length <= 1 And farmAppIds(0) Is Nothing) Then

            '关闭定时器
            TimerForClip.Enabled = False
            TimerForProfile.Enabled = False

            ButtonClipboard.Enabled = True
            ButtonProfile.Enabled = True
            ButtonClear.Enabled = True
            ButtonStartStop.Text = langStrings.b_start

            killSAM()

            If reloadingProfile Then
                NotifyIconMain.Icon = My.Resources.Resource.icon_error
            End If
            log(langStrings.msg_nocards, True)
        ElseIf Not reloadingProfile Then '如果现在正在挂卡中（个人资料），跳过改变按钮
            enableButton(True)
            ButtonStartStop.Enabled = True
            selectedAppId = dgvList.Rows.Item(0).Cells(2).Value

            ButtonClipboard.Enabled = True
            ButtonProfile.Enabled = True
            ButtonClear.Enabled = True
        Else '重读取情况
            LinkLabelSCE.Enabled = True
            ButtonStartStop.Enabled = True

            '如果之前的挂机的游戏已经不存在于列表中，则停止SAM，重开新的SAM挂新的游戏
            If Not stillFarm Then
                farmingAppNumInArray = 0
                runSAMInBackground()
            End If

            flagFarmingApp()

            makeCheckboxReadonly(True)
        End If

        reloadingProfile = False '将重新读取个人资料标记为假，完成一次完整的重读取个人资料的过程
        loadingMsg(False) '隐藏读取提示


        '终止两个线程
        Try
            threadGetCardsData.Abort()
            threadGetHtmlSource.Abort()
            threadRunAppInBackground.Abort()
        Catch ThreadAbortException As Exception
        End Try

        '强制回收垃圾
        System.GC.Collect()
    End Sub

    '开始结束按钮
    Private Sub ButtonStartStop_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonStartStop.Click

        '如果没有进行挂卡，则
        If farmingState = False Then
            killSAM() '关闭SAM

            '从当前挂卡列表中找到需要挂卡的数据
            findFarmApp()

            If farmAppIds.Length <= 1 Then
                Return
            End If

            If fromProfile = False Then
                '剪贴板方式
                farmingAppNumInArray = 0
                '如果没记录到这个游戏的App ID，则直接退出
                If farmAppIds(farmingAppNumInArray) Is Nothing Then
                    Exit Sub
                End If
                If farmAppIds.Length > 2 Then '仅在有两个游戏以上的时候才需要定时切换游戏
                    TimerForClip.Enabled = True '打开定时器
                End If
            Else
                '个人资料方式
                farmingAppNumInArray = 0 '个人资料方式只用挂第一个游戏，直到这个游戏挂完
                If farmAppIds(farmingAppNumInArray) Is Nothing Then
                    Exit Sub
                End If
                TimerForProfile.Enabled = True
            End If

            '后台运行SAM
            runSAMInBackground()

            ButtonStartStop.Text = langStrings.b_stop
            log(langStrings.msg_start, False)
            farmingState = True

            '禁止按钮
            ButtonSelect.Enabled = False
            ButtonUnselect.Enabled = False
            ButtonInverse.Enabled = False
            ButtonClipboard.Enabled = False
            ButtonProfile.Enabled = False
            ButtonClear.Enabled = False

            makeCheckboxReadonly(True)
            NotifyIconMain.Icon = My.Resources.Resource.icon_running
        Else
            '终止SAM
            killSAM()

            '强制终止两个线程
            Try
                threadGetCardsData.Abort()
                threadGetHtmlSource.Abort()
                threadRunAppInBackground.Abort()
            Catch ThreadAbortException As Exception
            End Try


            '关闭俩个定时器
            TimerForClip.Enabled = False
            TimerForProfile.Enabled = False

            '清空挂卡标志
            For i = 0 To dgvList.Rows.Count - 1 Step 1
                dgvList.Rows.Item(i).Cells(0).Value = Nothing
            Next

            ButtonStartStop.Text = langStrings.b_start '改变按钮内容
            log(langStrings.msg_stop, False)
            farmingState = False

            '设置按钮状态
            ButtonSelect.Enabled = True
            ButtonUnselect.Enabled = True
            ButtonInverse.Enabled = True
            ButtonClipboard.Enabled = True
            ButtonProfile.Enabled = True
            ButtonClear.Enabled = True

            makeCheckboxReadonly(False)
            NotifyIconMain.Icon = My.Resources.Resource.icon_normal
        End If
    End Sub
    Private Sub clearInfoAndList()
        dgvList.Rows.Clear() '清除游戏列表
        LabelCards.Text = 0
        LabelGames.Text = 0
    End Sub

    '读取剪贴板数据按钮监听
    Private Sub ButtonClipboard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonClipboard.Click
        clearInfoAndList()

        htmlSource = Nothing
        ButtonClipboard.Enabled = False
        ButtonProfile.Enabled = False
        ButtonClear.Enabled = False
        log(langStrings.msg_readclip, False)
        htmlSource = Clipboard.GetText() '从剪贴板获取数据

        threadGetCardsData = New System.Threading.Thread(AddressOf getCardsData)
        threadGetCardsData.IsBackground = True
        threadGetCardsData.Priority = Threading.ThreadPriority.BelowNormal
        threadGetCardsData.Start()
        fromProfile = False '将数据来源设置为剪贴板

    End Sub

    '从个人资料读取
    Private Sub ButtonProfile_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonProfile.Click
        clearInfoAndList()

        ButtonClipboard.Enabled = False
        ButtonProfile.Enabled = False
        ButtonClear.Enabled = False
        enableButton(False)
        ButtonStartStop.Enabled = False
        log(langStrings.msg_readprofile, False)
        fromProfile = True
        Me.Enabled = False
        WebForm.Show()
    End Sub

    '剪贴板挂卡的定时事件
    Protected Sub TimerForClip_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimerForClip.Tick
        '进行下一个游戏的挂卡
        If farmingAppNumInArray >= farmAppIds.Length - 2 Then
            farmingAppNumInArray = 0
        Else
            farmingAppNumInArray = farmingAppNumInArray + 1
        End If
        runSAMInBackground()
    End Sub
    '个人资料挂卡的定时事件
    Protected Sub TimerForProfile_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimerForProfile.Tick
        reloadingProfile = True '设置重载个人资料
        log(langStrings.msg_reload, False)
        dgvList.Rows.Clear() '清除游戏列表
        threadGetHtmlSource = New System.Threading.Thread(AddressOf getHtmlSource)
        threadGetHtmlSource.IsBackground = True
        threadGetHtmlSource.Priority = Threading.ThreadPriority.BelowNormal
        threadGetHtmlSource.Start()
    End Sub

    '设立运行标记程序
    Private Sub flagFarmingApp()
        For i = 0 To dgvList.Rows.Count - 1 Step 1
            If (Trim(Str(dgvList.Rows.Item(i).Cells(2).Value)) = farmAppIds(farmingAppNumInArray)) Then
                dgvList.Rows.Item(i).Cells(0).Value = "●"
            Else
                dgvList.Rows.Item(i).Cells(0).Value = Nothing
            End If
        Next
    End Sub

    '寻找已选择的AppID
    Private Sub findFarmApp()
        '将打上勾的游戏添加到挂卡数组中
        ReDim farmAppIds(0)
        Dim j = 0
        For i = 0 To dgvList.Rows.Count - 1 Step 1
            If (dgvList.Rows.Item(i).Cells(1).Value = True) Then
                ReDim Preserve farmAppIds(j + 1)
                farmAppIds(j) = Trim(Str(dgvList.Rows.Item(i).Cells(2).Value))
                j = j + 1
            End If
        Next
    End Sub

    '后台运行SAM
    Private Sub runSAMInBackground()
        '判断Steam进程是否存在
        If System.Diagnostics.Process.GetProcessesByName("Steam").Length = 0 Then

            enableButton(True)

            '关闭定时器
            TimerForClip.Enabled = False
            TimerForProfile.Enabled = False

            ButtonClipboard.Enabled = True
            ButtonProfile.Enabled = True
            ButtonClear.Enabled = True
            ButtonStartStop.Text = langStrings.b_start

            '不存在则退出
            log(langStrings.msg_nosteam, True)
        Else
            '存在则新开SAM
            '如果SAM不文件已存在，则释放到临时文件夹中
            Dim FileExists As Boolean
            FileExists = My.Computer.FileSystem.FileExists(SAMEXEPath)
            If Not FileExists Then
                Using sCreateMSIFile As New FileStream(SAMEXEPath, FileMode.Create)
                    sCreateMSIFile.Write(My.Resources.Resource.SAM, 0, My.Resources.Resource.SAM.Length)
                End Using
            End If
            FileExists = My.Computer.FileSystem.FileExists(SAMDLLPath)
            If Not FileExists Then
                Using sCreateMSIFile As New FileStream(SAMDLLPath, FileMode.Create)
                    sCreateMSIFile.Write(My.Resources.Resource.SAM_API, 0, My.Resources.Resource.SAM_API.Length)
                End Using
            End If

            threadRunAppInBackground = New System.Threading.Thread(AddressOf runAppInBackground)
            threadRunAppInBackground.IsBackground = True
            threadRunAppInBackground.Priority = Threading.ThreadPriority.BelowNormal
            threadRunAppInBackground.Start()
        End If
    End Sub
    Private Sub runAppInBackground()
        killSAM()

        System.Threading.Thread.Sleep(2000) '给系统留出时间操作

        Dim process As Process '定义进程
        process = New Process
        process.StartInfo.FileName = SAMEXEPath
        process.StartInfo.Arguments = farmAppIds(farmingAppNumInArray)
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        process.Start()
        flagFarmingApp()
        log(farmAppIds(farmingAppNumInArray) & langStrings.msg_appstart, False)

        Try
            threadRunAppInBackground.Abort()
        Catch ThreadAbortException As Exception
        End Try

    End Sub

    '停止SAM
    Private Sub killSAM()
        Dim sProcesses() As System.Diagnostics.Process
        Dim sprocess As System.Diagnostics.Process
        sProcesses = System.Diagnostics.Process.GetProcesses
        For Each sprocess In sProcesses
            If sprocess.ProcessName = SAM_EXE_NAME Then
                sprocess.Kill()
            End If
        Next
    End Sub

    '全选按钮
    Private Sub ButtonSelect_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonSelect.Click
        makeCheckboxChecked(True)
    End Sub
    '全不选按钮
    Private Sub ButtonUnselect_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonUnselect.Click
        makeCheckboxChecked(False)
    End Sub
    '反向选择按钮
    Private Sub ButtonInverse_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonInverse.Click
        For i = 0 To dgvList.Rows.Count - 1 Step 1
            If (dgvList.Rows.Item(i).Cells(1).Value = True) Then
                dgvList.Rows.Item(i).Cells(1).Value = False
            Else
                dgvList.Rows.Item(i).Cells(1).Value = True
            End If
        Next
    End Sub

    '按钮允许禁止子程序
    Private Sub enableButton(ByVal enable As Boolean)
        ButtonStartStop.Enabled = enable
        ButtonSelect.Enabled = enable
        ButtonUnselect.Enabled = enable
        ButtonInverse.Enabled = enable

        LinkLabelSCE.Enabled = enable
    End Sub
    '选择按钮是否打勾子程序
    Private Sub makeCheckboxChecked(ByVal all As Boolean)
        For i = 0 To dgvList.Rows.Count - 1 Step 1
            dgvList.Rows.Item(i).Cells(1).Value = all
        Next
    End Sub
    '选择按钮是否可用子程序
    Private Sub makeCheckboxReadonly(ByVal onlyread As Boolean)
        For i = 0 To dgvList.Rows.Count - 1 Step 1
            dgvList.Rows.Item(i).Cells(1).ReadOnly = onlyread
        Next
    End Sub
    '添加日志的托管事件
    Public Delegate Sub dgvLogAddRowDelgate(dgv As DataGridView, logTime As String, logMsg As String)
    Public Sub dgvLogAddRowInvoke(dgv As DataGridView, logTime As String, logMsg As String)
        dgvLog.Rows.Insert(0, logTime, logMsg)
    End Sub
    '日志记录
    Private Sub log(ByVal msg As String, ByVal showMsgbox As Boolean)
        '使用托管增加行
        dgvLog.Invoke(New dgvLogAddRowDelgate(AddressOf dgvLogAddRowInvoke), New Object() {dgvLog, DateAndTime.DateString & " " & DateAndTime.TimeString, msg})

        If showMsgbox Then
            '窗口没有最小化则MsgBox，否则气泡通知
            If Me.WindowState <> FormWindowState.Minimized Then
                MsgBox(msg, , langStrings.title)
            Else
                NotifyIconMain.ShowBalloonTip(2000, langStrings.title, msg, ToolTipIcon.None)
            End If
        End If
    End Sub
    '监听列表点击事件
    Private Sub dgvList_CellClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles dgvList.CellClick
        If Not e.RowIndex = -1 Then
            selectedAppId = dgvList.Rows(e.RowIndex).Cells(2).Value
        End If
    End Sub

    '判断程序是否重复运行
    Private Declare Function EnumWindows Lib "user32" (ByVal lpEnumFunc As Long, ByVal lParam As Long) As Long
    Private Declare Function GetWindowThreadProcessId Lib "user32" (ByVal hwnd As Long, ByVal lpdwProcessId As Long) As Long
    Private Declare Function ShowWindow Lib "user32" (ByVal hwnd As Long, ByVal nCmdShow As Long) As Long
    Private Function IsInstanceRunning() As Boolean
        Dim current As Process = System.Diagnostics.Process.GetCurrentProcess()
        Dim processes As Process() = System.Diagnostics.Process.GetProcessesByName(current.ProcessName)
        Dim p As Process
        For Each p In processes
            If p.Id <> current.Id Then
                If System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("/", "\") = current.MainModule.FileName Then
                    Return True
                End If
            End If
        Next
        Return False
    End Function

    '拉取html数据
    Private Sub getHtmlSource()
        Dim multiPages As Boolean = True '是否按分页处理
        Dim pageNum As Integer = 1 '页码
        Dim currentHtmlSource As String '当前页面的源码
        htmlSource = Nothing

        While multiPages = True
            Try

                pageNum += 1

                Dim httpReq As System.Net.HttpWebRequest
                Dim httpResp As System.Net.HttpWebResponse
                Dim httpURL As New System.Uri(badgesURL & "/?p=" & pageNum)
                httpReq = CType(WebRequest.Create(httpURL), HttpWebRequest)
                httpReq.CookieContainer = cookieContainer '将之前获得的cookie加入到http请求中
                httpReq.Timeout = 30000 '将超时设置为30秒
                httpReq.Method = "GET"
                httpResp = CType(httpReq.GetResponse(), HttpWebResponse)
                httpReq.KeepAlive = False
                Dim reader As StreamReader = New StreamReader(httpResp.GetResponseStream, System.Text.Encoding.UTF8) '这里将编码设置成为UTF-8，避免特殊符号乱码
                currentHtmlSource = reader.ReadToEnd() '网页源码

                '对取得的html数据进行分析
                Dim mm As Match = Regex.Match(currentHtmlSource.ToString(), _
                    "\?p=" & pageNum, _
                    RegexOptions.IgnoreCase Or RegexOptions.IgnorePatternWhitespace)

                '将分页的HTML拼接在一起
                htmlSource = htmlSource & currentHtmlSource

                '如果没有成功匹配，说明没有下一页了，直接开始处理html数据
                If Not mm.Success Then
                    multiPages = False
                    threadGetCardsData = New System.Threading.Thread(AddressOf getCardsData)
                    threadGetCardsData.IsBackground = True
                    threadGetCardsData.Priority = Threading.ThreadPriority.BelowNormal
                    threadGetCardsData.Start()
                End If

            Catch WebException As Exception
                '如果拉取网页发生了异常
                log(langStrings.msg_loadprofileerr, True)
                If reloadingProfile Then
                    NotifyIconMain.Icon = My.Resources.Resource.icon_error
                End If

                htmlSource = Nothing

                ButtonClipboard.Enabled = True
                ButtonProfile.Enabled = True
                ButtonClear.Enabled = True
                loadingMsg(False)
            End Try
        End While
    End Sub

    '几个LinkLabel的打开链接
    Private Sub LinkLabelSCE_LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles LinkLabelSCE.LinkClicked
        System.Diagnostics.Process.Start("http://www.steamcardexchange.net/index.php?gamepage-appid-" & Trim(Str(selectedAppId)))
    End Sub
    Private Sub LinkLabelBlog_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabelBlog.LinkClicked
        System.Diagnostics.Process.Start(LinkLabelBlog.Text)
    End Sub

    Private Sub LinkLabelDonation_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabelDonation.LinkClicked
        System.Diagnostics.Process.Start(LinkLabelDonation.Text)
    End Sub
    Private Sub LinkLabelSAM_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabelSAM.LinkClicked
        System.Diagnostics.Process.Start(LinkLabelSAM.Text)
    End Sub
    Private Sub LinkLabelGithub_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabelGtihub.LinkClicked
        System.Diagnostics.Process.Start(LabelGithub.Text)
    End Sub
    Private Sub LinkLabelDownload_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabelDownload.LinkClicked
        System.Diagnostics.Process.Start("https://github.com/neverweep/SteamTradingCardsFarmer/raw/master/SteamTradingCardsFarmer/bin/Debug/SteamTradingCardsFarmer.exe")
    End Sub

    '监听网页窗口是否关闭的事件
    Private Sub CheckBoxWebListener_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CheckBoxWebListener.CheckedChanged
        If CheckBoxWebListener.Checked = True Then
            If logout = False Then
                '如果取得了cookie
                If cookieText <> Nothing And cookieText <> "" Then

                    loadingMsg(True)

                    '将其处理，加到CookieContainer中
                    cookieContainer = New CookieContainer()
                    Dim cookies() = cookieText.Split(";")

                    For Each cookie In cookies
                        If (Trim(cookie) <> "") Then
                            Dim cookieNameValue() = cookie.Split("=")
                            Dim ck = New Cookie(cookieNameValue(0).Trim().ToString(), cookieNameValue(1).Trim().ToString())
                            ck.Domain = "steamcommunity.com"
                            cookieContainer.Add(ck)
                        End If
                    Next

                    threadGetHtmlSource = New System.Threading.Thread(AddressOf getHtmlSource)
                    threadGetHtmlSource.IsBackground = True
                    threadGetHtmlSource.Priority = Threading.ThreadPriority.BelowNormal
                    threadGetHtmlSource.Start()
                Else
                    '如果没得到html数据
                    log(langStrings.msg_nocards, True)

                    ButtonClipboard.Enabled = True
                    ButtonProfile.Enabled = True
                    ButtonClear.Enabled = True

                    loadingMsg(False)
                End If

                cookieText = Nothing
            Else
                logout = False
                ButtonClipboard.Enabled = True
                ButtonProfile.Enabled = True
                ButtonClear.Enabled = True
            End If
        End If

        CheckBoxWebListener.Checked = False
    End Sub
    '清除Cookie按钮点击事件
    Private Sub ButtonClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonClear.Click
        clearInfoAndList()

        logout = True
        ButtonClipboard.Enabled = False
        ButtonProfile.Enabled = False
        ButtonClear.Enabled = False
        enableButton(False)
        ButtonStartStop.Enabled = False
        log(langStrings.msg_readprofile, False)
        Me.Enabled = False
        WebForm.Show()
    End Sub

    Private Sub loadingMsg(show As Boolean)
        PanelLoading.Visible = show
        ProgressBarLoading.Style = IIf(show, ProgressBarStyle.Marquee, ProgressBarStyle.Blocks)
    End Sub
End Class
