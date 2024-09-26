Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Public Class Kriteria
    Sub kosong()
        TextBox1.Clear()
        TextBox2.Clear()

    End Sub
    Sub tampil()
        Call koneksi()
        da = New MySqlDataAdapter("select *from kriteria", conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "kriteria")
        dgv.DataSource = ds.Tables("kriteria")
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv.RowsDefaultCellStyle.BackColor = Color.LightBlue
        dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgv.ReadOnly = True
        tutupdb()


    End Sub
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ' Membersihkan semua bidang input
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub
    Private Sub Kriteria_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tampil()

    End Sub

    Private Sub dgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
        Call tampil()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Call koneksi()

            ' Pengecekan apakah kode_kriteria dan nama_kriteria tidak boleh kosong
            If String.IsNullOrWhiteSpace(TextBox1.Text) AndAlso String.IsNullOrWhiteSpace(TextBox2.Text) Then
                MessageBox.Show("Kode kriteria dan nama kriteria harus diisi.")
                Exit Sub
            ElseIf String.IsNullOrWhiteSpace(TextBox1.Text) Then
                MessageBox.Show("Kode kriteria harus diisi.")
                Exit Sub
            ElseIf String.IsNullOrWhiteSpace(TextBox2.Text) Then
                MessageBox.Show("Nama kriteria harus diisi.")
                Exit Sub
            End If

            Dim simpan As String
            Dim edit As String

            cmd = New MySqlCommand("SELECT * FROM kriteria WHERE kode_kriteria = @kode_kriteria", conn)
            cmd.Parameters.AddWithValue("@kode_kriteria", TextBox1.Text)
            dr = cmd.ExecuteReader()

            If Not dr.HasRows Then
                Call koneksi() ' Buka koneksi kembali untuk operasi penyimpanan data baru

                simpan = "INSERT INTO kriteria VALUES (@kode, @nama)"
                cmd = New MySqlCommand(simpan, conn)
                cmd.Parameters.AddWithValue("@kode", TextBox1.Text)
                cmd.Parameters.AddWithValue("@nama", TextBox2.Text)
                cmd.ExecuteNonQuery()

                MsgBox("Data Berhasil Disimpan")
                tampil()
            Else
                MessageBox.Show("ID kriteria ditemukan. Gunakan tombol Ubah untuk mengedit data.")
            End If

        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message)

        Finally
            tutupdb()
            kosong()
        End Try

    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            ' Pengecekan apakah kolom kode kriteria kosong
            If String.IsNullOrWhiteSpace(TextBox1.Text) Then
                MessageBox.Show("Kode kriteria harus diisi.")
                Exit Sub
            End If

            ' Pengecekan apakah kode kriteria ada dalam database
            Call koneksi()
            Dim queryCheck As String = "SELECT COUNT(*) FROM kriteria WHERE kode_kriteria = @kode_kriteria"
            Dim cmdCheck As New MySqlCommand(queryCheck, conn)
            cmdCheck.Parameters.AddWithValue("@kode_kriteria", TextBox1.Text)
            Dim count As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())
            If count = 0 Then
                MessageBox.Show("Kode kriteria tidak ditemukan dalam database.")
                Exit Sub
            End If

            ' Konfirmasi penghapusan data dari pengguna
            Dim confirmResult As DialogResult = MessageBox.Show("Yakin data mau dihapus?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            ' Jika pengguna memilih 'Yes', lanjutkan dengan penghapusan data
            If confirmResult = DialogResult.Yes Then
                Call koneksi()

                ' Buka koneksi untuk operasi penghapusan data
                Call koneksi()

                Dim hapus As String = "DELETE FROM kriteria WHERE kode_kriteria = @kode_kriteria"
                Dim cmdDelete As New MySqlCommand(hapus, conn)
                cmdDelete.Parameters.AddWithValue("@kode_kriteria", TextBox1.Text)
                cmdDelete.ExecuteNonQuery()

                MsgBox("Data berhasil dihapus.")
                tampil()

            End If

        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message)
        Finally
            tutupdb()
            kosong() ' Kosongkan TextBox setelah penghapusan berhasil
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Call koneksi()

            ' Pengecekan apakah kode_kriteria dan nama_kriteria tidak boleh kosong
            If String.IsNullOrWhiteSpace(TextBox1.Text) OrElse String.IsNullOrWhiteSpace(TextBox2.Text) Then
                MessageBox.Show("Kode kriteria dan nama kriteria harus diisi.")
                Exit Sub
            End If

            Dim edit As String

            cmd = New MySqlCommand("SELECT * FROM kriteria WHERE kode_kriteria = @kode_kriteria", conn)
            cmd.Parameters.AddWithValue("@kode_kriteria", TextBox1.Text)
            dr = cmd.ExecuteReader()

            If Not dr.HasRows Then
                MessageBox.Show("ID kriteria tidak ditemukan. Gunakan tombol Simpan untuk menyimpan data baru.")
            Else
                dr.Close() ' Tutup reader sebelum membuka koneksi kembali
                conn.Close()

                ' Buka koneksi kembali untuk operasi update data
                Call koneksi()

                ' Pengecekan apakah nama_kriteria tidak boleh kosong
                If String.IsNullOrWhiteSpace(TextBox2.Text) Then
                    MessageBox.Show("Nama kriteria harus diisi.")
                    Exit Sub
                End If

                edit = "UPDATE kriteria SET nama_kriteria = @nama_kriteria WHERE kode_kriteria = @kode_kriteria"
                cmd = New MySqlCommand(edit, conn)
                cmd.Parameters.AddWithValue("@nama_kriteria", TextBox2.Text)
                cmd.Parameters.AddWithValue("@kode_kriteria", TextBox1.Text)
                cmd.ExecuteNonQuery()

                MsgBox("Data Berhasil Diupdate")
                tampil()
            End If

        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            tutupdb()
            kosong()
        End Try


    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
    End Sub



    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Down Then
            TextBox1.Focus()
        ElseIf e.KeyCode = Keys.Up Then

            TextBox1.Focus()
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Down Then
            TextBox2.Focus()
        ElseIf e.KeyCode = Keys.Up Then

            TextBox2.Focus()
        End If
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then ' Mengecek jika tombol Enter ditekan
            Try
                Call koneksi()
                Dim cmd As New MySqlCommand("SELECT * FROM kriteria WHERE kode_kriteria = @kode", conn)
                cmd.Parameters.AddWithValue("@kode", TextBox1.Text)
                Dim dr As MySqlDataReader = cmd.ExecuteReader

                If dr.Read() Then
                    TextBox2.Text = dr("nama_kriteria").ToString()
                Else
                    MsgBox("Kode tidak ditemukan!!")
                    Call kosong()
                End If
                dr.Close()
            Catch ex As Exception
                MsgBox("Terjadi kesalahan: " & ex.Message)
            Finally
                conn.Close()
            End Try
        End If
    End Sub
End Class