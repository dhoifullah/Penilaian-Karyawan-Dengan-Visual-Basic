Imports System.Configuration
Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class crips
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ' Membersihkan semua bidang input
        ComboBox1.SelectedItem = Nothing
        ComboBox2.SelectedItem = Nothing
        ComboBox3.SelectedItem = Nothing
        TextBox1.Text = ""
    End Sub

    Private Sub crips_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MunculodeAnggota()
        tampil()
    End Sub
    Sub tampil()
        Call koneksi()
        da = New MySqlDataAdapter("select * from crips", conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "crips")
        dgv.DataSource = ds.Tables("crips")
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv.RowsDefaultCellStyle.BackColor = Color.LightBlue
        dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgv.ReadOnly = True
        tutupdb()

    End Sub
    Sub MunculodeAnggota()
        Call koneksi()
        ComboBox1.Items.Clear()
        Dim cmd As MySqlCommand = New MySqlCommand("SELECT * FROM kriteria", conn)
        Dim dr As MySqlDataReader = cmd.ExecuteReader
        While dr.Read()
            ComboBox1.Items.Add(dr.Item("kode_kriteria")) ' Ganti "nama_field_yang_diinginkan" dengan nama kolom yang ingin ditampilkan di ComboBox
        End While
        dr.Close()
        conn.Close()
    End Sub
    Private Sub ComboBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles ComboBox2.KeyDown
        ' Memeriksa apakah tombol yang ditekan adalah tombol backspace
        If e.KeyCode = Keys.Back Then
            ' Mengatur nilai ComboBox2 menjadi null
            ComboBox2.SelectedItem = Nothing
        End If
    End Sub

    Private Sub ComboBox3_KeyDown(sender As Object, e As KeyEventArgs) Handles ComboBox3.KeyDown
        ' Memeriksa apakah tombol yang ditekan adalah tombol backspace
        If e.KeyCode = Keys.Back Then
            ' Mengatur nilai ComboBox3 menjadi null
            ComboBox3.SelectedItem = Nothing
        End If
    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Mendapatkan kode kriteria dari input pengguna
        Dim crips_kriteria As String = ComboBox1.SelectedItem?.ToString()

        ' Pengecekan apakah kode kriteria tidak kosong
        If String.IsNullOrWhiteSpace(crips_kriteria) Then
            MsgBox("Kode Kriteria harus dipilih.")
            Exit Sub
        End If

        ' Pengecekan apakah nilai dari ComboBox2 atau ComboBox3 sudah dipilih
        Dim nilai As String = ""
        If ComboBox2.SelectedItem IsNot Nothing AndAlso ComboBox3.SelectedItem IsNot Nothing Then
            MsgBox("Harap pilih hanya satu nilai dari ComboBox2 atau ComboBox3.")
            Exit Sub
        ElseIf ComboBox2.SelectedItem IsNot Nothing Then
            nilai = ComboBox2.SelectedItem.ToString()
        ElseIf ComboBox3.SelectedItem IsNot Nothing Then
            nilai = ComboBox3.SelectedItem.ToString()
        Else
            MsgBox("Harap pilih nilai dari ComboBox2 atau ComboBox3.")
            Exit Sub
        End If

        ' Mendapatkan bobot dari TextBox1
        Dim bobot As String = TextBox1.Text

        ' Pengecekan apakah semua kolom terisi
        If String.IsNullOrWhiteSpace(bobot) Then
            MsgBox("Harap lengkapi semua kolom.")
            Exit Sub
        End If

        Try
            Call koneksi()

            ' Pengecekan apakah nilai yang sama untuk kode kriteria tersebut sudah ada dalam database
            Dim cmdCheckDuplicate As New MySqlCommand("SELECT COUNT(*) FROM crips WHERE crips_kriteria = @crips AND nilai = @nilai", conn)
            cmdCheckDuplicate.Parameters.AddWithValue("@crips", crips_kriteria)
            cmdCheckDuplicate.Parameters.AddWithValue("@nilai", nilai)
            Dim countDuplicate As Integer = Convert.ToInt32(cmdCheckDuplicate.ExecuteScalar())

            If countDuplicate > 0 Then
                MsgBox("Data dengan nilai yang sama untuk kode kriteria tersebut sudah ada dalam database. Silakan masukkan nilai yang berbeda.")
                Exit Sub
            End If

            ' Insert data jika belum ada duplikasi nilai untuk kode kriteria yang sama
            Dim cmd As New MySqlCommand("INSERT INTO crips (crips_kriteria, nilai, bobot) VALUES (@crips, @nilai, @bobot)", conn)
            cmd.Parameters.AddWithValue("@crips", crips_kriteria)
            cmd.Parameters.AddWithValue("@nilai", nilai)
            cmd.Parameters.AddWithValue("@bobot", bobot)

            cmd.ExecuteNonQuery()

            MsgBox("Data berhasil disimpan.")

            ' Mengosongkan kolom setelah data berhasil disimpan
            ComboBox1.SelectedIndex = -1 ' Mengosongkan ComboBox1
            ComboBox2.SelectedIndex = -1 ' Mengosongkan ComboBox2
            ComboBox3.SelectedIndex = -1 ' Mengosongkan ComboBox3
            TextBox1.Text = "" ' Mengosongkan TextBox1

        Catch ex As Exception
            MsgBox("Gagal menyimpan data: " & ex.Message)
        Finally
            tampil() ' Refresh DataGridView
            conn.Close()
        End Try



    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Mendapatkan kode kriteria dari input pengguna
        Dim crips_kriteria As String = ComboBox1.SelectedItem?.ToString()

        ' Pengecekan apakah kode kriteria tidak kosong
        If String.IsNullOrWhiteSpace(crips_kriteria) Then
            MsgBox("Kode Kriteria harus dipilih.")
            Exit Sub
        End If

        ' Pengecekan apakah nilai dari ComboBox2 atau ComboBox3 sudah dipilih
        Dim nilai As String = Nothing
        If ComboBox2.SelectedItem IsNot Nothing Then
            nilai = ComboBox2.SelectedItem.ToString()
        ElseIf ComboBox3.SelectedItem IsNot Nothing Then
            nilai = ComboBox3.SelectedItem.ToString()
        Else
            MsgBox("Harap pilih nilai dari ComboBox2 atau ComboBox3.")
            Exit Sub
        End If

        ' Mendapatkan bobot dari TextBox1
        Dim bobot As String = TextBox1.Text

        ' Pengecekan apakah semua kolom terisi
        If String.IsNullOrWhiteSpace(bobot) Then
            MsgBox("Harap lengkapi semua kolom.")
            Exit Sub
        End If

        Try
            Call koneksi()

            ' Mendapatkan bobot yang ada dalam database untuk data yang akan diupdate
            Dim cmdGetExistingBobot As New MySqlCommand("SELECT bobot FROM crips WHERE crips_kriteria = @crips AND nilai = @nilai", conn)
            cmdGetExistingBobot.Parameters.AddWithValue("@crips", crips_kriteria)
            cmdGetExistingBobot.Parameters.AddWithValue("@nilai", nilai)
            Dim existingBobot As String = cmdGetExistingBobot.ExecuteScalar()?.ToString()

            ' Pengecekan apakah bobot baru sama dengan bobot yang ada dalam database
            If existingBobot IsNot Nothing AndAlso existingBobot.Equals(bobot) Then
                MsgBox("Bobot yang dimasukkan sama dengan bobot yang sudah ada dalam database. Tidak ada yang perlu diupdate.")
            Else
                ' Update data jika bobot baru tidak sama dengan bobot yang ada dalam database
                Dim cmdUpdate As New MySqlCommand("UPDATE crips SET bobot = @bobot WHERE crips_kriteria = @crips AND (nilai = @nilai1 OR nilai = @nilai2)", conn)
                cmdUpdate.Parameters.AddWithValue("@bobot", bobot)
                cmdUpdate.Parameters.AddWithValue("@crips", crips_kriteria)
                cmdUpdate.Parameters.AddWithValue("@nilai1", If(ComboBox2.SelectedItem IsNot Nothing, ComboBox2.SelectedItem.ToString(), ""))
                cmdUpdate.Parameters.AddWithValue("@nilai2", If(ComboBox3.SelectedItem IsNot Nothing, ComboBox3.SelectedItem.ToString(), ""))

                Dim rowsAffected As Integer = cmdUpdate.ExecuteNonQuery()

                If rowsAffected > 0 Then
                    MsgBox("Data berhasil diupdate.")

                    ' Mengosongkan kolom setelah data berhasil diupdate
                    ComboBox1.SelectedIndex = -1 ' Mengosongkan ComboBox1
                    ComboBox2.SelectedIndex = -1 ' Mengosongkan ComboBox2
                    ComboBox3.SelectedIndex = -1 ' Mengosongkan ComboBox3
                    TextBox1.Text = "" ' Mengosongkan TextBox1
                End If
            End If

        Catch ex As Exception
            MsgBox("Gagal menyimpan data: " & ex.Message)
        Finally
            tampil() ' Refresh DataGridView
            conn.Close()
        End Try

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' Pengecekan apakah kolom ID Kriteria kosong
        If ComboBox1.SelectedItem Is Nothing Then
            MsgBox("Kode kriteria harus dipilih.")
            Exit Sub
        End If

        ' Konfirmasi penghapusan data dari pengguna
        Dim confirmResult As DialogResult = MessageBox.Show("Yakin data mau dihapus?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        ' Jika pengguna memilih 'Yes', lanjutkan dengan penghapusan data
        If confirmResult = DialogResult.Yes Then
            Try
                Call koneksi()

                ' Pengecekan apakah ID Kriteria ada dalam database
                Dim cmdCheck As New MySqlCommand("SELECT COUNT(*) FROM crips WHERE crips_kriteria = @crips", conn)
                cmdCheck.Parameters.AddWithValue("@crips", ComboBox1.SelectedItem.ToString())
                Dim count As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())

                If count = 0 Then
                    MsgBox("ID Kriteria tidak ditemukan dalam database.")
                    Exit Sub
                End If

                ' Buka koneksi untuk operasi penghapusan data
                Dim hapus As String = "DELETE FROM crips WHERE crips_kriteria = @crips"
                Dim cmdDelete As New MySqlCommand(hapus, conn)
                cmdDelete.Parameters.AddWithValue("@crips", ComboBox1.SelectedItem.ToString())
                cmdDelete.ExecuteNonQuery()

                MsgBox("Data berhasil dihapus.")
                tampil() ' Refresh DataGridView

            Catch ex As Exception
                MsgBox("Terjadi kesalahan: " & ex.Message)
            Finally
                tutupdb() ' Tutup koneksi
            End Try
        End If

    End Sub


End Class