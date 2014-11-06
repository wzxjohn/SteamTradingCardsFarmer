Imports System.Threading

Public Class Language
    Friend info_restart As String
    Friend msg_appstart As String
    Friend msg_appstop As String
    Friend msg_launch As String
    Friend msg_loadprofileerr As String
    Friend msg_nocards As String
    Friend msg_nosteam As String
    Friend msg_readclip As String
    Friend msg_readprofile As String
    Friend msg_reload As String
    Friend msg_running As String
    Friend msg_start As String
    Friend msg_stop As String
    Friend b_clear As String
    Friend b_select As String
    Friend b_unselect As String
    Friend b_inverse As String
    Friend b_profile As String
    Friend b_clipboard As String
    Friend b_start As String
    Friend b_stop As String
    Friend dgv_id As String
    Friend dgv_title As String
    Friend dgv_drop As String
    Friend dgv_total As String
    Friend t_home As String
    Friend t_about As String
    Friend t_log As String
    Friend gb_info As String
    Friend gb_selection As String
    Friend gb_source As String
    Friend l_cards As String
    Friend l_games As String
    Friend ll_sce As String
    Friend l_notice As String
    Friend l_warning As String
    Friend l_blog As String
    Friend l_donation As String
    Friend dgv_time As String
    Friend dgv_msg As String
    Friend title As String
    Friend webform_loading As String

    Public Sub init()
        Select Case System.Threading.Thread.CurrentThread.CurrentCulture.ToString()
            Case "zh-CN"
                title = "Steam 挂卡工具"
                info_restart = "重新启动程序生效"
                msg_appstart = " 开始"
                msg_appstop = " 停止"
                msg_launch = "Steam 挂卡工具启动"
                msg_loadprofileerr = "读取个人资料出错"
                msg_nocards = "没有可掉落卡片或数据错误"
                msg_nosteam = "没有运行 Steam"
                msg_readclip = "读取剪贴板"
                msg_readprofile = "读取个人资料"
                msg_reload = "更新个人资料"
                msg_running = "Steam 挂卡工具已在运行"
                msg_start = "开始挂卡"
                msg_stop = "停止挂卡"
                b_clear = "清除"
                b_select = "全选"
                b_unselect = "全不选"
                b_inverse = "反选"
                b_profile = "个人资料"
                b_clipboard = "剪贴板"
                b_start = "开始"
                b_stop = "停止"
                dgv_id = "游戏 ID"
                dgv_title = "游戏标题"
                dgv_drop = "掉落"
                dgv_total = "总计"
                t_home = "主页"
                t_about = "关于"
                t_log = "日志"
                gb_info = "信息"
                gb_selection = "选择"
                gb_source = "来源"
                l_cards = "可掉落卡片数："
                l_games = "可掉落游戏数："
                ll_sce = "打开 SteamCardExchange.net"
                l_notice = "本程序使用 SAM（Steam Achievement Manager）模拟运行 Steamwork 程序来掉落卡片。"
                l_warning = "本程序可能会对你的 Steam 帐号有不良影响，使用风险自负！"
                l_blog = "我的博客："
                l_donation = "捐赠链接："
                dgv_time = "时间"
                dgv_msg = "消息"
                webform_loading = "读取个人资料"
            Case "zh-TW"
                title = "Steam 掛卡工具"
                info_restart = "重新開機程式生效"
                msg_appstart = " 開始"
                msg_appstop = " 停止"
                msg_launch = "Steam 掛卡工具啟動"
                msg_loadprofileerr = "讀取個人資料出錯"
                msg_nocards = "沒有可掉落卡片或資料錯誤"
                msg_nosteam = "沒有運行 Steam"
                msg_readclip = "讀取剪貼板"
                msg_readprofile = "讀取個人資料"
                msg_reload = "更新個人資料"
                msg_running = "Steam 掛卡工具已在運行"
                msg_start = "開始掛卡"
                msg_stop = "停止掛卡"
                b_clear = "清除"
                b_select = "全選"
                b_unselect = "全不選"
                b_inverse = "反選"
                b_profile = "個人資料"
                b_clipboard = "剪貼板"
                b_start = "開始"
                b_stop = "停止"
                dgv_id = "遊戲 ID"
                dgv_title = "遊戲標題"
                dgv_drop = "掉落"
                dgv_total = "總計"
                t_home = "主頁"
                t_about = "關於"
                t_log = "日誌"
                gb_info = "信息"
                gb_selection = "選擇"
                gb_source = "來源"
                l_cards = "可掉落卡片數："
                l_games = "可掉落遊戲數："
                ll_sce = "打開 SteamCardExchange.net"
                l_notice = "本程式使用 SAM（Steam Achievement Manager）類比運行 Steamwork 程式來掉落卡片。"
                l_warning = "本程式可能會對你的 Steam 帳號有不良影響，使用風險自負！"
                l_blog = "我的博客："
                l_donation = "捐贈連結："
                dgv_time = "時間"
                dgv_msg = "消息"
                webform_loading = "讀取個人資料"
            Case Else
                title = "Steam Trading Cards Farmer"
                info_restart = "Restart program to apply"
                msg_appstart = " starts"
                msg_appstop = " stops"
                msg_launch = "Steam Cards Farmer starts"
                msg_loadprofileerr = "Read profile error"
                msg_nocards = "No droppable cards or invalid data"
                msg_nosteam = "Steam is not running"
                msg_readclip = "Read clipboard"
                msg_readprofile = "Read profile"
                msg_reload = "Reload profile"
                msg_running = "Steam Trading Cards Farmer is already running"
                msg_start = "Start farming"
                msg_stop = "Stop farming"
                b_clear = "Clear"
                b_select = "Select All"
                b_unselect = "Unselect All"
                b_inverse = "Inverse"
                b_profile = "Read Profile"
                b_clipboard = "Read Clipboard"
                b_start = "Start"
                b_stop = "Stop"
                dgv_id = "App ID"
                dgv_title = "Title"
                dgv_drop = "Drop"
                dgv_total = "Total"
                t_home = "Home"
                t_about = "About"
                t_log = "Log"
                gb_info = "Infomation"
                gb_selection = "Selection"
                gb_source = "Source"
                l_cards = "Droppable Cards"
                l_games = "Droppable Games"
                ll_sce = "Navigate to SteamCardExchange.net"
                l_notice = "This program use SAM(Steam Achievement Manager) to simulate a Steamwork app running to obtain trading cards."
                l_warning = "This program may be dangerous to your steam account. USE AT YOUR OWN RISK!"
                l_blog = "My Blog："
                l_donation = "Donation Landing Page："
                dgv_time = "Time"
                dgv_msg = "Message"
                webform_loading = "Reading Profile"
        End Select
    End Sub
End Class
