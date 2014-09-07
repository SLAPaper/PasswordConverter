Imports System
Imports System.Security.Cryptography
Imports System.Text

Public Class countMD5
    Public Shared Function getMd5Hash(ByVal input As String) As String
        Dim md5Hasher As New MD5CryptoServiceProvider()
        Dim data As Byte() = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input))
        Dim sBuilder As New StringBuilder()
        Dim i As Integer
        For i = 0 To data.Length - 1
            sBuilder.Append(data(i).ToString("x2"))
        Next i
        sBuilder.Append(vbCrLf + md5ToBase64(data))
        Return sBuilder.ToString()
    End Function

    Public Shared Function verifyMd5Hash(ByVal input As String, ByVal hash As String) As Boolean
        Dim hashOfInput As String = getMd5Hash(input)
        Dim comparer As StringComparer = StringComparer.OrdinalIgnoreCase
        If 0 = comparer.Compare(hashOfInput, hash) Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Shared Function md5ToBase64(ByVal input As Byte()) As String
        Dim base64Dict As Dictionary(Of Byte, String) = New Dictionary(Of Byte, String) From {{0, "A"}, {1, "B"}, {2, "C"}, {3, "D"}, {4, "E"}, {5, "F"}, {6, "G"}, {7, "H"}, {8, "I"}, {9, "J"}, {10, "K"}, {11, "L"}, {12, "M"}, {13, "N"}, {14, "O"}, {15, "P"}, {16, "Q"}, {17, "R"}, {18, "S"}, {19, "T"}, {20, "U"}, {21, "V"}, {22, "W"}, {23, "X"}, {24, "Y"}, {25, "Z"}, {26, "a"}, {27, "b"}, {28, "c"}, {29, "d"}, {30, "e"}, {31, "f"}, {32, "g"}, {33, "h"}, {34, "i"}, {35, "j"}, {36, "k"}, {37, "l"}, {38, "m"}, {39, "n"}, {40, "o"}, {41, "p"}, {42, "q"}, {43, "r"}, {44, "s"}, {45, "t"}, {46, "u"}, {47, "v"}, {48, "w"}, {49, "x"}, {50, "y"}, {51, "z"}, {52, "0"}, {53, "1"}, {54, "2"}, {55, "3"}, {56, "4"}, {57, "5"}, {58, "6"}, {59, "7"}, {60, "8"}, {61, "9"}, {62, "*"}, {63, "$"}}
        Dim sBuilder As New StringBuilder()
        Dim bytes As List(Of Byte) = New List(Of Byte)
        For i As Integer = 0 To input.Length / 3
            Dim j As Integer = 2
            If i * 3 + j >= input.Length Then
                j = input.Length - i * 3 - 1
            End If
            Dim x As UInteger = 0
            For k As Integer = 0 To j
                x = x Or (CUInt(input(i * 3 + k)) << ((2 - k) * 8))
            Next
            For k As Integer = 3 To 0 Step -1
                bytes.Add((x And (63 << (k * 6))) >> (k * 6))
            Next
        Next
        For i As Integer = 0 To input.Length - 1
            sBuilder.Append(base64Dict(bytes(i)))
        Next
        Return sBuilder.ToString()
    End Function
End Class
