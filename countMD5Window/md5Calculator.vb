Public Class Md5Calculator

    Private Shared Sub TextBox_Input_TextChanged(sender As Object, e As EventArgs) Handles TextBox_Input.TextChanged

    End Sub

    Private Sub TextBox_Input_OnFocus(sender As Object, e As EventArgs) Handles TextBox_Input.GotFocus
        TextBox_Input.Text = ""
    End Sub

    Private Sub TextBox_Input_LostFocus(sender As Object, e As EventArgs) Handles TextBox_Input.LostFocus
        If TextBox_Input.Text = "" Then
            TextBox_Input.Text = "请输入需要计算哈希值的字符串。"
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim input As String = TextBox_Input.Text
        Dim result As String = CountMd5.GetMd5Hash(input) + vbCrLf + vbCrLf + CountMd5.GetShaHash(input)
        TextBox_Result.Text = result
        TextBox_Result.Focus()
    End Sub
End Class
