Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient

Public Class Karyawan
    Private Sub Karyawan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tampilkaryawan()
    End Sub
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ' Membersihkan semua bidang input
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""

    End Sub


    Sub tampilkaryawan()
        Call koneksi()
        da = New MySqlDataAdapter("select * from karyawan", conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "karyawan")
        dgv.DataSource = ds.Tables("karyawan")
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv.RowsDefaultCellStyle.BackColor = Color.LightBlue
        dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgv.ReadOnly = True
        tutupdb()

    End Sub
    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Try
                Call koneksi()

                Dim idKaryawan As String = TextBox1.Text.Trim()

                ' Periksa apakah ID karyawan telah diisi
                If String.IsNullOrWhiteSpace(idKaryawan) Then
                    MessageBox.Show("Masukkan ID karyawan untuk pencarian.")
                    Exit Sub
                End If

                Dim query As String = "SELECT * FROM karyawan WHERE id = @id"
                Dim cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@id", idKaryawan)
                dr = cmd.ExecuteReader()

                If dr.Read() Then
                    TextBox2.Text = dr("nama").ToString()
                    TextBox3.Text = dr("no_telepon").ToString()
                    TextBox4.Text = dr("alamat").ToString()
                    TextBox5.Text = dr("bagian_kerja").ToString()
                Else
                    MessageBox.Show("Data karyawan dengan ID tersebut tidak ditemukan.")
                    TextBox2.Clear()
                    TextBox3.Clear()
                    TextBox4.Clear()
                    TextBox5.Clear()
                End If

                dr.Close()
                conn.Close()

            Catch ex As Exception
                MessageBox.Show("Terjadi kesalahan: " & ex.Message)
            Finally
                tutupdb()
            End Try
        End If
    End Sub


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Call koneksi()

            Dim idKaryawan As String = TextBox1.Text.Trim()
            Dim namaKaryawan As String = TextBox2.Text.Trim()
            Dim noTelepon As String = TextBox3.Text.Trim()
            Dim alamatKaryawan As String = TextBox4.Text.Trim()
            Dim bagianKerja As String = TextBox5.Text.Trim()

            ' Periksa apakah semua kolom telah diisi
            If String.IsNullOrWhiteSpace(idKaryawan) OrElse String.IsNullOrWhiteSpace(namaKaryawan) OrElse String.IsNullOrWhiteSpace(noTelepon) OrElse String.IsNullOrWhiteSpace(alamatKaryawan) OrElse String.IsNullOrWhiteSpace(bagianKerja) Then
                MessageBox.Show("Semua kolom harus diisi.")
                Exit Sub
            End If

            ' Periksa apakah ID karyawan sudah ada di database
            Dim queryCheck As String = "SELECT COUNT(*) FROM karyawan WHERE id = @id"
            Dim cmdCheck As New MySqlCommand(queryCheck, conn)
            cmdCheck.Parameters.AddWithValue("@id", idKaryawan)
            Dim count As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())

            If count > 0 Then
                MessageBox.Show("ID karyawan sudah ada. Gunakan tombol Ubah untuk mengedit data.")
                Exit Sub
            End If

            ' Simpan data karyawan baru ke dalam database
            Dim queryInsert As String = "INSERT INTO karyawan (id, nama, no_telepon, alamat, bagian_kerja) VALUES (@id, @nama, @noTelepon, @alamat, @bagianKerja)"
            Dim cmdInsert As New MySqlCommand(queryInsert, conn)
            cmdInsert.Parameters.AddWithValue("@id", idKaryawan)
            cmdInsert.Parameters.AddWithValue("@nama", namaKaryawan)
            cmdInsert.Parameters.AddWithValue("@noTelepon", noTelepon)
            cmdInsert.Parameters.AddWithValue("@alamat", alamatKaryawan)
            cmdInsert.Parameters.AddWithValue("@bagianKerja", bagianKerja)
            cmdInsert.ExecuteNonQuery()

            MessageBox.Show("Data karyawan berhasil disimpan.")

            ' Bersihkan TextBox setelah penyimpanan berhasil
            ClearTextBoxes()

        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message)
        Finally
            tampilkaryawan()
            tutupdb()
        End Try
    End Sub

    Private Sub ClearTextBoxes()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Call koneksi()

            ' Pengecekan apakah semua kolom kosong
            If String.IsNullOrWhiteSpace(TextBox1.Text) OrElse String.IsNullOrWhiteSpace(TextBox2.Text) OrElse String.IsNullOrWhiteSpace(TextBox3.Text) OrElse String.IsNullOrWhiteSpace(TextBox4.Text) OrElse String.IsNullOrWhiteSpace(TextBox5.Text) Then
                MessageBox.Show("Semua kolom harus diisi.")
                Exit Sub
            End If

            Dim edit As String

            Dim idKaryawan As String = TextBox1.Text.Trim()

            ' Periksa apakah data karyawan dengan ID tersebut ada dalam database
            Dim queryCheck As String = "SELECT COUNT(*) FROM karyawan WHERE id = @id"
            Dim cmdCheck As New MySqlCommand(queryCheck, conn)
            cmdCheck.Parameters.AddWithValue("@id", idKaryawan)
            Dim count As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())

            If count = 0 Then
                MessageBox.Show("ID karyawan tidak ditemukan. Gunakan tombol Simpan untuk menyimpan data baru.")
                Exit Sub
            End If

            ' Lakukan operasi update data karyawan
            edit = "UPDATE karyawan SET nama = @nama, no_telepon = @no_telepon, alamat = @alamat, bagian_kerja = @bagian_kerja WHERE id = @id"
            Dim cmdUpdate As New MySqlCommand(edit, conn)
            cmdUpdate.Parameters.AddWithValue("@nama", TextBox2.Text)
            cmdUpdate.Parameters.AddWithValue("@no_telepon", TextBox3.Text)
            cmdUpdate.Parameters.AddWithValue("@alamat", TextBox4.Text)
            cmdUpdate.Parameters.AddWithValue("@bagian_kerja", TextBox5.Text)
            cmdUpdate.Parameters.AddWithValue("@id", idKaryawan)
            cmdUpdate.ExecuteNonQuery()

            MsgBox("Data berhasil diubah.")
            ClearTextBoxes()

        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message)
        Finally
            tampilkaryawan()
            tutupdb()
        End Try


    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            Call koneksi()

            Dim idKaryawan As String = TextBox1.Text.Trim()

            ' Periksa apakah ID karyawan telah diisi
            If String.IsNullOrWhiteSpace(idKaryawan) Then
                MessageBox.Show("Masukkan ID karyawan yang akan dihapus.")
                Exit Sub
            End If

            ' Periksa apakah data karyawan dengan ID tersebut ada dalam database
            Dim queryCheck As String = "SELECT * FROM karyawan WHERE id = @id"
            Dim cmdCheck As New MySqlCommand(queryCheck, conn)
            cmdCheck.Parameters.AddWithValue("@id", idKaryawan)
            Dim drCheck As MySqlDataReader = cmdCheck.ExecuteReader()

            If Not drCheck.HasRows Then
                MessageBox.Show("Data karyawan dengan ID tersebut tidak ditemukan.")
                drCheck.Close()
                Exit Sub
            End If

            drCheck.Close()

            ' Konfirmasi penghapusan data
            If MessageBox.Show("Yakin ingin menghapus data karyawan dengan ID " & idKaryawan & "?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Dim queryDelete As String = "DELETE FROM karyawan WHERE id = @id"
                Dim cmdDelete As New MySqlCommand(queryDelete, conn)
                cmdDelete.Parameters.AddWithValue("@id", idKaryawan)
                cmdDelete.ExecuteNonQuery()

                MessageBox.Show("Data karyawan berhasil dihapus.")
                TextBox1.Clear()
                TextBox2.Clear()
                TextBox3.Clear()
                TextBox4.Clear()
                TextBox5.Clear()
            End If

            conn.Close()

        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message)
        Finally
            tampilkaryawan()
            tutupdb()
        End Try
    End Sub
End Class