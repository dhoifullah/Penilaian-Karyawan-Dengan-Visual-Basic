Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient

Public Class Atribut
    Private Sub Atribut_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tampil()
        MunculodeAnggota()
    End Sub
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ' Membersihkan semua bidang input
        ComboBox1.SelectedItem = Nothing
        ComboBox2.SelectedItem = Nothing

    End Sub
    Sub tampil()
        Call koneksi()
        da = New MySqlDataAdapter("select * from atribut_kriteria", conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "atribut_kriteria")
        dgv.DataSource = ds.Tables("atribut_kriteria")
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim kode_atribut_kriteria As String = ComboBox1.SelectedItem?.ToString()
        Dim atribut As String = ComboBox2.SelectedItem?.ToString()

        ' Pengecekan apakah semua kolom terisi
        If String.IsNullOrWhiteSpace(kode_atribut_kriteria) OrElse String.IsNullOrWhiteSpace(atribut) Then
            MsgBox("Harap lengkapi semua kolom.")
            Exit Sub
        End If

        Try
            Call koneksi()

            ' Pengecekan apakah data sudah ada dalam database
            Dim cmdCheck As New MySqlCommand("SELECT COUNT(*) FROM atribut_kriteria WHERE kode_atribut_kriteria = @kode_kriteria AND atribut = @atribut", conn)
            cmdCheck.Parameters.AddWithValue("@kode_kriteria", kode_atribut_kriteria)
            cmdCheck.Parameters.AddWithValue("@atribut", atribut)
            Dim count As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())

            If count > 0 Then
                MsgBox("Data untuk kode kriteria dan atribut tersebut sudah ada dalam database. Gunakan tombol Update untuk mengubah data.")
                Exit Sub
            End If

            ' Insert data jika data belum ada dalam database
            Dim cmd As New MySqlCommand("INSERT INTO atribut_kriteria (kode_atribut_kriteria, atribut) VALUES (@kode_kriteria, @atribut)", conn)
            cmd.Parameters.AddWithValue("@kode_kriteria", kode_atribut_kriteria)
            cmd.Parameters.AddWithValue("@atribut", atribut)

            cmd.ExecuteNonQuery()

            MsgBox("Data berhasil disimpan.")
            tampil() ' Refresh DataGridView
        Catch ex As Exception
            MsgBox("Gagal menyimpan data: " & ex.Message)
        Finally
            conn.Close()
        End Try


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim kode_atribut_kriteria As String = ComboBox1.SelectedItem?.ToString()
        Dim atribut As String = ComboBox2.SelectedItem?.ToString()

        ' Pengecekan apakah semua kolom terisi
        If String.IsNullOrWhiteSpace(kode_atribut_kriteria) OrElse String.IsNullOrWhiteSpace(atribut) Then
            MsgBox("Harap lengkapi semua kolom.")
            Exit Sub
        End If

        Try
            Call koneksi()

            ' Pengecekan apakah data sudah ada dalam database
            Dim cmdCheck As New MySqlCommand("SELECT COUNT(*) FROM atribut_kriteria WHERE kode_atribut_kriteria = @kode_kriteria AND atribut = @atribut", conn)
            cmdCheck.Parameters.AddWithValue("@kode_kriteria", kode_atribut_kriteria)
            cmdCheck.Parameters.AddWithValue("@atribut", atribut)
            Dim count As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())

            If count = 0 Then
                MsgBox("Data untuk kode kriteria dan atribut tersebut tidak ada dalam database. Gunakan tombol Simpan untuk menambah data baru.")
                Exit Sub
            End If

            ' Update data jika data sudah ada dalam database
            Dim cmd As New MySqlCommand("UPDATE atribut_kriteria SET atribut = @atribut WHERE kode_atribut_kriteria = @kode_kriteria", conn)
            cmd.Parameters.AddWithValue("@atribut", atribut)
            cmd.Parameters.AddWithValue("@kode_kriteria", kode_atribut_kriteria)

            cmd.ExecuteNonQuery()

            MsgBox("Data berhasil diupdate.")
            tampil() ' Refresh DataGridView
        Catch ex As Exception
            MsgBox("Gagal mengupdate data: " & ex.Message)
        Finally
            conn.Close()
        End Try

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim kode_atribut_kriteria As String = ComboBox1.SelectedItem?.ToString()
        Dim atribut As String = ComboBox2.SelectedItem?.ToString()

        ' Pengecekan apakah semua kolom terisi
        If String.IsNullOrWhiteSpace(kode_atribut_kriteria) OrElse String.IsNullOrWhiteSpace(atribut) Then
            MsgBox("Harap lengkapi semua kolom.")
            Exit Sub
        End If

        Try
            Call koneksi()

            ' Pengecekan apakah data sudah ada dalam database
            Dim cmdCheck As New MySqlCommand("SELECT COUNT(*) FROM atribut_kriteria WHERE kode_atribut_kriteria = @kode_kriteria AND atribut = @atribut", conn)
            cmdCheck.Parameters.AddWithValue("@kode_kriteria", kode_atribut_kriteria)
            cmdCheck.Parameters.AddWithValue("@atribut", atribut)
            Dim count As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())

            If count = 0 Then
                MsgBox("Data untuk kode kriteria dan atribut tersebut tidak ada dalam database.")
                Exit Sub
            End If

            ' Hapus data jika data sudah ada dalam database
            Dim cmd As New MySqlCommand("DELETE FROM atribut_kriteria WHERE kode_atribut_kriteria = @kode_kriteria AND atribut = @atribut", conn)
            cmd.Parameters.AddWithValue("@kode_kriteria", kode_atribut_kriteria)
            cmd.Parameters.AddWithValue("@atribut", atribut)

            cmd.ExecuteNonQuery()

            MsgBox("Data berhasil dihapus.")
            tampil() ' Refresh DataGridView
        Catch ex As Exception
            MsgBox("Gagal menghapus data: " & ex.Message)
        Finally
            conn.Close()
        End Try

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
    End Sub
End Class