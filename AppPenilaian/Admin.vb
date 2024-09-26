Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc

Public Class Admin
    Sub kosong()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()


    End Sub
    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Chr(13) Then
            Call koneksi()
            cmd = New MySqlCommand("Select * from tbl_admin where id ='" & TextBox3.Text & "'", conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                TextBox1.Text = dr.Item("username")
                TextBox2.Text = dr.Item("password")



            Else
                MsgBox("Kode Tidak Ditemukan")

            End If
        End If

    End Sub
    Sub tampil()
        Call koneksi()
        da = New MySqlDataAdapter("select * from tbl_admin", conn)
        ds = New DataSet
        da.Fill(ds, "tbl_admin")
        dgv.DataSource = ds.Tables("tbl_admin")
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv.RowsDefaultCellStyle.BackColor = Color.LightBlue
        dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgv.ReadOnly = True
        tutupdb()


    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            ' Pengecekan apakah semua kolom telah diisi
            If String.IsNullOrWhiteSpace(TextBox1.Text) OrElse String.IsNullOrWhiteSpace(TextBox2.Text) OrElse String.IsNullOrWhiteSpace(TextBox3.Text) Then
                MessageBox.Show("Semua kolom harus diisi.")
                Exit Sub
            End If

            Call koneksi()

            Dim simpan As String

            ' Mencari data berdasarkan ID
            Dim query As String = "SELECT * FROM tbl_admin WHERE id = @id"
            cmd = New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@id", TextBox3.Text)
            dr = cmd.ExecuteReader()

            ' Jika data tidak ditemukan, lakukan penyimpanan
            If Not dr.HasRows Then
                dr.Close() ' Tutup reader sebelum membuka koneksi kembali
                Call koneksi() ' Buka koneksi untuk operasi penyimpanan data baru

                ' Perintah SQL untuk menyimpan data
                simpan = "INSERT INTO tbl_admin (id, username, password) VALUES (@id, @username, @password)"
                cmd = New MySqlCommand(simpan, conn)
                cmd.Parameters.AddWithValue("@id", TextBox3.Text)
                cmd.Parameters.AddWithValue("@username", TextBox1.Text)
                cmd.Parameters.AddWithValue("@password", TextBox2.Text)
                cmd.ExecuteNonQuery()

                MsgBox("Data Berhasil Disimpan")
                tampil()
            Else
                MsgBox("ID admin sudah ada. Gunakan tombol Ubah untuk mengedit data.")
            End If
        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message)
        Finally
            tutupdb() ' Tutup koneksi
            kosong() ' Kosongkan TextBox setelah penyimpanan berhasil
        End Try

    End Sub




    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            ' Pengecekan apakah kolom kode kriteria kosong
            If String.IsNullOrWhiteSpace(TextBox3.Text) Then
                MessageBox.Show("Kode harus diisi.")
                Exit Sub
            End If

            ' Pengecekan apakah ID ada dalam database
            Call koneksi()
            Dim cmdCheck As New MySqlCommand("SELECT COUNT(*) FROM tbl_admin WHERE id = @id", conn)
            cmdCheck.Parameters.AddWithValue("@id", TextBox3.Text)
            Dim count As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())

            If count = 0 Then
                MessageBox.Show("ID tidak ditemukan dalam database.")
                Exit Sub
            End If

            ' Konfirmasi penghapusan data dari pengguna
            Dim confirmResult As DialogResult = MessageBox.Show("Yakin data mau dihapus?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            ' Jika pengguna memilih 'Yes', lanjutkan dengan penghapusan data
            If confirmResult = DialogResult.Yes Then
                ' Buka koneksi untuk operasi penghapusan data
                Dim hapus As String = "DELETE FROM tbl_admin WHERE id = @id"
                Dim cmdDelete As New MySqlCommand(hapus, conn)
                cmdDelete.Parameters.AddWithValue("@id", TextBox3.Text)
                cmdDelete.ExecuteNonQuery()

                MsgBox("Data berhasil dihapus.")
                tampil()
                kosong() ' Kosongkan TextBox setelah penghapusan berhasil
            End If

        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message)

        Finally
            tutupdb() ' Tutup koneksi
        End Try



    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            ' Pengecekan apakah semua kolom telah diisi
            If String.IsNullOrWhiteSpace(TextBox1.Text) OrElse String.IsNullOrWhiteSpace(TextBox2.Text) OrElse String.IsNullOrWhiteSpace(TextBox3.Text) Then
                MessageBox.Show("Semua kolom harus diisi.")
                Exit Sub
            End If

            Call koneksi()

            Dim edit As String

            ' Mencari data berdasarkan ID
            Dim query As String = "SELECT * FROM tbl_admin WHERE id = @id"
            cmd = New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@id", TextBox3.Text)
            dr = cmd.ExecuteReader()

            ' Jika data ditemukan, lakukan pembaruan
            If dr.HasRows Then
                dr.Close() ' Tutup reader sebelum membuka koneksi kembali
                Call koneksi() ' Buka koneksi untuk operasi pembaruan data

                ' Perintah SQL untuk memperbarui data
                edit = "UPDATE tbl_admin SET username = @username, password = @password WHERE id = @id"
                cmd = New MySqlCommand(edit, conn)
                cmd.Parameters.AddWithValue("@username", TextBox1.Text)
                cmd.Parameters.AddWithValue("@password", TextBox2.Text)
                cmd.Parameters.AddWithValue("@id", TextBox3.Text)
                cmd.ExecuteNonQuery()

                MsgBox("Data Berhasil Diubah")
                tampil()
            Else
                MsgBox("ID admin tidak ditemukan. Gunakan tombol Simpan untuk menyimpan data baru.")
            End If
        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message)
        Finally
            tutupdb() ' Tutup koneksi
            kosong() ' Kosongkan TextBox setelah pembaruan berhasil
        End Try

    End Sub
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ' Membersihkan semua bidang input
        TextBox2.Text = ""
        TextBox1.Text = ""
        TextBox3.Text = ""
    End Sub
    Private Sub Admin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tampil()
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Down Then
            TextBox2.Focus()
        ElseIf e.KeyCode = Keys.Up Then

            TextBox3.Focus()
        End If
    End Sub
    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Down Then
            TextBox3.Focus()
        ElseIf e.KeyCode = Keys.Up Then

            TextBox1.Focus()
        End If

    End Sub

    Private Sub TextBox3_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox3.KeyDown
        If e.KeyCode = Keys.Down Then
            TextBox1.Focus()
        ElseIf e.KeyCode = Keys.Up Then

            TextBox2.Focus()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class