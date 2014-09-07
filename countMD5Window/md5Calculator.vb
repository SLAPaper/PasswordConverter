Public Class md5Calculator

    Private Sub TextBox_Input_TextChanged(sender As Object, e As EventArgs) Handles TextBox_Input.TextChanged

    End Sub

    Private Sub TextBox_Input_OnFocus(sender As Object, e As EventArgs) Handles TextBox_Input.GotFocus
        TextBox_Input.Text = ""
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim input As String = Me.TextBox_Input.Text
        Dim result As String = countMD5.getMd5Hash(input)
        Me.TextBox_Result.Text = result
    End Sub
End Class
