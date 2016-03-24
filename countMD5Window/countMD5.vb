Imports System.Security.Cryptography
Imports System.Text

Public Class CountMd5
    Public Shared Function GetMd5Hash(input As String) As String
        Dim md5Hasher As New MD5CryptoServiceProvider()
        Dim sBuilder As New StringBuilder()

        sBuilder.Append("MD5, base64(*$)" + vbCrLf)
        Dim data As Byte() = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input))
        Dim i As Integer
        For i = 0 To data.Length - 1
            sBuilder.Append(data(i).ToString("x2"))
        Next i
        sBuilder.Append(vbCrLf + HexToBase64(data))

        Return sBuilder.ToString()
    End Function

    Public Shared Function VerifyMd5Hash(input As String, hash As String) As Boolean
        Dim hashOfInput As String = GetMd5Hash(input)
        Dim comparer As StringComparer = StringComparer.OrdinalIgnoreCase
        If 0 = comparer.Compare(hashOfInput, hash) Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Shared Function HexToBase64(input As Byte()) As String
        Dim base64Dict = New Dictionary(Of Byte, String) From {{0, "A"}, {1, "B"}, {2, "C"}, {3, "D"}, {4, "E"}, {5, "F"}, {6, "G"}, {7, "H"}, {8, "I"}, {9, "J"}, {10, "K"}, {11, "L"}, {12, "M"}, {13, "N"}, {14, "O"}, {15, "P"}, {16, "Q"}, {17, "R"}, {18, "S"}, {19, "T"}, {20, "U"}, {21, "V"}, {22, "W"}, {23, "X"}, {24, "Y"}, {25, "Z"}, {26, "a"}, {27, "b"}, {28, "c"}, {29, "d"}, {30, "e"}, {31, "f"}, {32, "g"}, {33, "h"}, {34, "i"}, {35, "j"}, {36, "k"}, {37, "l"}, {38, "m"}, {39, "n"}, {40, "o"}, {41, "p"}, {42, "q"}, {43, "r"}, {44, "s"}, {45, "t"}, {46, "u"}, {47, "v"}, {48, "w"}, {49, "x"}, {50, "y"}, {51, "z"}, {52, "0"}, {53, "1"}, {54, "2"}, {55, "3"}, {56, "4"}, {57, "5"}, {58, "6"}, {59, "7"}, {60, "8"}, {61, "9"}, {62, "*"}, {63, "$"}}
        Dim sBuilder As New StringBuilder()
        Dim bytes = New List(Of Byte)
        For i = 0 To input.Length / 3
            Dim j = 2
            If i * 3 + j >= input.Length Then
                j = input.Length - i * 3 - 1
            End If
            Dim x As UInteger = 0
            For k = 0 To j
                x = x Or (CUInt(input(i * 3 + k)) << ((2 - k) * 8))
            Next
            For k = 3 To 0 Step -1
                bytes.Add((x And (63 << (k * 6))) >> (k * 6))
            Next
        Next
        For i = 0 To input.Length - 1
            sBuilder.Append(base64Dict(bytes(i)))
        Next
        Return sBuilder.ToString()
    End Function

    Public Shared Function GetShaHash(input As String) As String
        Dim sha1Hasher As New SHA1CryptoServiceProvider()
        Dim sha256Hasher As New SHA256CryptoServiceProvider()
        Dim sha384Hasher As New SHA384CryptoServiceProvider()
        Dim sha512Hasher As New SHA512CryptoServiceProvider()

        Dim data As Byte() = Encoding.Default.GetBytes(input)
        Dim sBuilder As New StringBuilder()

        Dim sha1Data As Byte() = sha1Hasher.ComputeHash(data)

        sBuilder.Append("SHA1, Base64(*$)" + vbCrLf)
        Dim i As Integer
        For i = 0 To sha1Data.Length - 1
            sBuilder.Append(sha1Data(i).ToString("x2"))
        Next i
        sBuilder.Append(vbCrLf + HexToBase64(sha1Data) + vbCrLf + vbCrLf)

        sBuilder.Append("SHA256, Base64(*$)" + vbCrLf)
        Dim sha256Data As Byte() = sha256Hasher.ComputeHash(data)
        For i = 0 To sha256Data.Length - 1
            sBuilder.Append(sha256Data(i).ToString("x2"))
        Next i
        sBuilder.Append(vbCrLf + HexToBase64(sha256Data) + vbCrLf + vbCrLf)

        sBuilder.Append("SHA384, Base64(*$)" + vbCrLf)
        Dim sha384Data As Byte() = sha384Hasher.ComputeHash(data)
        For i = 0 To sha384Data.Length - 1
            sBuilder.Append(sha384Data(i).ToString("x2"))
        Next i
        sBuilder.Append(vbCrLf + HexToBase64(sha384Data) + vbCrLf + vbCrLf)

        sBuilder.Append("SHA512, Base64(*$)" + vbCrLf)
        Dim sha512Data As Byte() = sha512Hasher.ComputeHash(data)
        For i = 0 To sha512Data.Length - 1
            sBuilder.Append(sha512Data(i).ToString("x2"))
        Next i
        sBuilder.Append(vbCrLf + HexToBase64(sha512Data))

        Return sBuilder.ToString()
    End Function
End Class
