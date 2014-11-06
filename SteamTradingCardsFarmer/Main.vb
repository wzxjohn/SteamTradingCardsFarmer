Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Net
Imports System.Resources
Imports System.Reflection
Imports System.Threading
Imports System.Globalization
Imports System.Runtime.InteropServices

Public Class Main
    Private VERSION As String = "0.1" '定义版本号
    Private SAM_EXE_NAME As String = "SAM.Game" '定义SAM文件名
    Private SAM_DLL_NAME As String = "SAM.API" '定义SAM API文件名
    Private SAMEXEPath As String = Path.GetTempPath() & SAM_EXE_NAME & ".exe" '定义SAM路径（临时文件夹）
    Private SAMDLLPath As String = Path.GetTempPath() & SAM_DLL_NAME & ".dll" '定义SAM API路径（临时文件夹）
    Private avaliableCards As Integer = 0 '定义可掉落卡片数
    Private avaliableGames As Integer = 0 '定义可掉落游戏数
    Private selectedAppId As String = "753" '定义当前选择游戏的APP ID
    Private farmAppId() As String '定义当前需要挂机的游戏的APP ID数组
    Private currentAppNum As Integer = 0 '定义当前挂机的游戏在farmAppId中的标引
    Private isFarming As Boolean = False '定义当前是否在执行挂机
    Private fromProfile As Boolean '定义当前挂机数据来源是否为个人资料页面
    Private reloadWeb As Boolean = False '定义是否是重载个人资料页
    Private instantRunning As Boolean = False '定义程序实例是否已在运行
    Private process As Process '定义进程

    Friend language As New Language '语言资源
    Friend htmlSource As String 'html源码
    Friend cc As CookieContainer 'CookieContainer
    Friend cookieText As String '从网页中获取到的Cookie文本
    Friend badgesURL As String '个人资料页面地址

    Private threadGetCardsData As New System.Threading.Thread(AddressOf getCardsData) '分析卡片数据线程
    Private threadGetHtmlSource As New System.Threading.Thread(AddressOf getHtmlSource) '拉取徽章页面线程

    '程序的初始化
    Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '判断程序是否已在运行，如果已经在运行就退出程序
        If (IsInstanceRunning()) Then
            instantRunning = True
            MsgBox(language.msg_running, , language.title)
            process.GetCurrentProcess().Kill()
        End If

        '初始化界面语言
        language.init()
        Me.Text = language.title & " " & VERSION
        TabPageHome.Text = language.t_home
        TabPageAbout.Text = language.t_about
        TabPageLog.Text = language.t_log
        ButtonClear.Text = language.b_clear
        ButtonClipboard.Text = language.b_clipboard
        ButtonInverse.Text = language.b_inverse
        ButtonSelect.Text = language.b_select
        ButtonUnselect.Text = language.b_unselect
        ButtonInverse.Text = language.b_inverse
        ButtonStartStop.Text = language.b_start
        ButtonProfile.Text = language.b_profile
        GroupBoxInfo.Text = language.gb_info
        GroupBoxSelection.Text = language.gb_selection
        GroupBoxSource.Text = language.gb_source
        dgvList.Columns(2).HeaderText = language.dgv_id
        dgvList.Columns(3).HeaderText = language.dgv_title
        dgvList.Columns(4).HeaderText = language.dgv_drop
        dgvList.Columns(5).HeaderText = language.dgv_total
        dgvLog.Columns(0).HeaderText = language.dgv_time
        dgvLog.Columns(1).HeaderText = language.dgv_msg
        LabelCardsTitle.Text = language.l_cards
        LabelGamesTitle.Text = language.l_games
        LabelWarning.Text = language.l_warning
        LabelNotice.Text = language.l_notice
        LabelBlog.Text = language.l_blog
        LabelDonation.Text = language.l_donation
        LinkLabelSCE.Text = language.ll_sce
        LabelGithub.Text = language.l_github
        LinkLabelDownload.Text = language.ll_download

        log(language.msg_launch, False)

        TimerForClip.Interval = 60 * 25 * 1000 '设置剪贴板方式切换游戏时间
        TimerForProfile.Interval = 10000 '60 * 15 * 1000 '设置个人资料方式切换游戏时间

        dgvList.SelectionMode = DataGridViewSelectionMode.FullRowSelect '设置多行选择
        dgvLog.SelectionMode = DataGridViewSelectionMode.FullRowSelect '设置多行选择

        NotifyIconMain.Icon = Me.Icon '将托盘图报设置为与程序自身一致
        NotifyIconMain.Text = language.title

        Control.CheckForIllegalCrossThreadCalls = False
    End Sub

    '设置托盘图标和事件
    Private Sub Main_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            Me.Visible = False
            Me.NotifyIconMain.Visible = True
        End If
    End Sub
    Private Sub NotifyIcon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NotifyIconMain.Click
        Me.Visible = True
        Me.WindowState = FormWindowState.Normal
    End Sub

    '退出程序时删除临时文件
    Private Sub Main_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        If instantRunning = False Then '判断是否有实例运行，否则在这里就会把正在运行的SAM也关掉
            '关闭sam
            stopSAM()

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

    '获取卡片数据
    Private Sub getCardsData()
        '先关闭所有按键
        btnEnable(False)

        dgvList.Rows.Clear() '清除游戏列表
        LinkLabelSCE.Enabled = False

        '清空info信息
        avaliableCards = 0
        avaliableGames = 0

        '如果html源码数据不存在，则进行一些处理
        If htmlSource Is Nothing Or htmlSource = "" Or htmlSource Is DBNull.Value Then
            btnEnable(False)
            log(language.msg_nocards, True)

            TimerForClip.Enabled = False
            TimerForProfile.Enabled = False

            ButtonClipboard.Enabled = True
            ButtonProfile.Enabled = True
            ButtonClear.Enabled = True
            Exit Sub '跳过后续步骤
        End If

        Dim m As Match = Regex.Match(Replace(htmlSource, Chr(10), "", , , vbBinaryCompare), _
                            "badge_row_overlay"" .*?gamecards\/(\d+)\/.*?progress_info_bold"">(\d+)?.*?card_drop_info_header.*?(\d+).*?badge_title"">(.*?)\&nbsp;.*?style=""clear", _
                            RegexOptions.IgnoreCase Or RegexOptions.IgnorePatternWhitespace)

        Dim appToKeep As Boolean = False

        Do While m.Success '如果成功匹配，则进行添加到列表的任务
            If Val(m.Groups(2).Value) <= Val(m.Groups(3).Value) And Val(m.Groups(2).Value) > 0 Then '掉落卡片能掉落卡片时才添加视图中
                '将每个游戏的卡片资料加入到列表中
                dgvList.Rows.Add(Nothing, IIf(reloadWeb, False, True), Val(m.Groups(1).Value.Trim()), m.Groups(4).Value.ToString().Trim(), Val(m.Groups(2).Value), Val(m.Groups(3).Value))
                avaliableGames = avaliableGames + 1
                avaliableCards = avaliableCards + Val(m.Groups(2).Value)
                If reloadWeb Then
                    For Each item In farmAppId
                        If Val(item) = Val(m.Groups(1).Value) Then
                            dgvList.Rows.Item(avaliableGames - 1).Cells(1).Value = True
                        End If
                    Next

                    '如果之前挂机的游戏没挂完，这里设置一个状态
                    If m.Groups(1).Value.Trim() = farmAppId(0).ToString().Trim() Then
                        appToKeep = True
                    End If
                End If
            End If
            m = m.NextMatch() '进行下一个匹配
        Loop

        '显示到info窗口
        LabelCards.Text = avaliableCards
        LabelGames.Text = avaliableGames

        '重置变量为空
        htmlSource = Nothing

        '如果当前能够挂卡的游戏数量为0，则提示并退出；否则将一些按钮设置为可用状态
        If (avaliableGames = 0) Then
            btnEnable(False)
            log(language.msg_nocards, True)

            '关闭定时器
            TimerForClip.Enabled = False
            TimerForProfile.Enabled = False

            ButtonClipboard.Enabled = True
            ButtonProfile.Enabled = True
            ButtonClear.Enabled = True

            stopSAM()
        ElseIf Not reloadWeb Then '如果现在正在挂卡中（个人资料），跳过改变按钮
            btnEnable(True)
            ButtonStartStop.Enabled = True
            selectedAppId = dgvList.Rows.Item(0).Cells(2).Value

            ButtonClipboard.Enabled = True
            ButtonProfile.Enabled = True
            ButtonClear.Enabled = True
        Else '重读取情况
            LinkLabelSCE.Enabled = True
            ButtonStartStop.Enabled = True

            '重新将需要挂机的游戏加入到数组中去
            findFarmAppIds()
            '如果之前的挂机的游戏已经不存在于列表中，则停止SAM，重开新的SAM挂新的游戏
            If Not appToKeep Then
                stopSAM()
                currentAppNum = 0
                runSAMInBackground()
            End If

            farmingFlag()

            disabelCheckbox(True)
        End If

        '将重新读取个人资料标记为假，完成一次完整的重读取个人资料的过程
        reloadWeb = False

        '终止两个线程
        Try
            threadGetCardsData.Abort()
            threadGetHtmlSource.Abort()
        Catch ThreadAbortException As Exception
        End Try

        '强制回收垃圾
        System.GC.Collect()
    End Sub

    '开始结束按钮
    Private Sub ButtonStartStop_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonStartStop.Click

        '如果没有进行挂卡，则
        If isFarming = False Then
            stopSAM() '关闭SAM

            '从当前挂卡列表中找到需要挂卡的数据
            findFarmAppIds()

            If farmAppId.Length <= 1 Then
                Return
            End If

            If fromProfile = False Then
                '剪贴板方式
                currentAppNum = 0
                '如果没记录到这个游戏的App ID，则直接退出
                If farmAppId(currentAppNum) Is Nothing Then
                    Exit Sub
                End If
                If farmAppId.Length > 2 Then '仅在有两个游戏以上的时候才需要定时切换游戏
                    TimerForClip.Enabled = True '打开定时器
                End If
                runSAMInBackground()
            Else
                '个人资料方式
                currentAppNum = 0 '个人资料方式只用挂第一个游戏，直到这个游戏挂完
                If farmAppId(currentAppNum) Is Nothing Then
                    Exit Sub
                End If
                TimerForProfile.Enabled = True
                runSAMInBackground()
            End If

            ButtonStartStop.Text = language.b_stop
            log(language.msg_start, False)
            isFarming = True

            '禁止按钮
            ButtonSelect.Enabled = False
            ButtonUnselect.Enabled = False
            ButtonInverse.Enabled = False
            ButtonClipboard.Enabled = False
            ButtonProfile.Enabled = False
            ButtonClear.Enabled = False

            disabelCheckbox(True)
        Else
            '终止SAM
            stopSAM()

            '强制终止两个线程
            Try
                threadGetCardsData.Abort()
                threadGetHtmlSource.Abort()
            Catch ThreadAbortException As Exception
            End Try


            '关闭俩个定时器
            TimerForClip.Enabled = False
            TimerForProfile.Enabled = False

            '清空挂卡标志
            For i = 0 To dgvList.Rows.Count - 1 Step 1
                dgvList.Rows.Item(i).Cells(0).Value = Nothing
            Next

            ButtonStartStop.Text = language.b_start '改变按钮内容
            log(language.msg_stop, False)
            isFarming = False

            '设置按钮状态
            ButtonSelect.Enabled = True
            ButtonUnselect.Enabled = True
            ButtonInverse.Enabled = True
            ButtonClipboard.Enabled = True
            ButtonProfile.Enabled = True
            ButtonClear.Enabled = True

            disabelCheckbox(False)
        End If
    End Sub


    '读取剪贴板数据按钮监听
    Private Sub ButtonClipboard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonClipboard.Click
        htmlSource = Nothing
        ButtonClipboard.Enabled = False
        ButtonProfile.Enabled = False
        log(language.msg_readclip, False)
        htmlSource = Clipboard.GetText() '从剪贴板获取数据

        threadGetCardsData = New System.Threading.Thread(AddressOf getCardsData)
        threadGetCardsData.IsBackground = True
        threadGetCardsData.Priority = Threading.ThreadPriority.BelowNormal
        threadGetCardsData.Start()
        fromProfile = False '将数据来源设置为剪贴板

    End Sub

    '从个人资料读取
    Private Sub ButtonProfile_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonProfile.Click
        htmlSource = Nothing
        ButtonClipboard.Enabled = False
        ButtonProfile.Enabled = False
        ButtonClear.Enabled = False
        btnEnable(False)
        ButtonStartStop.Enabled = False
        log(language.msg_readprofile, False)
        CheckBoxWebListener.Checked = False
        fromProfile = True
        Me.Enabled = False
        WebForm.Show()
    End Sub

    '剪贴板挂卡的定时事件
    Protected Sub TimerForClip_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimerForClip.Tick
        '关闭SAM
        stopSAM()

        '进行下一个游戏的挂卡
        If currentAppNum >= farmAppId.Length - 2 Then
            currentAppNum = 0
        Else
            currentAppNum = currentAppNum + 1
        End If
        runSAMInBackground()
    End Sub
    '个人资料挂卡的定时事件
    Protected Sub TimerForProfile_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimerForProfile.Tick
        reloadWeb = True '设置重载个人资料
        log(language.msg_reload, False)
        threadGetHtmlSource = New System.Threading.Thread(AddressOf getHtmlSource)
        threadGetHtmlSource.IsBackground = True
        threadGetHtmlSource.Priority = Threading.ThreadPriority.BelowNormal
        threadGetHtmlSource.Start()
    End Sub

    '设立运行标记程序
    Private Sub farmingFlag()
        For i = 0 To dgvList.Rows.Count - 1 Step 1
            If (Trim(Str(dgvList.Rows.Item(i).Cells(2).Value)) = farmAppId(currentAppNum)) Then
                dgvList.Rows.Item(i).Cells(0).Value = "●"
            Else
                dgvList.Rows.Item(i).Cells(0).Value = Nothing
            End If
        Next
    End Sub
    '寻找已选择的AppID
    Private Sub findFarmAppIds()
        '将打上勾的游戏添加到挂卡数组中
        ReDim farmAppId(0)
        Dim j = 0
        For i = 0 To dgvList.Rows.Count - 1 Step 1
            If (dgvList.Rows.Item(i).Cells(1).Value = True) Then
                ReDim Preserve farmAppId(j + 1)
                farmAppId(j) = Trim(Str(dgvList.Rows.Item(i).Cells(2).Value))
                j = j + 1
            End If
        Next
    End Sub

    '后台运行SAM
    Private Sub runSAMInBackground()
        '判断Steam进程是否存在
        If System.Diagnostics.Process.GetProcessesByName("Steam").Length = 0 Then
            '不存在退出
            log(language.msg_nosteam, True)
            Exit Sub
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

            stopSAM()
            process = New Process
            process.StartInfo.FileName = SAMEXEPath
            process.StartInfo.Arguments = farmAppId(currentAppNum)
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            process.Start()
            farmingFlag()
            log(farmAppId(currentAppNum) & language.msg_appstart, False)
        End If
    End Sub
    '停止SAM
    Private Sub stopSAM()
        Dim sProcesses() As System.Diagnostics.Process
        Dim sprocess As System.Diagnostics.Process
        sProcesses = System.Diagnostics.Process.GetProcesses
        For Each sprocess In sProcesses
            If sprocess.ProcessName = SAM_EXE_NAME Then
                sprocess.Kill()
            End If
        Next

        System.Threading.Thread.Sleep(500)
    End Sub

    '全选按钮
    Private Sub ButtonSelect_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonSelect.Click
        selectCheckbox(True)
    End Sub
    '全不选按钮
    Private Sub ButtonUnselect_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonUnselect.Click
        selectCheckbox(False)
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
    Private Sub btnEnable(ByVal enable As Boolean)
        ButtonStartStop.Enabled = enable
        ButtonSelect.Enabled = enable
        ButtonUnselect.Enabled = enable
        ButtonInverse.Enabled = enable

        LinkLabelSCE.Enabled = enable
    End Sub
    '选择按钮是否打勾子程序
    Private Sub selectCheckbox(ByVal all As Boolean)
        For i = 0 To dgvList.Rows.Count - 1 Step 1
            dgvList.Rows.Item(i).Cells(1).Value = all
        Next
    End Sub
    '选择按钮是否可用子程序
    Private Sub disabelCheckbox(ByVal onlyread As Boolean)
        For i = 0 To dgvList.Rows.Count - 1 Step 1
            dgvList.Rows.Item(i).Cells(1).ReadOnly = onlyread
        Next
    End Sub

    '日志记录
    Private Sub log(ByVal msg As String, ByVal showMsgbox As Boolean)
        dgvLog.Rows.Insert(0, DateAndTime.DateString & " " & DateAndTime.TimeString, msg)
        If (showMsgbox) Then
            '窗口没有最小化则MsgBox，否则气泡通知
            If Me.WindowState <> FormWindowState.Minimized Then
                MsgBox(msg, , language.title)
            Else
                NotifyIconMain.ShowBalloonTip(2000, language.title, msg, ToolTipIcon.None)
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
        Dim multiPages As Boolean = True
        Dim pageNum As Integer = 1
        Dim currentHtmlSource As String = ""
        htmlSource = ""

        While multiPages = True
            Try

                pageNum += 1

                Dim httpReq As System.Net.HttpWebRequest
                Dim httpResp As System.Net.HttpWebResponse
                Dim httpURL As New System.Uri(badgesURL & "/?p=" & pageNum)
                httpReq = CType(WebRequest.Create(httpURL), HttpWebRequest)
                httpReq.CookieContainer = cc '将之前获得的cookie加入到http请求中
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

                htmlSource = htmlSource & currentHtmlSource

                If Not mm.Success Then
                    multiPages = False
                    threadGetCardsData = New System.Threading.Thread(AddressOf getCardsData)
                    threadGetCardsData.IsBackground = True
                    threadGetCardsData.Priority = Threading.ThreadPriority.BelowNormal
                    threadGetCardsData.Start()
                End If

            Catch WebException As Exception
                '如果拉取网页发生了异常
                log(language.msg_loadprofileerr, True)

                ButtonClipboard.Enabled = True
                ButtonProfile.Enabled = True
            End Try
        End While
    End Sub

    '几个LinkLabel的打开链接
    Private Sub LinkLabelSCE_LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles LinkLabelSCE.LinkClicked
        System.Diagnostics.Process.Start("http://www.steamcardexchange.net/index.php?gamepage-appid-" & Trim(Str(selectedAppId)))
    End Sub
    Private Sub LinkLabelBlog_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabelBlog.LinkClicked
        System.Diagnostics.Process.Start("http://xiaoxia.de/")
    End Sub

    Private Sub LinkLabelDonation_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabelDonation.LinkClicked
        System.Diagnostics.Process.Start("http://xiaoxia.de/upload/donation.html")
    End Sub
    Private Sub LinkLabelSAM_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabelSAM.LinkClicked
        System.Diagnostics.Process.Start("http://gib.me/sam/")
    End Sub
    Private Sub LinkLabelGithub_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabelGtihub.LinkClicked
        System.Diagnostics.Process.Start("https://github.com/neverweep/SteamTradingCardsFarmer")
    End Sub
    Private Sub LinkLabelDownload_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabelDownload.LinkClicked
        System.Diagnostics.Process.Start("https://github.com/neverweep/SteamTradingCardsFarmer/raw/master/SteamTradingCardsFarmer/bin/Debug/SteamTradingCardsFarmer.exe")
    End Sub

    '监听网页窗口是否关闭的事件
    Private Sub CheckBoxWebListener_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CheckBoxWebListener.CheckedChanged
        If CheckBoxWebListener.Checked = True Then
            '如果取得了cookie
            If cookieText <> Nothing And cookieText <> "" Then

                '将其处理，加到CookieContainer中
                cc = New CookieContainer()
                Dim cookies() = cookieText.Split(";")

                For Each cookie In cookies
                    If (Trim(cookie) <> "") Then
                        Dim cookieNameValue() = cookie.Split("=")
                        Dim ck = New Cookie(cookieNameValue(0).Trim().ToString(), cookieNameValue(1).Trim().ToString())
                        ck.Domain = "steamcommunity.com"
                        cc.Add(ck)
                    End If
                Next

                threadGetHtmlSource = New System.Threading.Thread(AddressOf getHtmlSource)
                threadGetHtmlSource.IsBackground = True
                threadGetHtmlSource.Priority = Threading.ThreadPriority.BelowNormal
                threadGetHtmlSource.Start()
            Else
                '如果没得到html数据
                log(language.msg_nocards, True)

                ButtonClipboard.Enabled = True
                ButtonProfile.Enabled = True
                ButtonClear.Enabled = True
            End If

            cookieText = Nothing
            CheckBoxWebListener.Checked = False
        End If
    End Sub

    'name表示资源文件中的key值,方法说明:根据key值name自动查找当前语言区域适配的资源文件并且返回key对应的value值 
    Private Function GetMessage(ByVal name As String)
        Try
            Dim rm As ResourceManager = New ResourceManager("SteamTradingCardsFarmer.Language", Assembly.GetExecutingAssembly())
            Return rm.GetString(name)
        Catch ex As Exception
            Return "can't load language"
        End Try
    End Function

    Private Sub ButtonClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonClear.Click
        Dim i As Integer = 0
        InternetSetCookie("http://steamcommunity.com", Nothing, Nothing)
        InternetSetCookie("https://steamcommunity.com", Nothing, Nothing)
        MsgBox(language.info_restart, , language.title)
    End Sub
    <DllImport("wininet.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
    Public Shared Function InternetSetCookie(ByVal lpszUrl As String, _
      ByVal lpszCookieName As String, ByVal lpszCookieData As String) As Boolean
    End Function
End Class
