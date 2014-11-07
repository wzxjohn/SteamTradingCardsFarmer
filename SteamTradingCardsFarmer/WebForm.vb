Imports System.Text.RegularExpressions
Imports System.Net
Imports System.IO

Public Class WebForm
    Private Sub WebForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Me.Text = Main.langStrings.webform_loading
        WebBrowser.IsWebBrowserContextMenuEnabled = False
        WebBrowser.Navigate("https://steamcommunity.com/login/home/#mainBody")
    End Sub
    Private Sub WebForm_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        Main.CheckBoxWebListener.Checked = True
        Main.Enabled = True
        Me.Dispose()
        System.GC.Collect()
    End Sub

    Public Sub WebBrowser_Navigated(ByVal sender As Object, ByVal e As WebBrowserNavigatedEventArgs) Handles WebBrowser.Navigated
        Me.Text = WebBrowser.Url.ToString
        Dim m As Match = Regex.Match(WebBrowser.Url.ToString, _
                           "(https?:\/\/steamcommunity.com\/((id\/.*?)|(profiles\/\d+))\/home)", _
                           RegexOptions.IgnoreCase Or RegexOptions.IgnorePatternWhitespace)
        If m.Success Then

            Dim profileURL = m.Groups(1).ToString().Trim().Replace("home", "badges")
            Dim cookieText = WebBrowser.Document.Cookie

            cookieText = Replace(cookieText, Chr(10), "", , , vbBinaryCompare) '删除换行和控制字符
            cookieText = Replace(cookieText, Chr(13), "", , , vbBinaryCompare)
            cookieText = Replace(cookieText, ",", "%2C")

            Main.badgesURL = profileURL
            Main.cookieText = cookieText

            Me.Close()
        ElseIf Not WebBrowser.Url.ToString.StartsWith("https://steamcommunity.com/login/home") Then
            WebBrowser.Navigate("https://steamcommunity.com/login/home/#mainBody")
        End If
    End Sub
End Class