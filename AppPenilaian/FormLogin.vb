Imports System.Data.Odbc
Imports System.Diagnostics.Eventing.Reader
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient
Public Class FormLogin

    Public Sub hilang()
        TextBox1.Clear()
        TextBox2.Clear()

    End Sub
    Sub Terbuka()
        FormMenuUtama.LoginToolStripMenuItem.Visible = False
        FormMenuUtama.LogoutToolStripMenuItem.Visible = True
        FormMenuUtama.MasterToolStripMenuItem.Visible = True
        FormMenuUtama.AdminToolStripMenuItem.Visible = True
        FormMenuUtama.DataAlternatifToolStripMenuItem.Visible = True
        FormMenuUtama.CripsToolStripMenuItem.Visible = True
        FormMenuUtama.PerhitunganToolStripMenuItem.Visible = True

    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    ''change textbox being password char
    Private Sub InitializeMyControl()
        TextBox2.Text = ""
        TextBox2.PasswordChar = "*"
        TextBox2.MaxLength = 14
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim conn As MySqlConnection
        conn = New MySqlConnection
        conn.ConnectionString = "server=localhost; user id=root; password= ; database=db_aplikasi"
        Try
            conn.Open()
        Catch ex As Exception
            MsgBox("Ada kesalahan dalam koneksi database")
        End Try
        Dim myAdapter As New MySqlDataAdapter

        Dim sqlQuery = "SELECT * FROM tbl_admin WHERE username='" + TextBox1.Text + "' AND password='" + TextBox2.Text + "'"
        Dim myCommand As New MySqlCommand
        myCommand.Connection = conn
        myCommand.CommandText = sqlQuery

        myAdapter.SelectCommand = myCommand
        Dim myData As MySqlDataReader
        myData = myCommand.ExecuteReader()

        If myData.HasRows = 0 Then
            MsgBox("username dan password salah!! ",
                   MsgBoxStyle.Exclamation, "Error Login")
        Else
            MsgBox("Login berhasil, Selamat datang " & TextBox1.Text & "!", MsgBoxStyle.Information, "Successfull Login")
            Call Terbuka()
            Me.Hide()
        End If
        Call hilang()
    End Sub

    Private Sub FormLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If (e.KeyChar = Chr(13)) Then

            TextBox2.Focus()

        End If
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If (e.KeyChar = Chr(13)) Then

            Button1.Focus()

        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Down Then
            TextBox2.Focus()
        ElseIf e.KeyCode = Keys.Up Then

            TextBox2.Focus()
        End If
    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Down Then
            TextBox1.Focus()
        ElseIf e.KeyCode = Keys.Up Then

            TextBox1.Focus()
        End If
    End Sub
End Class