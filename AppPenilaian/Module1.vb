
Imports MySql.Data.MySqlClient
Module module1
    Public conn As MySqlConnection
    Public cmd As MySqlCommand
    Public da As MySqlDataAdapter
    Public dr As MySqlDataReader
    Public ds As DataSet
    Public str As String
    Public db As Integer

    Sub koneksi()
        Try
            Dim str As String = "server=localhost; user id=root; password=; database= db_aplikasi"
            conn = New MySqlConnection(str)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Sub tutupdb()
        Try
            Dim sqlconn As String
            sqlconn = "server=localhost; user id=root; password=; database= db_aplikasi"
            conn = New MySqlConnection(sqlconn)
            If conn.State = ConnectionState.Open Then
                conn.Close()

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Module

